using PBL3.DataBase;
using PBL3.UI;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;

namespace PBL3
{
    public partial class BanHang : Form
    {
        private const int DiemMoiMocGiam = 10;
        private const int TienGiamMoiMoc = 10000;
        private const int NguongCongDiem = 100000;
        private const int DiemCongMoiNguong = 10;

        private DataTable? _nhanVienTable;
        private bool _isEditingExisting;
        private string? _selectedMaNvDbValue;
        private Button? _btnYeuCauNghiPhep;
        private Button? _btnYeuCauNghiHan;
        private Panel? _banHangPanel;

        private readonly DataTable _hoaDonTable = new DataTable();
        private DataTable? _menuTable;
        private DataTable? _khachHangTable;
        private readonly string _monAnImageFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MonAnImages");

        private DataGridView? _bhDgvMenu;
        private DataGridView? _bhDgvHoaDon;
        private ComboBox? _bhCboLoaiMon;
        private ComboBox? _bhCboSize;
        private ComboBox? _bhCboKhachHang;
        private NumericUpDown? _bhNudSoLuong;
        private Button? _bhBtnThem;
        private Button? _bhBtnXoa;
        private Button? _bhBtnThanhToan;
        private CheckBox? _bhChkKhongLienKetKhach;
        private Label? _bhLblTongTien;
        private PictureBox? _bhPicMon;
        private MuaHang? _muaHangEmbedded;
        private KhachHang? _khachHangEmbedded;
        private string _startModule = "BanHang";
        private string _printContent = string.Empty;
        private Image? _printQrImage;

        private string _loggedInMaNV;
        private bool _isNavigating;

        public BanHang()
        {
            _loggedInMaNV = "1";
            InitializeComponent();
            _cboTimTheo.SelectionChangeCommitted += SearchControl_Changed;
            _txtMatKhau.UseSystemPasswordChar = true;
            if (IsInDesignerHost())
            {
                ApplyDesignerPreviewState();
            }
        }

        private bool IsInDesignerHost()
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
            {
                return true;
            }

            if (Site?.DesignMode == true)
            {
                return true;
            }

            return string.Equals(Process.GetCurrentProcess().ProcessName, "devenv", StringComparison.OrdinalIgnoreCase);
        }

        private void ApplyDesignerPreviewState()
        {
            InitializeBanHangDesignerSurface();
        }

        private void InitializeBanHangDesignerSurface()
        {
            if (!IsInDesignerHost())
            {
                return;
            }

            lb_QLNhanVienTitle.Text = "Bán hàng";
            btn_QLNV.BackColor = Color.Bisque;
            label4.ForeColor = SystemColors.ControlText;
            btn_QLKH.BackColor = Color.Salmon;
            label6.ForeColor = Color.White;

            EnsureBanHangPanel();
            if (_banHangPanel is not null)
            {
                _banHangPanel.BringToFront();
                _banHangPanel.Visible = true;
            }
        }

        public BanHang(string maNV, string startModule = "BanHang") : this()
        {
            _loggedInMaNV = maNV;
            _startModule = startModule;
        }

