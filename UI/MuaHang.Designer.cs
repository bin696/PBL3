using PBL3.UI;

namespace PBL3
{
    partial class MuaHang
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
            lblPhieuNhap = new Label();
            lblNguyenLieu = new Label();
            lblDonGia = new Label();
            lblSoLuong = new Label();
            lblDonViTinh = new Label();
            lblNhaCungCap = new Label();
            lblTongTien = new Label();
            btnLuuNhapHang = new Button();
            btnXoaDong = new Button();
            btnThem = new Button();
            txtDonGia = new TextBox();
            nudSoLuong = new NumericUpDown();
            cboDonViTinh = new ComboBox();
            cboNhaCungCap = new ComboBox();
            dgvPhieuNhap = new DataGridView();
            dgvNguyenLieu = new DataGridView();
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
            ((System.ComponentModel.ISupportInitialize)nudSoLuong).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvPhieuNhap).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvNguyenLieu).BeginInit();
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
            hcnt_Khung.Controls.Add(lblPhieuNhap);
            hcnt_Khung.Controls.Add(lblNguyenLieu);
            hcnt_Khung.Controls.Add(lblDonGia);
            hcnt_Khung.Controls.Add(lblSoLuong);
            hcnt_Khung.Controls.Add(lblDonViTinh);
            hcnt_Khung.Controls.Add(lblNhaCungCap);
            hcnt_Khung.Controls.Add(lblTongTien);
            hcnt_Khung.Controls.Add(btnLuuNhapHang);
            hcnt_Khung.Controls.Add(btnXoaDong);
            hcnt_Khung.Controls.Add(btnThem);
            hcnt_Khung.Controls.Add(txtDonGia);
            hcnt_Khung.Controls.Add(nudSoLuong);
            hcnt_Khung.Controls.Add(cboDonViTinh);
            hcnt_Khung.Controls.Add(cboNhaCungCap);
            hcnt_Khung.Controls.Add(dgvPhieuNhap);
            hcnt_Khung.Controls.Add(dgvNguyenLieu);
            hcnt_Khung.Location = new Point(342, 65);
            hcnt_Khung.Name = "hcnt_Khung";
            hcnt_Khung.Size = new Size(870, 717);
            hcnt_Khung.TabIndex = 1;
            // 
            // lblPhieuNhap
            // 
            lblPhieuNhap.AutoSize = true;
            lblPhieuNhap.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblPhieuNhap.ForeColor = Color.SaddleBrown;
            lblPhieuNhap.Location = new Point(18, 380);
            lblPhieuNhap.Name = "lblPhieuNhap";
            lblPhieuNhap.Size = new Size(118, 28);
            lblPhieuNhap.TabIndex = 15;
            lblPhieuNhap.Text = "Phiếu nhập";
            // 
            // lblNguyenLieu
            // 
            lblNguyenLieu.AutoSize = true;
            lblNguyenLieu.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblNguyenLieu.ForeColor = Color.SaddleBrown;
            lblNguyenLieu.Location = new Point(18, 20);
            lblNguyenLieu.Name = "lblNguyenLieu";
            lblNguyenLieu.Size = new Size(225, 28);
            lblNguyenLieu.TabIndex = 14;
            lblNguyenLieu.Text = "Danh mục nguyên liệu";
            // 
            // lblDonGia
            // 
            lblDonGia.AutoSize = true;
            lblDonGia.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblDonGia.Location = new Point(493, 283);
            lblDonGia.Name = "lblDonGia";
            lblDonGia.Size = new Size(63, 20);
            lblDonGia.TabIndex = 13;
            lblDonGia.Text = "Đơn giá";
            // 
            // lblSoLuong
            // 
            lblSoLuong.AutoSize = true;
            lblSoLuong.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblSoLuong.Location = new Point(493, 230);
            lblSoLuong.Name = "lblSoLuong";
            lblSoLuong.Size = new Size(71, 20);
            lblSoLuong.TabIndex = 12;
            lblSoLuong.Text = "Số lượng";
            // 
            // lblDonViTinh
            // 
            lblDonViTinh.AutoSize = true;
            lblDonViTinh.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblDonViTinh.Location = new Point(493, 176);
            lblDonViTinh.Name = "lblDonViTinh";
            lblDonViTinh.Size = new Size(86, 20);
            lblDonViTinh.TabIndex = 11;
            lblDonViTinh.Text = "Đơn vị tính";
            // 
            // lblNhaCungCap
            // 
            lblNhaCungCap.AutoSize = true;
            lblNhaCungCap.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblNhaCungCap.Location = new Point(493, 26);
            lblNhaCungCap.Name = "lblNhaCungCap";
            lblNhaCungCap.Size = new Size(104, 20);
            lblNhaCungCap.TabIndex = 10;
            lblNhaCungCap.Text = "Nhà cung cấp";
            // 
            // lblTongTien
            // 
            lblTongTien.AutoSize = true;
            lblTongTien.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTongTien.ForeColor = Color.Firebrick;
            lblTongTien.Location = new Point(18, 679);
            lblTongTien.Name = "lblTongTien";
            lblTongTien.Size = new Size(145, 28);
            lblTongTien.TabIndex = 9;
            lblTongTien.Text = "Tổng tiền: 0 đ";
            // 
            // btnLuuNhapHang
            // 
            btnLuuNhapHang.BackColor = Color.Peru;
            btnLuuNhapHang.FlatStyle = FlatStyle.Flat;
            btnLuuNhapHang.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnLuuNhapHang.ForeColor = Color.White;
            btnLuuNhapHang.Location = new Point(715, 679);
            btnLuuNhapHang.Name = "btnLuuNhapHang";
            btnLuuNhapHang.Size = new Size(145, 35);
            btnLuuNhapHang.TabIndex = 8;
            btnLuuNhapHang.Text = "Lưu nhập hàng";
            btnLuuNhapHang.UseVisualStyleBackColor = false;
            btnLuuNhapHang.Click += btnLuuNhapHang_Click;
            // 
            // btnXoaDong
            // 
            btnXoaDong.BackColor = Color.IndianRed;
            btnXoaDong.FlatStyle = FlatStyle.Flat;
            btnXoaDong.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnXoaDong.ForeColor = Color.White;
            btnXoaDong.Location = new Point(581, 678);
            btnXoaDong.Name = "btnXoaDong";
            btnXoaDong.Size = new Size(128, 35);
            btnXoaDong.TabIndex = 7;
            btnXoaDong.Text = "Xóa dòng";
            btnXoaDong.UseVisualStyleBackColor = false;
            btnXoaDong.Click += btnXoaDong_Click;
            // 
            // btnThem
            // 
            btnThem.BackColor = Color.SandyBrown;
            btnThem.FlatStyle = FlatStyle.Flat;
            btnThem.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnThem.ForeColor = Color.White;
            btnThem.Location = new Point(493, 339);
            btnThem.Name = "btnThem";
            btnThem.Size = new Size(323, 38);
            btnThem.TabIndex = 6;
            btnThem.Text = "Thêm vào phiếu nhập";
            btnThem.UseVisualStyleBackColor = false;
            btnThem.Click += btnThem_Click;
            // 
            // txtDonGia
            // 
            txtDonGia.Location = new Point(493, 306);
            txtDonGia.Name = "txtDonGia";
            txtDonGia.Size = new Size(323, 27);
            txtDonGia.TabIndex = 5;
            // 
            // nudSoLuong
            // 
            nudSoLuong.DecimalPlaces = 2;
            nudSoLuong.Location = new Point(493, 253);
            nudSoLuong.Minimum = new decimal(new int[] { 1, 0, 0, 131072 });
            nudSoLuong.Name = "nudSoLuong";
            nudSoLuong.Size = new Size(180, 27);
            nudSoLuong.TabIndex = 4;
            nudSoLuong.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // cboDonViTinh
            // 
            cboDonViTinh.DropDownStyle = ComboBoxStyle.DropDownList;
            cboDonViTinh.FormattingEnabled = true;
            cboDonViTinh.Location = new Point(493, 199);
            cboDonViTinh.Name = "cboDonViTinh";
            cboDonViTinh.Size = new Size(323, 28);
            cboDonViTinh.TabIndex = 3;
            // 
            // cboNhaCungCap
            // 
            cboNhaCungCap.DropDownStyle = ComboBoxStyle.DropDownList;
            cboNhaCungCap.FormattingEnabled = true;
            cboNhaCungCap.Location = new Point(493, 50);
            cboNhaCungCap.Name = "cboNhaCungCap";
            cboNhaCungCap.Size = new Size(323, 28);
            cboNhaCungCap.TabIndex = 2;
            // 
            // dgvPhieuNhap
            // 
            dgvPhieuNhap.AllowUserToAddRows = false;
            dgvPhieuNhap.AllowUserToDeleteRows = false;
            dgvPhieuNhap.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPhieuNhap.BackgroundColor = Color.WhiteSmoke;
            dgvPhieuNhap.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPhieuNhap.Location = new Point(18, 412);
            dgvPhieuNhap.MultiSelect = false;
            dgvPhieuNhap.Name = "dgvPhieuNhap";
            dgvPhieuNhap.ReadOnly = true;
            dgvPhieuNhap.RowHeadersVisible = false;
            dgvPhieuNhap.RowHeadersWidth = 51;
            dgvPhieuNhap.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPhieuNhap.Size = new Size(842, 260);
            dgvPhieuNhap.TabIndex = 1;
            // 
            // dgvNguyenLieu
            // 
            dgvNguyenLieu.AllowUserToAddRows = false;
            dgvNguyenLieu.AllowUserToDeleteRows = false;
            dgvNguyenLieu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvNguyenLieu.BackgroundColor = Color.WhiteSmoke;
            dgvNguyenLieu.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvNguyenLieu.Location = new Point(18, 52);
            dgvNguyenLieu.MultiSelect = false;
            dgvNguyenLieu.Name = "dgvNguyenLieu";
            dgvNguyenLieu.ReadOnly = true;
            dgvNguyenLieu.RowHeadersVisible = false;
            dgvNguyenLieu.RowHeadersWidth = 51;
            dgvNguyenLieu.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvNguyenLieu.Size = new Size(458, 325);
            dgvNguyenLieu.TabIndex = 0;
            dgvNguyenLieu.SelectionChanged += dgvNguyenLieu_SelectionChanged;
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
            btn_QLHDN.BackColor = Color.Bisque;
            btn_QLHDN.Controls.Add(pb_QLHDN);
            btn_QLHDN.Controls.Add(label8);
            btn_QLHDN.Location = new Point(21, 371);
            btn_QLHDN.Name = "btn_QLHDN";
            btn_QLHDN.Size = new Size(256, 53);
            btn_QLHDN.TabIndex = 1;
            btn_QLHDN.Click += btn_QLHDN_Click;
            // 
            // pb_QLHDN
            // 
            pb_QLHDN.Image = Properties.Resources.nguyenlieu;
            pb_QLHDN.Location = new Point(10, 0);
            pb_QLHDN.Name = "pb_QLHDN";
            pb_QLHDN.Size = new Size(51, 51);
            pb_QLHDN.SizeMode = PictureBoxSizeMode.StretchImage;
            pb_QLHDN.TabIndex = 2;
            pb_QLHDN.TabStop = false;
            pb_QLHDN.Click += btn_QLHDN_Click;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 12F);
            label8.Location = new Point(58, 11);
            label8.Name = "label8";
            label8.Size = new Size(114, 28);
            label8.TabIndex = 0;
            label8.Text = "Khách hàng";
            label8.Click += btn_QLHDN_Click;
            // 
            // btn_QLMA
            // 
            btn_QLMA.BackColor = Color.Salmon;
            btn_QLMA.Controls.Add(pb_QLMA);
            btn_QLMA.Controls.Add(label7);
            btn_QLMA.Location = new Point(21, 297);
            btn_QLMA.Name = "btn_QLMA";
            btn_QLMA.Size = new Size(256, 53);
            btn_QLMA.TabIndex = 1;
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
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 12F);
            label7.ForeColor = Color.White;
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
            btn_QLKH.Name = "btn_QLKH";
            btn_QLKH.Size = new Size(256, 53);
            btn_QLKH.TabIndex = 1;
            btn_QLKH.Click += btn_QLKH_Click;
            btn_QLKH.Paint += btn_QLKH_Paint;
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
            // MuaHang
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.mt;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1236, 837);
            Controls.Add(roundedPanel1);
            DoubleBuffered = true;
            Name = "MuaHang";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Mua hàng";
            Load += MuaHang_Load;
            roundedPanel1.ResumeLayout(false);
            roundedPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pb_Admin).EndInit();
            btn_DangXuat.ResumeLayout(false);
            btn_DangXuat.PerformLayout();
            hcnt_Khung.ResumeLayout(false);
            hcnt_Khung.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudSoLuong).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvPhieuNhap).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvNguyenLieu).EndInit();
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
        private DataGridView dgvNguyenLieu;
        private DataGridView dgvPhieuNhap;
        private ComboBox cboNhaCungCap;
        private ComboBox cboDonViTinh;
        private NumericUpDown nudSoLuong;
        private TextBox txtDonGia;
        private Button btnThem;
        private Button btnXoaDong;
        private Button btnLuuNhapHang;
        private Label lblTongTien;
        private Label lblNhaCungCap;
        private Label lblDonViTinh;
        private Label lblSoLuong;
        private Label lblDonGia;
        private Label lblNguyenLieu;
        private Label lblPhieuNhap;
    }
}

