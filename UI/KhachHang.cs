using PBL3.DataBase;
using PBL3.UI;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace PBL3
{
    public partial class KhachHang : Form
    {
        private const int DiemMoiMocGiam = 10;
        private const int TienGiamMoiMoc = 10000;
        private const int NguongCongDiem = 100000;
        private const int DiemCongMoiNguong = 10;

        private readonly string _maNv;
        private bool _isNavigating;

        public KhachHang() : this("1")
        {
        }

        public KhachHang(string maNv)
        {
            _maNv = maNv;
            InitializeComponent();
        }

        private void btn_QLNV_Click(object? sender, EventArgs e) => OpenAndClose(new TrangNhanVien1(_maNv));
        private void btn_QLNCC_Click(object? sender, EventArgs e) => OpenAndClose(new TrangHoaDon(_maNv));
        private void btn_QLKH_Click(object? sender, EventArgs e) => OpenAndClose(new BanHang(_maNv));
        private void btn_QLMA_Click(object? sender, EventArgs e) => OpenAndClose(new MuaHang(_maNv));

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

        private void btnLamMoi_Click(object? sender, EventArgs e)
        {
            LoadKhachHang();
        }

        private void KhachHang_Load(object? sender, EventArgs e)
        {
            LoadKhachHang();
        }

        private void LoadKhachHang()
        {
            const string sql = @"
SELECT MaKH, SDT, ISNULL(DiemTichLuy,0) AS DiemTichLuy,
       (ISNULL(DiemTichLuy,0) / 10) * 10000 AS GiamGiaToiDa
FROM dbo.KHACH_HANG
ORDER BY MaKH";

            using SqlConnection conn = DbHelper.GetConnection();
            using SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dgvKhachHang.DataSource = dt;
            if (dgvKhachHang.Columns.Contains("GiamGiaToiDa"))
            {
                dgvKhachHang.Columns["GiamGiaToiDa"].HeaderText = "Giảm tối đa (đ)";
                dgvKhachHang.Columns["GiamGiaToiDa"].DefaultCellStyle.Format = "N0";
            }

            lblCongThuc.Text = $"Công thức: {DiemMoiMocGiam} điểm = {TienGiamMoiMoc:N0}đ giảm giá | Cộng {DiemCongMoiNguong} điểm mỗi {NguongCongDiem:N0}đ thanh toán";
        }

        private void btnThem_Click(object? sender, EventArgs e)
        {
            string sdt = txtSdt.Text.Trim();
            string ten = txtTen.Text.Trim();

            if (string.IsNullOrWhiteSpace(sdt) || !long.TryParse(sdt, out _))
            {
                MessageBox.Show("SĐT không hợp lệ.", "Thiếu dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (IsPhoneExists(sdt))
            {
                MessageBox.Show("SĐT đã tồn tại.", "Trùng dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using SqlConnection conn = DbHelper.GetConnection();
                conn.Open();

                if (string.IsNullOrWhiteSpace(ten))
                {
                    ten = $"Khách hàng {GetNextMaKh(conn)}";
                }

                bool hasIdentity;
                using (SqlCommand cmdCheck = new SqlCommand("SELECT CASE WHEN COLUMNPROPERTY(OBJECT_ID('dbo.KHACH_HANG'),'MaKH','IsIdentity') = 1 THEN 1 ELSE 0 END", conn))
                {
                    hasIdentity = Convert.ToInt32(cmdCheck.ExecuteScalar() ?? 0) == 1;
                }

                if (hasIdentity)
                {
                    using SqlCommand cmd = new SqlCommand("INSERT INTO dbo.KHACH_HANG (TenKH, SDT, DiemTichLuy) VALUES (@TenKH, @SDT, 0)", conn);
                    cmd.Parameters.Add("@TenKH", SqlDbType.NVarChar, 100).Value = ten;
                    cmd.Parameters.Add("@SDT", SqlDbType.VarChar, 20).Value = sdt;
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    int next = GetNextMaKh(conn);
                    using SqlCommand cmd = new SqlCommand("INSERT INTO dbo.KHACH_HANG (MaKH, TenKH, SDT, DiemTichLuy) VALUES (@MaKH, @TenKH, @SDT, 0)", conn);
                    cmd.Parameters.Add("@MaKH", SqlDbType.Int).Value = next;
                    cmd.Parameters.Add("@TenKH", SqlDbType.NVarChar, 100).Value = ten;
                    cmd.Parameters.Add("@SDT", SqlDbType.VarChar, 20).Value = sdt;
                    cmd.ExecuteNonQuery();
                }

                txtSdt.Clear();
                txtTen.Clear();
                LoadKhachHang();
                MessageBox.Show("Thêm khách hàng thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không thể thêm khách hàng.\n{ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static bool IsPhoneExists(string sdt)
        {
            using SqlConnection conn = DbHelper.GetConnection();
            using SqlCommand cmd = new SqlCommand("SELECT COUNT(1) FROM dbo.KHACH_HANG WHERE SDT = @SDT", conn);
            cmd.Parameters.Add("@SDT", SqlDbType.VarChar, 20).Value = sdt;
            conn.Open();
            return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
        }

        private static int GetNextMaKh(SqlConnection conn)
        {
            using SqlCommand cmd = new SqlCommand(@"SELECT ISNULL(MAX(TRY_CAST(CASE WHEN CONVERT(VARCHAR(20), MaKH) LIKE 'KH%' THEN SUBSTRING(CONVERT(VARCHAR(20), MaKH), 3, LEN(CONVERT(VARCHAR(20), MaKH)) - 2) ELSE CONVERT(VARCHAR(20), MaKH) END AS INT)), 0) + 1 FROM dbo.KHACH_HANG", conn);
            return Convert.ToInt32(cmd.ExecuteScalar());
        }
    }
}
