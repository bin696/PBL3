using PBL3.DataBase;
using System.Data;
using System.Data.SqlClient;

namespace PBL3
{
    public partial class HopThuYeuCauForm : Form
    {
        private readonly List<YeuCauItem> _items = new();
        private YeuCauItem? _selected;

        public HopThuYeuCauForm()
        {
            InitializeComponent();
            _cboLoai.SelectedIndexChanged += (_, __) => LoadRequests();
            _cboTrangThai.SelectedIndexChanged += (_, __) => LoadRequests();
            _btnDuyet.Click += (_, __) => ApproveSelected();
            _btnTuChoi.Click += (_, __) => RejectSelected();
            _btnXoa.Click += (_, __) => DeleteSelected();
            lblBtnThem.Click += (_, __) => ApproveSelected();
            label1.Click += (_, __) => RejectSelected();
            label2.Click += (_, __) => DeleteSelected();

            Load += (_, __) => LoadRequests();
        }

        private void LoadRequests()
        {
            try
            {
                _items.Clear();
                _flpCards.Controls.Clear();

                string loai = Convert.ToString(_cboLoai.SelectedItem) ?? "Tất cả";
                string tt = Convert.ToString(_cboTrangThai.SelectedItem) ?? "Tất cả";

                using SqlConnection conn = DbHelper.GetConnection();
                using SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = @"
SELECT yc.MaYeuCau, yc.MaNV, yc.LoaiYeuCau, yc.NoiDung, yc.NgayGui, yc.TuNgay, yc.DenNgay, yc.TrangThai, yc.PhanHoiAdmin,
       ISNULL(nv.HoTen, N'Không rõ') AS HoTen, ISNULL(cv.TenCV, N'') AS TenCV
FROM dbo.YEU_CAU yc
LEFT JOIN dbo.NHAN_VIEN nv ON TRY_CAST(nv.MaNV AS INT) = yc.MaNV
LEFT JOIN dbo.CHUC_VU cv ON cv.MaCV = nv.MaCV
WHERE (@Loai = N'Tất cả' OR yc.LoaiYeuCau LIKE @LoaiLike)
  AND (@TrangThai = -1 OR yc.TrangThai = @TrangThai)
ORDER BY CASE WHEN yc.TrangThai = 0 THEN 0 ELSE 1 END, yc.NgayGui DESC";

                cmd.Parameters.AddWithValue("@Loai", loai);
                cmd.Parameters.AddWithValue("@LoaiLike", loai == "Tất cả" ? "%" : $"%{loai}%");
                cmd.Parameters.AddWithValue("@TrangThai", tt switch
                {
                    "Chờ duyệt" => 0,
                    "Đã duyệt" => 1,
                    "Đã từ chối" => 2,
                    _ => -1
                });

                conn.Open();
                using var rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    var item = new YeuCauItem
                    {
                        MaYeuCau = rd.GetInt32(0),
                        MaNV = rd.GetInt32(1),
                        LoaiYeuCau = Convert.ToString(rd[2]) ?? string.Empty,
                        NoiDung = Convert.ToString(rd[3]) ?? string.Empty,
                        NgayGui = rd.IsDBNull(4) ? DateTime.Now : Convert.ToDateTime(rd[4]),
                        TuNgay = rd.IsDBNull(5) ? null : Convert.ToDateTime(rd[5]),
                        DenNgay = rd.IsDBNull(6) ? null : Convert.ToDateTime(rd[6]),
                        TrangThai = rd.IsDBNull(7) ? 0 : Convert.ToInt32(rd[7]),
                        PhanHoiAdmin = Convert.ToString(rd[8]) ?? string.Empty,
                        HoTen = Convert.ToString(rd[9]) ?? string.Empty,
                        TenCV = Convert.ToString(rd[10]) ?? string.Empty
                    };
                    _items.Add(item);
                    _flpCards.Controls.Add(BuildCard(item));
                }

                int pending = _items.Count(x => x.TrangThai == 0);
                Text = pending > 0 ? $"Hộp thư yêu cầu ({pending})" : "Hộp thư yêu cầu";

                if (_items.Count > 0)
                {
                    SelectItem(_items[0]);
                }
                else
                {
                    ClearDetail();
                }
            }
            catch (Exception ex)
            {
                ClearDetail();
                MessageBox.Show($"Không tải được danh sách yêu cầu.\n{ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Control BuildCard(YeuCauItem item)
        {
            var card = new Panel
            {
                Width = 210,
                Height = 80,
                Margin = new Padding(1),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Cursor = Cursors.Hand,
                Tag = item
            };

            var left = new Panel { Dock = DockStyle.Left, Width = 6, BackColor = GetTypeColor(item.LoaiYeuCau) };
            card.Controls.Add(left);

            var lblName = new Label { Left = 12, Top = 8, AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold), Text = $"{item.HoTen} (NV{item.MaNV})" };
            var lblLoai = new Label { Left = 12, Top = 30, AutoSize = true, ForeColor = GetTypeColor(item.LoaiYeuCau), Text = NormalizeLoaiYeuCau(item.LoaiYeuCau) };
            var lblTime = new Label { Left = 12, Top = 50, AutoSize = true, ForeColor = Color.DimGray, Text = FormatTime(item.NgayGui) };

            card.Controls.Add(lblName);
            card.Controls.Add(lblLoai);
            card.Controls.Add(lblTime);

            void click(object? s, EventArgs e) => SelectItem(item);
            card.Click += click;
            lblName.Click += click;
            lblLoai.Click += click;
            lblTime.Click += click;
            left.Click += click;

            return card;
        }

        private void SelectItem(YeuCauItem item)
        {
            _selected = item;
            _lblNhanVien.Text = $"Nhân viên: {item.HoTen} (NV{item.MaNV})";
            _lblChucVu.Text = $"Chức vụ: {item.TenCV}";
            _lblLoai.Text = $"Loại yêu cầu: {NormalizeLoaiYeuCau(item.LoaiYeuCau)}";
            _lblThoiGian.Text = $"Thời gian gửi: {FormatTime(item.NgayGui)}";
            _lblKhoangNgay.Text = item.TuNgay.HasValue && item.DenNgay.HasValue
                ? $"Thời gian nghỉ: {item.TuNgay:dd/MM/yyyy} - {item.DenNgay:dd/MM/yyyy} (Tổng {(item.DenNgay.Value - item.TuNgay.Value).Days + 1} ngày)"
                : "Thời gian nghỉ: -";
            _lblTrangThai.Text = $"Trạng thái: {StateText(item.TrangThai)}";
            _txtNoiDung.Text = item.NoiDung;
            _txtPhanHoi.Text = item.PhanHoiAdmin;

            _lblCaGanNhat.Text = "Ca gần nhất: -";
            LoadLatestShift(item.MaNV);
            LoadHistory(item.MaNV);
        }

        private void LoadLatestShift(int maNv)
        {
            try
            {
                using SqlConnection conn = DbHelper.GetConnection();
                using SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = @"
SELECT TOP 1 ct.TenCa, pc.NgayLam
FROM dbo.PHAN_CONG_CA pc
JOIN dbo.CA_TRUC ct ON ct.MaCa = pc.MaCa
WHERE TRY_CAST(pc.MaNV AS INT) = @MaNV
ORDER BY pc.NgayLam DESC";
                cmd.Parameters.AddWithValue("@MaNV", maNv);
                conn.Open();
                using var rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    string tenCa = Convert.ToString(rd[0]) ?? "";
                    DateTime ngay = rd.IsDBNull(1) ? DateTime.MinValue : Convert.ToDateTime(rd[1]);
                    _lblCaGanNhat.Text = $"Ca gần nhất: {tenCa} - {ngay:dd/MM/yyyy}";
                }
            }
            catch { }
        }

