using PBL3.UI;

namespace PBL3
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            // Start the application with the HoaDon form (designer) so runtime matches the drag-drop layout.
            Application.Run(new TrangDangNhap());
        }
    }
}