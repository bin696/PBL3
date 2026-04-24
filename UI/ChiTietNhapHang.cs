using PBL3.DataBase;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.ComponentModel;
using System.Globalization;
using System.Net.Http;
using System.Text;

namespace PBL3
{
    public partial class ChiTietNhapHang : Form
    {
        private readonly string _maNcc;
        private readonly string _tenNcc;
        private readonly string _nguoiNhap;
        private readonly string _maNv;

        private readonly DataTable _gioNhapTable = new DataTable();
        private string _printContent = string.Empty;
        private string _printMaHdn = string.Empty;
        private string _printTenNcc = string.Empty;
        private DateTime _printNgayNhap;
        private decimal _printTongTien;
        private Image? _printQrImage;

        public ChiTietNhapHang() : this(string.Empty, "Nhà cung cấp", "Admin", "1")
        {
        }

        public ChiTietNhapHang(string maNcc, string tenNcc, string nguoiNhap, string maNv)
        {
            _maNcc = maNcc;
            _tenNcc = tenNcc;
            _nguoiNhap = string.IsNullOrWhiteSpace(nguoiNhap) ? "Admin" : nguoiNhap;
            _maNv = string.IsNullOrWhiteSpace(maNv) ? "1" : maNv;

            InitializeComponent();
            _lblTenNccValue.Text = _tenNcc;
            _lblNguoiNhapValue.Text = _nguoiNhap;

            Load += ChiTietNhapHang_Load;
            _cboNguyenLieu.SelectionChangeCommitted += (_, __) => SyncDonGiaFromSelection();
            _btnThem.Click += BtnThem_Click;
            _btnXoaDong.Click += BtnXoaDong_Click;
            _btnXacNhan.Click += BtnXacNhan_Click;
            _btnHuy.Click += BtnHuy_Click;
        }

        private void ChiTietNhapHang_Load(object? sender, EventArgs e)
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
            {
                _lblNgayNhap.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
                _lblTongTien.Text = "Tổng tiền: 0 VNĐ";
                return;
            }

            _lblNgayNhap.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
            InitGioNhapGrid();
            LoadNguyenLieu();
            UpdateTongTien();
        }

