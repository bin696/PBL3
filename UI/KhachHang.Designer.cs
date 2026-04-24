using PBL3.UI;

namespace PBL3
{
    partial class KhachHang
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            roundedPanel1 = new RoundedPanel();
            pb_Admin = new PictureBox();
            btn_DangXuat = new RoundedPanel();
            lb_DangXuat = new Label();
            lb_Admin = new Label();
            hcnt_Khung = new RoundedPanel();
            lblCongThuc = new Label();
            btnLamMoi = new Button();
            lblTitle = new Label();
            lblTen = new Label();
            lblSdt = new Label();
            btnThem = new Button();
            txtTen = new TextBox();
            txtSdt = new TextBox();
            dgvKhachHang = new DataGridView();
            hcnt_KhungMenuAD = new RoundedPanel();
            btn_QLHDN = new RoundedPanel();
            pb_QLHDN = new PictureBox();
            label8 = new Label();
            btn_QLMA = new RoundedPanel();
            pb_QLMA = new PictureBox();
            label7 = new Label();
            btn_QLKH = new RoundedPanel();
            pb_QLKH = new PictureBox();
            label6 = new Label();
            btn_QLNCC = new RoundedPanel();
            pb_QLNCC = new PictureBox();
            label5 = new Label();
            btn_QLNV = new RoundedPanel();
            pb_QLNV = new PictureBox();
            label4 = new Label();
            lb_DMQL = new Label();
            roundedPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pb_Admin).BeginInit();
            btn_DangXuat.SuspendLayout();
            hcnt_Khung.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvKhachHang).BeginInit();
            hcnt_KhungMenuAD.SuspendLayout();
            btn_QLHDN.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pb_QLHDN).BeginInit();
            btn_QLMA.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pb_QLMA).BeginInit();
            btn_QLKH.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pb_QLKH).BeginInit();
            btn_QLNCC.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pb_QLNCC).BeginInit();
            btn_QLNV.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pb_QLNV).BeginInit();
            SuspendLayout();
            // 
            // roundedPanel1
            // 
            roundedPanel1.Controls.Add(pb_Admin);
            roundedPanel1.Controls.Add(btn_DangXuat);
            roundedPanel1.Controls.Add(lb_Admin);
            roundedPanel1.Controls.Add(hcnt_Khung);
            roundedPanel1.Controls.Add(hcnt_KhungMenuAD);
            roundedPanel1.Location = new Point(14, 16);
            roundedPanel1.Name = "roundedPanel1";
            roundedPanel1.Size = new Size(1232, 811);
            roundedPanel1.TabIndex = 0;
            // 
            // pb_Admin
            // 
            pb_Admin.BackColor = SystemColors.Control;
            pb_Admin.Image = Properties.Resources.nhanvien;
            pb_Admin.Location = new Point(19, 11);
            pb_Admin.Name = "pb_Admin";
            pb_Admin.Size = new Size(51, 51);
            pb_Admin.SizeMode = PictureBoxSizeMode.StretchImage;
            pb_Admin.TabIndex = 2;
            pb_Admin.TabStop = false;
            // 
            // btn_DangXuat
            // 
            btn_DangXuat.BackColor = Color.SandyBrown;
            btn_DangXuat.Controls.Add(lb_DangXuat);
            btn_DangXuat.Location = new Point(194, 16);
            btn_DangXuat.Name = "btn_DangXuat";
            btn_DangXuat.Size = new Size(126, 33);
            btn_DangXuat.TabIndex = 3;
            btn_DangXuat.Click += btn_DangXuat_Click;
            // 
            // lb_DangXuat
            // 
            lb_DangXuat.AutoSize = true;
            lb_DangXuat.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lb_DangXuat.ForeColor = Color.White;
            lb_DangXuat.Location = new Point(18, 4);
            lb_DangXuat.Name = "lb_DangXuat";
            lb_DangXuat.Size = new Size(93, 23);
            lb_DangXuat.TabIndex = 0;
            lb_DangXuat.Text = "Đăng xuất";
            lb_DangXuat.Click += btn_DangXuat_Click;
            // 
            // lb_Admin
            // 
            lb_Admin.AutoSize = true;
            lb_Admin.Font = new Font("Segoe UI", 12F);
            lb_Admin.Location = new Point(66, 16);
            lb_Admin.Name = "lb_Admin";
            lb_Admin.Size = new Size(100, 28);
            lb_Admin.TabIndex = 2;
            lb_Admin.Text = "Nhân viên";
            // 
            // hcnt_Khung
            // 
            hcnt_Khung.BackColor = Color.Linen;
            hcnt_Khung.Controls.Add(lblCongThuc);
            hcnt_Khung.Controls.Add(btnLamMoi);
            hcnt_Khung.Controls.Add(lblTitle);
            hcnt_Khung.Controls.Add(lblTen);
            hcnt_Khung.Controls.Add(lblSdt);
            hcnt_Khung.Controls.Add(btnThem);
            hcnt_Khung.Controls.Add(txtTen);
            hcnt_Khung.Controls.Add(txtSdt);
            hcnt_Khung.Controls.Add(dgvKhachHang);
            hcnt_Khung.Location = new Point(342, 65);
            hcnt_Khung.Name = "hcnt_Khung";
            hcnt_Khung.Size = new Size(870, 717);
            hcnt_Khung.TabIndex = 1;
            // 
            // lblCongThuc
            // 
            lblCongThuc.AutoSize = true;
            lblCongThuc.Font = new Font("Segoe UI", 9F);
            lblCongThuc.ForeColor = Color.Firebrick;
            lblCongThuc.Location = new Point(24, 98);
            lblCongThuc.Name = "lblCongThuc";
            lblCongThuc.Size = new Size(529, 20);
            lblCongThuc.TabIndex = 7;
            lblCongThuc.Text = "Công thức: 1 điểm = 1.000đ giảm giá | Cộng 10 điểm mỗi 100.000đ thanh toán";
            // 
            // btnLamMoi
            // 
            btnLamMoi.BackColor = Color.Peru;
            btnLamMoi.FlatStyle = FlatStyle.Flat;
            btnLamMoi.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnLamMoi.ForeColor = Color.White;
            btnLamMoi.Location = new Point(687, 88);
            btnLamMoi.Name = "btnLamMoi";
            btnLamMoi.Size = new Size(159, 31);
            btnLamMoi.TabIndex = 8;
            btnLamMoi.Text = "Làm mới";
            btnLamMoi.UseVisualStyleBackColor = false;
            btnLamMoi.Click += btnLamMoi_Click;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTitle.ForeColor = Color.SaddleBrown;
            lblTitle.Location = new Point(24, 18);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(123, 28);
            lblTitle.TabIndex = 6;
            lblTitle.Text = "Khách hàng";
            // 
            // lblTen
            // 
            lblTen.AutoSize = true;
            lblTen.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTen.Location = new Point(299, 55);
            lblTen.Name = "lblTen";
            lblTen.Size = new Size(79, 20);
            lblTen.TabIndex = 5;
            lblTen.Text = "Tên khách";
            lblTen.Visible = false;
            // 
            // lblSdt
            // 
            lblSdt.AutoSize = true;
            lblSdt.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblSdt.Location = new Point(24, 55);
            lblSdt.Name = "lblSdt";
            lblSdt.Size = new Size(37, 20);
            lblSdt.TabIndex = 4;
            lblSdt.Text = "SĐT";
            lblSdt.Visible = false;
            // 
            // btnThem
            // 
            btnThem.BackColor = Color.SandyBrown;
            btnThem.FlatStyle = FlatStyle.Flat;
            btnThem.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnThem.ForeColor = Color.White;
            btnThem.Location = new Point(687, 50);
            btnThem.Name = "btnThem";
            btnThem.Size = new Size(159, 31);
            btnThem.TabIndex = 3;
            btnThem.Text = "Thêm khách hàng";
            btnThem.UseVisualStyleBackColor = false;
            btnThem.Click += btnThem_Click;
            btnThem.Visible = false;
            // 
            // txtTen
            // 
            txtTen.Location = new Point(384, 52);
            txtTen.Name = "txtTen";
            txtTen.PlaceholderText = "Để trống sẽ tự tạo";
            txtTen.Size = new Size(210, 27);
            txtTen.TabIndex = 2;
            txtTen.Visible = false;
            // 
            // txtSdt
            // 
            txtSdt.Location = new Point(67, 52);
            txtSdt.Name = "txtSdt";
            txtSdt.Size = new Size(180, 27);
            txtSdt.TabIndex = 1;
            txtSdt.Visible = false;
            // 
            // dgvKhachHang
            // 
            dgvKhachHang.AllowUserToAddRows = false;
            dgvKhachHang.AllowUserToDeleteRows = false;
            dgvKhachHang.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvKhachHang.BackgroundColor = Color.WhiteSmoke;
            dgvKhachHang.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvKhachHang.Location = new Point(24, 134);
            dgvKhachHang.MultiSelect = false;
            dgvKhachHang.Name = "dgvKhachHang";
            dgvKhachHang.ReadOnly = true;
            dgvKhachHang.RowHeadersVisible = false;
            dgvKhachHang.RowHeadersWidth = 51;
            dgvKhachHang.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvKhachHang.Size = new Size(822, 550);
            dgvKhachHang.TabIndex = 0;
            // 
            // hcnt_KhungMenuAD
            // 
            hcnt_KhungMenuAD.BackColor = Color.Linen;
            hcnt_KhungMenuAD.Controls.Add(btn_QLHDN);
            hcnt_KhungMenuAD.Controls.Add(btn_QLMA);
            hcnt_KhungMenuAD.Controls.Add(btn_QLKH);
            hcnt_KhungMenuAD.Controls.Add(btn_QLNCC);
            hcnt_KhungMenuAD.Controls.Add(btn_QLNV);
            hcnt_KhungMenuAD.Controls.Add(lb_DMQL);
            hcnt_KhungMenuAD.Location = new Point(19, 65);
            hcnt_KhungMenuAD.Name = "hcnt_KhungMenuAD";
            hcnt_KhungMenuAD.Size = new Size(301, 717);
            hcnt_KhungMenuAD.TabIndex = 0;
            // 
            // btn_QLHDN
            // 
            btn_QLHDN.BackColor = Color.Salmon;
            btn_QLHDN.Controls.Add(pb_QLHDN);
            btn_QLHDN.Controls.Add(label8);
            btn_QLHDN.Location = new Point(21, 371);
            btn_QLHDN.Name = "btn_QLHDN";
            btn_QLHDN.Size = new Size(256, 53);
            btn_QLHDN.TabIndex = 1;
            // 
            // pb_QLHDN
            // 
            pb_QLHDN.Image = Properties.Resources.khachhang;
            pb_QLHDN.Location = new Point(10, 0);
            pb_QLHDN.Name = "pb_QLHDN";
            pb_QLHDN.Size = new Size(51, 51);
            pb_QLHDN.SizeMode = PictureBoxSizeMode.StretchImage;
            pb_QLHDN.TabIndex = 2;
            pb_QLHDN.TabStop = false;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 12F);
            label8.ForeColor = Color.White;
            label8.Location = new Point(58, 11);
            label8.Name = "label8";
            label8.Size = new Size(114, 28);
            label8.TabIndex = 0;
            label8.Text = "Khách hàng";
            // 
            // btn_QLMA
            // 
            btn_QLMA.BackColor = Color.Bisque;
            btn_QLMA.Controls.Add(pb_QLMA);
            btn_QLMA.Controls.Add(label7);
            btn_QLMA.Location = new Point(21, 297);
            btn_QLMA.Name = "btn_QLMA";
            btn_QLMA.Size = new Size(256, 53);
            btn_QLMA.TabIndex = 1;
            btn_QLMA.Click += btn_QLMA_Click;
            // 
            // pb_QLMA
            // 
            pb_QLMA.Image = Properties.Resources.monan;
            pb_QLMA.Location = new Point(10, 0);
            pb_QLMA.Name = "pb_QLMA";
            pb_QLMA.Size = new Size(51, 51);
            pb_QLMA.SizeMode = PictureBoxSizeMode.StretchImage;
            pb_QLMA.TabIndex = 2;
            pb_QLMA.TabStop = false;
            pb_QLMA.Click += btn_QLMA_Click;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 12F);
            label7.Location = new Point(58, 11);
            label7.Name = "label7";
            label7.Size = new Size(100, 28);
            label7.TabIndex = 0;
            label7.Text = "Mua hàng";
            label7.Click += btn_QLMA_Click;
            // 
            // btn_QLKH
            // 
            btn_QLKH.BackColor = Color.Bisque;
            btn_QLKH.Controls.Add(pb_QLKH);
            btn_QLKH.Controls.Add(label6);
            btn_QLKH.Location = new Point(21, 223);
            btn_QLKH.Name = "btn_QLKH";
            btn_QLKH.Size = new Size(256, 53);
            btn_QLKH.TabIndex = 1;
            btn_QLKH.Click += btn_QLKH_Click;
            // 
            // pb_QLKH
            // 
            pb_QLKH.Image = Properties.Resources.khachhang;
            pb_QLKH.Location = new Point(10, 0);
            pb_QLKH.Name = "pb_QLKH";
            pb_QLKH.Size = new Size(51, 51);
            pb_QLKH.SizeMode = PictureBoxSizeMode.StretchImage;
            pb_QLKH.TabIndex = 2;
            pb_QLKH.TabStop = false;
            pb_QLKH.Click += btn_QLKH_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 12F);
            label6.Location = new Point(58, 11);
            label6.Name = "label6";
            label6.Size = new Size(93, 28);
            label6.TabIndex = 0;
            label6.Text = "Bán hàng";
            label6.Click += btn_QLKH_Click;
            // 
            // btn_QLNCC
            // 
            btn_QLNCC.BackColor = Color.Bisque;
            btn_QLNCC.Controls.Add(pb_QLNCC);
            btn_QLNCC.Controls.Add(label5);
            btn_QLNCC.Location = new Point(21, 149);
            btn_QLNCC.Name = "btn_QLNCC";
            btn_QLNCC.Size = new Size(256, 53);
            btn_QLNCC.TabIndex = 1;
            btn_QLNCC.Click += btn_QLNCC_Click;
            // 
            // pb_QLNCC
            // 
            pb_QLNCC.Image = Properties.Resources.ncc;
            pb_QLNCC.Location = new Point(10, 0);
            pb_QLNCC.Name = "pb_QLNCC";
            pb_QLNCC.Size = new Size(51, 51);
            pb_QLNCC.SizeMode = PictureBoxSizeMode.StretchImage;
            pb_QLNCC.TabIndex = 2;
            pb_QLNCC.TabStop = false;
            pb_QLNCC.Click += btn_QLNCC_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 12F);
            label5.Location = new Point(58, 11);
            label5.Name = "label5";
            label5.Size = new Size(88, 28);
            label5.TabIndex = 0;
            label5.Text = "Hóa đơn";
            label5.Click += btn_QLNCC_Click;
            // 
            // btn_QLNV
            // 
            btn_QLNV.BackColor = Color.Bisque;
            btn_QLNV.Controls.Add(pb_QLNV);
            btn_QLNV.Controls.Add(label4);
            btn_QLNV.Location = new Point(21, 76);
            btn_QLNV.Name = "btn_QLNV";
            btn_QLNV.Size = new Size(256, 53);
            btn_QLNV.TabIndex = 1;
            btn_QLNV.Click += btn_QLNV_Click;
            // 
            // pb_QLNV
            // 
            pb_QLNV.Image = Properties.Resources.nhanvien;
            pb_QLNV.Location = new Point(10, 0);
            pb_QLNV.Name = "pb_QLNV";
            pb_QLNV.Size = new Size(51, 51);
            pb_QLNV.SizeMode = PictureBoxSizeMode.StretchImage;
            pb_QLNV.TabIndex = 2;
            pb_QLNV.TabStop = false;
            pb_QLNV.Click += btn_QLNV_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 12F);
            label4.Location = new Point(58, 11);
            label4.Name = "label4";
            label4.Size = new Size(168, 28);
            label4.TabIndex = 0;
            label4.Text = "Thông tin cá nhân";
            label4.Click += btn_QLNV_Click;
            // 
            // lb_DMQL
            // 
            lb_DMQL.AutoSize = true;
            lb_DMQL.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            lb_DMQL.ForeColor = Color.Salmon;
            lb_DMQL.Location = new Point(19, 20);
            lb_DMQL.Name = "lb_DMQL";
            lb_DMQL.Size = new Size(260, 35);
            lb_DMQL.TabIndex = 0;
            lb_DMQL.Text = "Danh mục Nhân viên";
            // 
            // hcnt_KhungMenuAD
            // 
            hcnt_KhungMenuAD.BackColor = Color.Linen;
            hcnt_KhungMenuAD.Controls.Add(btn_QLHDN);
            hcnt_KhungMenuAD.Controls.Add(btn_QLMA);
            hcnt_KhungMenuAD.Controls.Add(btn_QLKH);
            hcnt_KhungMenuAD.Controls.Add(btn_QLNCC);
            hcnt_KhungMenuAD.Controls.Add(btn_QLNV);
            hcnt_KhungMenuAD.Controls.Add(lb_DMQL);
            hcnt_KhungMenuAD.Location = new Point(19, 65);
            hcnt_KhungMenuAD.Name = "hcnt_KhungMenuAD";
            hcnt_KhungMenuAD.Size = new Size(301, 717);
            hcnt_KhungMenuAD.TabIndex = 0;
            // 
            // btn_QLHDN
            // 
            btn_QLHDN.BackColor = Color.Salmon;
            btn_QLHDN.Controls.Add(pb_QLHDN);
            btn_QLHDN.Controls.Add(label8);
            btn_QLHDN.Location = new Point(21, 371);
            btn_QLHDN.Name = "btn_QLHDN";
            btn_QLHDN.Size = new Size(256, 53);
            btn_QLHDN.TabIndex = 1;
            // 
            // pb_QLHDN
            // 
            pb_QLHDN.Image = Properties.Resources.khachhang;
            pb_QLHDN.Location = new Point(10, 0);
            pb_QLHDN.Name = "pb_QLHDN";
            pb_QLHDN.Size = new Size(51, 51);
            pb_QLHDN.SizeMode = PictureBoxSizeMode.StretchImage;
            pb_QLHDN.TabIndex = 2;
            pb_QLHDN.TabStop = false;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 12F);
            label8.ForeColor = Color.White;
            label8.Location = new Point(58, 11);
            label8.Name = "label8";
            label8.Size = new Size(108, 28);
            label8.TabIndex = 0;
            label8.Text = "Khách hàng";
            // 
            // btn_QLMA
            // 
            btn_QLMA.BackColor = Color.Bisque;
            btn_QLMA.Controls.Add(pb_QLMA);
            btn_QLMA.Controls.Add(label7);
            btn_QLMA.Location = new Point(21, 297);
            btn_QLMA.Name = "btn_QLMA";
            btn_QLMA.Size = new Size(256, 53);
            btn_QLMA.TabIndex = 1;
            btn_QLMA.Click += btn_QLMA_Click;
            // 
            // pb_QLMA
            // 
            pb_QLMA.Image = Properties.Resources.monan;
            pb_QLMA.Location = new Point(10, 0);
            pb_QLMA.Name = "pb_QLMA";
            pb_QLMA.Size = new Size(51, 51);
            pb_QLMA.SizeMode = PictureBoxSizeMode.StretchImage;
            pb_QLMA.TabIndex = 2;
            pb_QLMA.TabStop = false;
            pb_QLMA.Click += btn_QLMA_Click;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 12F);
            label7.Location = new Point(58, 11);
            label7.Name = "label7";
            label7.Size = new Size(100, 28);
            label7.TabIndex = 0;
            label7.Text = "Mua hàng";
            label7.Click += btn_QLMA_Click;
            // 
            // btn_QLKH
            // 
            btn_QLKH.BackColor = Color.Bisque;
            btn_QLKH.Controls.Add(pb_QLKH);
            btn_QLKH.Controls.Add(label6);
            btn_QLKH.Location = new Point(21, 223);
            btn_QLKH.Name = "btn_QLKH";
            btn_QLKH.Size = new Size(256, 53);
            btn_QLKH.TabIndex = 1;
            btn_QLKH.Click += btn_QLKH_Click;
            // 
            // pb_QLKH
            // 
            pb_QLKH.Image = Properties.Resources.khachhang;
            pb_QLKH.Location = new Point(10, 0);
            pb_QLKH.Name = "pb_QLKH";
            pb_QLKH.Size = new Size(51, 51);
            pb_QLKH.SizeMode = PictureBoxSizeMode.StretchImage;
            pb_QLKH.TabIndex = 2;
            pb_QLKH.TabStop = false;
            pb_QLKH.Click += btn_QLKH_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 12F);
            label6.Location = new Point(58, 11);
            label6.Name = "label6";
            label6.Size = new Size(93, 28);
            label6.TabIndex = 0;
            label6.Text = "Bán hàng";
            label6.Click += btn_QLKH_Click;
            // 
            // btn_QLNCC
            // 
            btn_QLNCC.BackColor = Color.Bisque;
            btn_QLNCC.Controls.Add(pb_QLNCC);
            btn_QLNCC.Controls.Add(label5);
            btn_QLNCC.Location = new Point(21, 149);
            btn_QLNCC.Name = "btn_QLNCC";
            btn_QLNCC.Size = new Size(256, 53);
            btn_QLNCC.TabIndex = 1;
            btn_QLNCC.Click += btn_QLNCC_Click;
            // 
            // pb_QLNCC
            // 
            pb_QLNCC.Image = Properties.Resources.ncc;
            pb_QLNCC.Location = new Point(10, 0);
            pb_QLNCC.Name = "pb_QLNCC";
            pb_QLNCC.Size = new Size(51, 51);
            pb_QLNCC.SizeMode = PictureBoxSizeMode.StretchImage;
            pb_QLNCC.TabIndex = 2;
            pb_QLNCC.TabStop = false;
            pb_QLNCC.Click += btn_QLNCC_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 12F);
            label5.Location = new Point(58, 11);
            label5.Name = "label5";
            label5.Size = new Size(88, 28);
            label5.TabIndex = 0;
            label5.Text = "Hóa đơn";
            label5.Click += btn_QLNCC_Click;
            // 
            // btn_QLNV
            // 
            btn_QLNV.BackColor = Color.Bisque;
            btn_QLNV.Controls.Add(pb_QLNV);
            btn_QLNV.Controls.Add(label4);
            btn_QLNV.Location = new Point(21, 76);
            btn_QLNV.Name = "btn_QLNV";
            btn_QLNV.Size = new Size(256, 53);
            btn_QLNV.TabIndex = 1;
            btn_QLNV.Click += btn_QLNV_Click;
            // 
            // pb_QLNV
            // 
            pb_QLNV.Image = Properties.Resources.nhanvien;
            pb_QLNV.Location = new Point(10, 0);
            pb_QLNV.Name = "pb_QLNV";
            pb_QLNV.Size = new Size(51, 51);
            pb_QLNV.SizeMode = PictureBoxSizeMode.StretchImage;
            pb_QLNV.TabIndex = 2;
            pb_QLNV.TabStop = false;
            pb_QLNV.Click += btn_QLNV_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 12F);
            label4.Location = new Point(58, 11);
            label4.Name = "label4";
            label4.Size = new Size(168, 28);
            label4.TabIndex = 0;
            label4.Text = "Thông tin cá nhân";
            label4.Click += btn_QLNV_Click;
            // 
            // lb_DMQL
            // 
            lb_DMQL.AutoSize = true;
            lb_DMQL.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            lb_DMQL.ForeColor = Color.Salmon;
            lb_DMQL.Location = new Point(40, 12);
            lb_DMQL.Name = "lb_DMQL";
            lb_DMQL.Size = new Size(223, 35);
            lb_DMQL.TabIndex = 0;
            lb_DMQL.Text = "Danh mục chức năng";
            // 
            // KhachHang
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.mt;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1236, 837);
            Controls.Add(roundedPanel1);
            DoubleBuffered = true;
            Name = "KhachHang";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Khách hàng";
            Load += KhachHang_Load;
            roundedPanel1.ResumeLayout(false);
            roundedPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pb_Admin).EndInit();
            btn_DangXuat.ResumeLayout(false);
            btn_DangXuat.PerformLayout();
            hcnt_Khung.ResumeLayout(false);
            hcnt_Khung.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvKhachHang).EndInit();
            hcnt_KhungMenuAD.ResumeLayout(false);
            hcnt_KhungMenuAD.PerformLayout();
            btn_QLHDN.ResumeLayout(false);
            btn_QLHDN.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pb_QLHDN).EndInit();
            btn_QLMA.ResumeLayout(false);
            btn_QLMA.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pb_QLMA).EndInit();
            btn_QLKH.ResumeLayout(false);
            btn_QLKH.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pb_QLKH).EndInit();
            btn_QLNCC.ResumeLayout(false);
            btn_QLNCC.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pb_QLNCC).EndInit();
            btn_QLNV.ResumeLayout(false);
            btn_QLNV.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pb_QLNV).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private RoundedPanel roundedPanel1;
        private PictureBox pb_Admin;
        private RoundedPanel btn_DangXuat;
        private Label lb_DangXuat;
        private Label lb_Admin;
        private RoundedPanel hcnt_Khung;
        private RoundedPanel hcnt_KhungMenuAD;
        private RoundedPanel btn_QLHDN;
        private PictureBox pb_QLHDN;
        private Label label8;
        private RoundedPanel btn_QLMA;
        private PictureBox pb_QLMA;
        private Label label7;
        private RoundedPanel btn_QLKH;
        private PictureBox pb_QLKH;
        private Label label6;
        private RoundedPanel btn_QLNCC;
        private PictureBox pb_QLNCC;
        private Label label5;
        private RoundedPanel btn_QLNV;
        private PictureBox pb_QLNV;
        private Label label4;
        private Label lb_DMQL;
        private DataGridView dgvKhachHang;
        private TextBox txtSdt;
        private TextBox txtTen;
        private Button btnThem;
        private Label lblSdt;
        private Label lblTen;
        private Label lblTitle;
        private Label lblCongThuc;
        private Button btnLamMoi;
    }
}