        private void TrangNhanVien1_Load(object? sender, EventArgs e)
        {
            try
            {
                pnlDanhSachNhanVien.Visible = false;
                pnlTimKiem.Visible = false;
                _btnThem.Visible = false;
                _btnXoa.Visible = false;
                _btnLamMoi.Visible = false;
                lblTrangThai.Visible = false;
                _cboTrangThai.Visible = false;

                _cboChucVu.Enabled = false;
                btn_QLNV.BackColor = Color.Salmon;
                label4.ForeColor = Color.White;

                LoadChucVu();
                LoadNhanVien();
                _isEditingExisting = true;
                UpdatePasswordUiState();
                ConfigureProfileLayout();
                EnsureLeaveRequestButtons();
                if (string.Equals(_startModule, "MuaHang", StringComparison.OrdinalIgnoreCase))
                {
                    ShowMuaHangInRightPanel();
                }
                else if (string.Equals(_startModule, "KhachHang", StringComparison.OrdinalIgnoreCase))
                {
                    ShowKhachHangInRightPanel();
                }
                else if (string.Equals(_startModule, "ThongTinCaNhan", StringComparison.OrdinalIgnoreCase))
                {
                    OpenAndClose(new TrangNhanVien1(_selectedMaNvDbValue ?? _loggedInMaNV));
                    return;
                }
                else
                {
                    ShowBanHangInRightPanel();
                }
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
SELECT nv.MaNV, nv.HoTen, nv.NgaySinh, nv.SDT, nv.DiaChi, nv.MatKhau, nv.TrangThai, nv.MaCV
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
                _txtDiaChi.Text = Convert.ToString(reader["DiaChi"]);
                _txtMatKhau.Clear();

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

            if (!long.TryParse(_txtSdt.Text.Trim(), out _))
            {
                MessageBox.Show("Số điện thoại không hợp lệ.", "Dữ liệu sai", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                        cmd.Parameters.Add("@MatKhau", SqlDbType.NVarChar, 100).Value = _txtMatKhau.Text.Trim();
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
                return;
            }

            const string sql = @"
UPDATE dbo.NHAN_VIEN
SET HoTen = @HoTen,
    NgaySinh = @NgaySinh,
    SDT = @SDT,
    DiaChi = @DiaChi,
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
                cmd.Parameters.Add("@DiaChi", SqlDbType.NVarChar, 200).Value = _txtDiaChi.Text.Trim();
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
            ShowBanHangInRightPanel();
        }

        private void btn_QLMA_Click(object? sender, EventArgs e)
        {
            OpenAndClose(new MuaHang(_selectedMaNvDbValue ?? _loggedInMaNV));
        }

        private void btn_QLHDN_Click(object? sender, EventArgs e)
        {
            OpenAndClose(new KhachHang(_selectedMaNvDbValue ?? _loggedInMaNV));
        }

        private void EnsureBanHangPanel()
        {
            if (_banHangPanel is null && _designerBanHangPreview is not null)
            {
                _banHangPanel = _designerBanHangPreview;
            }

            if (_banHangPanel is not null)
            {
                if (_banHangPanel.Parent != hcnt_Khung)
                {
                    hcnt_Khung.Controls.Add(_banHangPanel);
                }

                InitHoaDonGrid();

                if (IsInDesignerHost())
                {
                    InitializeBanHangDesignerData();
                }
                else
                {
                    ConfigureBanHangGridSelectionMode();
                    if (_bhDgvMenu is not null && _bhDgvMenu.DataSource is null)
                    {
                        _bhDgvMenu.Columns.Clear();
                    }

                    if (_bhDgvHoaDon is not null && _bhDgvHoaDon.DataSource is null)
                    {
                        _bhDgvHoaDon.Columns.Clear();
                    }

                    if (_menuTable is null)
                    {
                        LoadDanhMucMonAnData();
                    }

                    if (_khachHangTable is null)
                    {
                        LoadKhachHangOptionsData();
                    }
                }

                if (_bhChkKhongLienKetKhach is not null)
                {
                    _bhChkKhongLienKetKhach.Checked = true;
                }

                EnsureDiemKhachLabel();

                UpdateKhachHangUiStateData();
                return;
            }

            if (_banHangPanel is not null)
            {
                return;
            }

            _banHangPanel = new Panel
            {
                BackColor = Color.FromArgb(248, 242, 235),
                Location = new Point(18, 52),
                Size = new Size(834, 648),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom
            };

            _bhDgvMenu = new DataGridView
            {
                Location = new Point(18, 52),
                Size = new Size(560, 250),
                ReadOnly = true,
                MultiSelect = false,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
            _bhDgvMenu.EnableHeadersVisualStyles = false;
            _bhDgvMenu.DefaultCellStyle.BackColor = Color.White;
            _bhDgvMenu.DefaultCellStyle.ForeColor = Color.Black;
            _bhDgvMenu.RowsDefaultCellStyle.BackColor = Color.White;
            _bhDgvMenu.AlternatingRowsDefaultCellStyle.BackColor = Color.White;
            _bhDgvMenu.SelectionChanged += BhDgvMenu_SelectionChanged;

            _bhDgvHoaDon = new DataGridView
            {
                Location = new Point(18, 360),
                Size = new Size(798, 220),
                ReadOnly = true,
                MultiSelect = false,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom
            };
            _bhDgvHoaDon.EnableHeadersVisualStyles = false;
            _bhDgvHoaDon.DefaultCellStyle.BackColor = Color.White;
            _bhDgvHoaDon.DefaultCellStyle.ForeColor = Color.Black;
            _bhDgvHoaDon.RowsDefaultCellStyle.BackColor = Color.White;
            _bhDgvHoaDon.AlternatingRowsDefaultCellStyle.BackColor = Color.White;

            _bhCboLoaiMon = new ComboBox { Location = new Point(465, 18), Size = new Size(113, 28), DropDownStyle = ComboBoxStyle.DropDownList, Anchor = AnchorStyles.Top | AnchorStyles.Right };
            _bhCboLoaiMon.SelectedIndexChanged += BhCboLoaiMon_SelectedIndexChanged;
            _bhCboKhachHang = new ComboBox { Location = new Point(600, 118), Size = new Size(216, 28), DropDownStyle = ComboBoxStyle.DropDownList, Anchor = AnchorStyles.Top | AnchorStyles.Right };
            _bhCboKhachHang.SelectedIndexChanged += BhCboKhachHang_SelectedIndexChanged;
            _bhCboSize = new ComboBox { Location = new Point(600, 186), Size = new Size(216, 28), DropDownStyle = ComboBoxStyle.DropDownList, Anchor = AnchorStyles.Top | AnchorStyles.Right };

            _bhNudSoLuong = new NumericUpDown { Location = new Point(600, 252), Size = new Size(120, 27), Minimum = 1, Value = 1, Anchor = AnchorStyles.Top | AnchorStyles.Right };
            _bhChkKhongLienKetKhach = new CheckBox { Location = new Point(600, 148), Size = new Size(216, 24), Text = "Không liên kết khách hàng", Anchor = AnchorStyles.Top | AnchorStyles.Right };
            _bhChkKhongLienKetKhach.CheckedChanged += BhChkKhongLienKetKhach_CheckedChanged;

            _bhBtnThem = new Button { Location = new Point(600, 298), Size = new Size(216, 38), Text = "Thêm vào hóa đơn", BackColor = Color.SandyBrown, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Anchor = AnchorStyles.Top | AnchorStyles.Right };
            _bhBtnThem.Click += BhBtnThem_Click;
            _bhBtnXoa = new Button { Location = new Point(675, 595), Size = new Size(140, 38), Text = "Xóa dòng", BackColor = Color.IndianRed, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Anchor = AnchorStyles.Bottom | AnchorStyles.Right };
            _bhBtnXoa.Click += BhBtnXoa_Click;
            _bhBtnThanhToan = new Button { Location = new Point(520, 595), Size = new Size(145, 38), Text = "Thanh toán", BackColor = Color.Peru, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Anchor = AnchorStyles.Bottom | AnchorStyles.Right };
            _bhBtnThanhToan.Click += BhBtnThanhToan_Click;

            _bhLblTongTien = new Label { Location = new Point(18, 600), AutoSize = true, Font = new Font("Segoe UI", 12F, FontStyle.Bold), ForeColor = Color.Firebrick, Text = "Tổng tiền: 0 đ", Anchor = AnchorStyles.Bottom | AnchorStyles.Left };
            _bhLblDiemKhach = new Label { Location = new Point(600, 96), AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold), ForeColor = Color.SaddleBrown, Text = "Điểm tích lũy: 0", Anchor = AnchorStyles.Top | AnchorStyles.Right };
            _bhPicMon = new PictureBox { Location = new Point(600, 18), Size = new Size(120, 70), BackColor = Color.Gainsboro, SizeMode = PictureBoxSizeMode.Zoom, Anchor = AnchorStyles.Top | AnchorStyles.Right };

            _banHangPanel.Controls.Add(new Label { Text = "Danh mục món ăn (Tên/Ảnh/Giá)", Font = new Font("Segoe UI", 12F, FontStyle.Bold), ForeColor = Color.SaddleBrown, AutoSize = true, Location = new Point(18, 18) });
            _banHangPanel.Controls.Add(new Label { Text = "Loại món", Font = new Font("Segoe UI", 9F, FontStyle.Bold), AutoSize = true, Location = new Point(394, 22), Anchor = AnchorStyles.Top | AnchorStyles.Right });
            _banHangPanel.Controls.Add(new Label { Text = "Khách hàng", Font = new Font("Segoe UI", 9F, FontStyle.Bold), AutoSize = true, Location = new Point(600, 98), Anchor = AnchorStyles.Top | AnchorStyles.Right });
            _banHangPanel.Controls.Add(new Label { Text = "Size", Font = new Font("Segoe UI", 9F, FontStyle.Bold), AutoSize = true, Location = new Point(600, 168), Anchor = AnchorStyles.Top | AnchorStyles.Right });
            _banHangPanel.Controls.Add(new Label { Text = "Số lượng", Font = new Font("Segoe UI", 9F, FontStyle.Bold), AutoSize = true, Location = new Point(600, 232), Anchor = AnchorStyles.Top | AnchorStyles.Right });
            _banHangPanel.Controls.Add(new Label { Text = "Hóa đơn", Font = new Font("Segoe UI", 12F, FontStyle.Bold), ForeColor = Color.SaddleBrown, AutoSize = true, Location = new Point(18, 328) });

            _banHangPanel.Controls.Add(_bhDgvMenu);
            _banHangPanel.Controls.Add(_bhDgvHoaDon);
            _banHangPanel.Controls.Add(_bhCboLoaiMon);
            _banHangPanel.Controls.Add(_bhCboKhachHang);
            _banHangPanel.Controls.Add(_bhCboSize);
            _banHangPanel.Controls.Add(_bhNudSoLuong);
            _banHangPanel.Controls.Add(_bhChkKhongLienKetKhach);
            _banHangPanel.Controls.Add(_bhBtnThem);
            _banHangPanel.Controls.Add(_bhBtnXoa);
            _banHangPanel.Controls.Add(_bhBtnThanhToan);
            _banHangPanel.Controls.Add(_bhLblTongTien);
            _banHangPanel.Controls.Add(_bhLblDiemKhach);
            _banHangPanel.Controls.Add(_bhPicMon);

            hcnt_Khung.Controls.Add(_banHangPanel);

            InitHoaDonGrid();
            if (IsInDesignerHost())
            {
                InitializeBanHangDesignerData();
            }
            else
            {
                LoadDanhMucMonAnData();
                LoadKhachHangOptionsData();
            }

            _bhChkKhongLienKetKhach.Checked = true;
            UpdateKhachHangUiStateData();
        }

        private void InitializeBanHangDesignerData()
        {
            if (_bhDgvMenu is null || _bhCboLoaiMon is null || _bhCboSize is null || _bhCboKhachHang is null)
            {
                return;
            }

            DataTable dtMenu = new DataTable();
            dtMenu.Columns.Add("MaMon", typeof(string));
            dtMenu.Columns.Add("TenMon", typeof(string));
            dtMenu.Columns.Add("MaLoai", typeof(string));
            dtMenu.Columns.Add("TenLoai", typeof(string));
            dtMenu.Columns.Add("MaDVT", typeof(string));
            dtMenu.Columns.Add("TenDVT", typeof(string));
            dtMenu.Columns.Add("DonGia", typeof(decimal));
            dtMenu.Rows.Add("MA1", "", "", "", "", "", 0m);

            _menuTable = dtMenu;
            _bhDgvMenu.DataSource = _menuTable;

            if (!_bhDgvMenu.Columns.Contains("AnhMon"))
            {
                DataGridViewImageColumn imageColumn = new DataGridViewImageColumn
                {
                    Name = "AnhMon",
                    HeaderText = "Ảnh",
                    ImageLayout = DataGridViewImageCellLayout.Zoom
                };
                _bhDgvMenu.Columns.Insert(0, imageColumn);
            }

            if (_bhDgvMenu.Rows.Count > 0)
            {
                _bhDgvMenu.Rows[0].Cells["AnhMon"].Value = new Bitmap(1, 1);
            }

            _bhDgvMenu.Columns["MaLoai"].Visible = false;
            _bhDgvMenu.Columns["MaDVT"].Visible = false;

            DataTable dtLoai = new DataTable();
            dtLoai.Columns.Add("Value", typeof(string));
            dtLoai.Columns.Add("Text", typeof(string));
            dtLoai.Rows.Add(string.Empty, "Tất cả loại");
            _bhCboLoaiMon.DataSource = dtLoai;
            _bhCboLoaiMon.DisplayMember = "Text";
            _bhCboLoaiMon.ValueMember = "Value";

            DataTable dtSize = new DataTable();
            dtSize.Columns.Add("MaDVPV", typeof(string));
            dtSize.Columns.Add("HienThi", typeof(string));
            dtSize.Rows.Add("0", "Đĩa - 0 đ");
            _bhCboSize.DataSource = dtSize;
            _bhCboSize.DisplayMember = "HienThi";
            _bhCboSize.ValueMember = "MaDVPV";

            DataTable dtKh = new DataTable();
            dtKh.Columns.Add("MaKH", typeof(int));
            dtKh.Columns.Add("TenKH", typeof(string));
            dtKh.Columns.Add("SDT", typeof(string));
            dtKh.Columns.Add("DiemTichLuy", typeof(int));
            dtKh.Columns.Add("HienThi", typeof(string));
            dtKh.Rows.Add(0, "Khách lẻ", string.Empty, 0, "Khách lẻ");
            _khachHangTable = dtKh;
            _bhCboKhachHang.DataSource = _khachHangTable;
            _bhCboKhachHang.DisplayMember = "HienThi";
            _bhCboKhachHang.ValueMember = "MaKH";
        }

        private void InitHoaDonGrid()
        {
            if (_bhDgvHoaDon is null)
            {
                return;
            }

            if (_hoaDonTable.Columns.Count == 0)
            {
                _hoaDonTable.Columns.Add("MaMon", typeof(string));
                _hoaDonTable.Columns.Add("TenMon", typeof(string));
                _hoaDonTable.Columns.Add("MaDVPV", typeof(string));
                _hoaDonTable.Columns.Add("Size", typeof(string));
                _hoaDonTable.Columns.Add("SoLuong", typeof(int));
                _hoaDonTable.Columns.Add("DonGia", typeof(decimal));
                _hoaDonTable.Columns.Add("ThanhTien", typeof(decimal), "SoLuong * DonGia");
            }

            _bhDgvHoaDon.AutoGenerateColumns = _bhDgvHoaDon.Columns.Count == 0;
            _bhDgvHoaDon.DataSource = _hoaDonTable;
            foreach (DataGridViewColumn col in _bhDgvHoaDon.Columns)
            {
                string key = string.IsNullOrWhiteSpace(col.DataPropertyName) ? col.Name : col.DataPropertyName;

                switch (key)
                {
                    case "MaMon":
                    case "MaDVPV":
                        col.Visible = false;
                        break;
                    case "TenMon":
                        col.HeaderText = "Tên món";
                        break;
                    case "Size":
                        col.HeaderText = "Tên ĐVT";
                        break;
                    case "SoLuong":
                        col.HeaderText = "SL";
                        col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        break;
                    case "DonGia":
                        col.HeaderText = "Đơn giá";
                        col.DefaultCellStyle.Format = "N0";
                        col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        break;
                    case "ThanhTien":
                        col.HeaderText = "T.Tiền";
                        col.DefaultCellStyle.Format = "N0";
                        col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        break;
                }
            }
        }

        private void ConfigureBanHangGridSelectionMode()
        {
            if (_bhDgvMenu is not null)
            {
                _bhDgvMenu.MultiSelect = false;
                _bhDgvMenu.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                _bhDgvMenu.ReadOnly = true;
            }

            if (_bhDgvHoaDon is not null)
            {
                _bhDgvHoaDon.MultiSelect = false;
                _bhDgvHoaDon.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                _bhDgvHoaDon.ReadOnly = true;
            }
        }

        private void EnsureDiemKhachLabel()
        {
            if (_banHangPanel is null || _bhLblDiemKhach is not null)
            {
                return;
            }

            _bhLblDiemKhach = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.SaddleBrown,
                Location = new Point(525, 20),
                Text = "Điểm tích lũy: 0"
            };

            _banHangPanel.Controls.Add(_bhLblDiemKhach);
            _bhLblDiemKhach.BringToFront();
        }

        private void LoadDanhMucMonAnData()
        {
            if (_bhDgvMenu is null)
            {
                return;
            }
            const string sql = @"
SELECT mb.MaMon, mb.TenMon, mb.MaLoai, lm.TenLoai, mb.MaDVT, dvt.TenDVT
FROM dbo.MON_BAN mb
LEFT JOIN dbo.LOAI_MON lm ON lm.MaLoai = mb.MaLoai
LEFT JOIN dbo.DON_VI_TINH dvt ON dvt.MaDVT = mb.MaDVT
WHERE ISNULL(mb.TrangThai, N'Đang bán') <> N'Ngừng bán'
ORDER BY mb.MaMon";

            using SqlConnection conn = DbHelper.GetConnection();
            conn.Open();
            using SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (!dt.Columns.Contains("DonGia"))
            {
                dt.Columns.Add("DonGia", typeof(decimal));
            }

            Dictionary<string, decimal> giaMap = LoadGiaMapByMon(conn);
            foreach (DataRow row in dt.Rows)
            {
                string key = NormalizeMonKey(Convert.ToString(row["MaMon"]));
                row["DonGia"] = giaMap.TryGetValue(key, out decimal gia) ? gia : 0m;
            }

            _menuTable = dt;
            _bhDgvMenu.AutoGenerateColumns = _bhDgvMenu.Columns.Count == 0;
            _bhDgvMenu.DataSource = _menuTable;

            if (!_bhDgvMenu.Columns.Contains("AnhMon"))
            {
                DataGridViewImageColumn imageColumn = new DataGridViewImageColumn { Name = "AnhMon", HeaderText = string.Empty, ImageLayout = DataGridViewImageCellLayout.Zoom };
                _bhDgvMenu.Columns.Insert(0, imageColumn);
            }

            if (_bhDgvMenu.Columns["AnhMon"] is DataGridViewImageColumn anhCol)
            {
                anhCol.HeaderText = "Ảnh";
                anhCol.Width = 70;
            }

            if (_bhDgvMenu.Columns.Contains("MaMon")) _bhDgvMenu.Columns["MaMon"].Visible = false;
            if (_bhDgvMenu.Columns.Contains("MaLoai")) _bhDgvMenu.Columns["MaLoai"].Visible = false;
            if (_bhDgvMenu.Columns.Contains("MaDVT")) _bhDgvMenu.Columns["MaDVT"].Visible = false;
            if (_bhDgvMenu.Columns.Contains("TenMon")) _bhDgvMenu.Columns["TenMon"].HeaderText = "Tên món";
            if (_bhDgvMenu.Columns.Contains("TenLoai")) _bhDgvMenu.Columns["TenLoai"].HeaderText = "Tên loại";
            if (_bhDgvMenu.Columns.Contains("TenDVT")) _bhDgvMenu.Columns["TenDVT"].Visible = false;
            if (_bhDgvMenu.Columns.Contains("DonGia"))
            {
                _bhDgvMenu.Columns["DonGia"].HeaderText = "Đơn giá";
                _bhDgvMenu.Columns["DonGia"].DefaultCellStyle.Format = "N0";
                _bhDgvMenu.Columns["DonGia"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            FillThumbnailValuesData();
            LoadLoaiMonOptionsData();

            if (_bhDgvMenu.Rows.Count > 0)
            {
                _bhDgvMenu.ClearSelection();
                _bhDgvMenu.Rows[0].Selected = true;
                _bhDgvMenu.CurrentCell = _bhDgvMenu.Rows[0].Cells["TenMon"];
                LoadSizeForSelectedMonData();
            }
        }

        private void LoadLoaiMonOptionsData()
        {
            if (_menuTable is null || _bhCboLoaiMon is null)
            {
                return;
            }

            DataTable dtLoai = new DataTable();
            dtLoai.Columns.Add("Value", typeof(string));
            dtLoai.Columns.Add("Text", typeof(string));
            dtLoai.Rows.Add(string.Empty, "Tất cả loại");
            foreach (DataRow row in _menuTable.DefaultView.ToTable(true, "MaLoai", "TenLoai").Rows)
            {
                dtLoai.Rows.Add(Convert.ToString(row["MaLoai"]) ?? string.Empty, Convert.ToString(row["TenLoai"]) ?? string.Empty);
            }

            _bhCboLoaiMon.DataSource = dtLoai;
            _bhCboLoaiMon.DisplayMember = "Text";
            _bhCboLoaiMon.ValueMember = "Value";
        }

        private void LoadKhachHangOptionsData()
        {
            if (_bhCboKhachHang is null)
            {
                return;
            }

            using SqlConnection conn = DbHelper.GetConnection();
            using SqlDataAdapter da = new SqlDataAdapter("SELECT MaKH, TenKH, SDT, ISNULL(DiemTichLuy,0) AS DiemTichLuy FROM dbo.KHACH_HANG ORDER BY MaKH", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (!dt.Columns.Contains("HienThi"))
            {
                dt.Columns.Add("HienThi", typeof(string));
            }

            foreach (DataRow r in dt.Rows)
            {
                string ten = Convert.ToString(r["TenKH"]) ?? string.Empty;
                string sdt = Convert.ToString(r["SDT"]) ?? string.Empty;
                r["HienThi"] = string.IsNullOrWhiteSpace(sdt) ? ten : $"{ten} - {sdt}";
            }

            _khachHangTable = dt;
            _bhCboKhachHang.DataSource = _khachHangTable;
            _bhCboKhachHang.DisplayMember = "HienThi";
            _bhCboKhachHang.ValueMember = "MaKH";
        }

        private void UpdateKhachHangUiStateData()
        {
            if (_bhChkKhongLienKetKhach is null || _bhCboKhachHang is null || _bhLblDiemKhach is null)
            {
                return;
            }

            bool linked = !_bhChkKhongLienKetKhach.Checked;
            _bhCboKhachHang.Enabled = linked;
            if (_bhTxtSdtKhachHang is not null)
            {
                _bhTxtSdtKhachHang.Enabled = linked;
                _bhTxtSdtKhachHang.BackColor = linked ? Color.Bisque : Color.Gainsboro;
                if (!linked)
                {
                    _bhTxtSdtKhachHang.Text = string.Empty;
                }
            }

            if (_bhBtnTraCuuKhachHang is not null)
            {
                _bhBtnTraCuuKhachHang.Enabled = linked;
            }

            _bhLblDiemKhach.Text = linked ? $"Điểm tích lũy: {GetSelectedKhachHangPointsData()}" : "Điểm tích lũy: 0";
        }

        private void FillThumbnailValuesData()
        {
            if (_bhDgvMenu is null)
            {
                return;
            }

            foreach (DataGridViewRow row in _bhDgvMenu.Rows)
            {
                if (row.IsNewRow)
                {
                    continue;
                }

                string maMon = row.DataBoundItem is DataRowView rv
                    ? (Convert.ToString(rv["MaMon"]) ?? string.Empty)
                    : (_bhDgvMenu.Columns.Contains("MaMon") ? Convert.ToString(row.Cells["MaMon"].Value) ?? string.Empty : string.Empty);
                row.Cells["AnhMon"].Value = GetMonThumbnailData(maMon);
            }
        }

        private Image GetMonThumbnailData(string maMon)
        {
            string[] exts = [".jpg", ".jpeg", ".png", ".bmp", ".gif"];
            string? file = exts.Select(ext => Path.Combine(_monAnImageFolder, $"{maMon}{ext}")).FirstOrDefault(File.Exists);
            if (file is null)
            {
                return new Bitmap(1, 1);
            }

            using FileStream s = new FileStream(file, FileMode.Open, FileAccess.Read);
            using Image img = Image.FromStream(s);
            return new Bitmap(img);
        }

        private void LoadSizeForSelectedMonData()
        {
            if (_bhDgvMenu is null || _bhCboSize is null || _bhBtnThem is null || _bhDgvMenu.SelectedRows.Count == 0)
            {
                return;
            }

            DataRowView? selectedView = _bhDgvMenu.SelectedRows[0].DataBoundItem as DataRowView;
            string maMon = selectedView is not null
                ? (Convert.ToString(selectedView["MaMon"]) ?? string.Empty)
                : (Convert.ToString(_bhDgvMenu.SelectedRows[0].Cells["MaMon"].Value) ?? string.Empty);
            string maMonRaw = maMon.Trim();
            string maMonNumeric = NormalizeMonKey(maMonRaw);
            using SqlConnection conn = DbHelper.GetConnection();
            conn.Open();

            string giaColumn = ResolveMonDvpvGiaColumn(conn);
            string sql = $@"SELECT mdv.MaDVPV, dv.TenDVPV, ISNULL(mdv.{giaColumn},0) AS DonGia,
CAST(dv.TenDVPV + N' - ' + FORMAT(ISNULL(mdv.{giaColumn},0), 'N0') + N' đ' AS NVARCHAR(120)) AS HienThi
FROM dbo.MON_DON_VI_PHUC_VU mdv
INNER JOIN dbo.DON_VI_PHUC_VU dv ON dv.MaDVPV = mdv.MaDVPV
WHERE CONVERT(VARCHAR(20), mdv.MaMon) = @MaMonRaw
   OR (CASE
           WHEN CONVERT(VARCHAR(20), mdv.MaMon) LIKE 'MA%'
               THEN SUBSTRING(CONVERT(VARCHAR(20), mdv.MaMon), 3, LEN(CONVERT(VARCHAR(20), mdv.MaMon)) - 2)
           ELSE CONVERT(VARCHAR(20), mdv.MaMon)
       END) = @MaMonNumeric";

            using SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add("@MaMonRaw", SqlDbType.VarChar, 20).Value = maMonRaw;
            cmd.Parameters.Add("@MaMonNumeric", SqlDbType.VarChar, 20).Value = maMonNumeric;
            using SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count == 0)
            {
                string giaFallbackExpression = ResolveMonBanGiaExpression(conn, "mb");
                string sqlFallback = $@"SELECT ISNULL(mb.MaDVT, 0) AS MaDVPV,
ISNULL(dvt.TenDVT, N'Mặc định') AS TenDVPV,
{giaFallbackExpression} AS DonGia,
CAST(ISNULL(dvt.TenDVT, N'Mặc định') + N' - ' + FORMAT({giaFallbackExpression}, 'N0') + N' đ' AS NVARCHAR(120)) AS HienThi
FROM dbo.MON_BAN mb
LEFT JOIN dbo.DON_VI_TINH dvt ON dvt.MaDVT = mb.MaDVT
WHERE CONVERT(VARCHAR(20), mb.MaMon) = @MaMonRaw
   OR (CASE
           WHEN CONVERT(VARCHAR(20), mb.MaMon) LIKE 'MA%'
               THEN SUBSTRING(CONVERT(VARCHAR(20), mb.MaMon), 3, LEN(CONVERT(VARCHAR(20), mb.MaMon)) - 2)
           ELSE CONVERT(VARCHAR(20), mb.MaMon)
       END) = @MaMonNumeric";

                using SqlCommand fallbackCmd = new SqlCommand(sqlFallback, conn);
                fallbackCmd.Parameters.Add("@MaMonRaw", SqlDbType.VarChar, 20).Value = maMonRaw;
                fallbackCmd.Parameters.Add("@MaMonNumeric", SqlDbType.VarChar, 20).Value = maMonNumeric;
                using SqlDataAdapter fallbackDa = new SqlDataAdapter(fallbackCmd);
                fallbackDa.Fill(dt);

                if (dt.Rows.Count == 0)
                {
                    object? maDvtValue = selectedView?["MaDVT"];
                    string tenDvt = Convert.ToString(selectedView?["TenDVT"]) ?? "Mặc định";
                    decimal giaMon = ToDecimalValue(selectedView?["DonGia"]);
                    int maDvpv = 0;
                    if (maDvtValue is not null && maDvtValue != DBNull.Value)
                    {
                        int.TryParse(Convert.ToString(maDvtValue), out maDvpv);
                    }

                    DataRow r = dt.NewRow();
                    r["MaDVPV"] = maDvpv;
                    r["TenDVPV"] = tenDvt;
                    r["DonGia"] = giaMon;
                    r["HienThi"] = $"{tenDvt} - {giaMon:N0} đ";
                    dt.Rows.Add(r);
                }
            }

            _bhCboSize.DataSource = dt;
            _bhCboSize.DisplayMember = "HienThi";
            _bhCboSize.ValueMember = "MaDVPV";
            _bhBtnThem.Enabled = dt.Rows.Count > 0;
            if (_bhPicMon is not null)
            {
                _bhPicMon.Image = GetMonThumbnailData(maMon);
            }
        }

        private static Dictionary<string, decimal> LoadGiaMapByMon(SqlConnection conn)
        {
            Dictionary<string, decimal> map = new Dictionary<string, decimal>(StringComparer.OrdinalIgnoreCase);
            if (!TableExists(conn, "MON_DON_VI_PHUC_VU"))
            {
                return map;
            }

            string giaCol = ResolveMonDvpvGiaColumn(conn);
            bool hasTrangThai = TableColumnExists(conn, "MON_DON_VI_PHUC_VU", "TrangThai");
            string trangThaiFilter = hasTrangThai ? "WHERE ISNULL(TrangThai, N'Đang bán') <> N'Ngừng bán'" : string.Empty;

            string sql = $@"SELECT MaMon, MIN(ISNULL({giaCol}, 0)) AS DonGia
FROM dbo.MON_DON_VI_PHUC_VU
{trangThaiFilter}
GROUP BY MaMon";

            using SqlCommand cmd = new SqlCommand(sql, conn);
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string key = NormalizeMonKey(Convert.ToString(reader["MaMon"]));
                decimal gia = ToDecimalValue(reader["DonGia"]);
                if (!string.IsNullOrWhiteSpace(key) && !map.ContainsKey(key))
                {
                    map[key] = gia;
                }
            }

            return map;
        }

        private static bool TableColumnExists(SqlConnection conn, string tableName, string columnName)
        {
            using SqlCommand cmd = new SqlCommand("SELECT CASE WHEN EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='dbo' AND TABLE_NAME=@TableName AND COLUMN_NAME=@ColumnName) THEN 1 ELSE 0 END", conn);
            cmd.Parameters.Add("@TableName", SqlDbType.VarChar, 128).Value = tableName;
            cmd.Parameters.Add("@ColumnName", SqlDbType.VarChar, 128).Value = columnName;
            return Convert.ToInt32(cmd.ExecuteScalar()) == 1;
        }

        private static string ResolveMonDvpvGiaColumn(SqlConnection conn)
        {
            const string sql = @"SELECT TOP 1 COLUMN_NAME
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_SCHEMA = 'dbo'
  AND TABLE_NAME = 'MON_DON_VI_PHUC_VU'
  AND COLUMN_NAME IN ('DonGia', 'GiaBan', 'Gia')
ORDER BY CASE COLUMN_NAME WHEN 'DonGia' THEN 1 WHEN 'GiaBan' THEN 2 WHEN 'Gia' THEN 3 ELSE 99 END";

            using SqlCommand cmd = new SqlCommand(sql, conn);
            string? columnName = Convert.ToString(cmd.ExecuteScalar());
            return string.IsNullOrWhiteSpace(columnName) ? "DonGia" : columnName;
        }

        private static string ResolveMonBanGiaExpression(SqlConnection conn, string tableAlias)
        {
            const string sql = @"SELECT TOP 1 COLUMN_NAME
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_SCHEMA = 'dbo'
  AND TABLE_NAME = 'MON_BAN'
  AND COLUMN_NAME IN ('DonGia', 'GiaBan', 'Gia')
ORDER BY CASE COLUMN_NAME WHEN 'DonGia' THEN 1 WHEN 'GiaBan' THEN 2 WHEN 'Gia' THEN 3 ELSE 99 END";

            using SqlCommand cmd = new SqlCommand(sql, conn);
            string? columnName = Convert.ToString(cmd.ExecuteScalar());
            return string.IsNullOrWhiteSpace(columnName)
                ? "CAST(0 AS DECIMAL(18,2))"
                : $"ISNULL({tableAlias}.{columnName}, 0)";
        }

        private static bool TableExists(SqlConnection conn, string tableName)
        {
            using SqlCommand cmd = new SqlCommand("SELECT CASE WHEN EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA='dbo' AND TABLE_NAME=@TableName) THEN 1 ELSE 0 END", conn);
            cmd.Parameters.Add("@TableName", SqlDbType.VarChar, 128).Value = tableName;
            return Convert.ToInt32(cmd.ExecuteScalar()) == 1;
        }

        private int GetSelectedKhachHangPointsData()
        {
            if (_bhChkKhongLienKetKhach?.Checked != false || _bhCboKhachHang?.SelectedItem is not DataRowView rv)
            {
                return 0;
            }

            return Convert.ToInt32(rv["DiemTichLuy"] ?? 0);
        }

        private int? GetSelectedKhachHangIdData()
        {
            if (_bhChkKhongLienKetKhach?.Checked != false || _bhCboKhachHang?.SelectedValue is null)
            {
                return null;
            }

            return int.TryParse(Convert.ToString(_bhCboKhachHang.SelectedValue), out int id) ? id : null;
        }

        private int CreateCustomerByPhone(string sdt)
        {
            using SqlConnection conn = DbHelper.GetConnection();
            conn.Open();

            // If phone already exists (race), return existing id
            using (SqlCommand chk = new SqlCommand("SELECT MaKH FROM dbo.KHACH_HANG WHERE SDT = @SDT", conn))
            {
                chk.Parameters.Add("@SDT", SqlDbType.VarChar, 20).Value = sdt;
                object? exists = chk.ExecuteScalar();
                if (exists is not null && exists != DBNull.Value)
                {
                    return Convert.ToInt32(exists);
                }
            }

            // Create new customer with default name
            bool hasIdentity;
            using (SqlCommand cmdCheck = new SqlCommand("SELECT CASE WHEN COLUMNPROPERTY(OBJECT_ID('dbo.KHACH_HANG'),'MaKH','IsIdentity') = 1 THEN 1 ELSE 0 END", conn))
            {
                hasIdentity = Convert.ToInt32(cmdCheck.ExecuteScalar() ?? 0) == 1;
            }

            if (hasIdentity)
            {
                using SqlCommand cmd = new SqlCommand("INSERT INTO dbo.KHACH_HANG (TenKH, SDT, DiemTichLuy) OUTPUT INSERTED.MaKH VALUES (@TenKH, @SDT, 0)", conn);
                cmd.Parameters.Add("@TenKH", SqlDbType.NVarChar, 100).Value = $"Khách hàng";
                cmd.Parameters.Add("@SDT", SqlDbType.VarChar, 20).Value = sdt;
                object? id = cmd.ExecuteScalar();
                return Convert.ToInt32(id);
            }

            // compute next MaKH similar to KhachHang.GetNextMaKh
            int next;
            using (SqlCommand cmdNext = new SqlCommand(@"SELECT ISNULL(MAX(TRY_CAST(CASE WHEN CONVERT(VARCHAR(20), MaKH) LIKE 'KH%' THEN SUBSTRING(CONVERT(VARCHAR(20), MaKH), 3, LEN(CONVERT(VARCHAR(20), MaKH)) - 2) ELSE CONVERT(VARCHAR(20), MaKH) END AS INT)), 0) + 1 FROM dbo.KHACH_HANG", conn))
            {
                next = Convert.ToInt32(cmdNext.ExecuteScalar());
            }

            using SqlCommand cmd2 = new SqlCommand("INSERT INTO dbo.KHACH_HANG (MaKH, TenKH, SDT, DiemTichLuy) OUTPUT INSERTED.MaKH VALUES (@MaKH, @TenKH, @SDT, 0)", conn);
            cmd2.Parameters.Add("@MaKH", SqlDbType.Int).Value = next;
            cmd2.Parameters.Add("@TenKH", SqlDbType.NVarChar, 100).Value = $"Khách hàng";
            cmd2.Parameters.Add("@SDT", SqlDbType.VarChar, 20).Value = sdt;
            object? id2 = cmd2.ExecuteScalar();
            return Convert.ToInt32(id2);
        }

        private void UpdateTongTienData()
        {
            if (_bhLblTongTien is null)
            {
                return;
            }

            decimal total = _hoaDonTable.AsEnumerable().Sum(r => r.Field<decimal>("ThanhTien"));
            _bhLblTongTien.Text = $"Tổng tiền: {total:N0} đ";
        }

        private void BhDgvMenu_SelectionChanged(object? sender, EventArgs e) => LoadSizeForSelectedMonData();

        private void BhCboLoaiMon_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (_menuTable is null || _bhCboLoaiMon is null)
            {
                return;
            }

            string maLoai = Convert.ToString(_bhCboLoaiMon.SelectedValue) ?? string.Empty;
            _menuTable.DefaultView.RowFilter = string.IsNullOrWhiteSpace(maLoai) ? string.Empty : $"Convert(MaLoai, 'System.String') = '{maLoai.Replace("'", "''")}'";
            FillThumbnailValuesData();
        }

        private void BhCboKhachHang_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (_bhCboKhachHang?.SelectedItem is DataRowView rv && _bhTxtSdtKhachHang is not null)
            {
                _bhTxtSdtKhachHang.Text = Convert.ToString(rv["SDT"]) ?? string.Empty;
            }

            UpdateKhachHangUiStateData();
        }

