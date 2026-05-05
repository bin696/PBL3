using System;
using System.Data.SqlClient;
using PBL3.Models;
using PBL3.DataBase;

namespace PBL3.Repositories
{
    public class NhanVienRepository
    {
        public NhanVien GetNhanVienBySDTAndMatKhau(string sdt, string matKhau)
        {
            NhanVien nv = null;
            using (SqlConnection conn = DbHelper.GetConnection())
            {
                conn.Open();
                string query = @"
                    SELECT nv.MaNV, nv.MaCV, cv.TenCV
                    FROM dbo.NHAN_VIEN nv
                    LEFT JOIN dbo.CHUC_VU cv ON cv.MaCV = nv.MaCV
                    WHERE nv.SDT = @sdt AND nv.MatKhau = @mk";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@sdt", sdt);
                    cmd.Parameters.AddWithValue("@mk", matKhau);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            nv = new NhanVien
                            {
                                MaNV = reader["MaNV"]?.ToString()?.Trim() ?? string.Empty,
                                MaCV = reader["MaCV"]?.ToString()?.Trim() ?? string.Empty,
                            };
                        }
                    }
                }
            }
            return nv;
        }
    }
}