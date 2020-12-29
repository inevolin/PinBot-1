namespace PinBot
{
    partial class SettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.labelmin = new System.Windows.Forms.Label();
            this.labelmax = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtMinTimeout = new System.Windows.Forms.MaskedTextBox();
            this.txtMaxTimeout = new System.Windows.Forms.MaskedTextBox();
            this.txtMax = new System.Windows.Forms.MaskedTextBox();
            this.SuspendLayout();
            // 
            // labelmin
            // 
            this.labelmin.AutoSize = true;
            this.labelmin.Location = new System.Drawing.Point(46, 23);
            this.labelmin.Name = "labelmin";
            this.labelmin.Size = new System.Drawing.Size(67, 13);
            this.labelmin.TabIndex = 24;
            this.labelmin.Text = "Timeout min:";
            // 
            // labelmax
            // 
            this.labelmax.AutoSize = true;
            this.labelmax.Location = new System.Drawing.Point(43, 49);
            this.labelmax.Name = "labelmax";
            this.labelmax.Size = new System.Drawing.Size(70, 13);
            this.labelmax.TabIndex = 25;
            this.labelmax.Text = "Timeout max:";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(12, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 17);
            this.label3.TabIndex = 26;
            this.label3.Text = "Repins max:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtMinTimeout
            // 
            this.txtMinTimeout.Location = new System.Drawing.Point(119, 20);
            this.txtMinTimeout.Mask = "0000000";
            this.txtMinTimeout.Name = "txtMinTimeout";
            this.txtMinTimeout.PromptChar = ' ';
            this.txtMinTimeout.Size = new System.Drawing.Size(42, 20);
            this.txtMinTimeout.TabIndex = 1;
            this.txtMinTimeout.Text = "30";
            // 
            // txtMaxTimeout
            // 
            this.txtMaxTimeout.Location = new System.Drawing.Point(119, 46);
            this.txtMaxTimeout.Mask = "0000000";
            this.txtMaxTimeout.Name = "txtMaxTimeout";
            this.txtMaxTimeout.PromptChar = ' ';
            this.txtMaxTimeout.Size = new System.Drawing.Size(42, 20);
            this.txtMaxTimeout.TabIndex = 2;
            this.txtMaxTimeout.Text = "150";
            // 
            // txtMax
            // 
            this.txtMax.Location = new System.Drawing.Point(119, 72);
            this.txtMax.Mask = "0000000";
            this.txtMax.Name = "txtMax";
            this.txtMax.PromptChar = ' ';
            this.txtMax.Size = new System.Drawing.Size(42, 20);
            this.txtMax.TabIndex = 3;
            this.txtMax.Text = "50";
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(214, 113);
            this.Controls.Add(this.labelmin);
            this.Controls.Add(this.labelmax);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtMinTimeout);
            this.Controls.Add(this.txtMaxTimeout);
            this.Controls.Add(this.txtMax);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "SettingsForm";
            this.Text = "Settings";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Settings_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelmin;
        private System.Windows.Forms.Label labelmax;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.MaskedTextBox txtMinTimeout;
        private System.Windows.Forms.MaskedTextBox txtMaxTimeout;
        private System.Windows.Forms.MaskedTextBox txtMax;


    }
}