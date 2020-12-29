using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Threading;

namespace updater
{
    public partial class Form1 : Form
    {
        private string mainprogram;
        public Form1()
        {
            InitializeComponent();
            btnUpdate.Enabled = false;
            
            this.TopMost = true;

            try
            {
                Cursor = Cursors.WaitCursor;
                if (File.Exists("./PinBot.exe"))
                    mainprogram = "PinBot.exe";
                else
                {
                    using (WebClient client = new WebClient())
                    {
                        client.DownloadFile("http://healzer.com/pinbot/PinBot.exe", "./PinBot.exe");
                    }
                    mainprogram = "PinBot.exe";
                }
                Thread t = new Thread(checkv);
                t.Start();
                Cursor = Cursors.Default;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR"); Environment.Exit(Environment.ExitCode); }
            
        }

        void checkv()
        {
            try
            {
                string pv = FileVersionInfo.GetVersionInfo("./" + mainprogram).ProductVersion;
                string cpv = http.GET("http://healzer.com/pinbot/update.txt", "", null, null);

                lbl.Invoke(new MethodInvoker(delegate
                {
                    lbl.Text = "Your version: " + pv + "\nLatest version: " + cpv; ;
                    lbl.ForeColor = Color.Green;
                }));
                btnUpdate.Invoke(new MethodInvoker(delegate
                {
                    btnUpdate.Enabled = false;
                }));
                if (int.Parse(cpv.Replace(".", "")) > int.Parse(pv.Replace(".", "")))
                {
                    lbl.Invoke(new MethodInvoker(delegate
                    {
                        lbl.ForeColor = Color.Red;
                    }));
                    btnUpdate.Invoke(new MethodInvoker(delegate
                    {
                        btnUpdate.Enabled = true;
                    }));
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR"); Environment.Exit(Environment.ExitCode); }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                int attempts = 0;
                while (File.Exists("./" + mainprogram))
                {
                    try
                    {
                        foreach (var process in Process.GetProcessesByName(mainprogram.Replace(".exe", "")))
                        {
                            process.Kill();
                        }
                        File.Delete("./" + mainprogram);
                        ++attempts;
                    }
                    catch { }
                    if (attempts > 5) throw new Exception();
                }
                using (WebClient client = new WebClient())
                {
                    client.DownloadFile("http://healzer.com/pinbot/PinBot.exe", "./PinBot.exe");
                }
                Thread t = new Thread(checkv);
                t.Start();
                Cursor = Cursors.Default;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR"); Environment.Exit(Environment.ExitCode); }
        }
    }
}
