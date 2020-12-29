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

    public partial class UsersMapping : Form
    {
        public Dictionary<string, List<string>> mapped = new Dictionary<string, List<string>>();

        public UsersMapping(account acc, Dictionary<string, List<string>> maps)
        {
            InitializeComponent();

            lstUsersMapping.DataSource = new BindingSource(acc.boards, null);
            lstUsersMapping.DisplayMember = "Value";
            lstUsersMapping.ValueMember = "Key";

            if (maps.Values.SelectMany(x => x).Count() > 0)
            {
                foreach (Object s in maps)
                {
                    KeyValuePair<string, List<string>> kv = (KeyValuePair<string, List<string>>)s;
                    mapped.Add(kv.Key, kv.Value);
                }
            }
            else
            {

                foreach (Object s in lstUsersMapping.Items)
                {
                    KeyValuePair<string, string> kv = (KeyValuePair<string, string>)s;
                    mapped.Add(kv.Key, new List<string>());
                }
            }

            try
            {
                txtPinCategories.Text = string.Join("\n", mapped[lstUsersMapping.SelectedValue.ToString()].ToArray());
            }
            catch { }
        }

        

        private void btnReset_Click(object sender, EventArgs e)
        {
            mapped = new Dictionary<string, List<string>>();
            txtPinCategories.Text = "";
            foreach (Object s in lstUsersMapping.Items)
            {
                KeyValuePair<string, string> kv = (KeyValuePair<string, string>)s;
                if (!mapped.ContainsKey(kv.Key))
                    mapped.Add(kv.Key, new List<string>());
            }
        }

        private void lstUsersMapping_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtPinCategories.Text = string.Join("\n", mapped[lstUsersMapping.SelectedValue.ToString()].ToArray());
            }
            catch { txtPinCategories.Text = ""; }
        }

        private void txtPinCategories_TextChanged(object sender, EventArgs e)
        {
            try
            {
                List<string> l = new List<string>(txtPinCategories.Text.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries));
                if (!mapped.ContainsKey(lstUsersMapping.SelectedValue.ToString()))
                    mapped.Add(lstUsersMapping.SelectedValue.ToString(), l);
                else
                    mapped[lstUsersMapping.SelectedValue.ToString()] = l;
            }
            catch { }
        }

        private void UsersMapping_FormClosing(object sender, FormClosingEventArgs e)
        {

            foreach (Object i in mapped)
            {
                KeyValuePair<string, List<string>> kv = (KeyValuePair<string, List<string>>)i;
                foreach (string s in kv.Value)
                {
                    if (!Regex.IsMatch(s, "/(.+?)/(.+?)/"))
                    {
                        
                        e.Cancel = true;
                    }
                }
            }
            if (e.Cancel)
            {
                MessageBox.Show("Invalid entried, please check", "Invalid entries", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            Dictionary<string, List<string>> keep = new Dictionary<string, List<string>>();
            foreach (KeyValuePair<string, List<string>> k in mapped)
            {
                if (k.Value.Count > 0)
                    keep.Add(k.Key, k.Value);
            }
            mapped = keep;
        }

    }
}
