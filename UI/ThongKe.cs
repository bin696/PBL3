using System.Windows.Forms;
using System;
using System.Data;
using System.Globalization;
using PBL3.DataBase;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;

namespace PBL3
{
    public partial class ThongKe : Form
    {
        private Chart? _chartRevenue;
        private Chart? _chartOrdersByHour;
        private Chart? _chartCategoryShare;

        public ThongKe()
        {
            InitializeComponent();
            WireActionButtons();
            cboDateFilter.SelectedIndex = 2;
            // start with custom date pickers disabled
            dtpFrom.Enabled = false;
            dtpTo.Enabled = false;
            InitializeCharts();
            ApplyDashboardStyles();
            lblTotalRevenue.Text = "Tổng doanh thu: 0 đ";
            lblTotalProfit.Text = "Tổng lợi nhuận: 0 đ";
            lblTotalOrders.Text = "Tổng số đơn: 0";
            lblNewCustomers.Text = "Khách mới: 0";
            this.Load += ThongKe_Load;
        }

        private void WireActionButtons()
        {
            btnRefresh.Click += btnRefresh_Click;
            label1.Click += btnRefresh_Click;

            roundedPanel2.Click += btnExportCsv_Click;
            label2.Click += btnExportCsv_Click;
        }

        private void InitializeCharts()
        {
            _chartRevenue = new Chart
            {
                Location = dgvRevenue.Location,
                Size = dgvRevenue.Size,
                BackColor = Color.White,
                BorderlineWidth = 0
            };
            _chartRevenue.ChartAreas.Clear();
            _chartRevenue.Series.Clear();
            _chartRevenue.ChartAreas.Add(new ChartArea("RevenueArea"));
            _chartRevenue.Series.Add(new Series("DoanhThu") { ChartType = SeriesChartType.Line, BorderWidth = 3, XValueType = ChartValueType.Date });

            _chartOrdersByHour = new Chart
            {
                Location = dgvOrdersByHour.Location,
                Size = dgvOrdersByHour.Size,
                BackColor = Color.White,
                BorderlineWidth = 0
            };
            _chartOrdersByHour.ChartAreas.Clear();
            _chartOrdersByHour.Series.Clear();
            _chartOrdersByHour.ChartAreas.Add(new ChartArea("HourArea"));
            _chartOrdersByHour.Series.Add(new Series("SoDon") { ChartType = SeriesChartType.Column, BorderWidth = 2 });

            _chartCategoryShare = new Chart
            {
                Location = dgvCategoryShare.Location,
                Size = dgvCategoryShare.Size,
                BackColor = Color.White,
                BorderlineWidth = 0
            };
            _chartCategoryShare.ChartAreas.Clear();
            _chartCategoryShare.Series.Clear();
            _chartCategoryShare.ChartAreas.Add(new ChartArea("CategoryArea"));
            _chartCategoryShare.Series.Add(new Series("Nhom") { ChartType = SeriesChartType.Pie, BorderWidth = 1 });

            hcnt_Khung.Controls.Add(_chartRevenue);
            hcnt_Khung.Controls.Add(_chartOrdersByHour);
            hcnt_Khung.Controls.Add(_chartCategoryShare);
            _chartRevenue.BringToFront();
            _chartOrdersByHour.BringToFront();
            _chartCategoryShare.BringToFront();

            // Remove designer placeholders khỏi giao diện runtime, chỉ giữ để map vị trí trong code
            hcnt_Khung.Controls.Remove(dgvRevenue);
            hcnt_Khung.Controls.Remove(dgvOrdersByHour);
            hcnt_Khung.Controls.Remove(dgvCategoryShare);
        }

        private void ApplyDashboardStyles()
        {
            lblTotalRevenue.AutoSize = true;
            lblTotalProfit.AutoSize = true;
            lblTotalOrders.AutoSize = true;
            lblNewCustomers.AutoSize = true;
            lblTotalRevenue.TextAlign = ContentAlignment.MiddleCenter;
            lblTotalProfit.TextAlign = ContentAlignment.MiddleCenter;
            lblTotalOrders.TextAlign = ContentAlignment.MiddleCenter;
            lblNewCustomers.TextAlign = ContentAlignment.MiddleCenter;
            lblTotalRevenue.ForeColor = Color.Black;
            lblTotalProfit.ForeColor = Color.Black;
            lblTotalOrders.ForeColor = Color.Black;
            lblNewCustomers.ForeColor = Color.Black;

            dgvTopItems.BackgroundColor = Color.White;
            dgvTopItems.BorderStyle = BorderStyle.None;
            dgvCategoryShare.BackgroundColor = Color.White;
            dgvCategoryShare.BorderStyle = BorderStyle.None;
            dgvLowStock.BackgroundColor = Color.White;
            dgvLowStock.BorderStyle = BorderStyle.None;
            dgvRevenue.RowHeadersVisible = false;
            dgvOrdersByHour.RowHeadersVisible = false;
            dgvCategoryShare.RowHeadersVisible = false;
            dgvTopItems.RowHeadersVisible = false;
            dgvLowStock.RowHeadersVisible = false;

            label1.Text = "Làm mới";
            label2.Text = "Xuất CSV";

            ArrangeSummaryLabels();

            // always keep summary labels visible above other controls
            lblTotalRevenue.Visible = lblTotalProfit.Visible = lblTotalOrders.Visible = lblNewCustomers.Visible = true;
            lblTotalRevenue.BringToFront();
            lblTotalProfit.BringToFront();
            lblTotalOrders.BringToFront();
            lblNewCustomers.BringToFront();
        }

