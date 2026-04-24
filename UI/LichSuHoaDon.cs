using PBL3.DataBase;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;

namespace PBL3
{
    public partial class LichSuHoaDon : Form
    {
        private sealed class FilterOption
        {
            public string Display { get; init; } = string.Empty;
            public string FieldName { get; init; } = string.Empty;
            public string Value { get; init; } = string.Empty;
            public override string ToString() => Display;
        }

        private readonly bool _isAdmin;
        private readonly string? _maNvDangNhap;
        private readonly List<FilterOption> _filterOptions = new List<FilterOption>();
        private DataTable? _masterTable;
        private string _currentInvoiceType = "BAN";
        private string? _selectedMaHoaDon;

        private bool _hasTrangThaiHdb;
        private bool _hasTrangThaiHdn;
        private string _trangThaiHdbType = string.Empty;
        private string _trangThaiHdnType = string.Empty;

        private string _printContent = string.Empty;
        private Image? _printQrImage;
        private string _printMaHd = string.Empty;
        private string _printNhanVien = string.Empty;
        private string _printDoiTac = string.Empty;
        private DateTime _printThoiGian;
        private decimal _printTongTien;


        public LichSuHoaDon() : this(true, null)
        {
        }

        public LichSuHoaDon(bool isAdmin, string? maNvDangNhap)
        {
            _isAdmin = isAdmin;
            _maNvDangNhap = maNvDangNhap;
            InitializeComponent();

            _btnHoaDonBan.Click += (_, __) => SwitchInvoiceType("BAN");
            _btnHoaDonNhap.Click += (_, __) => SwitchInvoiceType("NHAP");

            _txtTimMaHD.TextChanged += (_, __) => ApplySearchFilter();
            _cboLocDoiTuong.SelectionChangeCommitted += (_, __) => ApplySearchFilter();
            _dtpTuNgay.ValueChanged += FilterTime_Changed;
            _dtpDenNgay.ValueChanged += FilterTime_Changed;

            _dgvHoaDonMaster.CellClick += DgvHoaDonMaster_CellClick;

            _btnLamMoi.Click += BtnLamMoi_Click;
            _btnHuyHoaDon.Click += BtnHuyHoaDon_Click;
            _btnXuatBaoCao.Click += BtnXuatBaoCao_Click;
            _btnInLai.Click += BtnInLai_Click;

            btn_QLNCC.Click += btn_QLNCC_Click;
            btn_QLKH.Click += btn_QLKH_Click;
            btn_QLNV.Click += btn_QLNV_Click;
            btn_QLMA.Click += btn_QLMA_Click;
            btn_QLHDN.Click += btn_QLHDN_Click;
            btn_ThongKe.Click += btn_ThongKe_Click;
            btn_DangXuat.Click += btn_DangXuat_Click;
            btn_LSHD.Click += (_, __) => { };

            // Layout is now managed in Designer to match runtime with Design view.
        }

