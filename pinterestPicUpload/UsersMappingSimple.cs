using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace PinBot
{

    public partial class UsersMappingSimple : Form
    {
        public List<string> users = new List<string>();

        public UsersMappingSimple(account acc, List<string> list)
        {
            InitializeComponent();
            users = list;
            try
            {
                txtPinCategories.Text = string.Join("\n", users.ToArray());
            }
            catch { }
        }

        

        private void btnReset_Click(object sender, EventArgs e)
        {
            users = new List<string>();
            txtPinCategories.Text = "";
           
        }

        private void txtPinCategories_TextChanged(object sender, EventArgs e)
        {
            try
            {
                List<string> l = new List<string>(txtPinCategories.Text.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries));
                users = l;
            }
            catch { }
        }

        private void UsersMapping_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (users.Count < 1)
                return;
            foreach (string s in users)
            {
                if (!Regex.IsMatch(s, "/(.+?)/(.+?)/"))
                {

                    e.Cancel = true;
                }

            }
            if (e.Cancel)
                MessageBox.Show("Invalid entried, please check", "Invalid entries", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

    }
}
