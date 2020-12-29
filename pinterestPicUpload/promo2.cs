using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Windows.Forms;

namespace PinBot
{
    public partial class promo2 : Form
    {
        public bool OK = false;
        string returnData, code;
        string hid;

        public promo2()
        {
            hid = account.hardwareID();
            InitializeComponent();
            
        }
        private void promo2_Load(object sender, EventArgs e)
        {
            checkExists();
        }
        void checkExists()
        {
            p1.Enabled = false;
            this.Cursor = Cursors.WaitCursor;

            returnData = null;
            
            Thread t = new Thread(checkExists_POST);
            t.IsBackground = true;
            t.Start();

            while (returnData == null)
            {
                Application.DoEvents();
            }


            this.Cursor = Cursors.Default;
            if (returnData.Equals("1")) //email already in DB
            {
                OK = true;
                this.Close();
                return;
            }
            
            p1.Enabled = true;
            
        }
        void checkExists_POST()
        {
            try
            {
                
                StringBuilder strBuilder = new StringBuilder();
                strBuilder.AppendLine("hid=" + HttpUtility.UrlEncode(hid));
                byte[] hdrbytes = System.Text.Encoding.UTF8.GetBytes(strBuilder.ToString());
                List<byte[]> data = new List<byte[]>();
                data.Add(hdrbytes);

                returnData = http.POST("http://healzer.com/pinbot/trial.php", "", "application/x-www-form-urlencoded; charset=UTF-8", data, null, new System.Net.CookieContainer(), null);

            }
            catch { returnData = "error"; }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (txtEmail.Text.Length <= 0)
            {
                MessageBox.Show("Email address missing", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (txtName.Text.Length <= 0)
            {
                MessageBox.Show("Email address missing", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (! IsValidEmail(txtEmail.Text))
            {
                MessageBox.Show("Invalid email address", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            btnSubmit.Enabled = false;
            this.Cursor = Cursors.WaitCursor;

            string name = txtName.Text;
            string email = txtEmail.Text;

            returnData = null;
            Thread t = new Thread(() => submitData(name, email));
            t.IsBackground = true;
            t.Start();


            while (returnData == null)
                Application.DoEvents();


            this.Cursor = Cursors.Default;

            if (returnData.Equals("1")) //email already in DB
            {
                OK = true;
                this.Close();
                return;
            }
            else if (Regex.IsMatch(returnData, "^\\d\\d\\d\\d\\d\\d$"))
            {
                code = returnData;
                p1.Visible = p1.Enabled = false;
                p2.Visible = true;
            }
            else
            {
               // MessageBox.Show("Something went wrong. Please try again or contact support.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                btnSubmit.Enabled = true;
                return;
            }
        }
        void resend()
        {
            string name = txtName.Text;
            string email = txtEmail.Text;
            returnData = null;
            Thread t = new Thread(() => submitData(name, email));
            t.IsBackground = true;
            t.Start();


            while (returnData == null)
                Application.DoEvents();

            if (returnData.ToLower().Contains("[[[ERROR]]]"))
            {
                MessageBox.Show("Something went wrong. Please try again or contact support.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else if (returnData.Equals("1")) //email already in DB
            {
                OK = true;
                this.Close();
                return;
            }
            else {
                code = returnData;
                MessageBox.Show("Code has been sent to your mailbox", "Code sent", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }        
        void submitData(string name, string email, bool resend = false)
        {
            try
            {
                StringBuilder strBuilder = new StringBuilder();
                strBuilder.AppendLine("name=" + HttpUtility.UrlEncode(name) + "&email=" + HttpUtility.UrlEncode(email) + "&hid=" + HttpUtility.UrlEncode(hid) + (resend ? "&resend=1" : "") );
                byte[] hdrbytes = System.Text.Encoding.UTF8.GetBytes(strBuilder.ToString());
                List<byte[]> data = new List<byte[]>();
                data.Add(hdrbytes);

                returnData = http.POST("http://healzer.com/pinbot/trial.php","","application/x-www-form-urlencoded; charset=UTF-8",data,null,new System.Net.CookieContainer(),null);

            }
            catch { returnData = "error"; }
        }
        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private void btnVerifyCode_Click(object sender, EventArgs e)
        {

            if (!Regex.IsMatch(code, "\\d\\d\\d\\d\\d\\d"))
            {
                MessageBox.Show("Code must be 6 digits", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            btnVerifyCode.Enabled = false;
            this.Cursor = Cursors.WaitCursor;
            
            code = txtCode.Text;

            returnData = null;
            Thread t = new Thread(() => verifyCode(code));
            t.IsBackground = true;
            t.Start();

            while (returnData == null)
                Application.DoEvents();

            this.Cursor = Cursors.Default;
            if (returnData.ToLower().Contains("[[[ERROR]]]"))
            {
                MessageBox.Show("Something went wrong. Please try again or contact support.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                btnVerifyCode.Enabled = true;
                return;
            }
            else if (returnData.ToLower().Equals("invalid"))
            {
                if (MessageBox.Show("Invalid code! Resend confirmation code?", "Invalid code", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    resend();
                }
                btnVerifyCode.Enabled = true;
                return;
            }
            else if (returnData.ToLower().Equals("1"))
            {
                MessageBox.Show("Success! Enjoy using PinBot Trial", "Success", MessageBoxButtons.OK);
                OK = true;
                this.Close();
                return;
            }
            else
            {
                MessageBox.Show("Something went wrong. Please try again or contact support", "Fatal error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnVerifyCode.Enabled = true;
                return;
            }

            

        }
        void verifyCode(string code)
        {
            try
            {
                StringBuilder strBuilder = new StringBuilder();
                strBuilder.AppendLine("hid=" + HttpUtility.UrlEncode(hid) + "&code=" + HttpUtility.UrlEncode(code));
                byte[] hdrbytes = System.Text.Encoding.UTF8.GetBytes(strBuilder.ToString());
                List<byte[]> data = new List<byte[]>();
                data.Add(hdrbytes);

                returnData = http.POST("http://healzer.com/pinbot/trial.php", "", "application/x-www-form-urlencoded; charset=UTF-8", data, null, new System.Net.CookieContainer(), null);

            }
            catch { returnData = "error"; }
        }

        private void btnResend_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            resend();
            this.Cursor = Cursors.Default;
        }

        private void btnGoBack_Click(object sender, EventArgs e)
        {
            btnSubmit.Enabled = true;
            p1.Visible = p1.Enabled = true;
            p2.Visible = false;
        }

        private void btnPrivacy_Click(object sender, EventArgs e)
        {
            Process.Start("http://healzer.com/privacy.html");
        }

        

    }
}
