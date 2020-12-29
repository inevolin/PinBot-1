using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PinBot
{
    public partial class ManageCategories : Form
    {
        public List<string> cats = new List<string>();

        public ManageCategories(account acc, List<string> list)
        {
            InitializeComponent();
            cats = list;
            try
            {
                txtCats.Text = string.Join("\n", list.ToArray());
            }
            catch { }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            cats = new List<string>();
            txtCats.Text = "";
        }

        private void txtCats_TextChanged(object sender, EventArgs e)
        {
            try
            {
                List<string> l = new List<string>(txtCats.Text.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries));
                cats = l;
            }
            catch { }
        }
    }
}
