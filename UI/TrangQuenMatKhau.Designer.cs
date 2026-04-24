using PBL3.UI;

namespace PBL3
{
    partial class TrangQuenMatKhau
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            roundedPanel1 = new RoundedPanel();
            lblTieuDe = new Label();
            btn_QuayLai = new Button();
            pnlStep1 = new Panel();
            btnGuiMaXacNhan = new Button();
            txtTaiKhoanEmail = new TextBox();
            lblTaiKhoanEmail = new Label();
            pnlStep3 = new Panel();
            btnHoanTat = new Button();
            txtNhapLaiMatKhau = new TextBox();
            lblNhapLaiMatKhau = new Label();
            pnlStep2 = new Panel();
            btnXacNhanOtp = new Button();
            btnGuiLaiMa = new Button();
            lblDemNguoc = new Label();
            txtOtp = new TextBox();
            lblOtp = new Label();
            txtMatKhauMoi = new TextBox();
            lblMatKhauMoi = new Label();
            roundedPanel1.SuspendLayout();
            pnlStep1.SuspendLayout();
            pnlStep3.SuspendLayout();
            pnlStep2.SuspendLayout();
            SuspendLayout();
            // 
            // roundedPanel1
            // 
            roundedPanel1.BackColor = Color.White;
            roundedPanel1.Controls.Add(lblTieuDe);
            roundedPanel1.Controls.Add(btn_QuayLai);
            roundedPanel1.Controls.Add(pnlStep1);
            roundedPanel1.Controls.Add(pnlStep2);
            roundedPanel1.Controls.Add(pnlStep3);
            roundedPanel1.Location = new Point(27, 26);
            roundedPanel1.Margin = new Padding(3, 2, 3, 2);
            roundedPanel1.Name = "roundedPanel1";
            roundedPanel1.Size = new Size(387, 411);
            roundedPanel1.TabIndex = 0;
            // 
            // lblTieuDe
            // 
            lblTieuDe.AutoSize = true;
            lblTieuDe.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTieuDe.ForeColor = Color.DarkOrange;
            lblTieuDe.Location = new Point(101, 15);
            lblTieuDe.Name = "lblTieuDe";
            lblTieuDe.Size = new Size(199, 32);
            lblTieuDe.TabIndex = 0;
            lblTieuDe.Text = "Lấy lại mật khẩu";
            // 
            // btn_QuayLai
            // 
            btn_QuayLai.BackColor = Color.LightSalmon;
            btn_QuayLai.FlatAppearance.BorderSize = 0;
            btn_QuayLai.FlatStyle = FlatStyle.Flat;
            btn_QuayLai.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btn_QuayLai.ForeColor = Color.White;
            btn_QuayLai.Location = new Point(18, 15);
            btn_QuayLai.Margin = new Padding(3, 2, 3, 2);
            btn_QuayLai.Name = "btn_QuayLai";
            btn_QuayLai.Size = new Size(77, 26);
            btn_QuayLai.TabIndex = 1;
            btn_QuayLai.Text = "Quay lại";
            btn_QuayLai.UseVisualStyleBackColor = false;
            btn_QuayLai.Click += btn_QuayLai_Click;
            // 
            // pnlStep1
            // 
            pnlStep1.Controls.Add(btnGuiMaXacNhan);
            pnlStep1.Controls.Add(txtTaiKhoanEmail);
            pnlStep1.Controls.Add(lblTaiKhoanEmail);
            pnlStep1.Location = new Point(22, 71);
            pnlStep1.Margin = new Padding(3, 2, 3, 2);
            pnlStep1.Name = "pnlStep1";
            pnlStep1.Size = new Size(340, 320);
            pnlStep1.TabIndex = 2;
            // 
            // btnGuiMaXacNhan
            // 
            btnGuiMaXacNhan.BackColor = Color.LightSalmon;
            btnGuiMaXacNhan.FlatAppearance.BorderSize = 0;
            btnGuiMaXacNhan.FlatStyle = FlatStyle.Flat;
            btnGuiMaXacNhan.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnGuiMaXacNhan.ForeColor = Color.White;
            btnGuiMaXacNhan.Location = new Point(61, 181);
            btnGuiMaXacNhan.Margin = new Padding(3, 2, 3, 2);
            btnGuiMaXacNhan.Name = "btnGuiMaXacNhan";
            btnGuiMaXacNhan.Size = new Size(184, 34);
            btnGuiMaXacNhan.TabIndex = 2;
            btnGuiMaXacNhan.Text = "Gửi mã xác nhận";
            btnGuiMaXacNhan.UseVisualStyleBackColor = false;
            btnGuiMaXacNhan.Click += btnGuiMaXacNhan_Click;
            // 
            // txtTaiKhoanEmail
            // 
            txtTaiKhoanEmail.Font = new Font("Segoe UI", 11F);
            txtTaiKhoanEmail.Location = new Point(24, 122);
            txtTaiKhoanEmail.Margin = new Padding(3, 2, 3, 2);
            txtTaiKhoanEmail.Name = "txtTaiKhoanEmail";
            txtTaiKhoanEmail.PlaceholderText = "Mã NV hoặc Email";
            txtTaiKhoanEmail.Size = new Size(288, 27);
            txtTaiKhoanEmail.TabIndex = 1;
            // 
            // lblTaiKhoanEmail
            // 
            lblTaiKhoanEmail.AutoSize = true;
            lblTaiKhoanEmail.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTaiKhoanEmail.ForeColor = Color.DimGray;
            lblTaiKhoanEmail.Location = new Point(24, 94);
            lblTaiKhoanEmail.Name = "lblTaiKhoanEmail";
            lblTaiKhoanEmail.Size = new Size(129, 20);
            lblTaiKhoanEmail.TabIndex = 0;
            lblTaiKhoanEmail.Text = "Tài khoản / Email";
            // 
            // pnlStep3
            // 
            pnlStep3.Controls.Add(btnHoanTat);
            pnlStep3.Controls.Add(txtNhapLaiMatKhau);
            pnlStep3.Controls.Add(lblNhapLaiMatKhau);
            pnlStep3.Controls.Add(txtMatKhauMoi);
            pnlStep3.Controls.Add(lblMatKhauMoi);
            pnlStep3.Location = new Point(22, 71);
            pnlStep3.Margin = new Padding(3, 2, 3, 2);
            pnlStep3.Name = "pnlStep3";
            pnlStep3.Size = new Size(340, 320);
            pnlStep3.TabIndex = 4;
            // 
            // btnHoanTat
            // 
            btnHoanTat.BackColor = Color.LightSalmon;
            btnHoanTat.FlatAppearance.BorderSize = 0;
            btnHoanTat.FlatStyle = FlatStyle.Flat;
            btnHoanTat.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnHoanTat.ForeColor = Color.White;
            btnHoanTat.Location = new Point(83, 202);
            btnHoanTat.Margin = new Padding(3, 2, 3, 2);
            btnHoanTat.Name = "btnHoanTat";
            btnHoanTat.Size = new Size(184, 34);
            btnHoanTat.TabIndex = 4;
            btnHoanTat.Text = "Hoàn tất";
            btnHoanTat.UseVisualStyleBackColor = false;
            btnHoanTat.Click += btnHoanTat_Click;
            // 
            // txtNhapLaiMatKhau
            // 
            txtNhapLaiMatKhau.Font = new Font("Segoe UI", 11F);
            txtNhapLaiMatKhau.Location = new Point(50, 146);
            txtNhapLaiMatKhau.Margin = new Padding(3, 2, 3, 2);
            txtNhapLaiMatKhau.Name = "txtNhapLaiMatKhau";
            txtNhapLaiMatKhau.Size = new Size(254, 27);
            txtNhapLaiMatKhau.TabIndex = 3;
            txtNhapLaiMatKhau.UseSystemPasswordChar = true;
            // 
            // lblNhapLaiMatKhau
            // 
            lblNhapLaiMatKhau.AutoSize = true;
            lblNhapLaiMatKhau.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblNhapLaiMatKhau.ForeColor = Color.DimGray;
            lblNhapLaiMatKhau.Location = new Point(50, 123);
            lblNhapLaiMatKhau.Name = "lblNhapLaiMatKhau";
            lblNhapLaiMatKhau.Size = new Size(137, 20);
            lblNhapLaiMatKhau.TabIndex = 2;
            lblNhapLaiMatKhau.Text = "Nhập lại mật khẩu";
            // 
            // pnlStep2
            // 
            pnlStep2.Controls.Add(btnXacNhanOtp);
            pnlStep2.Controls.Add(btnGuiLaiMa);
            pnlStep2.Controls.Add(lblDemNguoc);
            pnlStep2.Controls.Add(txtOtp);
            pnlStep2.Controls.Add(lblOtp);
            pnlStep2.Location = new Point(22, 69);
            pnlStep2.Margin = new Padding(3, 2, 3, 2);
            pnlStep2.Name = "pnlStep2";
            pnlStep2.Size = new Size(340, 320);
            pnlStep2.TabIndex = 3;
            // 
            // btnXacNhanOtp
            // 
            btnXacNhanOtp.BackColor = Color.LightSalmon;
            btnXacNhanOtp.FlatAppearance.BorderSize = 0;
            btnXacNhanOtp.FlatStyle = FlatStyle.Flat;
            btnXacNhanOtp.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnXacNhanOtp.ForeColor = Color.White;
            btnXacNhanOtp.Location = new Point(79, 202);
            btnXacNhanOtp.Margin = new Padding(3, 2, 3, 2);
            btnXacNhanOtp.Name = "btnXacNhanOtp";
            btnXacNhanOtp.Size = new Size(184, 34);
            btnXacNhanOtp.TabIndex = 4;
            btnXacNhanOtp.Text = "Xác nhận mã";
            btnXacNhanOtp.UseVisualStyleBackColor = false;
            btnXacNhanOtp.Click += btnXacNhanOtp_Click;
            // 
            // btnGuiLaiMa
            // 
            btnGuiLaiMa.FlatStyle = FlatStyle.Flat;
            btnGuiLaiMa.ForeColor = Color.DarkOrange;
            btnGuiLaiMa.Location = new Point(232, 107);
            btnGuiLaiMa.Margin = new Padding(3, 2, 3, 2);
            btnGuiLaiMa.Name = "btnGuiLaiMa";
            btnGuiLaiMa.Size = new Size(83, 26);
            btnGuiLaiMa.TabIndex = 3;
            btnGuiLaiMa.Text = "Gửi lại mã";
            btnGuiLaiMa.UseVisualStyleBackColor = true;
            btnGuiLaiMa.Click += btnGuiLaiMa_Click;
            // 
            // lblDemNguoc
            // 
            lblDemNguoc.Font = new Font("Segoe UI", 9F);
            lblDemNguoc.ForeColor = Color.Gray;
            lblDemNguoc.Location = new Point(26, 135);
            lblDemNguoc.Name = "lblDemNguoc";
            lblDemNguoc.Size = new Size(289, 15);
            lblDemNguoc.TabIndex = 2;
            lblDemNguoc.Text = "Gửi lại mã sau (60s)";
            lblDemNguoc.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // txtOtp
            // 
            txtOtp.Font = new Font("Segoe UI", 11F);
            txtOtp.Location = new Point(29, 106);
            txtOtp.Margin = new Padding(3, 2, 3, 2);
            txtOtp.Name = "txtOtp";
            txtOtp.Size = new Size(194, 27);
            txtOtp.TabIndex = 1;
            // 
            // lblOtp
            // 
            lblOtp.AutoSize = true;
            lblOtp.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblOtp.ForeColor = Color.DimGray;
            lblOtp.Location = new Point(29, 76);
            lblOtp.Name = "lblOtp";
            lblOtp.Size = new Size(105, 20);
            lblOtp.TabIndex = 0;
            lblOtp.Text = "Nhập mã OTP";
            // 
            // txtMatKhauMoi
            // 
            txtMatKhauMoi.Font = new Font("Segoe UI", 11F);
            txtMatKhauMoi.Location = new Point(50, 80);
            txtMatKhauMoi.Margin = new Padding(3, 2, 3, 2);
            txtMatKhauMoi.Name = "txtMatKhauMoi";
            txtMatKhauMoi.Size = new Size(254, 27);
            txtMatKhauMoi.TabIndex = 1;
            txtMatKhauMoi.UseSystemPasswordChar = true;
            // 
            // lblMatKhauMoi
            // 
            lblMatKhauMoi.AutoSize = true;
            lblMatKhauMoi.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblMatKhauMoi.ForeColor = Color.DimGray;
            lblMatKhauMoi.Location = new Point(50, 57);
            lblMatKhauMoi.Name = "lblMatKhauMoi";
            lblMatKhauMoi.Size = new Size(106, 20);
            lblMatKhauMoi.TabIndex = 0;
            lblMatKhauMoi.Text = "Mật khẩu mới";
            // 
            // TrangQuenMatKhau
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.pbl;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(443, 459);
            Controls.Add(roundedPanel1);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(3, 2, 3, 2);
            MaximizeBox = false;
            Name = "TrangQuenMatKhau";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quên mật khẩu";
            roundedPanel1.ResumeLayout(false);
            roundedPanel1.PerformLayout();
            pnlStep1.ResumeLayout(false);
            pnlStep1.PerformLayout();
            pnlStep3.ResumeLayout(false);
            pnlStep3.PerformLayout();
            pnlStep2.ResumeLayout(false);
            pnlStep2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private RoundedPanel roundedPanel1;
        private Label lblTieuDe;
        private Button btn_QuayLai;
        private Panel pnlStep1;
        private Label lblTaiKhoanEmail;
        private TextBox txtTaiKhoanEmail;
        private Button btnGuiMaXacNhan;
        private Panel pnlStep2;
        private Label lblOtp;
        private TextBox txtOtp;
        private Label lblDemNguoc;
        private Button btnGuiLaiMa;
        private Button btnXacNhanOtp;
        private Panel pnlStep3;
        private Label lblMatKhauMoi;
        private TextBox txtMatKhauMoi;
        private Label lblNhapLaiMatKhau;
        private TextBox txtNhapLaiMatKhau;
        private Button btnHoanTat;
    }
}