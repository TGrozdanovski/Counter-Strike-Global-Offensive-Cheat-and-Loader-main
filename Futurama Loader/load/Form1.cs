using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using JLibrary.PortableExecutable;
using InjectionLibrary;



namespace load
{
    public partial class Form1 : Form
    {     
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.Text == ("Counter-Strike: Global Offensive"))
            {
                label6.Text = "Undetected";                
                label6.ForeColor = Color.Lime;
            }

            if (listBox1.Text == ("Counter-Strike: Source"))
            {
                label6.Text = "Outdated";
                
                label6.ForeColor = Color.Yellow;
            }

            if (listBox1.Text == ("Counter-Strike: 1.6"))
            {
                label6.Text = "Undetected";               
                label6.ForeColor = Color.Lime;
            }

            if (listBox1.Text == ("Team Fortress 2"))
            {
                label6.Text = "Outdated";                
                label6.ForeColor = Color.Yellow;
            }

            if (listBox1.Text == ("Garry's Mod"))
            {
                label6.Text = "Outdated";                
                label6.ForeColor = Color.Yellow;
            }
            
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (listBox1.Text == ("Counter-Strike: Global Offensive"))  // Futurama
            {
                var injector = InjectionMethod.Create(InjectionMethodType.Standard);
                var processid = Process.GetProcessesByName("csgo")[0].Id;
                var hModule = IntPtr.Zero;
                using (var cheat = new PortableExecutable(Properties.Resources.Futurama))
                    hModule = injector.Inject(cheat, processid);
                Application.Exit();
            }
        }
    }
}
