using PBL3.UI;
// charts are represented by DataGridView placeholders to avoid requiring Charting assembly

namespace PBL3
{
    partial class ThongKe
    {
        // designer-added labels for grid titles and date range
        private Label lblRevenueTitle;
        private Label lblHourTitle;
        private Label lblCategoryTitle;
        private Label lblTopItemsTitle;
        private Label lblLowStockTitle;
        private Label lblDateRange;
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
            lblTotalRevenue = new Label();
            lblTotalProfit = new Label();
            lblTotalOrders = new Label();
            lblNewCustomers = new Label();
            cboDateFilter = new ComboBox();
            dtpFrom = new DateTimePicker();
            dtpTo = new DateTimePicker();
            dgvRevenue = new DataGridView();
            dgvOrdersByHour = new DataGridView();
            dgvCategoryShare = new DataGridView();
            lblRevenueTitle = new Label();
            lblHourTitle = new Label();
            lblCategoryTitle = new Label();
            lblTopItemsTitle = new Label();
            lblLowStockTitle = new Label();
            lblDateRange = new Label();
            dgvTopItems = new DataGridView();
            dgvLowStock = new DataGridView();
            roundedPanel1 = new RoundedPanel();
            pb_Admin = new PictureBox();
            btn_DangXuat = new RoundedPanel();
            lb_DangXuat = new Label();
            lb_Admin = new Label();
            hcnt_Khung = new RoundedPanel();
            roundedPanel2 = new RoundedPanel();
            label2 = new Label();
            btnRefresh = new RoundedPanel();
            label1 = new Label();
            lb_QLNhanVienTitle = new Label();
            hcnt_KhungMenuAD = new RoundedPanel();
            btn_ThongKe = new RoundedPanel();
            pb_ThongKe = new PictureBox();
            label10 = new Label();
            btn_QLHDB = new RoundedPanel();
            pb_QLHDB = new PictureBox();
            label9 = new Label();
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
            _btnLamMoi = new RoundedPanel();
            lblBtnLamMoi = new Label();
            _btnXoa = new RoundedPanel();
            lblBtnXoa = new Label();
            pnlSdtInput = new RoundedPanel();
            _txtSdt = new TextBox();
            _btnXemLichTruc = new RoundedPanel();
            _lblBtnXemLichTruc = new Label();
            _btnSua = new RoundedPanel();
            lblBtnSua = new Label();
            _btnDatLaiMatKhau = new RoundedPanel();
            _lblBtnDatLaiMatKhau = new Label();
            lblSdt = new Label();
            _btnThem = new RoundedPanel();
            lblBtnThem = new Label();
            pnlTimKiem = new RoundedPanel();
            _txtTimKiem = new TextBox();
            _cboChucVu = new ComboBox();
            lblChucVu = new Label();
            _cboTrangThai = new ComboBox();
            lblTrangThai = new Label();
            pnlMatKhauInput = new RoundedPanel();
            _txtMatKhau = new TextBox();
            lblMatKhau = new Label();
            pnlDiaChiInput = new RoundedPanel();
            _txtDiaChi = new TextBox();
            lblDiaChi = new Label();
            _dtpNgaySinh = new DateTimePicker();
            lblNgaySinh = new Label();
            pnlHoTenInput = new RoundedPanel();
            _txtHoTen = new TextBox();
            lblHoTen = new Label();
            pnlMaNVInput = new RoundedPanel();
            _txtMaNV = new TextBox();
            lblMaNV = new Label();
            _cboTimTheo = new ComboBox();
            _dgvNhanVien = new DataGridView();
            lb_DSNhanVien = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvRevenue).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvOrdersByHour).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvCategoryShare).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvTopItems).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvLowStock).BeginInit();
            roundedPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pb_Admin).BeginInit();
            btn_DangXuat.SuspendLayout();
            hcnt_Khung.SuspendLayout();
            roundedPanel2.SuspendLayout();
            btnRefresh.SuspendLayout();
            hcnt_KhungMenuAD.SuspendLayout();
            btn_ThongKe.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pb_ThongKe).BeginInit();
            btn_QLHDB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pb_QLHDB).BeginInit();
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
            ((System.ComponentModel.ISupportInitialize)_dgvNhanVien).BeginInit();
            SuspendLayout();
            // 
            // lblTotalRevenue
            // 
            lblTotalRevenue.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTotalRevenue.Location = new Point(35, 37);
            lblTotalRevenue.Name = "lblTotalRevenue";
            lblTotalRevenue.Size = new Size(175, 26);
            lblTotalRevenue.TabIndex = 7;
            lblTotalRevenue.Text = "Tổng doanh thu: 0đ";
            // 
            // lblTotalProfit
            // 
            lblTotalProfit.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTotalProfit.Location = new Point(228, 41);
            lblTotalProfit.Name = "lblTotalProfit";
            lblTotalProfit.Size = new Size(175, 26);
            lblTotalProfit.TabIndex = 8;
            lblTotalProfit.Text = "Tổng lợi nhuận: 0 đ";
            // 
            // lblTotalOrders
            // 
            lblTotalOrders.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTotalOrders.Location = new Point(431, 37);
            lblTotalOrders.Name = "lblTotalOrders";
            lblTotalOrders.Size = new Size(147, 26);
            lblTotalOrders.TabIndex = 9;
            lblTotalOrders.Text = "Tổng số đơn: 0";
            // 
            // lblNewCustomers
            // 
            lblNewCustomers.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblNewCustomers.Location = new Point(595, 37);
            lblNewCustomers.Name = "lblNewCustomers";
            lblNewCustomers.Size = new Size(141, 26);
            lblNewCustomers.TabIndex = 10;
            lblNewCustomers.Text = "Khách mới: 0";
            // 
            // cboDateFilter
            // 
            cboDateFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cboDateFilter.Items.AddRange(new object[] { "Hôm nay", "Hôm qua", "7 ngày qua", "Tháng này", "Tùy chỉnh" });
            cboDateFilter.Location = new Point(35, 69);
            cboDateFilter.Margin = new Padding(3, 2, 3, 2);
            cboDateFilter.Name = "cboDateFilter";
            cboDateFilter.Size = new Size(176, 23);
            cboDateFilter.TabIndex = 11;
            cboDateFilter.SelectedIndexChanged += cboDateFilter_SelectedIndexChanged;
            // 
            // dtpFrom
            // 
            dtpFrom.Format = DateTimePickerFormat.Short;
            dtpFrom.Location = new Point(228, 69);
            dtpFrom.Margin = new Padding(3, 2, 3, 2);
            dtpFrom.Name = "dtpFrom";
            dtpFrom.Size = new Size(123, 23);
            dtpFrom.TabIndex = 12;
            // 
            // dtpTo
            // 
            dtpTo.Format = DateTimePickerFormat.Short;
            dtpTo.Location = new Point(368, 69);
            dtpTo.Margin = new Padding(3, 2, 3, 2);
            dtpTo.Name = "dtpTo";
            dtpTo.Size = new Size(123, 23);
            dtpTo.TabIndex = 13;
            // 
            // dgvRevenue
            // 
            dgvRevenue.AllowUserToAddRows = false;
            dgvRevenue.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvRevenue.BackgroundColor = Color.WhiteSmoke;
            dgvRevenue.BorderStyle = BorderStyle.None;
            dgvRevenue.Location = new Point(35, 122);
            dgvRevenue.Margin = new Padding(3, 2, 3, 2);
            dgvRevenue.Name = "dgvRevenue";
            dgvRevenue.Size = new Size(220, 210);
            dgvRevenue.TabIndex = 22;
            // 
            // dgvOrdersByHour
            // 
            dgvOrdersByHour.AllowUserToAddRows = false;
            dgvOrdersByHour.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvOrdersByHour.BackgroundColor = Color.WhiteSmoke;
            dgvOrdersByHour.BorderStyle = BorderStyle.None;
            dgvOrdersByHour.Location = new Point(276, 122);
            dgvOrdersByHour.Margin = new Padding(3, 2, 3, 2);
            dgvOrdersByHour.Name = "dgvOrdersByHour";
            dgvOrdersByHour.Size = new Size(220, 210);
            dgvOrdersByHour.TabIndex = 23;
            // 
            // dgvCategoryShare
            // 
            dgvCategoryShare.AllowUserToAddRows = false;
            dgvCategoryShare.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCategoryShare.BackgroundColor = Color.WhiteSmoke;
            dgvCategoryShare.BorderStyle = BorderStyle.None;
            dgvCategoryShare.Location = new Point(516, 122);
            dgvCategoryShare.Margin = new Padding(3, 2, 3, 2);
            dgvCategoryShare.Name = "dgvCategoryShare";
            dgvCategoryShare.Size = new Size(220, 210);
            dgvCategoryShare.TabIndex = 24;
            // 
            // lblRevenueTitle
            // 
            lblRevenueTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblRevenueTitle.Location = new Point(35, 106);
            lblRevenueTitle.Name = "lblRevenueTitle";
            lblRevenueTitle.Size = new Size(126, 15);
            lblRevenueTitle.TabIndex = 17;
            lblRevenueTitle.Text = "Doanh thu theo ngày";
            // 
            // lblHourTitle
            // 
            lblHourTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblHourTitle.Location = new Point(276, 106);
            lblHourTitle.Name = "lblHourTitle";
            lblHourTitle.Size = new Size(86, 15);
            lblHourTitle.TabIndex = 18;
            lblHourTitle.Text = "Đơn theo giờ";
            lblHourTitle.Click += lblHourTitle_Click;
            // 
            // lblCategoryTitle
            // 
            lblCategoryTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblCategoryTitle.Location = new Point(516, 105);
            lblCategoryTitle.Name = "lblCategoryTitle";
            lblCategoryTitle.Size = new Size(139, 15);
            lblCategoryTitle.TabIndex = 19;
            lblCategoryTitle.Text = "Doanh thu theo nhóm";
            // 
            // lblTopItemsTitle
            // 
            lblTopItemsTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTopItemsTitle.Location = new Point(35, 333);
            lblTopItemsTitle.Name = "lblTopItemsTitle";
            lblTopItemsTitle.Size = new Size(108, 15);
            lblTopItemsTitle.TabIndex = 20;
            lblTopItemsTitle.Text = "Top 5 món bán chạy";
            // 
            // lblLowStockTitle
            // 
            lblLowStockTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblLowStockTitle.Location = new Point(407, 333);
            lblLowStockTitle.Name = "lblLowStockTitle";
            lblLowStockTitle.Size = new Size(122, 15);
            lblLowStockTitle.TabIndex = 21;
            lblLowStockTitle.Text = "Nguyên liệu sắp hết";
            // 
            // lblDateRange
            // 
            lblDateRange.Font = new Font("Segoe UI", 8F, FontStyle.Italic);
            lblDateRange.ForeColor = Color.DimGray;
            lblDateRange.Location = new Point(35, 92);
            lblDateRange.Name = "lblDateRange";
            lblDateRange.Size = new Size(350, 14);
            lblDateRange.TabIndex = 16;
            // 
            // dgvTopItems
            // 
            dgvTopItems.AllowUserToAddRows = false;
            dgvTopItems.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTopItems.Location = new Point(35, 350);
            dgvTopItems.Margin = new Padding(3, 2, 3, 2);
            dgvTopItems.Name = "dgvTopItems";
            dgvTopItems.Size = new Size(366, 154);
            dgvTopItems.TabIndex = 25;
            // 
            // dgvLowStock
            // 
            dgvLowStock.AllowUserToAddRows = false;
            dgvLowStock.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvLowStock.Location = new Point(407, 350);
            dgvLowStock.Margin = new Padding(3, 2, 3, 2);
            dgvLowStock.Name = "dgvLowStock";
            dgvLowStock.Size = new Size(327, 154);
            dgvLowStock.TabIndex = 26;
            // 
            // roundedPanel1
            // 
            roundedPanel1.Controls.Add(pb_Admin);
            roundedPanel1.Controls.Add(btn_DangXuat);
            roundedPanel1.Controls.Add(lb_Admin);
            roundedPanel1.Controls.Add(hcnt_Khung);
            roundedPanel1.Controls.Add(hcnt_KhungMenuAD);
            roundedPanel1.Location = new Point(12, 12);
            roundedPanel1.Name = "roundedPanel1";
            roundedPanel1.Size = new Size(1078, 608);
            roundedPanel1.TabIndex = 0;
            // 
            // pb_Admin
            // 
            pb_Admin.BackColor = SystemColors.Control;
            pb_Admin.Image = Properties.Resources.admin;
            pb_Admin.Location = new Point(17, 8);
            pb_Admin.Name = "pb_Admin";
            pb_Admin.Size = new Size(45, 38);
            pb_Admin.SizeMode = PictureBoxSizeMode.StretchImage;
            pb_Admin.TabIndex = 2;
            pb_Admin.TabStop = false;
            // 
            // btn_DangXuat
            // 
            btn_DangXuat.BackColor = Color.SandyBrown;
            btn_DangXuat.Controls.Add(lb_DangXuat);
            btn_DangXuat.Font = new Font("Segoe UI", 9F);
            btn_DangXuat.Location = new Point(170, 12);
            btn_DangXuat.Name = "btn_DangXuat";
            btn_DangXuat.Size = new Size(110, 25);
            btn_DangXuat.TabIndex = 3;
            btn_DangXuat.Click += btn_DangXuat_Click;
            btn_DangXuat.MouseEnter += btn_DangXuat_MouseEnter;
            btn_DangXuat.MouseLeave += btn_DangXuat_MouseLeave;
            // 
            // lb_DangXuat
            // 
            lb_DangXuat.AutoSize = true;
            lb_DangXuat.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lb_DangXuat.ForeColor = SystemColors.ButtonFace;
            lb_DangXuat.Location = new Point(16, 3);
            lb_DangXuat.Name = "lb_DangXuat";
            lb_DangXuat.Size = new Size(77, 19);
            lb_DangXuat.TabIndex = 2;
            lb_DangXuat.Text = "Đăng xuất";
            lb_DangXuat.Click += btn_DangXuat_Click;
            lb_DangXuat.MouseEnter += btn_DangXuat_MouseEnter;
            lb_DangXuat.MouseLeave += btn_DangXuat_MouseLeave;
            // 
            // lb_Admin
            // 
            lb_Admin.AutoSize = true;
            lb_Admin.Font = new Font("Segoe UI", 12F);
            lb_Admin.Location = new Point(58, 12);
            lb_Admin.Name = "lb_Admin";
            lb_Admin.Size = new Size(56, 21);
            lb_Admin.TabIndex = 2;
            lb_Admin.Text = "Admin";
            // 
            // hcnt_Khung
            // 
            hcnt_Khung.BackColor = Color.Linen;
            hcnt_Khung.Controls.Add(roundedPanel2);
            hcnt_Khung.Controls.Add(btnRefresh);
            hcnt_Khung.Controls.Add(lb_QLNhanVienTitle);
            hcnt_Khung.Controls.Add(lblTotalRevenue);
            hcnt_Khung.Controls.Add(lblTotalProfit);
            hcnt_Khung.Controls.Add(lblTotalOrders);
            hcnt_Khung.Controls.Add(lblNewCustomers);
            hcnt_Khung.Controls.Add(cboDateFilter);
            hcnt_Khung.Controls.Add(dtpFrom);
            hcnt_Khung.Controls.Add(dtpTo);
            hcnt_Khung.Controls.Add(lblDateRange);
            hcnt_Khung.Controls.Add(lblRevenueTitle);
            hcnt_Khung.Controls.Add(lblHourTitle);
            hcnt_Khung.Controls.Add(lblCategoryTitle);
            hcnt_Khung.Controls.Add(lblTopItemsTitle);
            hcnt_Khung.Controls.Add(lblLowStockTitle);
            hcnt_Khung.Controls.Add(dgvRevenue);
            hcnt_Khung.Controls.Add(dgvOrdersByHour);
            hcnt_Khung.Controls.Add(dgvCategoryShare);
            hcnt_Khung.Controls.Add(dgvTopItems);
            hcnt_Khung.Controls.Add(dgvLowStock);
            hcnt_Khung.Location = new Point(299, 49);
            hcnt_Khung.Name = "hcnt_Khung";
            hcnt_Khung.Size = new Size(761, 538);
            hcnt_Khung.TabIndex = 1;
            // 
            // roundedPanel2
            // 
            roundedPanel2.BackColor = Color.Peru;
            roundedPanel2.Controls.Add(label2);
            roundedPanel2.CornerRadius = 10;
            roundedPanel2.Location = new Point(659, 67);
            roundedPanel2.Margin = new Padding(3, 2, 3, 2);
            roundedPanel2.Name = "roundedPanel2";
            roundedPanel2.Size = new Size(75, 24);
            roundedPanel2.TabIndex = 27;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label2.ForeColor = Color.White;
            label2.Location = new Point(2, 3);
            label2.Name = "label2";
            label2.Size = new Size(69, 19);
            label2.TabIndex = 0;
            label2.Text = "Xuất CSV";
            // 
            // btnRefresh
            // 
            btnRefresh.BackColor = Color.Peru;
            btnRefresh.Controls.Add(label1);
            btnRefresh.CornerRadius = 10;
            btnRefresh.Location = new Point(578, 67);
            btnRefresh.Margin = new Padding(3, 2, 3, 2);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(75, 24);
            btnRefresh.TabIndex = 27;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label1.ForeColor = Color.White;
            label1.Location = new Point(2, 3);
            label1.Name = "label1";
            label1.Size = new Size(67, 19);
            label1.TabIndex = 0;
            label1.Text = "Làm mới";
            // 
            // lb_QLNhanVienTitle
            // 
            lb_QLNhanVienTitle.AutoSize = true;
            lb_QLNhanVienTitle.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            lb_QLNhanVienTitle.ForeColor = Color.Salmon;
            lb_QLNhanVienTitle.Location = new Point(306, 6);
            lb_QLNhanVienTitle.Name = "lb_QLNhanVienTitle";
            lb_QLNhanVienTitle.Size = new Size(100, 28);
            lb_QLNhanVienTitle.TabIndex = 4;
            lb_QLNhanVienTitle.Text = "Thống kê";
            // 
            // hcnt_KhungMenuAD
            // 
            hcnt_KhungMenuAD.BackColor = Color.Linen;
            hcnt_KhungMenuAD.Controls.Add(btn_ThongKe);
            hcnt_KhungMenuAD.Controls.Add(btn_QLHDB);
            hcnt_KhungMenuAD.Controls.Add(btn_QLHDN);
            hcnt_KhungMenuAD.Controls.Add(btn_QLMA);
            hcnt_KhungMenuAD.Controls.Add(btn_QLKH);
            hcnt_KhungMenuAD.Controls.Add(btn_QLNCC);
            hcnt_KhungMenuAD.Controls.Add(btn_QLNV);
            hcnt_KhungMenuAD.Controls.Add(lb_DMQL);
            hcnt_KhungMenuAD.Location = new Point(17, 49);
            hcnt_KhungMenuAD.Name = "hcnt_KhungMenuAD";
            hcnt_KhungMenuAD.Size = new Size(263, 538);
            hcnt_KhungMenuAD.TabIndex = 0;
            // 
            // btn_ThongKe
            // 
            btn_ThongKe.BackColor = Color.Salmon;
            btn_ThongKe.Controls.Add(pb_ThongKe);
            btn_ThongKe.Controls.Add(label10);
            btn_ThongKe.Location = new Point(18, 388);
            btn_ThongKe.Name = "btn_ThongKe";
            btn_ThongKe.Size = new Size(224, 40);
            btn_ThongKe.TabIndex = 1;
            btn_ThongKe.Click += btn_ThongKe_Click;
            // 
            // pb_ThongKe
            // 
            pb_ThongKe.Image = Properties.Resources.thongke;
            pb_ThongKe.Location = new Point(9, 0);
            pb_ThongKe.Name = "pb_ThongKe";
            pb_ThongKe.Size = new Size(45, 38);
            pb_ThongKe.SizeMode = PictureBoxSizeMode.StretchImage;
            pb_ThongKe.TabIndex = 2;
            pb_ThongKe.TabStop = false;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            label10.ForeColor = Color.White;
            label10.Location = new Point(51, 8);
            label10.Name = "label10";
            label10.Size = new Size(81, 21);
            label10.TabIndex = 0;
            label10.Text = "Thống kê";
            label10.Click += btn_ThongKe_Click;
            // 
            // btn_QLHDB
            // 
            btn_QLHDB.BackColor = Color.Bisque;
            btn_QLHDB.Controls.Add(pb_QLHDB);
            btn_QLHDB.Controls.Add(label9);
            btn_QLHDB.Location = new Point(18, 332);
            btn_QLHDB.Name = "btn_QLHDB";
            btn_QLHDB.Size = new Size(224, 40);
            btn_QLHDB.TabIndex = 1;
            btn_QLHDB.Click += btn_QLHDB_Click;
            // 
            // pb_QLHDB
            // 
            pb_QLHDB.Image = Properties.Resources.hoadonban1;
            pb_QLHDB.Location = new Point(9, 1);
            pb_QLHDB.Name = "pb_QLHDB";
            pb_QLHDB.Size = new Size(45, 38);
            pb_QLHDB.SizeMode = PictureBoxSizeMode.StretchImage;
            pb_QLHDB.TabIndex = 2;
            pb_QLHDB.TabStop = false;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 12F);
            label9.ForeColor = SystemColors.ControlText;
            label9.Location = new Point(51, 8);
            label9.Name = "label9";
            label9.Size = new Size(120, 21);
            label9.TabIndex = 0;
            label9.Text = "Lịch sử hóa đơn";
            label9.Click += btn_QLHDB_Click;
            // 
            // btn_QLHDN
            // 
            btn_QLHDN.BackColor = Color.Bisque;
            btn_QLHDN.Controls.Add(pb_QLHDN);
            btn_QLHDN.Controls.Add(label8);
            btn_QLHDN.Location = new Point(18, 277);
            btn_QLHDN.Name = "btn_QLHDN";
            btn_QLHDN.Size = new Size(224, 40);
            btn_QLHDN.TabIndex = 1;
            btn_QLHDN.Click += btn_QLHDN_Click;
            // 
            // pb_QLHDN
            // 
            pb_QLHDN.Image = Properties.Resources.nguyenlieu;
            pb_QLHDN.Location = new Point(9, 0);
            pb_QLHDN.Name = "pb_QLHDN";
            pb_QLHDN.Size = new Size(45, 38);
            pb_QLHDN.SizeMode = PictureBoxSizeMode.StretchImage;
            pb_QLHDN.TabIndex = 2;
            pb_QLHDN.TabStop = false;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 12F);
            label8.ForeColor = SystemColors.ControlText;
            label8.Location = new Point(51, 8);
            label8.Name = "label8";
            label8.Size = new Size(145, 21);
            label8.TabIndex = 0;
            label8.Text = "Quản lí nguyên liệu";
            label8.Click += btn_QLHDN_Click;
            // 
            // btn_QLMA
            // 
            btn_QLMA.BackColor = Color.Bisque;
            btn_QLMA.Controls.Add(pb_QLMA);
            btn_QLMA.Controls.Add(label7);
            btn_QLMA.Location = new Point(18, 222);
            btn_QLMA.Name = "btn_QLMA";
            btn_QLMA.Size = new Size(224, 40);
            btn_QLMA.TabIndex = 1;
            btn_QLMA.Click += btn_QLMA_Click;
            // 
            // pb_QLMA
            // 
            pb_QLMA.Image = Properties.Resources.monan;
            pb_QLMA.Location = new Point(9, 1);
            pb_QLMA.Name = "pb_QLMA";
            pb_QLMA.Size = new Size(45, 38);
            pb_QLMA.SizeMode = PictureBoxSizeMode.StretchImage;
            pb_QLMA.TabIndex = 2;
            pb_QLMA.TabStop = false;
            pb_QLMA.Click += btn_QLMA_Click;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 12F);
            label7.ForeColor = SystemColors.ControlText;
            label7.Location = new Point(51, 8);
            label7.Name = "label7";
            label7.Size = new Size(117, 21);
            label7.TabIndex = 0;
            label7.Text = "Quản lí món ăn";
            label7.Click += btn_QLMA_Click;
            // 
            // btn_QLKH
            // 
            btn_QLKH.BackColor = Color.Bisque;
            btn_QLKH.Controls.Add(pb_QLKH);
            btn_QLKH.Controls.Add(label6);
            btn_QLKH.Location = new Point(18, 167);
            btn_QLKH.Name = "btn_QLKH";
            btn_QLKH.Size = new Size(224, 40);
            btn_QLKH.TabIndex = 1;
            btn_QLKH.Click += btn_QLKH_Click;
            // 
            // pb_QLKH
            // 
            pb_QLKH.Image = Properties.Resources.khachhang;
            pb_QLKH.Location = new Point(9, 0);
            pb_QLKH.Name = "pb_QLKH";
            pb_QLKH.Size = new Size(45, 38);
            pb_QLKH.SizeMode = PictureBoxSizeMode.StretchImage;
            pb_QLKH.TabIndex = 2;
            pb_QLKH.TabStop = false;
            pb_QLKH.Click += btn_QLKH_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 12F);
            label6.ForeColor = SystemColors.ControlText;
            label6.Location = new Point(51, 8);
            label6.Name = "label6";
            label6.Size = new Size(144, 21);
            label6.TabIndex = 0;
            label6.Text = "Quản lí khách hàng";
            label6.Click += btn_QLKH_Click;
            // 
            // btn_QLNCC
            // 
            btn_QLNCC.BackColor = Color.Bisque;
            btn_QLNCC.Controls.Add(pb_QLNCC);
            btn_QLNCC.Controls.Add(label5);
            btn_QLNCC.Location = new Point(18, 114);
            btn_QLNCC.Name = "btn_QLNCC";
            btn_QLNCC.Size = new Size(224, 40);
            btn_QLNCC.TabIndex = 1;
            btn_QLNCC.Click += btn_QLNCC_Click;
            // 
            // pb_QLNCC
            // 
            pb_QLNCC.Image = Properties.Resources.ncc;
            pb_QLNCC.Location = new Point(9, 4);
            pb_QLNCC.Name = "pb_QLNCC";
            pb_QLNCC.Size = new Size(45, 38);
            pb_QLNCC.SizeMode = PictureBoxSizeMode.StretchImage;
            pb_QLNCC.TabIndex = 2;
            pb_QLNCC.TabStop = false;
            pb_QLNCC.Click += btn_QLNCC_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 12F);
            label5.ForeColor = SystemColors.ControlText;
            label5.Location = new Point(51, 8);
            label5.Name = "label5";
            label5.Size = new Size(156, 21);
            label5.TabIndex = 0;
            label5.Text = "Quản lí nhà cung cấp";
            label5.Click += btn_QLNCC_Click;
            // 
            // btn_QLNV
            // 
            btn_QLNV.BackColor = Color.Bisque;
            btn_QLNV.Controls.Add(pb_QLNV);
            btn_QLNV.Controls.Add(label4);
            btn_QLNV.Location = new Point(18, 63);
            btn_QLNV.Name = "btn_QLNV";
            btn_QLNV.Size = new Size(224, 40);
            btn_QLNV.TabIndex = 1;
            btn_QLNV.Click += btn_QLNV_Click;
            // 
            // pb_QLNV
            // 
            pb_QLNV.Image = Properties.Resources.nhanvien;
            pb_QLNV.Location = new Point(9, 4);
            pb_QLNV.Name = "pb_QLNV";
            pb_QLNV.Size = new Size(45, 38);
            pb_QLNV.SizeMode = PictureBoxSizeMode.StretchImage;
            pb_QLNV.TabIndex = 2;
            pb_QLNV.TabStop = false;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 12F);
            label4.ForeColor = SystemColors.ControlText;
            label4.Location = new Point(51, 8);
            label4.Name = "label4";
            label4.Size = new Size(132, 21);
            label4.TabIndex = 0;
            label4.Text = "Quản lí nhân viên";
            label4.Click += btn_QLNV_Click;
            // 
            // lb_DMQL
            // 
            lb_DMQL.AutoSize = true;
            lb_DMQL.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            lb_DMQL.ForeColor = Color.Salmon;
            lb_DMQL.Location = new Point(35, 9);
            lb_DMQL.Name = "lb_DMQL";
            lb_DMQL.Size = new Size(182, 28);
            lb_DMQL.TabIndex = 0;
            lb_DMQL.Text = "Danh mục Quản lí";
            // 
            // _btnLamMoi
            // 
            _btnLamMoi.Location = new Point(0, 0);
            _btnLamMoi.Name = "_btnLamMoi";
            _btnLamMoi.Size = new Size(200, 100);
            _btnLamMoi.TabIndex = 0;
            // 
            // lblBtnLamMoi
            // 
            lblBtnLamMoi.Location = new Point(0, 0);
            lblBtnLamMoi.Name = "lblBtnLamMoi";
            lblBtnLamMoi.Size = new Size(100, 23);
            lblBtnLamMoi.TabIndex = 0;
            // 
            // _btnXoa
            // 
            _btnXoa.Location = new Point(0, 0);
            _btnXoa.Name = "_btnXoa";
            _btnXoa.Size = new Size(200, 100);
            _btnXoa.TabIndex = 0;
            // 
            // lblBtnXoa
            // 
            lblBtnXoa.Location = new Point(0, 0);
            lblBtnXoa.Name = "lblBtnXoa";
            lblBtnXoa.Size = new Size(100, 23);
            lblBtnXoa.TabIndex = 0;
            // 
            // pnlSdtInput
            // 
            pnlSdtInput.Location = new Point(0, 0);
            pnlSdtInput.Name = "pnlSdtInput";
            pnlSdtInput.Size = new Size(200, 100);
            pnlSdtInput.TabIndex = 0;
            // 
            // _txtSdt
            // 
            _txtSdt.Location = new Point(0, 0);
            _txtSdt.Name = "_txtSdt";
            _txtSdt.Size = new Size(100, 23);
            _txtSdt.TabIndex = 0;
            // 
            // _btnXemLichTruc
            // 
            _btnXemLichTruc.Location = new Point(0, 0);
            _btnXemLichTruc.Name = "_btnXemLichTruc";
            _btnXemLichTruc.Size = new Size(200, 100);
            _btnXemLichTruc.TabIndex = 0;
            // 
            // _lblBtnXemLichTruc
            // 
            _lblBtnXemLichTruc.Location = new Point(0, 0);
            _lblBtnXemLichTruc.Name = "_lblBtnXemLichTruc";
            _lblBtnXemLichTruc.Size = new Size(100, 23);
            _lblBtnXemLichTruc.TabIndex = 0;
            // 
            // _btnSua
            // 
            _btnSua.Location = new Point(0, 0);
            _btnSua.Name = "_btnSua";
            _btnSua.Size = new Size(200, 100);
            _btnSua.TabIndex = 0;
            // 
            // lblBtnSua
            // 
            lblBtnSua.Location = new Point(0, 0);
            lblBtnSua.Name = "lblBtnSua";
            lblBtnSua.Size = new Size(100, 23);
            lblBtnSua.TabIndex = 0;
            // 
            // _btnDatLaiMatKhau
            // 
            _btnDatLaiMatKhau.Location = new Point(0, 0);
            _btnDatLaiMatKhau.Name = "_btnDatLaiMatKhau";
            _btnDatLaiMatKhau.Size = new Size(200, 100);
            _btnDatLaiMatKhau.TabIndex = 0;
            // 
            // _lblBtnDatLaiMatKhau
            // 
            _lblBtnDatLaiMatKhau.Location = new Point(0, 0);
            _lblBtnDatLaiMatKhau.Name = "_lblBtnDatLaiMatKhau";
            _lblBtnDatLaiMatKhau.Size = new Size(100, 23);
            _lblBtnDatLaiMatKhau.TabIndex = 0;
            // 
            // lblSdt
            // 
            lblSdt.Location = new Point(0, 0);
            lblSdt.Name = "lblSdt";
            lblSdt.Size = new Size(100, 23);
            lblSdt.TabIndex = 0;
            // 
            // _btnThem
            // 
            _btnThem.Location = new Point(0, 0);
            _btnThem.Name = "_btnThem";
            _btnThem.Size = new Size(200, 100);
            _btnThem.TabIndex = 0;
            // 
            // lblBtnThem
            // 
            lblBtnThem.Location = new Point(0, 0);
            lblBtnThem.Name = "lblBtnThem";
            lblBtnThem.Size = new Size(100, 23);
            lblBtnThem.TabIndex = 0;
            // 
            // pnlTimKiem
            // 
            pnlTimKiem.Location = new Point(0, 0);
            pnlTimKiem.Name = "pnlTimKiem";
            pnlTimKiem.Size = new Size(200, 100);
            pnlTimKiem.TabIndex = 0;
            // 
            // _txtTimKiem
            // 
            _txtTimKiem.Location = new Point(0, 0);
            _txtTimKiem.Name = "_txtTimKiem";
            _txtTimKiem.Size = new Size(100, 23);
            _txtTimKiem.TabIndex = 0;
            // 
            // _cboChucVu
            // 
            _cboChucVu.Location = new Point(0, 0);
            _cboChucVu.Name = "_cboChucVu";
            _cboChucVu.Size = new Size(121, 23);
            _cboChucVu.TabIndex = 0;
            // 
            // lblChucVu
            // 
            lblChucVu.Location = new Point(0, 0);
            lblChucVu.Name = "lblChucVu";
            lblChucVu.Size = new Size(100, 23);
            lblChucVu.TabIndex = 0;
            // 
            // _cboTrangThai
            // 
            _cboTrangThai.Location = new Point(0, 0);
            _cboTrangThai.Name = "_cboTrangThai";
            _cboTrangThai.Size = new Size(121, 23);
            _cboTrangThai.TabIndex = 0;
            // 
            // lblTrangThai
            // 
            lblTrangThai.Location = new Point(0, 0);
            lblTrangThai.Name = "lblTrangThai";
            lblTrangThai.Size = new Size(100, 23);
            lblTrangThai.TabIndex = 0;
            // 
            // pnlMatKhauInput
            // 
            pnlMatKhauInput.Location = new Point(0, 0);
            pnlMatKhauInput.Name = "pnlMatKhauInput";
            pnlMatKhauInput.Size = new Size(200, 100);
            pnlMatKhauInput.TabIndex = 0;
            // 
            // _txtMatKhau
            // 
            _txtMatKhau.Location = new Point(0, 0);
            _txtMatKhau.Name = "_txtMatKhau";
            _txtMatKhau.Size = new Size(100, 23);
            _txtMatKhau.TabIndex = 0;
            // 
            // lblMatKhau
            // 
            lblMatKhau.Location = new Point(0, 0);
            lblMatKhau.Name = "lblMatKhau";
            lblMatKhau.Size = new Size(100, 23);
            lblMatKhau.TabIndex = 0;
            // 
            // pnlDiaChiInput
            // 
            pnlDiaChiInput.Location = new Point(0, 0);
            pnlDiaChiInput.Name = "pnlDiaChiInput";
            pnlDiaChiInput.Size = new Size(200, 100);
            pnlDiaChiInput.TabIndex = 0;
            // 
            // _txtDiaChi
            // 
            _txtDiaChi.Location = new Point(0, 0);
            _txtDiaChi.Name = "_txtDiaChi";
            _txtDiaChi.Size = new Size(100, 23);
            _txtDiaChi.TabIndex = 0;
            // 
            // lblDiaChi
            // 
            lblDiaChi.Location = new Point(0, 0);
            lblDiaChi.Name = "lblDiaChi";
            lblDiaChi.Size = new Size(100, 23);
            lblDiaChi.TabIndex = 0;
            // 
            // _dtpNgaySinh
            // 
            _dtpNgaySinh.Location = new Point(0, 0);
            _dtpNgaySinh.Name = "_dtpNgaySinh";
            _dtpNgaySinh.Size = new Size(200, 23);
            _dtpNgaySinh.TabIndex = 0;
            // 
            // lblNgaySinh
            // 
            lblNgaySinh.Location = new Point(0, 0);
            lblNgaySinh.Name = "lblNgaySinh";
            lblNgaySinh.Size = new Size(100, 23);
            lblNgaySinh.TabIndex = 0;
            // 
            // pnlHoTenInput
            // 
            pnlHoTenInput.Location = new Point(0, 0);
            pnlHoTenInput.Name = "pnlHoTenInput";
            pnlHoTenInput.Size = new Size(200, 100);
            pnlHoTenInput.TabIndex = 0;
            // 
            // _txtHoTen
            // 
            _txtHoTen.Location = new Point(0, 0);
            _txtHoTen.Name = "_txtHoTen";
            _txtHoTen.Size = new Size(100, 23);
            _txtHoTen.TabIndex = 0;
            // 
            // lblHoTen
            // 
            lblHoTen.Location = new Point(0, 0);
            lblHoTen.Name = "lblHoTen";
            lblHoTen.Size = new Size(100, 23);
            lblHoTen.TabIndex = 0;
            // 
            // pnlMaNVInput
            // 
            pnlMaNVInput.Location = new Point(0, 0);
            pnlMaNVInput.Name = "pnlMaNVInput";
            pnlMaNVInput.Size = new Size(200, 100);
            pnlMaNVInput.TabIndex = 0;
            // 
            // _txtMaNV
            // 
            _txtMaNV.Location = new Point(0, 0);
            _txtMaNV.Name = "_txtMaNV";
            _txtMaNV.Size = new Size(100, 23);
            _txtMaNV.TabIndex = 0;
            // 
            // lblMaNV
            // 
            lblMaNV.Location = new Point(0, 0);
            lblMaNV.Name = "lblMaNV";
            lblMaNV.Size = new Size(100, 23);
            lblMaNV.TabIndex = 0;
            // 
            // _cboTimTheo
            // 
            _cboTimTheo.Location = new Point(0, 0);
            _cboTimTheo.Name = "_cboTimTheo";
            _cboTimTheo.Size = new Size(121, 23);
            _cboTimTheo.TabIndex = 0;
            // 
            // _dgvNhanVien
            // 
            _dgvNhanVien.Location = new Point(0, 0);
            _dgvNhanVien.Name = "_dgvNhanVien";
            _dgvNhanVien.Size = new Size(240, 150);
            _dgvNhanVien.TabIndex = 0;
            // 
            // lb_DSNhanVien
            // 
            lb_DSNhanVien.Location = new Point(0, 0);
            lb_DSNhanVien.Name = "lb_DSNhanVien";
            lb_DSNhanVien.Size = new Size(100, 23);
            lb_DSNhanVien.TabIndex = 0;
            // 
            // ThongKe
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.mt;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1101, 628);
            Controls.Add(roundedPanel1);
            DoubleBuffered = true;
            Name = "ThongKe";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ThongKe";
            ((System.ComponentModel.ISupportInitialize)dgvRevenue).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvOrdersByHour).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvCategoryShare).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvTopItems).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvLowStock).EndInit();
            roundedPanel1.ResumeLayout(false);
            roundedPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pb_Admin).EndInit();
            btn_DangXuat.ResumeLayout(false);
            btn_DangXuat.PerformLayout();
            hcnt_Khung.ResumeLayout(false);
            hcnt_Khung.PerformLayout();
            roundedPanel2.ResumeLayout(false);
            roundedPanel2.PerformLayout();
            btnRefresh.ResumeLayout(false);
            btnRefresh.PerformLayout();
            hcnt_KhungMenuAD.ResumeLayout(false);
            hcnt_KhungMenuAD.PerformLayout();
            btn_ThongKe.ResumeLayout(false);
            btn_ThongKe.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pb_ThongKe).EndInit();
            btn_QLHDB.ResumeLayout(false);
            btn_QLHDB.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pb_QLHDB).EndInit();
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
            ((System.ComponentModel.ISupportInitialize)_dgvNhanVien).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private RoundedPanel roundedPanel1;
        private RoundedPanel hcnt_KhungMenuAD;
        private Label lb_Admin;
        private RoundedPanel hcnt_Khung;
        private Label lb_DMQL;
        private Label lblTotalRevenue;
        private Label lblTotalProfit;
        private Label lblTotalOrders;
        private Label lblNewCustomers;
        private ComboBox cboDateFilter;
        private DateTimePicker dtpFrom;
        private DateTimePicker dtpTo;
        private Button btnExportCsv;
        private DataGridView dgvRevenue;
        private DataGridView dgvOrdersByHour;
        private DataGridView dgvCategoryShare;
        private DataGridView dgvTopItems;
        private DataGridView dgvLowStock;
        private RoundedPanel btn_QLNV;
        private RoundedPanel btn_QLNCC;
        private Label label5;
        private Label label4;
        private RoundedPanel btn_ThongKe;
        private Label label10;
        private RoundedPanel btn_QLHDB;
        private Label label9;
        private RoundedPanel btn_QLHDN;
        private Label label8;
        private RoundedPanel btn_QLMA;
        private Label label7;
        private RoundedPanel btn_QLKH;
        private Label label6;
        private PictureBox pb_Admin;
        private PictureBox pb_QLNV;
        private PictureBox pb_QLKH;
        private PictureBox pb_QLNCC;
        private PictureBox pb_QLMA;
        private PictureBox pb_QLHDN;
        private PictureBox pb_QLHDB;
        private PictureBox pb_ThongKe;
        private RoundedPanel btn_DangXuat;
        private Label lb_DangXuat;
        private Label lb_QLNhanVienTitle;
        private Label lblMaNV;
        private TextBox _txtMaNV;
        private Label lblHoTen;
        private TextBox _txtHoTen;
        private Label lblNgaySinh;
        private DateTimePicker _dtpNgaySinh;
        private Label lblSdt;
        private TextBox _txtSdt;
        private Label lblDiaChi;
        private TextBox _txtDiaChi;
        private Label lblMatKhau;
        private TextBox _txtMatKhau;
        private Label lblTrangThai;
        private ComboBox _cboTrangThai;
        private Label lblChucVu;
        private ComboBox _cboChucVu;
        private RoundedPanel _btnXemLichTruc;
        private Label _lblBtnXemLichTruc;
        private RoundedPanel _btnDatLaiMatKhau;
        private Label _lblBtnDatLaiMatKhau;
        private RoundedPanel pnlMatKhauInput;
        private RoundedPanel pnlDiaChiInput;
        private RoundedPanel pnlSdtInput;
        private RoundedPanel pnlHoTenInput;
        private RoundedPanel pnlMaNVInput;
        private RoundedPanel pnlTimKiem;
        private TextBox _txtTimKiem;
        private ComboBox _cboTimTheo;
        private RoundedPanel _btnThem;
        private Label lblBtnThem;
        private RoundedPanel _btnSua;
        private Label lblBtnSua;
        private RoundedPanel _btnXoa;
        private Label lblBtnXoa;
        private RoundedPanel _btnLamMoi;
        private Label lblBtnLamMoi;
        private Label lb_DSNhanVien;
        private DataGridView _dgvNhanVien;
        private Label label1;
        private RoundedPanel btnRefresh;
        private RoundedPanel roundedPanel2;
        private Label label2;
    }
}