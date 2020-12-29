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
    public partial class ImgImport : Form
    {
        private account acc;
        private bool TRIAL;

        public ImgImport(account acc,bool TRIAL)
        {
            InitializeComponent();
            this.acc = acc;
            this.TRIAL = TRIAL;
        }

        List<ucManage> ucm = new List<ucManage>();
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try {
                files.Filter = "Image files (*.jpg, *.jpeg, *.bmp, *.gif, *.png) | *.jpg; *.jpeg; *.bmp; *.gif; *.png";
                files.ShowDialog();

                foreach (string path in files.FileNames)
                {
                    BindingSource bs = new BindingSource(acc.boards, null);
                    ucManage um = new ucManage(path, "","", bs, null, true,TRIAL);
                    ucm.Add(um);
                    flp.Controls.Add(um);
                }
            }
            catch { MessageBox.Show("Error occured: #GenII", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < flp.Controls.Count; i++)
                {
                    Control c = flp.Controls[i];
                    if (c.GetType() == typeof(ucManage))
                        ((ucManage)c).save();

                }
            }
            catch { MessageBox.Show("Error occured: #SaveAll_II", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                flp.Controls.Clear();
            }
            catch { }
        }
    }
}
