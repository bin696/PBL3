using PBL3.DataBase;
using System.Data;
using System.Data.SqlClient;

namespace PBL3
{
    public class QuanLiDonViTinh : Form
    {
        private DataGridView _dgvDonViTinh;
        private TextBox _txtTenDVT;
        private TextBox _txtMaDVT;
        private Label _lblTenDVT;
        private UI.RoundedPanel _btnThem;
        private Label lblBtnThem;
        private UI.RoundedPanel _btnSua;
        private Label lblBtnSua;
        private UI.RoundedPanel _btnXoa;
        private Label lblBtnXoa;
        private UI.RoundedPanel _btnLamMoi;
        private Label lblBtnLamMoi;
        private DataTable? _donViTinhTable;

        public QuanLiDonViTinh()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            _dgvDonViTinh = new DataGridView();
            _txtTenDVT = new TextBox();
            _lblTenDVT = new Label();
            _btnThem = new PBL3.UI.RoundedPanel();
            lblBtnThem = new Label();
            _btnSua = new PBL3.UI.RoundedPanel();
            lblBtnSua = new Label();
            _btnXoa = new PBL3.UI.RoundedPanel();
            lblBtnXoa = new Label();
            _btnLamMoi = new PBL3.UI.RoundedPanel();
            lblBtnLamMoi = new Label();
            ((System.ComponentModel.ISupportInitialize)_dgvDonViTinh).BeginInit();
            _btnThem.SuspendLayout();
            _btnSua.SuspendLayout();
            _btnXoa.SuspendLayout();
            _btnLamMoi.SuspendLayout();
            SuspendLayout();
            // 
            // _dgvDonViTinh
            // 
            _dgvDonViTinh.AllowUserToAddRows = false;
            _dgvDonViTinh.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            _dgvDonViTinh.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            _dgvDonViTinh.Location = new Point(12, 12);
            _dgvDonViTinh.MultiSelect = false;
            _dgvDonViTinh.Name = "_dgvDonViTinh";
            _dgvDonViTinh.ReadOnly = true;
            _dgvDonViTinh.RowHeadersVisible = false;
            _dgvDonViTinh.RowHeadersWidth = 51;
            _dgvDonViTinh.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _dgvDonViTinh.Size = new Size(393, 241);
            _dgvDonViTinh.TabIndex = 0;
            _dgvDonViTinh.CellClick += DgvDonViTinh_CellClick;
            // hidden id textbox for MaDVT
            _txtMaDVT = new TextBox();
            _txtMaDVT.Location = new Point(12, 268);
            _txtMaDVT.Name = "_txtMaDVT";
            _txtMaDVT.Size = new Size(80, 23);
            _txtMaDVT.Visible = false;
            // 
            // _txtTenDVT
            // 
            _txtTenDVT.Location = new Point(104, 259);
            _txtTenDVT.Name = "_txtTenDVT";
            _txtTenDVT.Size = new Size(301, 23);
            _txtTenDVT.TabIndex = 4;
            // 
            // _lblTenDVT
            // 
            _lblTenDVT.AutoSize = true;
            _lblTenDVT.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            _lblTenDVT.Location = new Point(12, 260);
            _lblTenDVT.Name = "_lblTenDVT";
            _lblTenDVT.Size = new Size(63, 19);
            _lblTenDVT.TabIndex = 3;
            _lblTenDVT.Text = "Tên DVT";
            // 
            // _btnThem
            // 
            _btnThem.BackColor = Color.Coral;
            _btnThem.Controls.Add(lblBtnThem);
            _btnThem.CornerRadius = 10;
            _btnThem.Location = new Point(12, 303);
            _btnThem.Name = "_btnThem";
            _btnThem.Size = new Size(86, 32);
            _btnThem.TabIndex = 18;
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
            _btnSua.Location = new Point(104, 303);
            _btnSua.Name = "_btnSua";
            _btnSua.Size = new Size(86, 32);
            _btnSua.TabIndex = 19;
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
            _btnXoa.Location = new Point(196, 303);
            _btnXoa.Name = "_btnXoa";
            _btnXoa.Size = new Size(86, 32);
            _btnXoa.TabIndex = 20;
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
            _btnLamMoi.TabIndex = 21;
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
            // QuanLiDonViTinh
            // 
            ClientSize = new Size(420, 360);
            Controls.Add(_btnLamMoi);
            Controls.Add(_btnXoa);
            Controls.Add(_btnSua);
            Controls.Add(_btnThem);
            Controls.Add(_txtMaDVT);
            Controls.Add(_dgvDonViTinh);
            Controls.Add(_lblTenDVT);
            Controls.Add(_txtTenDVT);
            Name = "QuanLiDonViTinh";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quản lí đơn vị tính";
            Load += QuanLiDonViTinh_Load;
            ((System.ComponentModel.ISupportInitialize)_dgvDonViTinh).EndInit();
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

