using PBL3.DataBase;
using PBL3.UI;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace PBL3
{
    public partial class MuaHang : Form
    {
        private readonly string _maNv;
        private readonly bool _isAdminPopup;
        private readonly string? _preselectedMaNcc;
        private readonly string? _preselectedMaNl;
        private readonly DataTable _phieuNhapTable = new DataTable();
        private CheckBox? _chkNguyenLieuMoi;
        private TextBox? _txtNguyenLieuMoi;
        private Label? _lblNguyenLieuMoi;
        private bool _isNavigating;

        public MuaHang() : this("1")
        {
        }

        public MuaHang(string maNv, bool isAdminPopup = false, string? preselectedMaNcc = null, string? preselectedMaNl = null)
        {
            _maNv = maNv;
            _isAdminPopup = isAdminPopup;
            _preselectedMaNcc = preselectedMaNcc;
            _preselectedMaNl = preselectedMaNl;
            InitializeComponent();
        }

        private void MuaHang_Load(object? sender, EventArgs e)
        {
            try
            {
                InitPhieuNhapGrid();
                LoadNguyenLieu();
                LoadNhaCungCap();
                LoadDonViTinh();
                EnsureNguyenLieuMoiControls();
                ApplyAdminPopupLayout();
                ApplyInitialSelections();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không thể tải trang mua hàng.\n{ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MuaHang_Resize(object? sender, EventArgs e)
        {
            ApplyResponsiveLayout();
        }

        private void EnsureNguyenLieuMoiControls()
        {
            if (_chkNguyenLieuMoi is not null)
            {
                return;
            }

            _chkNguyenLieuMoi = new CheckBox
            {
                Text = "Nguyên liệu mới",
                AutoSize = true
            };
            _chkNguyenLieuMoi.CheckedChanged += (_, _) => UpdateNguyenLieuMoiState();

            _lblNguyenLieuMoi = new Label
            {
                Text = "Tên nguyên liệu",
                Font = lblDonGia.Font,
                AutoSize = true,
                Visible = false
            };

            _txtNguyenLieuMoi = new TextBox
            {
                Visible = false
            };

            hcnt_Khung.Controls.Add(_chkNguyenLieuMoi);
            hcnt_Khung.Controls.Add(_lblNguyenLieuMoi);
            hcnt_Khung.Controls.Add(_txtNguyenLieuMoi);

            _chkNguyenLieuMoi.Location = new Point(493, 84);
            _lblNguyenLieuMoi.Location = new Point(493, 112);
            _txtNguyenLieuMoi.Location = new Point(493, 135);
            _txtNguyenLieuMoi.Size = new Size(323, 27);
        }

        private void UpdateNguyenLieuMoiState()
        {
            bool isNew = _chkNguyenLieuMoi?.Checked == true;
            dgvNguyenLieu.Enabled = !isNew;

            if (isNew)
            {
                LoadNhaCungCap();
            }

            if (_lblNguyenLieuMoi is not null)
            {
                _lblNguyenLieuMoi.Visible = isNew;
            }

            if (_txtNguyenLieuMoi is not null)
            {
                _txtNguyenLieuMoi.Visible = isNew;
                if (!isNew)
                {
                    _txtNguyenLieuMoi.Clear();
                }
            }
        }

        private void ApplyResponsiveLayout()
        {
            return;
        }

        private void ApplyAdminPopupLayout()
        {
            if (!_isAdminPopup)
            {
                return;
            }

            hcnt_KhungMenuAD.Visible = false;
            pb_Admin.Visible = false;
            lb_Admin.Visible = false;
            btn_DangXuat.Visible = false;

            hcnt_Khung.Location = new Point(12, 12);
            roundedPanel1.Size = new Size(900, 760);
            this.ClientSize = new Size(928, 790);
            this.StartPosition = FormStartPosition.CenterParent;
        }

        private void ApplyInitialSelections()
        {
            if (!string.IsNullOrWhiteSpace(_preselectedMaNl))
            {
                SelectNguyenLieuByMa(_preselectedMaNl);
            }

            if (!string.IsNullOrWhiteSpace(_preselectedMaNcc) && cboNhaCungCap.DataSource is not null)
            {
                try
                {
                    cboNhaCungCap.SelectedValue = _preselectedMaNcc;
                }
                catch
                {
                }
            }
        }

        private void SelectNguyenLieuByMa(string maNl)
        {
            string key = maNl.Trim();
            foreach (DataGridViewRow row in dgvNguyenLieu.Rows)
            {
                if (row.IsNewRow)
                {
                    continue;
                }

                string current = Convert.ToString(row.Cells["MaNL"].Value) ?? string.Empty;
                if (!string.Equals(current, key, StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                row.Selected = true;
                if (row.Cells.Count > 0)
                {
                    dgvNguyenLieu.CurrentCell = row.Cells[0];
                }

                SyncInputFromSelectedNguyenLieu();
                break;
            }
        }

        private void btn_QLNV_Click(object? sender, EventArgs e) => OpenAndClose(new TrangNhanVien1(_maNv));
        private void btn_QLNCC_Click(object? sender, EventArgs e) => OpenAndClose(new TrangHoaDon(_maNv));
        private void btn_QLKH_Click(object? sender, EventArgs e) => OpenAndClose(new BanHang(_maNv));
        private void btn_QLHDN_Click(object? sender, EventArgs e) => OpenAndClose(new KhachHang(_maNv));

        private void btn_DangXuat_Click(object? sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn đăng xuất?",
                    "Đăng xuất", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                AdminNavigationManager.Logout(this);
            }
        }

        private void OpenAndClose(Form target)
        {
            AdminNavigationManager.Navigate(this, target);
        }

        private void InitPhieuNhapGrid()
        {
            _phieuNhapTable.Columns.Add("MaNL", typeof(string));
            _phieuNhapTable.Columns.Add("TenNL", typeof(string));
            _phieuNhapTable.Columns.Add("DonViTinh", typeof(string));
            _phieuNhapTable.Columns.Add("SoLuong", typeof(decimal));
            _phieuNhapTable.Columns.Add("DonGia", typeof(decimal));
            _phieuNhapTable.Columns.Add("ThanhTien", typeof(decimal), "SoLuong * DonGia");

            dgvPhieuNhap.DataSource = _phieuNhapTable;
            dgvPhieuNhap.Columns["MaNL"].Visible = false;
            dgvPhieuNhap.Columns["DonGia"].DefaultCellStyle.Format = "N0";
            dgvPhieuNhap.Columns["ThanhTien"].DefaultCellStyle.Format = "N0";
        }

        private void LoadNguyenLieu()
        {
            const string sql = @"
SELECT MaNL, TenNL, DonViTinh, ISNULL(GiaNhap, 0) AS GiaNhap, ISNULL(SoLuongTon, 0) AS SoLuongTon
FROM dbo.NGUYEN_LIEU
ORDER BY MaNL";

            using SqlConnection conn = DbHelper.GetConnection();
            using SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgvNguyenLieu.DataSource = dt;

            if (dgvNguyenLieu.Columns.Contains("GiaNhap"))
            {
                dgvNguyenLieu.Columns["GiaNhap"].DefaultCellStyle.Format = "N0";
            }

            if (dgvNguyenLieu.Rows.Count > 0)
            {
                dgvNguyenLieu.Rows[0].Selected = true;
                SyncInputFromSelectedNguyenLieu();
            }
        }

        private void LoadNhaCungCap(string? maNl = null)
        {
            using SqlConnection conn = DbHelper.GetConnection();
            string sql = string.IsNullOrWhiteSpace(maNl)
                ? "SELECT MaNCC, TenNCC FROM dbo.NHA_CUNG_CAP ORDER BY MaNCC"
                : @"SELECT DISTINCT ncc.MaNCC, ncc.TenNCC
FROM dbo.NHA_CUNG_CAP ncc
INNER JOIN dbo.HOA_DON_NHAP hdn ON hdn.MaNCC = ncc.MaNCC
INNER JOIN dbo.CT_HOA_DON_NHAP ctn ON ctn.MaHDN = hdn.MaHDN
WHERE ctn.MaNL = @MaNL
ORDER BY ncc.MaNCC";

            using SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            if (!string.IsNullOrWhiteSpace(maNl))
            {
                da.SelectCommand.Parameters.Add("@MaNL", SqlDbType.VarChar, 20).Value = maNl;
            }

            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count == 0 && !string.IsNullOrWhiteSpace(maNl))
            {
                using SqlDataAdapter daAll = new SqlDataAdapter("SELECT MaNCC, TenNCC FROM dbo.NHA_CUNG_CAP ORDER BY MaNCC", conn);
                dt = new DataTable();
                daAll.Fill(dt);
            }

            cboNhaCungCap.DataSource = dt;
            cboNhaCungCap.DisplayMember = "TenNCC";
            cboNhaCungCap.ValueMember = "MaNCC";
        }

        private void LoadDonViTinh()
        {
            const string sql = @"SELECT DISTINCT DonViTinh
FROM dbo.NGUYEN_LIEU
WHERE DonViTinh IS NOT NULL AND LTRIM(RTRIM(DonViTinh)) <> ''
ORDER BY DonViTinh";
            using SqlConnection conn = DbHelper.GetConnection();
            using SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);

            cboDonViTinh.DataSource = dt;
            cboDonViTinh.DisplayMember = "DonViTinh";
            cboDonViTinh.ValueMember = "DonViTinh";
        }

        private void dgvNguyenLieu_SelectionChanged(object? sender, EventArgs e)
        {
            SyncInputFromSelectedNguyenLieu();
        }

        private void SyncInputFromSelectedNguyenLieu()
        {
            if (dgvNguyenLieu.SelectedRows.Count == 0)
            {
                return;
            }

            DataGridViewRow row = dgvNguyenLieu.SelectedRows[0];
            string maNl = Convert.ToString(row.Cells["MaNL"].Value) ?? string.Empty;
            string donViTinh = Convert.ToString(row.Cells["DonViTinh"].Value) ?? string.Empty;
            decimal giaNhap = row.Cells["GiaNhap"].Value is null ? 0m : Convert.ToDecimal(row.Cells["GiaNhap"].Value);

            LoadNhaCungCap(maNl);

            if (!string.IsNullOrWhiteSpace(donViTinh) && cboDonViTinh.Items.Count > 0)
            {
                cboDonViTinh.SelectedValue = donViTinh;
            }

            txtDonGia.Text = giaNhap.ToString("0.##");
        }

        private void btnThem_Click(object? sender, EventArgs e)
        {
            bool isNewIngredient = _chkNguyenLieuMoi?.Checked == true;
            if (!isNewIngredient && dgvNguyenLieu.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn nguyên liệu.", "Thiếu dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cboNhaCungCap.SelectedValue is null)
            {
                MessageBox.Show("Vui lòng chọn nhà cung cấp.", "Thiếu dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cboDonViTinh.SelectedValue is null)
            {
                MessageBox.Show("Vui lòng chọn đơn vị tính.", "Thiếu dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtDonGia.Text.Trim(), out decimal donGia) || donGia < 0)
            {
                MessageBox.Show("Đơn giá không hợp lệ.", "Dữ liệu sai", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maNl;
            string tenNl;
            if (isNewIngredient)
            {
                tenNl = (_txtNguyenLieuMoi?.Text ?? string.Empty).Trim();
                if (string.IsNullOrWhiteSpace(tenNl))
                {
                    MessageBox.Show("Vui lòng nhập tên nguyên liệu mới.", "Thiếu dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                maNl = string.Empty;
            }
            else
            {
                DataGridViewRow row = dgvNguyenLieu.SelectedRows[0];
                maNl = Convert.ToString(row.Cells["MaNL"].Value) ?? string.Empty;
                tenNl = Convert.ToString(row.Cells["TenNL"].Value) ?? string.Empty;
            }

            string dvt = Convert.ToString(cboDonViTinh.SelectedValue) ?? string.Empty;
            decimal soLuong = nudSoLuong.Value;

            DataRow? existed = _phieuNhapTable.AsEnumerable()
                .FirstOrDefault(r => string.Equals(Convert.ToString(r["MaNL"]), maNl, StringComparison.OrdinalIgnoreCase)
                                  && string.Equals(Convert.ToString(r["DonViTinh"]), dvt, StringComparison.OrdinalIgnoreCase));

            if (existed is null)
            {
                _phieuNhapTable.Rows.Add(maNl, tenNl, dvt, soLuong, donGia);
            }
            else
            {
                existed["SoLuong"] = Convert.ToDecimal(existed["SoLuong"]) + soLuong;
                existed["DonGia"] = donGia;
            }

            UpdateTongTien();
        }

        private void btnXoaDong_Click(object? sender, EventArgs e)
        {
            if (dgvPhieuNhap.SelectedRows.Count == 0)
            {
                return;
            }

            foreach (DataGridViewRow row in dgvPhieuNhap.SelectedRows)
            {
                if (!row.IsNewRow)
                {
                    dgvPhieuNhap.Rows.Remove(row);
                }
            }

            UpdateTongTien();
        }

        private void UpdateTongTien()
        {
            decimal total = _phieuNhapTable.AsEnumerable().Sum(r => r.Field<decimal>("ThanhTien"));
            lblTongTien.Text = $"Tổng tiền: {total:N0} đ";
        }

        private void btnLuuNhapHang_Click(object? sender, EventArgs e)
        {
            if (_phieuNhapTable.Rows.Count == 0)
            {
                MessageBox.Show("Phiếu nhập đang trống.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (cboNhaCungCap.SelectedValue is null)
            {
                MessageBox.Show("Nhà cung cấp là bắt buộc.", "Thiếu dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal tongTien = _phieuNhapTable.AsEnumerable().Sum(r => r.Field<decimal>("ThanhTien"));
            object maNcc = int.TryParse(Convert.ToString(cboNhaCungCap.SelectedValue), out int maNccInt) ? maNccInt : (Convert.ToString(cboNhaCungCap.SelectedValue) ?? string.Empty);

            try
            {
                using SqlConnection conn = DbHelper.GetConnection();
                conn.Open();
                using SqlTransaction tran = conn.BeginTransaction();

                object maHdn = InsertHoaDonNhap(conn, tran, maNcc, tongTien);

                foreach (DataRow row in _phieuNhapTable.Rows)
                {
                    object maNlValue = row["MaNL"];
                    if (string.IsNullOrWhiteSpace(Convert.ToString(maNlValue)))
                    {
                        string tenNlMoi = Convert.ToString(row["TenNL"]) ?? string.Empty;
                        string dvtMoi = Convert.ToString(row["DonViTinh"]) ?? string.Empty;
                        decimal donGiaMoi = Convert.ToDecimal(row["DonGia"]);
                        maNlValue = EnsureNguyenLieuExists(conn, tran, tenNlMoi, dvtMoi, donGiaMoi);
                    }

                    decimal soLuong = Convert.ToDecimal(row["SoLuong"]);
                    decimal donGia = Convert.ToDecimal(row["DonGia"]);

                    InsertChiTietNhap(conn, tran, maHdn, maNlValue, soLuong, donGia);
                    UpdateSoLuongTon(conn, tran, maNlValue, soLuong, donGia);
                }

                tran.Commit();
                _phieuNhapTable.Clear();
                UpdateTongTien();
                LoadNguyenLieu();
                MessageBox.Show("Lưu phiếu nhập thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lưu phiếu nhập thất bại.\n{ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static int EnsureNguyenLieuExists(SqlConnection conn, SqlTransaction tran, string tenNl, string donViTinh, decimal donGia)
        {
            using (SqlCommand cmdFind = new SqlCommand("SELECT TOP 1 MaNL FROM dbo.NGUYEN_LIEU WHERE TenNL = @TenNL AND DonViTinh = @DonViTinh ORDER BY MaNL", conn, tran))
            {
                cmdFind.Parameters.Add("@TenNL", SqlDbType.NVarChar, 100).Value = tenNl;
                cmdFind.Parameters.Add("@DonViTinh", SqlDbType.NVarChar, 30).Value = donViTinh;
                object? exists = cmdFind.ExecuteScalar();
                if (exists is not null && exists != DBNull.Value)
                {
                    return Convert.ToInt32(exists);
                }
            }

            bool hasNguongColumn;
            using (SqlCommand cmdCol = new SqlCommand("SELECT CASE WHEN EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='dbo' AND TABLE_NAME='NGUYEN_LIEU' AND COLUMN_NAME='NguongToiThieu') THEN 1 ELSE 0 END", conn, tran))
            {
                hasNguongColumn = Convert.ToInt32(cmdCol.ExecuteScalar()) == 1;
            }

            string insertSql = hasNguongColumn
                ? "INSERT INTO dbo.NGUYEN_LIEU (TenNL, DonViTinh, SoLuongTon, GiaNhap, NguongToiThieu) OUTPUT INSERTED.MaNL VALUES (@TenNL, @DonViTinh, 0, @GiaNhap, 0)"
                : "INSERT INTO dbo.NGUYEN_LIEU (TenNL, DonViTinh, SoLuongTon, GiaNhap) OUTPUT INSERTED.MaNL VALUES (@TenNL, @DonViTinh, 0, @GiaNhap)";

            using SqlCommand cmdInsert = new SqlCommand(insertSql, conn, tran);
            cmdInsert.Parameters.Add("@TenNL", SqlDbType.NVarChar, 100).Value = tenNl;
            cmdInsert.Parameters.Add("@DonViTinh", SqlDbType.NVarChar, 30).Value = donViTinh;
            cmdInsert.Parameters.Add("@GiaNhap", SqlDbType.Decimal).Value = donGia;
            cmdInsert.Parameters["@GiaNhap"].Precision = 18;
            cmdInsert.Parameters["@GiaNhap"].Scale = 2;
            object? newId = cmdInsert.ExecuteScalar();
            return Convert.ToInt32(newId);
        }

        private static int GenerateNextMaHdnInt(SqlConnection conn, SqlTransaction tran)
        {
            using SqlCommand cmd = new SqlCommand("SELECT ISNULL(MAX(TRY_CAST(MaHDN AS INT)), 0) + 1 FROM dbo.HOA_DON_NHAP", conn, tran);
            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        private object InsertHoaDonNhap(SqlConnection conn, SqlTransaction tran, object maNcc, decimal tongTien)
        {
            bool maHdnIdentity;
            using (SqlCommand cmdIdentity = new SqlCommand("SELECT CASE WHEN COLUMNPROPERTY(OBJECT_ID('dbo.HOA_DON_NHAP'),'MaHDN','IsIdentity') = 1 THEN 1 ELSE 0 END", conn, tran))
            {
                maHdnIdentity = Convert.ToInt32(cmdIdentity.ExecuteScalar() ?? 0) == 1;
            }

            bool maHdnNumeric = IsColumnNumeric(conn, tran, "HOA_DON_NHAP", "MaHDN");
            bool maNvNumeric = IsColumnNumeric(conn, tran, "HOA_DON_NHAP", "MaNV");
            bool maNccNumeric = IsColumnNumeric(conn, tran, "HOA_DON_NHAP", "MaNCC");

            object maNvValue = maNvNumeric && int.TryParse(_maNv, out int nvInt) ? nvInt : _maNv;
            object maNccValue = maNccNumeric ? Convert.ToInt32(maNcc) : maNcc;

            if (maHdnIdentity)
            {
                using SqlCommand cmd = new SqlCommand("INSERT INTO dbo.HOA_DON_NHAP (NgayNhap, MaNV, MaNCC, TongTien) OUTPUT INSERTED.MaHDN VALUES (@NgayNhap, @MaNV, @MaNCC, @TongTien)", conn, tran);
                cmd.Parameters.Add("@NgayNhap", SqlDbType.DateTime).Value = DateTime.Now;
                cmd.Parameters.Add("@MaNV", maNvNumeric ? SqlDbType.Int : SqlDbType.VarChar, 20).Value = maNvValue;
                cmd.Parameters.Add("@MaNCC", maNccNumeric ? SqlDbType.Int : SqlDbType.VarChar, 20).Value = maNccValue;
                cmd.Parameters.Add("@TongTien", SqlDbType.Decimal).Value = tongTien;
                cmd.Parameters["@TongTien"].Precision = 18;
                cmd.Parameters["@TongTien"].Scale = 2;
                return cmd.ExecuteScalar() ?? 0;
            }

            object maHdnValue = maHdnNumeric ? GenerateNextMaHdnInt(conn, tran) : $"HDN{GenerateNextMaHdnInt(conn, tran)}";
            using (SqlCommand cmd = new SqlCommand("INSERT INTO dbo.HOA_DON_NHAP (MaHDN, NgayNhap, MaNV, MaNCC, TongTien) VALUES (@MaHDN, @NgayNhap, @MaNV, @MaNCC, @TongTien)", conn, tran))
            {
                cmd.Parameters.Add("@MaHDN", maHdnNumeric ? SqlDbType.Int : SqlDbType.VarChar, 20).Value = maHdnValue;
                cmd.Parameters.Add("@NgayNhap", SqlDbType.DateTime).Value = DateTime.Now;
                cmd.Parameters.Add("@MaNV", maNvNumeric ? SqlDbType.Int : SqlDbType.VarChar, 20).Value = maNvValue;
                cmd.Parameters.Add("@MaNCC", maNccNumeric ? SqlDbType.Int : SqlDbType.VarChar, 20).Value = maNccValue;
                cmd.Parameters.Add("@TongTien", SqlDbType.Decimal).Value = tongTien;
                cmd.Parameters["@TongTien"].Precision = 18;
                cmd.Parameters["@TongTien"].Scale = 2;
                cmd.ExecuteNonQuery();
            }

            return maHdnValue;
        }

        private static void InsertChiTietNhap(SqlConnection conn, SqlTransaction tran, object maHdn, object maNlValue, decimal soLuong, decimal donGia)
        {
            bool maHdnNumeric = IsColumnNumeric(conn, tran, "CT_HOA_DON_NHAP", "MaHDN");
            using SqlCommand cmd = new SqlCommand("INSERT INTO dbo.CT_HOA_DON_NHAP (MaHDN, MaNL, SoLuong, DonGia) VALUES (@MaHDN, @MaNL, @SoLuong, @DonGia)", conn, tran);
            cmd.Parameters.Add("@MaHDN", maHdnNumeric ? SqlDbType.Int : SqlDbType.VarChar, 20).Value = maHdn;
            cmd.Parameters.Add("@MaNL", SqlDbType.Int).Value = Convert.ToInt32(maNlValue);
            cmd.Parameters.Add("@SoLuong", SqlDbType.Decimal).Value = soLuong;
            cmd.Parameters.Add("@DonGia", SqlDbType.Decimal).Value = donGia;
            cmd.Parameters["@SoLuong"].Precision = 18;
            cmd.Parameters["@SoLuong"].Scale = 2;
            cmd.Parameters["@DonGia"].Precision = 18;
            cmd.Parameters["@DonGia"].Scale = 2;
            cmd.ExecuteNonQuery();
        }

        private static bool IsColumnNumeric(SqlConnection conn, SqlTransaction tran, string tableName, string columnName)
        {
            using SqlCommand cmd = new SqlCommand(@"SELECT TOP 1 DATA_TYPE
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_SCHEMA='dbo' AND TABLE_NAME=@TableName AND COLUMN_NAME=@ColumnName", conn, tran);
            cmd.Parameters.Add("@TableName", SqlDbType.VarChar, 128).Value = tableName;
            cmd.Parameters.Add("@ColumnName", SqlDbType.VarChar, 128).Value = columnName;
            string type = (Convert.ToString(cmd.ExecuteScalar()) ?? string.Empty).ToLowerInvariant();
            return type is "int" or "bigint" or "smallint" or "tinyint";
        }

        private static void UpdateSoLuongTon(SqlConnection conn, SqlTransaction tran, object maNlValue, decimal soLuong, decimal donGia)
        {
            using SqlCommand cmd = new SqlCommand("UPDATE dbo.NGUYEN_LIEU SET SoLuongTon = ISNULL(SoLuongTon,0) + @SoLuong, GiaNhap = @DonGia WHERE MaNL = @MaNL", conn, tran);
            cmd.Parameters.Add("@MaNL", SqlDbType.Int).Value = Convert.ToInt32(maNlValue);
            cmd.Parameters.Add("@SoLuong", SqlDbType.Decimal).Value = soLuong;
            cmd.Parameters.Add("@DonGia", SqlDbType.Decimal).Value = donGia;
            cmd.Parameters["@SoLuong"].Precision = 18;
            cmd.Parameters["@SoLuong"].Scale = 2;
            cmd.Parameters["@DonGia"].Precision = 18;
            cmd.Parameters["@DonGia"].Scale = 2;
            cmd.ExecuteNonQuery();
        }

        private void btn_QLKH_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
