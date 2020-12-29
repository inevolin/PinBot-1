namespace PinBot
{
    partial class promo2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(promo2));
            this.btnSubmit = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnPrivacy = new System.Windows.Forms.Button();
            this.p1 = new System.Windows.Forms.Panel();
            this.p2 = new System.Windows.Forms.Panel();
            this.txtCode = new System.Windows.Forms.MaskedTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnVerifyCode = new System.Windows.Forms.Button();
            this.btnResend = new System.Windows.Forms.Button();
            this.btnGoBack = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.p1.SuspendLayout();
            this.p2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSubmit
            // 
            this.btnSubmit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSubmit.Location = new System.Drawing.Point(246, 101);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 2;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(64, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Email address:";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(145, 49);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(176, 20);
            this.txtEmail.TabIndex = 0;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(145, 75);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(176, 20);
            this.txtName.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(84, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Full name:";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(6, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(403, 29);
            this.label3.TabIndex = 14;
            this.label3.Text = "Registration for PinBot Trial, we kindly request you to provide us with your name" +
    " and email address. A confirmation email will be sent to your email address to v" +
    "erify it.";
            // 
            // btnPrivacy
            // 
            this.btnPrivacy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPrivacy.Location = new System.Drawing.Point(6, 122);
            this.btnPrivacy.Name = "btnPrivacy";
            this.btnPrivacy.Size = new System.Drawing.Size(92, 23);
            this.btnPrivacy.TabIndex = 3;
            this.btnPrivacy.Text = "Privacy Policy";
            this.btnPrivacy.UseVisualStyleBackColor = true;
            this.btnPrivacy.Click += new System.EventHandler(this.btnPrivacy_Click);
            // 
            // p1
            // 
            this.p1.Controls.Add(this.btnPrivacy);
            this.p1.Controls.Add(this.label1);
            this.p1.Controls.Add(this.label3);
            this.p1.Controls.Add(this.btnSubmit);
            this.p1.Controls.Add(this.txtEmail);
            this.p1.Controls.Add(this.txtName);
            this.p1.Controls.Add(this.label2);
            this.p1.Location = new System.Drawing.Point(6, 9);
            this.p1.Name = "p1";
            this.p1.Size = new System.Drawing.Size(420, 148);
            this.p1.TabIndex = 16;
            // 
            // p2
            // 
            this.p2.Controls.Add(this.btnGoBack);
            this.p2.Controls.Add(this.pictureBox1);
            this.p2.Controls.Add(this.btnResend);
            this.p2.Controls.Add(this.txtCode);
            this.p2.Controls.Add(this.label4);
            this.p2.Controls.Add(this.btnVerifyCode);
            this.p2.Location = new System.Drawing.Point(6, 9);
            this.p2.Name = "p2";
            this.p2.Size = new System.Drawing.Size(420, 148);
            this.p2.TabIndex = 17;
            this.p2.Visible = false;
            // 
            // txtCode
            // 
            this.txtCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCode.Location = new System.Drawing.Point(111, 63);
            this.txtCode.Mask = "000000";
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(74, 26);
            this.txtCode.TabIndex = 1;
            this.txtCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.ForeColor = System.Drawing.Color.Green;
            this.label4.Location = new System.Drawing.Point(6, 5);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(404, 41);
            this.label4.TabIndex = 1;
            this.label4.Text = resources.GetString("label4.Text");
            // 
            // btnVerifyCode
            // 
            this.btnVerifyCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVerifyCode.Location = new System.Drawing.Point(191, 66);
            this.btnVerifyCode.Name = "btnVerifyCode";
            this.btnVerifyCode.Size = new System.Drawing.Size(75, 23);
            this.btnVerifyCode.TabIndex = 2;
            this.btnVerifyCode.Text = "Verify";
            this.btnVerifyCode.UseVisualStyleBackColor = true;
            this.btnVerifyCode.Click += new System.EventHandler(this.btnVerifyCode_Click);
            // 
            // btnResend
            // 
            this.btnResend.Location = new System.Drawing.Point(191, 95);
            this.btnResend.Name = "btnResend";
            this.btnResend.Size = new System.Drawing.Size(55, 23);
            this.btnResend.TabIndex = 3;
            this.btnResend.Text = "resend";
            this.btnResend.UseVisualStyleBackColor = true;
            this.btnResend.Click += new System.EventHandler(this.btnResend_Click);
            // 
            // btnGoBack
            // 
            this.btnGoBack.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnGoBack.Image = global::PinBot.Properties.Resources.back;
            this.btnGoBack.Location = new System.Drawing.Point(3, 122);
            this.btnGoBack.Name = "btnGoBack";
            this.btnGoBack.Size = new System.Drawing.Size(34, 23);
            this.btnGoBack.TabIndex = 9;
            this.btnGoBack.UseVisualStyleBackColor = true;
            this.btnGoBack.Click += new System.EventHandler(this.btnGoBack_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = global::PinBot.Properties.Resources.pinterest_icon;
            this.pictureBox1.Location = new System.Drawing.Point(337, 65);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(80, 80);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // promo2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(427, 159);
            this.Controls.Add(this.p1);
            this.Controls.Add(this.p2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "promo2";
            this.Text = "Trial registration";
            this.Load += new System.EventHandler(this.promo2_Load);
            this.p1.ResumeLayout(false);
            this.p1.PerformLayout();
            this.p2.ResumeLayout(false);
            this.p2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnPrivacy;
        private System.Windows.Forms.Panel p1;
        private System.Windows.Forms.Panel p2;
        private System.Windows.Forms.MaskedTextBox txtCode;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnVerifyCode;
        private System.Windows.Forms.Button btnResend;
        private System.Windows.Forms.Button btnGoBack;
    }
}