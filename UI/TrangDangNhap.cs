using PBL3.DataBase;
using System.Data.SqlClient;

namespace PBL3
{
    public partial class TrangDangNhap : Form
    {
        private bool _isLoggingIn;

        public TrangDangNhap()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
            if (sender is Label lbl)
            {
                // Exit the application when click the exit label
                if (lbl == lb_ChuaCoTK || lbl.Text.Equals("Exit", StringComparison.OrdinalIgnoreCase))
                {
                    Application.Exit();
                    return;
                }

                // Open forgot-password when clicking the "Quên mật khẩu" label
                if (lbl == lb_QuenMK || lbl.Text.Contains("Quên"))
                {
                    this.Hide();
                    TrangQuenMatKhau f = new TrangQuenMatKhau();
                    f.Show();
                    return;
                }

                // Fallback: originally used for "Đăng ký ngay"
                if (lbl.Text == "Đăng ký ngay")
                {
                    // keep previous behavior if registration label exists
                    this.Hide();
                    TrangQuenMatKhau f = new TrangQuenMatKhau();
                    f.Show();
                }
            }
        }

        private void roundedPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
        private void btn_DangNhap_MouseEnter(object sender, EventArgs e)
        {
            btn_DangNhap.BackColor = Color.FromArgb(255, 69, 0);
        }

        private void btn_DangNhap_MouseLeave(object sender, EventArgs e)
        {
            btn_DangNhap.BackColor = Color.LightSalmon;
        }

        private void lb_QuenMK_MouseEnter(object sender, EventArgs e)
        {
            lb_QuenMK.ForeColor = Color.FromArgb(255, 69, 0);
        }

        private void lb_QuenMK_MouseLeave(object sender, EventArgs e)
        {
            lb_QuenMK.ForeColor = Color.LightSalmon;
        }

        private void lb_DangKi_MouseEnter(object sender, EventArgs e)
        {
            lb_ChuaCoTK.ForeColor = Color.FromArgb(255, 69, 0);
        }

        private void lb_DangKi_MouseLeave(object sender, EventArgs e)
        {
            lb_ChuaCoTK.ForeColor = Color.LightSalmon;
        }
        private void hcnt_KhungDangNhap_Paint(object sender, PaintEventArgs e)
        {

        }

        private void TrangDangNhap_Load(object sender, EventArgs e)
        {
            try
            {
                using SqlConnection conn = DbHelper.GetConnection();
                conn.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Kết nối database thất bại.\n{ex.Message}\n\nKiểm tra lại:\n1) SQL Server instance (.\\SQLEXPRESS) đã chạy\n2) DB QL_FASTFOOD đã tồn tại\n3) Chuỗi kết nối 'QL_FASTFOOD' trong DataBase/App.config.",
                    "Lỗi kết nối",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btn_DangNhap_Click(object sender, EventArgs e)
        {
            if (_isLoggingIn)
            {
                return;
            }

            string soDienThoai = txt_TaiKhoan.Text.Trim();
            string matKhau = txt_MatKhau.Text.Trim();

            if (string.IsNullOrEmpty(soDienThoai) || string.IsNullOrEmpty(matKhau))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ số điện thoại và mật khẩu!",
                                "Thông báo",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            _isLoggingIn = true;
            btn_DangNhap.Enabled = false;
            lb_DangNhap.Enabled = false;

            try
            {
                using (SqlConnection conn = DbHelper.GetConnection())
                {
                    conn.Open();

                    string query = @"
                    SELECT nv.MaNV, nv.MaCV, cv.TenCV
                    FROM dbo.NHAN_VIEN nv
                    LEFT JOIN dbo.CHUC_VU cv ON cv.MaCV = nv.MaCV
                    WHERE nv.SDT = @sdt AND nv.MatKhau = @mk";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@sdt", soDienThoai);
                        cmd.Parameters.AddWithValue("@mk", matKhau);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string maChucVu = reader["MaCV"]?.ToString()?.Trim() ?? string.Empty;
                                string maNV = reader["MaNV"]?.ToString()?.Trim() ?? string.Empty;
                                bool laAdmin = maChucVu == "6" || maChucVu.Equals("CV06", StringComparison.OrdinalIgnoreCase);

                                Form target = laAdmin
                                    ? new QuanLiNhanVien()
                                    : new TrangNhanVien1(maNV);

                                this.Hide();
                                target.Show();

                                return;
                            }
                            else
                            {
                                MessageBox.Show("Sai số điện thoại hoặc mật khẩu!",
                                                "Lỗi",
                                                MessageBoxButtons.OK,
                                                MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            finally
            {
                if (!IsDisposed)
                {
                    _isLoggingIn = false;
                    btn_DangNhap.Enabled = true;
                    lb_DangNhap.Enabled = true;
                }
            }
        }
    }
}


