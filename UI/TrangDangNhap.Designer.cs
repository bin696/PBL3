using PBL3.UI;

namespace PBL3
{
    partial class TrangDangNhap
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            hcnt_KhungDangNhap = new RoundedPanel();
            lb_hoac = new Label();
            lb_MatKhau = new Label();
            lb_ChuaCoTK = new Label();
            lb_QuenMK = new Label();
            lb_TaiKhoan = new Label();
            lb_TieuDeHeThong = new Label();
            hcnt_MatKhau = new RoundedPanel();
            txt_MatKhau = new TextBox();
            btn_DangNhap = new RoundedPanel();
            lb_DangNhap = new Label();
            hcnt_TaiKhoan = new RoundedPanel();
            txt_TaiKhoan = new TextBox();
            lb_TieuDeDangNhap = new Label();
            hcnt_KhungDangNhap.SuspendLayout();
            hcnt_MatKhau.SuspendLayout();
            btn_DangNhap.SuspendLayout();
            hcnt_TaiKhoan.SuspendLayout();
            SuspendLayout();
            // 
            // hcnt_KhungDangNhap
            // 
            hcnt_KhungDangNhap.BackColor = SystemColors.ButtonHighlight;
            hcnt_KhungDangNhap.Controls.Add(lb_hoac);
            hcnt_KhungDangNhap.Controls.Add(lb_MatKhau);
            hcnt_KhungDangNhap.Controls.Add(lb_ChuaCoTK);
            hcnt_KhungDangNhap.Controls.Add(lb_QuenMK);
            hcnt_KhungDangNhap.Controls.Add(lb_TaiKhoan);
            hcnt_KhungDangNhap.Controls.Add(lb_TieuDeHeThong);
            hcnt_KhungDangNhap.Controls.Add(hcnt_MatKhau);
            hcnt_KhungDangNhap.Controls.Add(btn_DangNhap);
            hcnt_KhungDangNhap.Controls.Add(hcnt_TaiKhoan);
            hcnt_KhungDangNhap.Controls.Add(lb_TieuDeDangNhap);
            hcnt_KhungDangNhap.Location = new Point(315, 114);
            hcnt_KhungDangNhap.Name = "hcnt_KhungDangNhap";
            hcnt_KhungDangNhap.Size = new Size(481, 379);
            hcnt_KhungDangNhap.TabIndex = 0;
            hcnt_KhungDangNhap.Paint += hcnt_KhungDangNhap_Paint;
            // 
            // lb_hoac
            // 
            lb_hoac.AutoSize = true;
            lb_hoac.ForeColor = SystemColors.ControlDarkDark;
            lb_hoac.Location = new Point(127, 314);
            lb_hoac.Name = "lb_hoac";
            lb_hoac.Size = new Size(223, 15);
            lb_hoac.TabIndex = 4;
            lb_hoac.Text = "-------------------hoặc-------------------";
            // 
            // lb_MatKhau
            // 
            lb_MatKhau.AutoSize = true;
            lb_MatKhau.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lb_MatKhau.ForeColor = SystemColors.ControlDarkDark;
            lb_MatKhau.Location = new Point(36, 171);
            lb_MatKhau.Name = "lb_MatKhau";
            lb_MatKhau.Size = new Size(75, 19);
            lb_MatKhau.TabIndex = 3;
            lb_MatKhau.Text = "Mật khẩu:";
            lb_MatKhau.Click += label4_Click;
            // 
            // lb_ChuaCoTK
            // 
            lb_ChuaCoTK.AutoSize = true;
            lb_ChuaCoTK.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lb_ChuaCoTK.ForeColor = Color.Salmon;
            lb_ChuaCoTK.Location = new Point(228, 329);
            lb_ChuaCoTK.Name = "lb_ChuaCoTK";
            lb_ChuaCoTK.Size = new Size(44, 17);
            lb_ChuaCoTK.TabIndex = 1;
            lb_ChuaCoTK.Text = "Thoát";
            lb_ChuaCoTK.Click += label2_Click;
            // 
            // lb_QuenMK
            // 
            lb_QuenMK.AutoSize = true;
            lb_QuenMK.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lb_QuenMK.ForeColor = Color.Salmon;
            lb_QuenMK.Location = new Point(314, 238);
            lb_QuenMK.Name = "lb_QuenMK";
            lb_QuenMK.Size = new Size(116, 19);
            lb_QuenMK.TabIndex = 1;
            lb_QuenMK.Text = "Quên mật khẩu?";
            lb_QuenMK.Click += label2_Click;
            lb_QuenMK.MouseEnter += lb_QuenMK_MouseEnter;
            lb_QuenMK.MouseLeave += lb_QuenMK_MouseLeave;
            // 
            // lb_TaiKhoan
            // 
            lb_TaiKhoan.AutoSize = true;
            lb_TaiKhoan.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lb_TaiKhoan.ForeColor = SystemColors.ControlDarkDark;
            lb_TaiKhoan.Location = new Point(36, 108);
            lb_TaiKhoan.Name = "lb_TaiKhoan";
            lb_TaiKhoan.Size = new Size(77, 19);
            lb_TaiKhoan.TabIndex = 1;
            lb_TaiKhoan.Text = "Tài khoản:";
            lb_TaiKhoan.Click += label2_Click;
            // 
            // lb_TieuDeHeThong
            // 
            lb_TieuDeHeThong.AutoSize = true;
            lb_TieuDeHeThong.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lb_TieuDeHeThong.ForeColor = SystemColors.ControlDarkDark;
            lb_TieuDeHeThong.Location = new Point(91, 76);
            lb_TieuDeHeThong.Name = "lb_TieuDeHeThong";
            lb_TieuDeHeThong.Size = new Size(282, 19);
            lb_TieuDeHeThong.TabIndex = 1;
            lb_TieuDeHeThong.Text = "Hệ thống quản lí cửa hàng thức ăn nhanh";
            lb_TieuDeHeThong.Click += label2_Click;
            // 
            // hcnt_MatKhau
            // 
            hcnt_MatKhau.BackColor = SystemColors.ButtonFace;
            hcnt_MatKhau.Controls.Add(txt_MatKhau);
            hcnt_MatKhau.Location = new Point(36, 193);
            hcnt_MatKhau.Name = "hcnt_MatKhau";
            hcnt_MatKhau.Size = new Size(408, 42);
            hcnt_MatKhau.TabIndex = 2;
            hcnt_MatKhau.Paint += roundedPanel2_Paint;
            // 
            // txt_MatKhau
            // 
            txt_MatKhau.BackColor = SystemColors.ButtonFace;
            txt_MatKhau.BorderStyle = BorderStyle.None;
            txt_MatKhau.Font = new Font("Segoe UI", 12F);
            txt_MatKhau.ForeColor = SystemColors.ActiveCaptionText;
            txt_MatKhau.Location = new Point(20, 9);
            txt_MatKhau.Name = "txt_MatKhau";
            txt_MatKhau.Size = new Size(369, 22);
            txt_MatKhau.TabIndex = 0;
            // 
            // btn_DangNhap
            // 
            btn_DangNhap.BackColor = Color.LightSalmon;
            btn_DangNhap.Controls.Add(lb_DangNhap);
            btn_DangNhap.Location = new Point(127, 269);
            btn_DangNhap.Name = "btn_DangNhap";
            btn_DangNhap.Size = new Size(223, 42);
            btn_DangNhap.TabIndex = 2;
            btn_DangNhap.Click += btn_DangNhap_Click;
            btn_DangNhap.Paint += roundedPanel2_Paint;
            btn_DangNhap.MouseEnter += btn_DangNhap_MouseEnter;
            btn_DangNhap.MouseLeave += btn_DangNhap_MouseLeave;
            // 
            // lb_DangNhap
            // 
            lb_DangNhap.AutoSize = true;
            lb_DangNhap.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lb_DangNhap.ForeColor = Color.White;
            lb_DangNhap.Location = new Point(70, 11);
            lb_DangNhap.Name = "lb_DangNhap";
            lb_DangNhap.Size = new Size(81, 19);
            lb_DangNhap.TabIndex = 1;
            lb_DangNhap.Text = "Đăng nhập";
            lb_DangNhap.Click += btn_DangNhap_Click;
            lb_DangNhap.MouseEnter += btn_DangNhap_MouseEnter;
            lb_DangNhap.MouseLeave += btn_DangNhap_MouseLeave;
            // 
            // hcnt_TaiKhoan
            // 
            hcnt_TaiKhoan.BackColor = SystemColors.ButtonFace;
            hcnt_TaiKhoan.Controls.Add(txt_TaiKhoan);
            hcnt_TaiKhoan.Location = new Point(36, 130);
            hcnt_TaiKhoan.Name = "hcnt_TaiKhoan";
            hcnt_TaiKhoan.Size = new Size(408, 42);
            hcnt_TaiKhoan.TabIndex = 2;
            hcnt_TaiKhoan.Paint += roundedPanel2_Paint;
            // 
            // txt_TaiKhoan
            // 
            txt_TaiKhoan.BackColor = SystemColors.ButtonFace;
            txt_TaiKhoan.BorderStyle = BorderStyle.None;
            txt_TaiKhoan.Font = new Font("Segoe UI", 12F);
            txt_TaiKhoan.ForeColor = SystemColors.ActiveCaptionText;
            txt_TaiKhoan.Location = new Point(20, 9);
            txt_TaiKhoan.Name = "txt_TaiKhoan";
            txt_TaiKhoan.Size = new Size(369, 22);
            txt_TaiKhoan.TabIndex = 0;
            // 
            // lb_TieuDeDangNhap
            // 
            lb_TieuDeDangNhap.AutoSize = true;
            lb_TieuDeDangNhap.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lb_TieuDeDangNhap.Location = new Point(153, 31);
            lb_TieuDeDangNhap.Name = "lb_TieuDeDangNhap";
            lb_TieuDeDangNhap.Size = new Size(161, 37);
            lb_TieuDeDangNhap.TabIndex = 0;
            lb_TieuDeDangNhap.Text = "Đăng Nhập";
            // 
            // TrangDangNhap
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.mt;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1101, 628);
            Controls.Add(hcnt_KhungDangNhap);
            DoubleBuffered = true;
            Name = "TrangDangNhap";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "TrangDangNhap";
            Load += TrangDangNhap_Load;
            hcnt_KhungDangNhap.ResumeLayout(false);
            hcnt_KhungDangNhap.PerformLayout();
            hcnt_MatKhau.ResumeLayout(false);
            hcnt_MatKhau.PerformLayout();
            btn_DangNhap.ResumeLayout(false);
            btn_DangNhap.PerformLayout();
            hcnt_TaiKhoan.ResumeLayout(false);
            hcnt_TaiKhoan.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private RoundedPanel hcnt_KhungDangNhap;
        private Label lb_TieuDeDangNhap;
        private Label lb_TieuDeHeThong;
        private RoundedPanel hcnt_TaiKhoan;
        private TextBox txt_TaiKhoan;
        private RoundedPanel hcnt_MatKhau;
        private TextBox txt_MatKhau;
        private Label lb_MatKhau;
        private Label lb_TaiKhoan;
        private RoundedPanel btn_DangNhap;
        private Label lb_hoac;
        private Label lb_ChuaCoTK;
        private Label lb_DangNhap;
        private Label lb_QuenMK;
    }
}