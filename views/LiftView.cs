using System;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace LiftSystem.views
{
    public class LiftView
    {
        private Control _shaftPanel;
        private int _shaftHeight;
        private int _shaftWidth;

        public LiftView(Control panel, int height, int width)
        {
            _shaftWidth = (int)(width * 0.6);
            var roofHeight = 50;
            
            _shaftPanel = new Panel();
            _shaftPanel.Height = height;
            _shaftPanel.Width = _shaftWidth;
            _shaftPanel.Dock = DockStyle.Right;
            _shaftPanel.BackColor = Color.Aqua;
            panel.Controls.Add(_shaftPanel);
            
            var roof = new PictureBox();
            roof.Image = Properties.Resources.LiftShaftRoof;
                
            roof.Width = _shaftWidth;
            roof.Top = 0;
            roof.Size = new Size(150, 150);
            roof.Height = roofHeight;
            _shaftPanel.Controls.Add(roof);

            var floor = new PictureBox();

            floor.Image = Properties.Resources.LiftShaftFloor;
            floor.Width = _shaftWidth;
            floor.Height = roofHeight;
            floor.Top = 50;
            _shaftPanel.Controls.Add(floor);

            _shaftHeight = height - (roof.Height + floor.Height);
            
            
            var rp = new Panel();
            rp.Width = (int)(width * 0.1);
            rp.Dock = DockStyle.Right;
            panel.Controls.Add(rp);
        }
    }
}