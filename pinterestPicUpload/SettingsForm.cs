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
    public partial class SettingsForm : Form
    {
        public SettingsForm(Settings s, string lbl3, string lbl1 = "Timeout min:", string lbl2 = "Timeout max:")
        {
            InitializeComponent();
            labelmin.Text = lbl1;
            labelmax.Text = lbl2;
            label3.Text = lbl3;
            this.s = s;
            if (s != null)
            {
                txtMinTimeout.Text = s.TimeoutMin.ToString();
                txtMaxTimeout.Text = s.TimeoutMax.ToString();
                txtMax.Text = s.Max.ToString();
            }
        }
        public Settings s = null;
        private void Settings_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (int.Parse(txtMinTimeout.Text) > int.Parse(txtMaxTimeout.Text))
                {
                    MessageBox.Show("Timeout min must be smaller than max.", "Warning", MessageBoxButtons.OK);
                    txtMaxTimeout.Text = "" + (int.Parse(txtMinTimeout.Text) + 300);
                    e.Cancel = true;
                }
                
                int TimeoutMin = int.Parse(txtMinTimeout.Text);
                int TimeoutMax = int.Parse(txtMaxTimeout.Text);
                int Max = int.Parse(txtMax.Text);

                s = new Settings(TimeoutMin, TimeoutMax, Max);
            }
            catch { MessageBox.Show("Invalid fields", "Error", MessageBoxButtons.OK); e.Cancel = true; }

            /*if (int.Parse(txtPinMinTimeout.Text) < 60)
            {
                 MessageBox.Show("Pinterest does not like Spammers. Timeout min must be >= 60 seconds.", "Warning", MessageBoxButtons.OK);
                 txtPinMinTimeout.Text = "60";
                 txtPinMaxTimeout.Text = "300";
                 return;
            }*/
        }
       


    }
}
