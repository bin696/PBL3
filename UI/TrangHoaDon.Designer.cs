namespace PBL3.UI
{
    partial class TrangHoaDon
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
            pb_Admin = new PictureBox();
            btn_DangXuat = new RoundedPanel();
            lb_DangXuat = new Label();
            lb_Admin = new Label();
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
            lblMatKhau = new Label();
            _dtpNgay = new DateTimePicker();
            _pnlKhachHangView = new RoundedPanel();
            _lblKhachHangView = new Label();
            _lblTongCongTatCa = new Label();
            _btnLamMoi = new RoundedPanel();
            lblBtnLamMoi = new Label();
            lblHoTen = new Label();
            pnlMaNVInput = new RoundedPanel();
            txtMaHDB = new TextBox();
            lblMaNV = new Label();
            roundedPanel1 = new RoundedPanel();
            hcnt_Khung = new RoundedPanel();
            lblTitle = new Label();
            pnlBoLoc = new RoundedPanel();
            _btnHoaDonBan = new Button();
            _btnHoaDonNhap = new Button();
            pnlTimMa = new RoundedPanel();
            _txtTimMaHD = new TextBox();
            pnlDanhSachHoaDon = new RoundedPanel();
            lblMaster = new Label();
            _dgvHoaDonMaster = new DataGridView();
            pnlReceipt = new RoundedPanel();
            lblDetail = new Label();
            lblReceiptNhanVien = new Label();
            lblReceiptDoiTac = new Label();
            lblReceiptThanhToan = new Label();
            _dgvHoaDonDetail = new DataGridView();
            _btnHuyHoaDon = new RoundedPanel();
            lblBtnHuyHoaDon = new Label();
            _btnInLai = new RoundedPanel();
            lblBtnInLai = new Label();
            ((System.ComponentModel.ISupportInitialize)pb_Admin).BeginInit();
            btn_DangXuat.SuspendLayout();
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
            _pnlKhachHangView.SuspendLayout();
            _btnLamMoi.SuspendLayout();
            pnlMaNVInput.SuspendLayout();
            roundedPanel1.SuspendLayout();
            hcnt_Khung.SuspendLayout();
            pnlBoLoc.SuspendLayout();
            pnlTimMa.SuspendLayout();
            pnlDanhSachHoaDon.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_dgvHoaDonMaster).BeginInit();
            pnlReceipt.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_dgvHoaDonDetail).BeginInit();
            _btnHuyHoaDon.SuspendLayout();
            _btnInLai.SuspendLayout();
            SuspendLayout();
            // 
            // pb_Admin
            // 
            pb_Admin.BackColor = SystemColors.Control;
            pb_Admin.Image = Properties.Resources.admin;
            pb_Admin.Location = new Point(19, 11);
            pb_Admin.Margin = new Padding(3, 4, 3, 4);
            pb_Admin.Name = "pb_Admin";
            pb_Admin.Size = new Size(51, 51);
            pb_Admin.SizeMode = PictureBoxSizeMode.StretchImage;
            pb_Admin.TabIndex = 5;
            pb_Admin.TabStop = false;
            // 
            // btn_DangXuat
            // 
            btn_DangXuat.BackColor = Color.SandyBrown;
            btn_DangXuat.Controls.Add(lb_DangXuat);
            btn_DangXuat.Font = new Font("Segoe UI", 9F);
            btn_DangXuat.Location = new Point(194, 16);
            btn_DangXuat.Margin = new Padding(3, 4, 3, 4);
            btn_DangXuat.Name = "btn_DangXuat";
            btn_DangXuat.Size = new Size(126, 33);
            btn_DangXuat.TabIndex = 7;
            btn_DangXuat.Click += btn_DangXuat_Click;
            // 
            // lb_DangXuat
            // 
            lb_DangXuat.AutoSize = true;
            lb_DangXuat.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lb_DangXuat.ForeColor = SystemColors.ButtonFace;
            lb_DangXuat.Location = new Point(18, 4);
            lb_DangXuat.Name = "lb_DangXuat";
            lb_DangXuat.Size = new Size(93, 23);
            lb_DangXuat.TabIndex = 2;
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
            lb_Admin.TabIndex = 6;
            lb_Admin.Text = "Nhân viên";
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
            hcnt_KhungMenuAD.Location = new Point(20, 65);
            hcnt_KhungMenuAD.Margin = new Padding(3, 4, 3, 4);
            hcnt_KhungMenuAD.Name = "hcnt_KhungMenuAD";
            hcnt_KhungMenuAD.Size = new Size(301, 717);
            hcnt_KhungMenuAD.TabIndex = 4;
            // 
            // btn_QLHDN
            // 
            btn_QLHDN.BackColor = Color.Bisque;
            btn_QLHDN.Controls.Add(pb_QLHDN);
            btn_QLHDN.Controls.Add(label8);
            btn_QLHDN.Location = new Point(21, 369);
            btn_QLHDN.Margin = new Padding(3, 4, 3, 4);
            btn_QLHDN.Name = "btn_QLHDN";
            btn_QLHDN.Size = new Size(256, 53);
            btn_QLHDN.TabIndex = 1;
            btn_QLHDN.Click += btn_QLHDN_Click;
            // 
            // pb_QLHDN
            // 
            pb_QLHDN.Image = Properties.Resources.nguyenlieu;
            pb_QLHDN.Location = new Point(10, 0);
            pb_QLHDN.Margin = new Padding(3, 4, 3, 4);
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
            label8.ForeColor = SystemColors.ControlText;
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
            btn_QLMA.Location = new Point(21, 296);
            btn_QLMA.Margin = new Padding(3, 4, 3, 4);
            btn_QLMA.Name = "btn_QLMA";
            btn_QLMA.Size = new Size(256, 53);
            btn_QLMA.TabIndex = 1;
            btn_QLMA.Click += btn_QLMA_Click;
            // 
            // pb_QLMA
            // 
            pb_QLMA.Image = Properties.Resources.monan;
            pb_QLMA.Location = new Point(10, 1);
            pb_QLMA.Margin = new Padding(3, 4, 3, 4);
            pb_QLMA.Name = "pb_QLMA";
            pb_QLMA.Size = new Size(51, 51);
            pb_QLMA.SizeMode = PictureBoxSizeMode.StretchImage;
            pb_QLMA.TabIndex = 2;
            pb_QLMA.TabStop = false;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 12F);
            label7.ForeColor = SystemColors.ControlText;
            label7.Location = new Point(58, 11);
            label7.Name = "label7";
            label7.Size = new Size(100, 28);
            label7.TabIndex = 0;
            label7.Text = "Mua hàng";
            // 
            // btn_QLKH
            // 
            btn_QLKH.BackColor = Color.Bisque;
            btn_QLKH.Controls.Add(pb_QLKH);
            btn_QLKH.Controls.Add(label6);
            btn_QLKH.Location = new Point(21, 223);
            btn_QLKH.Margin = new Padding(3, 4, 3, 4);
            btn_QLKH.Name = "btn_QLKH";
            btn_QLKH.Size = new Size(256, 53);
            btn_QLKH.TabIndex = 1;
            btn_QLKH.Click += btn_QLKH_Click;
            // 
            // pb_QLKH
            // 
            pb_QLKH.Image = Properties.Resources.khachhang;
            pb_QLKH.Location = new Point(10, 0);
            pb_QLKH.Margin = new Padding(3, 4, 3, 4);
            pb_QLKH.Name = "pb_QLKH";
            pb_QLKH.Size = new Size(51, 51);
            pb_QLKH.SizeMode = PictureBoxSizeMode.StretchImage;
            pb_QLKH.TabIndex = 2;
            pb_QLKH.TabStop = false;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 12F);
            label6.ForeColor = SystemColors.ControlText;
            label6.Location = new Point(58, 11);
            label6.Name = "label6";
            label6.Size = new Size(93, 28);
            label6.TabIndex = 0;
            label6.Text = "Bán hàng";
            // 
            // btn_QLNCC
            // 
            btn_QLNCC.BackColor = Color.Coral;
            btn_QLNCC.Controls.Add(pb_QLNCC);
            btn_QLNCC.Controls.Add(label5);
            btn_QLNCC.Location = new Point(21, 149);
            btn_QLNCC.Margin = new Padding(3, 4, 3, 4);
            btn_QLNCC.Name = "btn_QLNCC";
            btn_QLNCC.Size = new Size(256, 53);
            btn_QLNCC.TabIndex = 1;
            btn_QLNCC.Click += btn_QLNCC_Click;
            // 
            // pb_QLNCC
            // 
            pb_QLNCC.Image = Properties.Resources.ncc;
            pb_QLNCC.Location = new Point(10, 0);
            pb_QLNCC.Margin = new Padding(3, 4, 3, 4);
            pb_QLNCC.Name = "pb_QLNCC";
            pb_QLNCC.Size = new Size(51, 51);
            pb_QLNCC.SizeMode = PictureBoxSizeMode.StretchImage;
            pb_QLNCC.TabIndex = 2;
            pb_QLNCC.TabStop = false;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 12F);
            label5.ForeColor = SystemColors.ControlText;
            label5.Location = new Point(58, 11);
            label5.Name = "label5";
            label5.Size = new Size(88, 28);
            label5.TabIndex = 0;
            label5.Text = "Hóa đơn";
            // 
            // btn_QLNV
            // 
            btn_QLNV.BackColor = Color.Bisque;
            btn_QLNV.Controls.Add(pb_QLNV);
            btn_QLNV.Controls.Add(label4);
            btn_QLNV.Location = new Point(21, 76);
            btn_QLNV.Margin = new Padding(3, 4, 3, 4);
            btn_QLNV.Name = "btn_QLNV";
            btn_QLNV.Size = new Size(256, 53);
            btn_QLNV.TabIndex = 1;
            btn_QLNV.Click += ThongTinCaNhan_Click;
            // 
            // pb_QLNV
            // 
            pb_QLNV.Image = Properties.Resources.nhanvien;
            pb_QLNV.Location = new Point(10, 0);
            pb_QLNV.Margin = new Padding(3, 4, 3, 4);
            pb_QLNV.Name = "pb_QLNV";
            pb_QLNV.Size = new Size(51, 51);
            pb_QLNV.SizeMode = PictureBoxSizeMode.StretchImage;
            pb_QLNV.TabIndex = 2;
            pb_QLNV.TabStop = false;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 12F);
            label4.ForeColor = SystemColors.ControlText;
            label4.Location = new Point(58, 11);
            label4.Name = "label4";
            label4.Size = new Size(168, 28);
            label4.TabIndex = 0;
            label4.Text = "Thông tin cá nhân";
            label4.Click += ThongTinCaNhan_Click;
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
            // lblMatKhau
            // 
            lblMatKhau.AutoSize = true;
            lblMatKhau.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblMatKhau.Location = new Point(555, 57);
            lblMatKhau.Name = "lblMatKhau";
            lblMatKhau.Size = new Size(82, 23);
            lblMatKhau.TabIndex = 30;
            lblMatKhau.Text = "Ngày lập";
            lblMatKhau.Click += lblMatKhau_Click;
            // 
            // _dtpNgay
            // 
            _dtpNgay.CustomFormat = "dd/MM/yyyy";
            _dtpNgay.Format = DateTimePickerFormat.Custom;
            _dtpNgay.Location = new Point(597, 83);
            _dtpNgay.Name = "_dtpNgay";
            _dtpNgay.Size = new Size(117, 27);
            _dtpNgay.TabIndex = 12;
            // 
            // _pnlKhachHangView
            // 
            _pnlKhachHangView.BackColor = Color.Gainsboro;
            _pnlKhachHangView.Controls.Add(_lblKhachHangView);
            _pnlKhachHangView.CornerRadius = 8;
            _pnlKhachHangView.Location = new Point(307, 82);
            _pnlKhachHangView.Name = "_pnlKhachHangView";
            _pnlKhachHangView.Size = new Size(195, 28);
            _pnlKhachHangView.TabIndex = 13;
            // 
            // _lblKhachHangView
            // 
            _lblKhachHangView.Font = new Font("Segoe UI", 9F);
            _lblKhachHangView.Location = new Point(8, 3);
            _lblKhachHangView.Name = "_lblKhachHangView";
            _lblKhachHangView.Size = new Size(179, 22);
            _lblKhachHangView.TabIndex = 0;
            _lblKhachHangView.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // _lblTongCongTatCa
            // 
            _lblTongCongTatCa.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            _lblTongCongTatCa.ForeColor = Color.Black;
            _lblTongCongTatCa.Location = new Point(36, 619);
            _lblTongCongTatCa.Name = "_lblTongCongTatCa";
            _lblTongCongTatCa.Size = new Size(327, 28);
            _lblTongCongTatCa.TabIndex = 10;
            _lblTongCongTatCa.Text = "Tổng cộng: 0 đ";
            _lblTongCongTatCa.TextAlign = ContentAlignment.MiddleLeft;
            _lblTongCongTatCa.Click += _lblTongCongTatCa_Click;
            // 
            // _btnLamMoi
            // 
            _btnLamMoi.BackColor = Color.Peru;
            _btnLamMoi.Controls.Add(lblBtnLamMoi);
            _btnLamMoi.CornerRadius = 10;
            _btnLamMoi.Location = new Point(25, 650);
            _btnLamMoi.Name = "_btnLamMoi";
            _btnLamMoi.Size = new Size(86, 40);
            _btnLamMoi.TabIndex = 9;
            // 
            // lblBtnLamMoi
            // 
            lblBtnLamMoi.AutoSize = true;
            lblBtnLamMoi.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblBtnLamMoi.ForeColor = Color.White;
            lblBtnLamMoi.Location = new Point(2, 8);
            lblBtnLamMoi.Name = "lblBtnLamMoi";
            lblBtnLamMoi.Size = new Size(81, 23);
            lblBtnLamMoi.TabIndex = 0;
            lblBtnLamMoi.Text = "Làm mới";
            // 
            // lblHoTen
            // 
            lblHoTen.AutoSize = true;
            lblHoTen.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblHoTen.Location = new Point(262, 56);
            lblHoTen.Name = "lblHoTen";
            lblHoTen.Size = new Size(103, 23);
            lblHoTen.TabIndex = 2;
            lblHoTen.Text = "Khách hàng";
            lblHoTen.Click += lblHoTen_Click;
            // 
            // pnlMaNVInput
            // 
            pnlMaNVInput.BackColor = Color.Gainsboro;
            pnlMaNVInput.Controls.Add(txtMaHDB);
            pnlMaNVInput.CornerRadius = 8;
            pnlMaNVInput.Location = new Point(57, 85);
            pnlMaNVInput.Name = "pnlMaNVInput";
            pnlMaNVInput.Size = new Size(125, 33);
            pnlMaNVInput.TabIndex = 23;
            // 
            // txtMaHDB
            // 
            txtMaHDB.BackColor = Color.Gainsboro;
            txtMaHDB.BorderStyle = BorderStyle.None;
            txtMaHDB.Enabled = false;
            txtMaHDB.Location = new Point(10, 5);
            txtMaHDB.Name = "txtMaHDB";
            txtMaHDB.ReadOnly = true;
            txtMaHDB.Size = new Size(102, 20);
            txtMaHDB.TabIndex = 1;
            txtMaHDB.TabStop = false;
            txtMaHDB.TextChanged += _txtMaNV_TextChanged;
            // 
            // lblMaNV
            // 
            lblMaNV.AutoSize = true;
            lblMaNV.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblMaNV.Location = new Point(27, 56);
            lblMaNV.Name = "lblMaNV";
            lblMaNV.Size = new Size(66, 23);
            lblMaNV.TabIndex = 0;
            lblMaNV.Text = "Mã HD";
            // 
            // roundedPanel1
            // 
            roundedPanel1.Controls.Add(hcnt_Khung);
            roundedPanel1.Controls.Add(pb_Admin);
            roundedPanel1.Controls.Add(lb_Admin);
            roundedPanel1.Controls.Add(btn_DangXuat);
            roundedPanel1.Controls.Add(hcnt_KhungMenuAD);
            roundedPanel1.Location = new Point(14, 16);
            roundedPanel1.Name = "roundedPanel1";
            roundedPanel1.Size = new Size(1232, 811);
            roundedPanel1.TabIndex = 9;
            // 
            // hcnt_Khung
            // 
            hcnt_Khung.BackColor = Color.Linen;
            hcnt_Khung.Controls.Add(lblTitle);
            hcnt_Khung.Controls.Add(pnlBoLoc);
            hcnt_Khung.Controls.Add(pnlDanhSachHoaDon);
            hcnt_Khung.Controls.Add(_btnInLai);
            hcnt_Khung.Controls.Add(_btnLamMoi);
            hcnt_Khung.Controls.Add(_lblTongCongTatCa);
            hcnt_Khung.Location = new Point(340, 65);
            hcnt_Khung.Name = "hcnt_Khung";
            hcnt_Khung.Size = new Size(870, 717);
            hcnt_Khung.TabIndex = 31;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            lblTitle.ForeColor = Color.Salmon;
            lblTitle.Location = new Point(375, 9);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(116, 35);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Hóa đơn";
            // 
            // pnlBoLoc
            // 
            pnlBoLoc.BackColor = Color.FromArgb(248, 242, 235);
            pnlBoLoc.Controls.Add(_btnHoaDonBan);
            pnlBoLoc.Controls.Add(_dtpNgay);
            pnlBoLoc.Controls.Add(_btnHoaDonNhap);
            pnlBoLoc.Controls.Add(_pnlKhachHangView);
            pnlBoLoc.Controls.Add(pnlTimMa);
            pnlBoLoc.Controls.Add(lblMatKhau);
            pnlBoLoc.Controls.Add(lblMaNV);
            pnlBoLoc.Controls.Add(lblHoTen);
            pnlBoLoc.Controls.Add(pnlMaNVInput);
            pnlBoLoc.CornerRadius = 16;
            pnlBoLoc.Location = new Point(18, 54);
            pnlBoLoc.Name = "pnlBoLoc";
            pnlBoLoc.Size = new Size(834, 186);
            pnlBoLoc.TabIndex = 1;
            // 
            // _btnHoaDonBan
            // 
            _btnHoaDonBan.BackColor = Color.Coral;
            _btnHoaDonBan.FlatStyle = FlatStyle.Flat;
            _btnHoaDonBan.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            _btnHoaDonBan.ForeColor = Color.White;
            _btnHoaDonBan.Location = new Point(231, 10);
            _btnHoaDonBan.Name = "_btnHoaDonBan";
            _btnHoaDonBan.Size = new Size(162, 33);
            _btnHoaDonBan.TabIndex = 0;
            _btnHoaDonBan.Text = "Hóa đơn bán";
            _btnHoaDonBan.UseVisualStyleBackColor = false;
            // 
            // _btnHoaDonNhap
            // 
            _btnHoaDonNhap.BackColor = Color.BurlyWood;
            _btnHoaDonNhap.FlatStyle = FlatStyle.Flat;
            _btnHoaDonNhap.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            _btnHoaDonNhap.Location = new Point(427, 10);
            _btnHoaDonNhap.Name = "_btnHoaDonNhap";
            _btnHoaDonNhap.Size = new Size(165, 33);
            _btnHoaDonNhap.TabIndex = 1;
            _btnHoaDonNhap.Text = "Hóa đơn nhập";
            _btnHoaDonNhap.UseVisualStyleBackColor = false;
            // 
            // pnlTimMa
            // 
            pnlTimMa.BackColor = Color.Bisque;
            pnlTimMa.Controls.Add(_txtTimMaHD);
            pnlTimMa.CornerRadius = 8;
            pnlTimMa.Location = new Point(27, 134);
            pnlTimMa.Name = "pnlTimMa";
            pnlTimMa.Size = new Size(326, 35);
            pnlTimMa.TabIndex = 9;
            // 
            // _txtTimMaHD
            // 
            _txtTimMaHD.BackColor = Color.Bisque;
            _txtTimMaHD.BorderStyle = BorderStyle.None;
            _txtTimMaHD.Location = new Point(9, 6);
            _txtTimMaHD.Name = "_txtTimMaHD";
            _txtTimMaHD.PlaceholderText = "Tìm kiếm";
            _txtTimMaHD.Size = new Size(309, 20);
            _txtTimMaHD.TabIndex = 0;
            // 
            // pnlDanhSachHoaDon
            // 
            pnlDanhSachHoaDon.BackColor = Color.FromArgb(248, 242, 235);
            pnlDanhSachHoaDon.Controls.Add(lblMaster);
            pnlDanhSachHoaDon.Controls.Add(_dgvHoaDonMaster);
            pnlDanhSachHoaDon.Controls.Add(pnlReceipt);
            pnlDanhSachHoaDon.CornerRadius = 16;
            pnlDanhSachHoaDon.Location = new Point(18, 246);
            pnlDanhSachHoaDon.Name = "pnlDanhSachHoaDon";
            pnlDanhSachHoaDon.Size = new Size(834, 374);
            pnlDanhSachHoaDon.TabIndex = 5;
            // 
            // lblMaster
            // 
            lblMaster.AutoSize = true;
            lblMaster.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblMaster.ForeColor = Color.FromArgb(110, 92, 75);
            lblMaster.Location = new Point(18, 12);
            lblMaster.Name = "lblMaster";
            lblMaster.Size = new Size(181, 25);
            lblMaster.TabIndex = 0;
            lblMaster.Text = "Danh sách hóa đơn";
            // 
            // _dgvHoaDonMaster
            // 
            _dgvHoaDonMaster.AllowUserToAddRows = false;
            _dgvHoaDonMaster.AllowUserToDeleteRows = false;
            _dgvHoaDonMaster.BackgroundColor = Color.WhiteSmoke;
            _dgvHoaDonMaster.BorderStyle = BorderStyle.None;
            _dgvHoaDonMaster.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            _dgvHoaDonMaster.Location = new Point(18, 46);
            _dgvHoaDonMaster.MultiSelect = false;
            _dgvHoaDonMaster.Name = "_dgvHoaDonMaster";
            _dgvHoaDonMaster.ReadOnly = true;
            _dgvHoaDonMaster.RowHeadersVisible = false;
            _dgvHoaDonMaster.RowHeadersWidth = 51;
            _dgvHoaDonMaster.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _dgvHoaDonMaster.Size = new Size(332, 328);
            _dgvHoaDonMaster.TabIndex = 1;
            // 
            // pnlReceipt
            // 
            pnlReceipt.BackColor = Color.White;
            pnlReceipt.Controls.Add(lblDetail);
            pnlReceipt.Controls.Add(lblReceiptNhanVien);
            pnlReceipt.Controls.Add(lblReceiptDoiTac);
            pnlReceipt.Controls.Add(lblReceiptThanhToan);
            pnlReceipt.Controls.Add(_dgvHoaDonDetail);
            pnlReceipt.Controls.Add(_btnHuyHoaDon);
            pnlReceipt.CornerRadius = 8;
            pnlReceipt.Location = new Point(358, 16);
            pnlReceipt.Name = "pnlReceipt";
            pnlReceipt.Size = new Size(458, 358);
            pnlReceipt.TabIndex = 4;
            // 
            // lblDetail
            // 
            lblDetail.AutoSize = true;
            lblDetail.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblDetail.ForeColor = Color.FromArgb(110, 92, 75);
            lblDetail.Location = new Point(12, 10);
            lblDetail.Name = "lblDetail";
            lblDetail.Size = new Size(114, 25);
            lblDetail.TabIndex = 2;
            lblDetail.Text = "Tờ hóa đơn";
            // 
            // lblReceiptNhanVien
            // 
            lblReceiptNhanVien.AutoSize = true;
            lblReceiptNhanVien.Font = new Font("Segoe UI", 10F);
            lblReceiptNhanVien.Location = new Point(12, 40);
            lblReceiptNhanVien.Name = "lblReceiptNhanVien";
            lblReceiptNhanVien.Size = new Size(132, 23);
            lblReceiptNhanVien.TabIndex = 3;
            lblReceiptNhanVien.Text = "👤 Nhân viên: -";
            // 
            // lblReceiptDoiTac
            // 
            lblReceiptDoiTac.AutoSize = true;
            lblReceiptDoiTac.Font = new Font("Segoe UI", 10F);
            lblReceiptDoiTac.Location = new Point(12, 64);
            lblReceiptDoiTac.Name = "lblReceiptDoiTac";
            lblReceiptDoiTac.Size = new Size(145, 23);
            lblReceiptDoiTac.TabIndex = 4;
            lblReceiptDoiTac.Text = "\U0001f91d Khách hàng: -";
            // 
            // lblReceiptThanhToan
            // 
            lblReceiptThanhToan.AutoSize = true;
            lblReceiptThanhToan.Font = new Font("Segoe UI", 10F);
            lblReceiptThanhToan.Location = new Point(12, 88);
            lblReceiptThanhToan.Name = "lblReceiptThanhToan";
            lblReceiptThanhToan.Size = new Size(142, 23);
            lblReceiptThanhToan.TabIndex = 5;
            lblReceiptThanhToan.Text = "💳 Thanh toán: -";
            // 
            // _dgvHoaDonDetail
            // 
            _dgvHoaDonDetail.AllowUserToAddRows = false;
            _dgvHoaDonDetail.AllowUserToDeleteRows = false;
            _dgvHoaDonDetail.BackgroundColor = Color.WhiteSmoke;
            _dgvHoaDonDetail.BorderStyle = BorderStyle.None;
            _dgvHoaDonDetail.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            _dgvHoaDonDetail.Location = new Point(12, 118);
            _dgvHoaDonDetail.MultiSelect = false;
            _dgvHoaDonDetail.Name = "_dgvHoaDonDetail";
            _dgvHoaDonDetail.ReadOnly = true;
            _dgvHoaDonDetail.RowHeadersVisible = false;
            _dgvHoaDonDetail.RowHeadersWidth = 51;
            _dgvHoaDonDetail.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _dgvHoaDonDetail.Size = new Size(433, 186);
            _dgvHoaDonDetail.TabIndex = 6;
            // 
            // _btnHuyHoaDon
            // 
            _btnHuyHoaDon.BackColor = Color.IndianRed;
            _btnHuyHoaDon.Controls.Add(lblBtnHuyHoaDon);
            _btnHuyHoaDon.CornerRadius = 10;
            _btnHuyHoaDon.Location = new Point(252, 310);
            _btnHuyHoaDon.Name = "_btnHuyHoaDon";
            _btnHuyHoaDon.Size = new Size(193, 36);
            _btnHuyHoaDon.TabIndex = 6;
            // 
            // lblBtnHuyHoaDon
            // 
            lblBtnHuyHoaDon.AutoSize = true;
            lblBtnHuyHoaDon.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblBtnHuyHoaDon.ForeColor = Color.White;
            lblBtnHuyHoaDon.Location = new Point(12, 6);
            lblBtnHuyHoaDon.Name = "lblBtnHuyHoaDon";
            lblBtnHuyHoaDon.Size = new Size(174, 23);
            lblBtnHuyHoaDon.TabIndex = 0;
            lblBtnHuyHoaDon.Text = "Yêu cầu hủy hóa đơn";
            // 
            // _btnInLai
            // 
            _btnInLai.BackColor = Color.BurlyWood;
            _btnInLai.Controls.Add(lblBtnInLai);
            _btnInLai.CornerRadius = 10;
            _btnInLai.Location = new Point(740, 650);
            _btnInLai.Name = "_btnInLai";
            _btnInLai.Size = new Size(112, 40);
            _btnInLai.TabIndex = 8;
            // 
            // lblBtnInLai
            // 
            lblBtnInLai.AutoSize = true;
            lblBtnInLai.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblBtnInLai.Location = new Point(13, 8);
            lblBtnInLai.Name = "lblBtnInLai";
            lblBtnInLai.Size = new Size(96, 23);
            lblBtnInLai.TabIndex = 0;
            lblBtnInLai.Text = "In hóa đơn";
            // 
            // TrangHoaDon
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.mt;
            ClientSize = new Size(1258, 837);
            Controls.Add(roundedPanel1);
            Name = "TrangHoaDon";
            Text = "TrangHoaDon";
            Load += TrangHoaDon_Load;
            ((System.ComponentModel.ISupportInitialize)pb_Admin).EndInit();
            btn_DangXuat.ResumeLayout(false);
            btn_DangXuat.PerformLayout();
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
            _pnlKhachHangView.ResumeLayout(false);
            _btnLamMoi.ResumeLayout(false);
            _btnLamMoi.PerformLayout();
            pnlMaNVInput.ResumeLayout(false);
            pnlMaNVInput.PerformLayout();
            roundedPanel1.ResumeLayout(false);
            roundedPanel1.PerformLayout();
            hcnt_Khung.ResumeLayout(false);
            hcnt_Khung.PerformLayout();
            pnlBoLoc.ResumeLayout(false);
            pnlBoLoc.PerformLayout();
            pnlTimMa.ResumeLayout(false);
            pnlTimMa.PerformLayout();
            pnlDanhSachHoaDon.ResumeLayout(false);
            pnlDanhSachHoaDon.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)_dgvHoaDonMaster).EndInit();
            pnlReceipt.ResumeLayout(false);
            pnlReceipt.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)_dgvHoaDonDetail).EndInit();
            _btnHuyHoaDon.ResumeLayout(false);
            _btnHuyHoaDon.PerformLayout();
            _btnInLai.ResumeLayout(false);
            _btnInLai.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pb_Admin;
        private RoundedPanel btn_DangXuat;
        private Label lb_DangXuat;
        private Label lb_Admin;
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
        private DateTimePicker _dtpNgay;
        private Label lblMatKhau;
        private RoundedPanel _pnlKhachHangView;
        private Label _lblKhachHangView;
        private Label _lblTongCongTatCa;
        private Label lblHoTen;
        private RoundedPanel pnlMaNVInput;
        private TextBox txtMaHDB;
        private Label lblMaNV;
        private RoundedPanel roundedPanel1;
        private RoundedPanel hcnt_Khung;
        private Label lblTitle;
        private RoundedPanel pnlBoLoc;
        private Button _btnHoaDonBan;
        private Button _btnHoaDonNhap;
        private RoundedPanel pnlTimMa;
        private TextBox _txtTimMaHD;
        private RoundedPanel pnlDanhSachHoaDon;
        private Label lblMaster;
        private DataGridView _dgvHoaDonMaster;
        private RoundedPanel pnlReceipt;
        private Label lblDetail;
        private Label lblReceiptNhanVien;
        private Label lblReceiptDoiTac;
        private Label lblReceiptThanhToan;
        private DataGridView _dgvHoaDonDetail;
        private RoundedPanel _btnHuyHoaDon;
        private Label lblBtnHuyHoaDon;
        private RoundedPanel _btnInLai;
        private Label lblBtnInLai;
        private RoundedPanel _btnLamMoi;
        private Label lblBtnLamMoi;
    }
}
