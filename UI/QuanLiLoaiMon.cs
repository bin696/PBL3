using PBL3.DataBase;
using System.Data;
using System.Data.SqlClient;

namespace PBL3
{
    public class QuanLiLoaiMon : Form
    {
        private DataGridView _dgvLoaiMon;
        private TextBox _txtMaLoai;
        private TextBox _txtTenLoai;
        private Label _lblMaLoai;
        private Label _lblTenLoai;
        private UI.RoundedPanel _btnThem;
        private Label lblBtnThem;
        private UI.RoundedPanel _btnSua;
        private Label lblBtnSua;
        private UI.RoundedPanel _btnXoa;
        private Label lblBtnXoa;
        private UI.RoundedPanel _btnLamMoi;
        private Label lblBtnLamMoi;
        private DataTable? _loaiMonTable;

        public QuanLiLoaiMon()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            _dgvLoaiMon = new DataGridView();
            _txtMaLoai = new TextBox();
            _txtTenLoai = new TextBox();
            _lblTenLoai = new Label();
            _btnThem = new PBL3.UI.RoundedPanel();
            lblBtnThem = new Label();
            _btnSua = new PBL3.UI.RoundedPanel();
            lblBtnSua = new Label();
            _btnXoa = new PBL3.UI.RoundedPanel();
            lblBtnXoa = new Label();
            _btnLamMoi = new PBL3.UI.RoundedPanel();
            lblBtnLamMoi = new Label();
            ((System.ComponentModel.ISupportInitialize)_dgvLoaiMon).BeginInit();
            _btnThem.SuspendLayout();
            _btnSua.SuspendLayout();
            _btnXoa.SuspendLayout();
            _btnLamMoi.SuspendLayout();
            SuspendLayout();
            // 
            // _dgvLoaiMon
            // 
            _dgvLoaiMon.AllowUserToAddRows = false;
            _dgvLoaiMon.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            _dgvLoaiMon.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            _dgvLoaiMon.Location = new Point(12, 12);
            _dgvLoaiMon.MultiSelect = false;
            _dgvLoaiMon.Name = "_dgvLoaiMon";
            _dgvLoaiMon.ReadOnly = true;
            _dgvLoaiMon.RowHeadersVisible = false;
            _dgvLoaiMon.RowHeadersWidth = 51;
            _dgvLoaiMon.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _dgvLoaiMon.Size = new Size(393, 241);
            _dgvLoaiMon.TabIndex = 8;
            _dgvLoaiMon.CellClick += DgvLoaiMon_CellClick;
            // hidden id textbox for MaLoai (used for edit/delete)
            _txtMaLoai.Location = new Point(12, 228);
            _txtMaLoai.Name = "_txtMaLoai";
            _txtMaLoai.Size = new Size(80, 23);
            _txtMaLoai.Visible = false;
            _dgvLoaiMon.CellContentClick += _dgvLoaiMon_CellContentClick;
            // 
            // _txtTenLoai
            // 
            _txtTenLoai.Location = new Point(104, 259);
            _txtTenLoai.Name = "_txtTenLoai";
            _txtTenLoai.Size = new Size(301, 23);
            _txtTenLoai.TabIndex = 4;
            // 
            // _lblTenLoai
            // 
            _lblTenLoai.AutoSize = true;
            _lblTenLoai.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            _lblTenLoai.Location = new Point(12, 260);
            _lblTenLoai.Name = "_lblTenLoai";
            _lblTenLoai.Size = new Size(61, 19);
            _lblTenLoai.TabIndex = 5;
            _lblTenLoai.Text = "Tên loại";
            // 
            // _btnThem
            // 
            _btnThem.BackColor = Color.Coral;
            _btnThem.Controls.Add(lblBtnThem);
            _btnThem.CornerRadius = 10;
            _btnThem.Location = new Point(13, 300);
            _btnThem.Name = "_btnThem";
            _btnThem.Size = new Size(86, 32);
            _btnThem.TabIndex = 19;
            _btnThem.Click += BtnThem_Click;
            // 
            // lblBtnThem
            // 
            lblBtnThem.AutoSize = true;
            lblBtnThem.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblBtnThem.ForeColor = Color.White;
            lblBtnThem.Location = new Point(17, 4);
            lblBtnThem.Name = "lblBtnThem";
            lblBtnThem.Size = new Size(46, 19);
            lblBtnThem.TabIndex = 0;
            lblBtnThem.Text = "Thêm";
            lblBtnThem.Click += BtnThem_Click;
            // 
            // _btnSua
            // 
            _btnSua.BackColor = Color.SandyBrown;
            _btnSua.Controls.Add(lblBtnSua);
            _btnSua.CornerRadius = 10;
            _btnSua.Location = new Point(105, 300);
            _btnSua.Name = "_btnSua";
            _btnSua.Size = new Size(86, 32);
            _btnSua.TabIndex = 20;
            _btnSua.Click += BtnSua_Click;
            // 
            // lblBtnSua
            // 
            lblBtnSua.AutoSize = true;
            lblBtnSua.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblBtnSua.ForeColor = Color.White;
            lblBtnSua.Location = new Point(23, 4);
            lblBtnSua.Name = "lblBtnSua";
            lblBtnSua.Size = new Size(34, 19);
            lblBtnSua.TabIndex = 0;
            lblBtnSua.Text = "Sửa";
            lblBtnSua.Click += BtnSua_Click;
            // 
            // _btnXoa
            // 
            _btnXoa.BackColor = Color.IndianRed;
            _btnXoa.Controls.Add(lblBtnXoa);
            _btnXoa.CornerRadius = 10;
            _btnXoa.Location = new Point(197, 300);
            _btnXoa.Name = "_btnXoa";
            _btnXoa.Size = new Size(86, 32);
            _btnXoa.TabIndex = 21;
            _btnXoa.Click += BtnXoa_Click;
            // 
            // lblBtnXoa
            // 
            lblBtnXoa.AutoSize = true;
            lblBtnXoa.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblBtnXoa.ForeColor = Color.White;
            lblBtnXoa.Location = new Point(23, 4);
            lblBtnXoa.Name = "lblBtnXoa";
            lblBtnXoa.Size = new Size(35, 19);
            lblBtnXoa.TabIndex = 0;
            lblBtnXoa.Text = "Xóa";
            lblBtnXoa.Click += BtnXoa_Click;
            // 
            // _btnLamMoi
            // 
            _btnLamMoi.BackColor = Color.Peru;
            _btnLamMoi.Controls.Add(lblBtnLamMoi);
            _btnLamMoi.CornerRadius = 10;
            _btnLamMoi.Location = new Point(319, 303);
            _btnLamMoi.Name = "_btnLamMoi";
            _btnLamMoi.Size = new Size(86, 32);
            _btnLamMoi.TabIndex = 22;
            _btnLamMoi.Click += BtnLamMoi_Click;
            // 
            // lblBtnLamMoi
            // 
            lblBtnLamMoi.AutoSize = true;
            lblBtnLamMoi.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblBtnLamMoi.ForeColor = Color.White;
            lblBtnLamMoi.Location = new Point(2, 4);
            lblBtnLamMoi.Name = "lblBtnLamMoi";
            lblBtnLamMoi.Size = new Size(67, 19);
            lblBtnLamMoi.TabIndex = 0;
            lblBtnLamMoi.Text = "Làm mới";
            lblBtnLamMoi.Click += BtnLamMoi_Click;
            // 
            // QuanLiLoaiMon
            // 
            BackColor = Color.FromArgb(248, 242, 235);
            ClientSize = new Size(420, 355);
            Controls.Add(_btnLamMoi);
            Controls.Add(_btnXoa);
            Controls.Add(_btnSua);
            Controls.Add(_btnThem);
            Controls.Add(_txtMaLoai);
            Controls.Add(_txtTenLoai);
            Controls.Add(_lblTenLoai);
            Controls.Add(_dgvLoaiMon);
            Name = "QuanLiLoaiMon";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quản lý loại món";
            Load += QuanLiLoaiMon_Load;
            ((System.ComponentModel.ISupportInitialize)_dgvLoaiMon).EndInit();
            _btnThem.ResumeLayout(false);
            _btnThem.PerformLayout();
            _btnSua.ResumeLayout(false);
            _btnSua.PerformLayout();
            _btnXoa.ResumeLayout(false);
            _btnXoa.PerformLayout();
            _btnLamMoi.ResumeLayout(false);
            _btnLamMoi.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private void QuanLiLoaiMon_Load(object? sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                using SqlConnection conn = DbHelper.GetConnection();
                using SqlDataAdapter da = new SqlDataAdapter("SELECT MaLoai, TenLoai FROM dbo.LOAI_MON ORDER BY MaLoai", conn);
                _loaiMonTable = new DataTable();
                da.Fill(_loaiMonTable);
                _dgvLoaiMon.DataSource = _loaiMonTable;
                // hide ID column and set header for display column
                if (_dgvLoaiMon.Columns.Contains("MaLoai"))
                    _dgvLoaiMon.Columns["MaLoai"].Visible = false;

                if (_dgvLoaiMon.Columns.Contains("TenLoai") && _dgvLoaiMon.Columns["TenLoai"] is DataGridViewColumn colTenLoai)
                {
                    colTenLoai.HeaderText = "Tên loại";
                }
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không thể tải dữ liệu.\n{ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvLoaiMon_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = _dgvLoaiMon.Rows[e.RowIndex];
                string raw = Convert.ToString(row.Cells["MaLoai"].Value) ?? string.Empty;
                _txtMaLoai.Text = FormatMaLoaiForDisplay(raw);
                _txtTenLoai.Text = Convert.ToString(row.Cells["TenLoai"].Value) ?? string.Empty;
            }
        }

