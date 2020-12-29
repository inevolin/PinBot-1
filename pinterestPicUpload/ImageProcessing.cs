using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace PinBot
{
    public partial class ImageProcessing : Form
    {

        ImageHandler imageHandler = new ImageHandler();
        double zoomFactor = 1;
        Image original;
        public Image BITMAP;
        public ImageProcessing(Image img)
        {
            InitializeComponent();

            for (int i = 1; i < 150; i++)
                cboWatermarkSize.Items.Add(i);

            WatermarkControls(false);
            zoomFactor = 1;

            /*Image img;
            using (var bmpTemp = new Bitmap(path))
            {
                img = new Bitmap(bmpTemp);
            }*/
            original = img;
            BITMAP = img;
            imageHandler.CurrentBitmap = new Bitmap(img);
            //imageHandler.BitmapPath = System.IO.Path.GetFileName(path);
            
            picPanel.AutoScroll = true;
            picPanel.AutoScrollMinSize = new Size(Convert.ToInt32(imageHandler.CurrentBitmap.Width * zoomFactor), Convert.ToInt32(imageHandler.CurrentBitmap.Height * zoomFactor));

            pic.Invalidate();

        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to restore to original?", "Reset", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                picZoom.Value = 10;

                btnClear.PerformClick();

                WatermarkControls(false);

                imageHandler.CurrentBitmap = new Bitmap(original);
                //imageHandler.BitmapPath = System.IO.Path.GetFileName(path);

                picPanel.AutoScroll = true;
                picPanel.AutoScrollMinSize = new Size(Convert.ToInt32(imageHandler.CurrentBitmap.Width * zoomFactor), Convert.ToInt32(imageHandler.CurrentBitmap.Height * zoomFactor));

                pic.Invalidate();
            }
        }

        protected void WatermarkControls(bool enabled)
        {
            txtWatermark.Enabled = cboWatermarkSize.Enabled = lblColor.Enabled = enabled;
        }
        private void btnInsertText_Click(object sender, EventArgs e)
        {
            WatermarkControls(true);

            customLabel l = new customLabel("mydomain.com");
            l.BackColor = Color.Transparent;
            l.Font = new Font("Arial", 20);
            l.ForeColor = Color.Red;
            l.MouseDown += new MouseEventHandler(customLabel_MouseDown);
            l.MouseMove += new MouseEventHandler(customLabel_MouseMove);
            l.MouseUp += new MouseEventHandler(customLabel_MouseUp);
            pic.Controls.Add(l);
            pic.Invalidate();
        }
        private void btnWatermarkInsertImg_Click(object sender, EventArgs e)
        {
            picZoom.Value = 10;
            if (file.ShowDialog() == DialogResult.OK)
            {
                customPictureBox img = new customPictureBox(new Bitmap(Bitmap.FromFile(file.InitialDirectory + file.FileName)));
                img.ContextMenuStrip = context;
                img.MouseDown += new MouseEventHandler(customPictureBox_MouseDown);
                img.MouseMove += new MouseEventHandler(customPictureBox_MouseMove);
                img.MouseUp += new MouseEventHandler(customPictureBox_MouseUp);
                img.Width = (int)Math.Round(img.Width * zoomFactor, 0);
                img.Height = (int)Math.Round(img.Height * zoomFactor, 0);
                pic.Controls.Add(img);
                pic.Invalidate();
            }
        }

        private void picZoom_Scroll(object sender, EventArgs e)
        {
            
            zoomFactor = double.Parse(picZoom.Value.ToString())/10;
            picPanel.AutoScrollMinSize = new Size(Convert.ToInt32(imageHandler.CurrentBitmap.Width * zoomFactor), Convert.ToInt32(imageHandler.CurrentBitmap.Height * zoomFactor));

            pic.Invalidate();
        }

        private void pic_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawImage(imageHandler.CurrentBitmap, new Rectangle(this.AutoScrollPosition.X, this.AutoScrollPosition.Y, Convert.ToInt32(imageHandler.CurrentBitmap.Width * zoomFactor), Convert.ToInt32(imageHandler.CurrentBitmap.Height * zoomFactor)));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            picZoom.Value = 10;
            foreach (var c in pic.Controls)
            {
                if (c.GetType() == typeof(customLabel))
                {
                    customLabel l = (customLabel)c;
                    imageHandler.InsertText(l.Text, (int)Math.Round(l.Location.X / zoomFactor, 0), (int)Math.Round(l.Location.Y / zoomFactor, 0), lblColor.Font.Name.ToString(), (float)Math.Round(l.Font.Size / zoomFactor, 0), null, lblColor.ForeColor.ToKnownColor().ToString(), null);
                    pic.Invalidate();
                }
                else if (c.GetType() == typeof(customPictureBox))
                {
                    customPictureBox l = (customPictureBox)c;
                    imageHandler.InsertImage((Bitmap)l.Image, (int)Math.Round(l.Location.X / zoomFactor, 0), (int)Math.Round(l.Location.Y / zoomFactor, 0));
                    pic.Invalidate();
                }
            }
            
            btnClear.PerformClick();
        }

        private void picPanel_MouseEnter(object sender, EventArgs e)
        {
            picPanel.Focus();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            pic.Controls.Clear();
        }




        customLabel selectedLabel = new customLabel("");
        void customLabel_MouseDown(object sender, MouseEventArgs e)
        {
            
            WatermarkControls(true);

            txtWatermark.Text = ((customLabel)sender).Text;
            cboWatermarkSize.Text = ((customLabel)sender).Font.Size.ToString();
            lblColor.ForeColor = ((customLabel)sender).ForeColor;

            customLabel l = ((customLabel)sender);
            selectedLabel = l;
            l.actcontrol = sender as Control;
            l.preloc = e.Location;
            Cursor = Cursors.Default;

        }
        void customLabel_MouseMove(object sender, MouseEventArgs e)
        {
            customLabel l = ((customLabel)sender);
            if ( l.actcontrol == null || l.actcontrol != sender)
                return;
            var location = l.actcontrol.Location;
            location.Offset(e.Location.X - l.preloc.X, e.Location.Y - l.preloc.Y);
            l.actcontrol.Location = location;

        }
        void customLabel_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                customLabel l = ((customLabel)sender);
                l.actcontrol = null;
                Cursor = Cursors.Default;

                if (l.Location.X < 0)
                    l.Location = new Point(0, l.Location.Y);
                if (l.Location.Y < 0)
                    l.Location = new Point(l.Location.X, 0);
                if (l.Location.X + l.Width > Math.Round(imageHandler.width * zoomFactor, 0) )
                    l.Location = new Point ( (int)Math.Round(imageHandler.width * zoomFactor, 0) - l.Width, l.Location.Y);
                if (l.Location.Y + l.Height > (int)Math.Round(imageHandler.height * zoomFactor, 0) )
                    l.Location = new Point(l.Location.X, (int)Math.Round(imageHandler.height * zoomFactor, 0) - l.Height);
            }
            catch { }
        }

        customPictureBox selectedPictureBox = new customPictureBox(null);
        void customPictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            picZoom.Value = 10;
            customPictureBox l = ((customPictureBox)sender);
            selectedPictureBox = l;
            l.actcontrol = sender as Control;
            l.preloc = e.Location;
            Cursor = Cursors.Default;

        }
        void customPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            customPictureBox l = ((customPictureBox)sender);
            if (l.actcontrol == null || l.actcontrol != sender)
                return;
            var location = l.actcontrol.Location;
            location.Offset(e.Location.X - l.preloc.X, e.Location.Y - l.preloc.Y);
            l.actcontrol.Location = location;

        }
        void customPictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                customPictureBox l = ((customPictureBox)sender);
                l.actcontrol = null;
                Cursor = Cursors.Default;

                if (l.Location.X < 0)
                    l.Location = new Point(0, l.Location.Y);
                if (l.Location.Y < 0)
                    l.Location = new Point(l.Location.X, 0);
                if (l.Location.X + l.Width > Math.Round(imageHandler.width * zoomFactor, 0))
                    l.Location = new Point((int)Math.Round(imageHandler.width * zoomFactor, 0) - l.Width, l.Location.Y);
                if (l.Location.Y + l.Height > (int)Math.Round(imageHandler.height * zoomFactor, 0))
                    l.Location = new Point(l.Location.X, (int)Math.Round(imageHandler.height * zoomFactor, 0) - l.Height);
            }
            catch { }
        }
        private void delete_Click(object sender, EventArgs e)
        {
            pic.Controls.Remove(selectedPictureBox);
        }

        private void txtWatermark_TextChanged(object sender, EventArgs e)
        {
            selectedLabel.Text = txtWatermark.Text;
        }

        private void cboWatermarkSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Font f = new Font("Arial", float.Parse(cboWatermarkSize.Text));
            selectedLabel.Font = f;
        }

        private void lblColor_Click(object sender, EventArgs e)
        {
            color.ShowDialog();
            selectedLabel.ForeColor = color.Color;
            lblColor.ForeColor = color.Color;
        }



        
        private void btnColor_Gamma_Click(object sender, EventArgs e)
        {
            GammaForm gFrm = new GammaForm();
            gFrm.RedComponent = gFrm.GreenComponent = gFrm.BlueComponent = 0;
            if (gFrm.ShowDialog() == DialogResult.OK)
            {
                this.Cursor = Cursors.WaitCursor;
                imageHandler.RestorePrevious();
                imageHandler.SetGamma(gFrm.RedComponent, gFrm.GreenComponent, gFrm.BlueComponent);
                pic.Invalidate();
                this.Cursor = Cursors.Default;
            }
            pic.Refresh();
        }

        private void btnColor_FilterRed_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            imageHandler.RestorePrevious();
            imageHandler.SetColorFilter(ImageHandler.ColorFilterTypes.Red);
            pic.Invalidate();
            this.Cursor = Cursors.Default;
            pic.Refresh();
        }

        private void btnColor_FilterGreen_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            imageHandler.RestorePrevious();
            imageHandler.SetColorFilter(ImageHandler.ColorFilterTypes.Green);
            pic.Invalidate();
            this.Cursor = Cursors.Default;
            pic.Refresh();
        }

        private void btnColor_FilterBlue_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            imageHandler.RestorePrevious();
            imageHandler.SetColorFilter(ImageHandler.ColorFilterTypes.Blue);
            pic.Invalidate();
            this.Cursor = Cursors.Default;
            pic.Refresh();
        }

        private void btnColor_Brightness_Click(object sender, EventArgs e)
        {
            BrightnessForm bFrm = new BrightnessForm();
            bFrm.BrightnessValue = 0;
            if (bFrm.ShowDialog() == DialogResult.OK)
            {
                this.Cursor = Cursors.WaitCursor;
                imageHandler.RestorePrevious();
                imageHandler.SetBrightness(bFrm.BrightnessValue);
                pic.Invalidate();
                this.Cursor = Cursors.Default;
            }
            pic.Refresh();
        }

        private void btnColor_Contrast_Click(object sender, EventArgs e)
        {
            ContrastForm cFrm = new ContrastForm();
            cFrm.ContrastValue = 0;
            if (cFrm.ShowDialog() == DialogResult.OK)
            {
                this.Cursor = Cursors.WaitCursor;
                imageHandler.RestorePrevious();
                imageHandler.SetContrast(cFrm.ContrastValue);
                pic.Invalidate();
                this.Cursor = Cursors.Default;
            }
            pic.Refresh();
        }

        private void btnColor_Grayscale_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            imageHandler.RestorePrevious();
            imageHandler.SetGrayscale();
            pic.Invalidate();
            this.Cursor = Cursors.Default;
            pic.Refresh();
        }

        private void btnInvert_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            imageHandler.RestorePrevious();
            imageHandler.SetInvert();
            pic.Invalidate();
            this.Cursor = Cursors.Default;
            pic.Refresh();
        }

        private void btnImage_Crop_Click(object sender, EventArgs e)
        {
            CropForm cpFrm = new CropForm();
            cpFrm.CropXPosition = 0;
            cpFrm.CropYPosition = 0;
            cpFrm.CropWidth = imageHandler.CurrentBitmap.Width;
            cpFrm.CropHeight = imageHandler.CurrentBitmap.Height;
            if (cpFrm.ShowDialog() == DialogResult.OK)
            {
                this.Cursor = Cursors.WaitCursor;
                imageHandler.RestorePrevious();
                imageHandler.DrawOutCropArea(cpFrm.CropXPosition, cpFrm.CropYPosition, cpFrm.CropWidth, cpFrm.CropHeight);
                pic.Invalidate();
                if (MessageBox.Show("Do u want to crop this area?", "ImageProcessing", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    imageHandler.Crop(cpFrm.CropXPosition, cpFrm.CropYPosition, cpFrm.CropWidth, cpFrm.CropHeight);
                }
                else
                {
                    imageHandler.RemoveCropAreaDraw();
                }
                pic.Invalidate();
                this.Cursor = Cursors.Default;
            }
            pic.Refresh();
        }

        private void btnImage_Rot90_Click(object sender, EventArgs e)
        {
            imageHandler.RotateFlip(RotateFlipType.Rotate90FlipNone);
            picPanel.AutoScroll = true;
            picPanel.AutoScrollMinSize = new Size(Convert.ToInt32(imageHandler.CurrentBitmap.Width * zoomFactor), Convert.ToInt32(imageHandler.CurrentBitmap.Height * zoomFactor));
            pic.Invalidate();
            pic.Refresh();
        }

        private void btnImage_Rot180_Click(object sender, EventArgs e)
        {
            imageHandler.RotateFlip(RotateFlipType.Rotate180FlipNone);
            picPanel.AutoScroll = true;
            picPanel.AutoScrollMinSize = new Size(Convert.ToInt32(imageHandler.CurrentBitmap.Width * zoomFactor), Convert.ToInt32(imageHandler.CurrentBitmap.Height * zoomFactor));
            pic.Invalidate();
            pic.Refresh();
        }

        private void btnImage_Rot270_Click(object sender, EventArgs e)
        {
            imageHandler.RotateFlip(RotateFlipType.Rotate270FlipNone);
            picPanel.AutoScroll = true;
            picPanel.AutoScrollMinSize = new Size(Convert.ToInt32(imageHandler.CurrentBitmap.Width * zoomFactor), Convert.ToInt32(imageHandler.CurrentBitmap.Height * zoomFactor));
            pic.Invalidate();
            pic.Refresh();
        }

        private void btnImage_FlipHoriz_Click(object sender, EventArgs e)
        {
            imageHandler.RotateFlip(RotateFlipType.RotateNoneFlipX);
            picPanel.AutoScroll = true;
            picPanel.AutoScrollMinSize = new Size(Convert.ToInt32(imageHandler.CurrentBitmap.Width * zoomFactor), Convert.ToInt32(imageHandler.CurrentBitmap.Height * zoomFactor));
            pic.Invalidate();
            pic.Refresh();
        }

        private void btnImage_FlipVertical_Click(object sender, EventArgs e)
        {
            imageHandler.RotateFlip(RotateFlipType.RotateNoneFlipY);
            picPanel.AutoScroll = true;
            picPanel.AutoScrollMinSize = new Size(Convert.ToInt32(imageHandler.CurrentBitmap.Width * zoomFactor), Convert.ToInt32(imageHandler.CurrentBitmap.Height * zoomFactor));
            pic.Invalidate();
            pic.Refresh();
            
        }

        
        private void btnSave_Click(object sender, EventArgs e)
        {
            //SavedAs = @".\imgs\" + imageHandler.CurrentBitmap.GetHashCode().ToString() + ".jpg";
            //imageHandler.SaveBitmap( SavedAs );
            BITMAP = imageHandler.CurrentBitmap;
            this.Close();
        }

       

      

       


    }

    class customLabel : Label
    {
        public customLabel(string text)
        {
            this.Location = new Point(0, 0);
            this.AutoSize = true;
            this.Text = text;
        }

        public Control actcontrol;
        public Point preloc;
    }

    class customPictureBox : PictureBox
    {
        public customPictureBox(Bitmap img)
        {
            this.Location = new Point(0, 0);
            this.AutoSize = true;
            this.Image = img;
            this.SizeMode = PictureBoxSizeMode.Normal;
        }

        public Control actcontrol;
        public Point preloc;
    }
}
