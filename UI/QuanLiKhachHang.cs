using PBL3.DataBase;
using System.Data;
using System.Data.SqlClient;

namespace PBL3
{
    public partial class QuanLiKhachHang : Form
    {
        private DataTable? _khachHangTable;
        private string? _selectedMaKhDbValue;
        private string DiemTichLuyText
        {
            get => _txtDiaChi.Text;
            set => _txtDiaChi.Text = value;
        }

        private static bool IsValidPhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone)) return false;
            phone = phone.Trim();
            var m = System.Text.RegularExpressions.Regex.Match(phone, "^\\d{10}$");
            return m.Success;
        }

        public QuanLiKhachHang()
        {
            InitializeComponent();
            _cboTimTheo.SelectionChangeCommitted += SearchControl_Changed;
        }

        private void QuanLiKhachHang_Load(object? sender, EventArgs e)
        {
            try
            {
                LoadKhachHang();
                if (_cboTimTheo.Items.Count > 0 && _cboTimTheo.SelectedIndex < 0)
                {
                    _cboTimTheo.SelectedIndex = 0;
                }

                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không thể tải dữ liệu khách hàng.\n{ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadKhachHang()
        {
            const string sql = @"
SELECT MaKH, SDT, DiemTichLuy
FROM dbo.KHACH_HANG
ORDER BY MaKH";

            using SqlConnection conn = DbHelper.GetConnection();
            using SqlDataAdapter da = new SqlDataAdapter(sql, conn);

            DataTable dt = new DataTable();
            da.Fill(dt);

            _khachHangTable = dt;
            _dgvNhanVien.DataSource = _khachHangTable;
            _dgvNhanVien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            _dgvNhanVien.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
            _dgvNhanVien.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;

            SetHeaderText("MaKH", "MãKH");
            SetHeaderText("SDT", "SĐT");
            SetHeaderText("DiemTichLuy", "ĐiểmTíchLũy");

            SetColumnWidth("MaKH", 90);
            SetColumnWidth("SDT", 220);
            SetColumnWidth("DiemTichLuy", 130);

            ApplySearchFilter();
        }

        private void SearchControl_Changed(object? sender, EventArgs e)
        {
            ApplySearchFilter();
        }

        private void ApplySearchFilter()
        {
            if (_khachHangTable is null)
            {
                return;
            }

            string keyword = _txtTimKiem.Text.Trim().Replace("'", "''");
            if (string.IsNullOrWhiteSpace(keyword))
            {
                _khachHangTable.DefaultView.RowFilter = string.Empty;
                return;
            }

            string selected = (Convert.ToString(_cboTimTheo.SelectedItem) ?? "MãKH").Trim();
            string filter;

            if (selected == "SĐT" || selected == "SDT")
            {
                filter = $"SDT LIKE '%{keyword}%'";
            }
            else if (selected == "ĐiểmTíchLũy" || selected == "Điểm tích lũy" || selected == "DiemTichLuy")
            {
                filter = $"Convert(DiemTichLuy, 'System.String') LIKE '%{keyword}%'";
            }
            else
            {
                filter = $"Convert(MaKH, 'System.String') LIKE '%{keyword}%'";
            }

            _khachHangTable.DefaultView.RowFilter = filter;
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
            _selectedMaKhDbValue = Convert.ToString(row.Cells["MaKH"].Value) ?? string.Empty;
            _txtMaNV.Text = FormatMaKhForDisplay(_selectedMaKhDbValue);
            _txtSdt.Text = Convert.ToString(row.Cells["SDT"].Value) ?? string.Empty;
            DiemTichLuyText = Convert.ToString(row.Cells["DiemTichLuy"].Value) ?? "0";
        }

        private bool ValidateInput(bool isInsert)
        {
            if (!isInsert && string.IsNullOrWhiteSpace(_txtMaNV.Text))
            {
                MessageBox.Show("Vui lòng chọn khách hàng.", "Thiếu dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Phone validation: digits only, exactly 10 digits
            string phone = _txtSdt.Text.Trim();
            if (!IsValidPhone(phone))
            {
                MessageBox.Show("Số điện thoại không hợp lệ. Vui lòng nhập đúng 10 chữ số.", "Dữ liệu sai", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _txtSdt.Focus();
                return false;
            }

            if (IsPhoneExists(isInsert, phone))
            {
                MessageBox.Show("Số điện thoại khách hàng đã tồn tại.", "Dữ liệu trùng lặp", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!int.TryParse(DiemTichLuyText.Trim(), out int diem) || diem < 0)
            {
                MessageBox.Show("Điểm tích lũy không hợp lệ.", "Dữ liệu sai", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private bool IsPhoneExists(bool isInsert, string phone)
        {
            string query = isInsert ? "SELECT COUNT(1) FROM dbo.KHACH_HANG WHERE SDT = @SDT"
                                    : "SELECT COUNT(1) FROM dbo.KHACH_HANG WHERE SDT = @SDT AND MaKH != @ID";
            try
            {
                using SqlConnection conn = DbHelper.GetConnection();
                using SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.Add("@SDT", SqlDbType.VarChar).Value = phone;
                if (!isInsert)
                {
                    cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = _selectedMaKhDbValue ?? _txtMaNV.Text.Trim();
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
                using (SqlCommand cmdGet = new SqlCommand("SELECT MaKH FROM dbo.KHACH_HANG ORDER BY TRY_CAST(CASE WHEN CONVERT(VARCHAR(20), MaKH) LIKE 'KH%' THEN SUBSTRING(CONVERT(VARCHAR(20), MaKH), 3, LEN(CONVERT(VARCHAR(20), MaKH)) - 2) ELSE CONVERT(VARCHAR(20), MaKH) END AS INT)", conn))
                using (SqlDataReader reader = cmdGet.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader.IsDBNull(0))
                            continue;

                        string raw = Convert.ToString(reader.GetValue(0)) ?? string.Empty;
                        string numeric = raw.StartsWith("KH", StringComparison.OrdinalIgnoreCase) ? raw.Substring(2) : raw;
                        if (!int.TryParse(numeric, out int v))
                            continue;

                        if (v == nextId)
                            nextId++;
                        else if (v > nextId)
                            break;
                    }
                }

                bool hasIdentity = false;
                using (SqlCommand cmdCheck = new SqlCommand("SELECT CASE WHEN COLUMNPROPERTY(OBJECT_ID('dbo.KHACH_HANG'),'MaKH','IsIdentity') = 1 THEN 1 ELSE 0 END", conn))
                {
                    hasIdentity = Convert.ToInt32(cmdCheck.ExecuteScalar() ?? 0) == 1;
                }

                using SqlTransaction tran = conn.BeginTransaction();
                try
                {
                    if (hasIdentity)
                    {
                        using SqlCommand cmd = new SqlCommand("SET IDENTITY_INSERT dbo.KHACH_HANG ON; INSERT INTO dbo.KHACH_HANG (MaKH, TenKH, SDT, DiemTichLuy) VALUES (@MaKH, @TenKH, @SDT, @DiemTichLuy); SET IDENTITY_INSERT dbo.KHACH_HANG OFF;", conn, tran);
                        cmd.Parameters.Add("@MaKH", SqlDbType.Int).Value = nextId;
                        cmd.Parameters.Add("@TenKH", SqlDbType.NVarChar, 100).Value = _txtSdt.Text.Trim();
                        cmd.Parameters.Add("@SDT", SqlDbType.VarChar, 20).Value = _txtSdt.Text.Trim();
                        cmd.Parameters.Add("@DiemTichLuy", SqlDbType.Int).Value = int.Parse(DiemTichLuyText.Trim());
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        using SqlCommand cmd = new SqlCommand("INSERT INTO dbo.KHACH_HANG (MaKH, TenKH, SDT, DiemTichLuy) VALUES (@MaKH, @TenKH, @SDT, @DiemTichLuy)", conn, tran);
                        cmd.Parameters.Add("@MaKH", SqlDbType.Int).Value = nextId;
                        cmd.Parameters.Add("@TenKH", SqlDbType.NVarChar, 100).Value = _txtSdt.Text.Trim();
                        cmd.Parameters.Add("@SDT", SqlDbType.VarChar, 20).Value = _txtSdt.Text.Trim();
                        cmd.Parameters.Add("@DiemTichLuy", SqlDbType.Int).Value = int.Parse(DiemTichLuyText.Trim());
                        cmd.ExecuteNonQuery();
                    }

                    tran.Commit();
                }
                catch
                {
                    tran.Rollback();
                    throw;
                }

                MessageBox.Show("Thêm khách hàng thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadKhachHang();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Thêm khách hàng thất bại.\n{ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSua_Click(object? sender, EventArgs e)
        {
            if (!ValidateInput(false))
            {
                return;
            }

            const string sql = @"
UPDATE dbo.KHACH_HANG
SET TenKH = @TenKH,
    SDT = @SDT,
    DiemTichLuy = @DiemTichLuy
WHERE MaKH = @MaKH";

            try
            {
                using SqlConnection conn = DbHelper.GetConnection();
                using SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add("@MaKH", SqlDbType.VarChar, 20).Value = _selectedMaKhDbValue ?? _txtMaNV.Text.Trim();
                cmd.Parameters.Add("@TenKH", SqlDbType.NVarChar, 100).Value = _txtSdt.Text.Trim();
                cmd.Parameters.Add("@SDT", SqlDbType.VarChar, 20).Value = _txtSdt.Text.Trim();
                cmd.Parameters.Add("@DiemTichLuy", SqlDbType.Int).Value = int.Parse(DiemTichLuyText.Trim());

                conn.Open();
                int rows = cmd.ExecuteNonQuery();

                if (rows == 0)
                {
                    MessageBox.Show("Không tìm thấy khách hàng để cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                MessageBox.Show("Cập nhật khách hàng thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadKhachHang();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Cập nhật khách hàng thất bại.\n{ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnXoa_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_txtMaNV.Text))
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần xóa.", "Thiếu dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
                "Bạn có chắc chắn muốn xóa khách hàng này?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
            {
                return;
            }

            const string sql = "DELETE FROM dbo.KHACH_HANG WHERE MaKH = @MaKH";

            try
            {
                using SqlConnection conn = DbHelper.GetConnection();
                using SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add("@MaKH", SqlDbType.VarChar, 20).Value = _selectedMaKhDbValue ?? _txtMaNV.Text.Trim();

                conn.Open();
                int rows = cmd.ExecuteNonQuery();

                if (rows == 0)
                {
                    MessageBox.Show("Không tìm thấy khách hàng để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                MessageBox.Show("Xóa khách hàng thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadKhachHang();
                ClearForm();
            }
            catch (SqlException ex) when (ex.Number == 547)
            {
                MessageBox.Show("Không thể xóa khách hàng vì đang được sử dụng ở dữ liệu liên quan.", "Không thể xóa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Xóa khách hàng thất bại.\n{ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnLamMoi_Click(object? sender, EventArgs e)
        {
            ClearForm();
            LoadKhachHang();
        }

        private void ClearForm()
        {
            _selectedMaKhDbValue = null;
            _txtMaNV.Text = GenerateNextMaKH();
            _txtSdt.Clear();
            DiemTichLuyText = "0";
            _txtSdt.Focus();
        }

        private string GenerateNextMaKH()
        {
            const string sql = @"SELECT MaKH FROM dbo.KHACH_HANG
ORDER BY TRY_CAST(CASE WHEN CONVERT(VARCHAR(20), MaKH) LIKE 'KH%' THEN SUBSTRING(CONVERT(VARCHAR(20), MaKH), 3, LEN(CONVERT(VARCHAR(20), MaKH)) - 2) ELSE CONVERT(VARCHAR(20), MaKH) END AS INT)";

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
                    string numeric = raw.StartsWith("KH", StringComparison.OrdinalIgnoreCase) ? raw.Substring(2) : raw;
                    if (!int.TryParse(numeric, out int v))
                        continue;

                    if (v == nextId)
                        nextId++;
                    else if (v > nextId)
                        break;
                }

                return $"KH{nextId}";
            }
            catch
            {
                return "KH1";
            }
        }

        private static string FormatMaKhForDisplay(string? maKhValue)
        {
            if (string.IsNullOrWhiteSpace(maKhValue))
            {
                return string.Empty;
            }

            string value = maKhValue.Trim();
            return value.StartsWith("KH", StringComparison.OrdinalIgnoreCase) ? value.ToUpperInvariant() : $"KH{value}";
        }

        private void btn_QLKH_Click(object? sender, EventArgs e)
        {
        }

        private void btn_QLNCC_Click(object? sender, EventArgs e)
        {
            AdminNavigationManager.Navigate<QuanLiNhaCungCap>(this);
        }

        private void btn_QLNV_Click(object? sender, EventArgs e)
        {
            AdminNavigationManager.Navigate<QuanLiNhanVien>(this);
        }

        private void btn_QLMA_Click(object? sender, EventArgs e)
        {
            AdminNavigationManager.Navigate<QuanLiMonAn>(this);
        }

        private void btn_QLHDN_Click(object? sender, EventArgs e)
        {
            AdminNavigationManager.Navigate<QuanLiNguyenLieu>(this);
        }

        private void btn_QLHDB_Click(object? sender, EventArgs e)
        {
            AdminNavigationManager.Navigate<LichSuHoaDon>(this);
        }

        private void btn_ThongKe_Click(object? sender, EventArgs e)
        {
            AdminNavigationManager.Navigate<ThongKe>(this);
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

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void roundedPanel4_Paint(object sender, PaintEventArgs e)
        {
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }

        private void pnlFormNhanVien_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
