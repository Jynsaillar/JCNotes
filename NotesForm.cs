using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Linq;

namespace JCNotes
{
    public partial class NotesForm : Form
    {
        public NotesForm()
        {
            InitializeComponent();
            SetCustomFont(ref richTextBoxNotes);
        }

        private void SetCustomFont(ref RichTextBox richTextBoxNotes)
        {
            // TI-83 Plus Large font:
            // https://www.dafont.com/ti-83-plus-large.font
            using (var installedFonts = new System.Drawing.Text.InstalledFontCollection())
            {
                var fontHashset = installedFonts.Families.ToHashSet(); // Hashset for fast lookup
                // Checks if the font 'TI-83 Plus Large' is installed
                if (fontHashset.FirstOrDefault(
                    installedFont => String.Equals(installedFont.Name, "TI-83 Plus Large", StringComparison.InvariantCultureIgnoreCase))
                    is System.Drawing.FontFamily ti83Font)
                {
                    richTextBoxNotes.Font = new System.Drawing.Font(ti83Font.Name, 9);
                }
            }
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
