using PBL3.DataBase;
using PBL3.UI;
using System.Data;
using System.Data.SqlClient;

namespace PBL3
{
    public partial class QuanLiNhanVien : Form
    {
        private DataTable? _nhanVienTable;
        private bool _isEditingExisting;
        private string? _selectedMaNvDbValue;

        public QuanLiNhanVien()
        {
            InitializeComponent();
            _cboTimTheo.SelectionChangeCommitted += SearchControl_Changed;
            _txtMatKhau.UseSystemPasswordChar = true;
            _lblBtnXemLichTruc.Text = "Xem lịch trực";
        }

        private static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;
            // Chỉ chấp nhận email có đuôi @gmail.com
            try
            {
                var regex = new System.Text.RegularExpressions.Regex(@"^[A-Za-z0-9._%+-]+@gmail\.com$", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                return regex.IsMatch(email);
            }
            catch
            {
                return false;
            }
        }

        private static bool IsValidPhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone)) return false;
            phone = phone.Trim();
            // require exactly 10 digits
            var m = System.Text.RegularExpressions.Regex.Match(phone, "^\\d{10}$");
            return m.Success;
        }

        private void QuanLiNhanVien_Load(object? sender, EventArgs e)
        {
            try
            {
                LoadChucVu();
                LoadNhanVien();
                EnsureCreateModeUi();
                UpdateInboxButtonBadge();
                if (_cboTimTheo.Items.Count > 0 && _cboTimTheo.SelectedIndex < 0)
                {
                    _cboTimTheo.SelectedIndex = 0;
                }
                ClearForm();
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
SELECT nv.MaNV, nv.HoTen, nv.NgaySinh, nv.SDT, nv.Email, nv.DiaChi, nv.MatKhau, nv.TrangThai, nv.MaCV, cv.TenCV
FROM dbo.NHAN_VIEN nv
LEFT JOIN dbo.CHUC_VU cv ON cv.MaCV = nv.MaCV
ORDER BY nv.MaNV";

            using SqlConnection conn = DbHelper.GetConnection();
            using SqlCommand cmd = new SqlCommand(sql, conn);
            using SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();
            da.Fill(dt);
            _nhanVienTable = dt;
            _dgvNhanVien.DataSource = _nhanVienTable;
            _dgvNhanVien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            DataGridViewColumn? maCvColumn = _dgvNhanVien.Columns["MaCV"];
            if (maCvColumn is not null)
            {
                maCvColumn.Visible = false;
            }

            DataGridViewColumn? tenCvColumn = _dgvNhanVien.Columns["TenCV"];
            if (tenCvColumn is not null)
            {
                tenCvColumn.HeaderText = "ChứcVụ";
                tenCvColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            }

            SetHeaderText("MaNV", "MãNV");
            SetHeaderText("HoTen", "HọTên");
            SetHeaderText("NgaySinh", "NgàySinh");
            SetHeaderText("SDT", "SĐT");
            SetHeaderText("Email", "Email");
            SetHeaderText("DiaChi", "ĐịaChỉ");
            SetHeaderText("MatKhau", "MậtKhẩu");
            SetHeaderText("TrangThai", "TrạngThái");

            DataGridViewColumn? matKhauColumn = _dgvNhanVien.Columns["MatKhau"];
            if (matKhauColumn is not null)
            {
                matKhauColumn.Visible = false;
            }

            SetColumnWidth("MaNV", 70);
            SetColumnWidth("HoTen", 130);
            SetColumnWidth("NgaySinh", 90);
            SetColumnWidth("SDT", 90);
            SetColumnWidth("Email", 140);
            SetColumnWidth("DiaChi", 140);
            SetColumnWidth("TrangThai", 100);
            SetColumnWidth("TenCV", 110);

            ConfigureGridAppearance();

            ApplySearchFilter();

            if (_dgvNhanVien.Rows.Count > 0)
            {
                _dgvNhanVien.ClearSelection();
                _dgvNhanVien.CurrentCell = null;
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
            LeftColumn("Email");
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
                "Email" => $"Email LIKE '%{keyword}%'",
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
            textBox1.Text = Convert.ToString(row.Cells["Email"].Value) ?? string.Empty;
            _txtDiaChi.Text = Convert.ToString(row.Cells["DiaChi"].Value) ?? string.Empty;
            _txtMatKhau.Clear();
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

            // Phone validation: digits only, exactly 10 digits
            string phone = _txtSdt.Text.Trim();
            if (!IsValidPhone(phone))
            {
                MessageBox.Show("Số điện thoại không hợp lệ. Vui lòng nhập đúng 10 chữ số.", "Dữ liệu sai", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

            // Email format validation (optional field but if provided must be valid)
            string emailValue = textBox1.Text.Trim();
            if (!string.IsNullOrWhiteSpace(emailValue) && !IsValidEmail(emailValue))
            {
                MessageBox.Show("Email phải có dạng ten@gmail.com.", "Dữ liệu sai", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (isInsert && string.IsNullOrWhiteSpace(_txtMatKhau.Text))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu cho nhân viên mới.", "Thiếu dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                // ValidateInput shows messages; ensure we stop the add operation
                return;
            }

            // Double-check and show explicit messages if any format rules still fail
            string phoneToAdd = _txtSdt.Text.Trim();
            if (!IsValidPhone(phoneToAdd))
            {
                MessageBox.Show("Số điện thoại không hợp lệ. Vui lòng nhập đúng 10 chữ số.", "Dữ liệu sai", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _txtSdt.Focus();
                return;
            }
            string emailToAdd = textBox1.Text.Trim();
            if (!string.IsNullOrWhiteSpace(emailToAdd) && !IsValidEmail(emailToAdd))
            {
                MessageBox.Show("Email phải có dạng ten@gmail.com.", "Dữ liệu sai", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox1.Focus();
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
                        using SqlCommand cmd = new SqlCommand("SET IDENTITY_INSERT dbo.NHAN_VIEN ON; INSERT INTO dbo.NHAN_VIEN (MaNV, HoTen, NgaySinh, SDT, Email, DiaChi, MatKhau, TrangThai, MaCV) VALUES (@MaNV, @HoTen, @NgaySinh, @SDT, @Email, @DiaChi, @MatKhau, @TrangThai, @MaCV); SET IDENTITY_INSERT dbo.NHAN_VIEN OFF;", conn, tran);
                        cmd.Parameters.Add("@MaNV", SqlDbType.Int).Value = nextId;
                        cmd.Parameters.Add("@HoTen", SqlDbType.NVarChar, 100).Value = _txtHoTen.Text.Trim();
                        cmd.Parameters.Add("@NgaySinh", SqlDbType.Date).Value = _dtpNgaySinh.Value.Date;
                        cmd.Parameters.Add("@SDT", SqlDbType.VarChar, 20).Value = _txtSdt.Text.Trim();
                        cmd.Parameters.Add("@Email", SqlDbType.VarChar, 100).Value = textBox1.Text.Trim();
                        cmd.Parameters.Add("@DiaChi", SqlDbType.NVarChar, 200).Value = _txtDiaChi.Text.Trim();
                        cmd.Parameters.Add("@MatKhau", SqlDbType.NVarChar, 100).Value = _txtMatKhau.Text.Trim();
                        cmd.Parameters.Add("@TrangThai", SqlDbType.NVarChar, 30).Value = _cboTrangThai.Text.Trim();
                        cmd.Parameters.Add("@MaCV", SqlDbType.VarChar, 20).Value = _cboChucVu.SelectedValue?.ToString() ?? string.Empty;
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        using SqlCommand cmd = new SqlCommand("INSERT INTO dbo.NHAN_VIEN (MaNV, HoTen, NgaySinh, SDT, Email, DiaChi, MatKhau, TrangThai, MaCV) VALUES (@MaNV, @HoTen, @NgaySinh, @SDT, @Email, @DiaChi, @MatKhau, @TrangThai, @MaCV)", conn, tran);
                        cmd.Parameters.Add("@MaNV", SqlDbType.Int).Value = nextId;
                        cmd.Parameters.Add("@HoTen", SqlDbType.NVarChar, 100).Value = _txtHoTen.Text.Trim();
                        cmd.Parameters.Add("@NgaySinh", SqlDbType.Date).Value = _dtpNgaySinh.Value.Date;
                        cmd.Parameters.Add("@SDT", SqlDbType.VarChar, 20).Value = _txtSdt.Text.Trim();
                        cmd.Parameters.Add("@Email", SqlDbType.VarChar, 100).Value = textBox1.Text.Trim();
                        cmd.Parameters.Add("@DiaChi", SqlDbType.NVarChar, 200).Value = _txtDiaChi.Text.Trim();
                        cmd.Parameters.Add("@MatKhau", SqlDbType.NVarChar, 100).Value = _txtMatKhau.Text.Trim();
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
                // ValidateInput shows messages; ensure we stop the edit operation
                return;
            }

            // Additional explicit format checks for edit operation with clear MessageBox feedback
            string phoneToEdit = _txtSdt.Text.Trim();
            if (!IsValidPhone(phoneToEdit))
            {
                MessageBox.Show("Số điện thoại không hợp lệ. Vui lòng nhập đúng 10 chữ số.", "Dữ liệu sai", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _txtSdt.Focus();
                return;
            }
            string emailToEdit = textBox1.Text.Trim();
            if (!string.IsNullOrWhiteSpace(emailToEdit) && !IsValidEmail(emailToEdit))
            {
                MessageBox.Show("Email phải có dạng ten@gmail.com.", "Dữ liệu sai", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox1.Focus();
                return;
            }

            const string sql = @"
UPDATE dbo.NHAN_VIEN
SET HoTen = @HoTen,
    NgaySinh = @NgaySinh,
    SDT = @SDT,
    Email = @Email,
    DiaChi = @DiaChi,
    TrangThai = @TrangThai,
    MaCV = @MaCV,
    MatKhau = CASE WHEN @MatKhau = '' THEN MatKhau ELSE @MatKhau END
WHERE MaNV = @MaNV";

            try
            {
                using SqlConnection conn = DbHelper.GetConnection();
                using SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add("@MaNV", SqlDbType.VarChar, 20).Value = _selectedMaNvDbValue ?? _txtMaNV.Text.Trim();
                cmd.Parameters.Add("@HoTen", SqlDbType.NVarChar, 100).Value = _txtHoTen.Text.Trim();
                cmd.Parameters.Add("@NgaySinh", SqlDbType.Date).Value = _dtpNgaySinh.Value.Date;
                cmd.Parameters.Add("@SDT", SqlDbType.VarChar, 20).Value = _txtSdt.Text.Trim();
                cmd.Parameters.Add("@Email", SqlDbType.VarChar, 100).Value = textBox1.Text.Trim();
                cmd.Parameters.Add("@DiaChi", SqlDbType.NVarChar, 200).Value = _txtDiaChi.Text.Trim();
                cmd.Parameters.Add("@TrangThai", SqlDbType.NVarChar, 30).Value = _cboTrangThai.Text.Trim();
                cmd.Parameters.Add("@MaCV", SqlDbType.VarChar, 20).Value = _cboChucVu.SelectedValue?.ToString() ?? string.Empty;
                cmd.Parameters.Add("@MatKhau", SqlDbType.NVarChar, 100).Value = _txtMatKhau.Text.Trim();

                conn.Open();
                int rows = cmd.ExecuteNonQuery();

                if (rows == 0)
                {
                    MessageBox.Show("Không tìm thấy nhân viên để cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                MessageBox.Show("Cập nhật nhân viên thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadNhanVien();
                ClearForm();
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
        }

        private void ClearForm()
        {
            _selectedMaNvDbValue = null;
            _txtMaNV.Text = GenerateNextMaNhanVien();
            _txtHoTen.Clear();
            _txtSdt.Clear();
            textBox1.Clear();
            _txtDiaChi.Clear();
            _txtMatKhau.Clear();
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
            try
            {
                using var frm = new HopThuYeuCauForm();
                frm.ShowDialog(this);
                UpdateInboxButtonBadge();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không thể mở hộp thư yêu cầu.\n{ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
            AdminNavigationManager.Navigate<QuanLiNhaCungCap>(this);
        }

        private void btn_QLKH_Click(object? sender, EventArgs e)
        {
            AdminNavigationManager.Navigate<QuanLiKhachHang>(this);
        }

        private void btn_QLMA_Click(object? sender, EventArgs e)
        {
            AdminNavigationManager.Navigate<QuanLiMonAn>(this);
        }

        private void btn_ThongKe_Click(object? sender, EventArgs e)
        {
            AdminNavigationManager.Navigate<ThongKe>(this);
        }

        private void btn_QLHDN_Click(object? sender, EventArgs e)
        {
            AdminNavigationManager.Navigate<QuanLiNguyenLieu>(this);
        }

        private void btn_QLHDB_Click(object? sender, EventArgs e)
        {
            AdminNavigationManager.Navigate<LichSuHoaDon>(this);
        }

        private void UpdatePasswordUiState()
        {
            bool isCreateMode = !_isEditingExisting;
            _btnDatLaiMatKhau.Visible = !isCreateMode;
            _btnXemLichTruc.Visible = !isCreateMode;

            _txtMatKhau.ReadOnly = !isCreateMode;
            _txtMatKhau.PlaceholderText = isCreateMode ? string.Empty : "Nhấn 'Đặt lại mật khẩu'";
        }

        private void BtnDatLaiMatKhau_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_txtMaNV.Text))
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần đặt lại mật khẩu.", "Thiếu dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!TryPromptNewPassword(out string newPassword))
            {
                return;
            }

            const string sql = "UPDATE dbo.NHAN_VIEN SET MatKhau = @MatKhau WHERE MaNV = @MaNV";

            try
            {
                using SqlConnection conn = DbHelper.GetConnection();
                using SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add("@MatKhau", SqlDbType.NVarChar, 100).Value = newPassword;
                cmd.Parameters.Add("@MaNV", SqlDbType.VarChar, 20).Value = _selectedMaNvDbValue ?? _txtMaNV.Text.Trim();
                conn.Open();

                int rows = cmd.ExecuteNonQuery();
                if (rows == 0)
                {
                    MessageBox.Show("Không tìm thấy nhân viên để đặt lại mật khẩu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                MessageBox.Show("Đặt lại mật khẩu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đặt lại mật khẩu thất bại.\n{ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static bool TryPromptNewPassword(out string password)
        {
            password = string.Empty;

            using Form dialog = new Form
            {
                Text = "Đặt lại mật khẩu",
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterParent,
                MinimizeBox = false,
                MaximizeBox = false,
                ClientSize = new Size(320, 140)
            };

            Label lblNew = new Label { Text = "Mật khẩu mới", Left = 12, Top = 15, AutoSize = true };
            TextBox txtNew = new TextBox { Left = 120, Top = 12, Width = 180, UseSystemPasswordChar = true };
            Label lblConfirm = new Label { Text = "Xác nhận", Left = 12, Top = 50, AutoSize = true };
            TextBox txtConfirm = new TextBox { Left = 120, Top = 47, Width = 180, UseSystemPasswordChar = true };
            Button btnOk = new Button { Text = "OK", Left = 144, Top = 92, Width = 75, DialogResult = DialogResult.OK };
            Button btnCancel = new Button { Text = "Hủy", Left = 225, Top = 92, Width = 75, DialogResult = DialogResult.Cancel };

            dialog.Controls.Add(lblNew);
            dialog.Controls.Add(txtNew);
            dialog.Controls.Add(lblConfirm);
            dialog.Controls.Add(txtConfirm);
            dialog.Controls.Add(btnOk);
            dialog.Controls.Add(btnCancel);
            dialog.AcceptButton = btnOk;
            dialog.CancelButton = btnCancel;

            if (dialog.ShowDialog() != DialogResult.OK)
            {
                return false;
            }

            string newPassword = txtNew.Text.Trim();
            string confirmPassword = txtConfirm.Text.Trim();

            if (string.IsNullOrWhiteSpace(newPassword))
            {
                MessageBox.Show("Mật khẩu mới không được để trống.", "Thiếu dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (newPassword != confirmPassword)
            {
                MessageBox.Show("Mật khẩu xác nhận không khớp.", "Dữ liệu sai", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            password = newPassword;
            return true;
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

        private void UpdateInboxButtonBadge()
        {
            try
            {
                using SqlConnection conn = DbHelper.GetConnection();
                using SqlCommand cmd = new SqlCommand("SELECT COUNT(1) FROM dbo.YEU_CAU WHERE TrangThai = 0", conn);
                conn.Open();
                int pending = Convert.ToInt32(cmd.ExecuteScalar() ?? 0);
                label2.Text = pending > 0
                    ? $"Hộp thư yêu cầu ({pending})"
                    : "Hộp thư yêu cầu";
            }
            catch
            {
                label2.Text = "Hộp thư yêu cầu";
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

        }
    }
}
