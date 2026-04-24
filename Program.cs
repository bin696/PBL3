using PBL3.UI;
using System.Text;

namespace PBL3
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += (_, e) => ShowUnhandledError(e.Exception);
            AppDomain.CurrentDomain.UnhandledException += (_, e) =>
            {
                Exception ex = e.ExceptionObject as Exception ?? new Exception("Unknown unhandled exception.");
                ShowUnhandledError(ex);
            };

            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            // Start the application with the HoaDon form (designer) so runtime matches the drag-drop layout.
            Application.Run(new TrangDangNhap());
        }

        private static void ShowUnhandledError(Exception ex)
        {
            try
            {
                string logDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");
                Directory.CreateDirectory(logDir);
                string logPath = Path.Combine(logDir, "crash.log");

                StringBuilder sb = new StringBuilder();
                sb.AppendLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}]");
                sb.AppendLine(ex.ToString());
                sb.AppendLine(new string('-', 80));

                File.AppendAllText(logPath, sb.ToString(), Encoding.UTF8);
            }
            catch
            {
            }

            MessageBox.Show(
                $"Ứng dụng gặp lỗi không mong muốn:\n{ex.Message}\n\nChi tiết đã được ghi vào file logs/crash.log.",
                "Lỗi hệ thống",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }
}