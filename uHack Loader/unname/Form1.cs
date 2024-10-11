using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Management;
using System.Diagnostics;

namespace unname
{
    public partial class Form1 : Form
    {
        string oldexepath;
        int version;
        int count;



        #region Ignore this shit
        const int WM_NCLBUTTONDOWN = 0xA1;
        const int HT_CAPTION = 0x2;
        const int GWL_STYLE = -16;
        const long WS_VISIBLE = 0x10000000;


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

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            version = 19961337;

            var ping = new System.Net.NetworkInformation.Ping();

            var result = ping.Send("www.google.com");

            if (result.Status == System.Net.NetworkInformation.IPStatus.Success)
            {
                WebRequest request = WebRequest.Create("https://87u129bhajksd.000webhostapp.com/iashdoliahsouidhzxlkncl1290561231/version.txt");
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (response == null || response.StatusCode != HttpStatusCode.OK)
                {
                    MessageBox.Show("Could not connect");
                }
                else
                {
                    timer1.Start();
                }
            }
            else
            {
                MessageBox.Show("Could not connect");
            }

            string path = @"C:\Users\Public\Documents\LoaderPath.txt";
            if (File.Exists(path))
            {
                using (StreamReader sr = File.OpenText(path))
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        oldexepath = s;
                    }

                    sr.Close();
                }
            }

            if (File.Exists(oldexepath))
            {
                File.Delete(oldexepath);
                File.Delete(path);
            }

            var webRequest = WebRequest.Create("https://87u129bhajksd.000webhostapp.com/iashdoliahsouidhzxlkncl1290561231/status.txt");
            using (var response = webRequest.GetResponse())
            using (var content = response.GetResponseStream())
            using (var reader = new StreamReader(content))
            {
                var status = reader.ReadToEnd();

                if (status == "0")
                {
                    MessageBox.Show("Error: The cheat has been locked! DM unname on Discord (wot#2841) to inquire", "Cheat Locked!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Application.Exit();
                }
                else if (status == "1")
                {

                }
                else if (status == "2")
                {
                    MessageBox.Show("Error: The cheat is down for updates! DM unname on Discord (wot#2841) to inquire", "Cheat Under Maintenance!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Application.Exit();
                }
                else if (status == "3")
                {
                    MessageBox.Show("Error: The cheat is closed! There may have been a VAC detection! DM unname on Discord (wot#2841) to inquire", "Cheat Is Closed!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Application.Exit();
                }
            }

        }


        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            var webRequest = WebRequest.Create("https://87u129bhajksd.000webhostapp.com/iashdoliahsouidhzxlkncl1290561231/version.txt");
            using (var response = webRequest.GetResponse())
            using (var content = response.GetResponseStream())
            using (var reader = new StreamReader(content))
            {
                var webverison = reader.ReadToEnd();

                if (version.ToString() != webverison)
                {
                    timer1.Stop();
                    timer2.Start();
                }
                else
                {
                    label1.Text = "Up to date";
                    label1.ForeColor = Color.YellowGreen;
                    label2.Text = "No updates found";
                    label2.ForeColor = Color.YellowGreen;
                    timer3.Start();
                    timer1.Stop();
                }
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            label2.Text = "Downloading update";
            label2.ForeColor = Color.Orange;

            timer2.Stop();

            string path = @"C:\Users\Public\Documents\LoaderPath.txt";

            if (!File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine(System.Reflection.Assembly.GetEntryAssembly().Location);
                }
            }

            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[8];
            var random = new Random();
            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }
            var finalString = new String(stringChars);

            // Downloading the new version
            WebClient myWebClient = new WebClient();
            myWebClient.DownloadFile("https://thaisen.pw/forums/auth/loader.exe", Directory.GetCurrentDirectory() + "/" + finalString + ".exe");
            System.Diagnostics.Process.Start(Directory.GetCurrentDirectory() + "/" + finalString + ".exe");
            Application.Exit();
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            timer4.Start();
            timer3.Stop();
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            timer4.Stop();
            this.Hide();
            var form2 = new Form2();
            form2.Closed += (s, args) => this.Close();
            form2.Show();
        }

        private void timer5_Tick(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }       
    }
}
#endregion