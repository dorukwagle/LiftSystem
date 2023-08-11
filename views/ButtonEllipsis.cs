using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace LiftSystem.views
{
    public class ButtonEllipsis : Button
    {
        protected override void OnPaint(PaintEventArgs pevent)
        {
            GraphicsPath graphicsPath = new GraphicsPath();
            graphicsPath.AddEllipse(0, 0, ClientSize.Width, ClientSize.Height);
            Region = new System.Drawing.Region(graphicsPath);
            base.OnPaint(pevent);
        }
    }
}