namespace PinBot
{
    partial class ImageProcessing
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImageProcessing));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnImage_FlipVertical = new System.Windows.Forms.Button();
            this.btnImage_FlipHoriz = new System.Windows.Forms.Button();
            this.btnImage_Rot270 = new System.Windows.Forms.Button();
            this.btnImage_Rot180 = new System.Windows.Forms.Button();
            this.btnImage_Rot90 = new System.Windows.Forms.Button();
            this.btnImage_Crop = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnColor_FilterBlue = new System.Windows.Forms.Button();
            this.btnColor_FilterGreen = new System.Windows.Forms.Button();
            this.btnColor_FilterRed = new System.Windows.Forms.Button();
            this.btnColor_Contrast = new System.Windows.Forms.Button();
            this.btnColor_Brightness = new System.Windows.Forms.Button();
            this.btnInvert = new System.Windows.Forms.Button();
            this.btnColor_Grayscale = new System.Windows.Forms.Button();
            this.btnColor_Gamma = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.btnWatermarkInsertImg = new System.Windows.Forms.Button();
            this.lblColor = new System.Windows.Forms.Label();
            this.cboWatermarkSize = new System.Windows.Forms.ComboBox();
            this.txtWatermark = new System.Windows.Forms.RichTextBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnInsertText = new System.Windows.Forms.Button();
            this.picZoom = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.picPanel = new System.Windows.Forms.Panel();
            this.pic = new System.Windows.Forms.PictureBox();
            this.color = new System.Windows.Forms.ColorDialog();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.file = new System.Windows.Forms.OpenFileDialog();
            this.context = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.delete = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picZoom)).BeginInit();
            this.picPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic)).BeginInit();
            this.context.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(466, 16);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(230, 316);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnImage_FlipVertical);
            this.tabPage1.Controls.Add(this.btnImage_FlipHoriz);
            this.tabPage1.Controls.Add(this.btnImage_Rot270);
            this.tabPage1.Controls.Add(this.btnImage_Rot180);
            this.tabPage1.Controls.Add(this.btnImage_Rot90);
            this.tabPage1.Controls.Add(this.btnImage_Crop);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.btnColor_FilterBlue);
            this.tabPage1.Controls.Add(this.btnColor_FilterGreen);
            this.tabPage1.Controls.Add(this.btnColor_FilterRed);
            this.tabPage1.Controls.Add(this.btnColor_Contrast);
            this.tabPage1.Controls.Add(this.btnColor_Brightness);
            this.tabPage1.Controls.Add(this.btnInvert);
            this.tabPage1.Controls.Add(this.btnColor_Grayscale);
            this.tabPage1.Controls.Add(this.btnColor_Gamma);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(222, 290);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Image";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnImage_FlipVertical
            // 
            this.btnImage_FlipVertical.Location = new System.Drawing.Point(143, 184);
            this.btnImage_FlipVertical.Name = "btnImage_FlipVertical";
            this.btnImage_FlipVertical.Size = new System.Drawing.Size(60, 23);
            this.btnImage_FlipVertical.TabIndex = 7;
            this.btnImage_FlipVertical.Text = "Flip Vert";
            this.btnImage_FlipVertical.UseVisualStyleBackColor = true;
            this.btnImage_FlipVertical.Click += new System.EventHandler(this.btnImage_FlipVertical_Click);
            // 
            // btnImage_FlipHoriz
            // 
            this.btnImage_FlipHoriz.Location = new System.Drawing.Point(77, 184);
            this.btnImage_FlipHoriz.Name = "btnImage_FlipHoriz";
            this.btnImage_FlipHoriz.Size = new System.Drawing.Size(60, 23);
            this.btnImage_FlipHoriz.TabIndex = 7;
            this.btnImage_FlipHoriz.Text = "Flip Horiz";
            this.btnImage_FlipHoriz.UseVisualStyleBackColor = true;
            this.btnImage_FlipHoriz.Click += new System.EventHandler(this.btnImage_FlipHoriz_Click);
            // 
            // btnImage_Rot270
            // 
            this.btnImage_Rot270.Location = new System.Drawing.Point(143, 213);
            this.btnImage_Rot270.Name = "btnImage_Rot270";
            this.btnImage_Rot270.Size = new System.Drawing.Size(60, 23);
            this.btnImage_Rot270.TabIndex = 7;
            this.btnImage_Rot270.Text = "Rot 270°";
            this.btnImage_Rot270.UseVisualStyleBackColor = true;
            this.btnImage_Rot270.Click += new System.EventHandler(this.btnImage_Rot270_Click);
            // 
            // btnImage_Rot180
            // 
            this.btnImage_Rot180.Location = new System.Drawing.Point(77, 213);
            this.btnImage_Rot180.Name = "btnImage_Rot180";
            this.btnImage_Rot180.Size = new System.Drawing.Size(60, 23);
            this.btnImage_Rot180.TabIndex = 7;
            this.btnImage_Rot180.Text = "Rot 180°";
            this.btnImage_Rot180.UseVisualStyleBackColor = true;
            this.btnImage_Rot180.Click += new System.EventHandler(this.btnImage_Rot180_Click);
            // 
            // btnImage_Rot90
            // 
            this.btnImage_Rot90.Location = new System.Drawing.Point(11, 213);
            this.btnImage_Rot90.Name = "btnImage_Rot90";
            this.btnImage_Rot90.Size = new System.Drawing.Size(60, 23);
            this.btnImage_Rot90.TabIndex = 7;
            this.btnImage_Rot90.Text = "Rot 90°";
            this.btnImage_Rot90.UseVisualStyleBackColor = true;
            this.btnImage_Rot90.Click += new System.EventHandler(this.btnImage_Rot90_Click);
            // 
            // btnImage_Crop
            // 
            this.btnImage_Crop.Location = new System.Drawing.Point(11, 184);
            this.btnImage_Crop.Name = "btnImage_Crop";
            this.btnImage_Crop.Size = new System.Drawing.Size(60, 23);
            this.btnImage_Crop.TabIndex = 7;
            this.btnImage_Crop.Text = "Crop";
            this.btnImage_Crop.UseVisualStyleBackColor = true;
            this.btnImage_Crop.Click += new System.EventHandler(this.btnImage_Crop_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 79);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Corrections:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 168);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Image:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Filters:";
            // 
            // btnColor_FilterBlue
            // 
            this.btnColor_FilterBlue.Location = new System.Drawing.Point(143, 38);
            this.btnColor_FilterBlue.Name = "btnColor_FilterBlue";
            this.btnColor_FilterBlue.Size = new System.Drawing.Size(62, 23);
            this.btnColor_FilterBlue.TabIndex = 4;
            this.btnColor_FilterBlue.Text = "Blue";
            this.btnColor_FilterBlue.UseVisualStyleBackColor = true;
            this.btnColor_FilterBlue.Click += new System.EventHandler(this.btnColor_FilterBlue_Click);
            // 
            // btnColor_FilterGreen
            // 
            this.btnColor_FilterGreen.Location = new System.Drawing.Point(75, 38);
            this.btnColor_FilterGreen.Name = "btnColor_FilterGreen";
            this.btnColor_FilterGreen.Size = new System.Drawing.Size(62, 23);
            this.btnColor_FilterGreen.TabIndex = 3;
            this.btnColor_FilterGreen.Text = "Green";
            this.btnColor_FilterGreen.UseVisualStyleBackColor = true;
            this.btnColor_FilterGreen.Click += new System.EventHandler(this.btnColor_FilterGreen_Click);
            // 
            // btnColor_FilterRed
            // 
            this.btnColor_FilterRed.Location = new System.Drawing.Point(9, 38);
            this.btnColor_FilterRed.Name = "btnColor_FilterRed";
            this.btnColor_FilterRed.Size = new System.Drawing.Size(62, 23);
            this.btnColor_FilterRed.TabIndex = 2;
            this.btnColor_FilterRed.Text = "Red";
            this.btnColor_FilterRed.UseVisualStyleBackColor = true;
            this.btnColor_FilterRed.Click += new System.EventHandler(this.btnColor_FilterRed_Click);
            // 
            // btnColor_Contrast
            // 
            this.btnColor_Contrast.Location = new System.Drawing.Point(150, 95);
            this.btnColor_Contrast.Name = "btnColor_Contrast";
            this.btnColor_Contrast.Size = new System.Drawing.Size(55, 23);
            this.btnColor_Contrast.TabIndex = 0;
            this.btnColor_Contrast.Text = "Contrast";
            this.btnColor_Contrast.UseVisualStyleBackColor = true;
            this.btnColor_Contrast.Click += new System.EventHandler(this.btnColor_Contrast_Click);
            // 
            // btnColor_Brightness
            // 
            this.btnColor_Brightness.Location = new System.Drawing.Point(77, 95);
            this.btnColor_Brightness.Name = "btnColor_Brightness";
            this.btnColor_Brightness.Size = new System.Drawing.Size(67, 23);
            this.btnColor_Brightness.TabIndex = 0;
            this.btnColor_Brightness.Text = "Brightness";
            this.btnColor_Brightness.UseVisualStyleBackColor = true;
            this.btnColor_Brightness.Click += new System.EventHandler(this.btnColor_Brightness_Click);
            // 
            // btnInvert
            // 
            this.btnInvert.Location = new System.Drawing.Point(77, 124);
            this.btnInvert.Name = "btnInvert";
            this.btnInvert.Size = new System.Drawing.Size(62, 23);
            this.btnInvert.TabIndex = 0;
            this.btnInvert.Text = "Invert";
            this.btnInvert.UseVisualStyleBackColor = true;
            this.btnInvert.Click += new System.EventHandler(this.btnInvert_Click);
            // 
            // btnColor_Grayscale
            // 
            this.btnColor_Grayscale.Location = new System.Drawing.Point(9, 124);
            this.btnColor_Grayscale.Name = "btnColor_Grayscale";
            this.btnColor_Grayscale.Size = new System.Drawing.Size(62, 23);
            this.btnColor_Grayscale.TabIndex = 0;
            this.btnColor_Grayscale.Text = "Grayscale";
            this.btnColor_Grayscale.UseVisualStyleBackColor = true;
            this.btnColor_Grayscale.Click += new System.EventHandler(this.btnColor_Grayscale_Click);
            // 
            // btnColor_Gamma
            // 
            this.btnColor_Gamma.Location = new System.Drawing.Point(9, 95);
            this.btnColor_Gamma.Name = "btnColor_Gamma";
            this.btnColor_Gamma.Size = new System.Drawing.Size(62, 23);
            this.btnColor_Gamma.TabIndex = 0;
            this.btnColor_Gamma.Text = "Gamma";
            this.btnColor_Gamma.UseVisualStyleBackColor = true;
            this.btnColor_Gamma.Click += new System.EventHandler(this.btnColor_Gamma_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.btnWatermarkInsertImg);
            this.tabPage3.Controls.Add(this.lblColor);
            this.tabPage3.Controls.Add(this.cboWatermarkSize);
            this.tabPage3.Controls.Add(this.txtWatermark);
            this.tabPage3.Controls.Add(this.btnClear);
            this.tabPage3.Controls.Add(this.btnApply);
            this.tabPage3.Controls.Add(this.btnInsertText);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(222, 290);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Watermark";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // btnWatermarkInsertImg
            // 
            this.btnWatermarkInsertImg.Location = new System.Drawing.Point(12, 192);
            this.btnWatermarkInsertImg.Name = "btnWatermarkInsertImg";
            this.btnWatermarkInsertImg.Size = new System.Drawing.Size(96, 23);
            this.btnWatermarkInsertImg.TabIndex = 6;
            this.btnWatermarkInsertImg.Text = "Insert image...";
            this.btnWatermarkInsertImg.UseVisualStyleBackColor = true;
            this.btnWatermarkInsertImg.Click += new System.EventHandler(this.btnWatermarkInsertImg_Click);
            // 
            // lblColor
            // 
            this.lblColor.AutoSize = true;
            this.lblColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblColor.Location = new System.Drawing.Point(87, 151);
            this.lblColor.Name = "lblColor";
            this.lblColor.Size = new System.Drawing.Size(54, 15);
            this.lblColor.TabIndex = 5;
            this.lblColor.Text = "ForeColor";
            this.lblColor.Click += new System.EventHandler(this.lblColor_Click);
            // 
            // cboWatermarkSize
            // 
            this.cboWatermarkSize.FormattingEnabled = true;
            this.cboWatermarkSize.Location = new System.Drawing.Point(12, 148);
            this.cboWatermarkSize.Name = "cboWatermarkSize";
            this.cboWatermarkSize.Size = new System.Drawing.Size(54, 21);
            this.cboWatermarkSize.TabIndex = 4;
            this.cboWatermarkSize.SelectedIndexChanged += new System.EventHandler(this.cboWatermarkSize_SelectedIndexChanged);
            // 
            // txtWatermark
            // 
            this.txtWatermark.Location = new System.Drawing.Point(12, 46);
            this.txtWatermark.Name = "txtWatermark";
            this.txtWatermark.Size = new System.Drawing.Size(200, 96);
            this.txtWatermark.TabIndex = 3;
            this.txtWatermark.Text = "";
            this.txtWatermark.TextChanged += new System.EventHandler(this.txtWatermark_TextChanged);
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.Location = new System.Drawing.Point(137, 264);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 2;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnApply
            // 
            this.btnApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApply.Location = new System.Drawing.Point(56, 264);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 1;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnInsertText
            // 
            this.btnInsertText.Location = new System.Drawing.Point(12, 17);
            this.btnInsertText.Name = "btnInsertText";
            this.btnInsertText.Size = new System.Drawing.Size(75, 23);
            this.btnInsertText.TabIndex = 0;
            this.btnInsertText.Text = "Insert text";
            this.btnInsertText.UseVisualStyleBackColor = true;
            this.btnInsertText.Click += new System.EventHandler(this.btnInsertText_Click);
            // 
            // picZoom
            // 
            this.picZoom.Location = new System.Drawing.Point(245, 1);
            this.picZoom.Maximum = 20;
            this.picZoom.Minimum = 1;
            this.picZoom.Name = "picZoom";
            this.picZoom.Size = new System.Drawing.Size(214, 45);
            this.picZoom.TabIndex = 3;
            this.picZoom.Value = 10;
            this.picZoom.Scroll += new System.EventHandler(this.picZoom_Scroll);
            this.picZoom.ValueChanged += new System.EventHandler(this.picZoom_Scroll);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(202, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Zoom:";
            // 
            // picPanel
            // 
            this.picPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.picPanel.AutoScroll = true;
            this.picPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picPanel.Controls.Add(this.pic);
            this.picPanel.Location = new System.Drawing.Point(3, 36);
            this.picPanel.Name = "picPanel";
            this.picPanel.Size = new System.Drawing.Size(460, 296);
            this.picPanel.TabIndex = 5;
            this.picPanel.MouseEnter += new System.EventHandler(this.picPanel_MouseEnter);
            // 
            // pic
            // 
            this.pic.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pic.Location = new System.Drawing.Point(3, 3);
            this.pic.Name = "pic";
            this.pic.Size = new System.Drawing.Size(451, 288);
            this.pic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pic.TabIndex = 0;
            this.pic.TabStop = false;
            this.pic.Paint += new System.Windows.Forms.PaintEventHandler(this.pic_Paint);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(139, 4);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(57, 23);
            this.btnReset.TabIndex = 6;
            this.btnReset.Text = "reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(13, 4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(107, 23);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "Save and close";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // file
            // 
            this.file.FileName = "openFileDialog1";
            // 
            // context
            // 
            this.context.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.delete});
            this.context.Name = "context";
            this.context.Size = new System.Drawing.Size(107, 26);
            // 
            // delete
            // 
            this.delete.Name = "delete";
            this.delete.Size = new System.Drawing.Size(106, 22);
            this.delete.Text = "delete";
            this.delete.Click += new System.EventHandler(this.delete_Click);
            // 
            // ImageProcessing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(698, 337);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.picPanel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.picZoom);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ImageProcessing";
            this.Text = "ImageProcessing";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picZoom)).EndInit();
            this.picPanel.ResumeLayout(false);
            this.picPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic)).EndInit();
            this.context.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pic;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button btnInsertText;
        private System.Windows.Forms.TrackBar picZoom;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel picPanel;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label lblColor;
        private System.Windows.Forms.ComboBox cboWatermarkSize;
        private System.Windows.Forms.RichTextBox txtWatermark;
        private System.Windows.Forms.ColorDialog color;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnColor_FilterRed;
        private System.Windows.Forms.Button btnColor_Gamma;
        private System.Windows.Forms.Button btnColor_FilterBlue;
        private System.Windows.Forms.Button btnColor_FilterGreen;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnColor_Contrast;
        private System.Windows.Forms.Button btnColor_Brightness;
        private System.Windows.Forms.Button btnInvert;
        private System.Windows.Forms.Button btnColor_Grayscale;
        private System.Windows.Forms.Button btnImage_FlipVertical;
        private System.Windows.Forms.Button btnImage_FlipHoriz;
        private System.Windows.Forms.Button btnImage_Rot270;
        private System.Windows.Forms.Button btnImage_Rot180;
        private System.Windows.Forms.Button btnImage_Rot90;
        private System.Windows.Forms.Button btnImage_Crop;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnWatermarkInsertImg;
        private System.Windows.Forms.OpenFileDialog file;
        private System.Windows.Forms.ContextMenuStrip context;
        private System.Windows.Forms.ToolStripMenuItem delete;
    }
}