        private void ArrangeSummaryLabels()
        {
            int y = 44;
            int x = 40;
            int gap = 24;

            lblTotalRevenue.Location = new Point(x, y);
            x += lblTotalRevenue.Width + gap;

            lblTotalProfit.Location = new Point(x, y);
            x += lblTotalProfit.Width + gap;

            lblTotalOrders.Location = new Point(x, y);
            x += lblTotalOrders.Width + gap;

            lblNewCustomers.Location = new Point(x, y);
        }
        private void ThongKe_Load(object? sender, EventArgs e)
        {
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
                return;
            LoadDashboard();
        }
        private void cboDateFilter_SelectedIndexChanged(object? sender, EventArgs e)
        {
            // enable/disable custom date pickers (robust to encoding/culture differences)
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
            {
                dtpFrom.Enabled = dtpTo.Enabled = false;
                return;
            }

            bool custom = false;
            try
            {
                if (cboDateFilter.SelectedIndex >= 0 && cboDateFilter.SelectedIndex == 4)
                {
                    custom = true;
                }
                else
                {
                    var s = (cboDateFilter.SelectedItem?.ToString() ?? string.Empty).ToLowerInvariant();
                    if (s.Contains("tùy") || s.Contains("tuy") || s.Contains("tùy chỉnh") || s.Contains("tuy chinh"))
                    {
                        custom = true;
                    }
                }
            }
            catch
            {
                custom = false;
            }

            dtpFrom.Enabled = dtpTo.Enabled = custom;
        }

        private void btnRefresh_Click(object? sender, EventArgs e)
        {
            if (dtpFrom.Value.Date > dtpTo.Value.Date)
            {
                MessageBox.Show("Ngày bắt đầu không được lớn hơn ngày kết thúc.", "Dữ liệu sai", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpTo.Value = dtpFrom.Value.Date;
            }

            LoadDashboard();
        }

        private void btnExportCsv_Click(object? sender, EventArgs e)
        {
            try
            {
                string fn = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), $"ThongKe_{DateTime.Now:yyyyMMddHHmm}.csv");
                using StreamWriter sw = new StreamWriter(fn, false, new System.Text.UTF8Encoding(true));
                sw.WriteLine("Metric,Value");
                sw.WriteLine($"FromDate,{dtpFrom.Value:yyyy-MM-dd}");
                sw.WriteLine($"ToDate,{dtpTo.Value:yyyy-MM-dd}");
                sw.WriteLine($"TotalRevenue,{ExtractNumberFromLabel(lblTotalRevenue.Text)}");
                sw.WriteLine($"TotalProfit,{ExtractNumberFromLabel(lblTotalProfit.Text)}");
                sw.WriteLine($"TotalOrders,{ExtractNumberFromLabel(lblTotalOrders.Text)}");
                sw.WriteLine($"NewCustomers,{ExtractNumberFromLabel(lblNewCustomers.Text)}");
                MessageBox.Show($"Xuất CSV thành công: {fn}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"L?i xu?t CSV: {ex.Message}", "L?i", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static string ExtractNumberFromLabel(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return "0";
            }

            var match = System.Text.RegularExpressions.Regex.Match(text, @"-?[\d\.,]+");
            if (!match.Success)
            {
                return "0";
            }

            string raw = match.Value.Replace(".", string.Empty).Replace(",", string.Empty);
            return long.TryParse(raw, out long n) ? n.ToString(CultureInfo.InvariantCulture) : "0";
        }

