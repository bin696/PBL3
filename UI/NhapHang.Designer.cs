namespace PBL3
{
    partial class NhapHang
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
            _lblNccTitle = new Label();
            _cboNhaCungCap = new ComboBox();
            _lblNguoiNhapTitle = new Label();
            _lblNguoiNhapValue = new Label();
            _lblNgayNhapTitle = new Label();
            _lblNgayNhap = new Label();
            _lblNguyenLieuTitle = new Label();
            _cboNguyenLieu = new ComboBox();
            _lblSoLuongTitle = new Label();
            _nudSoLuong = new NumericUpDown();
            _lblDonGiaTitle = new Label();
            _txtDonGia = new TextBox();
            _btnThem = new Button();
            _btnXoaDong = new Button();
            _dgvGioHang = new DataGridView();
            _lblTongTien = new Label();
            _btnXacNhan = new Button();
            _btnHuy = new Button();
            ((System.ComponentModel.ISupportInitialize)_nudSoLuong).BeginInit();
            ((System.ComponentModel.ISupportInitialize)_dgvGioHang).BeginInit();
            SuspendLayout();
            // 
            // _lblNccTitle
            // 
            _lblNccTitle.AutoSize = true;
            _lblNccTitle.Location = new Point(12, 18);
            _lblNccTitle.Name = "_lblNccTitle";
            _lblNccTitle.Size = new Size(103, 20);
            _lblNccTitle.TabIndex = 0;
            _lblNccTitle.Text = "Nhà cung cấp:";
            // 
            // _cboNhaCungCap
            // 
            _cboNhaCungCap.DropDownStyle = ComboBoxStyle.DropDownList;
            _cboNhaCungCap.FormattingEnabled = true;
            _cboNhaCungCap.Location = new Point(116, 15);
            _cboNhaCungCap.Name = "_cboNhaCungCap";
            _cboNhaCungCap.Size = new Size(177, 28);
            _cboNhaCungCap.TabIndex = 1;
            // 
            // _lblNguoiNhapTitle
            // 
            _lblNguoiNhapTitle.AutoSize = true;
            _lblNguoiNhapTitle.Location = new Point(316, 18);
            _lblNguoiNhapTitle.Name = "_lblNguoiNhapTitle";
            _lblNguoiNhapTitle.Size = new Size(91, 20);
            _lblNguoiNhapTitle.TabIndex = 2;
            _lblNguoiNhapTitle.Text = "Người nhập:";
            // 
            // _lblNguoiNhapValue
            // 
            _lblNguoiNhapValue.AutoSize = true;
            _lblNguoiNhapValue.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            _lblNguoiNhapValue.Location = new Point(406, 18);
            _lblNguoiNhapValue.Name = "_lblNguoiNhapValue";
            _lblNguoiNhapValue.Size = new Size(15, 20);
            _lblNguoiNhapValue.TabIndex = 3;
            _lblNguoiNhapValue.Text = "-";
            // 
            // _lblNgayNhapTitle
            // 
            _lblNgayNhapTitle.AutoSize = true;
            _lblNgayNhapTitle.Location = new Point(12, 50);
            _lblNgayNhapTitle.Name = "_lblNgayNhapTitle";
            _lblNgayNhapTitle.Size = new Size(84, 20);
            _lblNgayNhapTitle.TabIndex = 4;
            _lblNgayNhapTitle.Text = "Ngày nhập:";
            // 
            // _lblNgayNhap
            // 
            _lblNgayNhap.AutoSize = true;
            _lblNgayNhap.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            _lblNgayNhap.Location = new Point(95, 50);
            _lblNgayNhap.Name = "_lblNgayNhap";
            _lblNgayNhap.Size = new Size(15, 20);
            _lblNgayNhap.TabIndex = 5;
            _lblNgayNhap.Text = "-";
            // 
            // _lblNguyenLieuTitle
            // 
            _lblNguyenLieuTitle.AutoSize = true;
            _lblNguyenLieuTitle.Location = new Point(12, 86);
            _lblNguyenLieuTitle.Name = "_lblNguyenLieuTitle";
            _lblNguyenLieuTitle.Size = new Size(91, 20);
            _lblNguyenLieuTitle.TabIndex = 6;
            _lblNguyenLieuTitle.Text = "Nguyên liệu:";
            // 
            // _cboNguyenLieu
            // 
            _cboNguyenLieu.DropDownStyle = ComboBoxStyle.DropDownList;
            _cboNguyenLieu.FormattingEnabled = true;
            _cboNguyenLieu.Location = new Point(104, 83);
            _cboNguyenLieu.Name = "_cboNguyenLieu";
            _cboNguyenLieu.Size = new Size(158, 28);
            _cboNguyenLieu.TabIndex = 7;
            // 
            // _lblSoLuongTitle
            // 
            _lblSoLuongTitle.AutoSize = true;
            _lblSoLuongTitle.Location = new Point(332, 86);
            _lblSoLuongTitle.Name = "_lblSoLuongTitle";
            _lblSoLuongTitle.Size = new Size(72, 20);
            _lblSoLuongTitle.TabIndex = 8;
            _lblSoLuongTitle.Text = "Số lượng:";
            // 
            // _nudSoLuong
            // 
            _nudSoLuong.DecimalPlaces = 2;
            _nudSoLuong.Location = new Point(405, 84);
            _nudSoLuong.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            _nudSoLuong.Minimum = new decimal(new int[] { 1, 0, 0, 131072 });
            _nudSoLuong.Name = "_nudSoLuong";
            _nudSoLuong.Size = new Size(110, 27);
            _nudSoLuong.TabIndex = 9;
            _nudSoLuong.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // _lblDonGiaTitle
            // 
            _lblDonGiaTitle.AutoSize = true;
            _lblDonGiaTitle.Location = new Point(12, 128);
            _lblDonGiaTitle.Name = "_lblDonGiaTitle";
            _lblDonGiaTitle.Size = new Size(65, 20);
            _lblDonGiaTitle.TabIndex = 10;
            _lblDonGiaTitle.Text = "Đơn giá:";
            // 
            // _txtDonGia
            // 
            _txtDonGia.Location = new Point(78, 125);
            _txtDonGia.Name = "_txtDonGia";
            _txtDonGia.Size = new Size(120, 27);
            _txtDonGia.TabIndex = 11;
            // 
            // _btnThem
            // 
            _btnThem.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _btnThem.BackColor = Color.FromArgb(255, 153, 102);
            _btnThem.FlatAppearance.BorderSize = 0;
            _btnThem.FlatStyle = FlatStyle.Flat;
            _btnThem.ForeColor = Color.White;
            _btnThem.Location = new Point(494, 122);
            _btnThem.Name = "_btnThem";
            _btnThem.Size = new Size(78, 32);
            _btnThem.TabIndex = 12;
            _btnThem.Text = "Thêm";
            _btnThem.UseVisualStyleBackColor = false;
            // 
            // _btnXoaDong
            // 
            _btnXoaDong.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            _btnXoaDong.BackColor = Color.Peru;
            _btnXoaDong.FlatAppearance.BorderSize = 0;
            _btnXoaDong.FlatStyle = FlatStyle.Flat;
            _btnXoaDong.ForeColor = Color.White;
            _btnXoaDong.Location = new Point(401, 122);
            _btnXoaDong.Name = "_btnXoaDong";
            _btnXoaDong.Size = new Size(87, 32);
            _btnXoaDong.TabIndex = 13;
            _btnXoaDong.Text = "Xóa";
            _btnXoaDong.UseVisualStyleBackColor = false;
            // 
            // _dgvGioHang
            // 
            _dgvGioHang.AllowUserToAddRows = false;
            _dgvGioHang.AllowUserToDeleteRows = false;
            _dgvGioHang.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            _dgvGioHang.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            _dgvGioHang.Location = new Point(12, 160);
            _dgvGioHang.MultiSelect = false;
            _dgvGioHang.Name = "_dgvGioHang";
            _dgvGioHang.ReadOnly = true;
            _dgvGioHang.RowHeadersVisible = false;
            _dgvGioHang.RowHeadersWidth = 51;
            _dgvGioHang.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _dgvGioHang.Size = new Size(560, 257);
            _dgvGioHang.TabIndex = 14;
            // 
            // _lblTongTien
            // 
            _lblTongTien.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            _lblTongTien.AutoSize = true;
            _lblTongTien.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            _lblTongTien.Location = new Point(12, 439);
            _lblTongTien.Name = "_lblTongTien";
            _lblTongTien.Size = new Size(149, 23);
            _lblTongTien.TabIndex = 15;
            _lblTongTien.Text = "Tổng tiền: 0 VNĐ";
            // 
            // _btnXacNhan
            // 
            _btnXacNhan.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            _btnXacNhan.BackColor = Color.Peru;
            _btnXacNhan.FlatAppearance.BorderSize = 0;
            _btnXacNhan.FlatStyle = FlatStyle.Flat;
            _btnXacNhan.ForeColor = Color.White;
            _btnXacNhan.Location = new Point(473, 423);
            _btnXacNhan.Name = "_btnXacNhan";
            _btnXacNhan.Size = new Size(99, 38);
            _btnXacNhan.TabIndex = 16;
            _btnXacNhan.Text = "Xác nhận";
            _btnXacNhan.UseVisualStyleBackColor = false;
            // 
            // _btnHuy
            // 
            _btnHuy.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            _btnHuy.Location = new Point(367, 423);
            _btnHuy.Name = "_btnHuy";
            _btnHuy.Size = new Size(94, 38);
            _btnHuy.TabIndex = 17;
            _btnHuy.Text = "Thoát";
            _btnHuy.UseVisualStyleBackColor = true;
            // 
            // NhapHang
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(584, 481);
            Controls.Add(_btnHuy);
            Controls.Add(_btnXacNhan);
            Controls.Add(_lblTongTien);
            Controls.Add(_dgvGioHang);
            Controls.Add(_btnXoaDong);
            Controls.Add(_btnThem);
            Controls.Add(_txtDonGia);
            Controls.Add(_lblDonGiaTitle);
            Controls.Add(_nudSoLuong);
            Controls.Add(_lblSoLuongTitle);
            Controls.Add(_cboNguyenLieu);
            Controls.Add(_lblNguyenLieuTitle);
            Controls.Add(_lblNgayNhap);
            Controls.Add(_lblNgayNhapTitle);
            Controls.Add(_lblNguoiNhapValue);
            Controls.Add(_lblNguoiNhapTitle);
            Controls.Add(_cboNhaCungCap);
            Controls.Add(_lblNccTitle);
            MinimumSize = new Size(600, 520);
            Name = "NhapHang";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Nhập hàng";
            ((System.ComponentModel.ISupportInitialize)_nudSoLuong).EndInit();
            ((System.ComponentModel.ISupportInitialize)_dgvGioHang).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label _lblNccTitle;
        private ComboBox _cboNhaCungCap;
        private Label _lblNguoiNhapTitle;
        private Label _lblNguoiNhapValue;
        private Label _lblNgayNhapTitle;
        private Label _lblNgayNhap;
        private Label _lblNguyenLieuTitle;
        private ComboBox _cboNguyenLieu;
        private Label _lblSoLuongTitle;
        private NumericUpDown _nudSoLuong;
        private Label _lblDonGiaTitle;
        private TextBox _txtDonGia;
        private Button _btnThem;
        private Button _btnXoaDong;
        private DataGridView _dgvGioHang;
        private Label _lblTongTien;
        private Button _btnXacNhan;
        private Button _btnHuy;
    }
}