        private void ClearForm()
        {
            _txtMaLoai.Clear();
            _txtTenLoai.Clear();
            _txtTenLoai.Focus();
        }

        private void BtnLamMoi_Click(object? sender, EventArgs e)
        {
            ClearForm();
            LoadData();
        }

        private static string FormatMaLoaiForDisplay(string? maLoaiValue)
        {
            if (string.IsNullOrWhiteSpace(maLoaiValue))
            {
                return string.Empty;
            }

            string value = maLoaiValue.Trim();
            return value.StartsWith("LO", StringComparison.OrdinalIgnoreCase) ? value.ToUpperInvariant() : $"LO{value}";
        }

        private static string ParseMaLoaiForDb(string? displayValue)
        {
            if (string.IsNullOrWhiteSpace(displayValue))
            {
                return string.Empty;
            }

            string v = displayValue.Trim();
            if (v.StartsWith("LO", StringComparison.OrdinalIgnoreCase))
            {
                return v.Substring(2);
            }

            return v;
        }

        private void BtnThem_Click(object? sender, EventArgs e)
        {
            string tenLoai = _txtTenLoai.Text.Trim();
            if (string.IsNullOrWhiteSpace(tenLoai))
            {
                MessageBox.Show("Vui lòng nhập tên loại món.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using SqlConnection conn = DbHelper.GetConnection();
                using SqlCommand cmd = new SqlCommand("INSERT INTO dbo.LOAI_MON (TenLoai) VALUES (@TenLoai)", conn);
                cmd.Parameters.Add("@TenLoai", SqlDbType.NVarChar, 100).Value = tenLoai;

                conn.Open();
                cmd.ExecuteNonQuery();

                MessageBox.Show("Thêm loại món thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSua_Click(object? sender, EventArgs e)
        {
            string maLoai = ParseMaLoaiForDb(_txtMaLoai.Text);
            string tenLoai = _txtTenLoai.Text.Trim();

            if (string.IsNullOrWhiteSpace(maLoai))
            {
                MessageBox.Show("Vui lòng chọn loại món cần sửa.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(tenLoai))
            {
                MessageBox.Show("Vui lòng nhập tên loại món.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using SqlConnection conn = DbHelper.GetConnection();
                using SqlCommand cmd = new SqlCommand("UPDATE dbo.LOAI_MON SET TenLoai = @TenLoai WHERE MaLoai = @MaLoai", conn);
                cmd.Parameters.Add("@MaLoai", SqlDbType.VarChar, 20).Value = maLoai;
                cmd.Parameters.Add("@TenLoai", SqlDbType.NVarChar, 100).Value = tenLoai;

                conn.Open();
                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    MessageBox.Show("Sửa loại món thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy loại món này.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi sửa: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnXoa_Click(object? sender, EventArgs e)
        {
            string maLoai = ParseMaLoaiForDb(_txtMaLoai.Text);

            if (string.IsNullOrWhiteSpace(maLoai))
            {
                MessageBox.Show("Vui lòng chọn loại món cần xóa.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Bạn có chắc muốn xóa loại món này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            try
            {
                using SqlConnection conn = DbHelper.GetConnection();
                using SqlCommand cmd = new SqlCommand("DELETE FROM dbo.LOAI_MON WHERE MaLoai = @MaLoai", conn);
                cmd.Parameters.Add("@MaLoai", SqlDbType.VarChar, 20).Value = maLoai;

                conn.Open();
                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    MessageBox.Show("Xóa loại món thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy loại món này.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (SqlException ex) when (ex.Number == 547)
            {
                MessageBox.Show("Không thể xóa loại món này vì đang được sử dụng ở dữ liệu khác.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void _dgvLoaiMon_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
