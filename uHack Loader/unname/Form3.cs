using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ManualMapInjection.Injection;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Security.Cryptography;
using System.Net;
using System.Runtime.InteropServices;

namespace unname
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private String KeyText = "ASDfghJKL";
        private String FilePath = "";

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

        private void Form3_Load(object sender, EventArgs e)
        {
            label1.Text = "" + Properties.Settings.Default.Username + "";
            webBrowser1.Navigate("https://87u129bhajksd.000webhostapp.com/check.php?username=" + Properties.Settings.Default.Username + "&password=0&hwid=0");
        }

        // Decrpytion code
        private void DecryptFile()
        {
            try
            {
                string inName = this.FilePath;
                if (this.KeyText == "")
                {
                    throw new Exception("Please enter a key.");
                }
                if (Path.GetExtension(inName) != ".des")
                {
                    throw new Exception("Not a .des file.");
                }
                string outName = Path.ChangeExtension(FilePath, "");
                if (!overwriteifExist(outName))
                {
                    throw new IOException("File not overwritten");
                }

                byte[] desKey = this.keytoByteArray();
                byte[] desIV = this.keytoByteArray();

                using (FileStream fin = new FileStream(inName, FileMode.Open, FileAccess.Read))
                using (FileStream fout = new FileStream(outName, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    fout.SetLength(0);

                    byte[] bin = new byte[100];
                    long rdlen = 0;
                    long totlen = fin.Length;
                    int len;

                    DES des = new DESCryptoServiceProvider();
                    CryptoStream decStream = new CryptoStream(fout, des.CreateDecryptor(desKey, desIV), CryptoStreamMode.Write);

                    while (rdlen < totlen)
                    {
                        len = fin.Read(bin, 0, 100);
                        decStream.Write(bin, 0, len);
                        rdlen = rdlen + len;
                    }

                    label4.Text = "Decryption complete.";
                    decStream.Close();
                    fout.Close();
                    fin.Close();
                }
            }
            catch (Exception e)
            {
                if (e is System.IO.FileNotFoundException)
                {
                    MessageBox.Show("Could not open source or destination file.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1);
                }
                else if (e is System.Security.Cryptography.CryptographicException)
                {
                    MessageBox.Show("Bad key or file.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1);
                    File.Delete(Path.ChangeExtension(FilePath, ""));
                }
                else if (e is IOException)
                {

                }
                else
                {
                    MessageBox.Show(e.Message,
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1);
                }
            }
        }

        private byte[] keytoByteArray()
        {
            byte[] KeyArray = Enumerable.Repeat((byte)0, 8).ToArray();

            for (int i = 0; i < KeyText.Length; i++)
            {
                byte b = (byte)KeyText[i];
                KeyArray[i % 8] = (byte)(KeyArray[i % 8] + b);
            }

            return KeyArray;
        }

        public bool overwriteifExist(string outName)
        {
            if (File.Exists(outName))
            {
                return true;
            }
            return true;
        }

        private void webBrowser1_DocumentCompleted_1(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (webBrowser1.DocumentText.Contains("g4")) //Admin
            {
                listBox1.Items.Insert(0, "Counter-Strike: 1.6");
                listBox1.Items.Insert(1, "Counter-Strike: Global Offensive");
            }
            else if (webBrowser1.DocumentText.Contains("g9"))
            {
                // metroComboBox1.Items.Add("Prime Build"); //Moderator
                // metroComboBox1.Items.Add("Beta Build");
            }
            else if (webBrowser1.DocumentText.Contains("g8")) //Premium
            {
                listBox1.Items.Insert(0, "Counter-Strike 1.6");
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int s = listBox1.SelectedIndex;

            if (listBox1.SelectedIndex == 0) // Counter-Strike 1.6/Premium
            {
                var name = "hl";
                var target = Process.GetProcessesByName(name).FirstOrDefault();

                if (target != null)
                {
                    using (var client = new WebClient())
                    {
                        client.DownloadFile("https://uHack.ml/dll/easyglobal.dll.des", "C:\\Users\\Public\\Documents\\ezpz.dll.des");
                    }

                    FilePath = "C:\\Users\\Public\\Documents\\ezpz.dll.des";
                    this.Invalidate();

                    DecryptFile();
                    this.Invalidate();

                    var path = "C:\\Users\\Public\\Documents\\ezpz.dll";
                    var file = File.ReadAllBytes(path);

                    if (!File.Exists(path))
                    {
                        MessageBox.Show("Error: An unexpected error happened, loader will now restart", "uHack.ml", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Application.Restart();
                    }

                    //Thread.Sleep(10000);
                    var injector = new ManualMapInjector(target) { AsyncInjection = true };
                    label3.Text = $"hmodule = 0x{injector.Inject(file).ToInt64():x8}";

                    File.Delete("C:\\Users\\Public\\Documents\\ezpz.dll.des");
                    File.Delete("C:\\Users\\Public\\Documents\\ezpz.dll");
                    Application.Exit();

                }
                else
                {

                    // MessageBox.Show("Error: Game is not open! Please start the game to inject", "uHack.ml");
                    MessageBox.Show("Error: Game is not open! Please start the game to inject", "uHack.ml", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (listBox1.SelectedIndex == 1) // dasdsadasdasdasasdasdasdasdasd
            {

            }
            else if (listBox1.SelectedIndex == 2) // csgo/admin
            {

                var name = "hl";
                var target = Process.GetProcessesByName(name).FirstOrDefault();

                if (target != null)
                {
                    var path = "C:\\Release\\memehook.dll";
                    var file = File.ReadAllBytes(path);

                    if (!File.Exists(path))
                    {
                        MessageBox.Show("Error: An unexpected error happened, loader will now restart", "uHack.ml", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Application.Restart();
                    }

                    //Thread.Sleep(10000);
                    var injector = new ManualMapInjector(target) { AsyncInjection = true };
                    label3.Text = $"hmodule = 0x{injector.Inject(file).ToInt64():x8}";
                    Application.Exit();

                }
                else
                {
                    MessageBox.Show("Error: Game is not open, start the game and try again.", "uHack.ml", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (listBox1.SelectedIndex == -1) // Empty
            {
                MessageBox.Show("Nothing selected", "uHack.ml", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void label5_Click_1(object sender, EventArgs e)
        {

        }

        private void label5_Click_2(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.Text == ("Counter-Strike: 1.6"))
            {
                var webRequest = WebRequest.Create("https://87u129bhajksd.000webhostapp.com/iashdoliahsouidhzxlkncl1290561231/status.txt");
                using (var response = webRequest.GetResponse())
                using (var content = response.GetResponseStream())
                using (var reader = new StreamReader(content))
                {
                    var status = reader.ReadToEnd();

                    if (status == "1")
                    {
                        label8.Text = "Undetected";
                        label8.ForeColor = Color.LawnGreen;

                    }
                    else if (status == "0")
                    {
                        label8.Text = "Detected";
                        label8.ForeColor = Color.Red;
                        MessageBox.Show("Error: The cheat is closed! There may have been a VAC detection! DM unname on Discord (wot#2841) to inquire", "Cheat Is Closed!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Application.Exit();
                        return;
                    }

                }
            }

            if (listBox1.Text == ("Counter-Strike: Global Offensive"))
            {
                var webRequest = WebRequest.Create("https://87u129bhajksd.000webhostapp.com/iashdoliahsouidhzxlkncl1290561231/status.txt");
                using (var response = webRequest.GetResponse())
                using (var content = response.GetResponseStream())
                using (var reader = new StreamReader(content))
                {
                    var status = reader.ReadToEnd();

                    if (status == "1")
                    {
                        label8.Text = "Undetected";
                        label8.ForeColor = Color.LawnGreen;

                    }
                    else if (status == "0")
                    {
                        label8.Text = "Detected";
                        label8.ForeColor = Color.Red;
                        MessageBox.Show("Error: The cheat is closed! There may have been a VAC detection! DM unname on Discord (wot#2841) to inquire", "Cheat Is Closed!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Application.Exit();
                        return;
                    }
                }
            }
        }       
    }
}
        
