// using System;
// using System.ComponentModel;
// using System.Drawing;
// using System.Drawing.Printing;
// using System.IO;
// using System.Reflection;
// using System.Threading;
// using System.Threading.Tasks;
// using System.Windows.Forms;
//
// namespace LiftSystem.views
// {
//     public class LiftView
//     {
//         private Control _shaftPanel;
//         private int _shaftHeight;
//         private int _shaftWidth;
//         
//         public LiftView(Control panel, int height, int width)
//         {
//             _shaftWidth = (int)(width * 0.6);
//             var roofHeight = 100;
//
//             _shaftPanel = new PictureBox();
//             _shaftPanel.Height = height;
//             _shaftPanel.Width = _shaftWidth;
//             _shaftPanel.Dock = DockStyle.Right;
//             _shaftPanel.BackColor = Color.Transparent;
//             panel.Controls.Add(_shaftPanel);
//             
//             var roof = new PictureBox();
//             roof.Image = Properties.Resources.LiftCarLeftDoor;
//             roof.Top = 0;
//             roof.Left = 200;
//             roof.BackColor = Color.Transparent;
//             roof.Height = roofHeight;
//             
//             var floor = new PictureBox();
//             floor.Top = 0;
//             floor.Height = 100;
//             floor.Image = Properties.Resources.LiftShaftFloor;
//             floor.Width = _shaftWidth;
//             floor.Height = roofHeight;
//             floor.BackColor = Color.Transparent;
//             
//             _shaftPanel.Controls.Add(roof);
//             _shaftPanel.Controls.Add(floor);
//
//             _shaftHeight = height - (roof.Height + floor.Height);
//             
//             
//             var rp = new Panel();
//             rp.Width = (int)(width * 0.1);
//             rp.Dock = DockStyle.Right;
//             panel.Controls.Add(rp);
//         }
//     }
// }