        private void LoadHistory(int maNv)
        {
            _lstHistory.Items.Clear();
            try
            {
                using SqlConnection conn = DbHelper.GetConnection();
                using SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = @"
SELECT TOP 20 LoaiYeuCau, NgayGui, TrangThai
FROM dbo.YEU_CAU
WHERE MaNV = @MaNV
ORDER BY NgayGui DESC";
                cmd.Parameters.AddWithValue("@MaNV", maNv);
                conn.Open();
                using var rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    string loai = Convert.ToString(rd[0]) ?? "";
                    DateTime ngay = rd.IsDBNull(1) ? DateTime.Now : Convert.ToDateTime(rd[1]);
                    int st = rd.IsDBNull(2) ? 0 : Convert.ToInt32(rd[2]);
                    _lstHistory.Items.Add($"{ngay:dd/MM/yyyy HH:mm} - {NormalizeLoaiYeuCau(loai)} - {StateText(st)}");
                }
            }
            catch { }
        }

        private static string NormalizeLoaiYeuCau(string loai)
        {
            if (string.IsNullOrWhiteSpace(loai)) return string.Empty;

            string value = loai.Trim();
            if (value.Contains("nghỉ phép", StringComparison.OrdinalIgnoreCase)) return "Nghỉ phép";
            if (value.Contains("nghỉ hẳn", StringComparison.OrdinalIgnoreCase) || value.Contains("nghi han", StringComparison.OrdinalIgnoreCase)) return "Nghỉ hẳn";
            return value;
        }

