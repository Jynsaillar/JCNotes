using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace JCNotes
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImport("user32")]
        private static extern bool ReleaseCapture();

        [DllImport("user32")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wp, int lp);

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture(); // Release cursor capture
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0); // Redirect left mouse button down message from client area to caption area
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            OnMouseDown(e);
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem == toolStripMenuItemCloseForm)
            {
                Application.Exit();
            }
        }
    }
}
