using PBL3.DataBase;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace PBL3.UI
{
    public partial class TrangHoaDon : Form
    {
        private readonly string _loggedInMaNV;
        private string _loaiHoaDon = "ban";
        private string? _selectedMaHD;
        private DataTable? _masterTable;
        private string _printContent = string.Empty;
        private Image? _printQrImage;
        private string _printMaHd = string.Empty;
        private string _printNhanVien = string.Empty;
        private string _printDoiTac = string.Empty;
        private DateTime _printThoiGian;
        private decimal _printTongTien;

        public TrangHoaDon() : this("1") { }

        public TrangHoaDon(string maNV)
        {
            _loggedInMaNV = maNV;
            InitializeComponent();
        }

        private void TrangHoaDon_Load(object sender, EventArgs e)
        {
            btn_QLNCC.BackColor = Color.Salmon;
            label5.ForeColor = Color.White;

            _dtpNgay.Value = DateTime.Now;
            _dtpNgay.Enabled = false;
            lblBtnHuyHoaDon.Text = "Yêu cầu hủy hóa đơn";

            _btnHoaDonBan.Click += (s, ev) => ChuyenLoaiHoaDon("ban");
            _btnHoaDonNhap.Click += (s, ev) => ChuyenLoaiHoaDon("nhap");
            _txtTimMaHD.TextChanged += (s, ev) => ApplyFilter();
            _dgvHoaDonMaster.SelectionChanged += (s, ev) => LoadSelectedHoaDon();
            _btnHuyHoaDon.Click += _btnHuyHoaDon_Click;
            lblBtnHuyHoaDon.Click += _btnHuyHoaDon_Click;
            _btnLamMoi.Click += _btnLamMoi_Click;
            lblBtnLamMoi.Click += _btnLamMoi_Click;
            _btnInLai.Click += _btnInLai_Click;
            lblBtnInLai.Click += _btnInLai_Click;

            // `_pnlKhachHangView`, `_lblKhachHangView` and `_lblTongCongTatCa` are created in the Designer so they
            // are available in design view. At runtime we only update their Text values.

            ChuyenLoaiHoaDon("ban");
        }

        private void ChuyenLoaiHoaDon(string loai)
        {
            _loaiHoaDon = loai;
            bool isBan = _loaiHoaDon == "ban";

            _btnHoaDonBan.BackColor = isBan ? Color.Coral : Color.BurlyWood;
            _btnHoaDonBan.ForeColor = isBan ? Color.White : Color.Black;
            _btnHoaDonNhap.BackColor = isBan ? Color.BurlyWood : Color.Coral;
            _btnHoaDonNhap.ForeColor = isBan ? Color.Black : Color.White;

            lblTitle.Text = isBan ? "Hóa đơn bán" : "Hóa đơn nhập";
            lblHoTen.Text = isBan ? "Khách hàng" : "Nhà cung cấp";

            LoadHoaDonMaster();
        }

        private void LoadHoaDonMaster()
        {
            string sql = _loaiHoaDon == "ban"
                ? @"SELECT hdb.MaHDB AS MaHD, hdb.NgayBan AS ThoiGian, ISNULL(kh.SDT, N'Khách lẻ') AS DoiTac,
                          ISNULL(kh.SDT, N'') AS SDT, ISNULL(hdb.TongTien,0) AS TongTien, ISNULL(nv.HoTen, N'-') AS NhanVien
                   FROM HOA_DON_BAN hdb
                   LEFT JOIN KHACH_HANG kh ON kh.MaKH = hdb.MaKH
                   LEFT JOIN NHAN_VIEN nv ON nv.MaNV = hdb.MaNV
                   WHERE hdb.MaNV = @MaNV
                   ORDER BY hdb.MaHDB DESC"
                : @"SELECT hdn.MaHDN AS MaHD, hdn.NgayNhap AS ThoiGian, ISNULL(ncc.TenNCC, N'-') AS DoiTac,
                          CAST(N'' AS NVARCHAR(20)) AS SDT, ISNULL(hdn.TongTien,0) AS TongTien, ISNULL(nv.HoTen, N'-') AS NhanVien
                   FROM HOA_DON_NHAP hdn
                   LEFT JOIN NHA_CUNG_CAP ncc ON ncc.MaNCC = hdn.MaNCC
                   LEFT JOIN NHAN_VIEN nv ON nv.MaNV = hdn.MaNV
                   WHERE hdn.MaNV = @MaNV
                   ORDER BY hdn.MaHDN DESC";

            using SqlConnection conn = DbHelper.GetConnection();
            using SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@MaNV", _loggedInMaNV);
            using SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            _masterTable = dt;
            _dgvHoaDonMaster.DataSource = _masterTable;

            if (_dgvHoaDonMaster.Columns.Contains("MaHD")) _dgvHoaDonMaster.Columns["MaHD"].HeaderText = "Mã HD";

            if (_dgvHoaDonMaster.Columns.Contains("ThoiGian"))
            {
                _dgvHoaDonMaster.Columns["ThoiGian"].HeaderText = "Thời gian";
                _dgvHoaDonMaster.Columns["ThoiGian"].DefaultCellStyle.Format = "HH:mm - dd/MM";
            }

            if (_dgvHoaDonMaster.Columns.Contains("TongTien"))
            {
                _dgvHoaDonMaster.Columns["TongTien"].HeaderText = "Tổng tiền";
                _dgvHoaDonMaster.Columns["TongTien"].DefaultCellStyle.Format = "N0";
                _dgvHoaDonMaster.Columns["TongTien"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            if (_dgvHoaDonMaster.Columns.Contains("DoiTac"))
            {
                _dgvHoaDonMaster.Columns["DoiTac"].HeaderText = _loaiHoaDon == "ban" ? "Khách hàng" : "Nhà cung cấp";
            }

            if (_dgvHoaDonMaster.Columns.Contains("NhanVien")) _dgvHoaDonMaster.Columns["NhanVien"].Visible = false;
            if (_dgvHoaDonMaster.Columns.Contains("SDT")) _dgvHoaDonMaster.Columns["SDT"].Visible = false;

            decimal tongTatCa = 0;
            foreach (DataRow row in _masterTable.Rows)
            {
                tongTatCa += Convert.ToDecimal(row["TongTien"] ?? 0);
            }
            if (_lblTongCongTatCa != null)
            {
                _lblTongCongTatCa.Text = $"Tổng cộng: {tongTatCa:N0} đ";
            }

            if (_dgvHoaDonMaster.Rows.Count > 0)
            {
                _dgvHoaDonMaster.Rows[0].Selected = true;
                _dgvHoaDonMaster.CurrentCell = _dgvHoaDonMaster.Rows[0].Cells[0];
                LoadSelectedHoaDon();
            }
            else
            {
                ClearReceipt();
            }
        }

        private void ApplyFilter()
        {
            if (_masterTable is null) return;

            string key = _txtTimMaHD.Text.Trim().Replace("'", "''");
            string filter = "1=1";
            if (!string.IsNullOrWhiteSpace(key))
                filter += $" AND (CONVERT(MaHD, 'System.String') LIKE '%{key}%' OR CONVERT(SDT, 'System.String') LIKE '%{key}%')";

            _masterTable.DefaultView.RowFilter = filter;
        }

        private void LoadSelectedHoaDon()
        {
            if (_dgvHoaDonMaster.CurrentRow is null)
            {
                ClearReceipt();
                return;
            }

            _selectedMaHD = _dgvHoaDonMaster.CurrentRow.Cells["MaHD"].Value?.ToString();
            if (string.IsNullOrWhiteSpace(_selectedMaHD))
            {
                ClearReceipt();
                return;
            }

            txtMaHDB.Text = _selectedMaHD;

            if (_dgvHoaDonMaster.CurrentRow.Cells["ThoiGian"].Value is DateTime dt)
                _dtpNgay.Value = dt;

            lblReceiptNhanVien.Text = $"👤 Nhân viên: {_dgvHoaDonMaster.CurrentRow.Cells["NhanVien"].Value}";
            lblReceiptDoiTac.Text = $"🤝 {(_loaiHoaDon == "ban" ? "Khách hàng" : "Nhà cung cấp")}: {_dgvHoaDonMaster.CurrentRow.Cells["DoiTac"].Value}";
            lblReceiptThanhToan.Text = string.Empty;
            if (_lblKhachHangView != null) _lblKhachHangView.Text = Convert.ToString(_dgvHoaDonMaster.CurrentRow.Cells["DoiTac"].Value) ?? "-";

            LoadHoaDonDetail(_selectedMaHD);
        }

        private static string? DetectFirstColumn(SqlConnection conn, string tableName, params string[] candidates)
        {
            foreach (string candidate in candidates)
            {
                using SqlCommand cmd = new SqlCommand(@"SELECT CASE WHEN EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='dbo' AND TABLE_NAME=@TableName AND COLUMN_NAME=@Col) THEN 1 ELSE 0 END", conn);
                cmd.Parameters.AddWithValue("@TableName", tableName);
                cmd.Parameters.AddWithValue("@Col", candidate);
                object? result = cmd.ExecuteScalar();
                if (result is not null && Convert.ToInt32(result) == 1)
                    return candidate;
            }
            return null;
        }

        private static bool TableColumnExists(SqlConnection conn, string tableName, string columnName)
        {
            using SqlCommand cmd = new SqlCommand(@"
SELECT CASE WHEN EXISTS (
    SELECT 1
    FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_SCHEMA='dbo' AND TABLE_NAME=@TableName AND COLUMN_NAME=@Col
) THEN 1 ELSE 0 END", conn);
            cmd.Parameters.AddWithValue("@TableName", tableName);
            cmd.Parameters.AddWithValue("@Col", columnName);
            object? res = cmd.ExecuteScalar();
            return res is not null && Convert.ToInt32(res) == 1;
        }

        private void LoadHoaDonDetail(string maHd)
        {
            using SqlConnection conn = DbHelper.GetConnection();
            conn.Open();

            string sql;
            if (_loaiHoaDon == "ban")
            {
                string? donGiaCol = DetectFirstColumn(conn, "CT_HOA_DON_BAN", "DonGia", "DonGiaBan", "Gia", "GiaBan", "DonGiaCT", "DonGiaBanLe", "GiaTien");
                string? monGiaCol = DetectFirstColumn(conn, "MON_BAN", "DonGia", "GiaBan", "Gia", "DonGiaBan", "DonGiaBanLe", "GiaTien");
                string? thanhTienCol = DetectFirstColumn(conn, "CT_HOA_DON_BAN", "ThanhTien", "TongTien", "Tien", "GiaTien");
                bool hasCtMaDvpv = TableColumnExists(conn, "CT_HOA_DON_BAN", "MaDVPV");
                string? mdvGiaCol = DetectFirstColumn(conn, "MON_DON_VI_PHUC_VU", "DonGia", "GiaBan", "Gia", "DonGiaBan", "GiaTien");
                string? mdvMaDvpvCol = DetectFirstColumn(conn, "MON_DON_VI_PHUC_VU", "MaDVPV", "MaDV", "MaDonViPhucVu");
                string monGiaExpr = monGiaCol is null ? "CAST(0 AS DECIMAL(18,2))" : $"ISNULL(mb.[{monGiaCol}], 0)";
                string donGiaExpr;
                string mdvJoin = string.Empty;

                if (donGiaCol is not null)
                {
                    donGiaExpr = $"ISNULL(ct.[{donGiaCol}], 0)";
                }
                else if (mdvGiaCol is not null && hasCtMaDvpv && !string.IsNullOrWhiteSpace(mdvMaDvpvCol))
                {
                    mdvJoin = $"\nLEFT JOIN dbo.MON_DON_VI_PHUC_VU mdv ON mdv.MaMon = ct.MaMon AND mdv.[{mdvMaDvpvCol}] = ct.MaDVPV";
                    donGiaExpr = $"ISNULL(mdv.[{mdvGiaCol}], {monGiaExpr})";
                }
                else
                {
                    donGiaExpr = monGiaExpr;
                }

                string thanhTienExpr = thanhTienCol is not null ? $"ISNULL(ct.[{thanhTienCol}], ISNULL(ct.SoLuong,0) * {donGiaExpr})" : $"ISNULL(ct.SoLuong,0) * {donGiaExpr}";

                sql = $@"
SELECT ISNULL(mb.TenMon, CONVERT(NVARCHAR(100), ct.MaMon)) AS TenHang,
       ISNULL(ct.SoLuong, 0) AS SoLuong,
       CASE WHEN {donGiaExpr} = 0 AND ISNULL(ct.SoLuong,0) > 0 THEN ({thanhTienExpr} / NULLIF(ISNULL(ct.SoLuong,0),0)) ELSE {donGiaExpr} END AS DonGia,
       {thanhTienExpr} AS ThanhTien
FROM CT_HOA_DON_BAN ct
LEFT JOIN MON_BAN mb ON mb.MaMon = ct.MaMon
{mdvJoin}
WHERE ct.MaHDB = @MaHD";
            }
            else
            {
                string? donGiaCol = DetectFirstColumn(conn, "CT_HOA_DON_NHAP", "DonGia", "DonGiaNhap", "Gia", "GiaNhap", "GiaTien");
                string? thanhTienCol = DetectFirstColumn(conn, "CT_HOA_DON_NHAP", "ThanhTien", "TongTien", "Tien", "GiaTien");
                string donGiaExpr = donGiaCol is not null ? $"ISNULL(ct.[{donGiaCol}], 0)" : "CAST(0 AS DECIMAL(18,2))";
                string thanhTienExpr = thanhTienCol is not null ? $"ISNULL(ct.[{thanhTienCol}], ISNULL(ct.SoLuong,0) * {donGiaExpr})" : $"ISNULL(ct.SoLuong,0) * {donGiaExpr}";

                sql = $@"
SELECT ISNULL(nl.TenNL, CONVERT(NVARCHAR(100), ct.MaNL)) AS TenHang,
       ISNULL(ct.SoLuong, 0) AS SoLuong,
       CASE WHEN {donGiaExpr} = 0 AND ISNULL(ct.SoLuong,0) > 0 THEN ({thanhTienExpr} / NULLIF(ISNULL(ct.SoLuong,0),0)) ELSE {donGiaExpr} END AS DonGia,
       {thanhTienExpr} AS ThanhTien
FROM CT_HOA_DON_NHAP ct
LEFT JOIN NGUYEN_LIEU nl ON nl.MaNL = ct.MaNL
WHERE ct.MaHDN = @MaHD";
            }

            using SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add("@MaHD", SqlDbType.VarChar, 20).Value = maHd;
            using SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            _dgvHoaDonDetail.DataSource = dt;

            if (_dgvHoaDonDetail.Columns.Contains("TenHang"))
            {
                _dgvHoaDonDetail.Columns["TenHang"].HeaderText = "Tên món";
                _dgvHoaDonDetail.Columns["TenHang"].Width = 180;
            }
            if (_dgvHoaDonDetail.Columns.Contains("SoLuong"))
            {
                _dgvHoaDonDetail.Columns["SoLuong"].HeaderText = "SL";
                _dgvHoaDonDetail.Columns["SoLuong"].Width = 40;
                _dgvHoaDonDetail.Columns["SoLuong"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            if (_dgvHoaDonDetail.Columns.Contains("DonGia"))
            {
                _dgvHoaDonDetail.Columns["DonGia"].HeaderText = "Đơn giá";
                _dgvHoaDonDetail.Columns["DonGia"].Width = 100;
                _dgvHoaDonDetail.Columns["DonGia"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            if (_dgvHoaDonDetail.Columns.Contains("ThanhTien"))
            {
                _dgvHoaDonDetail.Columns["ThanhTien"].HeaderText = "Thành Tiền";
                _dgvHoaDonDetail.Columns["ThanhTien"].Width = 110;
                _dgvHoaDonDetail.Columns["ThanhTien"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            if (_dgvHoaDonDetail.Columns.Contains("DonGia")) _dgvHoaDonDetail.Columns["DonGia"].DefaultCellStyle.Format = "N0";
            if (_dgvHoaDonDetail.Columns.Contains("ThanhTien")) _dgvHoaDonDetail.Columns["ThanhTien"].DefaultCellStyle.Format = "N0";
        }

        private void _btnHuyHoaDon_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_selectedMaHD))
            {
                MessageBox.Show("Vui lòng chọn hóa đơn cần hủy.");
                return;
            }

            if (!TryGetSelectedInvoiceTime(out DateTime createdAt))
            {
                MessageBox.Show("Không đọc được thời gian hóa đơn.");
                return;
            }

            if (DateTime.Now.Subtract(createdAt).TotalMinutes > 10)
            {
                string? pass = PromptForPassword("Hóa đơn quá 10 phút. Nhập mật khẩu Quản lý/Admin để xác nhận.");
                if (string.IsNullOrWhiteSpace(pass) || !ValidateManagerPassword(pass))
                {
                    MessageBox.Show("Không đủ quyền hủy hóa đơn.");
                    return;
                }
            }

            if (MessageBox.Show($"Xác nhận hủy hóa đơn [{_selectedMaHD}]?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            string detailTable = _loaiHoaDon == "ban" ? "CT_HOA_DON_BAN" : "CT_HOA_DON_NHAP";
            string headerTable = _loaiHoaDon == "ban" ? "HOA_DON_BAN" : "HOA_DON_NHAP";
            string key = _loaiHoaDon == "ban" ? "MaHDB" : "MaHDN";

            using SqlConnection conn = DbHelper.GetConnection();
            conn.Open();
            using SqlTransaction tran = conn.BeginTransaction();
            try
            {
                using SqlCommand cmdDetail = new SqlCommand($"DELETE FROM {detailTable} WHERE {key} = @MaHD", conn, tran);
                cmdDetail.Parameters.AddWithValue("@MaHD", _selectedMaHD);
                cmdDetail.ExecuteNonQuery();

                using SqlCommand cmdHeader = new SqlCommand($"DELETE FROM {headerTable} WHERE {key} = @MaHD", conn, tran);
                cmdHeader.Parameters.AddWithValue("@MaHD", _selectedMaHD);
                cmdHeader.ExecuteNonQuery();

                tran.Commit();
            }
            catch
            {
                tran.Rollback();
                throw;
            }

            LoadHoaDonMaster();
            ClearReceipt();
            MessageBox.Show("Đã hủy hóa đơn.");
        }

        private bool TryGetSelectedInvoiceTime(out DateTime invoiceTime)
        {
            invoiceTime = DateTime.MinValue;
            object? value = _dgvHoaDonMaster.CurrentRow?.Cells["ThoiGian"]?.Value;
            if (value is DateTime dt)
            {
                invoiceTime = dt;
                return true;
            }

            return DateTime.TryParse(Convert.ToString(value), out invoiceTime);
        }

        private static string? PromptForPassword(string title)
        {
            using Form prompt = new Form();
            prompt.Width = 360;
            prompt.Height = 170;
            prompt.Text = "Xác nhận quyền";
            prompt.FormBorderStyle = FormBorderStyle.FixedDialog;
            prompt.StartPosition = FormStartPosition.CenterParent;

            Label lbl = new Label { Left = 16, Top = 16, Width = 320, Text = title };
            TextBox txt = new TextBox { Left = 16, Top = 46, Width = 320, PasswordChar = '*' };
            Button ok = new Button { Text = "Xác nhận", Left = 176, Width = 76, Top = 80, DialogResult = DialogResult.OK };
            Button cancel = new Button { Text = "Hủy", Left = 260, Width = 76, Top = 80, DialogResult = DialogResult.Cancel };

            prompt.Controls.Add(lbl);
            prompt.Controls.Add(txt);
            prompt.Controls.Add(ok);
            prompt.Controls.Add(cancel);
            prompt.AcceptButton = ok;
            prompt.CancelButton = cancel;

            return prompt.ShowDialog() == DialogResult.OK ? txt.Text : null;
        }

        private bool ValidateManagerPassword(string password)
        {
            using SqlConnection conn = DbHelper.GetConnection();
            using SqlCommand cmd = new SqlCommand(@"
SELECT COUNT(1)
FROM NHAN_VIEN nv
LEFT JOIN CHUC_VU cv ON nv.MaCV = cv.MaCV
WHERE nv.MatKhau = @MatKhau
  AND (
        ISNULL(cv.TenCV, N'') LIKE N'%Quản%'
        OR ISNULL(cv.TenCV, N'') LIKE N'%Admin%'
      )", conn);
            cmd.Parameters.AddWithValue("@MatKhau", password);

            conn.Open();
            object? result = cmd.ExecuteScalar();
            return result is not null && Convert.ToInt32(result) > 0;
        }

        private void ClearReceipt()
        {
            _selectedMaHD = null;
            txtMaHDB.Clear();
            _dtpNgay.Value = DateTime.Now;
            lblReceiptNhanVien.Text = "👤 Nhân viên: -";
            lblReceiptDoiTac.Text = _loaiHoaDon == "ban" ? "🤝 Khách hàng: -" : "🤝 Nhà cung cấp: -";
            lblReceiptThanhToan.Text = string.Empty;
            _dgvHoaDonDetail.DataSource = null;
            if (_lblKhachHangView != null) _lblKhachHangView.Text = "-";
        }

        private void _btnLamMoi_Click(object? sender, EventArgs e)
        {
            _txtTimMaHD.Clear();
            LoadHoaDonMaster();
            ClearReceipt();
        }

        private void _btnInLai_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_selectedMaHD))
            {
                MessageBox.Show("Vui lòng chọn hóa đơn để in lại.");
                return;
            }

            if (_dgvHoaDonMaster.CurrentRow is null)
            {
                MessageBox.Show("Vui lòng chọn hóa đơn để in lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow row = _dgvHoaDonMaster.CurrentRow;
            decimal tongTien = Convert.ToDecimal(row.Cells["TongTien"].Value ?? 0m, CultureInfo.InvariantCulture);
            DateTime thoiGian = Convert.ToDateTime(row.Cells["ThoiGian"].Value, CultureInfo.InvariantCulture);
            _printMaHd = Convert.ToString(row.Cells["MaHD"].Value) ?? string.Empty;
            _printNhanVien = Convert.ToString(row.Cells["NhanVien"].Value) ?? "-";
            _printDoiTac = Convert.ToString(row.Cells["DoiTac"].Value) ?? "-";
            _printThoiGian = thoiGian;
            _printTongTien = tongTien;

            string qrPayload;
            if (_loaiHoaDon == "nhap")
            {
                qrPayload = BuildNhapKhoQrId(_printMaHd, _printThoiGian);
            }
            else
            {
                string vnPayCode = BuildVnPayCode(Convert.ToString(row.Cells["MaHD"].Value), tongTien, thoiGian);
                qrPayload = BuildVnPayQrPayload(vnPayCode, tongTien, thoiGian);
            }

            _printQrImage?.Dispose();
            _printQrImage = TryCreateQrImage(qrPayload, 170);

            _printContent = string.Empty;

            PrintDocument doc = new PrintDocument();
            // Configure a narrow receipt-sized page (width in hundredths of an inch)
            // ~80mm receipt width ≈ 3.15in -> 315 hundredths
            doc.DefaultPageSettings.PaperSize = new PaperSize("Receipt", 315, 1200);
            doc.DefaultPageSettings.Margins = new Margins(10, 10, 10, 10);
            doc.PrintPage += Doc_PrintPage;

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

        private void Doc_PrintPage(object? sender, PrintPageEventArgs e)
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

            bool isNhap = _loaiHoaDon == "nhap";

            DrawCentered("TỨ ĐẠI THIÊN LONG", brandFont);
            DrawCentered("169 Nguyễn Lương Bằng", normalFont);
            DrawCentered("SĐT: 0374895922", normalFont);

            y += 2f;
            e.Graphics.DrawLine(Pens.Gray, left, y, right, y);
            y += 4f;

            DrawCentered(isNhap ? "PHIẾU NHẬP KHO" : "HÓA ĐƠN BÁN HÀNG", titleFont);
            e.Graphics.DrawString($"Mã hóa đơn: {_printMaHd}", normalFont, Brushes.Black, left, y); y += normalFont.GetHeight(e.Graphics) + 1f;
            e.Graphics.DrawString($"Ngày: {_printThoiGian:dd/MM/yyyy HH:mm}", normalFont, Brushes.Black, left, y); y += normalFont.GetHeight(e.Graphics) + 1f;
            e.Graphics.DrawString($"{(isNhap ? "Nhà cung cấp" : "Khách hàng")}: {_printDoiTac}", normalFont, Brushes.Black, left, y); y += normalFont.GetHeight(e.Graphics) + 1f;
            e.Graphics.DrawString($"{(isNhap ? "Người nhập" : "Nhân viên")}: {_printNhanVien}", normalFont, Brushes.Black, left, y); y += normalFont.GetHeight(e.Graphics) + 3f;

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

            e.Graphics.DrawString(isNhap ? "Tên hàng" : "Tên món", monoFont, Brushes.Black, nameRect);
            e.Graphics.DrawString("SL", monoFont, Brushes.Black, qtyRect, rightFormat);
            e.Graphics.DrawString("Đơn giá", monoFont, Brushes.Black, priceRect, rightFormat);
            e.Graphics.DrawString("T.Tiền", monoFont, Brushes.Black, totalRect, rightFormat);
            y += 15f;

            e.Graphics.DrawLine(Pens.LightGray, left, y, right, y);
            y += 3f;

            foreach (DataGridViewRow r in _dgvHoaDonDetail.Rows)
            {
                if (r.IsNewRow) continue;

                string ten = Convert.ToString(r.Cells["TenHang"].Value) ?? string.Empty;
                string sl = Convert.ToString(r.Cells["SoLuong"].Value) ?? "0";
                decimal dg = 0m;
                decimal tt = 0m;
                try { dg = Convert.ToDecimal(r.Cells["DonGia"].Value ?? 0m, CultureInfo.InvariantCulture); } catch { }
                try { tt = Convert.ToDecimal(r.Cells["ThanhTien"].Value ?? 0m, CultureInfo.InvariantCulture); } catch { }

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

            e.Graphics.DrawString(isNhap ? $"TỔNG TIỀN: {_printTongTien:N0} VNĐ" : $"TỔNG CỘNG: {_printTongTien:N0} đ", totalFont, Brushes.Black, left, y);
            y += totalFont.GetHeight(e.Graphics) + 4f;

            if (_printQrImage is not null)
            {
                y += 3f;
                if (isNhap)
                {
                    DrawCentered("Mã xác thực nhập kho", normalFont);
                }
                else
                {
                    DrawCentered("Quét mã để thanh toán", normalFont);
                }

                const float qrSize = 74f;
                float qrX = left + (width - qrSize) / 2f;
                e.Graphics.DrawImage(_printQrImage, qrX, y, qrSize, qrSize);
                y += qrSize + 6f;
            }

            if (isNhap)
            {
                y += 4f;
                float signY = y;
                float half = width / 2f;
                e.Graphics.DrawString("Người nhập kho", normalFont, Brushes.Black, left, signY);
                e.Graphics.DrawString("Người nhận hàng", normalFont, Brushes.Black, left + half, signY);
                signY += normalFont.GetHeight(e.Graphics) + 16f;
                e.Graphics.DrawLine(Pens.Black, left, signY, left + half - 12f, signY);
                e.Graphics.DrawLine(Pens.Black, left + half, signY, right, signY);
            }
            else
            {
                DrawCentered("Cảm ơn Quý khách. Hẹn gặp lại!", normalFont);
            }
        }

        private void ThongTinCaNhan_Click(object sender, EventArgs e) => OpenAndClose(new TrangNhanVien1(_loggedInMaNV));
        private void btn_QLKH_Click(object sender, EventArgs e) => OpenAndClose(new BanHang(_loggedInMaNV));
        private void btn_QLMA_Click(object sender, EventArgs e) => OpenAndClose(new MuaHang(_loggedInMaNV));
        private void btn_QLHDN_Click(object sender, EventArgs e) => OpenAndClose(new KhachHang(_loggedInMaNV));
        private void btn_QLNCC_Click(object sender, EventArgs e) => _btnLamMoi_Click(sender, e);

        private void btn_DangXuat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn đăng xuất?", "Đăng xuất", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                PBL3.AdminNavigationManager.Logout(this);
            }
        }

        private void OpenAndClose(Form target) => PBL3.AdminNavigationManager.Navigate(this, target);

        private void lblHoTen_Click(object sender, EventArgs e) { }
        private void lblMatKhau_Click(object sender, EventArgs e) { }
        private void _txtMaNV_TextChanged(object sender, EventArgs e) { }

        private void _lblTongCongTatCa_Click(object sender, EventArgs e)
        {

        }

        private void txtKhachHang_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