        private void ApproveSelected()
        {
            if (_selected is null) return;

            try
            {
                using SqlConnection conn = DbHelper.GetConnection();
                conn.Open();
                using SqlTransaction tran = conn.BeginTransaction();

                using (SqlCommand cmd = new SqlCommand("UPDATE dbo.YEU_CAU SET TrangThai = 1, PhanHoiAdmin = @PhanHoi WHERE MaYeuCau = @Id", conn, tran))
                {
                    cmd.Parameters.AddWithValue("@PhanHoi", _txtPhanHoi.Text.Trim());
                    cmd.Parameters.AddWithValue("@Id", _selected.MaYeuCau);
                    cmd.ExecuteNonQuery();
                }

                if (_selected.LoaiYeuCau.Contains("hẳn", StringComparison.OrdinalIgnoreCase) || _selected.LoaiYeuCau.Contains("han", StringComparison.OrdinalIgnoreCase))
                {
                    using SqlCommand cmdNv = new SqlCommand("UPDATE dbo.NHAN_VIEN SET TrangThai = N'Nghỉ việc' WHERE TRY_CAST(MaNV AS INT) = @MaNV", conn, tran);
                    cmdNv.Parameters.AddWithValue("@MaNV", _selected.MaNV);
                    cmdNv.ExecuteNonQuery();
                }

                tran.Commit();
                MessageBox.Show("Đã duyệt yêu cầu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadRequests();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Duyệt yêu cầu thất bại.\n{ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RejectSelected()
        {
            if (_selected is null) return;
            if (string.IsNullOrWhiteSpace(_txtPhanHoi.Text))
            {
                MessageBox.Show("Bạn phải nhập phản hồi khi từ chối.", "Thiếu dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using SqlConnection conn = DbHelper.GetConnection();
                using SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE dbo.YEU_CAU SET TrangThai = 2, PhanHoiAdmin = @PhanHoi WHERE MaYeuCau = @Id";
                cmd.Parameters.AddWithValue("@PhanHoi", _txtPhanHoi.Text.Trim());
                cmd.Parameters.AddWithValue("@Id", _selected.MaYeuCau);
                conn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Đã từ chối yêu cầu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadRequests();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Từ chối yêu cầu thất bại.\n{ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeleteSelected()
        {
            if (_selected is null) return;
            if (_selected.TrangThai == 0)
            {
                MessageBox.Show("Không nên xóa yêu cầu đang chờ duyệt.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Xóa yêu cầu này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                using SqlConnection conn = DbHelper.GetConnection();
                using SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "DELETE FROM dbo.YEU_CAU WHERE MaYeuCau = @Id";
                cmd.Parameters.AddWithValue("@Id", _selected.MaYeuCau);
                conn.Open();
                cmd.ExecuteNonQuery();
                LoadRequests();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Xóa yêu cầu thất bại.\n{ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearDetail()
        {
            _selected = null;
            _lblNhanVien.Text = "Nhân viên: -";
            _lblChucVu.Text = "Chức vụ: -";
            _lblCaGanNhat.Text = "Ca gần nhất: -";
            _lblLoai.Text = "Loại yêu cầu: -";
            _lblThoiGian.Text = "Thời gian gửi: -";
            _lblKhoangNgay.Text = "Thời gian nghỉ: -";
            _lblTrangThai.Text = "Trạng thái: -";
            _txtNoiDung.Clear();
            _txtPhanHoi.Clear();
            _lstHistory.Items.Clear();
        }

        private static Color GetTypeColor(string loai)
        {
            if (loai.Contains("hẳn", StringComparison.OrdinalIgnoreCase) || loai.Contains("han", StringComparison.OrdinalIgnoreCase))
                return Color.IndianRed;
            return Color.SeaGreen;
        }

        private static string StateText(int s) => s switch
        {
            1 => "✅ Đã duyệt",
            2 => "❌ Đã từ chối",
            _ => "⏳ Chờ duyệt"
        };

        private static string FormatTime(DateTime dt)
        {
            if (dt.Date == DateTime.Today)
                return $"Hôm nay, {dt:HH:mm}";
            if (dt.Date == DateTime.Today.AddDays(-1))
                return $"Hôm qua, {dt:HH:mm}";
            return dt.ToString("dd/MM/yyyy HH:mm");
        }

        private class YeuCauItem
        {
            public int MaYeuCau { get; set; }
            public int MaNV { get; set; }
            public string HoTen { get; set; } = string.Empty;
            public string TenCV { get; set; } = string.Empty;
            public string LoaiYeuCau { get; set; } = string.Empty;
            public string NoiDung { get; set; } = string.Empty;
            public DateTime NgayGui { get; set; }
            public DateTime? TuNgay { get; set; }
            public DateTime? DenNgay { get; set; }
            public int TrangThai { get; set; }
            public string PhanHoiAdmin { get; set; } = string.Empty;
        }

        private void lblLoaiFilter_Click(object sender, EventArgs e)
        {

        }
    }
}
