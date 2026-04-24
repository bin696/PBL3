using PBL3.DataBase;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Text.RegularExpressions;

namespace PBL3
{
    public partial class QuanLiNguyenLieu : Form
    {
        private DataTable? _nguyenLieuTable;
        private bool _onlyLowStock;
        private string? _selectedMaNlDbValue;
        private Label? _lblTongGiaTriTonKho;

        public QuanLiNguyenLieu()
        {
            InitializeComponent();
            _cboTimTheo.SelectionChangeCommitted += SearchControl_Changed;
            _cboDonViTinh.Items.AddRange(new object[] { "Kg", "Lít", "Túi", "Thùng", "Cái" });
            if (_cboDonViTinh.Items.Count > 0)
            {
                _cboDonViTinh.SelectedIndex = 0;
            }

            txtSoLuongTon.ReadOnly = true;
            txtSoLuongTon.BackColor = Color.Gainsboro;
            InitializeTongGiaTriTonKhoLabel();
        }

        private void QuanLiNguyenLieu_Load(object? sender, EventArgs e)
        {
            NormalizeDonViTinhValues();
            SeedSampleNguyenLieuIfEmpty();
            LoadNguyenLieu();
            if (_cboTimTheo.Items.Count > 0 && _cboTimTheo.SelectedIndex < 0)
            {
                _cboTimTheo.SelectedIndex = 0;
            }
            ClearForm();
        }

        private void LoadNguyenLieu()
        {
            const string sql = @"
SELECT MaNL, TenNL, DonViTinh, GiaNhap, SoLuongTon, NguongToiThieu
FROM dbo.NGUYEN_LIEU
ORDER BY TRY_CAST(MaNL AS INT), MaNL";

            using SqlConnection conn = DbHelper.GetConnection();
            using SqlDataAdapter da = new SqlDataAdapter(sql, conn);

            DataTable dt = new DataTable();
            da.Fill(dt);
            _nguyenLieuTable = dt;

            dgvNguyenLieu.DataSource = _nguyenLieuTable;
            dgvNguyenLieu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgvNguyenLieu.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
            dgvNguyenLieu.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            SetHeaderText("MaNL", "Mã NL");
            SetHeaderText("TenNL", "Tên nguyên liệu");
            SetHeaderText("DonViTinh", "Đơn vị tính");
            SetHeaderText("GiaNhap", "Giá nhập");
            SetHeaderText("NguongToiThieu", "Ngưỡng tối thiểu");
            SetHeaderText("SoLuongTon", "Số lượng tồn");


            ConfigureNguyenLieuGridColumns();

            ApplySearchFilter();
            ApplyLowStockFilterAndColor();
            UpdateTongGiaTriTonKhoLabel();
        }

        private void SearchControl_Changed(object? sender, EventArgs e)
        {
            ApplySearchFilter();
        }

        private void ApplySearchFilter()
        {
            if (_nguyenLieuTable is null)
            {
                return;
            }

            string keyword = _txtTimKiem.Text.Trim().Replace("'", "''");
            if (string.IsNullOrWhiteSpace(keyword))
            {
                _nguyenLieuTable.DefaultView.RowFilter = string.Empty;
                return;
            }

            string selected = (Convert.ToString(_cboTimTheo.SelectedItem) ?? "MãNL").Trim();
            string filter = selected switch
            {
                "TênNL" => $"TenNL LIKE '%{keyword}%'",
                "ĐơnVịTính" => $"DonViTinh LIKE '%{keyword}%'",
                "GiáNhập" => $"Convert(GiaNhap, 'System.String') LIKE '%{keyword}%'",
                "SốLượngTồn" => $"Convert(SoLuongTon, 'System.String') LIKE '%{keyword}%'",
                _ => $"Convert(MaNL, 'System.String') LIKE '%{keyword}%'"
            };

            _nguyenLieuTable.DefaultView.RowFilter = filter;
        }

        private void SetHeaderText(string columnName, string text)
        {
            DataGridViewColumn? c = dgvNguyenLieu.Columns[columnName];
            if (c != null)
            {
                c.HeaderText = text;
            }
        }

