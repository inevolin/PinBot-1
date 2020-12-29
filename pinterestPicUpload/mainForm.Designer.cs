namespace PinBot
{
  partial class mainForm
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainForm));
      this.tabs = new System.Windows.Forms.TabControl();
      this.Main_ = new System.Windows.Forms.TabPage();
      this.pictureBox1 = new System.Windows.Forms.PictureBox();
      this.grpLogin = new System.Windows.Forms.GroupBox();
      this.pnlProxy = new System.Windows.Forms.Panel();
      this.label32 = new System.Windows.Forms.Label();
      this.txtProxyIP = new System.Windows.Forms.TextBox();
      this.btnProxyTest = new System.Windows.Forms.Button();
      this.label33 = new System.Windows.Forms.Label();
      this.txtProxyPassword = new System.Windows.Forms.TextBox();
      this.txtProxyPort = new System.Windows.Forms.TextBox();
      this.label35 = new System.Windows.Forms.Label();
      this.txtProxyUsername = new System.Windows.Forms.TextBox();
      this.label34 = new System.Windows.Forms.Label();
      this.btnSave = new System.Windows.Forms.Button();
      this.chkProxy = new System.Windows.Forms.CheckBox();
      this.label1 = new System.Windows.Forms.Label();
      this.btnLogin = new System.Windows.Forms.Button();
      this.label2 = new System.Windows.Forms.Label();
      this.txtPassword = new System.Windows.Forms.MaskedTextBox();
      this.txtEmail = new System.Windows.Forms.MaskedTextBox();
      this.grpMainLicense = new System.Windows.Forms.GroupBox();
      this.btnTrial = new System.Windows.Forms.Button();
      this.btnValidate = new System.Windows.Forms.Button();
      this.label29 = new System.Windows.Forms.Label();
      this.txtTransactionID = new System.Windows.Forms.TextBox();
      this.Scrape_ = new System.Windows.Forms.TabPage();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.btnImportImages = new System.Windows.Forms.Button();
      this.btnManageQueue = new System.Windows.Forms.Button();
      this.grpScrapeSettings = new System.Windows.Forms.GroupBox();
      this.numScrapeSourceURL = new System.Windows.Forms.NumericUpDown();
      this.numScrapeWebsiteInDesc = new System.Windows.Forms.NumericUpDown();
      this.label9 = new System.Windows.Forms.Label();
      this.label8 = new System.Windows.Forms.Label();
      this.label7 = new System.Windows.Forms.Label();
      this.txtScrapeSourceURL = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.clbSources = new System.Windows.Forms.CheckedListBox();
      this.label5 = new System.Windows.Forms.Label();
      this.txtScrapeURL = new System.Windows.Forms.TextBox();
      this.label4 = new System.Windows.Forms.Label();
      this.txtNumScrapes = new System.Windows.Forms.NumericUpDown();
      this.btnScapeBoardMapping = new System.Windows.Forms.Button();
      this.btnScrapeStart = new System.Windows.Forms.Button();
      this.pictureBox9 = new System.Windows.Forms.PictureBox();
      this.Pin_ = new System.Windows.Forms.TabPage();
      this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
      this.grpPinURL = new System.Windows.Forms.GroupBox();
      this.label10 = new System.Windows.Forms.Label();
      this.numPinWebsiteInDesc = new System.Windows.Forms.NumericUpDown();
      this.txtPinURL = new System.Windows.Forms.TextBox();
      this.grpSourceURL = new System.Windows.Forms.GroupBox();
      this.label6 = new System.Windows.Forms.Label();
      this.numPinSourceURL = new System.Windows.Forms.NumericUpDown();
      this.txtPinSourceURL = new System.Windows.Forms.TextBox();
      this.btnPinStart = new System.Windows.Forms.Button();
      this.grpPinSettings = new System.Windows.Forms.GroupBox();
      this.tpDelayTo = new System.Windows.Forms.DateTimePicker();
      this.tpDelayFrom = new System.Windows.Forms.DateTimePicker();
      this.chkContionuousRun = new System.Windows.Forms.CheckBox();
      this.chkScheduled = new System.Windows.Forms.CheckBox();
      this.chkPinWatermark = new System.Windows.Forms.CheckBox();
      this.btnPinSettings = new System.Windows.Forms.Button();
      this.btnPinBoardMapping = new System.Windows.Forms.Button();
      this.chkAutopilot = new System.Windows.Forms.RadioButton();
      this.chkManual = new System.Windows.Forms.RadioButton();
      this.dtpScheduled = new System.Windows.Forms.DateTimePicker();
      this.pictureBox2 = new System.Windows.Forms.PictureBox();
      this.Repin_ = new System.Windows.Forms.TabPage();
      this.pictureBox7 = new System.Windows.Forms.PictureBox();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.btnRepinEditQueue = new System.Windows.Forms.Button();
      this.btnStartRepin = new System.Windows.Forms.Button();
      this.grpRepinSettings = new System.Windows.Forms.GroupBox();
      this.chkRepinScheduled = new System.Windows.Forms.CheckBox();
      this.tpRepinDelayTo = new System.Windows.Forms.DateTimePicker();
      this.tpRepinDelayFrom = new System.Windows.Forms.DateTimePicker();
      this.chkRepinContinousRun = new System.Windows.Forms.CheckBox();
      this.dtpRepinScheduled = new System.Windows.Forms.DateTimePicker();
      this.rdbRepinManual = new System.Windows.Forms.RadioButton();
      this.rdbRepinAutopilot = new System.Windows.Forms.RadioButton();
      this.btnRepinSettings = new System.Windows.Forms.Button();
      this.btnStartRepinScrape = new System.Windows.Forms.Button();
      this.grpRepinScrapeSettings = new System.Windows.Forms.GroupBox();
      this.label11 = new System.Windows.Forms.Label();
      this.numRepinScrapes = new System.Windows.Forms.NumericUpDown();
      this.btnRepinEditUserMapping = new System.Windows.Forms.Button();
      this.btnRepinBoardMapping = new System.Windows.Forms.Button();
      this.Like_ = new System.Windows.Forms.TabPage();
      this.pictureBox6 = new System.Windows.Forms.PictureBox();
      this.grpLikeSettings = new System.Windows.Forms.GroupBox();
      this.chkLikeScheduled = new System.Windows.Forms.CheckBox();
      this.tpLikeDelayTo = new System.Windows.Forms.DateTimePicker();
      this.tpLikeDelayFrom = new System.Windows.Forms.DateTimePicker();
      this.chkLikeContinuousRun = new System.Windows.Forms.CheckBox();
      this.dtpLikeScheduled = new System.Windows.Forms.DateTimePicker();
      this.btnLikeEditCats = new System.Windows.Forms.Button();
      this.btnLikeEditUsers = new System.Windows.Forms.Button();
      this.rdbLikeManual = new System.Windows.Forms.RadioButton();
      this.btnLikeSettings = new System.Windows.Forms.Button();
      this.btnLikeStart = new System.Windows.Forms.Button();
      this.Invite_ = new System.Windows.Forms.TabPage();
      this.pictureBox5 = new System.Windows.Forms.PictureBox();
      this.btnInviteStart = new System.Windows.Forms.Button();
      this.grpInviteSettings = new System.Windows.Forms.GroupBox();
      this.chkInviteScheduled = new System.Windows.Forms.CheckBox();
      this.tpInviteDelayTo = new System.Windows.Forms.DateTimePicker();
      this.tpInviteDelayFrom = new System.Windows.Forms.DateTimePicker();
      this.chkInviteContinuousRun = new System.Windows.Forms.CheckBox();
      this.dtpInviteScheduled = new System.Windows.Forms.DateTimePicker();
      this.rdbInviteManual = new System.Windows.Forms.RadioButton();
      this.btnInviteSettings = new System.Windows.Forms.Button();
      this.label49 = new System.Windows.Forms.Label();
      this.lstInviteBoards = new System.Windows.Forms.ListBox();
      this.Follow_ = new System.Windows.Forms.TabPage();
      this.pictureBox3 = new System.Windows.Forms.PictureBox();
      this.grpFollowAlgorithm = new System.Windows.Forms.GroupBox();
      this.chkFollow_hasTW = new System.Windows.Forms.CheckBox();
      this.chkFollow_hasFB = new System.Windows.Forms.CheckBox();
      this.txtFollow_PinsMax = new System.Windows.Forms.MaskedTextBox();
      this.label18 = new System.Windows.Forms.Label();
      this.txtFollow_PinsMin = new System.Windows.Forms.MaskedTextBox();
      this.label19 = new System.Windows.Forms.Label();
      this.chkFollow_hasWebsite = new System.Windows.Forms.CheckBox();
      this.chkFollow_isPartner = new System.Windows.Forms.CheckBox();
      this.txtFollow_BoardsMax = new System.Windows.Forms.MaskedTextBox();
      this.label16 = new System.Windows.Forms.Label();
      this.txtFollow_BoardsMin = new System.Windows.Forms.MaskedTextBox();
      this.label17 = new System.Windows.Forms.Label();
      this.txtFollow_FollowingMax = new System.Windows.Forms.MaskedTextBox();
      this.label14 = new System.Windows.Forms.Label();
      this.txtFollow_FollowingMin = new System.Windows.Forms.MaskedTextBox();
      this.label15 = new System.Windows.Forms.Label();
      this.txtFollow_FollowersMax = new System.Windows.Forms.MaskedTextBox();
      this.label13 = new System.Windows.Forms.Label();
      this.txtFollow_FollowersMin = new System.Windows.Forms.MaskedTextBox();
      this.label12 = new System.Windows.Forms.Label();
      this.grpFollowSettings = new System.Windows.Forms.GroupBox();
      this.chkFollowScheduled = new System.Windows.Forms.CheckBox();
      this.tpFollowDelayTo = new System.Windows.Forms.DateTimePicker();
      this.tpFollowDelayFrom = new System.Windows.Forms.DateTimePicker();
      this.chkFollowContinuousRun = new System.Windows.Forms.CheckBox();
      this.dtpFollowScheduled = new System.Windows.Forms.DateTimePicker();
      this.rdbFollowManual = new System.Windows.Forms.RadioButton();
      this.chkFollowIgnoreCriteria = new System.Windows.Forms.CheckBox();
      this.btnFollowEditCats = new System.Windows.Forms.Button();
      this.btnFollowEditUsers = new System.Windows.Forms.Button();
      this.btnFollowSettings = new System.Windows.Forms.Button();
      this.btnFollowStart = new System.Windows.Forms.Button();
      this.Unfollow_ = new System.Windows.Forms.TabPage();
      this.pictureBox4 = new System.Windows.Forms.PictureBox();
      this.grpUnfollowAlgorithm = new System.Windows.Forms.GroupBox();
      this.chkUnfollowNonFollower = new System.Windows.Forms.CheckBox();
      this.txtUnfollow_PinsMax = new System.Windows.Forms.MaskedTextBox();
      this.label24 = new System.Windows.Forms.Label();
      this.txtUnfollow_PinsMin = new System.Windows.Forms.MaskedTextBox();
      this.label25 = new System.Windows.Forms.Label();
      this.txtUnfollow_BoardsMax = new System.Windows.Forms.MaskedTextBox();
      this.label22 = new System.Windows.Forms.Label();
      this.txtUnfollow_BoardsMin = new System.Windows.Forms.MaskedTextBox();
      this.label23 = new System.Windows.Forms.Label();
      this.txtUnfollow_FollowingMax = new System.Windows.Forms.MaskedTextBox();
      this.label20 = new System.Windows.Forms.Label();
      this.txtUnfollow_FollowingMin = new System.Windows.Forms.MaskedTextBox();
      this.label21 = new System.Windows.Forms.Label();
      this.chkUnfollow_hasWebsite = new System.Windows.Forms.CheckBox();
      this.chkUnfollow_isPartner = new System.Windows.Forms.CheckBox();
      this.txtUnfollow_FollowersMax = new System.Windows.Forms.MaskedTextBox();
      this.label26 = new System.Windows.Forms.Label();
      this.txtUnfollow_FollowersMin = new System.Windows.Forms.MaskedTextBox();
      this.label27 = new System.Windows.Forms.Label();
      this.grpUnfollowSettings = new System.Windows.Forms.GroupBox();
      this.chkUnfollowScheduled = new System.Windows.Forms.CheckBox();
      this.tpUnfollowDelayTo = new System.Windows.Forms.DateTimePicker();
      this.tpUnfollowDelayFrom = new System.Windows.Forms.DateTimePicker();
      this.chkUnfollowContinuousRun = new System.Windows.Forms.CheckBox();
      this.dtpUnfollowScheduled = new System.Windows.Forms.DateTimePicker();
      this.rdbUnfollowManual = new System.Windows.Forms.RadioButton();
      this.btnUnfollowSettings = new System.Windows.Forms.Button();
      this.btnUnfollowStart = new System.Windows.Forms.Button();
      this.Account_ = new System.Windows.Forms.TabPage();
      this.stats = new System.Windows.Forms.RichTextBox();
      this.btnAccountRefreshStats = new System.Windows.Forms.Button();
      this.pictureBox8 = new System.Windows.Forms.PictureBox();
      this.btnAccountReloadBoards = new System.Windows.Forms.Button();
      this.lblStatus1 = new System.Windows.Forms.ToolStripStatusLabel();
      this.statusStrip1 = new System.Windows.Forms.StatusStrip();
      this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripSplitButton();
      this.visitBlogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.visitWebsiteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.fAQToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.whatsNewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.clickTheBlueQuestionmarkToOpenTheManualToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.tslNotification = new System.Windows.Forms.ToolStripStatusLabel();
      this.lblUpdate = new System.Windows.Forms.ToolStripStatusLabel();
      this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
      this.tmrStatus = new System.Windows.Forms.Timer(this.components);
      this.tmrUpdate = new System.Windows.Forms.Timer(this.components);
      this.tabs.SuspendLayout();
      this.Main_.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
      this.grpLogin.SuspendLayout();
      this.pnlProxy.SuspendLayout();
      this.grpMainLicense.SuspendLayout();
      this.Scrape_.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.grpScrapeSettings.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.numScrapeSourceURL)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.numScrapeWebsiteInDesc)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.txtNumScrapes)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).BeginInit();
      this.Pin_.SuspendLayout();
      this.flowLayoutPanel2.SuspendLayout();
      this.grpPinURL.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.numPinWebsiteInDesc)).BeginInit();
      this.grpSourceURL.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.numPinSourceURL)).BeginInit();
      this.grpPinSettings.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
      this.Repin_.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).BeginInit();
      this.groupBox2.SuspendLayout();
      this.grpRepinSettings.SuspendLayout();
      this.grpRepinScrapeSettings.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.numRepinScrapes)).BeginInit();
      this.Like_.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
      this.grpLikeSettings.SuspendLayout();
      this.Invite_.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
      this.grpInviteSettings.SuspendLayout();
      this.Follow_.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
      this.grpFollowAlgorithm.SuspendLayout();
      this.grpFollowSettings.SuspendLayout();
      this.Unfollow_.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
      this.grpUnfollowAlgorithm.SuspendLayout();
      this.grpUnfollowSettings.SuspendLayout();
      this.Account_.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).BeginInit();
      this.statusStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // tabs
      // 
      this.tabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.tabs.Controls.Add(this.Main_);
      this.tabs.Controls.Add(this.Scrape_);
      this.tabs.Controls.Add(this.Pin_);
      this.tabs.Controls.Add(this.Repin_);
      this.tabs.Controls.Add(this.Like_);
      this.tabs.Controls.Add(this.Invite_);
      this.tabs.Controls.Add(this.Follow_);
      this.tabs.Controls.Add(this.Unfollow_);
      this.tabs.Controls.Add(this.Account_);
      this.tabs.HotTrack = true;
      this.tabs.ImeMode = System.Windows.Forms.ImeMode.NoControl;
      this.tabs.ItemSize = new System.Drawing.Size(70, 18);
      this.tabs.Location = new System.Drawing.Point(0, 5);
      this.tabs.Name = "tabs";
      this.tabs.SelectedIndex = 0;
      this.tabs.Size = new System.Drawing.Size(788, 234);
      this.tabs.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
      this.tabs.TabIndex = 100;
      // 
      // Main_
      // 
      this.Main_.BackColor = System.Drawing.Color.White;
      this.Main_.Controls.Add(this.pictureBox1);
      this.Main_.Controls.Add(this.grpLogin);
      this.Main_.Controls.Add(this.grpMainLicense);
      this.Main_.Location = new System.Drawing.Point(4, 22);
      this.Main_.Name = "Main_";
      this.Main_.Padding = new System.Windows.Forms.Padding(3);
      this.Main_.Size = new System.Drawing.Size(780, 208);
      this.Main_.TabIndex = 0;
      this.Main_.Text = "Main";
      // 
      // pictureBox1
      // 
      this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.pictureBox1.Image = global::PinBot.Properties.Resources.pinterest_icon;
      this.pictureBox1.Location = new System.Drawing.Point(647, 0);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new System.Drawing.Size(133, 208);
      this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
      this.pictureBox1.TabIndex = 8;
      this.pictureBox1.TabStop = false;
      // 
      // grpLogin
      // 
      this.grpLogin.Anchor = System.Windows.Forms.AnchorStyles.None;
      this.grpLogin.AutoSize = true;
      this.grpLogin.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
      this.grpLogin.Controls.Add(this.pnlProxy);
      this.grpLogin.Controls.Add(this.btnSave);
      this.grpLogin.Controls.Add(this.chkProxy);
      this.grpLogin.Controls.Add(this.label1);
      this.grpLogin.Controls.Add(this.btnLogin);
      this.grpLogin.Controls.Add(this.label2);
      this.grpLogin.Controls.Add(this.txtPassword);
      this.grpLogin.Controls.Add(this.txtEmail);
      this.grpLogin.Location = new System.Drawing.Point(8, 6);
      this.grpLogin.Name = "grpLogin";
      this.grpLogin.Size = new System.Drawing.Size(514, 135);
      this.grpLogin.TabIndex = 6;
      this.grpLogin.TabStop = false;
      this.grpLogin.Text = "Pinterest account login";
      this.grpLogin.Visible = false;
      // 
      // pnlProxy
      // 
      this.pnlProxy.Controls.Add(this.label32);
      this.pnlProxy.Controls.Add(this.txtProxyIP);
      this.pnlProxy.Controls.Add(this.btnProxyTest);
      this.pnlProxy.Controls.Add(this.label33);
      this.pnlProxy.Controls.Add(this.txtProxyPassword);
      this.pnlProxy.Controls.Add(this.txtProxyPort);
      this.pnlProxy.Controls.Add(this.label35);
      this.pnlProxy.Controls.Add(this.txtProxyUsername);
      this.pnlProxy.Controls.Add(this.label34);
      this.pnlProxy.Enabled = false;
      this.pnlProxy.Location = new System.Drawing.Point(270, 19);
      this.pnlProxy.Name = "pnlProxy";
      this.pnlProxy.Size = new System.Drawing.Size(238, 87);
      this.pnlProxy.TabIndex = 16;
      this.pnlProxy.Visible = false;
      // 
      // label32
      // 
      this.label32.AutoSize = true;
      this.label32.Location = new System.Drawing.Point(24, 8);
      this.label32.Name = "label32";
      this.label32.Size = new System.Drawing.Size(38, 13);
      this.label32.TabIndex = 6;
      this.label32.Text = "http://";
      // 
      // txtProxyIP
      // 
      this.txtProxyIP.Enabled = false;
      this.txtProxyIP.Location = new System.Drawing.Point(68, 5);
      this.txtProxyIP.Name = "txtProxyIP";
      this.txtProxyIP.Size = new System.Drawing.Size(100, 20);
      this.txtProxyIP.TabIndex = 4;
      this.toolTip1.SetToolTip(this.txtProxyIP, "Proxy IP");
      // 
      // btnProxyTest
      // 
      this.btnProxyTest.Enabled = false;
      this.btnProxyTest.Location = new System.Drawing.Point(182, 61);
      this.btnProxyTest.Name = "btnProxyTest";
      this.btnProxyTest.Size = new System.Drawing.Size(51, 23);
      this.btnProxyTest.TabIndex = 8;
      this.btnProxyTest.Text = "Test";
      this.btnProxyTest.UseVisualStyleBackColor = true;
      this.btnProxyTest.Click += new System.EventHandler(this.btnProxyTest_Click);
      // 
      // label33
      // 
      this.label33.AutoSize = true;
      this.label33.Location = new System.Drawing.Point(170, 8);
      this.label33.Name = "label33";
      this.label33.Size = new System.Drawing.Size(10, 13);
      this.label33.TabIndex = 8;
      this.label33.Text = ":";
      // 
      // txtProxyPassword
      // 
      this.txtProxyPassword.Enabled = false;
      this.txtProxyPassword.Location = new System.Drawing.Point(68, 63);
      this.txtProxyPassword.Name = "txtProxyPassword";
      this.txtProxyPassword.Size = new System.Drawing.Size(100, 20);
      this.txtProxyPassword.TabIndex = 7;
      this.toolTip1.SetToolTip(this.txtProxyPassword, "If authentication is required:\r\nproxy password.\r\nOtherwise leave empty.\r\n");
      // 
      // txtProxyPort
      // 
      this.txtProxyPort.Enabled = false;
      this.txtProxyPort.Location = new System.Drawing.Point(182, 5);
      this.txtProxyPort.Name = "txtProxyPort";
      this.txtProxyPort.Size = new System.Drawing.Size(49, 20);
      this.txtProxyPort.TabIndex = 5;
      this.toolTip1.SetToolTip(this.txtProxyPort, "Proxy Port");
      // 
      // label35
      // 
      this.label35.AutoSize = true;
      this.label35.Location = new System.Drawing.Point(6, 66);
      this.label35.Name = "label35";
      this.label35.Size = new System.Drawing.Size(56, 13);
      this.label35.TabIndex = 12;
      this.label35.Text = "Password:";
      // 
      // txtProxyUsername
      // 
      this.txtProxyUsername.Enabled = false;
      this.txtProxyUsername.Location = new System.Drawing.Point(68, 37);
      this.txtProxyUsername.Name = "txtProxyUsername";
      this.txtProxyUsername.Size = new System.Drawing.Size(100, 20);
      this.txtProxyUsername.TabIndex = 6;
      this.toolTip1.SetToolTip(this.txtProxyUsername, "If authentication is required:\r\nproxy username.\r\nOtherwise leave empty.");
      // 
      // label34
      // 
      this.label34.AutoSize = true;
      this.label34.Location = new System.Drawing.Point(6, 40);
      this.label34.Name = "label34";
      this.label34.Size = new System.Drawing.Size(58, 13);
      this.label34.TabIndex = 11;
      this.label34.Text = "Username:";
      // 
      // btnSave
      // 
      this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
      this.btnSave.FlatAppearance.BorderSize = 0;
      this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnSave.Image = global::PinBot.Properties.Resources.Save_icon;
      this.btnSave.Location = new System.Drawing.Point(2, 94);
      this.btnSave.Name = "btnSave";
      this.btnSave.Size = new System.Drawing.Size(24, 22);
      this.btnSave.TabIndex = 15;
      this.toolTip1.SetToolTip(this.btnSave, "Save login (and proxy credentials)");
      this.btnSave.UseVisualStyleBackColor = true;
      this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
      // 
      // chkProxy
      // 
      this.chkProxy.AutoSize = true;
      this.chkProxy.Location = new System.Drawing.Point(150, 80);
      this.chkProxy.Name = "chkProxy";
      this.chkProxy.Size = new System.Drawing.Size(52, 17);
      this.chkProxy.TabIndex = 3;
      this.chkProxy.Text = "Proxy";
      this.toolTip1.SetToolTip(this.chkProxy, "Premium only.");
      this.chkProxy.UseVisualStyleBackColor = true;
      this.chkProxy.CheckedChanged += new System.EventHandler(this.chkProxy_CheckedChanged);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(25, 27);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(35, 13);
      this.label1.TabIndex = 3;
      this.label1.Text = "Email:";
      // 
      // btnLogin
      // 
      this.btnLogin.Location = new System.Drawing.Point(69, 76);
      this.btnLogin.Name = "btnLogin";
      this.btnLogin.Size = new System.Drawing.Size(75, 23);
      this.btnLogin.TabIndex = 2;
      this.btnLogin.Text = "Log in";
      this.btnLogin.UseVisualStyleBackColor = true;
      this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(4, 53);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(56, 13);
      this.label2.TabIndex = 4;
      this.label2.Text = "Password:";
      // 
      // txtPassword
      // 
      this.txtPassword.AsciiOnly = true;
      this.txtPassword.Culture = new System.Globalization.CultureInfo("en-US");
      this.txtPassword.Location = new System.Drawing.Point(69, 50);
      this.txtPassword.Name = "txtPassword";
      this.txtPassword.PasswordChar = '*';
      this.txtPassword.Size = new System.Drawing.Size(181, 20);
      this.txtPassword.TabIndex = 1;
      this.txtPassword.UseSystemPasswordChar = true;
      // 
      // txtEmail
      // 
      this.txtEmail.AsciiOnly = true;
      this.txtEmail.Culture = new System.Globalization.CultureInfo("en-US");
      this.txtEmail.Location = new System.Drawing.Point(69, 24);
      this.txtEmail.Name = "txtEmail";
      this.txtEmail.Size = new System.Drawing.Size(181, 20);
      this.txtEmail.TabIndex = 0;
      // 
      // grpMainLicense
      // 
      this.grpMainLicense.Anchor = System.Windows.Forms.AnchorStyles.None;
      this.grpMainLicense.Controls.Add(this.btnTrial);
      this.grpMainLicense.Controls.Add(this.btnValidate);
      this.grpMainLicense.Controls.Add(this.label29);
      this.grpMainLicense.Controls.Add(this.txtTransactionID);
      this.grpMainLicense.Location = new System.Drawing.Point(8, 6);
      this.grpMainLicense.Name = "grpMainLicense";
      this.grpMainLicense.Size = new System.Drawing.Size(265, 119);
      this.grpMainLicense.TabIndex = 7;
      this.grpMainLicense.TabStop = false;
      this.grpMainLicense.Text = "License";
      // 
      // btnTrial
      // 
      this.btnTrial.Location = new System.Drawing.Point(173, 61);
      this.btnTrial.Name = "btnTrial";
      this.btnTrial.Size = new System.Drawing.Size(75, 23);
      this.btnTrial.TabIndex = 3;
      this.btnTrial.Text = "Trial";
      this.btnTrial.UseVisualStyleBackColor = true;
      this.btnTrial.Click += new System.EventHandler(this.btnTrial_Click);
      // 
      // btnValidate
      // 
      this.btnValidate.Location = new System.Drawing.Point(92, 61);
      this.btnValidate.Name = "btnValidate";
      this.btnValidate.Size = new System.Drawing.Size(75, 23);
      this.btnValidate.TabIndex = 2;
      this.btnValidate.Text = "Validate";
      this.btnValidate.UseVisualStyleBackColor = true;
      this.btnValidate.Click += new System.EventHandler(this.btnValidate_Click);
      // 
      // label29
      // 
      this.label29.AutoSize = true;
      this.label29.Location = new System.Drawing.Point(15, 38);
      this.label29.Name = "label29";
      this.label29.Size = new System.Drawing.Size(61, 13);
      this.label29.TabIndex = 1;
      this.label29.Text = "License ID:";
      // 
      // txtTransactionID
      // 
      this.txtTransactionID.Location = new System.Drawing.Point(92, 35);
      this.txtTransactionID.Name = "txtTransactionID";
      this.txtTransactionID.Size = new System.Drawing.Size(158, 20);
      this.txtTransactionID.TabIndex = 0;
      // 
      // Scrape_
      // 
      this.Scrape_.Controls.Add(this.groupBox1);
      this.Scrape_.Controls.Add(this.grpScrapeSettings);
      this.Scrape_.Controls.Add(this.btnScrapeStart);
      this.Scrape_.Controls.Add(this.pictureBox9);
      this.Scrape_.Location = new System.Drawing.Point(4, 22);
      this.Scrape_.Name = "Scrape_";
      this.Scrape_.Size = new System.Drawing.Size(780, 208);
      this.Scrape_.TabIndex = 4;
      this.Scrape_.Text = "Scrape";
      this.Scrape_.UseVisualStyleBackColor = true;
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.btnImportImages);
      this.groupBox1.Controls.Add(this.btnManageQueue);
      this.groupBox1.Location = new System.Drawing.Point(554, 5);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(214, 60);
      this.groupBox1.TabIndex = 24;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Queue";
      // 
      // btnImportImages
      // 
      this.btnImportImages.Location = new System.Drawing.Point(6, 23);
      this.btnImportImages.Name = "btnImportImages";
      this.btnImportImages.Size = new System.Drawing.Size(101, 23);
      this.btnImportImages.TabIndex = 4;
      this.btnImportImages.Text = "Import images";
      this.btnImportImages.UseVisualStyleBackColor = true;
      this.btnImportImages.Click += new System.EventHandler(this.btnImportImages_Click);
      // 
      // btnManageQueue
      // 
      this.btnManageQueue.Location = new System.Drawing.Point(113, 23);
      this.btnManageQueue.Name = "btnManageQueue";
      this.btnManageQueue.Size = new System.Drawing.Size(95, 23);
      this.btnManageQueue.TabIndex = 5;
      this.btnManageQueue.Text = "Edit queue";
      this.btnManageQueue.UseVisualStyleBackColor = true;
      this.btnManageQueue.Click += new System.EventHandler(this.btnManageQueue_Click);
      // 
      // grpScrapeSettings
      // 
      this.grpScrapeSettings.Controls.Add(this.numScrapeSourceURL);
      this.grpScrapeSettings.Controls.Add(this.numScrapeWebsiteInDesc);
      this.grpScrapeSettings.Controls.Add(this.label9);
      this.grpScrapeSettings.Controls.Add(this.label8);
      this.grpScrapeSettings.Controls.Add(this.label7);
      this.grpScrapeSettings.Controls.Add(this.txtScrapeSourceURL);
      this.grpScrapeSettings.Controls.Add(this.label3);
      this.grpScrapeSettings.Controls.Add(this.clbSources);
      this.grpScrapeSettings.Controls.Add(this.label5);
      this.grpScrapeSettings.Controls.Add(this.txtScrapeURL);
      this.grpScrapeSettings.Controls.Add(this.label4);
      this.grpScrapeSettings.Controls.Add(this.txtNumScrapes);
      this.grpScrapeSettings.Controls.Add(this.btnScapeBoardMapping);
      this.grpScrapeSettings.Location = new System.Drawing.Point(8, 5);
      this.grpScrapeSettings.Name = "grpScrapeSettings";
      this.grpScrapeSettings.Size = new System.Drawing.Size(540, 167);
      this.grpScrapeSettings.TabIndex = 23;
      this.grpScrapeSettings.TabStop = false;
      this.grpScrapeSettings.Text = "Settings";
      // 
      // numScrapeSourceURL
      // 
      this.numScrapeSourceURL.Location = new System.Drawing.Point(280, 128);
      this.numScrapeSourceURL.Name = "numScrapeSourceURL";
      this.numScrapeSourceURL.Size = new System.Drawing.Size(52, 20);
      this.numScrapeSourceURL.TabIndex = 50;
      this.numScrapeSourceURL.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
      this.numScrapeSourceURL.ValueChanged += new System.EventHandler(this.numScrapeSourceURL_ValueChanged);
      // 
      // numScrapeWebsiteInDesc
      // 
      this.numScrapeWebsiteInDesc.Location = new System.Drawing.Point(280, 86);
      this.numScrapeWebsiteInDesc.Name = "numScrapeWebsiteInDesc";
      this.numScrapeWebsiteInDesc.Size = new System.Drawing.Size(52, 20);
      this.numScrapeWebsiteInDesc.TabIndex = 49;
      this.numScrapeWebsiteInDesc.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
      this.numScrapeWebsiteInDesc.ValueChanged += new System.EventHandler(this.numScrapeWebsiteInDesc_ValueChanged);
      // 
      // label9
      // 
      this.label9.AutoSize = true;
      this.label9.Location = new System.Drawing.Point(236, 131);
      this.label9.Name = "label9";
      this.label9.Size = new System.Drawing.Size(38, 13);
      this.label9.TabIndex = 47;
      this.label9.Text = "%Pins:";
      // 
      // label8
      // 
      this.label8.AutoSize = true;
      this.label8.Location = new System.Drawing.Point(236, 88);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(38, 13);
      this.label8.TabIndex = 45;
      this.label8.Text = "%Pins:";
      // 
      // label7
      // 
      this.label7.AutoSize = true;
      this.label7.Location = new System.Drawing.Point(6, 112);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(69, 13);
      this.label7.TabIndex = 43;
      this.label7.Text = "Source URL:";
      // 
      // txtScrapeSourceURL
      // 
      this.txtScrapeSourceURL.Location = new System.Drawing.Point(9, 128);
      this.txtScrapeSourceURL.Name = "txtScrapeSourceURL";
      this.txtScrapeSourceURL.Size = new System.Drawing.Size(221, 20);
      this.txtScrapeSourceURL.TabIndex = 42;
      this.txtScrapeSourceURL.TextChanged += new System.EventHandler(this.txtScrapeSourceURL_TextChanged);
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(369, 17);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(49, 13);
      this.label3.TabIndex = 41;
      this.label3.Text = "Sources:";
      // 
      // clbSources
      // 
      this.clbSources.CheckOnClick = true;
      this.clbSources.FormattingEnabled = true;
      this.clbSources.Items.AddRange(new object[] {
            "WeHeartIt",
            "Imgfave"});
      this.clbSources.Location = new System.Drawing.Point(372, 33);
      this.clbSources.Name = "clbSources";
      this.clbSources.Size = new System.Drawing.Size(160, 34);
      this.clbSources.TabIndex = 2;
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(6, 69);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(114, 13);
      this.label5.TabIndex = 38;
      this.label5.Text = "Website in description:";
      // 
      // txtScrapeURL
      // 
      this.txtScrapeURL.Location = new System.Drawing.Point(9, 85);
      this.txtScrapeURL.Name = "txtScrapeURL";
      this.txtScrapeURL.Size = new System.Drawing.Size(221, 20);
      this.txtScrapeURL.TabIndex = 1;
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(369, 91);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(102, 13);
      this.label4.TabIndex = 36;
      this.label4.Text = "Scrapes per source:";
      // 
      // txtNumScrapes
      // 
      this.txtNumScrapes.Location = new System.Drawing.Point(475, 89);
      this.txtNumScrapes.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
      this.txtNumScrapes.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.txtNumScrapes.Name = "txtNumScrapes";
      this.txtNumScrapes.Size = new System.Drawing.Size(57, 20);
      this.txtNumScrapes.TabIndex = 3;
      this.txtNumScrapes.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
      // 
      // btnScapeBoardMapping
      // 
      this.btnScapeBoardMapping.Location = new System.Drawing.Point(9, 29);
      this.btnScapeBoardMapping.Name = "btnScapeBoardMapping";
      this.btnScapeBoardMapping.Size = new System.Drawing.Size(133, 23);
      this.btnScapeBoardMapping.TabIndex = 0;
      this.btnScapeBoardMapping.Text = "Edit board mapping...";
      this.btnScapeBoardMapping.UseVisualStyleBackColor = true;
      this.btnScapeBoardMapping.Click += new System.EventHandler(this.btnScapeBoardMapping_Click);
      // 
      // btnScrapeStart
      // 
      this.btnScrapeStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.btnScrapeStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
      this.btnScrapeStart.Location = new System.Drawing.Point(8, 177);
      this.btnScrapeStart.Name = "btnScrapeStart";
      this.btnScrapeStart.Size = new System.Drawing.Size(52, 23);
      this.btnScrapeStart.TabIndex = 6;
      this.btnScrapeStart.Text = "start";
      this.btnScrapeStart.UseVisualStyleBackColor = true;
      this.btnScrapeStart.Click += new System.EventHandler(this.btnScrapeStart_Click);
      // 
      // pictureBox9
      // 
      this.pictureBox9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.pictureBox9.Image = global::PinBot.Properties.Resources.pinterest_icon;
      this.pictureBox9.Location = new System.Drawing.Point(647, 0);
      this.pictureBox9.Name = "pictureBox9";
      this.pictureBox9.Size = new System.Drawing.Size(133, 208);
      this.pictureBox9.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
      this.pictureBox9.TabIndex = 25;
      this.pictureBox9.TabStop = false;
      // 
      // Pin_
      // 
      this.Pin_.Controls.Add(this.flowLayoutPanel2);
      this.Pin_.Controls.Add(this.btnPinStart);
      this.Pin_.Controls.Add(this.grpPinSettings);
      this.Pin_.Controls.Add(this.pictureBox2);
      this.Pin_.Location = new System.Drawing.Point(4, 22);
      this.Pin_.Name = "Pin_";
      this.Pin_.Padding = new System.Windows.Forms.Padding(3);
      this.Pin_.Size = new System.Drawing.Size(780, 208);
      this.Pin_.TabIndex = 1;
      this.Pin_.Text = "Pin";
      this.Pin_.UseVisualStyleBackColor = true;
      // 
      // flowLayoutPanel2
      // 
      this.flowLayoutPanel2.Controls.Add(this.grpPinURL);
      this.flowLayoutPanel2.Controls.Add(this.grpSourceURL);
      this.flowLayoutPanel2.Location = new System.Drawing.Point(264, 2);
      this.flowLayoutPanel2.Name = "flowLayoutPanel2";
      this.flowLayoutPanel2.Size = new System.Drawing.Size(370, 194);
      this.flowLayoutPanel2.TabIndex = 22;
      // 
      // grpPinURL
      // 
      this.grpPinURL.Controls.Add(this.label10);
      this.grpPinURL.Controls.Add(this.numPinWebsiteInDesc);
      this.grpPinURL.Controls.Add(this.txtPinURL);
      this.grpPinURL.Location = new System.Drawing.Point(3, 3);
      this.grpPinURL.Name = "grpPinURL";
      this.grpPinURL.Size = new System.Drawing.Size(361, 49);
      this.grpPinURL.TabIndex = 21;
      this.grpPinURL.TabStop = false;
      this.grpPinURL.Text = "Website in description";
      // 
      // label10
      // 
      this.label10.AutoSize = true;
      this.label10.Location = new System.Drawing.Point(259, 23);
      this.label10.Name = "label10";
      this.label10.Size = new System.Drawing.Size(38, 13);
      this.label10.TabIndex = 10;
      this.label10.Text = "%Pins:";
      // 
      // numPinWebsiteInDesc
      // 
      this.numPinWebsiteInDesc.Location = new System.Drawing.Point(303, 20);
      this.numPinWebsiteInDesc.Name = "numPinWebsiteInDesc";
      this.numPinWebsiteInDesc.Size = new System.Drawing.Size(52, 20);
      this.numPinWebsiteInDesc.TabIndex = 9;
      this.numPinWebsiteInDesc.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
      this.numPinWebsiteInDesc.ValueChanged += new System.EventHandler(this.numPinWebsiteInDesc_ValueChanged);
      // 
      // txtPinURL
      // 
      this.txtPinURL.Location = new System.Drawing.Point(6, 19);
      this.txtPinURL.Name = "txtPinURL";
      this.txtPinURL.Size = new System.Drawing.Size(237, 20);
      this.txtPinURL.TabIndex = 6;
      this.txtPinURL.TextChanged += new System.EventHandler(this.txtPinDescURL_TextChanged);
      // 
      // grpSourceURL
      // 
      this.grpSourceURL.Controls.Add(this.label6);
      this.grpSourceURL.Controls.Add(this.numPinSourceURL);
      this.grpSourceURL.Controls.Add(this.txtPinSourceURL);
      this.grpSourceURL.Location = new System.Drawing.Point(3, 58);
      this.grpSourceURL.Name = "grpSourceURL";
      this.grpSourceURL.Size = new System.Drawing.Size(361, 49);
      this.grpSourceURL.TabIndex = 22;
      this.grpSourceURL.TabStop = false;
      this.grpSourceURL.Text = "Source URL";
      this.grpSourceURL.Visible = false;
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(259, 22);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(38, 13);
      this.label6.TabIndex = 8;
      this.label6.Text = "%Pins:";
      // 
      // numPinSourceURL
      // 
      this.numPinSourceURL.Location = new System.Drawing.Point(303, 19);
      this.numPinSourceURL.Name = "numPinSourceURL";
      this.numPinSourceURL.Size = new System.Drawing.Size(52, 20);
      this.numPinSourceURL.TabIndex = 7;
      this.numPinSourceURL.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
      this.numPinSourceURL.ValueChanged += new System.EventHandler(this.numPinSourceURL_ValueChanged);
      // 
      // txtPinSourceURL
      // 
      this.txtPinSourceURL.Location = new System.Drawing.Point(6, 19);
      this.txtPinSourceURL.Name = "txtPinSourceURL";
      this.txtPinSourceURL.Size = new System.Drawing.Size(237, 20);
      this.txtPinSourceURL.TabIndex = 6;
      this.txtPinSourceURL.TextChanged += new System.EventHandler(this.txtPinSourceURL_TextChanged);
      // 
      // btnPinStart
      // 
      this.btnPinStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btnPinStart.Location = new System.Drawing.Point(8, 177);
      this.btnPinStart.Name = "btnPinStart";
      this.btnPinStart.Size = new System.Drawing.Size(52, 23);
      this.btnPinStart.TabIndex = 8;
      this.btnPinStart.Text = "start";
      this.btnPinStart.UseVisualStyleBackColor = true;
      this.btnPinStart.Click += new System.EventHandler(this.btnPinStart_Click);
      // 
      // grpPinSettings
      // 
      this.grpPinSettings.Controls.Add(this.tpDelayTo);
      this.grpPinSettings.Controls.Add(this.tpDelayFrom);
      this.grpPinSettings.Controls.Add(this.chkContionuousRun);
      this.grpPinSettings.Controls.Add(this.chkScheduled);
      this.grpPinSettings.Controls.Add(this.chkPinWatermark);
      this.grpPinSettings.Controls.Add(this.btnPinSettings);
      this.grpPinSettings.Controls.Add(this.btnPinBoardMapping);
      this.grpPinSettings.Controls.Add(this.chkAutopilot);
      this.grpPinSettings.Controls.Add(this.chkManual);
      this.grpPinSettings.Controls.Add(this.dtpScheduled);
      this.grpPinSettings.Location = new System.Drawing.Point(8, 5);
      this.grpPinSettings.Name = "grpPinSettings";
      this.grpPinSettings.Size = new System.Drawing.Size(250, 166);
      this.grpPinSettings.TabIndex = 3;
      this.grpPinSettings.TabStop = false;
      this.grpPinSettings.Text = "Settings";
      // 
      // tpDelayTo
      // 
      this.tpDelayTo.CustomFormat = "HH:mm";
      this.tpDelayTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
      this.tpDelayTo.Location = new System.Drawing.Point(189, 74);
      this.tpDelayTo.Name = "tpDelayTo";
      this.tpDelayTo.ShowUpDown = true;
      this.tpDelayTo.Size = new System.Drawing.Size(55, 20);
      this.tpDelayTo.TabIndex = 10;
      this.tpDelayTo.Visible = false;
      // 
      // tpDelayFrom
      // 
      this.tpDelayFrom.CustomFormat = "HH:mm";
      this.tpDelayFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
      this.tpDelayFrom.Location = new System.Drawing.Point(128, 74);
      this.tpDelayFrom.Name = "tpDelayFrom";
      this.tpDelayFrom.ShowUpDown = true;
      this.tpDelayFrom.Size = new System.Drawing.Size(55, 20);
      this.tpDelayFrom.TabIndex = 10;
      this.tpDelayFrom.Visible = false;
      // 
      // chkContionuousRun
      // 
      this.chkContionuousRun.AutoSize = true;
      this.chkContionuousRun.Location = new System.Drawing.Point(20, 77);
      this.chkContionuousRun.Name = "chkContionuousRun";
      this.chkContionuousRun.Size = new System.Drawing.Size(91, 17);
      this.chkContionuousRun.TabIndex = 9;
      this.chkContionuousRun.Text = "Continous run";
      this.chkContionuousRun.UseVisualStyleBackColor = true;
      this.chkContionuousRun.CheckedChanged += new System.EventHandler(this.chkContionuousRun_CheckedChanged);
      // 
      // chkScheduled
      // 
      this.chkScheduled.AutoSize = true;
      this.chkScheduled.Location = new System.Drawing.Point(156, 26);
      this.chkScheduled.Name = "chkScheduled";
      this.chkScheduled.Size = new System.Drawing.Size(77, 17);
      this.chkScheduled.TabIndex = 8;
      this.chkScheduled.Text = "Scheduled";
      this.chkScheduled.UseVisualStyleBackColor = true;
      this.chkScheduled.CheckedChanged += new System.EventHandler(this.chkScheduled_CheckedChanged);
      // 
      // chkPinWatermark
      // 
      this.chkPinWatermark.AutoSize = true;
      this.chkPinWatermark.Enabled = false;
      this.chkPinWatermark.Location = new System.Drawing.Point(20, 133);
      this.chkPinWatermark.Name = "chkPinWatermark";
      this.chkPinWatermark.Size = new System.Drawing.Size(78, 17);
      this.chkPinWatermark.TabIndex = 7;
      this.chkPinWatermark.Text = "Watermark";
      this.chkPinWatermark.UseVisualStyleBackColor = true;
      this.chkPinWatermark.CheckedChanged += new System.EventHandler(this.chkPinWatermark_CheckedChanged);
      // 
      // btnPinSettings
      // 
      this.btnPinSettings.Location = new System.Drawing.Point(20, 104);
      this.btnPinSettings.Name = "btnPinSettings";
      this.btnPinSettings.Size = new System.Drawing.Size(75, 23);
      this.btnPinSettings.TabIndex = 3;
      this.btnPinSettings.Text = "Settings...";
      this.btnPinSettings.UseVisualStyleBackColor = true;
      this.btnPinSettings.Click += new System.EventHandler(this.btnPinSettings_Click);
      // 
      // btnPinBoardMapping
      // 
      this.btnPinBoardMapping.Location = new System.Drawing.Point(111, 104);
      this.btnPinBoardMapping.Name = "btnPinBoardMapping";
      this.btnPinBoardMapping.Size = new System.Drawing.Size(133, 23);
      this.btnPinBoardMapping.TabIndex = 4;
      this.btnPinBoardMapping.Text = "Edit board mapping...";
      this.btnPinBoardMapping.UseVisualStyleBackColor = true;
      this.btnPinBoardMapping.Click += new System.EventHandler(this.btnPinBoardMapping_Click);
      // 
      // chkAutopilot
      // 
      this.chkAutopilot.AutoSize = true;
      this.chkAutopilot.Location = new System.Drawing.Point(84, 25);
      this.chkAutopilot.Name = "chkAutopilot";
      this.chkAutopilot.Size = new System.Drawing.Size(66, 17);
      this.chkAutopilot.TabIndex = 2;
      this.chkAutopilot.Text = "Autopilot";
      this.chkAutopilot.UseVisualStyleBackColor = true;
      this.chkAutopilot.CheckedChanged += new System.EventHandler(this.chkAutopilot_CheckedChanged);
      // 
      // chkManual
      // 
      this.chkManual.AutoSize = true;
      this.chkManual.Checked = true;
      this.chkManual.Location = new System.Drawing.Point(20, 25);
      this.chkManual.Name = "chkManual";
      this.chkManual.Size = new System.Drawing.Size(58, 17);
      this.chkManual.TabIndex = 0;
      this.chkManual.TabStop = true;
      this.chkManual.Text = "Normal";
      this.chkManual.UseVisualStyleBackColor = true;
      this.chkManual.CheckedChanged += new System.EventHandler(this.chkManual_CheckedChanged);
      // 
      // dtpScheduled
      // 
      this.dtpScheduled.CustomFormat = "MM/dd/yyyy  -  HH:mm";
      this.dtpScheduled.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
      this.dtpScheduled.Location = new System.Drawing.Point(20, 49);
      this.dtpScheduled.Name = "dtpScheduled";
      this.dtpScheduled.Size = new System.Drawing.Size(160, 20);
      this.dtpScheduled.TabIndex = 5;
      this.toolTip1.SetToolTip(this.dtpScheduled, "Scheduled date and time: MM dd yyyy  -  HH:mm\r\nNote that the time is in 24-hour f" +
        "ormat.");
      this.dtpScheduled.Visible = false;
      this.dtpScheduled.ValueChanged += new System.EventHandler(this.dtpScheduled_ValueChanged);
      // 
      // pictureBox2
      // 
      this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.pictureBox2.Image = global::PinBot.Properties.Resources.pinterest_icon;
      this.pictureBox2.Location = new System.Drawing.Point(647, 0);
      this.pictureBox2.Name = "pictureBox2";
      this.pictureBox2.Size = new System.Drawing.Size(133, 208);
      this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
      this.pictureBox2.TabIndex = 6;
      this.pictureBox2.TabStop = false;
      // 
      // Repin_
      // 
      this.Repin_.Controls.Add(this.pictureBox7);
      this.Repin_.Controls.Add(this.groupBox2);
      this.Repin_.Controls.Add(this.btnStartRepin);
      this.Repin_.Controls.Add(this.grpRepinSettings);
      this.Repin_.Controls.Add(this.btnStartRepinScrape);
      this.Repin_.Controls.Add(this.grpRepinScrapeSettings);
      this.Repin_.Location = new System.Drawing.Point(4, 22);
      this.Repin_.Name = "Repin_";
      this.Repin_.Size = new System.Drawing.Size(780, 208);
      this.Repin_.TabIndex = 6;
      this.Repin_.Text = "Repin";
      this.Repin_.UseVisualStyleBackColor = true;
      // 
      // pictureBox7
      // 
      this.pictureBox7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.pictureBox7.Image = global::PinBot.Properties.Resources.pinterest_icon;
      this.pictureBox7.Location = new System.Drawing.Point(647, 0);
      this.pictureBox7.Name = "pictureBox7";
      this.pictureBox7.Size = new System.Drawing.Size(133, 208);
      this.pictureBox7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
      this.pictureBox7.TabIndex = 26;
      this.pictureBox7.TabStop = false;
      // 
      // groupBox2
      // 
      this.groupBox2.Controls.Add(this.btnRepinEditQueue);
      this.groupBox2.Location = new System.Drawing.Point(448, 5);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(119, 60);
      this.groupBox2.TabIndex = 25;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Queue";
      // 
      // btnRepinEditQueue
      // 
      this.btnRepinEditQueue.Location = new System.Drawing.Point(6, 23);
      this.btnRepinEditQueue.Name = "btnRepinEditQueue";
      this.btnRepinEditQueue.Size = new System.Drawing.Size(95, 23);
      this.btnRepinEditQueue.TabIndex = 6;
      this.btnRepinEditQueue.Text = "Edit queue";
      this.btnRepinEditQueue.UseVisualStyleBackColor = true;
      this.btnRepinEditQueue.Click += new System.EventHandler(this.btnRepinEditQueue_Click);
      // 
      // btnStartRepin
      // 
      this.btnStartRepin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btnStartRepin.Location = new System.Drawing.Point(198, 177);
      this.btnStartRepin.Name = "btnStartRepin";
      this.btnStartRepin.Size = new System.Drawing.Size(96, 23);
      this.btnStartRepin.TabIndex = 24;
      this.btnStartRepin.Text = "start repin";
      this.btnStartRepin.UseVisualStyleBackColor = true;
      this.btnStartRepin.Click += new System.EventHandler(this.btnRepinStart_Click);
      // 
      // grpRepinSettings
      // 
      this.grpRepinSettings.Controls.Add(this.chkRepinScheduled);
      this.grpRepinSettings.Controls.Add(this.tpRepinDelayTo);
      this.grpRepinSettings.Controls.Add(this.tpRepinDelayFrom);
      this.grpRepinSettings.Controls.Add(this.chkRepinContinousRun);
      this.grpRepinSettings.Controls.Add(this.dtpRepinScheduled);
      this.grpRepinSettings.Controls.Add(this.rdbRepinManual);
      this.grpRepinSettings.Controls.Add(this.rdbRepinAutopilot);
      this.grpRepinSettings.Controls.Add(this.btnRepinSettings);
      this.grpRepinSettings.Location = new System.Drawing.Point(198, 5);
      this.grpRepinSettings.Name = "grpRepinSettings";
      this.grpRepinSettings.Size = new System.Drawing.Size(244, 166);
      this.grpRepinSettings.TabIndex = 23;
      this.grpRepinSettings.TabStop = false;
      this.grpRepinSettings.Text = "Repin";
      // 
      // chkRepinScheduled
      // 
      this.chkRepinScheduled.AutoSize = true;
      this.chkRepinScheduled.Location = new System.Drawing.Point(154, 20);
      this.chkRepinScheduled.Name = "chkRepinScheduled";
      this.chkRepinScheduled.Size = new System.Drawing.Size(77, 17);
      this.chkRepinScheduled.TabIndex = 14;
      this.chkRepinScheduled.Text = "Scheduled";
      this.chkRepinScheduled.UseVisualStyleBackColor = true;
      this.chkRepinScheduled.CheckedChanged += new System.EventHandler(this.chkRepinScheduled_CheckedChanged);
      // 
      // tpRepinDelayTo
      // 
      this.tpRepinDelayTo.CustomFormat = "HH:mm";
      this.tpRepinDelayTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
      this.tpRepinDelayTo.Location = new System.Drawing.Point(174, 107);
      this.tpRepinDelayTo.Name = "tpRepinDelayTo";
      this.tpRepinDelayTo.ShowUpDown = true;
      this.tpRepinDelayTo.Size = new System.Drawing.Size(55, 20);
      this.tpRepinDelayTo.TabIndex = 13;
      this.tpRepinDelayTo.Visible = false;
      // 
      // tpRepinDelayFrom
      // 
      this.tpRepinDelayFrom.CustomFormat = "HH:mm";
      this.tpRepinDelayFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
      this.tpRepinDelayFrom.Location = new System.Drawing.Point(113, 107);
      this.tpRepinDelayFrom.Name = "tpRepinDelayFrom";
      this.tpRepinDelayFrom.ShowUpDown = true;
      this.tpRepinDelayFrom.Size = new System.Drawing.Size(55, 20);
      this.tpRepinDelayFrom.TabIndex = 12;
      this.tpRepinDelayFrom.Visible = false;
      // 
      // chkRepinContinousRun
      // 
      this.chkRepinContinousRun.AutoSize = true;
      this.chkRepinContinousRun.Location = new System.Drawing.Point(16, 110);
      this.chkRepinContinousRun.Name = "chkRepinContinousRun";
      this.chkRepinContinousRun.Size = new System.Drawing.Size(91, 17);
      this.chkRepinContinousRun.TabIndex = 11;
      this.chkRepinContinousRun.Text = "Continous run";
      this.chkRepinContinousRun.UseVisualStyleBackColor = true;
      this.chkRepinContinousRun.CheckedChanged += new System.EventHandler(this.chkRepinContinousRun_CheckedChanged);
      // 
      // dtpRepinScheduled
      // 
      this.dtpRepinScheduled.CustomFormat = "MM/dd/yyyy  -  HH:mm";
      this.dtpRepinScheduled.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
      this.dtpRepinScheduled.Location = new System.Drawing.Point(16, 84);
      this.dtpRepinScheduled.Name = "dtpRepinScheduled";
      this.dtpRepinScheduled.Size = new System.Drawing.Size(160, 20);
      this.dtpRepinScheduled.TabIndex = 6;
      this.toolTip1.SetToolTip(this.dtpRepinScheduled, "Scheduled date and time: MM dd yyyy  -  HH:mm\r\nNote that the time is in 24-hour f" +
        "ormat.");
      this.dtpRepinScheduled.Visible = false;
      this.dtpRepinScheduled.ValueChanged += new System.EventHandler(this.dtpRepinScheduled_ValueChanged);
      // 
      // rdbRepinManual
      // 
      this.rdbRepinManual.AutoSize = true;
      this.rdbRepinManual.Checked = true;
      this.rdbRepinManual.Location = new System.Drawing.Point(16, 19);
      this.rdbRepinManual.Name = "rdbRepinManual";
      this.rdbRepinManual.Size = new System.Drawing.Size(60, 17);
      this.rdbRepinManual.TabIndex = 0;
      this.rdbRepinManual.TabStop = true;
      this.rdbRepinManual.Text = "Manual";
      this.rdbRepinManual.UseVisualStyleBackColor = true;
      this.rdbRepinManual.CheckedChanged += new System.EventHandler(this.rdbRepinManual_CheckedChanged);
      // 
      // rdbRepinAutopilot
      // 
      this.rdbRepinAutopilot.AutoSize = true;
      this.rdbRepinAutopilot.Location = new System.Drawing.Point(82, 19);
      this.rdbRepinAutopilot.Name = "rdbRepinAutopilot";
      this.rdbRepinAutopilot.Size = new System.Drawing.Size(66, 17);
      this.rdbRepinAutopilot.TabIndex = 1;
      this.rdbRepinAutopilot.Text = "Autopilot";
      this.rdbRepinAutopilot.UseVisualStyleBackColor = true;
      this.rdbRepinAutopilot.CheckedChanged += new System.EventHandler(this.rdbRepinAutopilot_CheckedChanged);
      // 
      // btnRepinSettings
      // 
      this.btnRepinSettings.Location = new System.Drawing.Point(16, 52);
      this.btnRepinSettings.Name = "btnRepinSettings";
      this.btnRepinSettings.Size = new System.Drawing.Size(75, 23);
      this.btnRepinSettings.TabIndex = 2;
      this.btnRepinSettings.Text = "Settings...";
      this.btnRepinSettings.UseVisualStyleBackColor = true;
      this.btnRepinSettings.Click += new System.EventHandler(this.btnRepinSettings_Click);
      // 
      // btnStartRepinScrape
      // 
      this.btnStartRepinScrape.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btnStartRepinScrape.Location = new System.Drawing.Point(8, 177);
      this.btnStartRepinScrape.Name = "btnStartRepinScrape";
      this.btnStartRepinScrape.Size = new System.Drawing.Size(96, 23);
      this.btnStartRepinScrape.TabIndex = 8;
      this.btnStartRepinScrape.Text = "start scrape";
      this.btnStartRepinScrape.UseVisualStyleBackColor = true;
      this.btnStartRepinScrape.Click += new System.EventHandler(this.btnStartRepinScrape_Click);
      // 
      // grpRepinScrapeSettings
      // 
      this.grpRepinScrapeSettings.Controls.Add(this.label11);
      this.grpRepinScrapeSettings.Controls.Add(this.numRepinScrapes);
      this.grpRepinScrapeSettings.Controls.Add(this.btnRepinEditUserMapping);
      this.grpRepinScrapeSettings.Controls.Add(this.btnRepinBoardMapping);
      this.grpRepinScrapeSettings.Location = new System.Drawing.Point(8, 5);
      this.grpRepinScrapeSettings.Name = "grpRepinScrapeSettings";
      this.grpRepinScrapeSettings.Size = new System.Drawing.Size(184, 166);
      this.grpRepinScrapeSettings.TabIndex = 22;
      this.grpRepinScrapeSettings.TabStop = false;
      this.grpRepinScrapeSettings.Text = "Scrape settings";
      // 
      // label11
      // 
      this.label11.AutoSize = true;
      this.label11.Location = new System.Drawing.Point(22, 87);
      this.label11.Name = "label11";
      this.label11.Size = new System.Drawing.Size(56, 13);
      this.label11.TabIndex = 6;
      this.label11.Text = "#Scrapes:";
      // 
      // numRepinScrapes
      // 
      this.numRepinScrapes.Location = new System.Drawing.Point(84, 84);
      this.numRepinScrapes.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
      this.numRepinScrapes.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.numRepinScrapes.Name = "numRepinScrapes";
      this.numRepinScrapes.Size = new System.Drawing.Size(59, 20);
      this.numRepinScrapes.TabIndex = 5;
      this.numRepinScrapes.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
      this.numRepinScrapes.ValueChanged += new System.EventHandler(this.numRepinScrapes_ValueChanged);
      // 
      // btnRepinEditUserMapping
      // 
      this.btnRepinEditUserMapping.Location = new System.Drawing.Point(10, 52);
      this.btnRepinEditUserMapping.Name = "btnRepinEditUserMapping";
      this.btnRepinEditUserMapping.Size = new System.Drawing.Size(133, 23);
      this.btnRepinEditUserMapping.TabIndex = 4;
      this.btnRepinEditUserMapping.Text = "Edit user mapping...";
      this.btnRepinEditUserMapping.UseVisualStyleBackColor = true;
      this.btnRepinEditUserMapping.Click += new System.EventHandler(this.btnRepinEditUserMapping_Click);
      // 
      // btnRepinBoardMapping
      // 
      this.btnRepinBoardMapping.Location = new System.Drawing.Point(10, 23);
      this.btnRepinBoardMapping.Name = "btnRepinBoardMapping";
      this.btnRepinBoardMapping.Size = new System.Drawing.Size(133, 23);
      this.btnRepinBoardMapping.TabIndex = 3;
      this.btnRepinBoardMapping.Text = "Edit board mapping...";
      this.btnRepinBoardMapping.UseVisualStyleBackColor = true;
      this.btnRepinBoardMapping.Click += new System.EventHandler(this.btnRepinBoardMapping_Click);
      // 
      // Like_
      // 
      this.Like_.Controls.Add(this.pictureBox6);
      this.Like_.Controls.Add(this.grpLikeSettings);
      this.Like_.Controls.Add(this.btnLikeStart);
      this.Like_.Location = new System.Drawing.Point(4, 22);
      this.Like_.Name = "Like_";
      this.Like_.Size = new System.Drawing.Size(780, 208);
      this.Like_.TabIndex = 7;
      this.Like_.Text = "Like";
      this.Like_.UseVisualStyleBackColor = true;
      // 
      // pictureBox6
      // 
      this.pictureBox6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.pictureBox6.Image = global::PinBot.Properties.Resources.pinterest_icon;
      this.pictureBox6.Location = new System.Drawing.Point(647, 0);
      this.pictureBox6.Name = "pictureBox6";
      this.pictureBox6.Size = new System.Drawing.Size(133, 208);
      this.pictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
      this.pictureBox6.TabIndex = 26;
      this.pictureBox6.TabStop = false;
      // 
      // grpLikeSettings
      // 
      this.grpLikeSettings.Controls.Add(this.chkLikeScheduled);
      this.grpLikeSettings.Controls.Add(this.tpLikeDelayTo);
      this.grpLikeSettings.Controls.Add(this.tpLikeDelayFrom);
      this.grpLikeSettings.Controls.Add(this.chkLikeContinuousRun);
      this.grpLikeSettings.Controls.Add(this.dtpLikeScheduled);
      this.grpLikeSettings.Controls.Add(this.btnLikeEditCats);
      this.grpLikeSettings.Controls.Add(this.btnLikeEditUsers);
      this.grpLikeSettings.Controls.Add(this.rdbLikeManual);
      this.grpLikeSettings.Controls.Add(this.btnLikeSettings);
      this.grpLikeSettings.Location = new System.Drawing.Point(8, 5);
      this.grpLikeSettings.Name = "grpLikeSettings";
      this.grpLikeSettings.Size = new System.Drawing.Size(328, 166);
      this.grpLikeSettings.TabIndex = 25;
      this.grpLikeSettings.TabStop = false;
      this.grpLikeSettings.Text = "Settings";
      // 
      // chkLikeScheduled
      // 
      this.chkLikeScheduled.AutoSize = true;
      this.chkLikeScheduled.Location = new System.Drawing.Point(72, 20);
      this.chkLikeScheduled.Name = "chkLikeScheduled";
      this.chkLikeScheduled.Size = new System.Drawing.Size(77, 17);
      this.chkLikeScheduled.TabIndex = 33;
      this.chkLikeScheduled.Text = "Scheduled";
      this.chkLikeScheduled.UseVisualStyleBackColor = true;
      this.chkLikeScheduled.CheckedChanged += new System.EventHandler(this.chkLikeScheduled_CheckedChanged);
      // 
      // tpLikeDelayTo
      // 
      this.tpLikeDelayTo.CustomFormat = "HH:mm";
      this.tpLikeDelayTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
      this.tpLikeDelayTo.Location = new System.Drawing.Point(164, 131);
      this.tpLikeDelayTo.Name = "tpLikeDelayTo";
      this.tpLikeDelayTo.ShowUpDown = true;
      this.tpLikeDelayTo.Size = new System.Drawing.Size(55, 20);
      this.tpLikeDelayTo.TabIndex = 32;
      this.tpLikeDelayTo.Visible = false;
      // 
      // tpLikeDelayFrom
      // 
      this.tpLikeDelayFrom.CustomFormat = "HH:mm";
      this.tpLikeDelayFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
      this.tpLikeDelayFrom.Location = new System.Drawing.Point(103, 131);
      this.tpLikeDelayFrom.Name = "tpLikeDelayFrom";
      this.tpLikeDelayFrom.ShowUpDown = true;
      this.tpLikeDelayFrom.Size = new System.Drawing.Size(55, 20);
      this.tpLikeDelayFrom.TabIndex = 31;
      this.tpLikeDelayFrom.Visible = false;
      // 
      // chkLikeContinuousRun
      // 
      this.chkLikeContinuousRun.AutoSize = true;
      this.chkLikeContinuousRun.Location = new System.Drawing.Point(6, 134);
      this.chkLikeContinuousRun.Name = "chkLikeContinuousRun";
      this.chkLikeContinuousRun.Size = new System.Drawing.Size(91, 17);
      this.chkLikeContinuousRun.TabIndex = 30;
      this.chkLikeContinuousRun.Text = "Continous run";
      this.chkLikeContinuousRun.UseVisualStyleBackColor = true;
      this.chkLikeContinuousRun.CheckedChanged += new System.EventHandler(this.chkLikeContinuousRun_CheckedChanged);
      // 
      // dtpLikeScheduled
      // 
      this.dtpLikeScheduled.CustomFormat = "MM/dd/yyyy  -  HH:mm";
      this.dtpLikeScheduled.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
      this.dtpLikeScheduled.Location = new System.Drawing.Point(155, 17);
      this.dtpLikeScheduled.Name = "dtpLikeScheduled";
      this.dtpLikeScheduled.Size = new System.Drawing.Size(160, 20);
      this.dtpLikeScheduled.TabIndex = 29;
      this.toolTip1.SetToolTip(this.dtpLikeScheduled, "Scheduled date and time: MM dd yyyy  -  HH:mm\r\nNote that the time is in 24-hour f" +
        "ormat.");
      this.dtpLikeScheduled.Visible = false;
      this.dtpLikeScheduled.ValueChanged += new System.EventHandler(this.dtpLikeScheduled_ValueChanged);
      // 
      // btnLikeEditCats
      // 
      this.btnLikeEditCats.Location = new System.Drawing.Point(6, 76);
      this.btnLikeEditCats.Name = "btnLikeEditCats";
      this.btnLikeEditCats.Size = new System.Drawing.Size(108, 23);
      this.btnLikeEditCats.TabIndex = 2;
      this.btnLikeEditCats.Text = "Edit categories";
      this.btnLikeEditCats.UseVisualStyleBackColor = true;
      this.btnLikeEditCats.Click += new System.EventHandler(this.btnLikeEditCats_Click);
      // 
      // btnLikeEditUsers
      // 
      this.btnLikeEditUsers.AccessibleDescription = "";
      this.btnLikeEditUsers.Location = new System.Drawing.Point(6, 105);
      this.btnLikeEditUsers.Name = "btnLikeEditUsers";
      this.btnLikeEditUsers.Size = new System.Drawing.Size(108, 23);
      this.btnLikeEditUsers.TabIndex = 3;
      this.btnLikeEditUsers.Text = "Edit users";
      this.btnLikeEditUsers.UseVisualStyleBackColor = true;
      this.btnLikeEditUsers.Click += new System.EventHandler(this.btnLikeEditUsers_Click);
      // 
      // rdbLikeManual
      // 
      this.rdbLikeManual.AutoSize = true;
      this.rdbLikeManual.Checked = true;
      this.rdbLikeManual.Location = new System.Drawing.Point(6, 19);
      this.rdbLikeManual.Name = "rdbLikeManual";
      this.rdbLikeManual.Size = new System.Drawing.Size(60, 17);
      this.rdbLikeManual.TabIndex = 27;
      this.rdbLikeManual.TabStop = true;
      this.rdbLikeManual.Text = "Manual";
      this.rdbLikeManual.UseVisualStyleBackColor = true;
      // 
      // btnLikeSettings
      // 
      this.btnLikeSettings.Location = new System.Drawing.Point(6, 47);
      this.btnLikeSettings.Name = "btnLikeSettings";
      this.btnLikeSettings.Size = new System.Drawing.Size(75, 23);
      this.btnLikeSettings.TabIndex = 1;
      this.btnLikeSettings.Text = "Settings...";
      this.btnLikeSettings.UseVisualStyleBackColor = true;
      this.btnLikeSettings.Click += new System.EventHandler(this.btnLikeSettings_Click);
      // 
      // btnLikeStart
      // 
      this.btnLikeStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btnLikeStart.Location = new System.Drawing.Point(8, 177);
      this.btnLikeStart.Name = "btnLikeStart";
      this.btnLikeStart.Size = new System.Drawing.Size(52, 23);
      this.btnLikeStart.TabIndex = 4;
      this.btnLikeStart.Text = "start";
      this.btnLikeStart.UseVisualStyleBackColor = true;
      this.btnLikeStart.Click += new System.EventHandler(this.btnLikeStart_Click);
      // 
      // Invite_
      // 
      this.Invite_.Controls.Add(this.pictureBox5);
      this.Invite_.Controls.Add(this.btnInviteStart);
      this.Invite_.Controls.Add(this.grpInviteSettings);
      this.Invite_.Location = new System.Drawing.Point(4, 22);
      this.Invite_.Name = "Invite_";
      this.Invite_.Size = new System.Drawing.Size(780, 208);
      this.Invite_.TabIndex = 8;
      this.Invite_.Text = "Invite";
      this.Invite_.UseVisualStyleBackColor = true;
      // 
      // pictureBox5
      // 
      this.pictureBox5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.pictureBox5.Image = global::PinBot.Properties.Resources.pinterest_icon;
      this.pictureBox5.Location = new System.Drawing.Point(647, 0);
      this.pictureBox5.Name = "pictureBox5";
      this.pictureBox5.Size = new System.Drawing.Size(133, 208);
      this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
      this.pictureBox5.TabIndex = 30;
      this.pictureBox5.TabStop = false;
      // 
      // btnInviteStart
      // 
      this.btnInviteStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btnInviteStart.Location = new System.Drawing.Point(8, 177);
      this.btnInviteStart.Name = "btnInviteStart";
      this.btnInviteStart.Size = new System.Drawing.Size(52, 23);
      this.btnInviteStart.TabIndex = 2;
      this.btnInviteStart.Text = "start";
      this.btnInviteStart.UseVisualStyleBackColor = true;
      this.btnInviteStart.Click += new System.EventHandler(this.btnInviteStart_Click);
      // 
      // grpInviteSettings
      // 
      this.grpInviteSettings.Controls.Add(this.chkInviteScheduled);
      this.grpInviteSettings.Controls.Add(this.tpInviteDelayTo);
      this.grpInviteSettings.Controls.Add(this.tpInviteDelayFrom);
      this.grpInviteSettings.Controls.Add(this.chkInviteContinuousRun);
      this.grpInviteSettings.Controls.Add(this.dtpInviteScheduled);
      this.grpInviteSettings.Controls.Add(this.rdbInviteManual);
      this.grpInviteSettings.Controls.Add(this.btnInviteSettings);
      this.grpInviteSettings.Controls.Add(this.label49);
      this.grpInviteSettings.Controls.Add(this.lstInviteBoards);
      this.grpInviteSettings.Location = new System.Drawing.Point(8, 5);
      this.grpInviteSettings.Name = "grpInviteSettings";
      this.grpInviteSettings.Size = new System.Drawing.Size(451, 166);
      this.grpInviteSettings.TabIndex = 29;
      this.grpInviteSettings.TabStop = false;
      this.grpInviteSettings.Text = "Settings";
      // 
      // chkInviteScheduled
      // 
      this.chkInviteScheduled.AutoSize = true;
      this.chkInviteScheduled.Location = new System.Drawing.Point(351, 39);
      this.chkInviteScheduled.Name = "chkInviteScheduled";
      this.chkInviteScheduled.Size = new System.Drawing.Size(77, 17);
      this.chkInviteScheduled.TabIndex = 28;
      this.chkInviteScheduled.Text = "Scheduled";
      this.chkInviteScheduled.UseVisualStyleBackColor = true;
      this.chkInviteScheduled.CheckedChanged += new System.EventHandler(this.chkInviteScheduled_CheckedChanged);
      // 
      // tpInviteDelayTo
      // 
      this.tpInviteDelayTo.CustomFormat = "HH:mm";
      this.tpInviteDelayTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
      this.tpInviteDelayTo.Location = new System.Drawing.Point(346, 110);
      this.tpInviteDelayTo.Name = "tpInviteDelayTo";
      this.tpInviteDelayTo.ShowUpDown = true;
      this.tpInviteDelayTo.Size = new System.Drawing.Size(55, 20);
      this.tpInviteDelayTo.TabIndex = 27;
      this.tpInviteDelayTo.Visible = false;
      // 
      // tpInviteDelayFrom
      // 
      this.tpInviteDelayFrom.CustomFormat = "HH:mm";
      this.tpInviteDelayFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
      this.tpInviteDelayFrom.Location = new System.Drawing.Point(285, 110);
      this.tpInviteDelayFrom.Name = "tpInviteDelayFrom";
      this.tpInviteDelayFrom.ShowUpDown = true;
      this.tpInviteDelayFrom.Size = new System.Drawing.Size(55, 20);
      this.tpInviteDelayFrom.TabIndex = 26;
      this.tpInviteDelayFrom.Visible = false;
      // 
      // chkInviteContinuousRun
      // 
      this.chkInviteContinuousRun.AutoSize = true;
      this.chkInviteContinuousRun.Location = new System.Drawing.Point(285, 87);
      this.chkInviteContinuousRun.Name = "chkInviteContinuousRun";
      this.chkInviteContinuousRun.Size = new System.Drawing.Size(91, 17);
      this.chkInviteContinuousRun.TabIndex = 25;
      this.chkInviteContinuousRun.Text = "Continous run";
      this.chkInviteContinuousRun.UseVisualStyleBackColor = true;
      this.chkInviteContinuousRun.CheckedChanged += new System.EventHandler(this.chkInviteContinuousRun_CheckedChanged);
      // 
      // dtpInviteScheduled
      // 
      this.dtpInviteScheduled.CustomFormat = "MM/dd/yyyy  -  HH:mm";
      this.dtpInviteScheduled.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
      this.dtpInviteScheduled.Location = new System.Drawing.Point(285, 61);
      this.dtpInviteScheduled.Name = "dtpInviteScheduled";
      this.dtpInviteScheduled.Size = new System.Drawing.Size(160, 20);
      this.dtpInviteScheduled.TabIndex = 24;
      this.toolTip1.SetToolTip(this.dtpInviteScheduled, "Scheduled date and time: MM dd yyyy  -  HH:mm\r\nNote that the time is in 24-hour f" +
        "ormat.");
      this.dtpInviteScheduled.Visible = false;
      this.dtpInviteScheduled.ValueChanged += new System.EventHandler(this.dtpInviteScheduled_ValueChanged);
      // 
      // rdbInviteManual
      // 
      this.rdbInviteManual.AutoSize = true;
      this.rdbInviteManual.Checked = true;
      this.rdbInviteManual.Location = new System.Drawing.Point(285, 38);
      this.rdbInviteManual.Name = "rdbInviteManual";
      this.rdbInviteManual.Size = new System.Drawing.Size(60, 17);
      this.rdbInviteManual.TabIndex = 22;
      this.rdbInviteManual.TabStop = true;
      this.rdbInviteManual.Text = "Manual";
      this.rdbInviteManual.UseVisualStyleBackColor = true;
      // 
      // btnInviteSettings
      // 
      this.btnInviteSettings.Location = new System.Drawing.Point(6, 35);
      this.btnInviteSettings.Name = "btnInviteSettings";
      this.btnInviteSettings.Size = new System.Drawing.Size(75, 23);
      this.btnInviteSettings.TabIndex = 0;
      this.btnInviteSettings.Text = "Settings...";
      this.btnInviteSettings.UseVisualStyleBackColor = true;
      this.btnInviteSettings.Click += new System.EventHandler(this.btnInviteSettings_Click);
      // 
      // label49
      // 
      this.label49.AutoSize = true;
      this.label49.Location = new System.Drawing.Point(97, 16);
      this.label49.Name = "label49";
      this.label49.Size = new System.Drawing.Size(43, 13);
      this.label49.TabIndex = 21;
      this.label49.Text = "Boards:";
      // 
      // lstInviteBoards
      // 
      this.lstInviteBoards.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
      this.lstInviteBoards.FormattingEnabled = true;
      this.lstInviteBoards.Location = new System.Drawing.Point(100, 35);
      this.lstInviteBoards.Name = "lstInviteBoards";
      this.lstInviteBoards.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
      this.lstInviteBoards.Size = new System.Drawing.Size(169, 121);
      this.lstInviteBoards.TabIndex = 1;
      // 
      // Follow_
      // 
      this.Follow_.Controls.Add(this.pictureBox3);
      this.Follow_.Controls.Add(this.grpFollowAlgorithm);
      this.Follow_.Controls.Add(this.grpFollowSettings);
      this.Follow_.Controls.Add(this.btnFollowStart);
      this.Follow_.Location = new System.Drawing.Point(4, 22);
      this.Follow_.Name = "Follow_";
      this.Follow_.Size = new System.Drawing.Size(780, 208);
      this.Follow_.TabIndex = 2;
      this.Follow_.Text = "Follow";
      this.Follow_.UseVisualStyleBackColor = true;
      // 
      // pictureBox3
      // 
      this.pictureBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.pictureBox3.Image = global::PinBot.Properties.Resources.pinterest_icon;
      this.pictureBox3.Location = new System.Drawing.Point(647, 0);
      this.pictureBox3.Name = "pictureBox3";
      this.pictureBox3.Size = new System.Drawing.Size(133, 208);
      this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
      this.pictureBox3.TabIndex = 25;
      this.pictureBox3.TabStop = false;
      // 
      // grpFollowAlgorithm
      // 
      this.grpFollowAlgorithm.Controls.Add(this.chkFollow_hasTW);
      this.grpFollowAlgorithm.Controls.Add(this.chkFollow_hasFB);
      this.grpFollowAlgorithm.Controls.Add(this.txtFollow_PinsMax);
      this.grpFollowAlgorithm.Controls.Add(this.label18);
      this.grpFollowAlgorithm.Controls.Add(this.txtFollow_PinsMin);
      this.grpFollowAlgorithm.Controls.Add(this.label19);
      this.grpFollowAlgorithm.Controls.Add(this.chkFollow_hasWebsite);
      this.grpFollowAlgorithm.Controls.Add(this.chkFollow_isPartner);
      this.grpFollowAlgorithm.Controls.Add(this.txtFollow_BoardsMax);
      this.grpFollowAlgorithm.Controls.Add(this.label16);
      this.grpFollowAlgorithm.Controls.Add(this.txtFollow_BoardsMin);
      this.grpFollowAlgorithm.Controls.Add(this.label17);
      this.grpFollowAlgorithm.Controls.Add(this.txtFollow_FollowingMax);
      this.grpFollowAlgorithm.Controls.Add(this.label14);
      this.grpFollowAlgorithm.Controls.Add(this.txtFollow_FollowingMin);
      this.grpFollowAlgorithm.Controls.Add(this.label15);
      this.grpFollowAlgorithm.Controls.Add(this.txtFollow_FollowersMax);
      this.grpFollowAlgorithm.Controls.Add(this.label13);
      this.grpFollowAlgorithm.Controls.Add(this.txtFollow_FollowersMin);
      this.grpFollowAlgorithm.Controls.Add(this.label12);
      this.grpFollowAlgorithm.Location = new System.Drawing.Point(305, 5);
      this.grpFollowAlgorithm.Name = "grpFollowAlgorithm";
      this.grpFollowAlgorithm.Size = new System.Drawing.Size(336, 193);
      this.grpFollowAlgorithm.TabIndex = 24;
      this.grpFollowAlgorithm.TabStop = false;
      this.grpFollowAlgorithm.Text = "Follow user if:";
      // 
      // chkFollow_hasTW
      // 
      this.chkFollow_hasTW.AutoSize = true;
      this.chkFollow_hasTW.Location = new System.Drawing.Point(30, 165);
      this.chkFollow_hasTW.Name = "chkFollow_hasTW";
      this.chkFollow_hasTW.Size = new System.Drawing.Size(78, 17);
      this.chkFollow_hasTW.TabIndex = 14;
      this.chkFollow_hasTW.Text = "has Twitter";
      this.chkFollow_hasTW.UseVisualStyleBackColor = true;
      // 
      // chkFollow_hasFB
      // 
      this.chkFollow_hasFB.AutoSize = true;
      this.chkFollow_hasFB.Location = new System.Drawing.Point(123, 165);
      this.chkFollow_hasFB.Name = "chkFollow_hasFB";
      this.chkFollow_hasFB.Size = new System.Drawing.Size(94, 17);
      this.chkFollow_hasFB.TabIndex = 15;
      this.chkFollow_hasFB.Text = "has Facebook";
      this.chkFollow_hasFB.UseVisualStyleBackColor = true;
      // 
      // txtFollow_PinsMax
      // 
      this.txtFollow_PinsMax.Location = new System.Drawing.Point(190, 110);
      this.txtFollow_PinsMax.Mask = "0000000";
      this.txtFollow_PinsMax.Name = "txtFollow_PinsMax";
      this.txtFollow_PinsMax.PromptChar = ' ';
      this.txtFollow_PinsMax.Size = new System.Drawing.Size(42, 20);
      this.txtFollow_PinsMax.TabIndex = 11;
      this.txtFollow_PinsMax.Text = "2000";
      // 
      // label18
      // 
      this.label18.AutoSize = true;
      this.label18.Location = new System.Drawing.Point(159, 113);
      this.label18.Name = "label18";
      this.label18.Size = new System.Drawing.Size(25, 13);
      this.label18.TabIndex = 37;
      this.label18.Text = "and";
      // 
      // txtFollow_PinsMin
      // 
      this.txtFollow_PinsMin.Location = new System.Drawing.Point(111, 110);
      this.txtFollow_PinsMin.Mask = "0000000";
      this.txtFollow_PinsMin.Name = "txtFollow_PinsMin";
      this.txtFollow_PinsMin.PromptChar = ' ';
      this.txtFollow_PinsMin.Size = new System.Drawing.Size(42, 20);
      this.txtFollow_PinsMin.TabIndex = 10;
      this.txtFollow_PinsMin.Text = "200";
      // 
      // label19
      // 
      this.label19.AutoSize = true;
      this.label19.Location = new System.Drawing.Point(28, 113);
      this.label19.Name = "label19";
      this.label19.Size = new System.Drawing.Size(77, 13);
      this.label19.TabIndex = 35;
      this.label19.Text = "#pins between";
      // 
      // chkFollow_hasWebsite
      // 
      this.chkFollow_hasWebsite.AutoSize = true;
      this.chkFollow_hasWebsite.Location = new System.Drawing.Point(123, 142);
      this.chkFollow_hasWebsite.Name = "chkFollow_hasWebsite";
      this.chkFollow_hasWebsite.Size = new System.Drawing.Size(82, 17);
      this.chkFollow_hasWebsite.TabIndex = 13;
      this.chkFollow_hasWebsite.Text = "has website";
      this.chkFollow_hasWebsite.UseVisualStyleBackColor = true;
      // 
      // chkFollow_isPartner
      // 
      this.chkFollow_isPartner.AutoSize = true;
      this.chkFollow_isPartner.Location = new System.Drawing.Point(30, 142);
      this.chkFollow_isPartner.Name = "chkFollow_isPartner";
      this.chkFollow_isPartner.Size = new System.Drawing.Size(69, 17);
      this.chkFollow_isPartner.TabIndex = 12;
      this.chkFollow_isPartner.Text = "is partner";
      this.chkFollow_isPartner.UseVisualStyleBackColor = true;
      // 
      // txtFollow_BoardsMax
      // 
      this.txtFollow_BoardsMax.Location = new System.Drawing.Point(190, 84);
      this.txtFollow_BoardsMax.Mask = "0000000";
      this.txtFollow_BoardsMax.Name = "txtFollow_BoardsMax";
      this.txtFollow_BoardsMax.PromptChar = ' ';
      this.txtFollow_BoardsMax.Size = new System.Drawing.Size(42, 20);
      this.txtFollow_BoardsMax.TabIndex = 9;
      this.txtFollow_BoardsMax.Text = "100";
      // 
      // label16
      // 
      this.label16.AutoSize = true;
      this.label16.Location = new System.Drawing.Point(159, 87);
      this.label16.Name = "label16";
      this.label16.Size = new System.Drawing.Size(25, 13);
      this.label16.TabIndex = 31;
      this.label16.Text = "and";
      // 
      // txtFollow_BoardsMin
      // 
      this.txtFollow_BoardsMin.Location = new System.Drawing.Point(111, 84);
      this.txtFollow_BoardsMin.Mask = "0000000";
      this.txtFollow_BoardsMin.Name = "txtFollow_BoardsMin";
      this.txtFollow_BoardsMin.PromptChar = ' ';
      this.txtFollow_BoardsMin.Size = new System.Drawing.Size(42, 20);
      this.txtFollow_BoardsMin.TabIndex = 8;
      this.txtFollow_BoardsMin.Text = "5";
      // 
      // label17
      // 
      this.label17.AutoSize = true;
      this.label17.Location = new System.Drawing.Point(15, 87);
      this.label17.Name = "label17";
      this.label17.Size = new System.Drawing.Size(90, 13);
      this.label17.TabIndex = 29;
      this.label17.Text = "#boards between";
      // 
      // txtFollow_FollowingMax
      // 
      this.txtFollow_FollowingMax.Location = new System.Drawing.Point(190, 58);
      this.txtFollow_FollowingMax.Mask = "0000000";
      this.txtFollow_FollowingMax.Name = "txtFollow_FollowingMax";
      this.txtFollow_FollowingMax.PromptChar = ' ';
      this.txtFollow_FollowingMax.Size = new System.Drawing.Size(42, 20);
      this.txtFollow_FollowingMax.TabIndex = 7;
      this.txtFollow_FollowingMax.Text = "5000";
      // 
      // label14
      // 
      this.label14.AutoSize = true;
      this.label14.Location = new System.Drawing.Point(159, 61);
      this.label14.Name = "label14";
      this.label14.Size = new System.Drawing.Size(25, 13);
      this.label14.TabIndex = 27;
      this.label14.Text = "and";
      // 
      // txtFollow_FollowingMin
      // 
      this.txtFollow_FollowingMin.Location = new System.Drawing.Point(111, 58);
      this.txtFollow_FollowingMin.Mask = "0000000";
      this.txtFollow_FollowingMin.Name = "txtFollow_FollowingMin";
      this.txtFollow_FollowingMin.PromptChar = ' ';
      this.txtFollow_FollowingMin.Size = new System.Drawing.Size(42, 20);
      this.txtFollow_FollowingMin.TabIndex = 6;
      this.txtFollow_FollowingMin.Text = "50";
      // 
      // label15
      // 
      this.label15.AutoSize = true;
      this.label15.Location = new System.Drawing.Point(6, 61);
      this.label15.Name = "label15";
      this.label15.Size = new System.Drawing.Size(99, 13);
      this.label15.TabIndex = 25;
      this.label15.Text = "#following between";
      // 
      // txtFollow_FollowersMax
      // 
      this.txtFollow_FollowersMax.Location = new System.Drawing.Point(190, 32);
      this.txtFollow_FollowersMax.Mask = "0000000";
      this.txtFollow_FollowersMax.Name = "txtFollow_FollowersMax";
      this.txtFollow_FollowersMax.PromptChar = ' ';
      this.txtFollow_FollowersMax.Size = new System.Drawing.Size(42, 20);
      this.txtFollow_FollowersMax.TabIndex = 5;
      this.txtFollow_FollowersMax.Text = "5000";
      // 
      // label13
      // 
      this.label13.AutoSize = true;
      this.label13.Location = new System.Drawing.Point(159, 35);
      this.label13.Name = "label13";
      this.label13.Size = new System.Drawing.Size(25, 13);
      this.label13.TabIndex = 23;
      this.label13.Text = "and";
      // 
      // txtFollow_FollowersMin
      // 
      this.txtFollow_FollowersMin.Location = new System.Drawing.Point(111, 32);
      this.txtFollow_FollowersMin.Mask = "0000000";
      this.txtFollow_FollowersMin.Name = "txtFollow_FollowersMin";
      this.txtFollow_FollowersMin.PromptChar = ' ';
      this.txtFollow_FollowersMin.Size = new System.Drawing.Size(42, 20);
      this.txtFollow_FollowersMin.TabIndex = 4;
      this.txtFollow_FollowersMin.Text = "50";
      // 
      // label12
      // 
      this.label12.AutoSize = true;
      this.label12.Location = new System.Drawing.Point(6, 35);
      this.label12.Name = "label12";
      this.label12.Size = new System.Drawing.Size(99, 13);
      this.label12.TabIndex = 0;
      this.label12.Text = "#followers between";
      // 
      // grpFollowSettings
      // 
      this.grpFollowSettings.Controls.Add(this.chkFollowScheduled);
      this.grpFollowSettings.Controls.Add(this.tpFollowDelayTo);
      this.grpFollowSettings.Controls.Add(this.tpFollowDelayFrom);
      this.grpFollowSettings.Controls.Add(this.chkFollowContinuousRun);
      this.grpFollowSettings.Controls.Add(this.dtpFollowScheduled);
      this.grpFollowSettings.Controls.Add(this.rdbFollowManual);
      this.grpFollowSettings.Controls.Add(this.chkFollowIgnoreCriteria);
      this.grpFollowSettings.Controls.Add(this.btnFollowEditCats);
      this.grpFollowSettings.Controls.Add(this.btnFollowEditUsers);
      this.grpFollowSettings.Controls.Add(this.btnFollowSettings);
      this.grpFollowSettings.Location = new System.Drawing.Point(8, 5);
      this.grpFollowSettings.Name = "grpFollowSettings";
      this.grpFollowSettings.Size = new System.Drawing.Size(291, 166);
      this.grpFollowSettings.TabIndex = 23;
      this.grpFollowSettings.TabStop = false;
      this.grpFollowSettings.Text = "Settings";
      // 
      // chkFollowScheduled
      // 
      this.chkFollowScheduled.AutoSize = true;
      this.chkFollowScheduled.Location = new System.Drawing.Point(181, 39);
      this.chkFollowScheduled.Name = "chkFollowScheduled";
      this.chkFollowScheduled.Size = new System.Drawing.Size(77, 17);
      this.chkFollowScheduled.TabIndex = 31;
      this.chkFollowScheduled.Text = "Scheduled";
      this.chkFollowScheduled.UseVisualStyleBackColor = true;
      this.chkFollowScheduled.CheckedChanged += new System.EventHandler(this.chkFollowScheduled_CheckedChanged);
      // 
      // tpFollowDelayTo
      // 
      this.tpFollowDelayTo.CustomFormat = "HH:mm";
      this.tpFollowDelayTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
      this.tpFollowDelayTo.Location = new System.Drawing.Point(181, 119);
      this.tpFollowDelayTo.Name = "tpFollowDelayTo";
      this.tpFollowDelayTo.ShowUpDown = true;
      this.tpFollowDelayTo.Size = new System.Drawing.Size(55, 20);
      this.tpFollowDelayTo.TabIndex = 30;
      this.tpFollowDelayTo.Visible = false;
      // 
      // tpFollowDelayFrom
      // 
      this.tpFollowDelayFrom.CustomFormat = "HH:mm";
      this.tpFollowDelayFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
      this.tpFollowDelayFrom.Location = new System.Drawing.Point(120, 119);
      this.tpFollowDelayFrom.Name = "tpFollowDelayFrom";
      this.tpFollowDelayFrom.ShowUpDown = true;
      this.tpFollowDelayFrom.Size = new System.Drawing.Size(55, 20);
      this.tpFollowDelayFrom.TabIndex = 29;
      this.tpFollowDelayFrom.Visible = false;
      // 
      // chkFollowContinuousRun
      // 
      this.chkFollowContinuousRun.AutoSize = true;
      this.chkFollowContinuousRun.Location = new System.Drawing.Point(120, 96);
      this.chkFollowContinuousRun.Name = "chkFollowContinuousRun";
      this.chkFollowContinuousRun.Size = new System.Drawing.Size(91, 17);
      this.chkFollowContinuousRun.TabIndex = 28;
      this.chkFollowContinuousRun.Text = "Continous run";
      this.chkFollowContinuousRun.UseVisualStyleBackColor = true;
      this.chkFollowContinuousRun.CheckedChanged += new System.EventHandler(this.chkFollowContinuousRun_CheckedChanged);
      // 
      // dtpFollowScheduled
      // 
      this.dtpFollowScheduled.CustomFormat = "MM/dd/yyyy  -  HH:mm";
      this.dtpFollowScheduled.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
      this.dtpFollowScheduled.Location = new System.Drawing.Point(121, 67);
      this.dtpFollowScheduled.Name = "dtpFollowScheduled";
      this.dtpFollowScheduled.Size = new System.Drawing.Size(160, 20);
      this.dtpFollowScheduled.TabIndex = 27;
      this.toolTip1.SetToolTip(this.dtpFollowScheduled, "Scheduled date and time: MM dd yyyy  -  HH:mm\r\nNote that the time is in 24-hour f" +
        "ormat.");
      this.dtpFollowScheduled.Visible = false;
      this.dtpFollowScheduled.ValueChanged += new System.EventHandler(this.dtpFollowScheduled_ValueChanged);
      // 
      // rdbFollowManual
      // 
      this.rdbFollowManual.AutoSize = true;
      this.rdbFollowManual.Checked = true;
      this.rdbFollowManual.Location = new System.Drawing.Point(121, 38);
      this.rdbFollowManual.Name = "rdbFollowManual";
      this.rdbFollowManual.Size = new System.Drawing.Size(60, 17);
      this.rdbFollowManual.TabIndex = 25;
      this.rdbFollowManual.TabStop = true;
      this.rdbFollowManual.Text = "Manual";
      this.rdbFollowManual.UseVisualStyleBackColor = true;
      // 
      // chkFollowIgnoreCriteria
      // 
      this.chkFollowIgnoreCriteria.AutoSize = true;
      this.chkFollowIgnoreCriteria.Location = new System.Drawing.Point(6, 122);
      this.chkFollowIgnoreCriteria.Name = "chkFollowIgnoreCriteria";
      this.chkFollowIgnoreCriteria.Size = new System.Drawing.Size(95, 17);
      this.chkFollowIgnoreCriteria.TabIndex = 4;
      this.chkFollowIgnoreCriteria.Text = "Disable criteria";
      this.chkFollowIgnoreCriteria.UseVisualStyleBackColor = true;
      this.chkFollowIgnoreCriteria.CheckedChanged += new System.EventHandler(this.chkFollowIgnoreCriteria_CheckedChanged);
      // 
      // btnFollowEditCats
      // 
      this.btnFollowEditCats.Location = new System.Drawing.Point(6, 64);
      this.btnFollowEditCats.Name = "btnFollowEditCats";
      this.btnFollowEditCats.Size = new System.Drawing.Size(108, 23);
      this.btnFollowEditCats.TabIndex = 2;
      this.btnFollowEditCats.Text = "Edit categories";
      this.btnFollowEditCats.UseVisualStyleBackColor = true;
      this.btnFollowEditCats.Click += new System.EventHandler(this.btnFollowEditCats_Click);
      // 
      // btnFollowEditUsers
      // 
      this.btnFollowEditUsers.Location = new System.Drawing.Point(6, 93);
      this.btnFollowEditUsers.Name = "btnFollowEditUsers";
      this.btnFollowEditUsers.Size = new System.Drawing.Size(108, 23);
      this.btnFollowEditUsers.TabIndex = 3;
      this.btnFollowEditUsers.Text = "Edit users";
      this.btnFollowEditUsers.UseVisualStyleBackColor = true;
      this.btnFollowEditUsers.Click += new System.EventHandler(this.btnFollowEditUsers_Click);
      // 
      // btnFollowSettings
      // 
      this.btnFollowSettings.Location = new System.Drawing.Point(6, 35);
      this.btnFollowSettings.Name = "btnFollowSettings";
      this.btnFollowSettings.Size = new System.Drawing.Size(75, 23);
      this.btnFollowSettings.TabIndex = 1;
      this.btnFollowSettings.Text = "Settings...";
      this.btnFollowSettings.UseVisualStyleBackColor = true;
      this.btnFollowSettings.Click += new System.EventHandler(this.btnFollowSettings_Click);
      // 
      // btnFollowStart
      // 
      this.btnFollowStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btnFollowStart.Location = new System.Drawing.Point(8, 177);
      this.btnFollowStart.Name = "btnFollowStart";
      this.btnFollowStart.Size = new System.Drawing.Size(52, 23);
      this.btnFollowStart.TabIndex = 16;
      this.btnFollowStart.Text = "start";
      this.btnFollowStart.UseVisualStyleBackColor = true;
      this.btnFollowStart.Click += new System.EventHandler(this.btnStartFollow_Click);
      // 
      // Unfollow_
      // 
      this.Unfollow_.Controls.Add(this.pictureBox4);
      this.Unfollow_.Controls.Add(this.grpUnfollowAlgorithm);
      this.Unfollow_.Controls.Add(this.grpUnfollowSettings);
      this.Unfollow_.Controls.Add(this.btnUnfollowStart);
      this.Unfollow_.Location = new System.Drawing.Point(4, 22);
      this.Unfollow_.Name = "Unfollow_";
      this.Unfollow_.Size = new System.Drawing.Size(780, 208);
      this.Unfollow_.TabIndex = 3;
      this.Unfollow_.Text = "Unfollow";
      this.Unfollow_.UseVisualStyleBackColor = true;
      // 
      // pictureBox4
      // 
      this.pictureBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.pictureBox4.Image = global::PinBot.Properties.Resources.pinterest_icon;
      this.pictureBox4.Location = new System.Drawing.Point(647, 0);
      this.pictureBox4.Name = "pictureBox4";
      this.pictureBox4.Size = new System.Drawing.Size(133, 208);
      this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
      this.pictureBox4.TabIndex = 29;
      this.pictureBox4.TabStop = false;
      // 
      // grpUnfollowAlgorithm
      // 
      this.grpUnfollowAlgorithm.Controls.Add(this.chkUnfollowNonFollower);
      this.grpUnfollowAlgorithm.Controls.Add(this.txtUnfollow_PinsMax);
      this.grpUnfollowAlgorithm.Controls.Add(this.label24);
      this.grpUnfollowAlgorithm.Controls.Add(this.txtUnfollow_PinsMin);
      this.grpUnfollowAlgorithm.Controls.Add(this.label25);
      this.grpUnfollowAlgorithm.Controls.Add(this.txtUnfollow_BoardsMax);
      this.grpUnfollowAlgorithm.Controls.Add(this.label22);
      this.grpUnfollowAlgorithm.Controls.Add(this.txtUnfollow_BoardsMin);
      this.grpUnfollowAlgorithm.Controls.Add(this.label23);
      this.grpUnfollowAlgorithm.Controls.Add(this.txtUnfollow_FollowingMax);
      this.grpUnfollowAlgorithm.Controls.Add(this.label20);
      this.grpUnfollowAlgorithm.Controls.Add(this.txtUnfollow_FollowingMin);
      this.grpUnfollowAlgorithm.Controls.Add(this.label21);
      this.grpUnfollowAlgorithm.Controls.Add(this.chkUnfollow_hasWebsite);
      this.grpUnfollowAlgorithm.Controls.Add(this.chkUnfollow_isPartner);
      this.grpUnfollowAlgorithm.Controls.Add(this.txtUnfollow_FollowersMax);
      this.grpUnfollowAlgorithm.Controls.Add(this.label26);
      this.grpUnfollowAlgorithm.Controls.Add(this.txtUnfollow_FollowersMin);
      this.grpUnfollowAlgorithm.Controls.Add(this.label27);
      this.grpUnfollowAlgorithm.Location = new System.Drawing.Point(219, 5);
      this.grpUnfollowAlgorithm.Name = "grpUnfollowAlgorithm";
      this.grpUnfollowAlgorithm.Size = new System.Drawing.Size(335, 193);
      this.grpUnfollowAlgorithm.TabIndex = 28;
      this.grpUnfollowAlgorithm.TabStop = false;
      this.grpUnfollowAlgorithm.Text = "Unfollow user if:";
      // 
      // chkUnfollowNonFollower
      // 
      this.chkUnfollowNonFollower.AutoSize = true;
      this.chkUnfollowNonFollower.Location = new System.Drawing.Point(199, 148);
      this.chkUnfollowNonFollower.Name = "chkUnfollowNonFollower";
      this.chkUnfollowNonFollower.Size = new System.Drawing.Size(105, 17);
      this.chkUnfollowNonFollower.TabIndex = 11;
      this.chkUnfollowNonFollower.Text = "not following you";
      this.chkUnfollowNonFollower.UseVisualStyleBackColor = true;
      // 
      // txtUnfollow_PinsMax
      // 
      this.txtUnfollow_PinsMax.Location = new System.Drawing.Point(250, 109);
      this.txtUnfollow_PinsMax.Mask = "0000000";
      this.txtUnfollow_PinsMax.Name = "txtUnfollow_PinsMax";
      this.txtUnfollow_PinsMax.PromptChar = ' ';
      this.txtUnfollow_PinsMax.Size = new System.Drawing.Size(42, 20);
      this.txtUnfollow_PinsMax.TabIndex = 8;
      this.txtUnfollow_PinsMax.Text = "5000";
      // 
      // label24
      // 
      this.label24.AutoSize = true;
      this.label24.Location = new System.Drawing.Point(159, 113);
      this.label24.Name = "label24";
      this.label24.Size = new System.Drawing.Size(85, 13);
      this.label24.TabIndex = 45;
      this.label24.Text = "and greater than";
      // 
      // txtUnfollow_PinsMin
      // 
      this.txtUnfollow_PinsMin.Location = new System.Drawing.Point(111, 110);
      this.txtUnfollow_PinsMin.Mask = "0000000";
      this.txtUnfollow_PinsMin.Name = "txtUnfollow_PinsMin";
      this.txtUnfollow_PinsMin.PromptChar = ' ';
      this.txtUnfollow_PinsMin.Size = new System.Drawing.Size(42, 20);
      this.txtUnfollow_PinsMin.TabIndex = 7;
      this.txtUnfollow_PinsMin.Text = "200";
      // 
      // label25
      // 
      this.label25.AutoSize = true;
      this.label25.Location = new System.Drawing.Point(27, 112);
      this.label25.Name = "label25";
      this.label25.Size = new System.Drawing.Size(78, 13);
      this.label25.TabIndex = 43;
      this.label25.Text = "#pins less than";
      // 
      // txtUnfollow_BoardsMax
      // 
      this.txtUnfollow_BoardsMax.Location = new System.Drawing.Point(250, 83);
      this.txtUnfollow_BoardsMax.Mask = "0000000";
      this.txtUnfollow_BoardsMax.Name = "txtUnfollow_BoardsMax";
      this.txtUnfollow_BoardsMax.PromptChar = ' ';
      this.txtUnfollow_BoardsMax.Size = new System.Drawing.Size(42, 20);
      this.txtUnfollow_BoardsMax.TabIndex = 6;
      this.txtUnfollow_BoardsMax.Text = "100";
      // 
      // label22
      // 
      this.label22.AutoSize = true;
      this.label22.Location = new System.Drawing.Point(159, 87);
      this.label22.Name = "label22";
      this.label22.Size = new System.Drawing.Size(85, 13);
      this.label22.TabIndex = 41;
      this.label22.Text = "and greater than";
      // 
      // txtUnfollow_BoardsMin
      // 
      this.txtUnfollow_BoardsMin.Location = new System.Drawing.Point(111, 84);
      this.txtUnfollow_BoardsMin.Mask = "0000000";
      this.txtUnfollow_BoardsMin.Name = "txtUnfollow_BoardsMin";
      this.txtUnfollow_BoardsMin.PromptChar = ' ';
      this.txtUnfollow_BoardsMin.Size = new System.Drawing.Size(42, 20);
      this.txtUnfollow_BoardsMin.TabIndex = 5;
      this.txtUnfollow_BoardsMin.Text = "5";
      // 
      // label23
      // 
      this.label23.AutoSize = true;
      this.label23.Location = new System.Drawing.Point(14, 87);
      this.label23.Name = "label23";
      this.label23.Size = new System.Drawing.Size(91, 13);
      this.label23.TabIndex = 39;
      this.label23.Text = "#boards less than";
      // 
      // txtUnfollow_FollowingMax
      // 
      this.txtUnfollow_FollowingMax.Location = new System.Drawing.Point(250, 57);
      this.txtUnfollow_FollowingMax.Mask = "0000000";
      this.txtUnfollow_FollowingMax.Name = "txtUnfollow_FollowingMax";
      this.txtUnfollow_FollowingMax.PromptChar = ' ';
      this.txtUnfollow_FollowingMax.Size = new System.Drawing.Size(42, 20);
      this.txtUnfollow_FollowingMax.TabIndex = 4;
      this.txtUnfollow_FollowingMax.Text = "5000";
      // 
      // label20
      // 
      this.label20.AutoSize = true;
      this.label20.Location = new System.Drawing.Point(159, 61);
      this.label20.Name = "label20";
      this.label20.Size = new System.Drawing.Size(85, 13);
      this.label20.TabIndex = 37;
      this.label20.Text = "and greater than";
      // 
      // txtUnfollow_FollowingMin
      // 
      this.txtUnfollow_FollowingMin.Location = new System.Drawing.Point(111, 58);
      this.txtUnfollow_FollowingMin.Mask = "0000000";
      this.txtUnfollow_FollowingMin.Name = "txtUnfollow_FollowingMin";
      this.txtUnfollow_FollowingMin.PromptChar = ' ';
      this.txtUnfollow_FollowingMin.Size = new System.Drawing.Size(42, 20);
      this.txtUnfollow_FollowingMin.TabIndex = 3;
      this.txtUnfollow_FollowingMin.Text = "50";
      // 
      // label21
      // 
      this.label21.AutoSize = true;
      this.label21.Location = new System.Drawing.Point(6, 61);
      this.label21.Name = "label21";
      this.label21.Size = new System.Drawing.Size(100, 13);
      this.label21.TabIndex = 35;
      this.label21.Text = "#following less than";
      // 
      // chkUnfollow_hasWebsite
      // 
      this.chkUnfollow_hasWebsite.AutoSize = true;
      this.chkUnfollow_hasWebsite.Location = new System.Drawing.Point(111, 148);
      this.chkUnfollow_hasWebsite.Name = "chkUnfollow_hasWebsite";
      this.chkUnfollow_hasWebsite.Size = new System.Drawing.Size(82, 17);
      this.chkUnfollow_hasWebsite.TabIndex = 10;
      this.chkUnfollow_hasWebsite.Text = "has website";
      this.chkUnfollow_hasWebsite.UseVisualStyleBackColor = true;
      // 
      // chkUnfollow_isPartner
      // 
      this.chkUnfollow_isPartner.AutoSize = true;
      this.chkUnfollow_isPartner.Location = new System.Drawing.Point(36, 148);
      this.chkUnfollow_isPartner.Name = "chkUnfollow_isPartner";
      this.chkUnfollow_isPartner.Size = new System.Drawing.Size(69, 17);
      this.chkUnfollow_isPartner.TabIndex = 9;
      this.chkUnfollow_isPartner.Text = "is partner";
      this.chkUnfollow_isPartner.UseVisualStyleBackColor = true;
      // 
      // txtUnfollow_FollowersMax
      // 
      this.txtUnfollow_FollowersMax.Location = new System.Drawing.Point(250, 31);
      this.txtUnfollow_FollowersMax.Mask = "0000000";
      this.txtUnfollow_FollowersMax.Name = "txtUnfollow_FollowersMax";
      this.txtUnfollow_FollowersMax.PromptChar = ' ';
      this.txtUnfollow_FollowersMax.Size = new System.Drawing.Size(42, 20);
      this.txtUnfollow_FollowersMax.TabIndex = 2;
      this.txtUnfollow_FollowersMax.Text = "5000";
      // 
      // label26
      // 
      this.label26.AutoSize = true;
      this.label26.Location = new System.Drawing.Point(159, 35);
      this.label26.Name = "label26";
      this.label26.Size = new System.Drawing.Size(85, 13);
      this.label26.TabIndex = 23;
      this.label26.Text = "and greater than";
      // 
      // txtUnfollow_FollowersMin
      // 
      this.txtUnfollow_FollowersMin.Location = new System.Drawing.Point(111, 32);
      this.txtUnfollow_FollowersMin.Mask = "0000000";
      this.txtUnfollow_FollowersMin.Name = "txtUnfollow_FollowersMin";
      this.txtUnfollow_FollowersMin.PromptChar = ' ';
      this.txtUnfollow_FollowersMin.Size = new System.Drawing.Size(42, 20);
      this.txtUnfollow_FollowersMin.TabIndex = 1;
      this.txtUnfollow_FollowersMin.Text = "50";
      // 
      // label27
      // 
      this.label27.AutoSize = true;
      this.label27.Location = new System.Drawing.Point(6, 35);
      this.label27.Name = "label27";
      this.label27.Size = new System.Drawing.Size(100, 13);
      this.label27.TabIndex = 0;
      this.label27.Text = "#followers less than";
      // 
      // grpUnfollowSettings
      // 
      this.grpUnfollowSettings.Controls.Add(this.chkUnfollowScheduled);
      this.grpUnfollowSettings.Controls.Add(this.tpUnfollowDelayTo);
      this.grpUnfollowSettings.Controls.Add(this.tpUnfollowDelayFrom);
      this.grpUnfollowSettings.Controls.Add(this.chkUnfollowContinuousRun);
      this.grpUnfollowSettings.Controls.Add(this.dtpUnfollowScheduled);
      this.grpUnfollowSettings.Controls.Add(this.rdbUnfollowManual);
      this.grpUnfollowSettings.Controls.Add(this.btnUnfollowSettings);
      this.grpUnfollowSettings.Location = new System.Drawing.Point(8, 5);
      this.grpUnfollowSettings.Name = "grpUnfollowSettings";
      this.grpUnfollowSettings.Size = new System.Drawing.Size(205, 166);
      this.grpUnfollowSettings.TabIndex = 27;
      this.grpUnfollowSettings.TabStop = false;
      this.grpUnfollowSettings.Text = "Settings";
      // 
      // chkUnfollowScheduled
      // 
      this.chkUnfollowScheduled.AutoSize = true;
      this.chkUnfollowScheduled.Location = new System.Drawing.Point(67, 75);
      this.chkUnfollowScheduled.Name = "chkUnfollowScheduled";
      this.chkUnfollowScheduled.Size = new System.Drawing.Size(77, 17);
      this.chkUnfollowScheduled.TabIndex = 31;
      this.chkUnfollowScheduled.Text = "Scheduled";
      this.chkUnfollowScheduled.UseVisualStyleBackColor = true;
      this.chkUnfollowScheduled.CheckedChanged += new System.EventHandler(this.chkUnfollowScheduled_CheckedChanged);
      // 
      // tpUnfollowDelayTo
      // 
      this.tpUnfollowDelayTo.CustomFormat = "HH:mm";
      this.tpUnfollowDelayTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
      this.tpUnfollowDelayTo.Location = new System.Drawing.Point(67, 143);
      this.tpUnfollowDelayTo.Name = "tpUnfollowDelayTo";
      this.tpUnfollowDelayTo.ShowUpDown = true;
      this.tpUnfollowDelayTo.Size = new System.Drawing.Size(55, 20);
      this.tpUnfollowDelayTo.TabIndex = 30;
      this.tpUnfollowDelayTo.Visible = false;
      // 
      // tpUnfollowDelayFrom
      // 
      this.tpUnfollowDelayFrom.CustomFormat = "HH:mm";
      this.tpUnfollowDelayFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
      this.tpUnfollowDelayFrom.Location = new System.Drawing.Point(6, 143);
      this.tpUnfollowDelayFrom.Name = "tpUnfollowDelayFrom";
      this.tpUnfollowDelayFrom.ShowUpDown = true;
      this.tpUnfollowDelayFrom.Size = new System.Drawing.Size(55, 20);
      this.tpUnfollowDelayFrom.TabIndex = 29;
      this.tpUnfollowDelayFrom.Visible = false;
      // 
      // chkUnfollowContinuousRun
      // 
      this.chkUnfollowContinuousRun.AutoSize = true;
      this.chkUnfollowContinuousRun.Location = new System.Drawing.Point(6, 123);
      this.chkUnfollowContinuousRun.Name = "chkUnfollowContinuousRun";
      this.chkUnfollowContinuousRun.Size = new System.Drawing.Size(91, 17);
      this.chkUnfollowContinuousRun.TabIndex = 28;
      this.chkUnfollowContinuousRun.Text = "Continous run";
      this.chkUnfollowContinuousRun.UseVisualStyleBackColor = true;
      this.chkUnfollowContinuousRun.CheckedChanged += new System.EventHandler(this.chkUnfollowContinuousRun_CheckedChanged);
      // 
      // dtpUnfollowScheduled
      // 
      this.dtpUnfollowScheduled.CustomFormat = "MM/dd/yyyy  -  HH:mm";
      this.dtpUnfollowScheduled.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
      this.dtpUnfollowScheduled.Location = new System.Drawing.Point(6, 97);
      this.dtpUnfollowScheduled.Name = "dtpUnfollowScheduled";
      this.dtpUnfollowScheduled.Size = new System.Drawing.Size(160, 20);
      this.dtpUnfollowScheduled.TabIndex = 27;
      this.toolTip1.SetToolTip(this.dtpUnfollowScheduled, "Scheduled date and time: MM dd yyyy  -  HH:mm\r\nNote that the time is in 24-hour f" +
        "ormat.");
      this.dtpUnfollowScheduled.Visible = false;
      this.dtpUnfollowScheduled.ValueChanged += new System.EventHandler(this.dtpUnfollowScheduled_ValueChanged);
      // 
      // rdbUnfollowManual
      // 
      this.rdbUnfollowManual.AutoSize = true;
      this.rdbUnfollowManual.Checked = true;
      this.rdbUnfollowManual.Location = new System.Drawing.Point(6, 74);
      this.rdbUnfollowManual.Name = "rdbUnfollowManual";
      this.rdbUnfollowManual.Size = new System.Drawing.Size(60, 17);
      this.rdbUnfollowManual.TabIndex = 25;
      this.rdbUnfollowManual.TabStop = true;
      this.rdbUnfollowManual.Text = "Manual";
      this.rdbUnfollowManual.UseVisualStyleBackColor = true;
      // 
      // btnUnfollowSettings
      // 
      this.btnUnfollowSettings.Location = new System.Drawing.Point(6, 35);
      this.btnUnfollowSettings.Name = "btnUnfollowSettings";
      this.btnUnfollowSettings.Size = new System.Drawing.Size(75, 23);
      this.btnUnfollowSettings.TabIndex = 0;
      this.btnUnfollowSettings.Text = "Settings...";
      this.btnUnfollowSettings.UseVisualStyleBackColor = true;
      this.btnUnfollowSettings.Click += new System.EventHandler(this.btnUnfollowSettings_Click);
      // 
      // btnUnfollowStart
      // 
      this.btnUnfollowStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btnUnfollowStart.Location = new System.Drawing.Point(8, 177);
      this.btnUnfollowStart.Name = "btnUnfollowStart";
      this.btnUnfollowStart.Size = new System.Drawing.Size(52, 23);
      this.btnUnfollowStart.TabIndex = 12;
      this.btnUnfollowStart.Text = "start";
      this.btnUnfollowStart.UseVisualStyleBackColor = true;
      this.btnUnfollowStart.Click += new System.EventHandler(this.btnUnfollowStart_Click);
      // 
      // Account_
      // 
      this.Account_.Controls.Add(this.stats);
      this.Account_.Controls.Add(this.btnAccountRefreshStats);
      this.Account_.Controls.Add(this.pictureBox8);
      this.Account_.Controls.Add(this.btnAccountReloadBoards);
      this.Account_.Location = new System.Drawing.Point(4, 22);
      this.Account_.Name = "Account_";
      this.Account_.Size = new System.Drawing.Size(780, 208);
      this.Account_.TabIndex = 9;
      this.Account_.Text = "Account";
      this.Account_.UseVisualStyleBackColor = true;
      // 
      // stats
      // 
      this.stats.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.stats.Location = new System.Drawing.Point(136, 41);
      this.stats.Margin = new System.Windows.Forms.Padding(2);
      this.stats.Name = "stats";
      this.stats.ReadOnly = true;
      this.stats.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
      this.stats.Size = new System.Drawing.Size(123, 93);
      this.stats.TabIndex = 9;
      this.stats.Text = "";
      // 
      // btnAccountRefreshStats
      // 
      this.btnAccountRefreshStats.Location = new System.Drawing.Point(136, 14);
      this.btnAccountRefreshStats.Margin = new System.Windows.Forms.Padding(2);
      this.btnAccountRefreshStats.Name = "btnAccountRefreshStats";
      this.btnAccountRefreshStats.Size = new System.Drawing.Size(122, 23);
      this.btnAccountRefreshStats.TabIndex = 8;
      this.btnAccountRefreshStats.Text = "Refresh stats";
      this.btnAccountRefreshStats.UseVisualStyleBackColor = true;
      this.btnAccountRefreshStats.Click += new System.EventHandler(this.btnAccountRefreshStats_Click);
      // 
      // pictureBox8
      // 
      this.pictureBox8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.pictureBox8.Image = global::PinBot.Properties.Resources.pinterest_icon;
      this.pictureBox8.Location = new System.Drawing.Point(647, 0);
      this.pictureBox8.Name = "pictureBox8";
      this.pictureBox8.Size = new System.Drawing.Size(133, 208);
      this.pictureBox8.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
      this.pictureBox8.TabIndex = 7;
      this.pictureBox8.TabStop = false;
      // 
      // btnAccountReloadBoards
      // 
      this.btnAccountReloadBoards.Location = new System.Drawing.Point(8, 14);
      this.btnAccountReloadBoards.Name = "btnAccountReloadBoards";
      this.btnAccountReloadBoards.Size = new System.Drawing.Size(122, 23);
      this.btnAccountReloadBoards.TabIndex = 0;
      this.btnAccountReloadBoards.Text = "Reload boards";
      this.toolTip1.SetToolTip(this.btnAccountReloadBoards, "If you\'ve created a new board on your account manually,\r\nthen you can reload them" +
        " into PinBot clicking this button.");
      this.btnAccountReloadBoards.UseVisualStyleBackColor = true;
      this.btnAccountReloadBoards.Click += new System.EventHandler(this.btnAccountReloadBoards_Click);
      // 
      // lblStatus1
      // 
      this.lblStatus1.BackColor = System.Drawing.Color.Transparent;
      this.lblStatus1.Margin = new System.Windows.Forms.Padding(5, 3, 2, 2);
      this.lblStatus1.Name = "lblStatus1";
      this.lblStatus1.Size = new System.Drawing.Size(38, 17);
      this.lblStatus1.Text = "status";
      // 
      // statusStrip1
      // 
      this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSplitButton1,
            this.tslNotification,
            this.lblUpdate,
            this.lblStatus1});
      this.statusStrip1.Location = new System.Drawing.Point(0, 235);
      this.statusStrip1.Name = "statusStrip1";
      this.statusStrip1.Size = new System.Drawing.Size(784, 22);
      this.statusStrip1.TabIndex = 6;
      this.statusStrip1.Text = "statusStrip1";
      // 
      // toolStripSplitButton1
      // 
      this.toolStripSplitButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.toolStripSplitButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.visitBlogToolStripMenuItem,
            this.visitWebsiteToolStripMenuItem,
            this.fAQToolStripMenuItem,
            this.toolStripSeparator2,
            this.whatsNewToolStripMenuItem,
            this.clickTheBlueQuestionmarkToOpenTheManualToolStripMenuItem});
      this.toolStripSplitButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSplitButton1.Image")));
      this.toolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
      this.toolStripSplitButton1.Name = "toolStripSplitButton1";
      this.toolStripSplitButton1.Size = new System.Drawing.Size(32, 20);
      this.toolStripSplitButton1.Tag = "Open the manual.";
      this.toolStripSplitButton1.Text = "toolStripSplitButton1";
      this.toolStripSplitButton1.ButtonClick += new System.EventHandler(this.toolStripSplitButton1_ButtonClick);
      // 
      // visitBlogToolStripMenuItem
      // 
      this.visitBlogToolStripMenuItem.Name = "visitBlogToolStripMenuItem";
      this.visitBlogToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
      this.visitBlogToolStripMenuItem.Text = "Visit blog";
      this.visitBlogToolStripMenuItem.Click += new System.EventHandler(this.visitBlogToolStripMenuItem_Click);
      // 
      // visitWebsiteToolStripMenuItem
      // 
      this.visitWebsiteToolStripMenuItem.Name = "visitWebsiteToolStripMenuItem";
      this.visitWebsiteToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
      this.visitWebsiteToolStripMenuItem.Text = "Visit website";
      this.visitWebsiteToolStripMenuItem.Click += new System.EventHandler(this.visitWebsiteToolStripMenuItem_Click);
      // 
      // fAQToolStripMenuItem
      // 
      this.fAQToolStripMenuItem.Name = "fAQToolStripMenuItem";
      this.fAQToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
      this.fAQToolStripMenuItem.Text = "FAQ";
      this.fAQToolStripMenuItem.Click += new System.EventHandler(this.fAQToolStripMenuItem_Click);
      // 
      // toolStripSeparator2
      // 
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new System.Drawing.Size(143, 6);
      // 
      // whatsNewToolStripMenuItem
      // 
      this.whatsNewToolStripMenuItem.Name = "whatsNewToolStripMenuItem";
      this.whatsNewToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
      this.whatsNewToolStripMenuItem.Text = "What\'s new?";
      this.whatsNewToolStripMenuItem.Click += new System.EventHandler(this.whatsNewToolStripMenuItem_Click);
      // 
      // clickTheBlueQuestionmarkToOpenTheManualToolStripMenuItem
      // 
      this.clickTheBlueQuestionmarkToOpenTheManualToolStripMenuItem.Name = "clickTheBlueQuestionmarkToOpenTheManualToolStripMenuItem";
      this.clickTheBlueQuestionmarkToOpenTheManualToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
      this.clickTheBlueQuestionmarkToOpenTheManualToolStripMenuItem.Text = "Open manual";
      this.clickTheBlueQuestionmarkToOpenTheManualToolStripMenuItem.Click += new System.EventHandler(this.clickTheBlueQuestionmarkToOpenTheManualToolStripMenuItem_Click);
      // 
      // tslNotification
      // 
      this.tslNotification.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
      this.tslNotification.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
      this.tslNotification.Image = global::PinBot.Properties.Resources.imp;
      this.tslNotification.IsLink = true;
      this.tslNotification.Name = "tslNotification";
      this.tslNotification.Size = new System.Drawing.Size(16, 17);
      this.tslNotification.Text = "   ";
      this.tslNotification.ToolTipText = "Important message (click me)";
      this.tslNotification.Visible = false;
      this.tslNotification.Click += new System.EventHandler(this.tslNotification_Click);
      // 
      // lblUpdate
      // 
      this.lblUpdate.BackColor = System.Drawing.Color.LimeGreen;
      this.lblUpdate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.lblUpdate.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblUpdate.ForeColor = System.Drawing.Color.White;
      this.lblUpdate.IsLink = true;
      this.lblUpdate.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
      this.lblUpdate.Name = "lblUpdate";
      this.lblUpdate.Size = new System.Drawing.Size(117, 17);
      this.lblUpdate.Text = "UPDATE AVAILABLE!";
      this.lblUpdate.Visible = false;
      this.lblUpdate.Click += new System.EventHandler(this.lblUpdate_Click);
      // 
      // tmrStatus
      // 
      this.tmrStatus.Interval = 250;
      this.tmrStatus.Tick += new System.EventHandler(this.tmrStatus_Tick);
      // 
      // tmrUpdate
      // 
      this.tmrUpdate.Interval = 900000;
      this.tmrUpdate.Tick += new System.EventHandler(this.tmrUpdate_Tick);
      // 
      // mainForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.SystemColors.ControlLight;
      this.ClientSize = new System.Drawing.Size(784, 257);
      this.Controls.Add(this.statusStrip1);
      this.Controls.Add(this.tabs);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MinimumSize = new System.Drawing.Size(800, 293);
      this.Name = "mainForm";
      this.Text = "PinterestBot © healzer.com";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.mainForm_FormClosing);
      this.Load += new System.EventHandler(this.Form1_Load);
      this.tabs.ResumeLayout(false);
      this.Main_.ResumeLayout(false);
      this.Main_.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
      this.grpLogin.ResumeLayout(false);
      this.grpLogin.PerformLayout();
      this.pnlProxy.ResumeLayout(false);
      this.pnlProxy.PerformLayout();
      this.grpMainLicense.ResumeLayout(false);
      this.grpMainLicense.PerformLayout();
      this.Scrape_.ResumeLayout(false);
      this.groupBox1.ResumeLayout(false);
      this.grpScrapeSettings.ResumeLayout(false);
      this.grpScrapeSettings.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.numScrapeSourceURL)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.numScrapeWebsiteInDesc)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.txtNumScrapes)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).EndInit();
      this.Pin_.ResumeLayout(false);
      this.flowLayoutPanel2.ResumeLayout(false);
      this.grpPinURL.ResumeLayout(false);
      this.grpPinURL.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.numPinWebsiteInDesc)).EndInit();
      this.grpSourceURL.ResumeLayout(false);
      this.grpSourceURL.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.numPinSourceURL)).EndInit();
      this.grpPinSettings.ResumeLayout(false);
      this.grpPinSettings.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
      this.Repin_.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).EndInit();
      this.groupBox2.ResumeLayout(false);
      this.grpRepinSettings.ResumeLayout(false);
      this.grpRepinSettings.PerformLayout();
      this.grpRepinScrapeSettings.ResumeLayout(false);
      this.grpRepinScrapeSettings.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.numRepinScrapes)).EndInit();
      this.Like_.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
      this.grpLikeSettings.ResumeLayout(false);
      this.grpLikeSettings.PerformLayout();
      this.Invite_.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
      this.grpInviteSettings.ResumeLayout(false);
      this.grpInviteSettings.PerformLayout();
      this.Follow_.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
      this.grpFollowAlgorithm.ResumeLayout(false);
      this.grpFollowAlgorithm.PerformLayout();
      this.grpFollowSettings.ResumeLayout(false);
      this.grpFollowSettings.PerformLayout();
      this.Unfollow_.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
      this.grpUnfollowAlgorithm.ResumeLayout(false);
      this.grpUnfollowAlgorithm.PerformLayout();
      this.grpUnfollowSettings.ResumeLayout(false);
      this.grpUnfollowSettings.PerformLayout();
      this.Account_.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).EndInit();
      this.statusStrip1.ResumeLayout(false);
      this.statusStrip1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TabControl tabs;
    private System.Windows.Forms.TabPage Main_;
    private System.Windows.Forms.TabPage Pin_;
    private System.Windows.Forms.TabPage Follow_;
    private System.Windows.Forms.TabPage Unfollow_;
    private System.Windows.Forms.Button btnLogin;
    private System.Windows.Forms.MaskedTextBox txtEmail;
    private System.Windows.Forms.MaskedTextBox txtPassword;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.ToolStripStatusLabel lblStatus1;
    private System.Windows.Forms.StatusStrip statusStrip1;
    private System.Windows.Forms.Button btnPinStart;
    private System.Windows.Forms.GroupBox grpPinSettings;
    private System.Windows.Forms.ToolTip toolTip1;
    private System.Windows.Forms.RadioButton chkManual;
    private System.Windows.Forms.RadioButton chkAutopilot;
    private System.Windows.Forms.DateTimePicker dtpScheduled;
    private System.Windows.Forms.GroupBox grpLogin;
    private System.Windows.Forms.TabPage Scrape_;
    private System.Windows.Forms.PictureBox pictureBox2;
    private System.Windows.Forms.Button btnScrapeStart;
    private System.Windows.Forms.Timer tmrStatus;
    private System.Windows.Forms.Timer tmrUpdate;
    private System.Windows.Forms.Button btnFollowStart;
    private System.Windows.Forms.GroupBox grpFollowSettings;
    private System.Windows.Forms.GroupBox grpFollowAlgorithm;
    private System.Windows.Forms.MaskedTextBox txtFollow_BoardsMax;
    private System.Windows.Forms.Label label16;
    private System.Windows.Forms.MaskedTextBox txtFollow_BoardsMin;
    private System.Windows.Forms.Label label17;
    private System.Windows.Forms.MaskedTextBox txtFollow_FollowingMax;
    private System.Windows.Forms.Label label14;
    private System.Windows.Forms.MaskedTextBox txtFollow_FollowingMin;
    private System.Windows.Forms.Label label15;
    private System.Windows.Forms.MaskedTextBox txtFollow_FollowersMax;
    private System.Windows.Forms.Label label13;
    private System.Windows.Forms.MaskedTextBox txtFollow_FollowersMin;
    private System.Windows.Forms.Label label12;
    private System.Windows.Forms.CheckBox chkFollow_hasWebsite;
    private System.Windows.Forms.CheckBox chkFollow_isPartner;
    private System.Windows.Forms.MaskedTextBox txtFollow_PinsMax;
    private System.Windows.Forms.Label label18;
    private System.Windows.Forms.MaskedTextBox txtFollow_PinsMin;
    private System.Windows.Forms.Label label19;
    private System.Windows.Forms.GroupBox grpUnfollowAlgorithm;
    private System.Windows.Forms.MaskedTextBox txtUnfollow_PinsMax;
    private System.Windows.Forms.Label label24;
    private System.Windows.Forms.MaskedTextBox txtUnfollow_PinsMin;
    private System.Windows.Forms.Label label25;
    private System.Windows.Forms.MaskedTextBox txtUnfollow_BoardsMax;
    private System.Windows.Forms.Label label22;
    private System.Windows.Forms.MaskedTextBox txtUnfollow_BoardsMin;
    private System.Windows.Forms.Label label23;
    private System.Windows.Forms.MaskedTextBox txtUnfollow_FollowingMax;
    private System.Windows.Forms.Label label20;
    private System.Windows.Forms.MaskedTextBox txtUnfollow_FollowingMin;
    private System.Windows.Forms.Label label21;
    private System.Windows.Forms.CheckBox chkUnfollow_hasWebsite;
    private System.Windows.Forms.CheckBox chkUnfollow_isPartner;
    private System.Windows.Forms.MaskedTextBox txtUnfollow_FollowersMax;
    private System.Windows.Forms.Label label26;
    private System.Windows.Forms.MaskedTextBox txtUnfollow_FollowersMin;
    private System.Windows.Forms.Label label27;
    private System.Windows.Forms.GroupBox grpUnfollowSettings;
    private System.Windows.Forms.Button btnUnfollowStart;
    private System.Windows.Forms.CheckBox chkFollow_hasTW;
    private System.Windows.Forms.CheckBox chkFollow_hasFB;
    private System.Windows.Forms.GroupBox grpPinURL;
    private System.Windows.Forms.TextBox txtPinURL;
    private System.Windows.Forms.CheckBox chkPinWatermark;
    private System.Windows.Forms.GroupBox grpMainLicense;
    private System.Windows.Forms.Button btnTrial;
    private System.Windows.Forms.Button btnValidate;
    private System.Windows.Forms.Label label29;
    private System.Windows.Forms.TextBox txtTransactionID;
    private System.Windows.Forms.TextBox txtProxyPassword;
    private System.Windows.Forms.Label label35;
    private System.Windows.Forms.Label label34;
    private System.Windows.Forms.TextBox txtProxyUsername;
    private System.Windows.Forms.TextBox txtProxyPort;
    private System.Windows.Forms.Label label33;
    private System.Windows.Forms.TextBox txtProxyIP;
    private System.Windows.Forms.Label label32;
    private System.Windows.Forms.CheckBox chkProxy;
    private System.Windows.Forms.Button btnProxyTest;
    private System.Windows.Forms.Button btnSave;
    private System.Windows.Forms.TabPage Repin_;
    private System.Windows.Forms.GroupBox grpRepinScrapeSettings;
    private System.Windows.Forms.Button btnStartRepinScrape;
    private System.Windows.Forms.TabPage Like_;
    private System.Windows.Forms.GroupBox grpLikeSettings;
    private System.Windows.Forms.Button btnLikeStart;
    private System.Windows.Forms.RadioButton rdbRepinAutopilot;
    private System.Windows.Forms.TabPage Invite_;
    private System.Windows.Forms.Label label49;
    private System.Windows.Forms.ListBox lstInviteBoards;
    private System.Windows.Forms.Button btnInviteStart;
    private System.Windows.Forms.GroupBox grpInviteSettings;
    private System.Windows.Forms.ToolStripSplitButton toolStripSplitButton1;
    private System.Windows.Forms.Button btnPinBoardMapping;
    private System.Windows.Forms.Button btnRepinBoardMapping;
    private System.Windows.Forms.ToolStripStatusLabel lblUpdate;
    private System.Windows.Forms.ToolStripMenuItem clickTheBlueQuestionmarkToOpenTheManualToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem visitWebsiteToolStripMenuItem;
    private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
    private System.Windows.Forms.Button btnRepinSettings;
    private System.Windows.Forms.Button btnPinSettings;
    private System.Windows.Forms.Button btnLikeSettings;
    private System.Windows.Forms.Button btnInviteSettings;
    private System.Windows.Forms.Button btnFollowSettings;
    private System.Windows.Forms.Button btnUnfollowSettings;
    private System.Windows.Forms.Button btnRepinEditUserMapping;
    private System.Windows.Forms.ToolStripStatusLabel tslNotification;
    private System.Windows.Forms.ToolStripMenuItem fAQToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    private System.Windows.Forms.Button btnFollowEditUsers;
    private System.Windows.Forms.Button btnLikeEditCats;
    private System.Windows.Forms.Button btnLikeEditUsers;
    private System.Windows.Forms.Button btnFollowEditCats;
    private System.Windows.Forms.TabPage Account_;
    private System.Windows.Forms.Button btnAccountReloadBoards;
    private System.Windows.Forms.ToolStripMenuItem visitBlogToolStripMenuItem;
    private System.Windows.Forms.CheckBox chkUnfollowNonFollower;
    private System.Windows.Forms.GroupBox grpScrapeSettings;
    private System.Windows.Forms.Button btnScapeBoardMapping;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.TextBox txtScrapeURL;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.NumericUpDown txtNumScrapes;
    private System.Windows.Forms.CheckedListBox clbSources;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.Button btnImportImages;
    private System.Windows.Forms.Button btnManageQueue;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.ToolStripMenuItem whatsNewToolStripMenuItem;
    private System.Windows.Forms.GroupBox grpSourceURL;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.NumericUpDown numPinSourceURL;
    private System.Windows.Forms.TextBox txtPinSourceURL;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.TextBox txtScrapeSourceURL;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.NumericUpDown numScrapeWebsiteInDesc;
    private System.Windows.Forms.NumericUpDown numScrapeSourceURL;
    private System.Windows.Forms.Label label10;
    private System.Windows.Forms.NumericUpDown numPinWebsiteInDesc;
    private System.Windows.Forms.Panel pnlProxy;
    private System.Windows.Forms.CheckBox chkFollowIgnoreCriteria;
    private System.Windows.Forms.RadioButton rdbRepinManual;
    private System.Windows.Forms.Label label11;
    private System.Windows.Forms.NumericUpDown numRepinScrapes;
    private System.Windows.Forms.GroupBox grpRepinSettings;
    private System.Windows.Forms.Button btnStartRepin;
    private System.Windows.Forms.Button btnRepinEditQueue;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.PictureBox pictureBox1;
    private System.Windows.Forms.PictureBox pictureBox9;
    private System.Windows.Forms.PictureBox pictureBox7;
    private System.Windows.Forms.PictureBox pictureBox6;
    private System.Windows.Forms.PictureBox pictureBox5;
    private System.Windows.Forms.PictureBox pictureBox3;
    private System.Windows.Forms.PictureBox pictureBox4;
    private System.Windows.Forms.PictureBox pictureBox8;
    private System.Windows.Forms.RichTextBox stats;
    private System.Windows.Forms.Button btnAccountRefreshStats;
    private System.Windows.Forms.DateTimePicker dtpRepinScheduled;
    private System.Windows.Forms.DateTimePicker dtpLikeScheduled;
    private System.Windows.Forms.RadioButton rdbLikeManual;
    private System.Windows.Forms.DateTimePicker dtpInviteScheduled;
    private System.Windows.Forms.RadioButton rdbInviteManual;
    private System.Windows.Forms.DateTimePicker dtpFollowScheduled;
    private System.Windows.Forms.RadioButton rdbFollowManual;
    private System.Windows.Forms.DateTimePicker dtpUnfollowScheduled;
    private System.Windows.Forms.RadioButton rdbUnfollowManual;
    private System.Windows.Forms.CheckBox chkScheduled;
    private System.Windows.Forms.CheckBox chkContionuousRun;
    private System.Windows.Forms.DateTimePicker tpDelayFrom;
    private System.Windows.Forms.DateTimePicker tpDelayTo;
    private System.Windows.Forms.DateTimePicker tpRepinDelayTo;
    private System.Windows.Forms.DateTimePicker tpRepinDelayFrom;
    private System.Windows.Forms.CheckBox chkRepinContinousRun;
    private System.Windows.Forms.CheckBox chkRepinScheduled;
    private System.Windows.Forms.CheckBox chkLikeScheduled;
    private System.Windows.Forms.DateTimePicker tpLikeDelayTo;
    private System.Windows.Forms.DateTimePicker tpLikeDelayFrom;
    private System.Windows.Forms.CheckBox chkLikeContinuousRun;
    private System.Windows.Forms.CheckBox chkInviteScheduled;
    private System.Windows.Forms.DateTimePicker tpInviteDelayTo;
    private System.Windows.Forms.DateTimePicker tpInviteDelayFrom;
    private System.Windows.Forms.CheckBox chkInviteContinuousRun;
    private System.Windows.Forms.CheckBox chkFollowScheduled;
    private System.Windows.Forms.DateTimePicker tpFollowDelayTo;
    private System.Windows.Forms.DateTimePicker tpFollowDelayFrom;
    private System.Windows.Forms.CheckBox chkFollowContinuousRun;
    private System.Windows.Forms.CheckBox chkUnfollowScheduled;
    private System.Windows.Forms.DateTimePicker tpUnfollowDelayTo;
    private System.Windows.Forms.DateTimePicker tpUnfollowDelayFrom;
    private System.Windows.Forms.CheckBox chkUnfollowContinuousRun;
  }
}

