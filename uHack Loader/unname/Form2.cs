using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HWIDGrabber;
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Management;
using System.Diagnostics;
using System.Net;

namespace unname
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        string hwid;

        #region Ignore this shit
        const int WM_NCLBUTTONDOWN = 0xA1;
        const int HT_CAPTION = 0x2;

        private void MoveWindow(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Win32.ReleaseCapture();
                Win32.SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        public static class Win32
        {
            public const int CS_DropSHADOW = 0x20000;
            public const int GCL_STYLE = -26;
            public const int WM_NCLBUTTONDOWN = 0xA1;
            public const int HT_CAPTION = 0x2;

            [DllImport("User32.dll")]
            public static extern short GetAsyncKeyState(Keys vKey);
            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            public static extern int SetClassLong(IntPtr hwnd, int nIndex, int dwNewLong);
            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            public static extern int GetClassLong(IntPtr hwnd, int nIndex);
            [DllImport("user32.dll")]
            public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
            [DllImport("user32.dll")]
            public static extern bool ReleaseCapture();
            [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
            public static extern IntPtr GetForegroundWindow();
            [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            public static extern int GetWindowThreadProcessId(IntPtr handle, out int processId);
            [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
            public static extern IntPtr CreateRoundRectRgn
            (
                int nLeftRect,     // x-coordinate of upper-left corner
                int nTopRect,      // y-coordinate of upper-left corner
                int nRightRect,    // x-coordinate of lower-right corner
                int nBottomRect,   // y-coordinate of lower-right corner
                int nWidthEllipse, // height of ellipse
                int nHeightEllipse // width of ellipse
            );

            public static bool GetKeyState(Keys key) => (GetAsyncKeyState(key) == short.MinValue + 1);
        }
        #endregion


        private void Form2_Load(object sender, EventArgs e)
        {
            hwid = HWDI.GetMachineGuid();

            if (Properties.Settings.Default.Checked == true)
            {
                textBox1.Text = Properties.Settings.Default.Username;
                textBox2.Text = Properties.Settings.Default.Password;
                checkBox1.Checked = Properties.Settings.Default.Checked;
            }
        }

        string UppercaseFirst(string str)
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty;
            return char.ToUpper(str[0]) + str.Substring(1).ToLower();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            string str = textBox1.Text;
            string upstr = UppercaseFirst(str);

            Properties.Settings.Default.Username = upstr;
            Properties.Settings.Default.Password = textBox2.Text;
            Properties.Settings.Default.Checked = checkBox1.Checked;
            Properties.Settings.Default.Save();

            webBrowser1.Navigate("https://h98dj198hjna9087hn312.000webhostapp.com/89yuaos8hd9889u12h912h9h98h9asddas/Upload/check.php?username=" + textBox1.Text + "&password=" + textBox2.Text + "&hwid=" + hwid);
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void webBrowser1_DocumentCompleted_1(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("Please provide Username and password", "uHack.ml", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (webBrowser1.DocumentText.Contains("p1"))
            {
                if (webBrowser1.DocumentText.Contains("g4") || webBrowser1.DocumentText.Contains("g8"))
                {
                    if (webBrowser1.DocumentText.Contains("h1"))
                    {
                        this.Hide();
                        var form3 = new Form3();
                        form3.Closed += (s, args) => this.Close();
                        form3.Show();
                    }
                    else if (webBrowser1.DocumentText.Contains("h0"))
                    {
                        MessageBox.Show("HWID Incorrect", "uHack.ml", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (webBrowser1.DocumentText.Contains("h3"))
                    {
                        MessageBox.Show("New HWID Set", "uHack.ml", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Hide();
                        var form3 = new Form3();
                        form3.Closed += (s, args) => this.Close();
                        form3.Show();
                    }
                }
                else
                {
                    MessageBox.Show("Group Incorrect", "uHack.ml", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                if (webBrowser1.DocumentText.Contains("p0"))
                    MessageBox.Show("Password Incorrect", "uHack.ml", MessageBoxButtons.OK, MessageBoxIcon.Error);

                else
                    MessageBox.Show("User not found", "uHack.ml", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void ButtonLogin2_Click(object sender, EventArgs e) // Close
        {            
            Application.Exit();
            
        }
    }
}