        private void ConfigureNguyenLieuGridColumns()
        {
            dgvNguyenLieu.DefaultCellStyle.WrapMode = DataGridViewTriState.False;

            DataGridViewColumn? maNlColumn = dgvNguyenLieu.Columns["MaNL"];
            if (maNlColumn is not null)
            {
                maNlColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                maNlColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            DataGridViewColumn? soLuongTonColumn = dgvNguyenLieu.Columns["SoLuongTon"];
            if (soLuongTonColumn is not null)
            {
                soLuongTonColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                soLuongTonColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            DataGridViewColumn? giaNhapColumn = dgvNguyenLieu.Columns["GiaNhap"];
            if (giaNhapColumn is not null)
            {
                giaNhapColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                giaNhapColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                giaNhapColumn.DefaultCellStyle.Format = "N0";
            }
            DataGridViewColumn? nguongCol = dgvNguyenLieu.Columns["NguongToiThieu"];
            if (nguongCol is not null)
            {
                nguongCol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                nguongCol.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        private void dgvNguyenLieu_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            DataGridViewRow row = dgvNguyenLieu.Rows[e.RowIndex];
            _selectedMaNlDbValue = Convert.ToString(row.Cells["MaNL"].Value) ?? string.Empty;
            txtMaNL.Text = FormatMaNlForDisplay(_selectedMaNlDbValue);
            txtTenNL.Text = Convert.ToString(row.Cells["TenNL"].Value) ?? string.Empty;
            _cboDonViTinh.SelectedItem = NormalizeDonViTinh(Convert.ToString(row.Cells["DonViTinh"].Value));
            decimal giaNhap = Convert.ToDecimal(row.Cells["GiaNhap"].Value ?? 0m, CultureInfo.InvariantCulture);
            txtGiaNhap.Text = giaNhap.ToString("N0", CultureInfo.CurrentCulture);
            txtSoLuongTon.Text = Convert.ToString(row.Cells["SoLuongTon"].Value) ?? "0";
        }

        private void btnThem_Click(object? sender, EventArgs e)
        {
            if (!ValidateInput(isInsert: true))
            {
                return;
            }
            // Generate next id and try to insert explicitly so gaps are reused.
            string nextMaDisplay = GenerateNextMaNL();
            string numericPart = nextMaDisplay.StartsWith("NL", StringComparison.OrdinalIgnoreCase) ? nextMaDisplay.Substring(2) : nextMaDisplay;
            int nextInt = int.TryParse(numericPart, out int tmp) ? tmp : 0;

            try
            {
                using SqlConnection conn = DbHelper.GetConnection();
                conn.Open();

                bool hasIdentity = false;
                using (SqlCommand cmdCheck = new SqlCommand("SELECT CASE WHEN COLUMNPROPERTY(OBJECT_ID('dbo.NGUYEN_LIEU'),'MaNL','IsIdentity') = 1 THEN 1 ELSE 0 END", conn))
                {
                    hasIdentity = Convert.ToInt32(cmdCheck.ExecuteScalar() ?? 0) == 1;
                }

                using SqlTransaction tran = conn.BeginTransaction();
                try
                {
                    if (hasIdentity)
                    {
                        using SqlCommand cmd = new SqlCommand("SET IDENTITY_INSERT dbo.NGUYEN_LIEU ON; INSERT INTO dbo.NGUYEN_LIEU (MaNL, TenNL, DonViTinh, GiaNhap, SoLuongTon) VALUES (@MaNL, @TenNL, @DonViTinh, @GiaNhap, 0); SET IDENTITY_INSERT dbo.NGUYEN_LIEU OFF;", conn, tran);
                        cmd.Parameters.Add("@MaNL", SqlDbType.Int).Value = nextInt;
                        cmd.Parameters.Add("@TenNL", SqlDbType.NVarChar, 100).Value = txtTenNL.Text.Trim();
                        cmd.Parameters.Add("@DonViTinh", SqlDbType.NVarChar, 20).Value = NormalizeDonViTinh(Convert.ToString(_cboDonViTinh.SelectedItem));
                        cmd.Parameters.Add("@GiaNhap", SqlDbType.Decimal).Value = ParseGiaNhapOrThrow();
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        using SqlCommand cmd = new SqlCommand("INSERT INTO dbo.NGUYEN_LIEU (MaNL, TenNL, DonViTinh, GiaNhap, SoLuongTon) VALUES (@MaNL, @TenNL, @DonViTinh, @GiaNhap, 0)", conn, tran);
                        cmd.Parameters.Add("@MaNL", SqlDbType.VarChar, 20).Value = nextMaDisplay;
                        cmd.Parameters.Add("@TenNL", SqlDbType.NVarChar, 100).Value = txtTenNL.Text.Trim();
                        cmd.Parameters.Add("@DonViTinh", SqlDbType.NVarChar, 20).Value = NormalizeDonViTinh(Convert.ToString(_cboDonViTinh.SelectedItem));
                        cmd.Parameters.Add("@GiaNhap", SqlDbType.Decimal).Value = ParseGiaNhapOrThrow();
                        cmd.ExecuteNonQuery();
                    }

                    tran.Commit();
                }
                catch
                {
                    tran.Rollback();
                    throw;
                }

                MessageBox.Show("Thêm nguyên liệu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadNguyenLieu();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm nguyên liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object? sender, EventArgs e)
        {
            if (!ValidateInput(isInsert: false))
            {
                return;
            }

            const string sql = @"
UPDATE dbo.NGUYEN_LIEU
SET TenNL = @TenNL,
    DonViTinh = @DonViTinh,
    GiaNhap = @GiaNhap,
NguongToiThieu = @NguongToiThieu
WHERE MaNL = @MaNL";

            try
            {
                using SqlConnection conn = DbHelper.GetConnection();
                using SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add("@MaNL", SqlDbType.VarChar, 20).Value = _selectedMaNlDbValue ?? txtMaNL.Text.Trim();
                cmd.Parameters.Add("@TenNL", SqlDbType.NVarChar, 100).Value = txtTenNL.Text.Trim();
                cmd.Parameters.Add("@DonViTinh", SqlDbType.NVarChar, 20).Value = NormalizeDonViTinh(Convert.ToString(_cboDonViTinh.SelectedItem));
                cmd.Parameters.Add("@GiaNhap", SqlDbType.Decimal).Value = ParseGiaNhapOrThrow();
                cmd.Parameters.Add("@NguongToiThieu", SqlDbType.Decimal).Value = _numNguongToiThieu.Value;
                conn.Open();
                int rows = cmd.ExecuteNonQuery();

                if (rows == 0)
                {
                    MessageBox.Show("Không tìm thấy nguyên liệu để cập nhật.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                MessageBox.Show("Sửa nguyên liệu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadNguyenLieu();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi sửa nguyên liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaNL.Text))
            {
                MessageBox.Show("Vui lòng chọn nguyên liệu.", "Thiếu dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DialogResult result = MessageBox.Show(
                "Bạn có chắc chắn muốn xóa nguyên liệu này?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
                return;

            const string sql = "DELETE FROM dbo.NGUYEN_LIEU WHERE MaNL = @MaNL";
            try
            {
                using SqlConnection conn = DbHelper.GetConnection();
                using SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add("@MaNL", SqlDbType.VarChar, 20).Value = _selectedMaNlDbValue ?? txtMaNL.Text.Trim();
                conn.Open();
                int rows = cmd.ExecuteNonQuery();

                if (rows == 0)
                {
                    MessageBox.Show("Không tìm thấy nguyên liệu để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                MessageBox.Show("Xóa nguyên liệu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadNguyenLieu();
                ClearForm();
            }
            catch (SqlException ex) when (ex.Number == 547)
            {
                MessageBox.Show("Không thể xóa nguyên liệu vì đang được sử dụng ở dữ liệu liên quan.", "Không thể xóa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa nguyên liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLamMoi_Click(object? sender, EventArgs e)
        {
            _onlyLowStock = false;
            lblBtnLocSapHet.Text = "Sắp hết hàng";
            LoadNguyenLieu();
            ClearForm();
        }

        private void btnLocSapHet_Click(object? sender, EventArgs e)
        {
            _onlyLowStock = !_onlyLowStock;
            lblBtnLocSapHet.Text = _onlyLowStock ? "Hiện tất cả" : "Sắp hết hàng";
            ApplyLowStockFilterAndColor();
        }

        private void numNguongToiThieu_ValueChanged(object? sender, EventArgs e)
        {
            ApplyLowStockFilterAndColor();
        }

        private void ApplyLowStockFilterAndColor()
        {
            if (_nguyenLieuTable is null)
            {
                return;
            }

            string keyword = _txtTimKiem.Text.Trim().Replace("'", "''");
            string selected = (Convert.ToString(_cboTimTheo.SelectedItem) ?? "MãNL").Trim();
            string searchFilter = string.Empty;
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                searchFilter = selected switch
                {
                    "TênNL" => $"TenNL LIKE '%{keyword}%'",
                    "ĐơnVịTính" => $"DonViTinh LIKE '%{keyword}%'",
                    "GiáNhập" => $"Convert(GiaNhap, 'System.String') LIKE '%{keyword}%'",
                    "SốLượngTồn" => $"Convert(SoLuongTon, 'System.String') LIKE '%{keyword}%'",
                    _ => $"Convert(MaNL, 'System.String') LIKE '%{keyword}%'"
                };
            }

            string lowFilter = "Convert(SoLuongTon, 'System.Decimal') <= Convert(NguongToiThieu, 'System.Decimal')";

            if (_onlyLowStock)
            {
                _nguyenLieuTable.DefaultView.RowFilter = string.IsNullOrEmpty(searchFilter)
                    ? lowFilter
                    : $"({searchFilter}) AND ({lowFilter})";
            }
            else
            {
                _nguyenLieuTable.DefaultView.RowFilter = searchFilter;
            }

            foreach (DataGridViewRow row in dgvNguyenLieu.Rows)
            {
                if (row.IsNewRow)
                {
                    continue;
                }

                decimal ton = 0m;
                decimal nguongTheoDong = 0m;

                object valTon = row.Cells["SoLuongTon"].Value;
                if (valTon != null && valTon != DBNull.Value)
                {
                    decimal.TryParse(Convert.ToString(valTon), NumberStyles.Number, CultureInfo.InvariantCulture, out ton);
                }

                object valNguong = row.Cells["NguongToiThieu"].Value;
                if (valNguong != null && valNguong != DBNull.Value)
                {
                    decimal.TryParse(Convert.ToString(valNguong), NumberStyles.Number, CultureInfo.InvariantCulture, out nguongTheoDong);
                }

                if (ton <= nguongTheoDong)
                {
                    row.DefaultCellStyle.BackColor = Color.LightYellow;
                    row.DefaultCellStyle.ForeColor = Color.DarkRed;
                }
                else
                {
                    row.DefaultCellStyle.BackColor = Color.White;
                    row.DefaultCellStyle.ForeColor = Color.Black;
                }
            }

            UpdateTongGiaTriTonKhoLabel();
        }

        private void btnNhapHangNhanh_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaNL.Text))
            {
                MessageBox.Show("Vui lòng chọn nguyên liệu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using NhapHang popup = new NhapHang(_selectedMaNlDbValue ?? txtMaNL.Text.Trim(), "Admin", "1");
            popup.ShowDialog(this);
            LoadNguyenLieu();
            ClearForm();
        }

        private bool ValidateInput(bool isInsert)
        {
            if (!isInsert && string.IsNullOrWhiteSpace(txtMaNL.Text))
            {
                MessageBox.Show("Vui lòng chọn nguyên liệu.", "Thiếu dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtTenNL.Text))
            {
                MessageBox.Show("Vui lòng nhập tên nguyên liệu.", "Thiếu dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtGiaNhap.Text))
            {
                MessageBox.Show("Vui lòng nhập giá nhập.", "Thiếu dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!TryParseGiaNhap(txtGiaNhap.Text.Trim(), out decimal giaNhap) || giaNhap <= 0)
            {
                MessageBox.Show("Giá nhập không hợp lệ.", "Dữ liệu sai", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void ClearForm()
        {
            _selectedMaNlDbValue = null;
            txtMaNL.Text = GenerateNextMaNL();
            txtTenNL.Clear();
            txtGiaNhap.Clear();
            txtSoLuongTon.Text = "0";
            if (_cboDonViTinh.Items.Count > 0)
            {
                _cboDonViTinh.SelectedIndex = 0;
            }
        }

        private void InitializeTongGiaTriTonKhoLabel()
        {
            if (_lblTongGiaTriTonKho is not null)
            {
                return;
            }

            _lblTongGiaTriTonKho = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(110, 92, 75),
                BackColor = Color.Transparent,
                Anchor = AnchorStyles.Left | AnchorStyles.Bottom,
                Text = "Tổng giá trị tồn kho hiện tại: 0 đ"
            };

            pnlDanhSachNguyenLieu.Controls.Add(_lblTongGiaTriTonKho);
            PositionTongGiaTriTonKhoLabel();
            pnlDanhSachNguyenLieu.Resize += (_, __) => PositionTongGiaTriTonKhoLabel();
            _lblTongGiaTriTonKho.BringToFront();
        }

        private void PositionTongGiaTriTonKhoLabel()
        {
            if (_lblTongGiaTriTonKho is null)
            {
                return;
            }

            int x = dgvNguyenLieu.Left;
            int y = Math.Min(pnlDanhSachNguyenLieu.Height - (_lblTongGiaTriTonKho.Height + 8), dgvNguyenLieu.Bottom + 8);
            _lblTongGiaTriTonKho.Location = new Point(x, y);
        }

        private void UpdateTongGiaTriTonKhoLabel()
        {
            if (_lblTongGiaTriTonKho is null || _nguyenLieuTable is null)
            {
                return;
            }

            decimal tongGiaTriTon = _nguyenLieuTable.AsEnumerable()
                .Sum(r =>
                {
                    decimal giaNhap = r["GiaNhap"] == DBNull.Value ? 0m : Convert.ToDecimal(r["GiaNhap"], CultureInfo.InvariantCulture);
                    decimal soLuongTon = r["SoLuongTon"] == DBNull.Value ? 0m : Convert.ToDecimal(r["SoLuongTon"], CultureInfo.InvariantCulture);
                    return giaNhap * soLuongTon;
                });

            _lblTongGiaTriTonKho.Text = $"Tổng giá trị tồn kho hiện tại: {tongGiaTriTon:N0} đ";
        }

        private static bool TryParseGiaNhap(string input, out decimal value)
        {
            if (decimal.TryParse(input, NumberStyles.Number, CultureInfo.CurrentCulture, out value))
            {
                return true;
            }

            if (decimal.TryParse(input, NumberStyles.Number, CultureInfo.InvariantCulture, out value))
            {
                return true;
            }

            string stripped = input.Replace(" ", string.Empty).Replace(",", string.Empty).Replace(".", string.Empty);
            return decimal.TryParse(stripped, NumberStyles.Number, CultureInfo.InvariantCulture, out value);
        }

        private decimal ParseGiaNhapOrThrow()
        {
            if (!TryParseGiaNhap(txtGiaNhap.Text.Trim(), out decimal giaNhap))
            {
                throw new InvalidOperationException("Giá nhập không hợp lệ.");
            }

            return giaNhap;
        }

        private static string NormalizeDonViTinh(string? donViTinh)
        {
            string raw = (donViTinh ?? string.Empty).Trim();
            if (string.IsNullOrWhiteSpace(raw))
            {
                return "Kg";
            }

            string normalized = raw.ToLowerInvariant();
            return normalized switch
            {
                "kg" or "kí" or "ki" or "kilo" => "Kg",
                "lit" or "lít" or "l" => "Lít",
                "tui" or "túi" => "Túi",
                "thung" or "thùng" => "Thùng",
                "cai" or "cái" => "Cái",
                _ => char.ToUpper(raw[0]) + raw[1..]
            };
        }

        private void NormalizeDonViTinhValues()
        {
            using SqlConnection conn = DbHelper.GetConnection();
            conn.Open();

            using SqlCommand selectCmd = new SqlCommand("SELECT MaNL, DonViTinh FROM dbo.NGUYEN_LIEU", conn);
            using SqlDataReader reader = selectCmd.ExecuteReader();
            List<(string MaNl, string DonViTinh)> updates = new List<(string MaNl, string DonViTinh)>();

            while (reader.Read())
            {
                string maNl = Convert.ToString(reader["MaNL"]) ?? string.Empty;
                string current = Convert.ToString(reader["DonViTinh"]) ?? string.Empty;
                string normalized = NormalizeDonViTinh(current);
                if (!string.Equals(current, normalized, StringComparison.Ordinal))
                {
                    updates.Add((maNl, normalized));
                }
            }

            reader.Close();

            foreach ((string maNl, string donViTinh) in updates)
            {
                using SqlCommand updateCmd = new SqlCommand("UPDATE dbo.NGUYEN_LIEU SET DonViTinh = @DonViTinh WHERE MaNL = @MaNL", conn);
                updateCmd.Parameters.Add("@DonViTinh", SqlDbType.NVarChar, 20).Value = donViTinh;
                updateCmd.Parameters.Add("@MaNL", SqlDbType.VarChar, 20).Value = maNl;
                updateCmd.ExecuteNonQuery();
            }
        }

        private void SeedSampleNguyenLieuIfEmpty()
        {
            using SqlConnection conn = DbHelper.GetConnection();
            conn.Open();

            using SqlCommand countCmd = new SqlCommand("SELECT COUNT(1) FROM dbo.NGUYEN_LIEU", conn);
            int currentCount = Convert.ToInt32(countCmd.ExecuteScalar(), CultureInfo.InvariantCulture);
            if (currentCount > 0)
            {
                return;
            }

            const string sql = @"
INSERT INTO dbo.NGUYEN_LIEU (TenNL, DonViTinh, GiaNhap, SoLuongTon, NguongToiThieu) VALUES
(N'Thịt gà',        N'Kg',   90000,  35, 20),
(N'Thịt bò',        N'Kg',  180000,  18, 15),
(N'Tôm',            N'Kg',  220000,  10, 12),
(N'Khoai tây',      N'Kg',   28000,  40, 25),
(N'Xà lách',        N'Kg',   30000,  12, 10),
(N'Cà chua',        N'Kg',   25000,  14, 10),
(N'Hành tây',       N'Kg',   22000,   9, 10),
(N'Bột chiên giòn', N'Kg',   45000,  22, 15),
(N'Dầu ăn',         N'Lít',  52000,  16, 12),
(N'Nước mắm',       N'Lít',  38000,   8, 10),
(N'Sốt mayonnaise', N'Lít',  70000,   6,  8),
(N'Tương ớt',       N'Lít',  55000,   7,  8),
(N'Phô mai lát',    N'Túi', 120000,   5,  6),
(N'Ly giấy',        N'Thùng',95000,   9,  6),
(N'Hộp giấy',       N'Thùng',85000,   4,  6);";

            using SqlCommand insertCmd = new SqlCommand(sql, conn);
            insertCmd.ExecuteNonQuery();
        }

        private string GenerateNextMaNL()
        {
            const string sql = @"SELECT MaNL FROM dbo.NGUYEN_LIEU
ORDER BY TRY_CAST(
    CASE WHEN CONVERT(VARCHAR(20), MaNL) LIKE 'NL%' THEN SUBSTRING(CONVERT(VARCHAR(20), MaNL), 3, LEN(CONVERT(VARCHAR(20), MaNL)) - 2) ELSE CONVERT(VARCHAR(20), MaNL) END AS INT)";

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
                string numeric = raw.StartsWith("NL", StringComparison.OrdinalIgnoreCase) ? raw.Substring(2) : raw;
                if (!int.TryParse(numeric, out int v))
                    continue;

                if (v == nextId)
                    nextId++;
                else if (v > nextId)
                    break;
            }

            return $"NL{nextId}";
        }

        private static string FormatMaNlForDisplay(string? maNlValue)
        {
            if (string.IsNullOrWhiteSpace(maNlValue))
            {
                return string.Empty;
            }

            string value = maNlValue.Trim();
            return value.StartsWith("NL", StringComparison.OrdinalIgnoreCase) ? value.ToUpperInvariant() : $"NL{value}";
        }

        private void btn_QLNV_Click(object? sender, EventArgs e)
        {
            AdminNavigationManager.Navigate<QuanLiNhanVien>(this);
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

        private void btn_DangXuat_Click(object? sender, EventArgs e)
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

        private void btn_DangXuat_MouseEnter(object? sender, EventArgs e)
        {
            btn_DangXuat.BackColor = Color.FromArgb(255, 69, 0);
        }

        private void btn_DangXuat_MouseLeave(object? sender, EventArgs e)
        {
            btn_DangXuat.BackColor = Color.LightSalmon;
        }

        private void roundedPanel1_Paint(object? sender, PaintEventArgs e)
        {
        }

        private void btn_ThongKe_Paint(object? sender, PaintEventArgs e)
        {
        }

        private void btn_ThongKe_Click(object? sender, EventArgs e)
        {
            AdminNavigationManager.Navigate<ThongKe>(this);
        }

        private void btn_QLHDB_Click(object? sender, EventArgs e)
        {
            AdminNavigationManager.Navigate<LichSuHoaDon>(this);
        }

        private void pictureBox1_Click(object? sender, EventArgs e)
        {
        }

        private void btn_QLHDN_Click(object? sender, EventArgs e)
        {
        }

        private void label1_Click(object? sender, EventArgs e)
        {
        }
    }
}
