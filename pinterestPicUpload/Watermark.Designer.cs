namespace PinBot
{
    partial class Watermark
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Watermark));
            this.panelPos = new System.Windows.Forms.Panel();
            this.chkMID = new System.Windows.Forms.CheckBox();
            this.chkBOTRIGHT = new System.Windows.Forms.CheckBox();
            this.chkBOTLEFT = new System.Windows.Forms.CheckBox();
            this.chkTOPRIGHT = new System.Windows.Forms.CheckBox();
            this.chkTOPLEFT = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.file = new System.Windows.Forms.OpenFileDialog();
            this.btnFileWatermark = new System.Windows.Forms.Button();
            this.chkSafeWatermark = new System.Windows.Forms.CheckBox();
            this.chkRandomize = new System.Windows.Forms.CheckBox();
            this.picPreview = new System.Windows.Forms.PictureBox();
            this.panelPos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).BeginInit();
            this.SuspendLayout();
            // 
            // panelPos
            // 
            this.panelPos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelPos.Controls.Add(this.chkMID);
            this.panelPos.Controls.Add(this.chkBOTRIGHT);
            this.panelPos.Controls.Add(this.chkBOTLEFT);
            this.panelPos.Controls.Add(this.chkTOPRIGHT);
            this.panelPos.Controls.Add(this.chkTOPLEFT);
            this.panelPos.Location = new System.Drawing.Point(15, 25);
            this.panelPos.Name = "panelPos";
            this.panelPos.Size = new System.Drawing.Size(78, 73);
            this.panelPos.TabIndex = 0;
            // 
            // chkMID
            // 
            this.chkMID.AutoSize = true;
            this.chkMID.Location = new System.Drawing.Point(31, 28);
            this.chkMID.Name = "chkMID";
            this.chkMID.Size = new System.Drawing.Size(15, 14);
            this.chkMID.TabIndex = 4;
            this.chkMID.UseVisualStyleBackColor = true;
            this.chkMID.CheckedChanged += new System.EventHandler(this.pos);
            // 
            // chkBOTRIGHT
            // 
            this.chkBOTRIGHT.AutoSize = true;
            this.chkBOTRIGHT.Checked = true;
            this.chkBOTRIGHT.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBOTRIGHT.Location = new System.Drawing.Point(58, 54);
            this.chkBOTRIGHT.Name = "chkBOTRIGHT";
            this.chkBOTRIGHT.Size = new System.Drawing.Size(15, 14);
            this.chkBOTRIGHT.TabIndex = 3;
            this.chkBOTRIGHT.UseVisualStyleBackColor = true;
            this.chkBOTRIGHT.CheckedChanged += new System.EventHandler(this.pos);
            // 
            // chkBOTLEFT
            // 
            this.chkBOTLEFT.AutoSize = true;
            this.chkBOTLEFT.Location = new System.Drawing.Point(3, 54);
            this.chkBOTLEFT.Name = "chkBOTLEFT";
            this.chkBOTLEFT.Size = new System.Drawing.Size(15, 14);
            this.chkBOTLEFT.TabIndex = 2;
            this.chkBOTLEFT.UseVisualStyleBackColor = true;
            this.chkBOTLEFT.CheckedChanged += new System.EventHandler(this.pos);
            // 
            // chkTOPRIGHT
            // 
            this.chkTOPRIGHT.AutoSize = true;
            this.chkTOPRIGHT.Location = new System.Drawing.Point(58, 3);
            this.chkTOPRIGHT.Name = "chkTOPRIGHT";
            this.chkTOPRIGHT.Size = new System.Drawing.Size(15, 14);
            this.chkTOPRIGHT.TabIndex = 1;
            this.chkTOPRIGHT.UseVisualStyleBackColor = true;
            this.chkTOPRIGHT.CheckedChanged += new System.EventHandler(this.pos);
            // 
            // chkTOPLEFT
            // 
            this.chkTOPLEFT.AutoSize = true;
            this.chkTOPLEFT.Location = new System.Drawing.Point(3, 3);
            this.chkTOPLEFT.Name = "chkTOPLEFT";
            this.chkTOPLEFT.Size = new System.Drawing.Size(15, 14);
            this.chkTOPLEFT.TabIndex = 0;
            this.chkTOPLEFT.UseVisualStyleBackColor = true;
            this.chkTOPLEFT.CheckedChanged += new System.EventHandler(this.pos);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Positions:";
            // 
            // file
            // 
            this.file.FileName = "openFileDialog1";
            // 
            // btnFileWatermark
            // 
            this.btnFileWatermark.Location = new System.Drawing.Point(347, 25);
            this.btnFileWatermark.Name = "btnFileWatermark";
            this.btnFileWatermark.Size = new System.Drawing.Size(97, 23);
            this.btnFileWatermark.TabIndex = 4;
            this.btnFileWatermark.Text = "Select image ...";
            this.btnFileWatermark.UseVisualStyleBackColor = true;
            this.btnFileWatermark.Click += new System.EventHandler(this.btnFileWatermark_Click);
            // 
            // chkSafeWatermark
            // 
            this.chkSafeWatermark.AutoSize = true;
            this.chkSafeWatermark.Checked = true;
            this.chkSafeWatermark.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSafeWatermark.Location = new System.Drawing.Point(118, 96);
            this.chkSafeWatermark.Name = "chkSafeWatermark";
            this.chkSafeWatermark.Size = new System.Drawing.Size(132, 30);
            this.chkSafeWatermark.TabIndex = 6;
            this.chkSafeWatermark.Text = "Watermark only when\r\nimage is large enough.";
            this.chkSafeWatermark.UseVisualStyleBackColor = true;
            this.chkSafeWatermark.CheckedChanged += new System.EventHandler(this.chkSafeWatermark_CheckedChanged);
            // 
            // chkRandomize
            // 
            this.chkRandomize.AutoSize = true;
            this.chkRandomize.Checked = true;
            this.chkRandomize.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRandomize.Location = new System.Drawing.Point(118, 25);
            this.chkRandomize.Name = "chkRandomize";
            this.chkRandomize.Size = new System.Drawing.Size(194, 56);
            this.chkRandomize.TabIndex = 6;
            this.chkRandomize.Text = "Place watermarks randomly\r\non selected positions. If unchecked\r\na watermark will " +
    "be placed on each\r\nposition!";
            this.chkRandomize.UseVisualStyleBackColor = true;
            this.chkRandomize.CheckedChanged += new System.EventHandler(this.chkRandomize_CheckedChanged);
            // 
            // picPreview
            // 
            this.picPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.picPreview.Location = new System.Drawing.Point(347, 54);
            this.picPreview.Name = "picPreview";
            this.picPreview.Size = new System.Drawing.Size(167, 95);
            this.picPreview.TabIndex = 7;
            this.picPreview.TabStop = false;
            // 
            // Watermark
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(526, 161);
            this.Controls.Add(this.picPreview);
            this.Controls.Add(this.chkRandomize);
            this.Controls.Add(this.chkSafeWatermark);
            this.Controls.Add(this.btnFileWatermark);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panelPos);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(542, 199);
            this.Name = "Watermark";
            this.Text = "Watermark Settings";
            this.panelPos.ResumeLayout(false);
            this.panelPos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelPos;
        private System.Windows.Forms.CheckBox chkMID;
        private System.Windows.Forms.CheckBox chkBOTRIGHT;
        private System.Windows.Forms.CheckBox chkBOTLEFT;
        private System.Windows.Forms.CheckBox chkTOPRIGHT;
        private System.Windows.Forms.CheckBox chkTOPLEFT;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.OpenFileDialog file;
        private System.Windows.Forms.Button btnFileWatermark;
        private System.Windows.Forms.CheckBox chkSafeWatermark;
        private System.Windows.Forms.CheckBox chkRandomize;
        private System.Windows.Forms.PictureBox picPreview;
    }
}