        private void LichSuHoaDon_Load(object? sender, EventArgs e)
        {
            try
            {
                DetectSchema();
                InitializeDefaultFilters();
                _btnHuyHoaDon.Visible = _isAdmin;
                SwitchInvoiceType("BAN");
                LoadTopMetrics();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không thể tải trang lịch sử hóa đơn.\n{ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DetectSchema()
        {
            using SqlConnection conn = DbHelper.GetConnection();
            conn.Open();

            EnsureTrangThaiColumns(conn);

            _hasTrangThaiHdb = TableColumnExists(conn, "HOA_DON_BAN", "TrangThai");
            _hasTrangThaiHdn = TableColumnExists(conn, "HOA_DON_NHAP", "TrangThai");

            _trangThaiHdbType = _hasTrangThaiHdb ? GetColumnDataType(conn, "HOA_DON_BAN", "TrangThai") : string.Empty;
            _trangThaiHdnType = _hasTrangThaiHdn ? GetColumnDataType(conn, "HOA_DON_NHAP", "TrangThai") : string.Empty;
        }

        private static void EnsureTrangThaiColumns(SqlConnection conn)
        {
            const string sql = @"
IF COL_LENGTH('dbo.HOA_DON_BAN', 'TrangThai') IS NULL
BEGIN
    ALTER TABLE dbo.HOA_DON_BAN
    ADD TrangThai bit NOT NULL CONSTRAINT DF_HOA_DON_BAN_TrangThai DEFAULT (1) WITH VALUES;
END

IF COL_LENGTH('dbo.HOA_DON_NHAP', 'TrangThai') IS NULL
BEGIN
    ALTER TABLE dbo.HOA_DON_NHAP
    ADD TrangThai bit NOT NULL CONSTRAINT DF_HOA_DON_NHAP_TrangThai DEFAULT (1) WITH VALUES;
END";

            using SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
        }

        private void InitializeDefaultFilters()
        {
            if (!_isAdmin)
            {
                _dtpTuNgay.Value = DateTime.Today;
                _dtpDenNgay.Value = DateTime.Today;
                return;
            }

            DateTime tuNgay = DateTime.Today.AddDays(-30);
            DateTime denNgay = DateTime.Today;

            try
            {
                using SqlConnection conn = DbHelper.GetConnection();
                conn.Open();

                const string sqlMin = @"
SELECT MIN(NgayAny)
FROM (
    SELECT MIN(NgayBan) AS NgayAny FROM dbo.HOA_DON_BAN
    UNION ALL
    SELECT MIN(NgayNhap) AS NgayAny FROM dbo.HOA_DON_NHAP
) x
WHERE NgayAny IS NOT NULL";

                const string sqlMax = @"
SELECT MAX(NgayAny)
FROM (
    SELECT MAX(NgayBan) AS NgayAny FROM dbo.HOA_DON_BAN
    UNION ALL
    SELECT MAX(NgayNhap) AS NgayAny FROM dbo.HOA_DON_NHAP
) x
WHERE NgayAny IS NOT NULL";

                using (SqlCommand cmdMin = new SqlCommand(sqlMin, conn))
                {
                    object? minObj = cmdMin.ExecuteScalar();
                    if (minObj is not null && minObj != DBNull.Value)
                    {
                        tuNgay = Convert.ToDateTime(minObj, CultureInfo.InvariantCulture).Date;
                    }
                }

                using (SqlCommand cmdMax = new SqlCommand(sqlMax, conn))
                {
                    object? maxObj = cmdMax.ExecuteScalar();
                    if (maxObj is not null && maxObj != DBNull.Value)
                    {
                        DateTime maxDate = Convert.ToDateTime(maxObj, CultureInfo.InvariantCulture).Date;
                        denNgay = maxDate > denNgay ? maxDate : denNgay;
                    }
                }
            }
            catch
            {
            }

            _dtpTuNgay.Value = tuNgay;
            _dtpDenNgay.Value = denNgay;
        }

        private static bool TableColumnExists(SqlConnection conn, string tableName, string columnName)
        {
            using SqlCommand cmd = new SqlCommand(@"SELECT CASE WHEN EXISTS (
SELECT 1
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_SCHEMA='dbo' AND TABLE_NAME=@TableName AND COLUMN_NAME=@ColumnName
) THEN 1 ELSE 0 END", conn);
            cmd.Parameters.Add("@TableName", SqlDbType.VarChar, 128).Value = tableName;
            cmd.Parameters.Add("@ColumnName", SqlDbType.VarChar, 128).Value = columnName;
            return Convert.ToInt32(cmd.ExecuteScalar(), CultureInfo.InvariantCulture) == 1;
        }

        private static string GetColumnDataType(SqlConnection conn, string tableName, string columnName)
        {
            using SqlCommand cmd = new SqlCommand(@"SELECT DATA_TYPE
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_SCHEMA='dbo' AND TABLE_NAME=@TableName AND COLUMN_NAME=@ColumnName", conn);
            cmd.Parameters.Add("@TableName", SqlDbType.VarChar, 128).Value = tableName;
            cmd.Parameters.Add("@ColumnName", SqlDbType.VarChar, 128).Value = columnName;
            return Convert.ToString(cmd.ExecuteScalar())?.Trim().ToLowerInvariant() ?? string.Empty;
        }

        private static string EscapeForRowFilter(string input)
        {
            return input.Replace("'", "''").Replace("[", "[[]").Replace("%", "[%]").Replace("*", "[*]");
        }

        private static decimal ToDecimalOrZero(object? value)
        {
            return value is null || value == DBNull.Value
                ? 0m
                : Convert.ToDecimal(value, CultureInfo.InvariantCulture);
        }

        private static int ToIntOrZero(object? value)
        {
            return value is null || value == DBNull.Value
                ? 0
                : Convert.ToInt32(value, CultureInfo.InvariantCulture);
        }

        private void SwitchInvoiceType(string type)
        {
            _currentInvoiceType = type;
            _selectedMaHoaDon = null;

            _btnHoaDonBan.BackColor = type == "BAN" ? Color.Salmon : Color.Bisque;
            _btnHoaDonNhap.BackColor = type == "NHAP" ? Color.Salmon : Color.Bisque;

            LoadFilterOptions();
            LoadMasterData();
            LoadTopMetrics();
            UpdateMetricCardsByType();
        }

        private void ApplyCompactLayout()
        {
            // Designer-managed layout
        }

        private void UpdateMetricCardsByType()
        {
            pnlCard3.Visible = true;
        }

        private void FilterTime_Changed(object? sender, EventArgs e)
        {
            if (_dtpTuNgay.Value.Date > _dtpDenNgay.Value.Date)
            {
                _dtpDenNgay.Value = _dtpTuNgay.Value.Date;
            }

            LoadMasterData();
            LoadTopMetrics();
        }

        private void LoadFilterOptions()
        {
            _filterOptions.Clear();
            _filterOptions.Add(new FilterOption { Display = "Tất cả", FieldName = string.Empty, Value = string.Empty });

            _filterOptions.Add(new FilterOption { Display = "Mã HĐ", FieldName = "MaHD", Value = string.Empty });
            _filterOptions.Add(new FilterOption { Display = "Thời gian", FieldName = "ThoiGian", Value = string.Empty });
            _filterOptions.Add(new FilterOption { Display = "Tổng tiền", FieldName = "TongTien", Value = string.Empty });

            _cboLocDoiTuong.DataSource = null;
            _cboLocDoiTuong.DataSource = _filterOptions;
            _cboLocDoiTuong.DisplayMember = nameof(FilterOption.Display);
            _cboLocDoiTuong.SelectedIndex = 0;
        }

        private string BuildTrangThaiExpression(string alias, bool hasTrangThai, string dataType)
        {
            if (!hasTrangThai)
            {
                return "N'Đã thanh toán'";
            }

            if (dataType == "bit")
            {
                return $"CASE WHEN ISNULL({alias}.TrangThai, 1) = 1 THEN N'Đã thanh toán' ELSE N'Đã hủy' END";
            }

            return $"CASE WHEN CAST({alias}.TrangThai AS NVARCHAR(50)) LIKE N'%hủy%' THEN N'Đã hủy' ELSE ISNULL(CAST({alias}.TrangThai AS NVARCHAR(50)), N'Đã thanh toán') END";
        }

        private void LoadMasterData()
        {
            string statusExpr = _currentInvoiceType == "BAN"
                ? BuildTrangThaiExpression("h", _hasTrangThaiHdb, _trangThaiHdbType)
                : BuildTrangThaiExpression("h", _hasTrangThaiHdn, _trangThaiHdnType);

            string sql;
            if (_currentInvoiceType == "BAN")
            {
                sql = $@"
SELECT h.MaHDB AS MaHD,
       h.NgayBan AS ThoiGian,
       ISNULL(nv.HoTen, N'') AS NguoiThucHien,
       ISNULL(kh.SDT, N'Khách lẻ') AS DoiTac,
       ISNULL(kh.SDT, N'') AS SDTKhach,
       ISNULL(h.TongTien, 0) AS TongTien,
       {statusExpr} AS TrangThai,
       CONVERT(VARCHAR(20), h.MaNV) AS MaNV,
       CONVERT(VARCHAR(20), h.MaKH) AS MaDoiTac
FROM dbo.HOA_DON_BAN h
LEFT JOIN dbo.NHAN_VIEN nv ON nv.MaNV = h.MaNV
LEFT JOIN dbo.KHACH_HANG kh ON kh.MaKH = h.MaKH
WHERE h.NgayBan >= @FromDate AND h.NgayBan < @ToDate";

                if (_hasTrangThaiHdb)
                {
                    sql += _trangThaiHdbType == "bit"
                        ? " AND ISNULL(h.TrangThai, 1) = 1"
                        : " AND CAST(h.TrangThai AS NVARCHAR(50)) NOT LIKE N'%hủy%'";
                }

                if (!_isAdmin && !string.IsNullOrWhiteSpace(_maNvDangNhap))
                {
                    sql += " AND CONVERT(VARCHAR(20), h.MaNV) = @MaNV";
                }

                sql += " ORDER BY h.NgayBan DESC";
            }
            else
            {
                sql = $@"
SELECT h.MaHDN AS MaHD,
       h.NgayNhap AS ThoiGian,
       ISNULL(nv.HoTen, N'') AS NguoiThucHien,
       ISNULL(ncc.TenNCC, N'') AS DoiTac,
       CAST(N'' AS NVARCHAR(20)) AS SDTKhach,
       ISNULL(h.TongTien, 0) AS TongTien,
       {statusExpr} AS TrangThai,
       CONVERT(VARCHAR(20), h.MaNV) AS MaNV,
       CONVERT(VARCHAR(20), h.MaNCC) AS MaDoiTac
FROM dbo.HOA_DON_NHAP h
LEFT JOIN dbo.NHAN_VIEN nv ON nv.MaNV = h.MaNV
LEFT JOIN dbo.NHA_CUNG_CAP ncc ON ncc.MaNCC = h.MaNCC
WHERE h.NgayNhap >= @FromDate AND h.NgayNhap < @ToDate";

                if (_hasTrangThaiHdn)
                {
                    sql += _trangThaiHdnType == "bit"
                        ? " AND ISNULL(h.TrangThai, 1) = 1"
                        : " AND CAST(h.TrangThai AS NVARCHAR(50)) NOT LIKE N'%hủy%'";
                }

                if (!_isAdmin && !string.IsNullOrWhiteSpace(_maNvDangNhap))
                {
                    sql += " AND CONVERT(VARCHAR(20), h.MaNV) = @MaNV";
                }

                sql += " ORDER BY h.NgayNhap DESC";
            }

            using SqlConnection conn = DbHelper.GetConnection();
            using SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add("@FromDate", SqlDbType.DateTime).Value = _dtpTuNgay.Value.Date;
            cmd.Parameters.Add("@ToDate", SqlDbType.DateTime).Value = _dtpDenNgay.Value.Date.AddDays(1);
            if (!_isAdmin && !string.IsNullOrWhiteSpace(_maNvDangNhap))
            {
                cmd.Parameters.Add("@MaNV", SqlDbType.VarChar, 20).Value = _maNvDangNhap;
            }

            using SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            _masterTable = dt;
            _dgvHoaDonMaster.DataSource = _masterTable;
            ConfigureMasterGrid();
            ApplySearchFilter();
        }

        private void ConfigureMasterGrid()
        {
            _dgvHoaDonMaster.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            _dgvHoaDonMaster.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
            _dgvHoaDonMaster.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;

            SetHeaderText(_dgvHoaDonMaster, "MaHD", "Mã HĐ");
            SetHeaderText(_dgvHoaDonMaster, "ThoiGian", "Thời gian");
            SetHeaderText(_dgvHoaDonMaster, "NguoiThucHien", "Người thực hiện");
            SetHeaderText(_dgvHoaDonMaster, "DoiTac", _currentInvoiceType == "BAN" ? "Khách hàng" : "Nhà cung cấp");
            SetHeaderText(_dgvHoaDonMaster, "TongTien", "Tổng tiền");
            SetHeaderText(_dgvHoaDonMaster, "TrangThai", "Trạng thái");

            SetColumnWidth(_dgvHoaDonMaster, "MaHD", 85);
            SetColumnWidth(_dgvHoaDonMaster, "ThoiGian", 100);
            SetColumnWidth(_dgvHoaDonMaster, "NguoiThucHien", 150);
            SetColumnWidth(_dgvHoaDonMaster, "DoiTac", 170);
            SetColumnWidth(_dgvHoaDonMaster, "TongTien", 120);
            SetColumnWidth(_dgvHoaDonMaster, "TrangThai", 110);

            if (_dgvHoaDonMaster.Columns.Contains("ThoiGian"))
            {
                _dgvHoaDonMaster.Columns["ThoiGian"].DefaultCellStyle.Format = "HH:mm - dd/MM";
            }

            if (_dgvHoaDonMaster.Columns.Contains("TongTien"))
            {
                _dgvHoaDonMaster.Columns["TongTien"].DefaultCellStyle.Format = "N0";
                _dgvHoaDonMaster.Columns["TongTien"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                _dgvHoaDonMaster.Columns["TongTien"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            HideColumn(_dgvHoaDonMaster, "SDTKhach");
            HideColumn(_dgvHoaDonMaster, "MaNV");
            HideColumn(_dgvHoaDonMaster, "MaDoiTac");
            HideColumn(_dgvHoaDonMaster, "NguoiThucHien");
            HideColumn(_dgvHoaDonMaster, "DoiTac");
            HideColumn(_dgvHoaDonMaster, "TrangThai");
        }

        private void ConfigureDetailGrid()
        {
            _dgvHoaDonDetail.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            _dgvHoaDonDetail.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
            _dgvHoaDonDetail.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;

            SetHeaderText(_dgvHoaDonDetail, "TenHang", _currentInvoiceType == "BAN" ? "Tên món" : "Tên nguyên liệu");
            SetHeaderText(_dgvHoaDonDetail, "SoLuong", "SL");
            SetHeaderText(_dgvHoaDonDetail, "DonGia", "Đơn giá");
            SetHeaderText(_dgvHoaDonDetail, "ThanhTien", "Thành tiền");
            SetHeaderText(_dgvHoaDonDetail, "GhiChu", "Ghi chú");

            SetColumnWidth(_dgvHoaDonDetail, "TenHang", 180);
            SetColumnWidth(_dgvHoaDonDetail, "SoLuong", 40);
            SetColumnWidth(_dgvHoaDonDetail, "DonGia", 100);
            SetColumnWidth(_dgvHoaDonDetail, "ThanhTien", 110);
            SetColumnWidth(_dgvHoaDonDetail, "GhiChu", 140);

            HideColumn(_dgvHoaDonDetail, "GhiChu");

            if (_dgvHoaDonDetail.Columns.Contains("SoLuong"))
            {
                _dgvHoaDonDetail.Columns["SoLuong"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            if (_dgvHoaDonDetail.Columns.Contains("DonGia"))
            {
                _dgvHoaDonDetail.Columns["DonGia"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                _dgvHoaDonDetail.Columns["DonGia"].DefaultCellStyle.Format = "N0";
            }

            if (_dgvHoaDonDetail.Columns.Contains("ThanhTien"))
            {
                _dgvHoaDonDetail.Columns["ThanhTien"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                _dgvHoaDonDetail.Columns["ThanhTien"].DefaultCellStyle.Format = "N0";
            }
        }

        private static void HideColumn(DataGridView dgv, string name)
        {
            if (dgv.Columns.Contains(name))
            {
                dgv.Columns[name].Visible = false;
            }
        }

        private static void SetHeaderText(DataGridView dgv, string columnName, string header)
        {
            if (dgv.Columns.Contains(columnName))
            {
                dgv.Columns[columnName].HeaderText = header;
            }
        }

        private static void SetColumnWidth(DataGridView dgv, string columnName, int width)
        {
            if (dgv.Columns.Contains(columnName))
            {
                dgv.Columns[columnName].Width = width;
            }
        }

        private void ApplySearchFilter()
        {
            if (_masterTable is null)
            {
                return;
            }

            string keyword = EscapeForRowFilter(_txtTimMaHD.Text.Trim());

            if (string.IsNullOrWhiteSpace(keyword))
            {
                _masterTable.DefaultView.RowFilter = string.Empty;
                UpdateDetailSelectionAfterFilter();
                return;
            }

            string rowFilter;
            if (_cboLocDoiTuong.SelectedItem is not FilterOption opt || string.IsNullOrWhiteSpace(opt.FieldName))
            {
                rowFilter =
                    $"Convert(MaHD, 'System.String') LIKE '%{keyword}%'" +
                    $" OR Convert(ThoiGian, 'System.String') LIKE '%{keyword}%'" +
                    $" OR Convert(TongTien, 'System.String') LIKE '%{keyword}%'";
            }
            else
            {
                rowFilter = $"Convert({opt.FieldName}, 'System.String') LIKE '%{keyword}%'";
            }

            _masterTable.DefaultView.RowFilter = rowFilter;
            UpdateDetailSelectionAfterFilter();
        }

        private void UpdateDetailSelectionAfterFilter()
        {
            if (_dgvHoaDonMaster.Rows.Count == 0)
            {
                _selectedMaHoaDon = null;
                _dgvHoaDonDetail.DataSource = null;
                UpdateReceiptHeader(null, null);
                return;
            }

            if (_dgvHoaDonMaster.CurrentRow is null || _dgvHoaDonMaster.CurrentRow.IsNewRow)
            {
                _dgvHoaDonMaster.Rows[0].Selected = true;
                _dgvHoaDonMaster.CurrentCell = _dgvHoaDonMaster.Rows[0].Cells[0];
            }

            if (_dgvHoaDonMaster.CurrentRow is not null)
            {
                _selectedMaHoaDon = Convert.ToString(_dgvHoaDonMaster.CurrentRow.Cells["MaHD"].Value);
                LoadDetailData(_selectedMaHoaDon);
                UpdateReceiptHeader(_dgvHoaDonMaster.CurrentRow, _currentInvoiceType);
            }
        }

        private static string? DetectFirstColumn(SqlConnection conn, string tableName, params string[] candidates)
        {
            foreach (string candidate in candidates)
            {
                if (TableColumnExists(conn, tableName, candidate))
                {
                    return candidate;
                }
            }

            return null;
        }

        private void LoadDetailData(string? maHd)
        {
            if (string.IsNullOrWhiteSpace(maHd))
            {
                _dgvHoaDonDetail.DataSource = null;
                return;
            }

            using SqlConnection conn = DbHelper.GetConnection();
            conn.Open();

            string sql;
            if (_currentInvoiceType == "BAN")
            {
                string? donGiaCol = DetectFirstColumn(conn, "CT_HOA_DON_BAN", "DonGia", "DonGiaBan", "Gia", "GiaBan", "DonGiaCT", "DonGiaBanLe");
                string? ghiChuCol = DetectFirstColumn(conn, "CT_HOA_DON_BAN", "GhiChu", "MoTa", "Note");

                bool hasCtMaDvpv = TableColumnExists(conn, "CT_HOA_DON_BAN", "MaDVPV");
                string? mdvGiaCol = DetectFirstColumn(conn, "MON_DON_VI_PHUC_VU", "DonGia", "GiaBan", "Gia", "DonGiaBan");
                string? mdvMaDvpvCol = DetectFirstColumn(conn, "MON_DON_VI_PHUC_VU", "MaDVPV", "MaDV", "MaDonViPhucVu");
                string? monGiaCol = DetectFirstColumn(conn, "MON_BAN", "DonGia", "GiaBan", "Gia", "DonGiaBan", "DonGiaBanLe");

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

                string ghiChuExpr = ghiChuCol is null ? "CAST(NULL AS NVARCHAR(300))" : $"ct.[{ghiChuCol}]";

                sql = $@"
SELECT ISNULL(mb.TenMon, CONVERT(NVARCHAR(100), ct.MaMon)) AS TenHang,
       ISNULL(ct.SoLuong, 0) AS SoLuong,
       {donGiaExpr} AS DonGia,
       ISNULL(ct.SoLuong, 0) * {donGiaExpr} AS ThanhTien,
       {ghiChuExpr} AS GhiChu
FROM dbo.CT_HOA_DON_BAN ct
LEFT JOIN dbo.MON_BAN mb ON mb.MaMon = ct.MaMon
{mdvJoin}
WHERE ct.MaHDB = @MaHD";
            }
            else
            {
                sql = @"
SELECT ISNULL(nl.TenNL, CONVERT(NVARCHAR(100), ct.MaNL)) AS TenHang,
       ISNULL(ct.SoLuong, 0) AS SoLuong,
       ISNULL(ct.DonGia, 0) AS DonGia,
       ISNULL(ct.SoLuong, 0) * ISNULL(ct.DonGia, 0) AS ThanhTien,
       CAST(NULL AS NVARCHAR(300)) AS GhiChu
FROM dbo.CT_HOA_DON_NHAP ct
LEFT JOIN dbo.NGUYEN_LIEU nl ON nl.MaNL = ct.MaNL
WHERE ct.MaHDN = @MaHD";
            }

            using SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add("@MaHD", SqlDbType.VarChar, 20).Value = maHd;

            using SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable detail = new DataTable();
            da.Fill(detail);
            _dgvHoaDonDetail.DataSource = detail;
            ConfigureDetailGrid();
        }

        private void DgvHoaDonMaster_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            DataGridViewRow row = _dgvHoaDonMaster.Rows[e.RowIndex];
            _selectedMaHoaDon = Convert.ToString(row.Cells["MaHD"].Value) ?? string.Empty;
            LoadDetailData(_selectedMaHoaDon);
            UpdateReceiptHeader(row, _currentInvoiceType);
        }

        private void UpdateReceiptHeader(DataGridViewRow? row, string? invoiceType)
        {
            string nhanVien = row is null ? "-" : (Convert.ToString(row.Cells["NguoiThucHien"].Value) ?? "-");
            string doiTac = row is null ? "-" : (Convert.ToString(row.Cells["DoiTac"].Value) ?? "-");

            lblReceiptNhanVien.Text = $"👤 Nhân viên: {nhanVien}";
            lblReceiptDoiTac.Text = invoiceType == "BAN"
                ? $"🤝 Khách hàng: {doiTac}"
                : $"🤝 Nhà cung cấp: {doiTac}";
            lblReceiptThanhToan.Text = string.Empty;
        }

        private void LoadTopMetrics()
        {
            DateTime from = _dtpTuNgay.Value.Date;
            DateTime to = _dtpDenNgay.Value.Date.AddDays(1);

            int soDonHuy = 0;

            using SqlConnection conn = DbHelper.GetConnection();
            conn.Open();

            if (_hasTrangThaiHdb)
            {
                string where = _trangThaiHdbType == "bit" ? "ISNULL(TrangThai,1)=0" : "CAST(TrangThai AS NVARCHAR(50)) LIKE N'%hủy%'";
                using SqlCommand cmd = new SqlCommand($"SELECT COUNT(1) FROM dbo.HOA_DON_BAN WHERE NgayBan >= @FromDate AND NgayBan < @ToDate AND {where}", conn);
                cmd.Parameters.Add("@FromDate", SqlDbType.DateTime).Value = from;
                cmd.Parameters.Add("@ToDate", SqlDbType.DateTime).Value = to;
                soDonHuy += ToIntOrZero(cmd.ExecuteScalar());
            }

            if (_hasTrangThaiHdn)
            {
                string where = _trangThaiHdnType == "bit" ? "ISNULL(TrangThai,1)=0" : "CAST(TrangThai AS NVARCHAR(50)) LIKE N'%hủy%'";
                using SqlCommand cmd = new SqlCommand($"SELECT COUNT(1) FROM dbo.HOA_DON_NHAP WHERE NgayNhap >= @FromDate AND NgayNhap < @ToDate AND {where}", conn);
                cmd.Parameters.Add("@FromDate", SqlDbType.DateTime).Value = from;
                cmd.Parameters.Add("@ToDate", SqlDbType.DateTime).Value = to;
                soDonHuy += ToIntOrZero(cmd.ExecuteScalar());
            }

            _lblSoDonHuyValue.Text = soDonHuy.ToString(CultureInfo.InvariantCulture);
        }

        private void BtnLamMoi_Click(object? sender, EventArgs e)
        {
            _txtTimMaHD.Clear();
            _cboLocDoiTuong.SelectedIndex = 0;
            LoadMasterData();
            LoadTopMetrics();
        }

        private void BtnHuyHoaDon_Click(object? sender, EventArgs e)
        {
            if (!_isAdmin)
            {
                MessageBox.Show("Bạn không có quyền hủy hóa đơn.", "Phân quyền", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(_selectedMaHoaDon))
            {
                MessageBox.Show("Vui lòng chọn hóa đơn cần hủy.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn hủy hóa đơn đã chọn?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes)
            {
                return;
            }

            try
            {
                using SqlConnection conn = DbHelper.GetConnection();
                conn.Open();
                using SqlTransaction tran = conn.BeginTransaction();

                if (_currentInvoiceType == "BAN")
                {
                    CancelBanInvoice(conn, tran, _selectedMaHoaDon);
                }
                else
                {
                    CancelNhapInvoice(conn, tran, _selectedMaHoaDon);
                }

                tran.Commit();
                MessageBox.Show("Hủy hóa đơn thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadMasterData();
                LoadTopMetrics();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hủy hóa đơn thất bại.\n{ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CancelBanInvoice(SqlConnection conn, SqlTransaction tran, string maHd)
        {
            if (!_hasTrangThaiHdb)
            {
                throw new InvalidOperationException("CSDL chưa có cột TrangThai cho hóa đơn bán.");
            }

            EnsureNotCanceled(conn, tran, "HOA_DON_BAN", "MaHDB", maHd, _trangThaiHdbType);
            UpdateInvoiceStatus(conn, tran, "HOA_DON_BAN", "MaHDB", maHd, _trangThaiHdbType);

            const string rollbackStockSql = @"
UPDATE nl
SET nl.SoLuongTon = nl.SoLuongTon + (dm.SoLuongSuDung * ct.SoLuong)
FROM dbo.NGUYEN_LIEU nl
INNER JOIN dbo.DINH_MUC_MON dm ON dm.MaNL = nl.MaNL
INNER JOIN dbo.CT_HOA_DON_BAN ct ON ct.MaMon = dm.MaMon
WHERE ct.MaHDB = @MaHD
  AND (dm.MaDVPV = ct.MaDVPV
       OR NOT EXISTS (
            SELECT 1
            FROM dbo.DINH_MUC_MON d2
            WHERE d2.MaMon = ct.MaMon AND d2.MaDVPV = ct.MaDVPV
       ))";

            using SqlCommand cmdStock = new SqlCommand(rollbackStockSql, conn, tran);
            cmdStock.Parameters.Add("@MaHD", SqlDbType.VarChar, 20).Value = maHd;
            cmdStock.ExecuteNonQuery();
        }

        private void CancelNhapInvoice(SqlConnection conn, SqlTransaction tran, string maHd)
        {
            if (!_hasTrangThaiHdn)
            {
                throw new InvalidOperationException("CSDL chưa có cột TrangThai cho hóa đơn nhập.");
            }

            EnsureNotCanceled(conn, tran, "HOA_DON_NHAP", "MaHDN", maHd, _trangThaiHdnType);
            UpdateInvoiceStatus(conn, tran, "HOA_DON_NHAP", "MaHDN", maHd, _trangThaiHdnType);

            const string rollbackStockSql = @"
UPDATE nl
SET nl.SoLuongTon = nl.SoLuongTon - ct.SoLuong
FROM dbo.NGUYEN_LIEU nl
INNER JOIN dbo.CT_HOA_DON_NHAP ct ON ct.MaNL = nl.MaNL
WHERE ct.MaHDN = @MaHD";

            using SqlCommand cmdStock = new SqlCommand(rollbackStockSql, conn, tran);
            cmdStock.Parameters.Add("@MaHD", SqlDbType.VarChar, 20).Value = maHd;
            cmdStock.ExecuteNonQuery();
        }

        private static void EnsureNotCanceled(SqlConnection conn, SqlTransaction tran, string tableName, string idColumn, string maHd, string statusType)
        {
            string sql = statusType == "bit"
                ? $"SELECT CASE WHEN ISNULL(TrangThai,1)=0 THEN 1 ELSE 0 END FROM dbo.{tableName} WHERE {idColumn} = @MaHD"
                : $"SELECT CASE WHEN CAST(TrangThai AS NVARCHAR(50)) LIKE N'%hủy%' THEN 1 ELSE 0 END FROM dbo.{tableName} WHERE {idColumn} = @MaHD";

            using SqlCommand cmd = new SqlCommand(sql, conn, tran);
            cmd.Parameters.Add("@MaHD", SqlDbType.VarChar, 20).Value = maHd;
            bool isCanceled = Convert.ToInt32(cmd.ExecuteScalar() ?? 0, CultureInfo.InvariantCulture) == 1;
            if (isCanceled)
            {
                throw new InvalidOperationException("Hóa đơn đã ở trạng thái Đã hủy.");
            }
        }

        private static void UpdateInvoiceStatus(SqlConnection conn, SqlTransaction tran, string tableName, string idColumn, string maHd, string statusType)
        {
            string sql = $"UPDATE dbo.{tableName} SET TrangThai = @TrangThai WHERE {idColumn} = @MaHD";
            using SqlCommand cmd = new SqlCommand(sql, conn, tran);
            cmd.Parameters.Add("@MaHD", SqlDbType.VarChar, 20).Value = maHd;

            if (statusType == "bit")
            {
                cmd.Parameters.Add("@TrangThai", SqlDbType.Bit).Value = false;
            }
            else
            {
                cmd.Parameters.Add("@TrangThai", SqlDbType.NVarChar, 50).Value = "Đã hủy";
            }

            int rows = cmd.ExecuteNonQuery();
            if (rows == 0)
            {
                throw new InvalidOperationException("Không tìm thấy hóa đơn để cập nhật trạng thái.");
            }
        }

        private void BtnXuatBaoCao_Click(object? sender, EventArgs e)
        {
            if (_masterTable is null)
            {
                return;
            }

            using SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "CSV file (*.csv)|*.csv",
                FileName = $"LichSuHoaDon_{_currentInvoiceType}_{DateTime.Now:yyyyMMdd_HHmmss}.csv"
            };

            if (sfd.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("MaHD,ThoiGian,NguoiThucHien,DoiTac,TongTien,TrangThai");

            foreach (DataRowView row in _masterTable.DefaultView)
            {
                string line = string.Join(",",
                    Csv(row["MaHD"]),
                    Csv(row["ThoiGian"]),
                    Csv(row["NguoiThucHien"]),
                    Csv(row["DoiTac"]),
                    Csv(row["TongTien"]),
                    Csv(row["TrangThai"]));
                sb.AppendLine(line);
            }

            File.WriteAllText(sfd.FileName, sb.ToString(), Encoding.UTF8);
            MessageBox.Show("Xuất báo cáo thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private static string Csv(object? value)
        {
            string text = Convert.ToString(value, CultureInfo.InvariantCulture) ?? string.Empty;
            text = text.Replace("\"", "\"\"");
            return $"\"{text}\"";
        }

        private void BtnInLai_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_selectedMaHoaDon) || _dgvHoaDonMaster.CurrentRow is null)
            {
                MessageBox.Show("Vui lòng chọn hóa đơn cần in lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow row = _dgvHoaDonMaster.CurrentRow;
            decimal tongTien = Convert.ToDecimal(row.Cells["TongTien"].Value ?? 0m, CultureInfo.InvariantCulture);
            DateTime thoiGian = Convert.ToDateTime(row.Cells["ThoiGian"].Value, CultureInfo.InvariantCulture);
            _printMaHd = Convert.ToString(row.Cells["MaHD"].Value) ?? string.Empty;
            _printNhanVien = Convert.ToString(row.Cells["NguoiThucHien"].Value) ?? "-";
            _printDoiTac = Convert.ToString(row.Cells["DoiTac"].Value) ?? "-";
            _printThoiGian = thoiGian;
            _printTongTien = tongTien;

            string qrPayload;
            if (_currentInvoiceType == "NHAP")
            {
                qrPayload = BuildNhapKhoQrId(_printMaHd, _printThoiGian);
            }
            else
            {
                string vnPayCode = BuildVnPayCode(_printMaHd, tongTien, thoiGian);
                qrPayload = BuildVnPayQrPayload(vnPayCode, tongTien, thoiGian);
            }

            _printQrImage?.Dispose();
            _printQrImage = TryCreateQrImage(qrPayload, 170);

            _printContent = string.Empty;

            PrintDocument doc = new PrintDocument();
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

            bool isNhap = _currentInvoiceType == "NHAP";

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
                DrawCentered(isNhap ? "Mã xác thực nhập kho" : "Quét mã để thanh toán", normalFont);

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

        private void btn_QLNCC_Click(object? sender, EventArgs e)
        {
            AdminNavigationManager.Navigate<QuanLiNhaCungCap>(this);
        }

        private void btn_QLKH_Click(object? sender, EventArgs e)
        {
            AdminNavigationManager.Navigate<QuanLiKhachHang>(this);
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

        private void btn_ThongKe_Click(object? sender, EventArgs e)
        {
            AdminNavigationManager.Navigate<ThongKe>(this);
        }

        private void btn_DangXuat_Click(object? sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất?", "Xác nhận đăng xuất", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes)
            {
                return;
            }
            AdminNavigationManager.Logout(this);
        }

        private void _txtTimMaHD_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
