using PBL3.DataBase;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Net;
using System.Net.Mail;

namespace PBL3
{
    public partial class TrangQuenMatKhau : Form
    {
        private string _currentOtp = string.Empty;
        private DateTime _otpExpireAt = DateTime.MinValue;
        private int _countdown = 60;
        private string _currentAccount = string.Empty;
        private string _currentEmail = string.Empty;
        private readonly System.Windows.Forms.Timer _otpTimer = new System.Windows.Forms.Timer();

        public TrangQuenMatKhau()
        {
            InitializeComponent();
            Load += TrangQuenMatKhau_Load;
        }

        private void TrangQuenMatKhau_Load(object? sender, EventArgs e)
        {
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Khôi phục mật khẩu";
            ShowStep(1);

            _otpTimer.Interval = 1000;
            _otpTimer.Tick += OtpTimer_Tick;
            FormClosed += (_, _) =>
            {
                _otpTimer.Stop();
                _otpTimer.Dispose();
            };
        }

        private void ShowStep(int step)
        {
            pnlStep1.Visible = step == 1;
            pnlStep2.Visible = step == 2;
            pnlStep3.Visible = step == 3;
        }

        private void BtnSendOtp_Click(object? sender, EventArgs e)
        {
            string account = txtTaiKhoanEmail.Text.Trim();
            if (string.IsNullOrWhiteSpace(account))
            {
                MessageBox.Show("Vui lòng nhập Mã nhân viên hoặc Email.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!TryResolveEmployeeEmail(account, out string email, out string error))
            {
                MessageBox.Show(error, "Không tìm thấy", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _currentAccount = account;
            _currentEmail = email;

            if (!SendOtpMail(email, out string sendError))
            {
                MessageBox.Show(sendError, "Lỗi gửi mail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            StartCountdown();
            ShowStep(2);
        }

        private void BtnResend_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_currentEmail)) return;
            if (!SendOtpMail(_currentEmail, out string sendError))
            {
                MessageBox.Show(sendError, "Lỗi gửi mail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            StartCountdown();
        }

        private void BtnConfirmOtp_Click(object? sender, EventArgs e)
        {
            if (DateTime.Now > _otpExpireAt)
            {
                MessageBox.Show("Mã OTP đã hết hạn. Vui lòng gửi lại mã.", "Hết hạn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtOtp.Text.Trim() != _currentOtp)
            {
                MessageBox.Show("Mã OTP không đúng.", "Sai mã", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ShowStep(3);
        }

        private void BtnFinish_Click(object? sender, EventArgs e)
        {
            string p1 = txtMatKhauMoi.Text;
            string p2 = txtNhapLaiMatKhau.Text;

            if (string.IsNullOrWhiteSpace(p1) || string.IsNullOrWhiteSpace(p2))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ mật khẩu.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (p1 != p2)
            {
                MessageBox.Show("Mật khẩu nhập lại không khớp.", "Không khớp", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using SqlConnection conn = DbHelper.GetConnection();
                conn.Open();

                string? emailCol = DetectFirstColumn(conn, "NHAN_VIEN", "Email", "Gmail", "Mail");
                string sql;
                if (!string.IsNullOrWhiteSpace(emailCol) && _currentAccount.Contains("@"))
                {
                    sql = $"UPDATE dbo.NHAN_VIEN SET MatKhau=@MatKhau WHERE [{emailCol}] = @Account";
                }
                else
                {
                    sql = "UPDATE dbo.NHAN_VIEN SET MatKhau=@MatKhau WHERE MaNV = @Account";
                }

                using SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add("@MatKhau", SqlDbType.NVarChar, 100).Value = p1;
                cmd.Parameters.Add("@Account", SqlDbType.NVarChar, 100).Value = _currentAccount;

                int rows = cmd.ExecuteNonQuery();
                if (rows == 0)
                {
                    MessageBox.Show("Không cập nhật được mật khẩu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                MessageBox.Show("Đặt lại mật khẩu thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Hide();
                new TrangDangNhap().Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi cập nhật mật khẩu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OtpTimer_Tick(object? sender, EventArgs e)
        {
            _countdown--;
            if (_countdown <= 0)
            {
                _otpTimer.Stop();
                lblDemNguoc.Text = "Bạn có thể gửi lại mã.";
                btnGuiLaiMa.Enabled = true;
                return;
            }
            lblDemNguoc.Text = $"Gửi lại mã sau ({_countdown}s)";
        }

        private void StartCountdown()
        {
            _countdown = 60;
            btnGuiLaiMa.Enabled = false;
            lblDemNguoc.Text = "Gửi lại mã sau (60s)";
            _otpTimer.Start();
        }

        private bool TryResolveEmployeeEmail(string account, out string email, out string error)
        {
            email = string.Empty;
            error = "Không tìm thấy tài khoản.";
            try
            {
                using SqlConnection conn = DbHelper.GetConnection();
                conn.Open();

                string? emailCol = DetectFirstColumn(conn, "NHAN_VIEN", "Email", "Gmail", "Mail");
                if (string.IsNullOrWhiteSpace(emailCol))
                {
                    error = "Bảng NHAN_VIEN chưa có cột Email.";
                    return false;
                }

                string sql = account.Contains("@")
                    ? $"SELECT TOP 1 [{emailCol}] FROM dbo.NHAN_VIEN WHERE [{emailCol}] = @Acc"
                    : $"SELECT TOP 1 [{emailCol}] FROM dbo.NHAN_VIEN WHERE MaNV = @Acc";

                using SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add("@Acc", SqlDbType.NVarChar, 100).Value = account;
                object? result = cmd.ExecuteScalar();
                email = Convert.ToString(result)?.Trim() ?? string.Empty;
                if (string.IsNullOrWhiteSpace(email))
                {
                    error = "Không tìm thấy email của tài khoản này.";
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                error = "Lỗi kiểm tra tài khoản: " + ex.Message;
                return false;
            }
        }

        private bool SendOtpMail(string emailNhanVien, out string error)
        {
            error = string.Empty;
            try
            {
                _currentOtp = RandomNumberGenerator.GetInt32(100000, 1000000).ToString();
                _otpExpireAt = DateTime.Now.AddMinutes(5);

                using var client = new SmtpClient("sandbox.smtp.mailtrap.io", 2525)
                {
                    Credentials = new NetworkCredential("f50cc688cb17f7", "603792ce63be31"),
                    EnableSsl = true
                };

                MailMessage mail = new MailMessage
                {
                    From = new MailAddress("admin@gmail.com", "Hệ quản lí cửa hàng thức ăn nhanh"),
                    Subject = "[Admin] Mã xác nhận khôi phục mật khẩu",
                    Body = $@"Chào bạn,

Chúng tôi nhận được yêu cầu đặt lại mật khẩu của bạn.
Mã OTP của bạn là: {_currentOtp}

Mã này có hiệu lực trong 5 phút. Vui lòng không cung cấp cho người khác."
                };
                mail.To.Add(emailNhanVien);

                client.Send(mail);
                MessageBox.Show("Mã OTP đã được gửi. Vui lòng kiểm tra Mailtrap.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        private static string? DetectFirstColumn(SqlConnection conn, string tableName, params string[] candidates)
        {
            foreach (string candidate in candidates)
            {
                using SqlCommand cmd = new SqlCommand(@"SELECT CASE WHEN EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='dbo' AND TABLE_NAME=@TableName AND COLUMN_NAME=@Col) THEN 1 ELSE 0 END", conn);
                cmd.Parameters.AddWithValue("@TableName", tableName);
                cmd.Parameters.AddWithValue("@Col", candidate);
                object? res = cmd.ExecuteScalar();
                if (res is not null && Convert.ToInt32(res) == 1)
                    return candidate;
            }
            return null;
        }

        private void btn_QuayLai_Click(object sender, EventArgs e)
        {
            Hide();
            TrangDangNhap f = new TrangDangNhap();
            f.Show();
        }

        private void btn_DangKy_Click(object sender, EventArgs e)
        {
            BtnSendOtp_Click(sender, e);
        }

        private void dtp_NgaySinh_ValueChanged(object sender, EventArgs e)
        {
        }

        private void RegisterButton_MouseEnter(object sender, EventArgs e)
        {
        }

        private void RegisterButton_MouseLeave(object sender, EventArgs e)
        {
        }

        private void lbl_SoDienThoai_Click(object sender, EventArgs e)
        {

        }

        private void btnGuiMaXacNhan_Click(object sender, EventArgs e)
        {
            BtnSendOtp_Click(sender, e);
        }

        private void btnXacNhanOtp_Click(object sender, EventArgs e)
        {
            BtnConfirmOtp_Click(sender, e);
        }

        private void btnGuiLaiMa_Click(object sender, EventArgs e)
        {
            BtnResend_Click(sender, e);
        }

        private void btnHoanTat_Click(object sender, EventArgs e)
        {
            BtnFinish_Click(sender, e);
        }

        private void txtTaiKhoanEmail_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