        private void LoadDashboard()
        {
            // compute date range by SelectedIndex to avoid text/encoding mismatch
            DateTime from = DateTime.Today;
            DateTime to = DateTime.Today.AddDays(1).AddTicks(-1);

            switch (cboDateFilter.SelectedIndex)
            {
                case 1: // Hôm qua
                    from = DateTime.Today.AddDays(-1);
                    to = DateTime.Today.AddTicks(-1);
                    break;
                case 2: // 7 ngày qua (bao gồm hôm nay)
                    from = DateTime.Today.AddDays(-6);
                    to = DateTime.Today.AddDays(1).AddTicks(-1);
                    break;
                case 3: // Tháng này
                    from = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                    to = DateTime.Today.AddDays(1).AddTicks(-1);
                    break;
                case 4: // Tùy chỉnh
                    from = dtpFrom.Value.Date;
                    to = dtpTo.Value.Date.AddDays(1).AddTicks(-1);
                    break;
                default: // Hôm nay
                    from = DateTime.Today;
                    to = DateTime.Today.AddDays(1).AddTicks(-1);
                    break;
            }

            bool isTodayRange = from.Date == to.Date;
            string activeHoaDonFilter = BuildActiveHoaDonBanCondition(null);
            string activeHoaDonFilterWithAlias = BuildActiveHoaDonBanCondition("h");

            // Load total revenue
            try
            {
                using var conn = DbHelper.GetConnection();
                conn.Open();
                // Diagnostic checks: report basic row counts for key tables to help identify why dashboard is empty
                try
                {
                    var sb = new System.Text.StringBuilder();
                    var checks = new (string name, string sql)[]
                    {
                        ("HOA_DON_BAN", "SELECT COUNT(1) FROM dbo.HOA_DON_BAN"),
                        ("CT_HOA_DON_BAN", "SELECT COUNT(1) FROM dbo.CT_HOA_DON_BAN"),
                        ("NGUYEN_LIEU", "SELECT COUNT(1) FROM dbo.NGUYEN_LIEU"),
                        ("MON_BAN", "SELECT COUNT(1) FROM dbo.MON_BAN")
                    };

                    foreach (var chk in checks)
                    {
                        try
                        {
                            using var ccmd = conn.CreateCommand();
                            ccmd.CommandText = chk.sql;
                            var r = ccmd.ExecuteScalar();
                            long countVal = r == null || r == DBNull.Value ? 0L : Convert.ToInt64(r);
                            sb.AppendLine($"{chk.name}: {countVal}");
                        }
                        catch (Exception e)
                        {
                            sb.AppendLine($"{chk.name}: ERROR ({e.Message})");
                        }
                    }

                    string diag = sb.ToString();
                    // If key tables are empty or any error occurred, show diagnostics so developer can act
                    bool anyZero = diag.Contains(": 0") || diag.Contains("ERROR");
                    if (anyZero)
                    {
                        MessageBox.Show($"Kiểm tra dữ liệuu:\n{diag}\nChú ý: nếu b?ng hóa ??n r?ng thì trang Th?ng kê s? không hi?n th? d? li?u.", "Ch?n ?oán Th?ng kê", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    // non-fatal diagnostic error
                    MessageBox.Show($"Lỗi kim tra ch?n ?oán: {ex.Message}", "Ch?n ?oán Th?ng kê", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                using var cmd = conn.CreateCommand();
                cmd.CommandText = $@"SELECT SUM(ISNULL(TongTien,0)) FROM dbo.HOA_DON_BAN WHERE NgayBan BETWEEN @from AND @to AND {activeHoaDonFilter}";
                cmd.Parameters.AddWithValue("@from", from);
                cmd.Parameters.AddWithValue("@to", to);
                var tot = cmd.ExecuteScalar();
                decimal revenueFromHoaDon = tot == DBNull.Value || tot == null ? 0m : Convert.ToDecimal(tot, CultureInfo.InvariantCulture);
                decimal totalRevenue = revenueFromHoaDon;
                // show applied date range for diagnostics
                try
                {
                    lblDateRange.Text = $"Khoảng: {from:dd/MM/yyyy} - {to:dd/MM/yyyy HH:mm:ss}";
                }
                catch { }

                // if stored TongTien is zero, try to compute revenue using only likely price columns
                string? detectedPriceCol = null;
                decimal altRev = 0m;
                if (totalRevenue == 0m)
                {
                    string[] ctPriceCandidates = { "DonGia", "DonGiaBan", "Gia", "GiaBan", "DonGiaCT", "DonGiaBanLe", "ThanhTien" };
                    foreach (var col in ctPriceCandidates)
                    {
                        using var chk = conn.CreateCommand();
                        chk.CommandText = "SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'CT_HOA_DON_BAN' AND COLUMN_NAME = @col";
                        chk.Parameters.AddWithValue("@col", col);
                        if (chk.ExecuteScalar() == null) continue;

                        using var revCmd = conn.CreateCommand();
                        revCmd.CommandText = $@"SELECT SUM(ISNULL(ct.SoLuong,0) * ISNULL(ct.[{col}],0)) FROM dbo.CT_HOA_DON_BAN ct JOIN dbo.HOA_DON_BAN h ON ct.MaHDB = h.MaHDB WHERE h.NgayBan BETWEEN @from AND @to AND {activeHoaDonFilterWithAlias}";
                        revCmd.Parameters.AddWithValue("@from", from);
                        revCmd.Parameters.AddWithValue("@to", to);
                        var alt = revCmd.ExecuteScalar();
                        decimal tryRev = alt == DBNull.Value || alt == null ? 0m : Convert.ToDecimal(alt, CultureInfo.InvariantCulture);
                        if (tryRev > 0m)
                        {
                            detectedPriceCol = col;
                            altRev = tryRev;
                            totalRevenue = altRev;
                            break;
                        }
                    }

                    // if still not found, check MON_BAN for numeric price columns and join
                    if (totalRevenue == 0m)
                    {
                        string[] mbPriceCandidates = { "DonGia", "DonGiaBan", "Gia", "GiaBan", "GiaTien", "Price" };
                        foreach (var col in mbPriceCandidates)
                        {
                            using var chk2 = conn.CreateCommand();
                            chk2.CommandText = "SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'MON_BAN' AND COLUMN_NAME = @col";
                            chk2.Parameters.AddWithValue("@col", col);
                            if (chk2.ExecuteScalar() == null) continue;

                            using var revCmd2 = conn.CreateCommand();
                            revCmd2.CommandText = $@"SELECT SUM(ISNULL(ct.SoLuong,0) * ISNULL(ma.[{col}],0)) FROM dbo.CT_HOA_DON_BAN ct JOIN dbo.MON_BAN ma ON ct.MaMon = ma.MaMon JOIN dbo.HOA_DON_BAN h ON ct.MaHDB = h.MaHDB WHERE h.NgayBan BETWEEN @from AND @to AND {activeHoaDonFilterWithAlias}";
                            revCmd2.Parameters.AddWithValue("@from", from);
                            revCmd2.Parameters.AddWithValue("@to", to);
                            var alt2 = revCmd2.ExecuteScalar();
                            decimal tryRev2 = alt2 == DBNull.Value || alt2 == null ? 0m : Convert.ToDecimal(alt2, CultureInfo.InvariantCulture);
                            if (tryRev2 > 0m)
                            {
                                detectedPriceCol = col;
                                altRev = tryRev2;
                                totalRevenue = altRev;
                                break;
                            }
                        }
                    }
                }

                lblTotalRevenue.Text = $"Tổng doanh thu: {totalRevenue:N0} đ";

                // total orders
                cmd.Parameters.Clear();
                cmd.CommandText = $@"SELECT COUNT(1) FROM dbo.HOA_DON_BAN WHERE NgayBan BETWEEN @from AND @to AND {activeHoaDonFilter}";
                cmd.Parameters.AddWithValue("@from", from);
                cmd.Parameters.AddWithValue("@to", to);
                var cnt = cmd.ExecuteScalar();
                int totalOrders = cnt == DBNull.Value || cnt == null ? 0 : Convert.ToInt32(cnt);
                lblTotalOrders.Text = $"Tổng số đơn: {totalOrders}";

                // new customers: some schemas don't have a 'NgayTao' column — detect available date column
                cmd.Parameters.Clear();
                string? dateCol = null;
                string[] candidates = new[] { "NgayTao", "NgayDK", "NgayDangKy", "NgayDangKyKH", "NgayDangKyTaiKhoan" };
                foreach (string c in candidates)
                {
                    using var chk = conn.CreateCommand();
                    chk.CommandText = "SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'KHACH_HANG' AND COLUMN_NAME = @col";
                    chk.Parameters.AddWithValue("@col", c);
                    var exists = chk.ExecuteScalar();
                    if (exists != null)
                    {
                        dateCol = c;
                        break;
                    }
                }

                if (!string.IsNullOrEmpty(dateCol))
                {
                    cmd.CommandText = $"SELECT COUNT(1) FROM dbo.KHACH_HANG WHERE [{dateCol}] BETWEEN @from AND @to";
                    cmd.Parameters.AddWithValue("@from", from);
                    cmd.Parameters.AddWithValue("@to", to);
                    var nc = cmd.ExecuteScalar();
                    int newCus = nc == DBNull.Value || nc == null ? 0 : Convert.ToInt32(nc);
                    lblNewCustomers.Text = $"Khách mới: {newCus}";
                }
                else
                {
                    // fallback: nếu KHACH_HANG không có cột ngày đăng ký,
                    // coi khách mới là khách có lần mua đầu tiên trong khoảng thời gian lọc
                    cmd.CommandText = $@"SELECT COUNT(1)
FROM (
    SELECT h.MaKH, MIN(h.NgayBan) AS FirstBuyDate
    FROM dbo.HOA_DON_BAN h
    WHERE h.MaKH IS NOT NULL AND {activeHoaDonFilterWithAlias}
    GROUP BY h.MaKH
) x
WHERE x.FirstBuyDate BETWEEN @from AND @to";
                    cmd.Parameters.AddWithValue("@from", from);
                    cmd.Parameters.AddWithValue("@to", to);
                    var ncFallback = cmd.ExecuteScalar();
                    int newCusFallback = ncFallback == DBNull.Value || ncFallback == null ? 0 : Convert.ToInt32(ncFallback);
                    lblNewCustomers.Text = $"Khách mới: {newCusFallback}";
                }

                // revenue chart data: today -> by hour, otherwise by day
                cmd.Parameters.Clear();
                if (isTodayRange)
                {
                    cmd.CommandText = $@"SELECT DATEPART(hour, NgayBan) AS Gio, SUM(ISNULL(TongTien,0)) AS DoanhThu FROM dbo.HOA_DON_BAN WHERE NgayBan BETWEEN @from AND @to AND {activeHoaDonFilter} GROUP BY DATEPART(hour, NgayBan) ORDER BY Gio";
                }
                else
                {
                    cmd.CommandText = $@"SELECT CONVERT(date, NgayBan) AS Ngay, SUM(ISNULL(TongTien,0)) AS DoanhThu FROM dbo.HOA_DON_BAN WHERE NgayBan BETWEEN @from AND @to AND {activeHoaDonFilter} GROUP BY CONVERT(date, NgayBan) ORDER BY Ngay";
                }
                cmd.Parameters.AddWithValue("@from", from);
                cmd.Parameters.AddWithValue("@to", to);
                using var daRev = new System.Data.SqlClient.SqlDataAdapter(cmd as System.Data.SqlClient.SqlCommand);
                var dtRev = new DataTable();
                daRev.Fill(dtRev);
                dgvRevenue.DataSource = dtRev;
                if (dgvRevenue.Columns.Contains("DoanhThu"))
                {
                    dgvRevenue.Columns["DoanhThu"].DefaultCellStyle.Format = "N0";
                    dgvRevenue.Columns["DoanhThu"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
                BindRevenueChart(dtRev);

                // top items
                cmd.Parameters.Clear();
                // table in this DB is named MON_BAN (menu items), not MON_AN
                cmd.CommandText = $@"SELECT TOP 5 ma.MaMon, ma.TenMon, SUM(ct.SoLuong) AS SoLuong FROM dbo.CT_HOA_DON_BAN ct JOIN dbo.MON_BAN ma ON ct.MaMon = ma.MaMon JOIN dbo.HOA_DON_BAN h ON ct.MaHDB = h.MaHDB WHERE h.NgayBan BETWEEN @from AND @to AND {activeHoaDonFilterWithAlias} GROUP BY ma.MaMon, ma.TenMon ORDER BY SoLuong DESC";
                cmd.Parameters.AddWithValue("@from", from);
                cmd.Parameters.AddWithValue("@to", to);
                using var da = new System.Data.SqlClient.SqlDataAdapter(cmd as System.Data.SqlClient.SqlCommand);
                var dtTop = new DataTable();
                da.Fill(dtTop);
                dgvTopItems.DataSource = dtTop;
                if (dgvTopItems.Columns.Contains("MaMon"))
                    dgvTopItems.Columns["MaMon"].Visible = false;
                if (dgvTopItems.Columns.Contains("SoLuong"))
                {
                    dgvTopItems.Columns["SoLuong"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }

                // orders by hour
                cmd.Parameters.Clear();
                cmd.CommandText = $@"SELECT DATEPART(hour, NgayBan) AS Gio, COUNT(1) AS SoDon FROM dbo.HOA_DON_BAN WHERE NgayBan BETWEEN @from AND @to AND {activeHoaDonFilter} GROUP BY DATEPART(hour, NgayBan) ORDER BY Gio";
                cmd.Parameters.AddWithValue("@from", from);
                cmd.Parameters.AddWithValue("@to", to);
                using var daHour = new System.Data.SqlClient.SqlDataAdapter(cmd as System.Data.SqlClient.SqlCommand);
                var dtHour = new DataTable();
                daHour.Fill(dtHour);
                dgvOrdersByHour.DataSource = dtHour;
                if (dgvOrdersByHour.Columns.Contains("SoDon"))
                {
                    dgvOrdersByHour.Columns["SoDon"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
                BindOrdersByHourChart(dtHour);

                // category share (revenue by category)
                cmd.Parameters.Clear();
                // use MON_BAN as item table
                // detect available price column in CT_HOA_DON_BAN to avoid "Invalid column name 'DonGia'" errors
                string[] priceCandidates = new[] { "DonGia", "DonGiaBan", "Gia", "GiaBan", "DonGiaCT", "DonGiaBanLe" };
                string? priceCol = null;
                foreach (string c in priceCandidates)
                {
                    using var chk = conn.CreateCommand();
                    chk.CommandText = "SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'CT_HOA_DON_BAN' AND COLUMN_NAME = @col";
                    chk.Parameters.AddWithValue("@col", c);
                    var exists = chk.ExecuteScalar();
                    if (exists != null)
                    {
                        priceCol = c;
                        break;
                    }
                }

                if (!string.IsNullOrEmpty(priceCol))
                {
                    cmd.CommandText = $@"SELECT lm.TenLoai AS Nhom, SUM(ct.SoLuong * ISNULL(ct.[{priceCol}],0)) AS DoanhThu FROM dbo.CT_HOA_DON_BAN ct JOIN dbo.MON_BAN ma ON ct.MaMon = ma.MaMon JOIN dbo.LOAI_MON lm ON ma.MaLoai = lm.MaLoai JOIN dbo.HOA_DON_BAN h ON ct.MaHDB = h.MaHDB WHERE h.NgayBan BETWEEN @from AND @to AND {activeHoaDonFilterWithAlias} GROUP BY lm.TenLoai ORDER BY DoanhThu DESC";
                    cmd.Parameters.AddWithValue("@from", from);
                    cmd.Parameters.AddWithValue("@to", to);
                    using var daCat = new System.Data.SqlClient.SqlDataAdapter(cmd as System.Data.SqlClient.SqlCommand);
                    var dtCat = new DataTable();
                    daCat.Fill(dtCat);
                    dgvCategoryShare.DataSource = dtCat;
                    BindCategoryChart(dtCat);
                    if (dgvCategoryShare.Columns.Contains("DoanhThu"))
                    {
                        dgvCategoryShare.Columns["DoanhThu"].DefaultCellStyle.Format = "N0";
                        dgvCategoryShare.Columns["DoanhThu"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    }
                }
                else
                {
                    // fallback: distribute h.TongTien proportionally by item quantity within each order
                    cmd.CommandText = $@"
SELECT lm.TenLoai AS Nhom,
       SUM( (h.TongTien * CAST(ct.SoLuong AS decimal(18,4)) / NULLIF(o.TotalQty,1)) ) AS DoanhThu
FROM dbo.CT_HOA_DON_BAN ct
JOIN dbo.MON_BAN ma ON ct.MaMon = ma.MaMon
JOIN dbo.LOAI_MON lm ON ma.MaLoai = lm.MaLoai
JOIN dbo.HOA_DON_BAN h ON ct.MaHDB = h.MaHDB
JOIN (
    SELECT MaHDB, SUM(SoLuong) AS TotalQty
    FROM dbo.CT_HOA_DON_BAN
    GROUP BY MaHDB
) o ON ct.MaHDB = o.MaHDB
WHERE h.NgayBan BETWEEN @from AND @to
  AND {activeHoaDonFilterWithAlias}
GROUP BY lm.TenLoai
ORDER BY DoanhThu DESC";
                    cmd.Parameters.AddWithValue("@from", from);
                    cmd.Parameters.AddWithValue("@to", to);
                    using var daCat2 = new System.Data.SqlClient.SqlDataAdapter(cmd as System.Data.SqlClient.SqlCommand);
                    var dtCat2 = new DataTable();
                    daCat2.Fill(dtCat2);
                    dgvCategoryShare.DataSource = dtCat2;
                    BindCategoryChart(dtCat2);
                }

                // low stock
                cmd.Parameters.Clear();
                cmd.CommandText = @"SELECT MaNL, TenNL, SoLuongTon, NguongToiThieu FROM dbo.NGUYEN_LIEU WHERE SoLuongTon <= NguongToiThieu";
                using var da2 = new System.Data.SqlClient.SqlDataAdapter(cmd as System.Data.SqlClient.SqlCommand);
                var dtLow = new DataTable();
                da2.Fill(dtLow);
                dgvLowStock.DataSource = dtLow;
                if (dgvLowStock.Columns.Contains("MaNL"))
                    dgvLowStock.Columns["MaNL"].Visible = false;
                if (dgvLowStock.Columns.Contains("SoLuongTon"))
                    dgvLowStock.Columns["SoLuongTon"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                if (dgvLowStock.Columns.Contains("NguongToiThieu"))
                    dgvLowStock.Columns["NguongToiThieu"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                HighlightLowStockRows();

                // compute profit on sold items in selected range: SUM((sale price - cost) * qty)
                cmd.Parameters.Clear();
                string revenueFromSoldExpr = ResolveRevenueFromSoldExpression(conn);
                cmd.CommandText = $@"
WITH CostPerMon AS (
    SELECT dm.MaMon, SUM(dm.SoLuongSuDung * ISNULL(nl.GiaNhap,0)) AS CostPerMon
    FROM dbo.DINH_MUC_MON dm
    JOIN dbo.NGUYEN_LIEU nl ON dm.MaNL = nl.MaNL
    GROUP BY dm.MaMon
)
SELECT
    SUM({revenueFromSoldExpr}) AS RevFromSold,
    SUM(ct.SoLuong * ISNULL(c.CostPerMon,0)) AS CostFromSold
FROM dbo.CT_HOA_DON_BAN ct
JOIN dbo.HOA_DON_BAN h ON ct.MaHDB = h.MaHDB
JOIN dbo.MON_BAN ma ON ct.MaMon = ma.MaMon
LEFT JOIN CostPerMon c ON ct.MaMon = c.MaMon
WHERE h.NgayBan BETWEEN @from AND @to
  AND {activeHoaDonFilterWithAlias}
";
                cmd.Parameters.AddWithValue("@from", from);
                cmd.Parameters.AddWithValue("@to", to);
                decimal totalCost = 0m;
                decimal revenueForProfit = totalRevenue;
                using (var pr = cmd.ExecuteReader())
                {
                    if (pr.Read())
                    {
                        revenueForProfit = pr.IsDBNull(0) ? 0m : Convert.ToDecimal(pr.GetValue(0), CultureInfo.InvariantCulture);
                        totalCost = pr.IsDBNull(1) ? 0m : Convert.ToDecimal(pr.GetValue(1), CultureInfo.InvariantCulture);
                    }
                }
                if (revenueForProfit <= 0m && totalRevenue > 0m)
                {
                    // fallback to already computed revenue in case detailed price columns are unavailable
                    revenueForProfit = totalRevenue;
                }
                if (totalRevenue == 0m && revenueForProfit > 0m)
                {
                    totalRevenue = revenueForProfit;
                    lblTotalRevenue.Text = $"Tổng doanh thu: {totalRevenue:N0} đ";
                }
                decimal profit = revenueForProfit - totalCost;
                lblTotalProfit.Text = $"Tổng lợi nhuận: {profit:N0} đ";
                ArrangeSummaryLabels();

                // debug MessageBox removed
            }
            catch (Exception ex)
            {
                lblTotalRevenue.Text = "Tổng doanh thu: 0 đ";
                lblTotalProfit.Text = "Tổng lợi nhuận: 0 đ";
                ArrangeSummaryLabels();
                MessageBox.Show($"L?i t?i d? li?u th?ng kê: {ex.Message}", "L?i", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private void btn_QLHDN_Click(object? sender, EventArgs e)
        {
            AdminNavigationManager.Navigate<QuanLiNguyenLieu>(this);
        }

        private void btn_QLHDB_Click(object? sender, EventArgs e)
        {
            AdminNavigationManager.Navigate<LichSuHoaDon>(this);
        }

        private void btn_DangXuat_MouseEnter(object? sender, EventArgs e)
        {
            btn_DangXuat.BackColor = Color.FromArgb(255, 69, 0);
        }

        private void btn_DangXuat_MouseLeave(object? sender, EventArgs e)
        {
            btn_DangXuat.BackColor = Color.LightSalmon;
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

        private void btn_ThongKe_Click(object? sender, EventArgs e)
        {

        }

        private void HighlightLowStockRows()
        {
            foreach (DataGridViewRow row in dgvLowStock.Rows)
            {
                if (row.IsNewRow) continue;
                decimal ton = 0m;
                decimal nguong = 0m;
                if (row.Cells["SoLuongTon"]?.Value != null && row.Cells["SoLuongTon"].Value != DBNull.Value)
                    ton = Convert.ToDecimal(row.Cells["SoLuongTon"].Value, CultureInfo.InvariantCulture);
                if (row.Cells["NguongToiThieu"]?.Value != null && row.Cells["NguongToiThieu"].Value != DBNull.Value)
                    nguong = Convert.ToDecimal(row.Cells["NguongToiThieu"].Value, CultureInfo.InvariantCulture);
                if (ton <= nguong)
                {
                    row.DefaultCellStyle.BackColor = Color.LightYellow;
                    row.DefaultCellStyle.ForeColor = Color.DarkRed;
                }
            }
        }

        private void BindRevenueChart(DataTable dt)
        {
            if (_chartRevenue == null) return;
            var series = _chartRevenue.Series[0];
            series.Points.Clear();
            bool byHour = dt.Columns.Contains("Gio");
            series.XValueType = byHour ? ChartValueType.Int32 : ChartValueType.String;
            foreach (DataRow row in dt.Rows)
            {
                decimal revenue = row["DoanhThu"] == DBNull.Value ? 0m : Convert.ToDecimal(row["DoanhThu"], CultureInfo.InvariantCulture);
                if (byHour)
                {
                    int hour = row["Gio"] == DBNull.Value ? 0 : Convert.ToInt32(row["Gio"]);
                    series.Points.AddXY(hour, revenue);
                }
                else
                {
                    DateTime day = Convert.ToDateTime(row["Ngay"]);
                    series.Points.AddXY(day.Day.ToString(CultureInfo.InvariantCulture), revenue);
                }
            }
            _chartRevenue.ChartAreas[0].AxisX.Interval = 1;
            _chartRevenue.ChartAreas[0].AxisX.LabelStyle.Format = byHour ? "0h" : string.Empty;
            _chartRevenue.ChartAreas[0].AxisY.LabelStyle.Format = "#,##0 đ";
        }

        private string ResolveRevenueFromSoldExpression(SqlConnection conn)
        {
            string[] ctPriceCandidates = { "DonGia", "DonGiaBan", "Gia", "GiaBan", "DonGiaCT", "DonGiaBanLe", "ThanhTien" };
            foreach (var c in ctPriceCandidates)
            {
                if (ColumnExists(conn, "CT_HOA_DON_BAN", c))
                {
                    // ThanhTien is usually line total, do not multiply by quantity again
                    if (string.Equals(c, "ThanhTien", StringComparison.OrdinalIgnoreCase))
                        return "ISNULL(ct.[ThanhTien],0)";
                    return $"(ISNULL(ct.SoLuong,0) * ISNULL(ct.[{c}],0))";
                }
            }

            string[] monPriceCandidates = { "DonGia", "DonGiaBan", "Gia", "GiaBan", "GiaTien", "Price" };
            foreach (var c in monPriceCandidates)
            {
                if (ColumnExists(conn, "MON_BAN", c))
                    return $"(ISNULL(ct.SoLuong,0) * ISNULL(ma.[{c}],0))";
            }

            return "0";
        }

        private static bool ColumnExists(SqlConnection conn, string tableName, string columnName)
        {
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = @table AND COLUMN_NAME = @col";
            cmd.Parameters.AddWithValue("@table", tableName);
            cmd.Parameters.AddWithValue("@col", columnName);
            return cmd.ExecuteScalar() != null;
        }

        private static string GetColumnDataType(SqlConnection conn, string tableName, string columnName)
        {
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = @table AND COLUMN_NAME = @col";
            cmd.Parameters.AddWithValue("@table", tableName);
            cmd.Parameters.AddWithValue("@col", columnName);
            return Convert.ToString(cmd.ExecuteScalar())?.Trim().ToLowerInvariant() ?? string.Empty;
        }

        private static string BuildActiveHoaDonBanCondition(string? alias)
        {
            using var conn = DbHelper.GetConnection();
            conn.Open();

            if (!ColumnExists(conn, "HOA_DON_BAN", "TrangThai"))
            {
                return "1 = 1";
            }

            string dataType = GetColumnDataType(conn, "HOA_DON_BAN", "TrangThai");
            string prefix = string.IsNullOrWhiteSpace(alias) ? string.Empty : $"{alias}.";

            if (dataType == "bit")
            {
                return $"ISNULL({prefix}TrangThai, 1) = 1";
            }

            return $"CAST({prefix}TrangThai AS NVARCHAR(50)) NOT LIKE N'%hủy%'";
        }

        private void BindOrdersByHourChart(DataTable dt)
        {
            if (_chartOrdersByHour == null) return;
            var series = _chartOrdersByHour.Series[0];
            series.Points.Clear();
            foreach (DataRow row in dt.Rows)
            {
                int hour = row["Gio"] == DBNull.Value ? 0 : Convert.ToInt32(row["Gio"]);
                int count = row["SoDon"] == DBNull.Value ? 0 : Convert.ToInt32(row["SoDon"]);
                series.Points.AddXY(hour, count);
            }
            _chartOrdersByHour.ChartAreas[0].AxisX.Interval = 1;
            _chartOrdersByHour.ChartAreas[0].AxisY.LabelStyle.Format = "N0";
        }

        private void BindCategoryChart(DataTable dt)
        {
            if (_chartCategoryShare == null) return;
            var series = _chartCategoryShare.Series[0];
            series.Points.Clear();
            decimal total = 0m;
            foreach (DataRow row in dt.Rows)
            {
                string nhom = Convert.ToString(row["Nhom"]) ?? "Khác";
                decimal doanhThu = row["DoanhThu"] == DBNull.Value ? 0m : Convert.ToDecimal(row["DoanhThu"], CultureInfo.InvariantCulture);
                if (doanhThu > 0)
                {
                    int idx = series.Points.AddXY(nhom, doanhThu);
                    series.Points[idx].LegendText = nhom;
                    series.Points[idx].Label = "#PERCENT{P0}";
                    total += doanhThu;
                }
            }

            // tránh vùng trắng khi tất cả doanh thu nhóm = 0
            if (series.Points.Count == 0 || total <= 0)
            {
                int idx = series.Points.AddXY("Không có", 1);
                series.Points[idx].LegendText = "Không có";
            }

            _chartCategoryShare.Legends.Clear();
            _chartCategoryShare.Legends.Add(new Legend("Legend1"));
            _chartCategoryShare.Titles.Clear();
            _chartCategoryShare.Titles.Add("Tỉ trọng doanh thu theo nhóm");
        }

        private void lblHourTitle_Click(object sender, EventArgs e)
        {

        }
    }
}