        private void BhChkKhongLienKetKhach_CheckedChanged(object? sender, EventArgs e) => UpdateKhachHangUiStateData();

        private void BhBtnTraCuuKhachHang_Click(object? sender, EventArgs e)
        {
            if (_bhChkKhongLienKetKhach?.Checked != false || _bhTxtSdtKhachHang is null || _khachHangTable is null)
            {
                return;
            }

            string sdt = _bhTxtSdtKhachHang.Text.Trim();
            if (string.IsNullOrWhiteSpace(sdt) || _bhCboKhachHang is null)
            {
                _bhLblDiemKhach!.Text = "Điểm tích lũy: 0";
                return;
            }

            DataRow? matchedRow = _khachHangTable.AsEnumerable()
                .FirstOrDefault(r => string.Equals((Convert.ToString(r["SDT"]) ?? string.Empty).Trim(), sdt, StringComparison.OrdinalIgnoreCase));

            if (matchedRow is null)
            {
                // No existing customer found. Ask user to create a new customer with this phone.
                DialogResult dr = MessageBox.Show($"Số điện thoại {sdt} chưa có trong hệ thống. Tạo khách hàng mới?", "Tạo khách hàng", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        int newId = CreateCustomerByPhone(sdt);
                        // reload options and select new customer
                        LoadKhachHangOptionsData();
                        if (_bhCboKhachHang is not null)
                        {
                            _bhCboKhachHang.SelectedValue = newId;
                        }
                        _bhLblDiemKhach!.Text = "Điểm tích lũy: 0";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Không thể tạo khách hàng mới.\n{ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    // Clear selection so previous customer's points are not reused
                    try
                    {
                        if (_bhCboKhachHang is not null)
                        {
                            _bhCboKhachHang.SelectedIndex = -1;
                        }
                    }
                    catch
                    {
                        // ignore
                    }

                    _bhLblDiemKhach!.Text = "Điểm tích lũy: 0";
                }

                return;
            }

            _bhCboKhachHang.SelectedValue = matchedRow["MaKH"];
            _bhLblDiemKhach!.Text = $"Điểm tích lũy: {Convert.ToInt32(matchedRow["DiemTichLuy"] ?? 0)}";
        }

        private void BhBtnThem_Click(object? sender, EventArgs e)
        {
            if (_bhDgvMenu is null || _bhCboSize is null || _bhNudSoLuong is null || _bhDgvMenu.SelectedRows.Count == 0 || _bhCboSize.SelectedItem is not DataRowView size)
            {
                return;
            }

            DataRowView? selectedView = _bhDgvMenu.SelectedRows[0].DataBoundItem as DataRowView;
            string maMon = selectedView is not null
                ? (Convert.ToString(selectedView["MaMon"]) ?? string.Empty)
                : (Convert.ToString(_bhDgvMenu.SelectedRows[0].Cells["MaMon"].Value) ?? string.Empty);
            string tenMon = selectedView is not null
                ? (Convert.ToString(selectedView["TenMon"]) ?? string.Empty)
                : (Convert.ToString(_bhDgvMenu.SelectedRows[0].Cells["TenMon"].Value) ?? string.Empty);
            string maDvpv = Convert.ToString(_bhCboSize.SelectedValue) ?? string.Empty;
            string sizeText = Convert.ToString(size["TenDVPV"]) ?? string.Empty;
            decimal donGia = ToDecimalValue(size["DonGia"]);
            int soLuong = (int)_bhNudSoLuong.Value;

            DataRow? existed = _hoaDonTable.AsEnumerable().FirstOrDefault(r => string.Equals(Convert.ToString(r["MaMon"]), maMon, StringComparison.OrdinalIgnoreCase) && string.Equals(Convert.ToString(r["MaDVPV"]), maDvpv, StringComparison.OrdinalIgnoreCase));
            if (existed is null)
            {
                _hoaDonTable.Rows.Add(maMon, tenMon, maDvpv, sizeText, soLuong, donGia);
            }
            else
            {
                existed["SoLuong"] = Convert.ToInt32(existed["SoLuong"]) + soLuong;
            }

            UpdateTongTienData();
            _bhDgvHoaDon?.Refresh();
        }

        private static string NormalizeMonKey(string? maMon)
        {
            if (string.IsNullOrWhiteSpace(maMon))
            {
                return string.Empty;
            }

            string value = maMon.Trim();
            if (value.StartsWith("MA", StringComparison.OrdinalIgnoreCase))
            {
                return value.Length > 2 ? value[2..] : string.Empty;
            }

            return value;
        }

        private static decimal ToDecimalValue(object? value)
        {
            if (value is null || value == DBNull.Value)
            {
                return 0m;
            }

            if (value is decimal d)
            {
                return d;
            }

            if (decimal.TryParse(Convert.ToString(value), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal parsedInvariant))
            {
                return parsedInvariant;
            }

            return decimal.TryParse(Convert.ToString(value), NumberStyles.Any, CultureInfo.CurrentCulture, out decimal parsedCurrent)
                ? parsedCurrent
                : 0m;
        }

        private void BhBtnXoa_Click(object? sender, EventArgs e)
        {
            if (_bhDgvHoaDon is null)
            {
                return;
            }

            foreach (DataGridViewRow row in _bhDgvHoaDon.SelectedRows)
            {
                if (!row.IsNewRow)
                {
                    _bhDgvHoaDon.Rows.Remove(row);
                }
            }

            UpdateTongTienData();
        }

        private void BhBtnThanhToan_Click(object? sender, EventArgs e)
        {
            if (_hoaDonTable.Rows.Count == 0)
            {
                MessageBox.Show("Hóa đơn đang trống.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            decimal tongTien = _hoaDonTable.AsEnumerable().Sum(r => r.Field<decimal>("ThanhTien"));
            int? maKh = GetSelectedKhachHangIdData();
            int diemHienTai = GetSelectedKhachHangPointsData();
            decimal giamGia = 0m;
            int diemDung = 0;

            if (maKh.HasValue && diemHienTai >= DiemMoiMocGiam)
            {
                int soMocTheoDiem = diemHienTai / DiemMoiMocGiam;
                int soMocTheoTien = (int)(tongTien / TienGiamMoiMoc);
                int soMocApDung = Math.Min(soMocTheoDiem, soMocTheoTien);
                decimal giamToiDa = soMocApDung * TienGiamMoiMoc;

                if (giamToiDa > 0 && MessageBox.Show($"Khách có {diemHienTai} điểm.\nQuy đổi: {DiemMoiMocGiam} điểm giảm {TienGiamMoiMoc:N0} đ.\nCó thể giảm tối đa {giamToiDa:N0} đ.\nDùng điểm giảm ngay không?", "Giảm giá", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    giamGia = giamToiDa;
                    diemDung = soMocApDung * DiemMoiMocGiam;
                }
            }

            decimal tongSauGiam = tongTien - giamGia;
            int diemCong = maKh.HasValue ? (int)(tongSauGiam / NguongCongDiem) * DiemCongMoiNguong : 0;

            using SqlConnection conn = DbHelper.GetConnection();
            conn.Open();
            using SqlTransaction tran = conn.BeginTransaction();
            try
            {
                int maHdb = InsertHoaDonBanData(conn, tran, tongSauGiam, maKh);
                DataTable printTable = _hoaDonTable.Copy();

                foreach (DataRow row in _hoaDonTable.Rows)
                {
                    string maMon = Convert.ToString(row["MaMon"]) ?? string.Empty;
                    string maDvpv = Convert.ToString(row["MaDVPV"]) ?? string.Empty;
                    int soLuong = Convert.ToInt32(row["SoLuong"]);
                    if (!int.TryParse(NormalizeMonKey(maMon), out int maMonInt))
                    {
                        throw new InvalidOperationException($"Mã món không hợp lệ: {maMon}");
                    }

                    if (!int.TryParse(maDvpv, out int maDvpvInt))
                    {
                        throw new InvalidOperationException($"Mã đơn vị phục vụ không hợp lệ: {maDvpv}");
                    }

                    InsertChiTietHoaDonData(conn, tran, maHdb, maMonInt, maDvpvInt, soLuong);
                    TruKhoNguyenLieuData(conn, tran, maMonInt, maDvpvInt, soLuong);
                }

                if (maKh.HasValue)
                {
                    using SqlCommand cmd = new SqlCommand("UPDATE dbo.KHACH_HANG SET DiemTichLuy = ISNULL(DiemTichLuy,0) + @Cong - @Tru WHERE MaKH=@MaKH", conn, tran);
                    cmd.Parameters.Add("@Cong", SqlDbType.Int).Value = diemCong;
                    cmd.Parameters.Add("@Tru", SqlDbType.Int).Value = diemDung;
                    cmd.Parameters.Add("@MaKH", SqlDbType.Int).Value = maKh.Value;
                    cmd.ExecuteNonQuery();
                }

                tran.Commit();

                ShowPaymentPreview(maHdb, printTable, tongSauGiam);

                _hoaDonTable.Clear();
                UpdateTongTienData();
                LoadKhachHangOptionsData();
                MessageBox.Show($"Thanh toán thành công. Tổng thanh toán: {tongSauGiam:N0} đ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                tran.Rollback();
                MessageBox.Show($"Thanh toán thất bại.\n{ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowPaymentPreview(int maHdb, DataTable detailTable, decimal tongTien)
        {
            DateTime thoiGian = DateTime.Now;
            string vnPayCode = BuildVnPayCode(maHdb.ToString(CultureInfo.InvariantCulture), tongTien, thoiGian);
            string qrPayload = BuildVnPayQrPayload(vnPayCode, tongTien, thoiGian);

            _printQrImage?.Dispose();
            _printQrImage = TryCreateQrImage(qrPayload, 170);

            _printContent = string.Empty;

            string khachHang = "Khách lẻ";
            if (_bhChkKhongLienKetKhach?.Checked == false && _bhCboKhachHang?.Text is not null)
            {
                khachHang = _bhCboKhachHang.Text.Trim();
            }

            PrintDocument doc = new PrintDocument();
            doc.DefaultPageSettings.PaperSize = new PaperSize("Receipt", 315, 1200);
            doc.DefaultPageSettings.Margins = new Margins(10, 10, 10, 10);
            doc.PrintPage += (s, e) =>
            {
                using Font brandFont = new Font("Segoe UI", 11, FontStyle.Bold);
                using Font titleFont = new Font("Segoe UI", 10, FontStyle.Bold);
                using Font normalFont = new Font("Segoe UI", 8.5f);
                using Font monoFont = new Font("Consolas", 8.5f);
                using Font totalFont = new Font("Segoe UI", 9.5f, FontStyle.Bold);

                float left = e.MarginBounds.Left;
                float right = e.MarginBounds.Right;
                float width = e.MarginBounds.Width;
                float y = e.MarginBounds.Top;

                void DrawCentered(string text, Font font)
                {
                    SizeF s2 = e.Graphics.MeasureString(text, font);
                    float x = left + (width - s2.Width) / 2f;
                    e.Graphics.DrawString(text, font, Brushes.Black, x, y);
                    y += s2.Height + 1f;
                }

                DrawCentered("TỨ ĐẠI THIÊN LONG", brandFont);
                DrawCentered("169 Nguyễn Lương Bằng", normalFont);
                DrawCentered("SĐT: 0374895922", normalFont);

                y += 2f;
                e.Graphics.DrawLine(Pens.Gray, left, y, right, y);
                y += 4f;

                DrawCentered("HÓA ĐƠN BÁN HÀNG", titleFont);
                e.Graphics.DrawString($"Mã hóa đơn: {maHdb}", normalFont, Brushes.Black, left, y); y += normalFont.GetHeight(e.Graphics) + 1f;
                e.Graphics.DrawString($"Ngày: {thoiGian:dd/MM/yyyy HH:mm}", normalFont, Brushes.Black, left, y); y += normalFont.GetHeight(e.Graphics) + 1f;
                e.Graphics.DrawString($"Khách hàng: {khachHang}", normalFont, Brushes.Black, left, y); y += normalFont.GetHeight(e.Graphics) + 1f;
                e.Graphics.DrawString($"Nhân viên: {_txtHoTen.Text.Trim()}", normalFont, Brushes.Black, left, y); y += normalFont.GetHeight(e.Graphics) + 3f;

                e.Graphics.DrawLine(Pens.Gray, left, y, right, y);
                y += 4f;

                float qtyW = 24f;
                float priceW = 52f;
                float totalW = 62f;
                const float colGap = 4f;
                float nameW = width - qtyW - priceW - totalW - (colGap * 2f);

                RectangleF nameRect = new RectangleF(left, y, nameW, 16f);
                RectangleF qtyRect = new RectangleF(nameRect.Right + colGap, y, qtyW, 16f);
                RectangleF priceRect = new RectangleF(qtyRect.Right + colGap, y, priceW, 16f);
                RectangleF totalRect = new RectangleF(priceRect.Right, y, totalW, 16f);
                using StringFormat rightFormat = new StringFormat { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Near };

                e.Graphics.DrawString("Tên món", monoFont, Brushes.Black, nameRect);
                e.Graphics.DrawString("SL", monoFont, Brushes.Black, qtyRect, rightFormat);
                e.Graphics.DrawString("Đơn giá", monoFont, Brushes.Black, priceRect, rightFormat);
                e.Graphics.DrawString("T.Tiền", monoFont, Brushes.Black, totalRect, rightFormat);
                y += 15f;

                e.Graphics.DrawLine(Pens.LightGray, left, y, right, y);
                y += 3f;

                foreach (DataRow row in detailTable.Rows)
                {
                    string tenMon = Convert.ToString(row["TenMon"]) ?? string.Empty;
                    string sl = Convert.ToString(row["SoLuong"]) ?? "0";
                    decimal dg = ToDecimalValue(row["DonGia"]);
                    decimal tt = ToDecimalValue(row["ThanhTien"]);

                    nameRect = new RectangleF(left, y, nameW, 14f);
                    qtyRect = new RectangleF(nameRect.Right + colGap, y, qtyW, 14f);
                    priceRect = new RectangleF(qtyRect.Right + colGap, y, priceW, 14f);
                    totalRect = new RectangleF(priceRect.Right, y, totalW, 14f);

                    e.Graphics.DrawString(tenMon, monoFont, Brushes.Black, nameRect);
                    e.Graphics.DrawString(sl, monoFont, Brushes.Black, qtyRect, rightFormat);
                    e.Graphics.DrawString($"{dg:N0}", monoFont, Brushes.Black, priceRect, rightFormat);
                    e.Graphics.DrawString($"{tt:N0}", monoFont, Brushes.Black, totalRect, rightFormat);
                    y += 14f;
                }

                y += 2f;
                e.Graphics.DrawLine(Pens.Gray, left, y, right, y);
                y += 4f;

                e.Graphics.DrawString($"TỔNG CỘNG: {tongTien:N0} đ", totalFont, Brushes.Black, left, y);
                y += totalFont.GetHeight(e.Graphics) + 4f;

                if (_printQrImage is not null)
                {
                    DrawCentered("Quét mã để thanh toán", normalFont);
                    const float qrSize = 74f;
                    float qrX = left + (width - qrSize) / 2f;
                    e.Graphics.DrawImage(_printQrImage, qrX, y, qrSize, qrSize);
                    y += qrSize + 6f;
                }

                DrawCentered("Cảm ơn Quý khách. Hẹn gặp lại!", normalFont);
            };

            using PrintPreviewDialog preview = new PrintPreviewDialog
            {
                Document = doc,
                Width = 900,
                Height = 700
            };

            preview.ShowDialog(this);
        }

        private static string BuildVnPayCode(string? maHd, decimal tongTien, DateTime thoiGian)
        {
            string id = string.IsNullOrWhiteSpace(maHd) ? "UNK" : maHd.Trim();
            long amount = decimal.ToInt64(decimal.Round(tongTien, 0, MidpointRounding.AwayFromZero));
            string seed = $"{id}|{amount}|{thoiGian:yyyyMMddHHmmss}";

            using SHA1 sha1 = SHA1.Create();
            byte[] hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(seed));
            string token = BitConverter.ToString(hash, 0, 3).Replace("-", string.Empty);
            return $"VNP-{amount}-{token}";
        }

        private static string BuildVnPayQrPayload(string vnPayCode, decimal tongTien, DateTime thoiGian)
        {
            long amount = decimal.ToInt64(decimal.Round(tongTien, 0, MidpointRounding.AwayFromZero));
            return $"VNPAY|CODE={vnPayCode}|AMOUNT={amount}|TIME={thoiGian:yyyyMMddHHmmss}";
        }

        private static Image? TryCreateQrImage(string payload, int size)
        {
            try
            {
                string url = $"https://api.qrserver.com/v1/create-qr-code/?size={size}x{size}&data={Uri.EscapeDataString(payload)}";
                using HttpClient client = new HttpClient();
                byte[] bytes = client.GetByteArrayAsync(url).GetAwaiter().GetResult();
                using MemoryStream ms = new MemoryStream(bytes);
                using Image img = Image.FromStream(ms);
                return new Bitmap(img);
            }
            catch
            {
                return null;
            }
        }

        private static int GenerateNextMaHoaDonDataInt(SqlConnection conn, SqlTransaction tran)
        {
            using SqlCommand cmd = new SqlCommand("SELECT ISNULL(MAX(TRY_CAST(MaHDB AS INT)), 0) + 1 FROM dbo.HOA_DON_BAN", conn, tran);
            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        private int InsertHoaDonBanData(SqlConnection conn, SqlTransaction tran, decimal tongTien, int? maKh)
        {
            bool hasIdentity;
            using (SqlCommand cmdIdentity = new SqlCommand("SELECT CASE WHEN COLUMNPROPERTY(OBJECT_ID('dbo.HOA_DON_BAN'),'MaHDB','IsIdentity') = 1 THEN 1 ELSE 0 END", conn, tran))
            {
                hasIdentity = Convert.ToInt32(cmdIdentity.ExecuteScalar() ?? 0) == 1;
            }

            if (hasIdentity)
            {
                using SqlCommand cmd = new SqlCommand("INSERT INTO dbo.HOA_DON_BAN (NgayBan, MaNV, MaKH, TongTien) OUTPUT INSERTED.MaHDB VALUES (@NgayBan, @MaNV, @MaKH, @TongTien);", conn, tran);
                cmd.Parameters.Add("@NgayBan", SqlDbType.DateTime).Value = DateTime.Now;
                cmd.Parameters.Add("@MaNV", SqlDbType.VarChar, 20).Value = _selectedMaNvDbValue ?? _loggedInMaNV;
                cmd.Parameters.Add("@MaKH", SqlDbType.Int).Value = maKh.HasValue ? maKh.Value : DBNull.Value;
                cmd.Parameters.Add("@TongTien", SqlDbType.Decimal).Value = tongTien;
                cmd.Parameters["@TongTien"].Precision = 18;
                cmd.Parameters["@TongTien"].Scale = 2;
                object? inserted = cmd.ExecuteScalar();
                return Convert.ToInt32(inserted);
            }

            int maHdb = GenerateNextMaHoaDonDataInt(conn, tran);
            using (SqlCommand cmd = new SqlCommand("INSERT INTO dbo.HOA_DON_BAN (MaHDB, NgayBan, MaNV, MaKH, TongTien) VALUES (@MaHDB, @NgayBan, @MaNV, @MaKH, @TongTien)", conn, tran))
            {
                cmd.Parameters.Add("@MaHDB", SqlDbType.Int).Value = maHdb;
                cmd.Parameters.Add("@NgayBan", SqlDbType.DateTime).Value = DateTime.Now;
                cmd.Parameters.Add("@MaNV", SqlDbType.VarChar, 20).Value = _selectedMaNvDbValue ?? _loggedInMaNV;
                cmd.Parameters.Add("@MaKH", SqlDbType.Int).Value = maKh.HasValue ? maKh.Value : DBNull.Value;
                cmd.Parameters.Add("@TongTien", SqlDbType.Decimal).Value = tongTien;
                cmd.Parameters["@TongTien"].Precision = 18;
                cmd.Parameters["@TongTien"].Scale = 2;
                cmd.ExecuteNonQuery();
            }

            return maHdb;
        }

        private static void InsertChiTietHoaDonData(SqlConnection conn, SqlTransaction tran, int maHdb, int maMon, int maDvpv, int soLuong)
        {
            using SqlCommand cmd = new SqlCommand("INSERT INTO dbo.CT_HOA_DON_BAN (MaHDB, MaMon, MaDVPV, SoLuong) VALUES (@MaHDB, @MaMon, @MaDVPV, @SoLuong)", conn, tran);
            cmd.Parameters.Add("@MaHDB", SqlDbType.Int).Value = maHdb;
            cmd.Parameters.Add("@MaMon", SqlDbType.Int).Value = maMon;
            cmd.Parameters.Add("@MaDVPV", SqlDbType.Int).Value = maDvpv;
            cmd.Parameters.Add("@SoLuong", SqlDbType.Int).Value = soLuong;
            cmd.ExecuteNonQuery();
        }

        private static void TruKhoNguyenLieuData(SqlConnection conn, SqlTransaction tran, int maMon, int maDvpv, int soLuong)
        {
            // First, verify stock is sufficient for all ingredients used by this menu item/size
            const string checkSql = @"SELECT nl.MaNL, nl.SoLuongTon, ISNULL(dm.SoLuongSuDung,0) AS SoLuongSuDung
FROM dbo.DINH_MUC_MON dm
INNER JOIN dbo.NGUYEN_LIEU nl ON nl.MaNL = dm.MaNL
WHERE dm.MaMon = @MaMon AND (dm.MaDVPV = @MaDVPV OR NOT EXISTS (SELECT 1 FROM dbo.DINH_MUC_MON WHERE MaMon = @MaMon AND MaDVPV = @MaDVPV))";

            using (SqlCommand checkCmd = new SqlCommand(checkSql, conn, tran))
            {
                checkCmd.Parameters.Add("@MaMon", SqlDbType.Int).Value = maMon;
                checkCmd.Parameters.Add("@MaDVPV", SqlDbType.Int).Value = maDvpv;

                using SqlDataReader reader = checkCmd.ExecuteReader();
                while (reader.Read())
                {
                    decimal soLuongTon = ToDecimalValue(reader["SoLuongTon"]);
                    decimal soLuongSuDung = ToDecimalValue(reader["SoLuongSuDung"]);
                    decimal required = soLuongSuDung * soLuong;
                    if (soLuongTon < required)
                    {
                        // Not enough stock for this ingredient
                        reader.Close();
                        throw new InvalidOperationException("Nguyên liệu không đủ");
                    }
                }
            }

            // If all ingredients have sufficient stock, apply the deduction
            using SqlCommand cmd = new SqlCommand(@"UPDATE nl SET nl.SoLuongTon = nl.SoLuongTon - dm.SoLuongSuDung * @SoLuong FROM dbo.NGUYEN_LIEU nl INNER JOIN dbo.DINH_MUC_MON dm ON dm.MaNL = nl.MaNL WHERE dm.MaMon = @MaMon AND (dm.MaDVPV = @MaDVPV OR NOT EXISTS (SELECT 1 FROM dbo.DINH_MUC_MON WHERE MaMon = @MaMon AND MaDVPV = @MaDVPV))", conn, tran);
            cmd.Parameters.Add("@SoLuong", SqlDbType.Int).Value = soLuong;
            cmd.Parameters.Add("@MaMon", SqlDbType.Int).Value = maMon;
            cmd.Parameters.Add("@MaDVPV", SqlDbType.Int).Value = maDvpv;
            cmd.ExecuteNonQuery();
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
            btn_QLMA.BackColor = Color.Bisque;
            label7.ForeColor = SystemColors.ControlText;
            btn_QLHDN.BackColor = Color.Bisque;
            label8.ForeColor = SystemColors.ControlText;

            EnsureBanHangPanel();
            _banHangPanel!.BringToFront();
            _banHangPanel.Visible = true;

            if (_muaHangEmbedded is not null && !_muaHangEmbedded.IsDisposed)
            {
                _muaHangEmbedded.Hide();
            }

            if (_khachHangEmbedded is not null && !_khachHangEmbedded.IsDisposed)
            {
                _khachHangEmbedded.Hide();
            }
        }

        private void ShowMuaHangInRightPanel()
        {
            pnlFormNhanVien.Visible = false;
            pnlDanhSachNhanVien.Visible = false;
            lb_QLNhanVienTitle.Text = "Mua hàng";

            btn_QLNV.BackColor = Color.Bisque;
            label4.ForeColor = SystemColors.ControlText;
            btn_QLKH.BackColor = Color.Bisque;
            label6.ForeColor = SystemColors.ControlText;
            btn_QLMA.BackColor = Color.Salmon;
            label7.ForeColor = Color.White;
            btn_QLHDN.BackColor = Color.Bisque;
            label8.ForeColor = SystemColors.ControlText;

            if (_banHangPanel is not null)
            {
                _banHangPanel.Visible = false;
            }

            if (_khachHangEmbedded is not null && !_khachHangEmbedded.IsDisposed)
            {
                _khachHangEmbedded.Hide();
            }

            if (_muaHangEmbedded is null || _muaHangEmbedded.IsDisposed)
            {
                _muaHangEmbedded = new MuaHang(_selectedMaNvDbValue ?? _loggedInMaNV)
                {
                    TopLevel = false,
                    FormBorderStyle = FormBorderStyle.None,
                    Dock = DockStyle.Fill
                };
                hcnt_Khung.Controls.Add(_muaHangEmbedded);
            }

            _muaHangEmbedded.BringToFront();
            _muaHangEmbedded.Show();
        }

        private void ShowKhachHangInRightPanel()
        {
            pnlFormNhanVien.Visible = false;
            pnlDanhSachNhanVien.Visible = false;
            lb_QLNhanVienTitle.Text = "Khách hàng";

            btn_QLNV.BackColor = Color.Bisque;
            label4.ForeColor = SystemColors.ControlText;
            btn_QLKH.BackColor = Color.Bisque;
            label6.ForeColor = SystemColors.ControlText;
            btn_QLMA.BackColor = Color.Bisque;
            label7.ForeColor = SystemColors.ControlText;
            btn_QLHDN.BackColor = Color.Salmon;
            label8.ForeColor = Color.White;

            if (_banHangPanel is not null)
            {
                _banHangPanel.Visible = false;
            }

            if (_muaHangEmbedded is not null && !_muaHangEmbedded.IsDisposed)
            {
                _muaHangEmbedded.Hide();
            }

            if (_khachHangEmbedded is null || _khachHangEmbedded.IsDisposed)
            {
                _khachHangEmbedded = new KhachHang
                {
                    TopLevel = false,
                    FormBorderStyle = FormBorderStyle.None,
                    Dock = DockStyle.Fill
                };
                hcnt_Khung.Controls.Add(_khachHangEmbedded);
            }

            _khachHangEmbedded.BringToFront();
            _khachHangEmbedded.Show();
        }

        private void ShowThongTinCaNhanPanel()
        {
            if (_banHangPanel is not null)
            {
                _banHangPanel.Visible = false;
            }

            if (_muaHangEmbedded is not null && !_muaHangEmbedded.IsDisposed)
            {
                _muaHangEmbedded.Hide();
            }

            if (_khachHangEmbedded is not null && !_khachHangEmbedded.IsDisposed)
            {
                _khachHangEmbedded.Hide();
            }

            lb_QLNhanVienTitle.Text = "Thông tin cá nhân";
            pnlFormNhanVien.Visible = true;
            pnlDanhSachNhanVien.Visible = false;

            btn_QLNV.BackColor = Color.Salmon;
            label4.ForeColor = Color.White;
            btn_QLKH.BackColor = Color.Bisque;
            label6.ForeColor = SystemColors.ControlText;
            btn_QLMA.BackColor = Color.Bisque;
            label7.ForeColor = SystemColors.ControlText;
            btn_QLHDN.BackColor = Color.Bisque;
            label8.ForeColor = SystemColors.ControlText;
        }

        private void UpdatePasswordUiState()
        {
            bool isCreateMode = !_isEditingExisting;
            _btnDatLaiMatKhau.Visible = !isCreateMode;
            _btnXemLichTruc.Visible = !isCreateMode;

            _txtMatKhau.ReadOnly = !isCreateMode;
            _txtMatKhau.PlaceholderText = isCreateMode ? string.Empty : "Nhấn 'Đặt lại mật khẩu'";
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

            lblMatKhau.Location = new Point(40, 208);
            pnlMatKhauInput.Location = new Point(40, 237);
            pnlMatKhauInput.Size = new Size(360, 33);
            _txtMatKhau.Size = new Size(336, 20);

            lblChucVu.Location = new Point(430, 208);
            _cboChucVu.Location = new Point(430, 237);
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
            if (_btnYeuCauNghiPhep is null)
            {
                _btnYeuCauNghiPhep = new Button
                {
                    Text = "Yêu cầu nghỉ phép",
                    BackColor = Color.SandyBrown,
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                    Location = new Point(40, 370),
                    Size = new Size(260, 42)
                };
                _btnYeuCauNghiPhep.FlatAppearance.BorderSize = 0;
                _btnYeuCauNghiPhep.Click += BtnYeuCauNghiPhep_Click;
                pnlFormNhanVien.Controls.Add(_btnYeuCauNghiPhep);
            }

            if (_btnYeuCauNghiHan is null)
            {
                _btnYeuCauNghiHan = new Button
                {
                    Text = "Yêu cầu nghỉ hẳn",
                    BackColor = Color.IndianRed,
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                    Location = new Point(320, 370),
                    Size = new Size(260, 42)
                };
                _btnYeuCauNghiHan.FlatAppearance.BorderSize = 0;
                _btnYeuCauNghiHan.Click += BtnYeuCauNghiHan_Click;
                pnlFormNhanVien.Controls.Add(_btnYeuCauNghiHan);
            }
        }

        private void BtnYeuCauNghiPhep_Click(object? sender, EventArgs e)
        {
            GuiYeuCauNghi("Yêu cầu nghỉ phép");
        }

        private void BtnYeuCauNghiHan_Click(object? sender, EventArgs e)
        {
            GuiYeuCauNghi("Yêu cầu nghỉ hẳn");
        }

        private void GuiYeuCauNghi(string trangThaiYeuCau)
        {
            if (string.IsNullOrWhiteSpace(_selectedMaNvDbValue))
            {
                MessageBox.Show("Không xác định được nhân viên đăng nhập.", "Thiếu dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!TryPromptLeaveReason(trangThaiYeuCau, out string lyDo))
            {
                return;
            }

            DialogResult confirm = MessageBox.Show(
                $"Bạn chắc chắn muốn gửi '{trangThaiYeuCau}'?\n\nLý do: {lyDo}",
                "Xác nhận gửi yêu cầu",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes)
            {
                return;
            }

            const string sql = "UPDATE dbo.NHAN_VIEN SET TrangThai = @TrangThai WHERE MaNV = @MaNV";

            try
            {
                using SqlConnection conn = DbHelper.GetConnection();
                conn.Open();

                using SqlTransaction tran = conn.BeginTransaction();
                using SqlCommand cmd = new SqlCommand(sql, conn, tran);
                cmd.Parameters.Add("@TrangThai", SqlDbType.NVarChar, 50).Value = trangThaiYeuCau;
                cmd.Parameters.Add("@MaNV", SqlDbType.VarChar, 20).Value = _selectedMaNvDbValue;

                int rows = cmd.ExecuteNonQuery();
                if (rows == 0)
                {
                    tran.Rollback();
                    MessageBox.Show("Gửi yêu cầu thất bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                const string sqlSaveReason = @"
IF OBJECT_ID(N'dbo.YEU_CAU_NGHI', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.YEU_CAU_NGHI
    (
        MaYeuCau INT IDENTITY(1,1) PRIMARY KEY,
        MaNV VARCHAR(20) NOT NULL,
        LoaiYeuCau NVARCHAR(50) NOT NULL,
        LyDo NVARCHAR(500) NOT NULL,
        NgayTao DATETIME NOT NULL DEFAULT GETDATE(),
        TrangThaiXuLy NVARCHAR(30) NOT NULL DEFAULT N'Chờ duyệt'
    );
END;

INSERT INTO dbo.YEU_CAU_NGHI (MaNV, LoaiYeuCau, LyDo)
VALUES (@MaNV, @LoaiYeuCau, @LyDo);";

                using SqlCommand cmdSaveReason = new SqlCommand(sqlSaveReason, conn, tran);
                cmdSaveReason.Parameters.Add("@MaNV", SqlDbType.VarChar, 20).Value = _selectedMaNvDbValue;
                cmdSaveReason.Parameters.Add("@LoaiYeuCau", SqlDbType.NVarChar, 50).Value = trangThaiYeuCau;
                cmdSaveReason.Parameters.Add("@LyDo", SqlDbType.NVarChar, 500).Value = lyDo;
                cmdSaveReason.ExecuteNonQuery();

                tran.Commit();

                MessageBox.Show("Đã gửi yêu cầu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không thể gửi yêu cầu.\n{ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static bool TryPromptLeaveReason(string loaiYeuCau, out string lyDo)
        {
            lyDo = string.Empty;

            using Form dialog = new Form
            {
                Text = loaiYeuCau,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterParent,
                MinimizeBox = false,
                MaximizeBox = false,
                ClientSize = new Size(430, 220)
            };

            Label lblReason = new Label { Text = "Nhập lý do:", Left = 12, Top = 15, AutoSize = true };
            TextBox txtReason = new TextBox
            {
                Left = 12,
                Top = 40,
                Width = 404,
                Height = 120,
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                MaxLength = 500
            };

            Button btnOk = new Button { Text = "Gửi", Left = 260, Top = 176, Width = 75, DialogResult = DialogResult.OK };
            Button btnCancel = new Button { Text = "Hủy", Left = 341, Top = 176, Width = 75, DialogResult = DialogResult.Cancel };

            dialog.Controls.Add(lblReason);
            dialog.Controls.Add(txtReason);
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
            OpenAndClose(new TrangNhanVien1(_selectedMaNvDbValue ?? _loggedInMaNV));
        }

        private void btn_QLNCC_Paint(object sender, PaintEventArgs e)
        {

        }

        private void OpenAndClose(Form target)
        {
            AdminNavigationManager.Navigate(this, target);
        }
    }
}
