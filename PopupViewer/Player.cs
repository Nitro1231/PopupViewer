using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PopupViewer {
    public partial class Player : Form {
        public Player() {
            InitializeComponent();
            DoubleBuffered = true;
            SetStyle(ControlStyles.ResizeRedraw, true);
        }

        private void Player_Load(object sender, EventArgs e) {

        }

        #region Form Resize
        private const int gripSize = 10; // Grip size
        private const int barHeight = 15; // Caption bar height;
        SolidBrush brush = new SolidBrush(Color.FromArgb(120, 224, 143)); // Bar Color

        protected override void OnPaint(PaintEventArgs e) {
            Rectangle bar = new Rectangle(ClientSize.Width - gripSize, ClientSize.Height - gripSize, gripSize, gripSize);
            ControlPaint.DrawSizeGrip(e.Graphics, BackColor, bar);
            bar = new Rectangle(0, 0, ClientSize.Width, barHeight);
            e.Graphics.FillRectangle(brush, bar);
        }

        protected override void WndProc(ref Message m) {
            if (m.Msg == 0x84) {  // WM_NCHITTEST
                Point pos = new Point(m.LParam.ToInt32());
                pos = PointToClient(pos);
                if (pos.Y < barHeight) {
                    m.Result = (IntPtr)2;  // HTCAPTION
                    return;
                }
                if (pos.X >= ClientSize.Width - gripSize && pos.Y >= ClientSize.Height - gripSize) {
                    m.Result = (IntPtr)17; // HTBOTTOMRIGHT
                    return;
                }
            }
            base.WndProc(ref m);
        }
        #endregion
    }
}
