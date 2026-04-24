namespace PBL3
{
    partial class HopThuYeuCauForm
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
            splitContainer1 = new SplitContainer();
            _flpCards = new FlowLayoutPanel();
            pnlFilter = new Panel();
            roundedPanel2 = new PBL3.UI.RoundedPanel();
            lblTrangThaiFilter = new Label();
            lblLoaiFilter = new Label();
            _cboTrangThai = new ComboBox();
            _cboLoai = new ComboBox();
            pnlRight = new Panel();
            _btnTuChoi = new PBL3.UI.RoundedPanel();
            label1 = new Label();
            _btnXoa = new PBL3.UI.RoundedPanel();
            label2 = new Label();
            _btnDuyet = new PBL3.UI.RoundedPanel();
            lblBtnThem = new Label();
            _lstHistory = new ListBox();
            _txtPhanHoi = new TextBox();
            lblPhanHoi = new Label();
            _txtNoiDung = new TextBox();
            roundedPanel1 = new PBL3.UI.RoundedPanel();
            _lblTitle = new Label();
            _lblNhanVien = new Label();
            _lblChucVu = new Label();
            _lblTrangThai = new Label();
            lblHistory = new Label();
            _lblThoiGian = new Label();
            _lblLoai = new Label();
            _lblCaGanNhat = new Label();
            _lblKhoangNgay = new Label();
            lblLyDo = new Label();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            pnlFilter.SuspendLayout();
            roundedPanel2.SuspendLayout();
            pnlRight.SuspendLayout();
            _btnTuChoi.SuspendLayout();
            _btnXoa.SuspendLayout();
            _btnDuyet.SuspendLayout();
            roundedPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Margin = new Padding(3, 4, 3, 4);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(_flpCards);
            splitContainer1.Panel1.Controls.Add(pnlFilter);
            splitContainer1.Panel1MinSize = 50;
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(pnlRight);
            splitContainer1.Size = new Size(1067, 651);
            splitContainer1.SplitterDistance = 221;
            splitContainer1.SplitterWidth = 5;
            splitContainer1.TabIndex = 0;
            // 
            // _flpCards
            // 
            _flpCards.AutoScroll = true;
            _flpCards.BackColor = Color.PeachPuff;
            _flpCards.Dock = DockStyle.Fill;
            _flpCards.FlowDirection = FlowDirection.TopDown;
            _flpCards.Location = new Point(0, 86);
            _flpCards.Margin = new Padding(3, 4, 3, 4);
            _flpCards.Name = "_flpCards";
            _flpCards.Size = new Size(221, 565);
            _flpCards.TabIndex = 1;
            _flpCards.WrapContents = false;
            // 
            // pnlFilter
            // 
            pnlFilter.BackColor = Color.PeachPuff;
            pnlFilter.Controls.Add(roundedPanel2);
            pnlFilter.Dock = DockStyle.Top;
            pnlFilter.Location = new Point(0, 0);
            pnlFilter.Margin = new Padding(3, 4, 3, 4);
            pnlFilter.Name = "pnlFilter";
            pnlFilter.Size = new Size(221, 86);
            pnlFilter.TabIndex = 0;
            // 
            // roundedPanel2
            // 
            roundedPanel2.BackColor = Color.PeachPuff;
            roundedPanel2.Controls.Add(lblTrangThaiFilter);
            roundedPanel2.Controls.Add(lblLoaiFilter);
            roundedPanel2.Controls.Add(_cboTrangThai);
            roundedPanel2.Controls.Add(_cboLoai);
            roundedPanel2.Location = new Point(3, 16);
            roundedPanel2.Name = "roundedPanel2";
            roundedPanel2.Size = new Size(210, 68);
            roundedPanel2.TabIndex = 4;
            // 
            // lblTrangThaiFilter
            // 
            lblTrangThaiFilter.AutoSize = true;
            lblTrangThaiFilter.Location = new Point(118, 8);
            lblTrangThaiFilter.Name = "lblTrangThaiFilter";
            lblTrangThaiFilter.Size = new Size(75, 20);
            lblTrangThaiFilter.TabIndex = 3;
            lblTrangThaiFilter.Text = "Trạng thái";
            // 
            // lblLoaiFilter
            // 
            lblLoaiFilter.AutoSize = true;
            lblLoaiFilter.Location = new Point(3, 8);
            lblLoaiFilter.Name = "lblLoaiFilter";
            lblLoaiFilter.Size = new Size(91, 20);
            lblLoaiFilter.TabIndex = 2;
            lblLoaiFilter.Text = "Loại yêu cầu";
            lblLoaiFilter.Click += lblLoaiFilter_Click;
            // 
            // _cboTrangThai
            // 
            _cboTrangThai.DropDownStyle = ComboBoxStyle.DropDownList;
            _cboTrangThai.FormattingEnabled = true;
            _cboTrangThai.Items.AddRange(new object[] { "Tất cả", "Chờ duyệt", "Đã duyệt", "Đã từ chối" });
            _cboTrangThai.Location = new Point(118, 32);
            _cboTrangThai.Margin = new Padding(3, 4, 3, 4);
            _cboTrangThai.Name = "_cboTrangThai";
            _cboTrangThai.Size = new Size(95, 28);
            _cboTrangThai.TabIndex = 1;
            // 
            // _cboLoai
            // 
            _cboLoai.DropDownStyle = ComboBoxStyle.DropDownList;
            _cboLoai.FormattingEnabled = true;
            _cboLoai.Items.AddRange(new object[] { "Tất cả", "Nghỉ phép", "Nghỉ hẳn" });
            _cboLoai.Location = new Point(5, 32);
            _cboLoai.Margin = new Padding(3, 4, 3, 4);
            _cboLoai.Name = "_cboLoai";
            _cboLoai.Size = new Size(106, 28);
            _cboLoai.TabIndex = 0;
            // 
            // pnlRight
            // 
            pnlRight.BackColor = Color.Bisque;
            pnlRight.Controls.Add(_btnTuChoi);
            pnlRight.Controls.Add(_btnDuyet);
            pnlRight.Controls.Add(_lstHistory);
            pnlRight.Controls.Add(_txtPhanHoi);
            pnlRight.Controls.Add(lblPhanHoi);
            pnlRight.Controls.Add(_txtNoiDung);
            pnlRight.Controls.Add(roundedPanel1);
            pnlRight.Dock = DockStyle.Fill;
            pnlRight.Location = new Point(0, 0);
            pnlRight.Margin = new Padding(3, 4, 3, 4);
            pnlRight.Name = "pnlRight";
            pnlRight.Padding = new Padding(14, 16, 14, 16);
            pnlRight.Size = new Size(841, 651);
            pnlRight.TabIndex = 0;
            // 
            // _btnTuChoi
            // 
            _btnTuChoi.BackColor = Color.LightCoral;
            _btnTuChoi.Controls.Add(label1);
            _btnTuChoi.CornerRadius = 10;
            _btnTuChoi.Location = new Point(573, 581);
            _btnTuChoi.Name = "_btnTuChoi";
            _btnTuChoi.Size = new Size(86, 32);
            _btnTuChoi.TabIndex = 18;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label1.ForeColor = Color.White;
            label1.Location = new Point(8, 4);
            label1.Name = "label1";
            label1.Size = new Size(69, 23);
            label1.TabIndex = 0;
            label1.Text = "Từ chối";
            // 
            // _btnXoa
            // 
            _btnXoa.BackColor = Color.DimGray;
            _btnXoa.Controls.Add(label2);
            _btnXoa.CornerRadius = 10;
            _btnXoa.Location = new Point(703, 565);
            _btnXoa.Name = "_btnXoa";
            _btnXoa.Size = new Size(86, 32);
            _btnXoa.TabIndex = 18;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label2.ForeColor = Color.White;
            label2.Location = new Point(23, 4);
            label2.Name = "label2";
            label2.Size = new Size(40, 23);
            label2.TabIndex = 0;
            label2.Text = "Xóa";
            // 
            // _btnDuyet
            // 
            _btnDuyet.BackColor = Color.OliveDrab;
            _btnDuyet.Controls.Add(lblBtnThem);
            _btnDuyet.CornerRadius = 10;
            _btnDuyet.Location = new Point(481, 581);
            _btnDuyet.Name = "_btnDuyet";
            _btnDuyet.Size = new Size(86, 32);
            _btnDuyet.TabIndex = 18;
            // 
            // lblBtnThem
            // 
            lblBtnThem.AutoSize = true;
            lblBtnThem.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblBtnThem.ForeColor = Color.White;
            lblBtnThem.Location = new Point(12, 4);
            lblBtnThem.Name = "lblBtnThem";
            lblBtnThem.Size = new Size(58, 23);
            lblBtnThem.TabIndex = 0;
            lblBtnThem.Text = "Duyệt";
            // 
            // _lstHistory
            // 
            _lstHistory.FormattingEnabled = true;
            _lstHistory.Location = new Point(37, 356);
            _lstHistory.Margin = new Padding(3, 4, 3, 4);
            _lstHistory.Name = "_lstHistory";
            _lstHistory.Size = new Size(766, 84);
            _lstHistory.TabIndex = 13;
            // 
            // _txtPhanHoi
            // 
            _txtPhanHoi.Location = new Point(40, 477);
            _txtPhanHoi.Margin = new Padding(3, 4, 3, 4);
            _txtPhanHoi.Multiline = true;
            _txtPhanHoi.Name = "_txtPhanHoi";
            _txtPhanHoi.ScrollBars = ScrollBars.Vertical;
            _txtPhanHoi.Size = new Size(763, 90);
            _txtPhanHoi.TabIndex = 11;
            // 
            // lblPhanHoi
            // 
            lblPhanHoi.AutoSize = true;
            lblPhanHoi.BackColor = Color.Linen;
            lblPhanHoi.Location = new Point(14, 453);
            lblPhanHoi.Name = "lblPhanHoi";
            lblPhanHoi.Size = new Size(141, 20);
            lblPhanHoi.TabIndex = 10;
            lblPhanHoi.Text = "Phản hồi của Admin";
            // 
            // _txtNoiDung
            // 
            _txtNoiDung.Location = new Point(37, 227);
            _txtNoiDung.Margin = new Padding(3, 4, 3, 4);
            _txtNoiDung.Multiline = true;
            _txtNoiDung.Name = "_txtNoiDung";
            _txtNoiDung.ReadOnly = true;
            _txtNoiDung.ScrollBars = ScrollBars.Vertical;
            _txtNoiDung.Size = new Size(766, 90);
            _txtNoiDung.TabIndex = 9;
            // 
            // roundedPanel1
            // 
            roundedPanel1.BackColor = Color.Linen;
            roundedPanel1.Controls.Add(_lblTitle);
            roundedPanel1.Controls.Add(_btnXoa);
            roundedPanel1.Controls.Add(_lblNhanVien);
            roundedPanel1.Controls.Add(_lblChucVu);
            roundedPanel1.Controls.Add(_lblTrangThai);
            roundedPanel1.Controls.Add(lblHistory);
            roundedPanel1.Controls.Add(_lblThoiGian);
            roundedPanel1.Controls.Add(_lblLoai);
            roundedPanel1.Controls.Add(_lblCaGanNhat);
            roundedPanel1.Controls.Add(_lblKhoangNgay);
            roundedPanel1.Controls.Add(lblLyDo);
            roundedPanel1.Location = new Point(14, 16);
            roundedPanel1.Name = "roundedPanel1";
            roundedPanel1.Size = new Size(810, 623);
            roundedPanel1.TabIndex = 19;
            // 
            // _lblTitle
            // 
            _lblTitle.AutoSize = true;
            _lblTitle.BackColor = Color.Linen;
            _lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            _lblTitle.ForeColor = Color.Salmon;
            _lblTitle.Location = new Point(315, 8);
            _lblTitle.Name = "_lblTitle";
            _lblTitle.Size = new Size(203, 32);
            _lblTitle.TabIndex = 0;
            _lblTitle.Text = "Hộp thư yêu cầu";
            // 
            // _lblNhanVien
            // 
            _lblNhanVien.AutoSize = true;
            _lblNhanVien.BackColor = Color.Linen;
            _lblNhanVien.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            _lblNhanVien.Location = new Point(11, 58);
            _lblNhanVien.Name = "_lblNhanVien";
            _lblNhanVien.Size = new Size(105, 23);
            _lblNhanVien.TabIndex = 1;
            _lblNhanVien.Text = "Nhân viên: -";
            // 
            // _lblChucVu
            // 
            _lblChucVu.AutoSize = true;
            _lblChucVu.BackColor = Color.Linen;
            _lblChucVu.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            _lblChucVu.Location = new Point(286, 58);
            _lblChucVu.Name = "_lblChucVu";
            _lblChucVu.Size = new Size(90, 23);
            _lblChucVu.TabIndex = 2;
            _lblChucVu.Text = "Chức vụ: -";
            // 
            // _lblTrangThai
            // 
            _lblTrangThai.AutoSize = true;
            _lblTrangThai.BackColor = Color.Linen;
            _lblTrangThai.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            _lblTrangThai.Location = new Point(536, 58);
            _lblTrangThai.Name = "_lblTrangThai";
            _lblTrangThai.Size = new Size(103, 23);
            _lblTrangThai.TabIndex = 7;
            _lblTrangThai.Text = "Trạng thái: -";
            // 
            // lblHistory
            // 
            lblHistory.AutoSize = true;
            lblHistory.BackColor = Color.Linen;
            lblHistory.Location = new Point(11, 316);
            lblHistory.Name = "lblHistory";
            lblHistory.Size = new Size(108, 20);
            lblHistory.TabIndex = 12;
            lblHistory.Text = "Lịch sử yêu cầu";
            // 
            // _lblThoiGian
            // 
            _lblThoiGian.AutoSize = true;
            _lblThoiGian.BackColor = Color.Linen;
            _lblThoiGian.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            _lblThoiGian.Location = new Point(11, 98);
            _lblThoiGian.Name = "_lblThoiGian";
            _lblThoiGian.Size = new Size(127, 23);
            _lblThoiGian.TabIndex = 5;
            _lblThoiGian.Text = "Thời gian gửi: -";
            // 
            // _lblLoai
            // 
            _lblLoai.AutoSize = true;
            _lblLoai.BackColor = Color.Linen;
            _lblLoai.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            _lblLoai.Location = new Point(286, 98);
            _lblLoai.Name = "_lblLoai";
            _lblLoai.Size = new Size(122, 23);
            _lblLoai.TabIndex = 4;
            _lblLoai.Text = "Loại yêu cầu: -";
            // 
            // _lblCaGanNhat
            // 
            _lblCaGanNhat.AutoSize = true;
            _lblCaGanNhat.BackColor = Color.Linen;
            _lblCaGanNhat.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            _lblCaGanNhat.Location = new Point(536, 98);
            _lblCaGanNhat.Name = "_lblCaGanNhat";
            _lblCaGanNhat.Size = new Size(120, 23);
            _lblCaGanNhat.TabIndex = 3;
            _lblCaGanNhat.Text = "Ca gần nhất: -";
            // 
            // _lblKhoangNgay
            // 
            _lblKhoangNgay.AutoSize = true;
            _lblKhoangNgay.BackColor = Color.Linen;
            _lblKhoangNgay.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            _lblKhoangNgay.ForeColor = Color.IndianRed;
            _lblKhoangNgay.Location = new Point(11, 138);
            _lblKhoangNgay.Name = "_lblKhoangNgay";
            _lblKhoangNgay.Size = new Size(136, 23);
            _lblKhoangNgay.TabIndex = 6;
            _lblKhoangNgay.Text = "Thời gian nghỉ: -";
            // 
            // lblLyDo
            // 
            lblLyDo.AutoSize = true;
            lblLyDo.BackColor = Color.Linen;
            lblLyDo.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblLyDo.Location = new Point(11, 187);
            lblLyDo.Name = "lblLyDo";
            lblLyDo.Size = new Size(44, 20);
            lblLyDo.TabIndex = 8;
            lblLyDo.Text = "Lý do";
            // 
            // HopThuYeuCauForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.mt;
            ClientSize = new Size(1067, 651);
            Controls.Add(splitContainer1);
            Margin = new Padding(3, 4, 3, 4);
            MinimumSize = new Size(800, 600);
            Name = "HopThuYeuCauForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Hộp thư yêu cầu";
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            pnlFilter.ResumeLayout(false);
            roundedPanel2.ResumeLayout(false);
            roundedPanel2.PerformLayout();
            pnlRight.ResumeLayout(false);
            pnlRight.PerformLayout();
            _btnTuChoi.ResumeLayout(false);
            _btnTuChoi.PerformLayout();
            _btnXoa.ResumeLayout(false);
            _btnXoa.PerformLayout();
            _btnDuyet.ResumeLayout(false);
            _btnDuyet.PerformLayout();
            roundedPanel1.ResumeLayout(false);
            roundedPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer splitContainer1;
        private Panel pnlFilter;
        private Label lblTrangThaiFilter;
        private Label lblLoaiFilter;
        private Panel pnlRight;
        private Label lblHistory;
        private Label lblPhanHoi;
        private Label lblLyDo;
        private ComboBox _cboLoai;
        private ComboBox _cboTrangThai;
        private FlowLayoutPanel _flpCards;
        private Label _lblTitle;
        private Label _lblNhanVien;
        private Label _lblChucVu;
        private Label _lblCaGanNhat;
        private Label _lblLoai;
        private Label _lblThoiGian;
        private Label _lblKhoangNgay;
        private Label _lblTrangThai;
        private TextBox _txtNoiDung;
        private TextBox _txtPhanHoi;
        private ListBox _lstHistory;

        private UI.RoundedPanel _btnDuyet;
        private Label lblBtnThem;
        private UI.RoundedPanel _btnTuChoi;
        private Label label1;
        private UI.RoundedPanel _btnXoa;
        private Label label2;
        private UI.RoundedPanel roundedPanel1;
        private UI.RoundedPanel roundedPanel2;
    }
}
