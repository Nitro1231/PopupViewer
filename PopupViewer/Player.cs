using CefSharp;
using CefSharp.WinForms;
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
        public ChromiumWebBrowser browser;

        public Player() {
            InitializeComponent();
            DoubleBuffered = true;
            SetStyle(ControlStyles.ResizeRedraw, true);
        }

        private void Player_Load(object sender, EventArgs e) {
            CWB.Location = new Point(0, barHeight);
            CWB.Size = new Size(Width, Height - gripSize - barHeight);
            InitBrowser();
            browser.Refresh();
            browser.Load("http://www.naver.com/");
            //browser.Load(textBox1.Text);
        }

        public void InitBrowser() {
            CefSettings settings = new CefSettings();
            settings.CefCommandLineArgs.Add("enable-media-stream", "1");
            settings.CefCommandLineArgs.Add("enable-npapi", "1");
            settings.CefCommandLineArgs.Add("ppapi-flash-path", @"C:\dll\pepflashplayer.dll");
            settings.CefCommandLineArgs.Add("ppapi-flash-version", "28.0.0.137");
            //settings.CachePath = "cache";

            Cef.Initialize(settings);
            browser = new ChromiumWebBrowser("http://www.google.com");
            CWB.Controls.Add(browser);
            browser.Dock = DockStyle.Fill;
        }

        #region Form Resize
        private const int gripSize = 10; // Grip size
        private const int barHeight = 15; // Caption bar height;
        SolidBrush brush = new SolidBrush(Color.FromArgb(120, 224, 143)); // Bar Color

        protected override void OnPaint(PaintEventArgs e) {
            Rectangle bar = new Rectangle(Width - gripSize, Height - gripSize, gripSize, gripSize);
            ControlPaint.DrawSizeGrip(e.Graphics, BackColor, bar);
            bar = new Rectangle(0, 0, Width, barHeight);
            e.Graphics.FillRectangle(brush, bar);
        }

        private void Player_SizeChanged(object sender, EventArgs e) {
            CWB.Size = new Size(Width, Height - gripSize - barHeight);
            //Update();
        }

        protected override void WndProc(ref Message m) {
            if (m.Msg == 0x84) {  // WM_NCHITTEST
                Point pos = new Point(m.LParam.ToInt32());
                pos = PointToClient(pos);
                if (pos.Y < barHeight) {
                    m.Result = (IntPtr)2;  // HTCAPTION
                    return;
                }
                if (pos.X >= Width - gripSize && pos.Y >= Height - gripSize) {
                    m.Result = (IntPtr)17; // HTBOTTOMRIGHT
                    return;
                }
            }
            base.WndProc(ref m);
        }
        #endregion

        private void CWB_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            browser.Load("chrome://version/");
        }
    }
}
