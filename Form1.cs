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
                SendMessage(Handle, 161, 2, 0); // Redirect left mouse button down message from client area to caption area
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
