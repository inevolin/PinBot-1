namespace PinBot
{
    partial class ucManage
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucManage));
            this.pic = new System.Windows.Forms.PictureBox();
            this.panel = new System.Windows.Forms.Panel();
            this.btnEditImg = new System.Windows.Forms.Button();
            this.picOK = new System.Windows.Forms.PictureBox();
            this.cboBoard = new System.Windows.Forms.ComboBox();
            this.txtDescription = new System.Windows.Forms.RichTextBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtSourceURL = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pic)).BeginInit();
            this.panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picOK)).BeginInit();
            this.SuspendLayout();
            // 
            // pic
            // 
            this.pic.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pic.Location = new System.Drawing.Point(0, 0);
            this.pic.Name = "pic";
            this.pic.Size = new System.Drawing.Size(281, 145);
            this.pic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pic.TabIndex = 0;
            this.pic.TabStop = false;
            // 
            // panel
            // 
            this.panel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel.Controls.Add(this.txtSourceURL);
            this.panel.Controls.Add(this.btnEditImg);
            this.panel.Controls.Add(this.picOK);
            this.panel.Controls.Add(this.cboBoard);
            this.panel.Controls.Add(this.txtDescription);
            this.panel.Controls.Add(this.btnDelete);
            this.panel.Controls.Add(this.btnSave);
            this.panel.Location = new System.Drawing.Point(0, 144);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(281, 116);
            this.panel.TabIndex = 1;
            // 
            // btnEditImg
            // 
            this.btnEditImg.Location = new System.Drawing.Point(167, 89);
            this.btnEditImg.Name = "btnEditImg";
            this.btnEditImg.Size = new System.Drawing.Size(35, 23);
            this.btnEditImg.TabIndex = 6;
            this.btnEditImg.Text = "edit";
            this.btnEditImg.UseVisualStyleBackColor = true;
            this.btnEditImg.Click += new System.EventHandler(this.btnEditImg_Click);
            // 
            // picOK
            // 
            this.picOK.Image = ((System.Drawing.Image)(resources.GetObject("picOK.Image")));
            this.picOK.Location = new System.Drawing.Point(96, 94);
            this.picOK.Name = "picOK";
            this.picOK.Size = new System.Drawing.Size(20, 18);
            this.picOK.TabIndex = 5;
            this.picOK.TabStop = false;
            this.picOK.Visible = false;
            // 
            // cboBoard
            // 
            this.cboBoard.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cboBoard.FormattingEnabled = true;
            this.cboBoard.Location = new System.Drawing.Point(18, 5);
            this.cboBoard.Name = "cboBoard";
            this.cboBoard.Size = new System.Drawing.Size(245, 21);
            this.cboBoard.TabIndex = 1;
            this.toolTip1.SetToolTip(this.cboBoard, "Choose a board for this Pin");
            // 
            // txtDescription
            // 
            this.txtDescription.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtDescription.Location = new System.Drawing.Point(18, 53);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(245, 35);
            this.txtDescription.TabIndex = 2;
            this.txtDescription.Text = "";
            this.toolTip1.SetToolTip(this.txtDescription, "Description for this Pin");
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnDelete.Location = new System.Drawing.Point(208, 90);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(55, 23);
            this.btnDelete.TabIndex = 4;
            this.btnDelete.Text = "delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(18, 90);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtSourceURL
            // 
            this.txtSourceURL.Location = new System.Drawing.Point(18, 29);
            this.txtSourceURL.Name = "txtSourceURL";
            this.txtSourceURL.Size = new System.Drawing.Size(245, 20);
            this.txtSourceURL.TabIndex = 7;
            this.toolTip1.SetToolTip(this.txtSourceURL, "Source URL for this Pin");
            // 
            // ucManage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.panel);
            this.Controls.Add(this.pic);
            this.Name = "ucManage";
            this.Size = new System.Drawing.Size(281, 263);
            ((System.ComponentModel.ISupportInitialize)(this.pic)).EndInit();
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picOK)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pic;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.RichTextBox txtDescription;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ComboBox cboBoard;
        private System.Windows.Forms.PictureBox picOK;
        private System.Windows.Forms.Button btnEditImg;
        private System.Windows.Forms.TextBox txtSourceURL;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
