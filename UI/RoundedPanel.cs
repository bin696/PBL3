using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace PBL3.UI
{
    public class RoundedPanel : Panel
    {
        [Browsable(true)]
        [Category("Appearance")]
        [DefaultValue(15)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int CornerRadius { get; set; } = 15;

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Kiểm tra tránh d = 0 gây lỗi vẽ
            int d = Math.Max(CornerRadius * 2, 1);

            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddArc(0, 0, d, d, 180, 90);
                path.AddArc(Width - d - 1, 0, d, d, 270, 90);
                path.AddArc(Width - d - 1, Height - d - 1, d, d, 0, 90);
                path.AddArc(0, Height - d - 1, d, d, 90, 90);
                path.CloseAllFigures();

                this.Region = new Region(path);

                using (Pen pen = new Pen(this.BackColor, 1))
                {
                    e.Graphics.DrawPath(pen, path);
                }
            }
        }
    }
}