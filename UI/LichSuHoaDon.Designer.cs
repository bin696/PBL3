using PBL3.UI;

namespace PBL3
{
    partial class LichSuHoaDon
    {
        private System.ComponentModel.IContainer? components = null;

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
            lb_Admin = new Label();
            btn_DangXuat = new RoundedPanel();
            lb_DangXuat = new Label();
            hcnt_KhungMenuAD = new RoundedPanel();
            btn_ThongKe = new RoundedPanel();
            pb_ThongKe = new PictureBox();
            lblThongKe = new Label();
            btn_QLHDN = new RoundedPanel();
            pb_QLHDN = new PictureBox();
            lblQLHDN = new Label();
            btn_QLMA = new RoundedPanel();
            pb_QLMA = new PictureBox();
            lblQLMA = new Label();
            btn_QLKH = new RoundedPanel();
            pb_QLKH = new PictureBox();
            lblQLKH = new Label();
            btn_QLNCC = new RoundedPanel();
            pb_QLNCC = new PictureBox();
            lblQLNCC = new Label();
            btn_QLNV = new RoundedPanel();
            pb_QLNV = new PictureBox();
            lblQLNV = new Label();
            btn_LSHD = new RoundedPanel();
            pb_LSHD = new PictureBox();
            lblLSHD = new Label();
            lb_DMQL = new Label();
            hcnt_Khung = new RoundedPanel();
            lblTitle = new Label();
            pnlBoLoc = new RoundedPanel();
            _btnHoaDonBan = new Button();
            _btnHoaDonNhap = new Button();
            pnlCard3 = new RoundedPanel();
            lblCard3Title = new Label();
            _lblSoDonHuyValue = new Label();
            pnlTimMa = new RoundedPanel();
            _txtTimMaHD = new TextBox();
            lblTuNgay = new Label();
            _dtpTuNgay = new DateTimePicker();
            lblDenNgay = new Label();
            _dtpDenNgay = new DateTimePicker();
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
            _cboLocDoiTuong = new ComboBox();
            _btnXuatBaoCao = new RoundedPanel();
            lblBtnXuatBaoCao = new Label();
            _btnInLai = new RoundedPanel();
            lblBtnInLai = new Label();
            _btnLamMoi = new RoundedPanel();
            lblBtnLamMoi = new Label();
            roundedPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pb_Admin).BeginInit();
            btn_DangXuat.SuspendLayout();
            hcnt_KhungMenuAD.SuspendLayout();
            btn_ThongKe.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pb_ThongKe).BeginInit();
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
            btn_LSHD.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pb_LSHD).BeginInit();
            hcnt_Khung.SuspendLayout();
            pnlBoLoc.SuspendLayout();
            pnlCard3.SuspendLayout();
            pnlTimMa.SuspendLayout();
            pnlDanhSachHoaDon.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_dgvHoaDonMaster).BeginInit();
            pnlReceipt.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_dgvHoaDonDetail).BeginInit();
            _btnHuyHoaDon.SuspendLayout();
            _btnXuatBaoCao.SuspendLayout();
            _btnInLai.SuspendLayout();
            _btnLamMoi.SuspendLayout();
            SuspendLayout();
            // 
            // roundedPanel1
            // 
            roundedPanel1.Controls.Add(pb_Admin);
            roundedPanel1.Controls.Add(lb_Admin);
            roundedPanel1.Controls.Add(btn_DangXuat);
            roundedPanel1.Controls.Add(hcnt_KhungMenuAD);
            roundedPanel1.Controls.Add(hcnt_Khung);
            roundedPanel1.Location = new Point(14, 16);
            roundedPanel1.Name = "roundedPanel1";
            roundedPanel1.Size = new Size(1232, 811);
            roundedPanel1.TabIndex = 0;
            // 
            // pb_Admin
            // 
            pb_Admin.BackColor = SystemColors.Control;
            pb_Admin.Image = Properties.Resources.admin;
            pb_Admin.Location = new Point(19, 11);
            pb_Admin.Name = "pb_Admin";
            pb_Admin.Size = new Size(51, 51);
            pb_Admin.SizeMode = PictureBoxSizeMode.StretchImage;
            pb_Admin.TabIndex = 0;
            pb_Admin.TabStop = false;
            // 
            // lb_Admin
            // 
            lb_Admin.AutoSize = true;
            lb_Admin.Font = new Font("Segoe UI", 12F);
            lb_Admin.Location = new Point(66, 16);
            lb_Admin.Name = "lb_Admin";
            lb_Admin.Size = new Size(70, 28);
            lb_Admin.TabIndex = 1;
            lb_Admin.Text = "Admin";
            // 
            // btn_DangXuat
            // 
            btn_DangXuat.BackColor = Color.SandyBrown;
            btn_DangXuat.Controls.Add(lb_DangXuat);
            btn_DangXuat.CornerRadius = 10;
            btn_DangXuat.Location = new Point(194, 16);
            btn_DangXuat.Name = "btn_DangXuat";
            btn_DangXuat.Size = new Size(126, 33);
            btn_DangXuat.TabIndex = 2;
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
            // hcnt_KhungMenuAD
            // 
            hcnt_KhungMenuAD.BackColor = Color.Linen;
            hcnt_KhungMenuAD.Controls.Add(btn_ThongKe);
            hcnt_KhungMenuAD.Controls.Add(btn_QLHDN);
            hcnt_KhungMenuAD.Controls.Add(btn_QLMA);
            hcnt_KhungMenuAD.Controls.Add(btn_QLKH);
            hcnt_KhungMenuAD.Controls.Add(btn_QLNCC);
            hcnt_KhungMenuAD.Controls.Add(btn_QLNV);
            hcnt_KhungMenuAD.Controls.Add(btn_LSHD);
            hcnt_KhungMenuAD.Controls.Add(lb_DMQL);
            hcnt_KhungMenuAD.Location = new Point(19, 65);
            hcnt_KhungMenuAD.Name = "hcnt_KhungMenuAD";
            hcnt_KhungMenuAD.Size = new Size(301, 717);
            hcnt_KhungMenuAD.TabIndex = 3;
            // 
            // btn_ThongKe
            // 
            btn_ThongKe.BackColor = Color.Bisque;
            btn_ThongKe.Controls.Add(pb_ThongKe);
            btn_ThongKe.Controls.Add(lblThongKe);
            btn_ThongKe.Location = new Point(21, 517);
            btn_ThongKe.Name = "btn_ThongKe";
            btn_ThongKe.Size = new Size(256, 53);
            btn_ThongKe.TabIndex = 7;
            btn_ThongKe.Click += btn_ThongKe_Click;
            // 
            // pb_ThongKe
            // 
            pb_ThongKe.Image = Properties.Resources.thongke;
            pb_ThongKe.Location = new Point(10, 0);
            pb_ThongKe.Name = "pb_ThongKe";
            pb_ThongKe.Size = new Size(51, 51);
            pb_ThongKe.SizeMode = PictureBoxSizeMode.StretchImage;
            pb_ThongKe.TabIndex = 0;
            pb_ThongKe.TabStop = false;
            pb_ThongKe.Click += btn_ThongKe_Click;
            // 
            // lblThongKe
            // 
            lblThongKe.AutoSize = true;
            lblThongKe.Font = new Font("Segoe UI", 12F);
            lblThongKe.Location = new Point(58, 11);
            lblThongKe.Name = "lblThongKe";
            lblThongKe.Size = new Size(93, 28);
            lblThongKe.TabIndex = 1;
            lblThongKe.Text = "Thống kê";
            lblThongKe.Click += btn_ThongKe_Click;
            // 
            // btn_QLHDN
            // 
            btn_QLHDN.BackColor = Color.Bisque;
            btn_QLHDN.Controls.Add(pb_QLHDN);
            btn_QLHDN.Controls.Add(lblQLHDN);
            btn_QLHDN.Location = new Point(21, 369);
            btn_QLHDN.Name = "btn_QLHDN";
            btn_QLHDN.Size = new Size(256, 53);
            btn_QLHDN.TabIndex = 6;
            btn_QLHDN.Click += btn_QLHDN_Click;
            // 
            // pb_QLHDN
            // 
            pb_QLHDN.Image = Properties.Resources.nguyenlieu;
            pb_QLHDN.Location = new Point(10, 0);
            pb_QLHDN.Name = "pb_QLHDN";
            pb_QLHDN.Size = new Size(51, 51);
            pb_QLHDN.SizeMode = PictureBoxSizeMode.StretchImage;
            pb_QLHDN.TabIndex = 0;
            pb_QLHDN.TabStop = false;
            pb_QLHDN.Click += btn_QLHDN_Click;
            // 
            // lblQLHDN
            // 
            lblQLHDN.AutoSize = true;
            lblQLHDN.Font = new Font("Segoe UI", 12F);
            lblQLHDN.Location = new Point(58, 11);
            lblQLHDN.Name = "lblQLHDN";
            lblQLHDN.Size = new Size(180, 28);
            lblQLHDN.TabIndex = 1;
            lblQLHDN.Text = "Quản lí nguyên liệu";
            lblQLHDN.Click += btn_QLHDN_Click;
            // 
            // btn_QLMA
            // 
            btn_QLMA.BackColor = Color.Bisque;
            btn_QLMA.Controls.Add(pb_QLMA);
            btn_QLMA.Controls.Add(lblQLMA);
            btn_QLMA.Location = new Point(21, 296);
            btn_QLMA.Name = "btn_QLMA";
            btn_QLMA.Size = new Size(256, 53);
            btn_QLMA.TabIndex = 5;
            btn_QLMA.Click += btn_QLMA_Click;
            // 
            // pb_QLMA
            // 
            pb_QLMA.Image = Properties.Resources.monan;
            pb_QLMA.Location = new Point(10, 1);
            pb_QLMA.Name = "pb_QLMA";
            pb_QLMA.Size = new Size(51, 51);
            pb_QLMA.SizeMode = PictureBoxSizeMode.StretchImage;
            pb_QLMA.TabIndex = 0;
            pb_QLMA.TabStop = false;
            pb_QLMA.Click += btn_QLMA_Click;
            // 
            // lblQLMA
            // 
            lblQLMA.AutoSize = true;
            lblQLMA.Font = new Font("Segoe UI", 12F);
            lblQLMA.Location = new Point(58, 11);
            lblQLMA.Name = "lblQLMA";
            lblQLMA.Size = new Size(145, 28);
            lblQLMA.TabIndex = 1;
            lblQLMA.Text = "Quản lí món ăn";
            lblQLMA.Click += btn_QLMA_Click;
            // 
            // btn_QLKH
            // 
            btn_QLKH.BackColor = Color.Bisque;
            btn_QLKH.Controls.Add(pb_QLKH);
            btn_QLKH.Controls.Add(lblQLKH);
            btn_QLKH.Location = new Point(21, 223);
            btn_QLKH.Name = "btn_QLKH";
            btn_QLKH.Size = new Size(256, 53);
            btn_QLKH.TabIndex = 4;
            btn_QLKH.Click += btn_QLKH_Click;
            // 
            // pb_QLKH
            // 
            pb_QLKH.Image = Properties.Resources.khachhang;
            pb_QLKH.Location = new Point(10, 0);
            pb_QLKH.Name = "pb_QLKH";
            pb_QLKH.Size = new Size(51, 51);
            pb_QLKH.SizeMode = PictureBoxSizeMode.StretchImage;
            pb_QLKH.TabIndex = 0;
            pb_QLKH.TabStop = false;
            pb_QLKH.Click += btn_QLKH_Click;
            // 
            // lblQLKH
            // 
            lblQLKH.AutoSize = true;
            lblQLKH.Font = new Font("Segoe UI", 12F);
            lblQLKH.Location = new Point(58, 11);
            lblQLKH.Name = "lblQLKH";
            lblQLKH.Size = new Size(179, 28);
            lblQLKH.TabIndex = 1;
            lblQLKH.Text = "Quản lí khách hàng";
            lblQLKH.Click += btn_QLKH_Click;
            // 
            // btn_QLNCC
            // 
            btn_QLNCC.BackColor = Color.Bisque;
            btn_QLNCC.Controls.Add(pb_QLNCC);
            btn_QLNCC.Controls.Add(lblQLNCC);
            btn_QLNCC.Location = new Point(21, 149);
            btn_QLNCC.Name = "btn_QLNCC";
            btn_QLNCC.Size = new Size(256, 53);
            btn_QLNCC.TabIndex = 3;
            btn_QLNCC.Click += btn_QLNCC_Click;
            // 
            // pb_QLNCC
            // 
            pb_QLNCC.Image = Properties.Resources.ncc;
            pb_QLNCC.Location = new Point(10, 0);
            pb_QLNCC.Name = "pb_QLNCC";
            pb_QLNCC.Size = new Size(51, 51);
            pb_QLNCC.SizeMode = PictureBoxSizeMode.StretchImage;
            pb_QLNCC.TabIndex = 0;
            pb_QLNCC.TabStop = false;
            pb_QLNCC.Click += btn_QLNCC_Click;
            // 
            // lblQLNCC
            // 
            lblQLNCC.AutoSize = true;
            lblQLNCC.Font = new Font("Segoe UI", 12F);
            lblQLNCC.Location = new Point(58, 11);
            lblQLNCC.Name = "lblQLNCC";
            lblQLNCC.Size = new Size(195, 28);
            lblQLNCC.TabIndex = 1;
            lblQLNCC.Text = "Quản lí nhà cung cấp";
            lblQLNCC.Click += btn_QLNCC_Click;
            // 
            // btn_QLNV
            // 
            btn_QLNV.BackColor = Color.Bisque;
            btn_QLNV.Controls.Add(pb_QLNV);
            btn_QLNV.Controls.Add(lblQLNV);
            btn_QLNV.Location = new Point(21, 76);
            btn_QLNV.Name = "btn_QLNV";
            btn_QLNV.Size = new Size(256, 53);
            btn_QLNV.TabIndex = 2;
            btn_QLNV.Click += btn_QLNV_Click;
            // 
            // pb_QLNV
            // 
            pb_QLNV.Image = Properties.Resources.nhanvien;
            pb_QLNV.Location = new Point(10, 0);
            pb_QLNV.Name = "pb_QLNV";
            pb_QLNV.Size = new Size(51, 51);
            pb_QLNV.SizeMode = PictureBoxSizeMode.StretchImage;
            pb_QLNV.TabIndex = 0;
            pb_QLNV.TabStop = false;
            pb_QLNV.Click += btn_QLNV_Click;
            // 
            // lblQLNV
            // 
            lblQLNV.AutoSize = true;
            lblQLNV.Font = new Font("Segoe UI", 12F);
            lblQLNV.Location = new Point(58, 11);
            lblQLNV.Name = "lblQLNV";
            lblQLNV.Size = new Size(163, 28);
            lblQLNV.TabIndex = 1;
            lblQLNV.Text = "Quản lí nhân viên";
            lblQLNV.Click += btn_QLNV_Click;
            // 
            // btn_LSHD
            // 
            btn_LSHD.BackColor = Color.Coral;
            btn_LSHD.Controls.Add(pb_LSHD);
            btn_LSHD.Controls.Add(lblLSHD);
            btn_LSHD.Location = new Point(21, 443);
            btn_LSHD.Name = "btn_LSHD";
            btn_LSHD.Size = new Size(256, 53);
            btn_LSHD.TabIndex = 8;
            // 
            // pb_LSHD
            // 
            pb_LSHD.Image = Properties.Resources.hoadonban1;
            pb_LSHD.Location = new Point(10, 1);
            pb_LSHD.Name = "pb_LSHD";
            pb_LSHD.Size = new Size(51, 51);
            pb_LSHD.SizeMode = PictureBoxSizeMode.StretchImage;
            pb_LSHD.TabIndex = 0;
            pb_LSHD.TabStop = false;
            // 
            // lblLSHD
            // 
            lblLSHD.AutoSize = true;
            lblLSHD.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblLSHD.ForeColor = Color.White;
            lblLSHD.Location = new Point(58, 11);
            lblLSHD.Name = "lblLSHD";
            lblLSHD.Size = new Size(163, 28);
            lblLSHD.TabIndex = 1;
            lblLSHD.Text = "Lịch sử hóa đơn";
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
            lb_DMQL.Text = "Danh mục Quản lí";
            // 
            // hcnt_Khung
            // 
            hcnt_Khung.BackColor = Color.Linen;
            hcnt_Khung.Controls.Add(lblTitle);
            hcnt_Khung.Controls.Add(pnlBoLoc);
            hcnt_Khung.Controls.Add(pnlDanhSachHoaDon);
            hcnt_Khung.Controls.Add(_btnXuatBaoCao);
            hcnt_Khung.Controls.Add(_btnInLai);
            hcnt_Khung.Controls.Add(_btnLamMoi);
            hcnt_Khung.Location = new Point(342, 65);
            hcnt_Khung.Name = "hcnt_Khung";
            hcnt_Khung.Size = new Size(870, 717);
            hcnt_Khung.TabIndex = 4;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            lblTitle.ForeColor = Color.Salmon;
            lblTitle.Location = new Point(326, 8);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(201, 35);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Lịch sử hóa đơn";
            // 
            // pnlBoLoc
            // 
            pnlBoLoc.BackColor = Color.FromArgb(248, 242, 235);
            pnlBoLoc.Controls.Add(_btnHoaDonBan);
            pnlBoLoc.Controls.Add(_btnHoaDonNhap);
            pnlBoLoc.Controls.Add(pnlCard3);
            pnlBoLoc.Controls.Add(pnlTimMa);
            pnlBoLoc.Controls.Add(lblTuNgay);
            pnlBoLoc.Controls.Add(_dtpTuNgay);
            pnlBoLoc.Controls.Add(lblDenNgay);
            pnlBoLoc.Controls.Add(_dtpDenNgay);
            pnlBoLoc.CornerRadius = 16;
            pnlBoLoc.Location = new Point(18, 54);
            pnlBoLoc.Name = "pnlBoLoc";
            pnlBoLoc.Size = new Size(834, 164);
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
            // pnlCard3
            // 
            pnlCard3.BackColor = Color.FromArgb(248, 242, 235);
            pnlCard3.Controls.Add(lblCard3Title);
            pnlCard3.Controls.Add(_lblSoDonHuyValue);
            pnlCard3.CornerRadius = 10;
            pnlCard3.Location = new Point(533, 78);
            pnlCard3.Name = "pnlCard3";
            pnlCard3.Size = new Size(270, 70);
            pnlCard3.TabIndex = 4;
            // 
            // lblCard3Title
            // 
            lblCard3Title.AutoSize = true;
            lblCard3Title.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblCard3Title.Location = new Point(15, 6);
            lblCard3Title.Name = "lblCard3Title";
            lblCard3Title.Size = new Size(101, 23);
            lblCard3Title.TabIndex = 0;
            lblCard3Title.Text = "Số đơn hủy";
            // 
            // _lblSoDonHuyValue
            // 
            _lblSoDonHuyValue.AutoSize = true;
            _lblSoDonHuyValue.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            _lblSoDonHuyValue.ForeColor = Color.IndianRed;
            _lblSoDonHuyValue.Location = new Point(18, 31);
            _lblSoDonHuyValue.Name = "_lblSoDonHuyValue";
            _lblSoDonHuyValue.Size = new Size(28, 32);
            _lblSoDonHuyValue.TabIndex = 1;
            _lblSoDonHuyValue.Text = "0";
            // 
            // pnlTimMa
            // 
            pnlTimMa.BackColor = Color.Bisque;
            pnlTimMa.Controls.Add(_txtTimMaHD);
            pnlTimMa.CornerRadius = 8;
            pnlTimMa.Location = new Point(24, 107);
            pnlTimMa.Name = "pnlTimMa";
            pnlTimMa.Size = new Size(326, 41);
            pnlTimMa.TabIndex = 9;
            // 
            // _txtTimMaHD
            // 
            _txtTimMaHD.BackColor = Color.Bisque;
            _txtTimMaHD.BorderStyle = BorderStyle.None;
            _txtTimMaHD.Location = new Point(9, 10);
            _txtTimMaHD.Name = "_txtTimMaHD";
            _txtTimMaHD.PlaceholderText = "Tìm kiếm";
            _txtTimMaHD.Size = new Size(309, 20);
            _txtTimMaHD.TabIndex = 0;
            _txtTimMaHD.TextChanged += _txtTimMaHD_TextChanged;
            // 
            // lblTuNgay
            // 
            lblTuNgay.AutoSize = true;
            lblTuNgay.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTuNgay.Location = new Point(23, 71);
            lblTuNgay.Name = "lblTuNgay";
            lblTuNgay.Size = new Size(31, 23);
            lblTuNgay.TabIndex = 2;
            lblTuNgay.Text = "Từ";
            // 
            // _dtpTuNgay
            // 
            _dtpTuNgay.CustomFormat = "dd/MM/yyyy";
            _dtpTuNgay.Format = DateTimePickerFormat.Custom;
            _dtpTuNgay.Location = new Point(59, 70);
            _dtpTuNgay.Name = "_dtpTuNgay";
            _dtpTuNgay.Size = new Size(115, 27);
            _dtpTuNgay.TabIndex = 3;
            // 
            // lblDenNgay
            // 
            lblDenNgay.AutoSize = true;
            lblDenNgay.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblDenNgay.Location = new Point(180, 72);
            lblDenNgay.Name = "lblDenNgay";
            lblDenNgay.Size = new Size(42, 23);
            lblDenNgay.TabIndex = 4;
            lblDenNgay.Text = "Đến";
            // 
            // _dtpDenNgay
            // 
            _dtpDenNgay.CustomFormat = "dd/MM/yyyy";
            _dtpDenNgay.Format = DateTimePickerFormat.Custom;
            _dtpDenNgay.Location = new Point(227, 70);
            _dtpDenNgay.Name = "_dtpDenNgay";
            _dtpDenNgay.Size = new Size(115, 27);
            _dtpDenNgay.TabIndex = 5;
            // 
            // pnlDanhSachHoaDon
            // 
            pnlDanhSachHoaDon.BackColor = Color.FromArgb(248, 242, 235);
            pnlDanhSachHoaDon.Controls.Add(lblMaster);
            pnlDanhSachHoaDon.Controls.Add(_dgvHoaDonMaster);
            pnlDanhSachHoaDon.Controls.Add(pnlReceipt);
            pnlDanhSachHoaDon.Controls.Add(_cboLocDoiTuong);
            pnlDanhSachHoaDon.CornerRadius = 16;
            pnlDanhSachHoaDon.Location = new Point(18, 224);
            pnlDanhSachHoaDon.Name = "pnlDanhSachHoaDon";
            pnlDanhSachHoaDon.Size = new Size(834, 396);
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
            _dgvHoaDonMaster.Size = new Size(332, 345);
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
            pnlReceipt.Size = new Size(458, 377);
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
            _dgvHoaDonDetail.Size = new Size(433, 206);
            _dgvHoaDonDetail.TabIndex = 6;
            // 
            // _btnHuyHoaDon
            // 
            _btnHuyHoaDon.BackColor = Color.IndianRed;
            _btnHuyHoaDon.Controls.Add(lblBtnHuyHoaDon);
            _btnHuyHoaDon.CornerRadius = 10;
            _btnHuyHoaDon.Location = new Point(322, 330);
            _btnHuyHoaDon.Name = "_btnHuyHoaDon";
            _btnHuyHoaDon.Size = new Size(123, 36);
            _btnHuyHoaDon.TabIndex = 6;
            _btnHuyHoaDon.Click += BtnHuyHoaDon_Click;
            // 
            // lblBtnHuyHoaDon
            // 
            lblBtnHuyHoaDon.AutoSize = true;
            lblBtnHuyHoaDon.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblBtnHuyHoaDon.ForeColor = Color.White;
            lblBtnHuyHoaDon.Location = new Point(5, 6);
            lblBtnHuyHoaDon.Name = "lblBtnHuyHoaDon";
            lblBtnHuyHoaDon.Size = new Size(113, 23);
            lblBtnHuyHoaDon.TabIndex = 0;
            lblBtnHuyHoaDon.Text = "Hủy hóa đơn";
            lblBtnHuyHoaDon.Click += BtnHuyHoaDon_Click;
            // 
            // _cboLocDoiTuong
            // 
            _cboLocDoiTuong.BackColor = Color.Bisque;
            _cboLocDoiTuong.DropDownStyle = ComboBoxStyle.DropDownList;
            _cboLocDoiTuong.FormattingEnabled = true;
            _cboLocDoiTuong.Location = new Point(205, 11);
            _cboLocDoiTuong.Name = "_cboLocDoiTuong";
            _cboLocDoiTuong.Size = new Size(126, 28);
            _cboLocDoiTuong.TabIndex = 7;
            // 
            // _btnXuatBaoCao
            // 
            _btnXuatBaoCao.BackColor = Color.BurlyWood;
            _btnXuatBaoCao.Controls.Add(lblBtnXuatBaoCao);
            _btnXuatBaoCao.CornerRadius = 10;
            _btnXuatBaoCao.Location = new Point(652, 650);
            _btnXuatBaoCao.Name = "_btnXuatBaoCao";
            _btnXuatBaoCao.Size = new Size(120, 40);
            _btnXuatBaoCao.TabIndex = 7;
            _btnXuatBaoCao.Click += BtnXuatBaoCao_Click;
            // 
            // lblBtnXuatBaoCao
            // 
            lblBtnXuatBaoCao.AutoSize = true;
            lblBtnXuatBaoCao.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblBtnXuatBaoCao.Location = new Point(12, 8);
            lblBtnXuatBaoCao.Name = "lblBtnXuatBaoCao";
            lblBtnXuatBaoCao.Size = new Size(92, 23);
            lblBtnXuatBaoCao.TabIndex = 0;
            lblBtnXuatBaoCao.Text = "Xuất Excel";
            lblBtnXuatBaoCao.Click += BtnXuatBaoCao_Click;
            // 
            // _btnInLai
            // 
            _btnInLai.BackColor = Color.BurlyWood;
            _btnInLai.Controls.Add(lblBtnInLai);
            _btnInLai.CornerRadius = 10;
            _btnInLai.Location = new Point(778, 650);
            _btnInLai.Name = "_btnInLai";
            _btnInLai.Size = new Size(74, 40);
            _btnInLai.TabIndex = 8;
            _btnInLai.Click += BtnInLai_Click;
            // 
            // lblBtnInLai
            // 
            lblBtnInLai.AutoSize = true;
            lblBtnInLai.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblBtnInLai.Location = new Point(13, 8);
            lblBtnInLai.Name = "lblBtnInLai";
            lblBtnInLai.Size = new Size(49, 23);
            lblBtnInLai.TabIndex = 0;
            lblBtnInLai.Text = "In lại";
            lblBtnInLai.Click += BtnInLai_Click;
            // 
            // _btnLamMoi
            // 
            _btnLamMoi.BackColor = Color.Peru;
            _btnLamMoi.Controls.Add(lblBtnLamMoi);
            _btnLamMoi.CornerRadius = 10;
            _btnLamMoi.Location = new Point(18, 641);
            _btnLamMoi.Name = "_btnLamMoi";
            _btnLamMoi.Size = new Size(86, 40);
            _btnLamMoi.TabIndex = 9;
            _btnLamMoi.Click += BtnLamMoi_Click;
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
            lblBtnLamMoi.Click += BtnLamMoi_Click;
            // 
            // LichSuHoaDon
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.mt;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1258, 837);
            Controls.Add(roundedPanel1);
            DoubleBuffered = true;
            Name = "LichSuHoaDon";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Lịch sử hóa đơn";
            Load += LichSuHoaDon_Load;
            roundedPanel1.ResumeLayout(false);
            roundedPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pb_Admin).EndInit();
            btn_DangXuat.ResumeLayout(false);
            btn_DangXuat.PerformLayout();
            hcnt_KhungMenuAD.ResumeLayout(false);
            hcnt_KhungMenuAD.PerformLayout();
            btn_ThongKe.ResumeLayout(false);
            btn_ThongKe.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pb_ThongKe).EndInit();
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
            btn_LSHD.ResumeLayout(false);
            btn_LSHD.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pb_LSHD).EndInit();
            hcnt_Khung.ResumeLayout(false);
            hcnt_Khung.PerformLayout();
            pnlBoLoc.ResumeLayout(false);
            pnlBoLoc.PerformLayout();
            pnlCard3.ResumeLayout(false);
            pnlCard3.PerformLayout();
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
            _btnXuatBaoCao.ResumeLayout(false);
            _btnXuatBaoCao.PerformLayout();
            _btnInLai.ResumeLayout(false);
            _btnInLai.PerformLayout();
            _btnLamMoi.ResumeLayout(false);
            _btnLamMoi.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private RoundedPanel roundedPanel1;
        private PictureBox pb_Admin;
        private Label lb_Admin;
        private RoundedPanel btn_DangXuat;
        private Label lb_DangXuat;
        private RoundedPanel hcnt_KhungMenuAD;
        private Label lb_DMQL;
        private RoundedPanel btn_LSHD;
        private PictureBox pb_LSHD;
        private Label lblLSHD;
        private RoundedPanel btn_QLNCC;
        private PictureBox pb_QLNCC;
        private Label lblQLNCC;
        private RoundedPanel btn_QLKH;
        private PictureBox pb_QLKH;
        private Label lblQLKH;
        private RoundedPanel btn_QLNV;
        private PictureBox pb_QLNV;
        private Label lblQLNV;
        private RoundedPanel btn_QLMA;
        private PictureBox pb_QLMA;
        private Label lblQLMA;
        private RoundedPanel btn_QLHDN;
        private PictureBox pb_QLHDN;
        private Label lblQLHDN;
        private RoundedPanel btn_ThongKe;
        private PictureBox pb_ThongKe;
        private Label lblThongKe;
        private RoundedPanel hcnt_Khung;
        private Label lblTitle;
        private RoundedPanel pnlBoLoc;
        private Button _btnHoaDonBan;
        private Button _btnHoaDonNhap;
        private Label lblTuNgay;
        private DateTimePicker _dtpTuNgay;
        private Label lblDenNgay;
        private DateTimePicker _dtpDenNgay;
        private ComboBox _cboLocDoiTuong;
        private RoundedPanel pnlTimMa;
        private TextBox _txtTimMaHD;
        private RoundedPanel pnlCard3;
        private Label lblCard3Title;
        private Label _lblSoDonHuyValue;
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
        private RoundedPanel _btnXuatBaoCao;
        private Label lblBtnXuatBaoCao;
        private RoundedPanel _btnInLai;
        private Label lblBtnInLai;
        private RoundedPanel _btnLamMoi;
        private Label lblBtnLamMoi;
    }
}