        private void InitGioNhapGrid()
        {
            _gioNhapTable.Columns.Add("MaNL", typeof(string));
            _gioNhapTable.Columns.Add("TenMon", typeof(string));
            _gioNhapTable.Columns.Add("SoLuong", typeof(decimal));
            _gioNhapTable.Columns.Add("DonGia", typeof(decimal));
            _gioNhapTable.Columns.Add("ThanhTien", typeof(decimal), "SoLuong * DonGia");

            _dgvGioHang.AutoGenerateColumns = false;
            _dgvGioHang.Columns.Clear();
            _dgvGioHang.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TenMon", HeaderText = "Tên món", FillWeight = 40 });
            _dgvGioHang.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "SoLuong", HeaderText = "Số lượng", FillWeight = 18 });
            _dgvGioHang.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "DonGia",
                HeaderText = "Đơn giá",
                FillWeight = 21,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "#,##0", Alignment = DataGridViewContentAlignment.MiddleRight }
            });
            _dgvGioHang.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ThanhTien",
                HeaderText = "Thành tiền",
                FillWeight = 21,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "#,##0", Alignment = DataGridViewContentAlignment.MiddleRight }
            });

            _dgvGioHang.DataSource = _gioNhapTable;
        }

        private void LoadNguyenLieu()
        {
            const string sql = @"
SELECT MaNL, TenNL, ISNULL(GiaNhap, 0) AS GiaNhap
FROM dbo.NGUYEN_LIEU
ORDER BY TenNL";

            using SqlConnection conn = DbHelper.GetConnection();
            using SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);

            _cboNguyenLieu.DataSource = dt;
            _cboNguyenLieu.DisplayMember = "TenNL";
            _cboNguyenLieu.ValueMember = "MaNL";

            SyncDonGiaFromSelection();
        }

        private void SyncDonGiaFromSelection()
        {
            if (_cboNguyenLieu.SelectedItem is not DataRowView drv)
            {
                return;
            }

            decimal gia = drv["GiaNhap"] == DBNull.Value ? 0m : Convert.ToDecimal(drv["GiaNhap"], CultureInfo.InvariantCulture);
            _txtDonGia.Text = gia.ToString("0.##", CultureInfo.InvariantCulture);
        }

        private void BtnThem_Click(object? sender, EventArgs e)
        {
            if (_cboNguyenLieu.SelectedValue is null)
            {
                MessageBox.Show("Vui lòng chọn nguyên liệu.", "Thiếu dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(_txtDonGia.Text.Trim(), NumberStyles.Number, CultureInfo.InvariantCulture, out decimal donGia)
                && !decimal.TryParse(_txtDonGia.Text.Trim(), NumberStyles.Number, CultureInfo.CurrentCulture, out donGia))
            {
                MessageBox.Show("Đơn giá không hợp lệ.", "Dữ liệu sai", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (donGia < 0)
            {
                MessageBox.Show("Đơn giá phải >= 0.", "Dữ liệu sai", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maNl = Convert.ToString(_cboNguyenLieu.SelectedValue) ?? string.Empty;
            string tenNl = (_cboNguyenLieu.Text ?? string.Empty).Trim();
            decimal soLuong = _nudSoLuong.Value;

            DataRow? existed = _gioNhapTable.AsEnumerable().FirstOrDefault(r => string.Equals(Convert.ToString(r["MaNL"]), maNl, StringComparison.OrdinalIgnoreCase));
            if (existed is null)
            {
                _gioNhapTable.Rows.Add(maNl, tenNl, soLuong, donGia);
            }
            else
            {
                existed["SoLuong"] = Convert.ToDecimal(existed["SoLuong"], CultureInfo.InvariantCulture) + soLuong;
                existed["DonGia"] = donGia;
            }

            UpdateTongTien();
        }

        private void UpdateTongTien()
        {
            decimal tongTien = _gioNhapTable.AsEnumerable().Sum(r => r.Field<decimal>("ThanhTien"));
            _lblTongTien.Text = $"Tổng tiền: {tongTien:#,##0} VNĐ";
        }

        private void BtnXoaDong_Click(object? sender, EventArgs e)
        {
            if (_dgvGioHang.CurrentRow is null || _dgvGioHang.CurrentRow.IsNewRow)
            {
                MessageBox.Show("Vui lòng chọn dòng cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            _dgvGioHang.Rows.Remove(_dgvGioHang.CurrentRow);
            UpdateTongTien();
        }

        private void BtnHuy_Click(object? sender, EventArgs e)
        {
            Close();
        }

        private void BtnXacNhan_Click(object? sender, EventArgs e)
        {
            if (_gioNhapTable.Rows.Count == 0)
            {
                MessageBox.Show("Danh sách nhập đang trống.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            decimal tongTien = _gioNhapTable.AsEnumerable().Sum(r => r.Field<decimal>("ThanhTien"));

            try
            {
                using SqlConnection conn = DbHelper.GetConnection();
                conn.Open();
                using SqlTransaction tran = conn.BeginTransaction();

                object maHdn = InsertHoaDonNhap(conn, tran, _maNcc, tongTien);

                foreach (DataRow row in _gioNhapTable.Rows)
                {
                    object maNl = row["MaNL"];
                    decimal soLuong = Convert.ToDecimal(row["SoLuong"], CultureInfo.InvariantCulture);
                    decimal donGia = Convert.ToDecimal(row["DonGia"], CultureInfo.InvariantCulture);

                    InsertChiTietNhap(conn, tran, maHdn, maNl, soLuong, donGia);
                    UpdateSoLuongTon(conn, tran, maNl, soLuong, donGia);
                }

                tran.Commit();

                BuildPrintContent(Convert.ToString(maHdn) ?? string.Empty, tongTien);
                ShowPrintPreview();

                MessageBox.Show("Nhập kho thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _gioNhapTable.Clear();
                UpdateTongTien();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Nhập kho thất bại.\n{ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private static int GenerateNextMaHdnInt(SqlConnection conn, SqlTransaction tran)
        {
            using SqlCommand cmd = new SqlCommand("SELECT ISNULL(MAX(TRY_CAST(MaHDN AS INT)), 0) + 1 FROM dbo.HOA_DON_NHAP", conn, tran);
            return Convert.ToInt32(cmd.ExecuteScalar(), CultureInfo.InvariantCulture);
        }

        private object InsertHoaDonNhap(SqlConnection conn, SqlTransaction tran, string maNcc, decimal tongTien)
        {
            bool maHdnIdentity;
            using (SqlCommand cmdIdentity = new SqlCommand("SELECT CASE WHEN COLUMNPROPERTY(OBJECT_ID('dbo.HOA_DON_NHAP'),'MaHDN','IsIdentity') = 1 THEN 1 ELSE 0 END", conn, tran))
            {
                maHdnIdentity = Convert.ToInt32(cmdIdentity.ExecuteScalar() ?? 0, CultureInfo.InvariantCulture) == 1;
            }

            bool maHdnNumeric = IsColumnNumeric(conn, tran, "HOA_DON_NHAP", "MaHDN");
            bool maNvNumeric = IsColumnNumeric(conn, tran, "HOA_DON_NHAP", "MaNV");
            bool maNccNumeric = IsColumnNumeric(conn, tran, "HOA_DON_NHAP", "MaNCC");

            object maNvValue = maNvNumeric && int.TryParse(_maNv, out int nvInt) ? nvInt : _maNv;
            object maNccValue = maNccNumeric && int.TryParse(maNcc, out int nccInt) ? nccInt : maNcc;

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
            cmd.Parameters.Add("@MaNL", SqlDbType.Int).Value = Convert.ToInt32(maNlValue, CultureInfo.InvariantCulture);
            cmd.Parameters.Add("@SoLuong", SqlDbType.Decimal).Value = soLuong;
            cmd.Parameters.Add("@DonGia", SqlDbType.Decimal).Value = donGia;
            cmd.Parameters["@SoLuong"].Precision = 18;
            cmd.Parameters["@SoLuong"].Scale = 2;
            cmd.Parameters["@DonGia"].Precision = 18;
            cmd.Parameters["@DonGia"].Scale = 2;
            cmd.ExecuteNonQuery();
        }

        private static void UpdateSoLuongTon(SqlConnection conn, SqlTransaction tran, object maNlValue, decimal soLuong, decimal donGia)
        {
            using SqlCommand cmd = new SqlCommand("UPDATE dbo.NGUYEN_LIEU SET SoLuongTon = ISNULL(SoLuongTon,0) + @SoLuong, GiaNhap = @DonGia WHERE MaNL = @MaNL", conn, tran);
            cmd.Parameters.Add("@MaNL", SqlDbType.Int).Value = Convert.ToInt32(maNlValue, CultureInfo.InvariantCulture);
            cmd.Parameters.Add("@SoLuong", SqlDbType.Decimal).Value = soLuong;
            cmd.Parameters.Add("@DonGia", SqlDbType.Decimal).Value = donGia;
            cmd.Parameters["@SoLuong"].Precision = 18;
            cmd.Parameters["@SoLuong"].Scale = 2;
            cmd.Parameters["@DonGia"].Precision = 18;
            cmd.Parameters["@DonGia"].Scale = 2;
            cmd.ExecuteNonQuery();
        }

        private void BuildPrintContent(string maHdn, decimal tongTien)
        {
            _printMaHdn = maHdn;
            _printTenNcc = _tenNcc;
            _printNgayNhap = DateTime.Now;
            _printTongTien = tongTien;

            string qrPayload = BuildNhapKhoQrId(_printMaHdn, _printNgayNhap);
            _printQrImage?.Dispose();
            _printQrImage = TryCreateQrImage(qrPayload, 170);
            _printContent = string.Empty;
        }

        private static string BuildNhapKhoQrId(string? maHd, DateTime thoiGian)
        {
            string idPart = string.IsNullOrWhiteSpace(maHd) ? "000" : maHd.Trim();
            return $"PNK-{thoiGian:yyyy-MM-dd}-{idPart}";
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

        private void ShowPrintPreview()
        {
            PrintDocument doc = new PrintDocument();
            doc.DefaultPageSettings.PaperSize = new PaperSize("Receipt", 315, 1200);
            doc.DefaultPageSettings.Margins = new Margins(10, 10, 10, 10);
            doc.PrintPage += (_, e) =>
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
                    SizeF s = e.Graphics.MeasureString(text, font);
                    float x = left + (width - s.Width) / 2f;
                    e.Graphics.DrawString(text, font, Brushes.Black, x, y);
                    y += s.Height + 1f;
                }

                DrawCentered("TỨ ĐẠI THIÊN LONG", brandFont);
                DrawCentered("169 Nguyễn Lương Bằng", normalFont);
                DrawCentered("SĐT: 0374895922", normalFont);

                y += 2f;
                e.Graphics.DrawLine(Pens.Gray, left, y, right, y);
                y += 4f;

                DrawCentered("PHIẾU NHẬP KHO", titleFont);
                e.Graphics.DrawString($"Mã hóa đơn: {_printMaHdn}", normalFont, Brushes.Black, left, y); y += normalFont.GetHeight(e.Graphics) + 1f;
                e.Graphics.DrawString($"Ngày: {_printNgayNhap:dd/MM/yyyy HH:mm}", normalFont, Brushes.Black, left, y); y += normalFont.GetHeight(e.Graphics) + 1f;
                e.Graphics.DrawString($"Nhà cung cấp: {_printTenNcc}", normalFont, Brushes.Black, left, y); y += normalFont.GetHeight(e.Graphics) + 1f;
                e.Graphics.DrawString($"Người nhập: {_nguoiNhap}", normalFont, Brushes.Black, left, y); y += normalFont.GetHeight(e.Graphics) + 3f;

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

                e.Graphics.DrawString("Tên hàng", monoFont, Brushes.Black, nameRect);
                e.Graphics.DrawString("SL", monoFont, Brushes.Black, qtyRect, rightFormat);
                e.Graphics.DrawString("Đơn giá", monoFont, Brushes.Black, priceRect, rightFormat);
                e.Graphics.DrawString("T.Tiền", monoFont, Brushes.Black, totalRect, rightFormat);
                y += 15f;

                e.Graphics.DrawLine(Pens.LightGray, left, y, right, y);
                y += 3f;

                foreach (DataRow row in _gioNhapTable.Rows)
                {
                    string ten = Convert.ToString(row["TenMon"]) ?? string.Empty;
                    string sl = Convert.ToDecimal(row["SoLuong"], CultureInfo.InvariantCulture).ToString("0.##", CultureInfo.InvariantCulture);
                    decimal dg = Convert.ToDecimal(row["DonGia"], CultureInfo.InvariantCulture);
                    decimal tt = Convert.ToDecimal(row["ThanhTien"], CultureInfo.InvariantCulture);

                    nameRect = new RectangleF(left, y, nameW, 14f);
                    qtyRect = new RectangleF(nameRect.Right + colGap, y, qtyW, 14f);
                    priceRect = new RectangleF(qtyRect.Right + colGap, y, priceW, 14f);
                    totalRect = new RectangleF(priceRect.Right, y, totalW, 14f);

                    e.Graphics.DrawString(ten, monoFont, Brushes.Black, nameRect);
                    e.Graphics.DrawString(sl, monoFont, Brushes.Black, qtyRect, rightFormat);
                    e.Graphics.DrawString($"{dg:N0}", monoFont, Brushes.Black, priceRect, rightFormat);
                    e.Graphics.DrawString($"{tt:N0}", monoFont, Brushes.Black, totalRect, rightFormat);
                    y += 14f;
                }

                y += 2f;
                e.Graphics.DrawLine(Pens.Gray, left, y, right, y);
                y += 4f;

                e.Graphics.DrawString($"TỔNG TIỀN: {_printTongTien:N0} VNĐ", totalFont, Brushes.Black, left, y);
                y += totalFont.GetHeight(e.Graphics) + 4f;

                if (_printQrImage is not null)
                {
                    y += 3f;
                    DrawCentered("Mã xác thực nhập kho", normalFont);
                    const float qrSize = 74f;
                    float qrX = left + (width - qrSize) / 2f;
                    e.Graphics.DrawImage(_printQrImage, qrX, y, qrSize, qrSize);
                    y += qrSize + 6f;
                }

                y += 4f;
                float signY = y;
                float half = width / 2f;
                e.Graphics.DrawString("Người nhập kho", normalFont, Brushes.Black, left, signY);
                e.Graphics.DrawString("Người nhận hàng", normalFont, Brushes.Black, left + half, signY);
                signY += normalFont.GetHeight(e.Graphics) + 16f;
                e.Graphics.DrawLine(Pens.Black, left, signY, left + half - 12f, signY);
                e.Graphics.DrawLine(Pens.Black, left + half, signY, right, signY);
            };

            using PrintPreviewDialog preview = new PrintPreviewDialog
            {
                Document = doc,
                Width = 900,
                Height = 700
            };

            preview.ShowDialog(this);
        }

        private void _lblNguoiNhapValue_Click(object sender, EventArgs e)
        {

        }
    }
}