        private void QuanLiDonViTinh_Load(object? sender, EventArgs e)
        {
            LoadDonViTinh();
            ClearForm();
        }

        private void LoadDonViTinh()
        {
            const string sql = @"SELECT MaDVT, TenDVT FROM dbo.DON_VI_TINH ORDER BY MaDVT";
            using SqlConnection conn = DbHelper.GetConnection();
            using SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            _donViTinhTable = dt;
            _dgvDonViTinh.DataSource = _donViTinhTable;
            if (_dgvDonViTinh.Columns.Contains("MaDVT"))
                _dgvDonViTinh.Columns["MaDVT"].Visible = false;
            if (_dgvDonViTinh.Columns.Contains("TenDVT"))
                _dgvDonViTinh.Columns["TenDVT"].HeaderText = "Tên đơn vị tính";
        }

        private void DgvDonViTinh_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            DataGridViewRow row = _dgvDonViTinh.Rows[e.RowIndex];
            string raw = Convert.ToString(row.Cells["MaDVT"].Value) ?? string.Empty;
            _txtMaDVT.Text = FormatMaDvtForDisplay(raw);
            _txtTenDVT.Text = Convert.ToString(row.Cells["TenDVT"].Value) ?? string.Empty;
        }

        private void BtnThem_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_txtTenDVT.Text))
            {
                MessageBox.Show("Vui lòng nhập tên đơn vị tính.", "Thiếu dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                using SqlConnection conn = DbHelper.GetConnection();
                conn.Open();

                int nextId = 1;
                using (SqlCommand cmdGet = new SqlCommand("SELECT MaDVT FROM dbo.DON_VI_TINH ORDER BY TRY_CAST(MaDVT AS INT)", conn))
                using (SqlDataReader reader = cmdGet.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader.IsDBNull(0))
                            continue;

                        if (!int.TryParse(Convert.ToString(reader.GetValue(0)), out int v))
                            continue;

                        if (v == nextId)
                        {
                            nextId++;
                        }
                        else if (v > nextId)
                        {
                            break;
                        }
                    }
                }

                bool hasIdentity = false;
                using (SqlCommand cmdCheck = new SqlCommand("SELECT CASE WHEN COLUMNPROPERTY(OBJECT_ID('dbo.DON_VI_TINH'),'MaDVT','IsIdentity') = 1 THEN 1 ELSE 0 END", conn))
                {
                    hasIdentity = Convert.ToInt32(cmdCheck.ExecuteScalar() ?? 0) == 1;
                }

                using SqlTransaction tran = conn.BeginTransaction();
                try
                {
                    if (hasIdentity)
                    {
                        using SqlCommand cmd = new SqlCommand("SET IDENTITY_INSERT dbo.DON_VI_TINH ON; INSERT INTO dbo.DON_VI_TINH (MaDVT, TenDVT) VALUES (@MaDVT, @TenDVT); SET IDENTITY_INSERT dbo.DON_VI_TINH OFF;", conn, tran);
                        cmd.Parameters.Add("@MaDVT", SqlDbType.Int).Value = nextId;
                        cmd.Parameters.Add("@TenDVT", SqlDbType.NVarChar, 100).Value = _txtTenDVT.Text.Trim();
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        using SqlCommand cmd = new SqlCommand("INSERT INTO dbo.DON_VI_TINH (MaDVT, TenDVT) VALUES (@MaDVT, @TenDVT)", conn, tran);
                        cmd.Parameters.Add("@MaDVT", SqlDbType.Int).Value = nextId;
                        cmd.Parameters.Add("@TenDVT", SqlDbType.NVarChar, 100).Value = _txtTenDVT.Text.Trim();
                        cmd.ExecuteNonQuery();
                    }

                    tran.Commit();
                }
                catch
                {
                    tran.Rollback();
                    throw;
                }

                MessageBox.Show("Thêm đơn vị tính thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDonViTinh();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSua_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_txtTenDVT.Text))
            {
                MessageBox.Show("Vui lòng nhập tên đơn vị tính.", "Thiếu dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            const string sql = @"UPDATE dbo.DON_VI_TINH SET TenDVT = @TenDVT WHERE MaDVT = @MaDVT";
            using SqlConnection conn = DbHelper.GetConnection();
            using SqlCommand cmd = new SqlCommand(sql, conn);
            string maDvtRaw = ParseMaDvtForDb(_txtMaDVT.Text.Trim());
            if (!int.TryParse(maDvtRaw, out int maDvtInt))
            {
                MessageBox.Show("Mã đơn vị tính không hợp lệ.", "Dữ liệu sai", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            cmd.Parameters.Add("@MaDVT", SqlDbType.Int).Value = maDvtInt;
            cmd.Parameters.Add("@TenDVT", SqlDbType.NVarChar, 100).Value = _txtTenDVT.Text.Trim();
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                LoadDonViTinh();
                ClearForm();
                MessageBox.Show("Sửa đơn vị tính thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi sửa: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnXoa_Click(object? sender, EventArgs e)
        {
            const string sql = @"DELETE FROM dbo.DON_VI_TINH WHERE MaDVT = @MaDVT";
            using SqlConnection conn = DbHelper.GetConnection();
            using SqlCommand cmd = new SqlCommand(sql, conn);
            string maDvtRaw = ParseMaDvtForDb(_txtMaDVT.Text.Trim());
            if (!int.TryParse(maDvtRaw, out int maDvtInt))
            {
                MessageBox.Show("Mã đơn vị tính không hợp lệ.", "Dữ liệu sai", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            cmd.Parameters.Add("@MaDVT", SqlDbType.Int).Value = maDvtInt;
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                LoadDonViTinh();
                ClearForm();
                MessageBox.Show("Xóa đơn vị tính thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnLamMoi_Click(object? sender, EventArgs e)
        {
            LoadDonViTinh();
            ClearForm();
        }

        private static string FormatMaDvtForDisplay(string? maDvtValue)
        {
            if (string.IsNullOrWhiteSpace(maDvtValue))
            {
                return string.Empty;
            }

            string value = maDvtValue.Trim();
            return value.StartsWith("DVT", StringComparison.OrdinalIgnoreCase) ? value.ToUpperInvariant() : $"DVT{value}";
        }

        private static string ParseMaDvtForDb(string? displayValue)
        {
            if (string.IsNullOrWhiteSpace(displayValue))
            {
                return string.Empty;
            }

            string v = displayValue.Trim();
            if (v.StartsWith("DVT", StringComparison.OrdinalIgnoreCase))
            {
                return v.Substring(3);
            }

            return v;
        }

        private void ClearForm()
        {
            _txtTenDVT.Text = string.Empty;
        }
    }
}
