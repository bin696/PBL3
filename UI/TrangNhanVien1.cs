using PBL3.DataBase;
using PBL3.UI;
using System.Data;
using System.Data.SqlClient;

namespace PBL3
{
    public partial class TrangNhanVien1 : Form
    {
        private DataTable? _nhanVienTable;
        private bool _isEditingExisting;
        private string? _selectedMaNvDbValue;
        private BanHang? _banHangEmbedded;
        private bool _isNavigating;

        private string _loggedInMaNV;
        private TextBox? _txtEmail;

        public TrangNhanVien1()
        {
            _loggedInMaNV = "1";
            InitializeComponent();
            _cboTimTheo.SelectionChangeCommitted += SearchControl_Changed;
        }

        public TrangNhanVien1(string maNV) : this()
        {
            _loggedInMaNV = maNV;
        }

        private void TrangNhanVien1_Load(object? sender, EventArgs e)
        {
            try
            {
                EnsureProfileExtraControls();
                pnlDanhSachNhanVien.Visible = false;
                lblTrangThai.Visible = false;
                _cboTrangThai.Visible = false;

                _cboChucVu.Enabled = false;
                btn_QLNV.BackColor = Color.Salmon;
                label4.ForeColor = Color.White;

                LoadChucVu();
                LoadNhanVien();
                _isEditingExisting = true;
                UpdatePasswordUiState();
                EnsureLeaveRequestButtons();
                ConfigureLichSuYeuCauGrid();
                LoadLichSuYeuCau();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không thể tải dữ liệu nhân viên.\n{ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadChucVu()
        {
            using SqlConnection conn = DbHelper.GetConnection();
            using SqlCommand cmd = new SqlCommand("SELECT MaCV, TenCV FROM dbo.CHUC_VU ORDER BY MaCV", conn);
            using SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();
            da.Fill(dt);

            _cboChucVu.DataSource = dt;
            _cboChucVu.DisplayMember = "TenCV";
            _cboChucVu.ValueMember = "MaCV";
        }

        private void LoadNhanVien()
        {
            const string sql = @"
SELECT nv.MaNV, nv.HoTen, nv.NgaySinh, nv.SDT, nv.Email, nv.DiaChi, nv.MatKhau, nv.TrangThai, nv.MaCV
FROM dbo.NHAN_VIEN nv
WHERE nv.MaNV = @MaNV";

            using SqlConnection conn = DbHelper.GetConnection();
            using SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add("@MaNV", SqlDbType.VarChar).Value = _loggedInMaNV;

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                _selectedMaNvDbValue = Convert.ToString(reader["MaNV"]);
                _txtMaNV.Text = FormatMaNvForDisplay(_selectedMaNvDbValue);
                _txtMaNV.ReadOnly = true;
                _txtHoTen.Text = Convert.ToString(reader["HoTen"]);
                _txtSdt.Text = Convert.ToString(reader["SDT"]);
                if (_txtEmail is not null)
                {
                    _txtEmail.Text = Convert.ToString(reader["Email"]);
                }
                _txtDiaChi.Text = Convert.ToString(reader["DiaChi"]);

                if (DateTime.TryParse(Convert.ToString(reader["NgaySinh"]), out DateTime ngaySinh))
                {
                    _dtpNgaySinh.Value = ngaySinh;
                }

                string maCv = Convert.ToString(reader["MaCV"]) ?? string.Empty;
                if (!string.IsNullOrWhiteSpace(maCv))
                {
                    _cboChucVu.SelectedValue = maCv;
                }
            }
        }

        private void EnsureCreateModeUi()
        {
            _isEditingExisting = false;
            UpdatePasswordUiState();
        }

        private void ConfigureGridAppearance()
        {
            _dgvNhanVien.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
            _dgvNhanVien.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;

            DataGridViewColumn? ngaySinhColumn = _dgvNhanVien.Columns["NgaySinh"];
            if (ngaySinhColumn is not null)
            {
                ngaySinhColumn.DefaultCellStyle.Format = "dd/MM/yyyy";
            }

            CenterColumn("MaNV");
            CenterColumn("SDT");
            CenterColumn("TrangThai");
            CenterColumn("TenCV");
            CenterColumn("NgaySinh");

            LeftColumn("HoTen");
            LeftColumn("DiaChi");
        }

        private void CenterColumn(string columnName)
        {
            DataGridViewColumn? column = _dgvNhanVien.Columns[columnName];
            if (column is not null)
            {
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        private void LeftColumn(string columnName)
        {
            DataGridViewColumn? column = _dgvNhanVien.Columns[columnName];
            if (column is not null)
            {
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            }
        }

        private void SearchControl_Changed(object? sender, EventArgs e)
        {
            ApplySearchFilter();
        }

        private void ApplySearchFilter()
        {
            if (_nhanVienTable is null)
            {
                return;
            }

            string keyword = _txtTimKiem.Text.Trim().Replace("'", "''");
            if (string.IsNullOrWhiteSpace(keyword))
            {
                _nhanVienTable.DefaultView.RowFilter = string.Empty;
                return;
            }

            string selected = (Convert.ToString(_cboTimTheo.SelectedItem) ?? "MãNV").Trim();
            string filter = selected switch
            {
                "HọTên" => $"HoTen LIKE '%{keyword}%'",
                "NgàySinh" => $"Convert(NgaySinh, 'System.String') LIKE '%{keyword}%'",
                "SĐT" => $"SDT LIKE '%{keyword}%'",
                "ĐịaChỉ" => $"DiaChi LIKE '%{keyword}%'",
                "ChứcVụ" => $"TenCV LIKE '%{keyword}%'",
                "TrạngThái" => $"TrangThai LIKE '%{keyword}%'",
                _ => $"Convert(MaNV, 'System.String') LIKE '%{keyword}%'"
            };

            _nhanVienTable.DefaultView.RowFilter = filter;
        }

        private void SetColumnWidth(string columnName, int width)
        {
            DataGridViewColumn? column = _dgvNhanVien.Columns[columnName];
            if (column is not null)
            {
                column.Width = width;
            }
        }

        private void SetHeaderText(string columnName, string headerText)
        {
            DataGridViewColumn? column = _dgvNhanVien.Columns[columnName];
            if (column is not null)
            {
                column.HeaderText = headerText;
            }
        }

        private void DgvNhanVien_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            DataGridViewRow row = _dgvNhanVien.Rows[e.RowIndex];
            _selectedMaNvDbValue = Convert.ToString(row.Cells["MaNV"].Value) ?? string.Empty;
            _txtMaNV.Text = FormatMaNvForDisplay(_selectedMaNvDbValue);
            _txtHoTen.Text = Convert.ToString(row.Cells["HoTen"].Value) ?? string.Empty;
            _txtSdt.Text = Convert.ToString(row.Cells["SDT"].Value) ?? string.Empty;
            _txtDiaChi.Text = Convert.ToString(row.Cells["DiaChi"].Value) ?? string.Empty;
            _cboTrangThai.Text = Convert.ToString(row.Cells["TrangThai"].Value) ?? "Đang làm";

            if (DateTime.TryParse(Convert.ToString(row.Cells["NgaySinh"].Value), out DateTime ngaySinh))
            {
                _dtpNgaySinh.Value = ngaySinh;
            }

            string maCv = Convert.ToString(row.Cells["MaCV"].Value) ?? string.Empty;
            if (!string.IsNullOrWhiteSpace(maCv))
            {
                _cboChucVu.SelectedValue = maCv;
            }

            _isEditingExisting = true;
            UpdatePasswordUiState();
        }

        private bool ValidateInput(bool isInsert)
        {
            if (!isInsert && string.IsNullOrWhiteSpace(_txtMaNV.Text))
            {
                MessageBox.Show("Không tạo được mã nhân viên tự động.", "Thiếu dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(_txtHoTen.Text))
            {
                MessageBox.Show("Vui lòng nhập họ tên.", "Thiếu dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(_txtSdt.Text))
            {
                MessageBox.Show("Vui lòng nhập số điện thoại.", "Thiếu dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!long.TryParse(_txtSdt.Text.Trim(), out _))
            {
                MessageBox.Show("Số điện thoại không hợp lệ.", "Dữ liệu sai", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            string email = _txtEmail?.Text.Trim() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(email) || !IsValidEmail(email))
            {
                MessageBox.Show("Email không hợp lệ.", "Dữ liệu sai", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (IsPhoneExists(isInsert, _txtSdt.Text.Trim()))
            {
                MessageBox.Show("Số điện thoại đã tồn tại trong hệ thống.", "Dữ liệu trùng lặp", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (_cboChucVu.SelectedValue is null)
            {
                MessageBox.Show("Vui lòng chọn chức vụ.", "Thiếu dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private bool IsPhoneExists(bool isInsert, string phone)
        {
            string query = isInsert ? "SELECT COUNT(1) FROM dbo.NHAN_VIEN WHERE SDT = @SDT"
                                    : "SELECT COUNT(1) FROM dbo.NHAN_VIEN WHERE SDT = @SDT AND MaNV != @ID";
            try
            {
                using SqlConnection conn = DbHelper.GetConnection();
                using SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.Add("@SDT", SqlDbType.VarChar).Value = phone;
                if (!isInsert)
                {
                    cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = _selectedMaNvDbValue ?? _txtMaNV.Text.Trim();
                }
                conn.Open();
                return (int)cmd.ExecuteScalar() > 0;
            }
            catch
            {
                return false;
            }
        }

        private void BtnThem_Click(object? sender, EventArgs e)
        {
            if (!ValidateInput(true))
            {
                return;
            }
            try
            {
                using SqlConnection conn = DbHelper.GetConnection();
                conn.Open();

                int nextId = 1;
                using (SqlCommand cmdGet = new SqlCommand("SELECT MaNV FROM dbo.NHAN_VIEN ORDER BY TRY_CAST(CASE WHEN CONVERT(VARCHAR(20), MaNV) LIKE 'NV%' THEN SUBSTRING(CONVERT(VARCHAR(20), MaNV), 3, LEN(CONVERT(VARCHAR(20), MaNV)) - 2) ELSE CONVERT(VARCHAR(20), MaNV) END AS INT)", conn))
                using (SqlDataReader reader = cmdGet.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader.IsDBNull(0))
                            continue;

                        string raw = Convert.ToString(reader.GetValue(0)) ?? string.Empty;
                        string numeric = raw.StartsWith("NV", StringComparison.OrdinalIgnoreCase) ? raw.Substring(2) : raw;
                        if (!int.TryParse(numeric, out int v))
                            continue;

                        if (v == nextId)
                        {
                            nextId++;
                        }
                        else if (v > nextId)
                        {
                            break;
                        }
                    }
                }

                bool hasIdentity = false;
                using (SqlCommand cmdCheck = new SqlCommand("SELECT CASE WHEN COLUMNPROPERTY(OBJECT_ID('dbo.NHAN_VIEN'),'MaNV','IsIdentity') = 1 THEN 1 ELSE 0 END", conn))
                {
                    hasIdentity = Convert.ToInt32(cmdCheck.ExecuteScalar() ?? 0) == 1;
                }

                using SqlTransaction tran = conn.BeginTransaction();
                try
                {
                    if (hasIdentity)
                    {
                        // Insert explicit MaNV using IDENTITY_INSERT so we can reuse gaps
                        using SqlCommand cmd = new SqlCommand("SET IDENTITY_INSERT dbo.NHAN_VIEN ON; INSERT INTO dbo.NHAN_VIEN (MaNV, HoTen, NgaySinh, SDT, DiaChi, MatKhau, TrangThai, MaCV) VALUES (@MaNV, @HoTen, @NgaySinh, @SDT, @DiaChi, @MatKhau, @TrangThai, @MaCV); SET IDENTITY_INSERT dbo.NHAN_VIEN OFF;", conn, tran);
                        cmd.Parameters.Add("@MaNV", SqlDbType.Int).Value = nextId;
                        cmd.Parameters.Add("@HoTen", SqlDbType.NVarChar, 100).Value = _txtHoTen.Text.Trim();
                        cmd.Parameters.Add("@NgaySinh", SqlDbType.Date).Value = _dtpNgaySinh.Value.Date;
                        cmd.Parameters.Add("@SDT", SqlDbType.VarChar, 20).Value = _txtSdt.Text.Trim();
                        cmd.Parameters.Add("@DiaChi", SqlDbType.NVarChar, 200).Value = _txtDiaChi.Text.Trim();
                        cmd.Parameters.Add("@MatKhau", SqlDbType.NVarChar, 100).Value = string.Empty;
                        cmd.Parameters.Add("@TrangThai", SqlDbType.NVarChar, 30).Value = _cboTrangThai.Text.Trim();
                        cmd.Parameters.Add("@MaCV", SqlDbType.VarChar, 20).Value = _cboChucVu.SelectedValue?.ToString() ?? string.Empty;
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        using SqlCommand cmd = new SqlCommand("INSERT INTO dbo.NHAN_VIEN (MaNV, HoTen, NgaySinh, SDT, DiaChi, MatKhau, TrangThai, MaCV) VALUES (@MaNV, @HoTen, @NgaySinh, @SDT, @DiaChi, @MatKhau, @TrangThai, @MaCV)", conn, tran);
                        cmd.Parameters.Add("@MaNV", SqlDbType.Int).Value = nextId;
                        cmd.Parameters.Add("@HoTen", SqlDbType.NVarChar, 100).Value = _txtHoTen.Text.Trim();
                        cmd.Parameters.Add("@NgaySinh", SqlDbType.Date).Value = _dtpNgaySinh.Value.Date;
                        cmd.Parameters.Add("@SDT", SqlDbType.VarChar, 20).Value = _txtSdt.Text.Trim();
                        cmd.Parameters.Add("@DiaChi", SqlDbType.NVarChar, 200).Value = _txtDiaChi.Text.Trim();
                        cmd.Parameters.Add("@MatKhau", SqlDbType.NVarChar, 100).Value = string.Empty;
                        cmd.Parameters.Add("@TrangThai", SqlDbType.NVarChar, 30).Value = _cboTrangThai.Text.Trim();
                        cmd.Parameters.Add("@MaCV", SqlDbType.VarChar, 20).Value = _cboChucVu.SelectedValue?.ToString() ?? string.Empty;
                        cmd.ExecuteNonQuery();
                    }

                    tran.Commit();
                }
                catch
                {
                    tran.Rollback();
                    throw;
                }

                MessageBox.Show("Thêm nhân viên thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadNhanVien();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Thêm nhân viên thất bại.\n{ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSua_Click(object? sender, EventArgs e)
        {
            if (!ValidateInput(false))
            {
                return;
            }

            const string sql = @"
UPDATE dbo.NHAN_VIEN
SET HoTen = @HoTen,
    NgaySinh = @NgaySinh,
    SDT = @SDT,
    Email = @Email,
    DiaChi = @DiaChi
WHERE MaNV = @MaNV";

            try
            {
                using SqlConnection conn = DbHelper.GetConnection();
                using SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add("@MaNV", SqlDbType.VarChar, 20).Value = _selectedMaNvDbValue ?? _txtMaNV.Text.Trim();
                cmd.Parameters.Add("@HoTen", SqlDbType.NVarChar, 100).Value = _txtHoTen.Text.Trim();
                cmd.Parameters.Add("@NgaySinh", SqlDbType.Date).Value = _dtpNgaySinh.Value.Date;
                cmd.Parameters.Add("@SDT", SqlDbType.VarChar, 20).Value = _txtSdt.Text.Trim();
                cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100).Value = (_txtEmail?.Text.Trim() ?? string.Empty);
                cmd.Parameters.Add("@DiaChi", SqlDbType.NVarChar, 200).Value = _txtDiaChi.Text.Trim();

                conn.Open();
                int rows = cmd.ExecuteNonQuery();

                if (rows == 0)
                {
                    MessageBox.Show("Không tìm thấy nhân viên để cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                MessageBox.Show("Cập nhật nhân viên thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadNhanVien();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Cập nhật nhân viên thất bại.\n{ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnXoa_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_txtMaNV.Text))
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần xóa.", "Thiếu dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
                "Bạn có chắc chắn muốn xóa nhân viên này?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
            {
                return;
            }

            const string sql = "DELETE FROM dbo.NHAN_VIEN WHERE MaNV = @MaNV";

            try
            {
                using SqlConnection conn = DbHelper.GetConnection();
                using SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add("@MaNV", SqlDbType.VarChar, 20).Value = _selectedMaNvDbValue ?? _txtMaNV.Text.Trim();

                conn.Open();
                int rows = cmd.ExecuteNonQuery();

                if (rows == 0)
                {
                    MessageBox.Show("Không tìm thấy nhân viên để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                MessageBox.Show("Xóa nhân viên thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadNhanVien();
                ClearForm();
            }
            catch (SqlException ex) when (ex.Number == 547)
            {
                MessageBox.Show("Không thể xóa nhân viên vì đang được sử dụng ở dữ liệu liên quan.", "Không thể xóa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Xóa nhân viên thất bại.\n{ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnLamMoi_Click(object? sender, EventArgs e)
        {
            ClearForm();
            LoadNhanVien();
            LoadLichSuYeuCau();
        }

        private void ClearForm()
        {
            _selectedMaNvDbValue = null;
            _txtMaNV.Text = GenerateNextMaNhanVien();
            _txtHoTen.Clear();
            _txtSdt.Clear();
            _txtDiaChi.Clear();
            _cboTrangThai.SelectedIndex = 0;
            _isEditingExisting = false;
            UpdatePasswordUiState();

            if (_cboChucVu.Items.Count > 0)
            {
                _cboChucVu.SelectedIndex = 0;
            }

            _dtpNgaySinh.Value = DateTime.Today;
            _txtHoTen.Focus();
        }

        private string GenerateNextMaNhanVien()
        {
            const string sql = @"SELECT MaNV FROM dbo.NHAN_VIEN
ORDER BY TRY_CAST(
    CASE
        WHEN CONVERT(VARCHAR(20), MaNV) LIKE 'NV%' THEN SUBSTRING(CONVERT(VARCHAR(20), MaNV), 3, LEN(CONVERT(VARCHAR(20), MaNV)) - 2)
        ELSE CONVERT(VARCHAR(20), MaNV)
    END AS INT)";

            try
            {
                using SqlConnection conn = DbHelper.GetConnection();
                conn.Open();
                using SqlCommand cmd = new SqlCommand(sql, conn);
                using SqlDataReader reader = cmd.ExecuteReader();
                int nextId = 1;
                while (reader.Read())
                {
                    if (reader.IsDBNull(0))
                        continue;

                    string raw = Convert.ToString(reader.GetValue(0)) ?? string.Empty;
                    if (!int.TryParse(raw.StartsWith("NV", StringComparison.OrdinalIgnoreCase) ? raw.Substring(2) : raw, out int v))
                        continue;

                    if (v == nextId)
                    {
                        nextId++;
                    }
                    else if (v > nextId)
                    {
                        break;
                    }
                }

                return $"NV{nextId}";
            }
            catch
            {
                return "NV1";
            }
        }

        private static string FormatMaNvForDisplay(string? maNvValue)
        {
            if (string.IsNullOrWhiteSpace(maNvValue))
            {
                return string.Empty;
            }

            string value = maNvValue.Trim();
            return value.StartsWith("NV", StringComparison.OrdinalIgnoreCase) ? value.ToUpperInvariant() : $"NV{value}";
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void roundedPanel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void btn_DangXuat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Bạn có chắc chắn muốn đăng xuất?",
                "Xác nhận đăng xuất",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                AdminNavigationManager.Logout(this);
            }
        }

        private void btn_DangXuat_MouseEnter(object sender, EventArgs e)
        {
            btn_DangXuat.BackColor = Color.FromArgb(255, 69, 0);
        }

        private void btn_DangXuat_MouseLeave(object sender, EventArgs e)
        {
            btn_DangXuat.BackColor = Color.LightSalmon;
        }

        private void roundedPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btn_ThongKe_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblHoTen_Click(object sender, EventArgs e)
        {

        }

        private void lblTrangThai_Click(object sender, EventArgs e)
        {

        }

        private void lblDiaChi_Click(object sender, EventArgs e)
        {

        }

        private void _cboTrangThai_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pnlSdtInput_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblNgaySinh_Click(object sender, EventArgs e)
        {

        }

        private void btn_QLNCC_Click(object? sender, EventArgs e)
        {
            OpenAndClose(new TrangHoaDon(_selectedMaNvDbValue ?? _loggedInMaNV));
        }

        private void btn_QLKH_Click(object? sender, EventArgs e)
        {
            OpenAndClose(new BanHang(_selectedMaNvDbValue ?? _loggedInMaNV));
        }

        private void btn_QLMA_Click(object? sender, EventArgs e)
        {
            OpenAndClose(new MuaHang(_selectedMaNvDbValue ?? _loggedInMaNV));
        }

        private void btn_QLHDN_Click(object? sender, EventArgs e)
        {
            OpenAndClose(new KhachHang(_selectedMaNvDbValue ?? _loggedInMaNV));
        }

        private void OpenAndClose(Form target)
        {
            AdminNavigationManager.Navigate(this, target);
        }

        private void ShowBanHangInRightPanel()
        {
            pnlFormNhanVien.Visible = false;
            pnlDanhSachNhanVien.Visible = false;
            lb_QLNhanVienTitle.Text = "Bán hàng";

            btn_QLNV.BackColor = Color.Bisque;
            label4.ForeColor = SystemColors.ControlText;
            btn_QLKH.BackColor = Color.Salmon;
            label6.ForeColor = Color.White;

            if (_banHangEmbedded is null || _banHangEmbedded.IsDisposed)
            {
                _banHangEmbedded = new BanHang(_selectedMaNvDbValue ?? _loggedInMaNV)
                {
                    TopLevel = false,
                    FormBorderStyle = FormBorderStyle.None,
                    Dock = DockStyle.Fill
                };
                hcnt_Khung.Controls.Add(_banHangEmbedded);
            }

            _banHangEmbedded.BringToFront();
            _banHangEmbedded.Show();
        }

        private void ShowThongTinCaNhanPanel()
        {
            if (_banHangEmbedded is not null && !_banHangEmbedded.IsDisposed)
            {
                _banHangEmbedded.Hide();
            }

            lb_QLNhanVienTitle.Text = "Thông tin cá nhân";
            pnlFormNhanVien.Visible = true;
            pnlDanhSachNhanVien.Visible = false;

            btn_QLNV.BackColor = Color.Salmon;
            label4.ForeColor = Color.White;
            btn_QLKH.BackColor = Color.Bisque;
            label6.ForeColor = SystemColors.ControlText;
        }

        private void UpdatePasswordUiState()
        {
            bool isCreateMode = !_isEditingExisting;
            _btnDatLaiMatKhau.Visible = !isCreateMode;
            _btnXemLichTruc.Visible = !isCreateMode;
        }

        private void ConfigureProfileLayout()
        {
            lb_QLNhanVienTitle.Location = new Point(306, 8);
            pnlFormNhanVien.Size = new Size(834, 640);

            lblMaNV.Location = new Point(40, 25);
            pnlMaNVInput.Location = new Point(40, 54);

            lblHoTen.Location = new Point(300, 25);
            pnlHoTenInput.Location = new Point(300, 54);
            pnlHoTenInput.Size = new Size(220, 33);
            _txtHoTen.Size = new Size(196, 20);

            lblNgaySinh.Location = new Point(570, 25);
            _dtpNgaySinh.Location = new Point(570, 56);
            _dtpNgaySinh.Size = new Size(210, 27);

            lblSdt.Location = new Point(40, 116);
            pnlSdtInput.Location = new Point(40, 145);
            pnlSdtInput.Size = new Size(220, 33);
            _txtSdt.Size = new Size(196, 20);

            lblDiaChi.Location = new Point(300, 116);
            pnlDiaChiInput.Location = new Point(300, 145);
            pnlDiaChiInput.Size = new Size(480, 33);
            _txtDiaChi.Size = new Size(456, 20);

            lblChucVu.Location = new Point(430, 156);
            _cboChucVu.Location = new Point(430, 178);
            _cboChucVu.Size = new Size(350, 28);

            _btnSua.Location = new Point(40, 300);
            _btnSua.Size = new Size(150, 38);
            lblBtnSua.Location = new Point(53, 7);

            _btnDatLaiMatKhau.Location = new Point(215, 300);
            _btnDatLaiMatKhau.Size = new Size(180, 38);
            _lblBtnDatLaiMatKhau.Location = new Point(40, 7);

            _btnXemLichTruc.Location = new Point(420, 300);
            _btnXemLichTruc.Size = new Size(160, 38);
            _lblBtnXemLichTruc.Location = new Point(22, 7);
        }

        private void EnsureLeaveRequestButtons()
        {
            _btnYeuCauNghiPhep.Visible = true;
            _btnYeuCauNghiHan.Visible = true;
            _btnYeuCauNghiPhep.BringToFront();
            _btnYeuCauNghiHan.BringToFront();
        }

        private void BtnYeuCauNghiPhep_Click(object? sender, EventArgs e)
        {
            GuiYeuCauNghi("Nghỉ phép", true);
        }

        private void BtnYeuCauNghiHan_Click(object? sender, EventArgs e)
        {
            GuiYeuCauNghi("Nghỉ hẳn", false);
        }

        private void GuiYeuCauNghi(string loaiYeuCau, bool hasDateRange)
        {
            if (string.IsNullOrWhiteSpace(_selectedMaNvDbValue))
            {
                MessageBox.Show("Không xác định được nhân viên đăng nhập.", "Thiếu dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!TryPromptLeaveReason(loaiYeuCau, hasDateRange, out string lyDo, out DateTime? tuNgay, out DateTime? denNgay, out int tongNgayNghi))
            {
                return;
            }

            DialogResult confirm = MessageBox.Show(
                $"Bạn chắc chắn muốn gửi '{loaiYeuCau}'?\n\nLý do: {lyDo}",
                "Xác nhận gửi yêu cầu",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes)
            {
                return;
            }

            int? maNvInt = ParseMaNvToInt(_selectedMaNvDbValue ?? _loggedInMaNV);
            if (maNvInt is null)
            {
                MessageBox.Show("Mã nhân viên không hợp lệ để gửi yêu cầu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            const string sql = @"
INSERT INTO dbo.YEU_CAU (MaNV, LoaiYeuCau, NoiDung, TuNgay, DenNgay, TrangThai, NgayGui, PhanHoiAdmin)
VALUES (@MaNV, @LoaiYeuCau, @NoiDung, @TuNgay, @DenNgay, 0, GETDATE(), N'')";

            try
            {
                using SqlConnection conn = DbHelper.GetConnection();
                using SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add("@MaNV", SqlDbType.Int).Value = maNvInt.Value;
                cmd.Parameters.Add("@LoaiYeuCau", SqlDbType.NVarChar, 50).Value = loaiYeuCau;

                cmd.Parameters.Add("@NoiDung", SqlDbType.NVarChar).Value = lyDo;
                cmd.Parameters.Add("@TuNgay", SqlDbType.Date).Value = (object?)tuNgay?.Date ?? DBNull.Value;
                cmd.Parameters.Add("@DenNgay", SqlDbType.Date).Value = (object?)denNgay?.Date ?? DBNull.Value;

                conn.Open();
                cmd.ExecuteNonQuery();

                MessageBox.Show("Đã gửi yêu cầu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadLichSuYeuCau();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không thể gửi yêu cầu.\n{ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigureLichSuYeuCauGrid()
        {
            dgvLichSuYeuCau.AutoGenerateColumns = false;
            dgvLichSuYeuCau.ReadOnly = true;
            dgvLichSuYeuCau.AllowUserToAddRows = false;
            dgvLichSuYeuCau.AllowUserToDeleteRows = false;
            dgvLichSuYeuCau.AllowUserToResizeRows = false;
            dgvLichSuYeuCau.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLichSuYeuCau.MultiSelect = false;
            dgvLichSuYeuCau.RowHeadersVisible = false;
            dgvLichSuYeuCau.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            if (dgvLichSuYeuCau.Columns.Count > 0)
            {
                return;
            }

            dgvLichSuYeuCau.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "STT",
                DataPropertyName = "STT",
                HeaderText = "STT",
                Width = 50,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            dgvLichSuYeuCau.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "NgayGui",
                DataPropertyName = "NgayGui",
                HeaderText = "Ngày gửi",
                Width = 95,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            dgvLichSuYeuCau.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Loai",
                DataPropertyName = "Loai",
                HeaderText = "Loại",
                Width = 100
            });

            dgvLichSuYeuCau.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TongNgay",
                DataPropertyName = "TongNgay",
                HeaderText = "Tổng ngày",
                Width = 80,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            dgvLichSuYeuCau.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TrangThai",
                DataPropertyName = "TrangThai",
                HeaderText = "Trạng thái",
                Width = 100
            });

            dgvLichSuYeuCau.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "PhanHoiAdmin",
                DataPropertyName = "PhanHoiAdmin",
                HeaderText = "Phản hồi từ Admin",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
        }

        private void LoadLichSuYeuCau()
        {
            int? maNvInt = ParseMaNvToInt(_selectedMaNvDbValue ?? _loggedInMaNV);
            if (maNvInt is null)
            {
                dgvLichSuYeuCau.DataSource = null;
                return;
            }

            const string sql = @"
SELECT NgayGui, LoaiYeuCau, TuNgay, DenNgay, TrangThai, ISNULL(PhanHoiAdmin, N'') AS PhanHoiAdmin
FROM dbo.YEU_CAU
WHERE MaNV = @MaNV
ORDER BY NgayGui DESC";

            try
            {
                using SqlConnection conn = DbHelper.GetConnection();
                using SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add("@MaNV", SqlDbType.Int).Value = maNvInt.Value;
                using SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable();
                da.Fill(dt);

                DataTable display = new DataTable();
                display.Columns.Add("STT", typeof(int));
                display.Columns.Add("NgayGui", typeof(string));
                display.Columns.Add("Loai", typeof(string));
                display.Columns.Add("TongNgay", typeof(string));
                display.Columns.Add("TrangThai", typeof(string));
                display.Columns.Add("PhanHoiAdmin", typeof(string));

                int stt = 1;
                foreach (DataRow row in dt.Rows)
                {
                    DateTime? ngayGui = row["NgayGui"] == DBNull.Value ? null : Convert.ToDateTime(row["NgayGui"]);
                    string loai = Convert.ToString(row["LoaiYeuCau"]) ?? string.Empty;
                    DateTime? tuNgay = row["TuNgay"] == DBNull.Value ? null : Convert.ToDateTime(row["TuNgay"]);
                    DateTime? denNgay = row["DenNgay"] == DBNull.Value ? null : Convert.ToDateTime(row["DenNgay"]);
                    int trangThai = row["TrangThai"] == DBNull.Value ? 0 : Convert.ToInt32(row["TrangThai"]);
                    string phanHoi = Convert.ToString(row["PhanHoiAdmin"]) ?? string.Empty;
                    string tongNgay = (tuNgay.HasValue && denNgay.HasValue)
                        ? ((denNgay.Value.Date - tuNgay.Value.Date).Days + 1).ToString()
                        : "-";

                    display.Rows.Add(
                        stt++,
                        ngayGui?.ToString("dd/MM/yyyy") ?? string.Empty,
                        NormalizeLoaiYeuCau(loai),
                        tongNgay,
                        RequestStateText(trangThai),
                        phanHoi);
                }

                dgvLichSuYeuCau.DataSource = display;
            }
            catch
            {
                dgvLichSuYeuCau.DataSource = null;
            }
        }

        private static string NormalizeLoaiYeuCau(string loai)
        {
            if (string.IsNullOrWhiteSpace(loai)) return string.Empty;
            string value = loai.Trim();
            if (value.Contains("nghỉ phép", StringComparison.OrdinalIgnoreCase)) return "Nghỉ phép";
            if (value.Contains("nghỉ hẳn", StringComparison.OrdinalIgnoreCase) || value.Contains("nghi han", StringComparison.OrdinalIgnoreCase)) return "Nghỉ hẳn";
            return value;
        }

        private static string RequestStateText(int state)
        {
            return state switch
            {
                1 => "Đã duyệt",
                2 => "Đã từ chối",
                _ => "Chờ duyệt"
            };
        }

        private static bool TryPromptLeaveReason(string loaiYeuCau, bool hasDateRange, out string lyDo, out DateTime? tuNgay, out DateTime? denNgay, out int tongNgayNghi)
        {
            lyDo = string.Empty;
            tuNgay = null;
            denNgay = null;
            tongNgayNghi = 0;

            using Form dialog = new Form
            {
                Text = loaiYeuCau,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterParent,
                MinimizeBox = false,
                MaximizeBox = false,
                ClientSize = hasDateRange ? new Size(430, 320) : new Size(430, 220)
            };

            Label lblReason = new Label { Text = "Nhập lý do:", Left = 12, Top = 15, AutoSize = true };
            TextBox txtReason = new TextBox
            {
                Left = 12,
                Top = 40,
                Width = 404,
                Height = hasDateRange ? 90 : 120,
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                MaxLength = 500
            };

            Label? lblTu = null;
            DateTimePicker? dtpTu = null;
            Label? lblDen = null;
            DateTimePicker? dtpDen = null;
            Label? lblTong = null;

            if (hasDateRange)
            {
                lblTu = new Label { Text = "Từ ngày", Left = 12, Top = 138, AutoSize = true };
                dtpTu = new DateTimePicker
                {
                    Left = 12,
                    Top = 160,
                    Width = 180,
                    Format = DateTimePickerFormat.Custom,
                    CustomFormat = "dd/MM/yyyy",
                    MinDate = DateTime.Today,
                    Value = DateTime.Today
                };

                lblDen = new Label { Text = "Đến ngày", Left = 210, Top = 138, AutoSize = true };
                dtpDen = new DateTimePicker
                {
                    Left = 210,
                    Top = 160,
                    Width = 180,
                    Format = DateTimePickerFormat.Custom,
                    CustomFormat = "dd/MM/yyyy",
                    MinDate = DateTime.Today,
                    Value = DateTime.Today
                };

                lblTong = new Label
                {
                    Left = 12,
                    Top = 195,
                    AutoSize = true,
                    Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                    ForeColor = Color.DarkSlateBlue,
                    Text = "Tổng số ngày nghỉ: 1"
                };

                void recalc()
                {
                    if (dtpDen!.Value.Date < dtpTu!.Value.Date)
                        dtpDen.Value = dtpTu.Value.Date;
                    int days = (dtpDen.Value.Date - dtpTu.Value.Date).Days + 1;
                    if (days < 1) days = 1;
                    lblTong!.Text = $"Tổng số ngày nghỉ: {days}";
                }

                dtpTu.ValueChanged += (_, __) => recalc();
                dtpDen.ValueChanged += (_, __) => recalc();
            }

            int buttonTop = hasDateRange ? 250 : 176;
            Button btnOk = new Button { Text = "Gửi", Left = 260, Top = buttonTop, Width = 75, DialogResult = DialogResult.OK };
            Button btnCancel = new Button { Text = "Hủy", Left = 341, Top = buttonTop, Width = 75, DialogResult = DialogResult.Cancel };

            dialog.Controls.Add(lblReason);
            dialog.Controls.Add(txtReason);
            if (hasDateRange)
            {
                dialog.Controls.Add(lblTu!);
                dialog.Controls.Add(dtpTu!);
                dialog.Controls.Add(lblDen!);
                dialog.Controls.Add(dtpDen!);
                dialog.Controls.Add(lblTong!);
            }
            dialog.Controls.Add(btnOk);
            dialog.Controls.Add(btnCancel);
            dialog.AcceptButton = btnOk;
            dialog.CancelButton = btnCancel;

            if (dialog.ShowDialog() != DialogResult.OK)
            {
                return false;
            }

            string reason = txtReason.Text.Trim();
            if (string.IsNullOrWhiteSpace(reason))
            {
                MessageBox.Show("Vui lòng nhập lý do.", "Thiếu dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (hasDateRange)
            {
                DateTime start = dtpTu!.Value.Date;
                DateTime end = dtpDen!.Value.Date;

                if (start < DateTime.Today)
                {
                    MessageBox.Show("Ngày bắt đầu không được nhỏ hơn ngày hiện tại.", "Dữ liệu sai", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if (end < start)
                {
                    MessageBox.Show("Đến ngày phải lớn hơn hoặc bằng Từ ngày.", "Dữ liệu sai", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                tuNgay = start;
                denNgay = end;
                tongNgayNghi = (end - start).Days + 1;
            }

            lyDo = reason;
            return true;
        }

        private void BtnDatLaiMatKhau_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_txtMaNV.Text))
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần đặt lại mật khẩu.", "Thiếu dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Lấy cả pass cũ và pass mới từ Dialog
            if (!TryPromptNewPassword(out string inputOldPassword, out string newPassword))
            {
                return;
            }

            const string sqlSelect = "SELECT MatKhau FROM dbo.NHAN_VIEN WHERE MaNV = @MaNV";
            const string sqlUpdate = "UPDATE dbo.NHAN_VIEN SET MatKhau = @MatKhau WHERE MaNV = @MaNV";

            try
            {
                using SqlConnection conn = DbHelper.GetConnection();
                conn.Open();

                string maNvValue = _selectedMaNvDbValue ?? _txtMaNV.Text.Trim();

                // 1. Kiểm tra mật khẩu cũ
                using SqlCommand checkCmd = new SqlCommand(sqlSelect, conn);
                checkCmd.Parameters.Add("@MaNV", SqlDbType.VarChar, 20).Value = maNvValue;

                string dbPassword = Convert.ToString(checkCmd.ExecuteScalar()) ?? string.Empty;

                if (!string.Equals(dbPassword, inputOldPassword, StringComparison.Ordinal))
                {
                    MessageBox.Show("Mật khẩu cũ không chính xác.", "Lỗi xác thực", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 2. Kiểm tra mật khẩu mới có trùng mật khẩu cũ không (Optional nhưng nên có)
                if (string.Equals(dbPassword, newPassword, StringComparison.Ordinal))
                {
                    MessageBox.Show("Mật khẩu mới không được giống mật khẩu cũ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 3. Thực hiện cập nhật
                using SqlCommand updateCmd = new SqlCommand(sqlUpdate, conn);
                updateCmd.Parameters.Add("@MatKhau", SqlDbType.NVarChar, 100).Value = newPassword;
                updateCmd.Parameters.Add("@MaNV", SqlDbType.VarChar, 20).Value = maNvValue;

                updateCmd.ExecuteNonQuery();
                MessageBox.Show("Đặt lại mật khẩu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi hệ thống: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static bool TryPromptNewPassword(out string oldPassword, out string newPassword)
        {
            oldPassword = string.Empty;
            newPassword = string.Empty;

            using Form dialog = new Form
            {
                Text = "Đặt lại mật khẩu",
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterParent,
                MinimizeBox = false,
                MaximizeBox = false,
                ClientSize = new Size(320, 180) // Tăng chiều cao để chứa thêm field
            };

            // Mật khẩu cũ
            Label lblOld = new Label { Text = "Mật khẩu cũ", Left = 12, Top = 15, AutoSize = true };
            TextBox txtOld = new TextBox { Left = 120, Top = 12, Width = 140, UseSystemPasswordChar = true };

            // Mật khẩu mới
            Label lblNew = new Label { Text = "Mật khẩu mới", Left = 12, Top = 50, AutoSize = true };
            TextBox txtNew = new TextBox { Left = 120, Top = 47, Width = 140, UseSystemPasswordChar = true };

            // Xác nhận
            Label lblConfirm = new Label { Text = "Xác nhận", Left = 12, Top = 85, AutoSize = true };
            TextBox txtConfirm = new TextBox { Left = 120, Top = 82, Width = 140, UseSystemPasswordChar = true };

            Button btnEye = new Button { Text = "👁", Left = 265, Top = 12, Width = 35, Height = 27 };
            Button btnOk = new Button { Text = "OK", Left = 144, Top = 130, Width = 75, DialogResult = DialogResult.OK };
            Button btnCancel = new Button { Text = "Hủy", Left = 225, Top = 130, Width = 75, DialogResult = DialogResult.Cancel };

            btnEye.Click += (_, __) =>
            {
                bool show = txtNew.UseSystemPasswordChar;
                txtOld.UseSystemPasswordChar = !show;
                txtNew.UseSystemPasswordChar = !show;
                txtConfirm.UseSystemPasswordChar = !show;
            };

            dialog.Controls.AddRange(new Control[] { lblOld, txtOld, lblNew, txtNew, lblConfirm, txtConfirm, btnEye, btnOk, btnCancel });
            dialog.AcceptButton = btnOk;
            dialog.CancelButton = btnCancel;

            if (dialog.ShowDialog() != DialogResult.OK) return false;

            if (string.IsNullOrWhiteSpace(txtOld.Text) || string.IsNullOrWhiteSpace(txtNew.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin mật khẩu.", "Thiếu dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (txtNew.Text != txtConfirm.Text)
            {
                MessageBox.Show("Mật khẩu xác nhận không khớp.", "Dữ liệu sai", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            oldPassword = txtOld.Text.Trim();
            newPassword = txtNew.Text.Trim();
            return true;
        }

        private void EnsureProfileExtraControls()
        {
            _txtEmail ??= textBox1;
        }

        private static int? ParseMaNvToInt(string? value)
        {
            if (string.IsNullOrWhiteSpace(value)) return null;
            string s = value.Trim();
            if (s.StartsWith("NV", StringComparison.OrdinalIgnoreCase))
            {
                s = s.Substring(2);
            }
            return int.TryParse(s, out int v) ? v : null;
        }

        private static bool IsValidEmail(string? email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;
            try
            {
                var regex = new System.Text.RegularExpressions.Regex(@"^[^\s@]+@[^\s@]+\.[^\s@]+$", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                return regex.IsMatch(email.Trim());
            }
            catch
            {
                return false;
            }
        }

        private void BtnXemLichTruc_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_txtMaNV.Text))
            {
                MessageBox.Show("Vui lòng chọn nhân viên để xem lịch trực.", "Thiếu dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            const string sql = @"
SELECT pc.MaCa, ct.TenCa, ct.GioBatDau, ct.GioKetThuc, ct.HeSoLuong, pc.NgayLam
FROM dbo.PHAN_CONG_CA pc
INNER JOIN dbo.CA_TRUC ct ON ct.MaCa = pc.MaCa
WHERE pc.MaNV = @MaNV
ORDER BY pc.NgayLam DESC, pc.MaCa";

            const decimal donGiaCaTruc = 176000m;

            try
            {
                using SqlConnection conn = DbHelper.GetConnection();
                using SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add("@MaNV", SqlDbType.VarChar, 20).Value = _selectedMaNvDbValue ?? _txtMaNV.Text.Trim();
                using SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Nhân viên này chưa có lịch trực.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                decimal tongHeSo = dt.AsEnumerable().Sum(r => Convert.ToDecimal(r["HeSoLuong"]));
                int tongSoCa = dt.Rows.Count;
                decimal luongDuKien = tongHeSo * donGiaCaTruc;

                using Form scheduleForm = new Form
                {
                    Text = $"Lịch trực - NV {_txtMaNV.Text}",
                    StartPosition = FormStartPosition.CenterParent,
                    Size = new Size(720, 420)
                };

                DataGridView dgv = new DataGridView
                {
                    Dock = DockStyle.Fill,
                    ReadOnly = true,
                    AllowUserToAddRows = false,
                    AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                    SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                    DataSource = dt
                };
                dgv.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
                dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;

                if (dgv.Columns["NgayLam"] is not null)
                {
                    dgv.Columns["NgayLam"].HeaderText = "Ngày làm";
                    dgv.Columns["NgayLam"].DefaultCellStyle.Format = "dd/MM/yyyy";
                }

                if (dgv.Columns["GioBatDau"] is not null)
                {
                    dgv.Columns["GioBatDau"].HeaderText = "Giờ bắt đầu";
                    dgv.Columns["GioBatDau"].DefaultCellStyle.Format = "HH:mm";
                }

                if (dgv.Columns["GioKetThuc"] is not null)
                {
                    dgv.Columns["GioKetThuc"].HeaderText = "Giờ kết thúc";
                    dgv.Columns["GioKetThuc"].DefaultCellStyle.Format = "HH:mm";
                }

                if (dgv.Columns["HeSoLuong"] is not null)
                {
                    dgv.Columns["HeSoLuong"].HeaderText = "Hệ số lương";
                    dgv.Columns["HeSoLuong"].DefaultCellStyle.Format = "N2";
                }

                if (dgv.Columns["MaCa"] is not null)
                {
                    dgv.Columns["MaCa"].HeaderText = "Mã ca";
                }

                if (dgv.Columns["TenCa"] is not null)
                {
                    dgv.Columns["TenCa"].HeaderText = "Tên ca";
                }

                Panel pnlSummary = new Panel
                {
                    Dock = DockStyle.Bottom,
                    Height = 52,
                    BackColor = Color.FromArgb(248, 242, 235)
                };

                Label lblTongSoCa = new Label
                {
                    AutoSize = true,
                    Location = new Point(12, 17),
                    Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                    Text = $"Tổng số ca làm: {tongSoCa}"
                };

                Label lblTongHeSo = new Label
                {
                    AutoSize = true,
                    Location = new Point(210, 17),
                    Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                    Text = $"Tổng hệ số: {tongHeSo:N2}"
                };

                Label lblLuongDuKien = new Label
                {
                    AutoSize = true,
                    Location = new Point(380, 17),
                    Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                    Text = $"Tiền lương theo ca: {luongDuKien:N0} đ"
                };

                pnlSummary.Controls.Add(lblTongSoCa);
                pnlSummary.Controls.Add(lblTongHeSo);
                pnlSummary.Controls.Add(lblLuongDuKien);

                scheduleForm.Controls.Add(pnlSummary);
                scheduleForm.Controls.Add(dgv);
                scheduleForm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không thể tải lịch trực.\n{ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private decimal GetLuongCoBanNhanVien(string maNv)
        {
            const string sql = @"
SELECT TOP 1 cv.LuongCoBan
FROM dbo.NHAN_VIEN nv
INNER JOIN dbo.CHUC_VU cv ON cv.MaCV = nv.MaCV
WHERE nv.MaNV = @MaNV";

            using SqlConnection conn = DbHelper.GetConnection();
            using SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add("@MaNV", SqlDbType.VarChar, 20).Value = maNv;
            conn.Open();

            object? result = cmd.ExecuteScalar();
            return result is null || result == DBNull.Value ? 0m : Convert.ToDecimal(result);
        }

        private void _txtMatKhau_TextChanged(object sender, EventArgs e)
        {

        }

        private void _dgvNhanVien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {
            ShowThongTinCaNhanPanel();
        }

        private void btn_QLNCC_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btn_QLKH_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
