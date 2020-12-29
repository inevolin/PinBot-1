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
    public partial class Watermark : Form
    {
        public Watermark()
        {
            InitializeComponent();
            pos(null, null);
        }

        public List<string> positions = new List<string>();
        public Image image = null;
        public bool safeWatermark = true;
        public bool randomWatermark = true;

        private void pos(object sender, EventArgs e)
        {
            positions.Clear();
            foreach (Control c in panelPos.Controls)
            {
                if ( c.GetType() == typeof(CheckBox) && ((CheckBox)c).Checked )
                    positions.Add(((CheckBox)c).Name.Replace("chk",""));

            }
        }

        private void btnFileWatermark_Click(object sender, EventArgs e)
        {
            if (file.ShowDialog() == DialogResult.OK)
            {
                try {
                    Image img;
                    string path = file.InitialDirectory + file.FileName;
                    using (var bmpTemp = new Bitmap(path))
                    {
                        img = new Bitmap(bmpTemp);
                    }
                    picPreview.Image = img;
                    image = img;
                }
                catch {
                    MessageBox.Show("Invalid file, please use another one", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void chkSafeWatermark_CheckedChanged(object sender, EventArgs e)
        {
            safeWatermark = chkSafeWatermark.Checked;
        }

        private void chkRandomize_CheckedChanged(object sender, EventArgs e)
        {
            randomWatermark = chkRandomize.Checked;
        }



    }
}
