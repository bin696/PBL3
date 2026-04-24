using System.Runtime.InteropServices;

namespace PBL3.UI // Đảm bảo tên namespace này trùng với tên Project của bạn
{
    public class CustomDateTimePicker : DateTimePicker
    {
        // Thuật toán can thiệp vào bộ nhớ Windows để vẽ lại nền
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

        private const int WM_ERASEBKGND = 0x0014;

        public CustomDateTimePicker()
        {
            // Thiết lập mặc định khi khởi tạo
            this.BackColor = SystemColors.Control; // Đây chính là màu ButtonFace
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_ERASEBKGND)
            {
                using (Graphics g = Graphics.FromHdc(m.WParam))
                {
                    // Vẽ màu nền trùng với màu ButtonFace (SystemColors.Control)
                    g.FillRectangle(SystemBrushes.Control, ClientRectangle);
                }
                m.Result = (IntPtr)1;
                return;
            }
            base.WndProc(ref m);
        }
    }
}