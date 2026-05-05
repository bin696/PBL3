using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace PBL3.DataBase
{
    internal static class DbHelper
    {
        private const string DefaultDatabaseName = "QL_FASTFOOD";
        private static readonly string _connStr = ResolveConnectionString();

        private static string ResolveConnectionString()
        {
            ConnectionStringSettings? fromDefault = ConfigurationManager.ConnectionStrings[DefaultDatabaseName];
            if (fromDefault is not null && !string.IsNullOrWhiteSpace(fromDefault.ConnectionString))
            {
                return NormalizeConnectionString(fromDefault.ConnectionString);
            }

            string dataBaseConfigPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DataBase", "App.config");
            if (File.Exists(dataBaseConfigPath))
            {
                ExeConfigurationFileMap map = new ExeConfigurationFileMap { ExeConfigFilename = dataBaseConfigPath };
                Configuration cfg = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);
                ConnectionStringSettings? fromDataBaseConfig = cfg.ConnectionStrings.ConnectionStrings[DefaultDatabaseName]
                                                        ?? cfg.ConnectionStrings.ConnectionStrings["DefaultConnection"];
                if (fromDataBaseConfig is not null && !string.IsNullOrWhiteSpace(fromDataBaseConfig.ConnectionString))
                {
                    return NormalizeConnectionString(fromDataBaseConfig.ConnectionString);
                }
            }

            throw new InvalidOperationException($"Không tìm thấy connection string '{DefaultDatabaseName}'. Vui lòng kiểm tra DataBase/App.config.");
        }

        private static string NormalizeConnectionString(string rawConnectionString)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(rawConnectionString);

            if (string.IsNullOrWhiteSpace(builder.DataSource))
            {
                throw new InvalidOperationException("Connection string thiếu Data Source (tên SQL Server instance).");
            }

            if (string.IsNullOrWhiteSpace(builder.InitialCatalog))
            {
                builder.InitialCatalog = DefaultDatabaseName;
            }

            if (builder.Encrypt && !builder.TrustServerCertificate)
            {
                builder.TrustServerCertificate = true;
            }

            return builder.ConnectionString;
        }

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(_connStr);
        }

        public static DataTable ExecuteQuery(string sql)
        {
            using (SqlConnection conn = GetConnection())
            {
                using SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public static int ExecuteNonQuery(string sql)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                using SqlCommand cmd = new SqlCommand(sql, conn);
                return cmd.ExecuteNonQuery();
            }
        }
    }
}