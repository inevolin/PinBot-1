using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Windows.Forms;

namespace PinBot
{
  public partial class mainForm : Form
  {
    account acc = null;
    WebProxy proxy;
    private scrape sc = null;
    private upload u = null;
    private follow f = null;
    private unfollow uf = null;
    private repin rp = null;
    private like lk = null;
    private invites iv = null;
    WebProxy wp = null;
    public bool TRIAL = true;
    List<TabPage> tabpages = new List<TabPage>();

    bool debug = false;
    string formText = "PinterestBot © healzer.com" + "   " + Application.ProductVersion.ToString();
    string formText_username = "";

    public mainForm()
    {
      //Console.WriteLine(HttpUtility.UrlPathEncode("fashion style"));
      InitializeComponent();
    }
    private void Form1_Load(object sender, EventArgs e)
    {

      Console.WriteLine(HttpUtility.UrlEncode("this is a test"));

      try
      {
        this.Text = formText;
        //grpLogin.Width = 265;
        pnlProxy.Enabled = pnlProxy.Visible = false;

        checkUpdate();
        checkUpdaterUpdate();
        Thread t = new Thread(check_importantMessage);
        t.Start();

        createDB();
        if (!testDB())
        {
          MessageBox.Show("ERROR: DBtest  -  Contact the developer, thank you!", "Critical error", MessageBoxButtons.OK);
          saveSerial();
          Environment.Exit(Environment.ExitCode);
        }


        license();

        PinCheckBoxChanged();
        tmrUpdate.Start();

        loadCredentials();


      }
      catch { MessageBox.Show("Fatal error #MainLoad. Please contact support.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); Environment.Exit(Environment.ExitCode); }

    }
    private void btnTrial_Click(object sender, EventArgs e)
    {
      try
      {
        promo2 p = new promo2();
        //p.Activate();
        p.ShowInTaskbar = true;
        p.StartPosition = FormStartPosition.CenterScreen;
        if (!p.OK)
          p.ShowDialog();
        if (!p.OK)
        {
          MessageBox.Show("You must register to use PinBot Trial", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Information);
          return;
        }

        trial();

        grpMainLicense.Visible = false;
        grpLogin.Visible = true;

        formText += "    TRIAL";
        this.Text = formText;
      }
      catch (Exception ex) { MessageBox.Show("Something went wrong. Please contact support #101T", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }
    }
    void license()
    {
      //removing tabs
      foreach (TabPage tp in tabs.TabPages)
      {
        tabpages.Add(tp);
      }
      tabs.TabPages.Clear();
      tabs.TabPages.Add(tabpages[0]);

      try
      {
        //read license
        Microsoft.Win32.RegistryKey key;
        key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("PinBotLicense", true);
        if (key == null)
          key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("PinBotLicense");
        else
          txtTransactionID.Text = key.GetValue("tid") == null ? "" : key.GetValue("tid").ToString();
        if (txtTransactionID.Text.Length > 0)
        {
          btnValidate.PerformClick();
        }
        key.Close();
      }
      catch (Exception ex)
      {
        MessageBox.Show("Fatal error #RegKey118. Please contact support.\n\n" + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        Environment.Exit(Environment.ExitCode);
      }

      if (TRIAL)
      {

        //promo2 p2 = new promo2();
        //p2.TopMost = true;
        //p2.ShowDialog();

        promo p = new promo();
        p.TopMost = true;
        p.ShowDialog();

      }

    }
    private void btnValidate_Click(object sender, EventArgs e)
    {
      try
      {
        if (txtTransactionID.Text.Length <= 0)
        {
          MessageBox.Show("License empty", "License error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
          return;
        }

        string hardwareID = account.hardwareID();
        try
        {
          Microsoft.Win32.RegistryKey key;
          key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("PinBotHID", true);
          if (key == null)
          {
            key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("PinBotHID");
            key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("PinBotHID", true);
            key.SetValue("PinBotHID", hardwareID);
          }
          else
          {
            hardwareID = key.GetValue("PinBotHID").ToString();
            key.Close();
          }
        }
        catch { }


        string TID = txtTransactionID.Text.Replace("'", "").Replace("?", "").Replace("&", "").Replace(":", "");


        string RS = http.GET("http://healzer.com/pinbot/check3.php?tid=" + TID + "&hid=" + hardwareID, "", null, null, null).Trim();


        MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

        byte[] raw_input = Encoding.UTF32.GetBytes(hardwareID);
        byte[] raw_output = md5.ComputeHash(raw_input);
        string md5_hid = "";
        foreach (byte myByte in raw_output)
          md5_hid += myByte.ToString("X2");


        if (RS.Equals(md5_hid, StringComparison.InvariantCultureIgnoreCase))
        {
          try
          {
            Microsoft.Win32.RegistryKey key;
            key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("PinBotLicense");
            key.SetValue("tid", TID);
            key.Close();
          }
          catch (Exception ex)
          {
            MessageBox.Show("Fatal error #RegKey175. Please contact support.\n\n" + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            Environment.Exit(Environment.ExitCode);
          }

          TRIAL = false; premium();

          tabs.TabPages.Clear();
          foreach (TabPage tp in tabpages)
          {
            tabs.TabPages.Add(tp);
          }

          grpMainLicense.Visible = false;
          grpLogin.Visible = true;
        }
        else
        {
          try
          {
            Microsoft.Win32.RegistryKey key;
            key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("PinBotLicense", true);
            if (key == null)
            {
              key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("PinBotLicense");
              key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("PinBotLicense", true);
            }
            if (key.GetValue("tid") != null)
            {
              key.DeleteValue("tid");
              key.Close();
            }
            MessageBox.Show(RS, "License error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
          }
          catch (Exception ex)
          {
            MessageBox.Show("Fatal error #RegKey210. Please contact support.\n\n" + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            Environment.Exit(Environment.ExitCode);
          }
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show("Fatal error #bVal161. Please contact support.\n\n" + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        Environment.Exit(Environment.ExitCode);
      }
    }
    void premium()
    {
      btnProxyTest.Enabled = chkProxy.Enabled = true;
      this.toolTip1.SetToolTip(this.chkProxy, "");
      txtProxyIP.Enabled = txtProxyPort.Enabled = txtProxyUsername.Enabled = txtProxyPassword.Enabled = true;

      //grpLogin.Width = 265;
      pnlProxy.Enabled = pnlProxy.Visible = false;

      chkPinWatermark.Enabled = true;

      for (int i = 0; i < clbSources.Items.Count; i++)
        clbSources.SetItemChecked(i, true);
    }
    void trial()
    {
      TRIAL = true;

      tabs.TabPages.Clear();
      foreach (TabPage tp in tabpages)
      {
        if (tp.Name.Equals("Like_") || tp.Name.Equals("Repin_") || tp.Name.Equals("Invite_")) continue;

        tabs.TabPages.Add(tp);
      }

      clbSources.Enabled = false;
      clbSources.SetItemChecked(0, true);

      chkUnfollowNonFollower.Enabled = false;

      grpSourceURL.Enabled = txtScrapeSourceURL.Enabled = numScrapeSourceURL.Enabled = false;
    }

    //MAIN
    private string status;
    void createDB()
    {
      try
      {
        if (!File.Exists(Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory + "/db")))
          SQLiteConnection.CreateFile(Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory + "/db"));


        SQLiteConnection m_dbConnection = new SQLiteConnection(@"Data Source=db;Version=3;New=False;Compress=True;");
        m_dbConnection.Open();
        string sql = "CREATE TABLE IF NOT EXISTS pins (path text, category text, description text, tags text, boardID text)";
        SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
        command.ExecuteNonQuery();


        sql = "CREATE TABLE IF NOT EXISTS acc (email text PRIMARY KEY, pass text, proxy text, ip text, port text, p_login text, p_pass text)";
        command = new SQLiteCommand(sql, m_dbConnection);
        command.ExecuteNonQuery();

        try
        {
          sql = "ALTER TABLE pins ADD COLUMN source_url text";
          command = new SQLiteCommand(sql, m_dbConnection);
          command.ExecuteNonQuery();
        }
        catch { }

        sql = "CREATE TABLE IF NOT EXISTS repins (pin text, ID INTEGER PRIMARY KEY AUTOINCREMENT)";
        command = new SQLiteCommand(sql, m_dbConnection);
        command.ExecuteNonQuery();

        m_dbConnection.Close();

      }
      catch (Exception ex) { if (debug) MessageBox.Show(ex.StackTrace); }
    }
    bool testDB()
    {
      try
      {
        SQLiteConnection m_dbConnection = new SQLiteConnection(@"Data Source=db;Version=3;New=False;Compress=True;");
        m_dbConnection.Open();
        string sql = "select count(*) as c from pins";
        SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
        SQLiteDataReader sqlreader = command.ExecuteReader();
        string i = "";
        while (sqlreader.Read())
        {
          i = sqlreader["c"].ToString();
        }
        m_dbConnection.Close();
        return true;
      }
      catch (Exception ex) { if (debug) MessageBox.Show(ex.StackTrace); return false; }
    }
    bool loginFromSerial()
    {

      if (File.Exists("account.ser"))
      {
        try
        {
          serializer sr = new serializer();
          try
          {
            acc = sr.DeSerializeObject("account.ser");
            acc.redef();

            // this.Cursor = Cursors.WaitCursor;
            // acc.loadBoards();
            // fillBoards();
            // this.Cursor = Cursors.Default;

            if (proxy != null) acc.proxy = proxy;
          }
          catch (SerializationException)
          {
            acc = new account();
            if (proxy != null) acc.proxy = proxy;
            return false;
          }

          //if (acc.VERSION == null)
          //{ acc = new account(); return false; }

          dtpRepinScheduled.Value = acc.scheduledRepin;
          dtpScheduled.Value = acc.scheduledPin;
          dtpLikeScheduled.Value = acc.scheduledLike;
          dtpInviteScheduled.Value = acc.scheduledInvite;
          dtpFollowScheduled.Value = acc.scheduledFollow;
          dtpUnfollowScheduled.Value = acc.scheduledUnfollow;

          txtScrapeURL.Text = acc.txtScrapeWebsiteURL == null ? "" : acc.txtScrapeWebsiteURL;
          txtPinURL.Text = acc.txtPinWebsiteURL == null ? "" : acc.txtPinWebsiteURL;
          txtPinSourceURL.Text = acc.txtPinSourceURL == null ? "" : acc.txtPinSourceURL;
          txtScrapeSourceURL.Text = acc.txtScrapeSourceURL == null ? "" : acc.txtScrapeSourceURL;

          numScrapeWebsiteInDesc.Value = acc.numScrapeWebsiteInDesc < 0 ? 10 : acc.numScrapeWebsiteInDesc;
          numPinWebsiteInDesc.Value = acc.numPinWebsiteInDesc < 0 ? 10 : acc.numPinWebsiteInDesc;
          numPinSourceURL.Value = acc.numPinSourceURL < 0 ? 10 : acc.numPinSourceURL;
          numScrapeSourceURL.Value = acc.numScrapeSourceURL < 0 ? 10 : acc.numScrapeSourceURL;

          numRepinScrapes.Value = acc.numRepinScrapes < 1 ? 20 : acc.numRepinScrapes;

          follow_unfollow_settings();

          if (txtEmail.Text.Length > 0 && !txtEmail.Text.Trim().Equals(acc.login) && txtPassword.Text.Length > 0 && !txtPassword.Text.Trim().Equals(acc.password))
          {
            acc = new account();
            if (proxy != null) acc.proxy = proxy;
            return false;
          }
          else if (txtEmail.Text.Length > 0 && !txtEmail.Text.Trim().Equals(acc.login))
          {
            acc = new account();
            if (proxy != null) acc.proxy = proxy;
            return false;
          }
          else if (txtPassword.Text.Length > 0 && !txtPassword.Text.Trim().Equals(acc.password))
            btnSave.PerformClick();

          if (!acc.lastLoginProxy && proxy != null)
            return false;


          if (DateTime.Now.Subtract(acc.lastLogin).Hours > 3)
            return false;

          return acc.Username.Length > 0;

          //return true;
        }
        catch (Exception ex)
        {
          if (debug) MessageBox.Show(ex.StackTrace);
          acc = new account();
          if (proxy != null) acc.proxy = proxy;
        }
      }
      else
      {
        acc = new account();
        if (proxy != null) acc.proxy = proxy;
        acc.redef();
      }
      return false;
    }
    void follow_unfollow_settings()
    {
      if (acc.follow_settings != null)
      {
        try
        {
          txtFollow_FollowersMin.Text = acc.follow_settings.followersMin.ToString().Equals("0") ? "50" : acc.follow_settings.followersMin.ToString();
          txtFollow_FollowersMax.Text = acc.follow_settings.followersMax.ToString().Equals("0") ? "5000" : acc.follow_settings.followersMax.ToString();

          txtFollow_FollowingMin.Text = acc.follow_settings.followingMin.ToString().Equals("0") ? "50" : acc.follow_settings.followingMin.ToString();
          txtFollow_FollowingMax.Text = acc.follow_settings.followingMax.ToString().Equals("0") ? "5000" : acc.follow_settings.followingMax.ToString();

          txtFollow_BoardsMin.Text = acc.follow_settings.boardsMin.ToString().Equals("0") ? "5" : acc.follow_settings.boardsMin.ToString();
          txtFollow_BoardsMax.Text = acc.follow_settings.boardsMax.ToString().Equals("0") ? "100" : acc.follow_settings.boardsMax.ToString();

          txtFollow_PinsMin.Text = acc.follow_settings.pinsMin.ToString().Equals("0") ? "200" : acc.follow_settings.pinsMin.ToString();
          txtFollow_PinsMax.Text = acc.follow_settings.pinsMax.ToString().Equals("0") ? "5000" : acc.follow_settings.pinsMax.ToString();

          chkFollow_isPartner.Checked = acc.follow_settings.isPartner;
          chkFollow_hasWebsite.Checked = acc.follow_settings.hasWebsite;
          chkFollow_hasTW.Checked = acc.follow_settings.hasTW;
          chkFollow_hasFB.Checked = acc.follow_settings.hasFB;

          chkFollowIgnoreCriteria.Checked = acc.follow_settings.ignoreCriteria;
        }
        catch { }
      }

      if (acc.unfollow_settings != null)
      {
        try
        {
          txtUnfollow_FollowersMin.Text = acc.unfollow_settings.followersMin.ToString().Equals("0") ? "50" : acc.unfollow_settings.followersMin.ToString();
          txtUnfollow_FollowersMax.Text = acc.unfollow_settings.followersMax.ToString().Equals("0") ? "5000" : acc.unfollow_settings.followersMax.ToString();

          txtUnfollow_FollowingMin.Text = acc.unfollow_settings.followingMin.ToString().Equals("0") ? "50" : acc.unfollow_settings.followingMin.ToString();
          txtUnfollow_FollowingMax.Text = acc.unfollow_settings.followingMax.ToString().Equals("0") ? "5000" : acc.unfollow_settings.followingMax.ToString();

          txtUnfollow_BoardsMin.Text = acc.unfollow_settings.boardsMin.ToString().Equals("0") ? "50" : acc.unfollow_settings.boardsMin.ToString();
          txtUnfollow_BoardsMax.Text = acc.unfollow_settings.boardsMax.ToString().Equals("0") ? "100" : acc.unfollow_settings.boardsMax.ToString();

          txtUnfollow_PinsMin.Text = acc.unfollow_settings.pinsMin.ToString().Equals("0") ? "200" : acc.unfollow_settings.pinsMin.ToString();
          txtUnfollow_PinsMax.Text = acc.unfollow_settings.pinsMax.ToString().Equals("0") ? "5000" : acc.unfollow_settings.pinsMax.ToString();

          chkUnfollow_isPartner.Checked = acc.unfollow_settings.isPartner;
          chkUnfollow_hasWebsite.Checked = acc.unfollow_settings.hasWebsite;
          chkUnfollowNonFollower.Checked = acc.unfollow_settings.notFollowingYou;
        }
        catch { }
      }
    }
    void save_follow_unfollow_settings()
    {
      if (acc == null)
        return;

      if (acc.follow_settings != null)
      {
        try
        {
          acc.follow_settings.followersMin = int.Parse(txtFollow_FollowersMin.Text);
          acc.follow_settings.followersMax = int.Parse(txtFollow_FollowersMax.Text);

          acc.follow_settings.followingMin = int.Parse(txtFollow_FollowingMin.Text);
          acc.follow_settings.followingMax = int.Parse(txtFollow_FollowingMax.Text);

          acc.follow_settings.boardsMin = int.Parse(txtFollow_BoardsMin.Text);
          acc.follow_settings.boardsMax = int.Parse(txtFollow_BoardsMax.Text);

          acc.follow_settings.pinsMin = int.Parse(txtFollow_PinsMin.Text);
          acc.follow_settings.pinsMax = int.Parse(txtFollow_PinsMax.Text);

          acc.follow_settings.isPartner = chkFollow_isPartner.Checked;
          acc.follow_settings.hasWebsite = chkFollow_hasWebsite.Checked;
          acc.follow_settings.hasTW = chkFollow_hasTW.Checked;
          acc.follow_settings.hasFB = chkFollow_hasFB.Checked;

          acc.follow_settings.ignoreCriteria = chkFollowIgnoreCriteria.Checked;
        }
        catch { }
      }

      if (acc.unfollow_settings != null)
      {
        try
        {
          acc.unfollow_settings.followersMin = int.Parse(txtUnfollow_FollowersMin.Text);
          acc.unfollow_settings.followersMax = int.Parse(txtUnfollow_FollowersMax.Text);

          acc.unfollow_settings.followingMin = int.Parse(txtUnfollow_FollowingMin.Text);
          acc.unfollow_settings.followingMax = int.Parse(txtUnfollow_FollowingMax.Text);

          acc.unfollow_settings.boardsMin = int.Parse(txtUnfollow_BoardsMin.Text);
          acc.unfollow_settings.boardsMax = int.Parse(txtUnfollow_BoardsMax.Text);

          acc.unfollow_settings.pinsMin = int.Parse(txtUnfollow_PinsMin.Text);
          acc.unfollow_settings.pinsMax = int.Parse(txtUnfollow_PinsMax.Text);

          acc.unfollow_settings.isPartner = chkUnfollow_isPartner.Checked;
          acc.unfollow_settings.hasWebsite = chkUnfollow_hasWebsite.Checked;
          acc.unfollow_settings.notFollowingYou = chkUnfollowNonFollower.Checked;
        }
        catch { }
      }
    }
    private void btnLogin_Click(object sender, EventArgs e)
    {

      this.Cursor = Cursors.WaitCursor;
      // txtPassword.Text = txtPassword;

      if (this.acc == null)
      {

        btnLogin.Enabled = false;
        status = lblStatus1.Text = "Connecting...";

        if (!loginFromSerial())
        {
          if (proxy != null) acc.proxy = proxy;
          else if (acc.lastLoginProxy)
          {
            if (MessageBox.Show("Previously you logged in using a Proxy, are you sure you wish to proceed without Proxy?", "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning) != DialogResult.Yes)
              return;
          }
          acc.Connected = false; acc.Error = null;
          acc.log_in(txtEmail.Text, txtPassword.Text);
        }

        while (!acc.Connected && acc.Error == null)
        {
          Application.DoEvents();
        }
        if (acc.Error != null)
        {
          btnLogin.Enabled = true;
          status = lblStatus1.Text = acc.Error;
          lblStatus1.ForeColor = Color.Red;
          this.acc = null;
        }
        else
        {
          btnLogin.Enabled = true;
          status = lblStatus1.Text = "Connected";
          lblStatus1.ForeColor = Color.Green;
          btnLogin.Text = "Log out";

          txtPassword.Enabled = false;
          txtEmail.Enabled = false;
          chkProxy.Enabled = false;
          pnlProxy.Enabled = false;

          fillBoards();
          tmrStatus.Enabled = true;
          saveSerial();

          formText_username = "            | " + acc.Username;
          this.Text = formText + formText_username;
        }

      }
      else
      {

        stopFollow();
        stopUnfollow();
        stopInvite();
        stopLike();
        stopRepin();
        stopPin();
        stopScrape();

        saveSerial();

        this.acc = null;
        lblStatus1.Text = status = "Logged out";
        lblStatus1.ForeColor = Color.Black;
        btnLogin.Text = "Log in";

        txtPassword.Enabled = true;
        txtEmail.Enabled = true;
        chkProxy.Enabled = true;
        pnlProxy.Enabled = true;

        tmrStatus.Enabled = false;

        formText_username = "";
        this.Text = formText;

      }
      this.Cursor = Cursors.Default;

    }
    void fillBoards()
    {
      //Invites
      lstInviteBoards.DataSource = new BindingSource(acc.boards, null);
      lstInviteBoards.DisplayMember = "Value";
      lstInviteBoards.ValueMember = "Key";


    }
    private void btnProxyTest_Click(object sender, EventArgs e)
    {
      try
      {
        wp = new WebProxy(txtProxyIP.Text.Trim(), int.Parse(txtProxyPort.Text.Trim()));
        // wp = new WebProxy();
        // Uri uri = new Uri("http://" + txtProxyIP.Text.Trim() + ":" + txtProxyPort.Text.Trim());
        // wp.Address = uri;

        if (txtProxyUsername.Text.Length > 0 && txtProxyPassword.Text.Length > 0)
          wp.Credentials = new NetworkCredential(txtProxyUsername.Text.Trim(), txtProxyPassword.Text.Trim());
        this.Cursor = Cursors.WaitCursor;

        proxy = wp;



        //string RS2 = http.GET("http://www.google.com/", "", new CookieContainer(), null, acc.proxy).Replace(",", "");


        string RS = http.GET("https://www.pinterest.com/", "", new CookieContainer(), null, proxy).Replace(",", "");
        //File.WriteAllText("./debug.txt", RS);

        this.Cursor = Cursors.Default;
        if (!RS.Contains("title=\"Pinterest\""))
        {
          RS = http.GET("http://www.pinterest.com/", "", new CookieContainer(), null, proxy).Replace(",", "");

          if (!RS.Contains("Sign up to discover ideas for all your projects and interests"))
          {
            proxy = null;
            MessageBox.Show("Proxy does not work.\nRe-check the IP, port (and credentials).\nContact your proxy provider if the issue persists.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
          }

        }

        proxy = wp;
        btnLogin.Enabled = true;
        MessageBox.Show("Proxy working!", "SUCCESS", MessageBoxButtons.OK, MessageBoxIcon.Information);

      }
      catch (Exception ex) { proxy = null; MessageBox.Show(ex.Message + Environment.NewLine + "Something went wrong.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }
    }
    private void chkProxy_CheckedChanged(object sender, EventArgs e)
    {
      if (TRIAL && chkProxy.Checked)
      {
        //MessageBox.Show("Sorry, this feature is for Premium only.", "Premium only", MessageBoxButtons.OK);
        chkProxy.Checked = false;
        pnlProxy.Enabled = pnlProxy.Visible = false;
        return;
      }
      else if (TRIAL)
      {
        return;
      }
      //MessageBox.Show("In development mode, will be available very soon!");
      //return;
      proxy = null;
      if (chkProxy.Checked)
      {
        //grpLogin.Width = 550;
        pnlProxy.Enabled = pnlProxy.Visible = true;
        btnLogin.Enabled = false;
      }
      else
      {
        //grpLogin.Width = 265;
        pnlProxy.Enabled = pnlProxy.Visible = false;
        btnLogin.Enabled = true;
      }
    }
    private void btnSave_Click(object sender, EventArgs e)
    {
      this.Cursor = Cursors.WaitCursor;

      try
      {
        SQLiteConnection m_dbConnection = new SQLiteConnection(@"Data Source=db;Version=3;New=False;Compress=True;");
        m_dbConnection.Open();
        string sql = "CREATE TABLE IF NOT EXISTS acc (email text PRIMARY KEY, pass text, proxy text, ip text, port text, p_login text, p_pass text)";
        SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
        command.ExecuteNonQuery();

        sql = "delete from acc";
        command = new SQLiteCommand(sql, m_dbConnection);
        command.ExecuteNonQuery();

        sql = "insert or replace into  acc  (email, pass, proxy, ip, port, p_login, p_pass) values ('" + txtEmail.Text.Trim() + "','" + txtPassword.Text.Trim() + "','" + chkProxy.Checked + "','" + txtProxyIP.Text.Trim() + "','" + txtProxyPort.Text.Trim() + "','" + txtProxyUsername.Text.Trim() + "','" + txtProxyPassword.Text.Trim() + "')";
        command = new SQLiteCommand(sql, m_dbConnection);
        command.ExecuteNonQuery();

        m_dbConnection.Close();

        lblStatus1.Text = "Saved!";
      }
      catch (Exception ex) { if (debug) MessageBox.Show(ex.StackTrace); lblStatus1.Text = "ERROR saving! Contact support."; }

      this.Cursor = Cursors.Default;
    }
    void loadCredentials()
    {
      SQLiteConnection m_dbConnection = new SQLiteConnection(@"Data Source=db;Version=3;New=False;Compress=True;");
      m_dbConnection.Open();
      string sql = "select email, pass, proxy, ip, port, p_login, p_pass from acc LIMIT 1 ";
      SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
      SQLiteDataReader sqlreader = command.ExecuteReader();
      while (sqlreader.Read())
      {
        txtEmail.Text = sqlreader["email"].ToString();
        txtPassword.Text = sqlreader["pass"].ToString();
        chkProxy.Checked = bool.Parse(sqlreader["proxy"].ToString());
        txtProxyIP.Text = sqlreader["ip"].ToString();
        txtProxyPort.Text = sqlreader["port"].ToString();
        txtProxyUsername.Text = sqlreader["p_login"].ToString();
        txtProxyPassword.Text = sqlreader["p_pass"].ToString();
      }
      m_dbConnection.Close();
    }

    //General
    private void tmrStatus_Tick(object sender, EventArgs e)
    {
      string s = status + "    |    ";
      try
      {
        if (u != null)
          s += u.ToString() + "    |    ";
        if (sc != null)
          s += sc.ToString() + "    |    ";
        if (f != null)
          s += f.ToString() + "    |    ";
        if (uf != null)
          s += uf.ToString() + "    |    ";
        if (rp != null)
          s += rp.ToString() + "    |    ";
        if (lk != null)
          s += lk.ToString() + "    |    ";
        if (iv != null)
          s += iv.ToString() + "    |    ";
      }
      catch (Exception ex) { if (debug) MessageBox.Show(ex.StackTrace); }
      lblStatus1.Text = s;

      try
      {
        if (acc != null && acc.Connected)
        {
          stats.Text = "";
          stats.Text += "Followers: " + acc.stats.followers + Environment.NewLine;
          stats.Text += "Following: " + acc.stats.following + Environment.NewLine;
          stats.Text += "Likes: " + acc.stats.likes + Environment.NewLine;
          stats.Text += "Pins: " + acc.stats.pins + Environment.NewLine;
          stats.Text += "Boards: " + acc.stats.boards;
        }
      }
      catch { }
    }
    private void tmrUpdate_Tick(object sender, EventArgs e)
    {
      Thread t = new Thread(checkUpdate);
      t.Start();
    }
    void checkUpdate()
    {
      try
      {
        string pv = Application.ProductVersion.ToString().Replace(".", "");
        string cpv = http.GET("http://healzer.com/pinbot/update.txt", "", null, null, null).Replace(".", "");

        if (int.Parse(cpv) > int.Parse(pv))
        {
          tmrUpdate.Enabled = false;
          lblUpdate.Visible = true;
        }
      }
      catch (Exception ex)
      {
        //MessageBox.Show("Error checking for update: Closing now...", "ERROR");
        //saveSerial();
        //Environment.Exit(Environment.ExitCode); 
      }
    }
    void checkUpdaterUpdate()
    {

      try
      {
        if (!File.Exists("./updater.exe"))
        {
          using (WebClient client = new WebClient())
          {
            client.DownloadFile("http://healzer.com/pinbot/updater.exe", "./updater.exe");
          }
        }
        string pv = FileVersionInfo.GetVersionInfo("./updater.exe").ProductVersion;
        string cpv = http.GET("http://healzer.com/pinbot/updater.txt", "", null, null, null);

        if (int.Parse(cpv.Replace(".", "")) > int.Parse(pv.Replace(".", "")))
        {
          int attempts = 0;
          while (File.Exists("./updater.exe"))
          {
            try
            {
              foreach (var process in Process.GetProcessesByName("updater"))
              {
                process.Kill();
              }
              File.Delete("./updater.exe");
              ++attempts;
            }
            catch (Exception ex) { if (debug) MessageBox.Show(ex.StackTrace); }
            if (attempts > 5) throw new Exception();
          }
          using (WebClient client = new WebClient())
          {
            client.DownloadFile("http://healzer.com/pinbot/updater.exe", "./updater.exe");
          }
        }
      }
      catch (Exception ex)
      {
        //if (debug) MessageBox.Show(ex.StackTrace);
        // saveSerial();
        //Environment.Exit(Environment.ExitCode);
      }

    }
    private void mainForm_FormClosing(object sender, FormClosingEventArgs e)
    {
      saveSerial();

      if (TRIAL)
      {
        promo p = new promo();
        p.TopMost = true;
        p.ShowDialog();
      }

      Environment.Exit(Environment.ExitCode);
    }
    void saveSerial()
    {
      if (this.acc != null)
        this.acc.VERSION = Application.ProductVersion.Replace(".", "");
      save_follow_unfollow_settings();
      serializer sr = new serializer();
      sr.SerializeObject("account.ser", this.acc);
    }
    bool isValidURL(string url)
    {
      bool valid = false;
      if (!url.Contains("http://") && !url.Contains("https://"))
        url = "http://" + url;
      try
      {
        var request = WebRequest.Create(url) as HttpWebRequest;
        request.Method = "HEAD";
        using (var response = (HttpWebResponse)request.GetResponse())
        {
          valid = response.StatusCode == HttpStatusCode.OK;
        }
      }
      catch
      {
        valid = false;
      }

      return valid;
    }

    //Pin
    private void btnPinStart_Click(object sender, EventArgs e)
    {
      if (btnPinStart.Text == "start")
      {
        if (acc == null || !acc.Connected)
        {
          this.Cursor = Cursors.Default;
          MessageBox.Show("Please login first", "Warning", MessageBoxButtons.OK);
          return;
        }
        if (acc.boards.Count <= 0)
        {
          this.Cursor = Cursors.Default;
          MessageBox.Show("Your account has no boards.", "Warning", MessageBoxButtons.OK);
          return;
        }
        if (txtPinSourceURL.Text.Length > 0)
        {
          this.Cursor = Cursors.WaitCursor;
          if (!isValidURL(txtPinSourceURL.Text))
          {
            this.Cursor = Cursors.Default;
            MessageBox.Show("Invalid Source URL", "Warning", MessageBoxButtons.OK);
            txtPinSourceURL.SelectAll();
            return;
          }
          this.Cursor = Cursors.Default;
        }

        if (chkScheduled.Checked && dtpScheduled.Value <= DateTime.Now)
        {
          MessageBox.Show("Please select a future date/time", "Warning", MessageBoxButtons.OK);
          return;
        }

        if (chkAutopilot.Checked)
        {

          if (acc.boards_category_mapped_pin.SelectMany(x => x.Value).Count() <= 0)
          {
            MessageBox.Show("No boards mapped to category", "Warning", MessageBoxButtons.OK);
            return;
          }


          try
          {
            SQLiteConnection m_dbConnection = new SQLiteConnection(@"Data Source=db;Version=3;New=False;Compress=True;");
            m_dbConnection.Open();
            string sql = "select count(*) as c from pins";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader sqlreader = command.ExecuteReader();
            string i = "";
            while (sqlreader.Read())
            {
              i = sqlreader["c"].ToString();
            }
            m_dbConnection.Close();
            if (int.Parse(i) > 0)
            {
              if (MessageBox.Show("The queue contains images which will be pinned first.\nDo you wish to continue?", "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            }
          }
          catch { }



          stopScrape();
          sc = new scrape(acc.setting_pin.Max, acc, watermark_settings, txtPinURL.Text, txtPinSourceURL.Text, true, TRIAL);
          sc.start();


        }
        grpPinURL.Enabled = false;
        btnPinBoardMapping.Enabled = false;
        grpPinSettings.Enabled = false;

        u = chkScheduled.Checked
              ? new upload(acc,
                           new ContinuousSettings
                           {
                             ContinuousRun = chkContionuousRun.Checked,
                             DelayFrom =
                               new TimeSpan(tpDelayFrom.Value.Hour, tpDelayFrom.Value.Minute, 0),
                             DelayTo =
                               new TimeSpan(tpDelayTo.Value.Hour, tpDelayTo.Value.Minute, 0)
                           },
                           dtpScheduled.Value)
              : new upload(acc,
                           new ContinuousSettings
                           {
                             ContinuousRun = chkContionuousRun.Checked,
                             DelayFrom =
                               new TimeSpan(tpDelayFrom.Value.Hour, tpDelayFrom.Value.Minute, 0),
                             DelayTo =
                               new TimeSpan(tpDelayTo.Value.Hour, tpDelayTo.Value.Minute, 0)
                           });
        u.start_thread();

        btnPinStart.Text = "stop";
      }
      else
      {
        stopPin();
      }
    }
    void stopPin()
    {
      btnPinStart.Enabled = false;
      if (sc != null)
      {
        if (sc.autoPilot)
        {
          stopScrape();
        }
      }

      if (u != null)
      {
        u.Abort = true;
        while (u.Active)
          Application.DoEvents();
        u = null;
      }

      grpPinURL.Enabled = true;
      btnPinBoardMapping.Enabled = true;
      grpPinSettings.Enabled = true;

      btnPinStart.Enabled = true;
      btnPinStart.Text = "start";
    }
    private void chkManual_CheckedChanged(object sender, EventArgs e)
    {
      PinCheckBoxChanged();
    }

    private void chkScheduled_CheckedChanged(object sender, EventArgs e)
    {
      dtpScheduled.Visible = chkScheduled.Checked;
    }

    private void chkContionuousRun_CheckedChanged(object sender, EventArgs e)
    {
      tpDelayFrom.Visible = chkContionuousRun.Checked;
      tpDelayTo.Visible = chkContionuousRun.Checked;
    }

    private void chkAutopilot_CheckedChanged(object sender, EventArgs e)
    {
      PinCheckBoxChanged();
    }

    private void PinCheckBoxChanged()
    {
      if (chkManual.Checked)
      {
        grpPinURL.Visible = grpSourceURL.Visible = chkPinWatermark.Visible = false;
        btnPinBoardMapping.Visible = false;
      }
      else if (chkAutopilot.Checked)
      {
        btnPinBoardMapping.Visible = true;
        grpPinURL.Visible = grpSourceURL.Visible = chkPinWatermark.Visible = true;
      }

    }

    private void btnPinBoardMapping_Click(object sender, EventArgs e)
    {
      if (acc == null || !acc.Connected)
      {
        MessageBox.Show("Please login first", "Warning", MessageBoxButtons.OK);
        return;
      }
      if (acc.boards.Count <= 0)
      {
        MessageBox.Show("Your account has no boards.", "Warning", MessageBoxButtons.OK);
        return;
      }
      BoardMapping bm = new BoardMapping(acc, acc.boards_category_mapped_pin);
      bm.ShowDialog();
      acc.boards_category_mapped_pin = bm.mapped;
    }
    private void btnPinClearQueue_Click(object sender, EventArgs e)
    {

    }
    private Watermark watermark_settings = new Watermark();
    private void chkPinWatermark_CheckedChanged(object sender, EventArgs e)
    {
      if (chkPinWatermark.Checked)
      {
        watermark_settings.ShowDialog();
        if (!(watermark_settings.image != null && watermark_settings.positions.Count > 0))
        {
          MessageBox.Show("Watermark settings are invalid,\nno watermark will be used.", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
          chkPinWatermark.Checked = false;
        }

      }
    }
    private void txtPinDescURL_TextChanged(object sender, EventArgs e)
    {
      if (acc != null)
        acc.txtPinWebsiteURL = txtPinURL.Text;
    }
    private void btnPinSettings_Click(object sender, EventArgs e)
    {
      if (acc == null || !acc.Connected)
      {
        MessageBox.Show("Please login first", "Warning", MessageBoxButtons.OK);
        return;
      }
      SettingsForm s = new SettingsForm(acc.setting_pin, "Pins max:");
      s.ShowDialog();
      acc.setting_pin = s.s;
    }
    private void txtPinSourceURL_TextChanged(object sender, EventArgs e)
    {
      if (acc != null)
        acc.txtPinSourceURL = txtPinSourceURL.Text;
    }
    private void numPinSourceURL_ValueChanged(object sender, EventArgs e)
    {
      if (acc != null)
        acc.numPinSourceURL = (int)numPinSourceURL.Value;
    }
    private void numPinWebsiteInDesc_ValueChanged(object sender, EventArgs e)
    {
      if (acc != null)
        acc.numPinWebsiteInDesc = (int)numPinWebsiteInDesc.Value;
    }


    //Scrape
    private void btnScrapeStart_Click(object sender, EventArgs e)
    {
      if (acc == null || !acc.Connected)
      {
        MessageBox.Show("Please login first", "Warning", MessageBoxButtons.OK);
        return;
      }
      if (acc.boards.Count <= 0)
      {
        MessageBox.Show("Your account has no boards.", "Warning", MessageBoxButtons.OK);
        return;
      }

      if (clbSources.CheckedItems.Count <= 0)
      {
        MessageBox.Show("No sources selected.", "Warning", MessageBoxButtons.OK);
        return;
      }

      if (acc.boards_category_mapped_pin.SelectMany(x => x.Value).Count() <= 0)
      {
        MessageBox.Show("No categories mapped onto boards.", "Warning", MessageBoxButtons.OK);
        return;
      }
      if (txtPinSourceURL.Text.Length > 0)
      {
        this.Cursor = Cursors.WaitCursor;
        if (!isValidURL(txtPinSourceURL.Text))
        {
          this.Cursor = Cursors.Default;
          MessageBox.Show("Invalid Source URL", "Warning", MessageBoxButtons.OK);
          txtPinSourceURL.SelectAll();
          return;
        }
        this.Cursor = Cursors.Default;
      }

      btnScrapeStart.Enabled = false;
      if (btnScrapeStart.Text == "start") //starting
      {
        grpScrapeSettings.Enabled = false;
        stopScrape();

        sc = new scrape((int)txtNumScrapes.Value, acc, watermark_settings, txtScrapeURL.Text, txtScrapeSourceURL.Text, false, TRIAL, clbSources.CheckedItems.Count);
        foreach (Object item in clbSources.CheckedItems)
        {
          if (item.ToString().Equals("WeHeartIt"))
            sc.weheartit = true;
          else if (item.ToString().Equals("Imgfave"))
            sc.imgfave = true;
        }
        sc.start();

        btnScrapeStart.Text = "stop";
      }
      else //stopping
      {
        stopScrape();
        btnScrapeStart.Text = "start";
        grpScrapeSettings.Enabled = true;
      }
      btnScrapeStart.Enabled = true;

    }
    void stopScrape()
    {
      if (sc != null)
      {
        sc.stop();
        while (sc.active)
        {
          Application.DoEvents();
        }
        sc = null;
      }
    }
    private void btnScapeBoardMapping_Click(object sender, EventArgs e)
    {
      if (acc == null || !acc.Connected)
      {
        MessageBox.Show("Please login first", "Warning", MessageBoxButtons.OK);
        return;
      }
      if (acc.boards.Count <= 0)
      {
        MessageBox.Show("Your account has no boards.", "Warning", MessageBoxButtons.OK);
        return;
      }
      BoardMapping bm = new BoardMapping(acc, acc.boards_category_mapped_pin);
      bm.ShowDialog();
      acc.boards_category_mapped_pin = bm.mapped;
    }
    private void btnManageQueue_Click(object sender, EventArgs e)
    {
      if (acc == null || !acc.Connected)
      {
        MessageBox.Show("Please login first", "Warning", MessageBoxButtons.OK);
        return;
      }
      ManageQueue mq = new ManageQueue(acc, TRIAL);
      mq.Show(this);
      mq.TopMost = false;
    }
    private void btnImportImages_Click(object sender, EventArgs e)
    {
      if (TRIAL)
      {
        MessageBox.Show("This feature is only available for PinBot Premium", "Premium", MessageBoxButtons.OK);
        return;
      }
      if (acc == null || !acc.Connected)
      {
        MessageBox.Show("Please login first", "Warning", MessageBoxButtons.OK);
        return;
      }
      ImgImport ii = new ImgImport(acc, TRIAL);
      ii.ShowDialog();
    }
    private void txtScrapeSourceURL_TextChanged(object sender, EventArgs e)
    {
      if (acc != null)
        acc.txtScrapeSourceURL = txtScrapeSourceURL.Text;
    }
    private void numScrapeSourceURL_ValueChanged(object sender, EventArgs e)
    {
      if (acc != null)
        acc.numScrapeSourceURL = (int)numScrapeSourceURL.Value;
    }
    private void numScrapeWebsiteInDesc_ValueChanged(object sender, EventArgs e)
    {
      if (acc != null)
        acc.numScrapeWebsiteInDesc = (int)numScrapeWebsiteInDesc.Value;
    }


    //Follow
    private void btnStartFollow_Click(object sender, EventArgs e)
    {
      try
      {
        if (btnFollowStart.Text == "start")
        {

          if (acc == null || !acc.Connected)
          {
            MessageBox.Show("Please login first", "Warning", MessageBoxButtons.OK);
            return;
          }

          if (
                  acc.users_follow.Count < 1
              &&
                  acc.FollowCategories.Count < 1
              )
          {
            MessageBox.Show("No users or categories defined", "Warning", MessageBoxButtons.OK);
            return;
          }
          if (chkFollowScheduled.Checked && dtpFollowScheduled.Value <= DateTime.Now)
          {
            MessageBox.Show("Please select a future date/time", "Warning", MessageBoxButtons.OK);
            stopFollow();
            return;
          }

          f = new follow(acc,
                         new ContinuousSettings(chkFollowContinuousRun.Checked,
                                                tpFollowDelayFrom.Value,
                                                tpFollowDelayTo.Value),
                         chkFollowScheduled.Checked ? (DateTime?)dtpFollowScheduled.Value : null);
          f.ignoreCriteria = chkFollowIgnoreCriteria.Checked;
          if (int.Parse(txtFollow_FollowersMax.Text) >= 0)
            f.algo_MaxFollowers = int.Parse(txtFollow_FollowersMax.Text);
          if (int.Parse(txtFollow_FollowersMin.Text) >= 0)
            f.algo_MinFollowers = int.Parse(txtFollow_FollowersMin.Text);
          if (int.Parse(txtFollow_FollowingMax.Text) >= 0)
            f.algo_MaxFollowing = int.Parse(txtFollow_FollowingMax.Text);
          if (int.Parse(txtFollow_FollowingMin.Text) >= 0)
            f.algo_MinFollowing = int.Parse(txtFollow_FollowingMin.Text);
          if (int.Parse(txtFollow_BoardsMin.Text) >= 0)
            f.algo_MinBoards = int.Parse(txtFollow_BoardsMin.Text);
          if (int.Parse(txtFollow_BoardsMax.Text) >= 0)
            f.algo_MaxBoards = int.Parse(txtFollow_BoardsMax.Text);
          if (int.Parse(txtFollow_PinsMin.Text) >= 0)
            f.algo_MinPins = int.Parse(txtFollow_PinsMin.Text);
          if (int.Parse(txtFollow_PinsMax.Text) >= 0)
            f.algo_MaxPins = int.Parse(txtFollow_PinsMax.Text);
          f.algo_MustHaveWebsite = chkFollow_hasWebsite.Checked;
          f.algo_MustBePartner = chkFollow_isPartner.Checked;
          f.algo_MustHaveFB = chkFollow_hasFB.Checked;
          f.algo_MustHaveTW = chkFollow_hasTW.Checked;

          f.StartFollow_thread();


          btnFollowStart.Text = "stop";
          grpFollowSettings.Enabled = false;
          grpFollowAlgorithm.Enabled = !chkFollowIgnoreCriteria.Checked;
        }
        else
        {
          stopFollow();
        }
      }
      catch (Exception ex) { if (debug) MessageBox.Show(ex.StackTrace); }

    }
    void stopFollow()
    {
      btnFollowStart.Enabled = false;
      if (f != null)
      {
        f.Abort = true;
        while (f.Active)
        {
          Application.DoEvents();
        }
        f = null;
      }

      grpFollowSettings.Enabled = true;
      grpFollowAlgorithm.Enabled = !chkFollowIgnoreCriteria.Checked;

      btnFollowStart.Enabled = true;
      btnFollowStart.Text = "start";
    }
    private void btnFollowSettings_Click(object sender, EventArgs e)
    {
      if (acc == null || !acc.Connected)
      {
        MessageBox.Show("Please login first", "Warning", MessageBoxButtons.OK);
        return;
      }
      SettingsForm s = new SettingsForm(acc.setting_follow, "Follows max:");
      s.ShowDialog();
      acc.setting_follow = s.s;
    }
    private void btnFollowEditUsers_Click(object sender, EventArgs e)
    {
      if (TRIAL)
      {
        MessageBox.Show("This feature is only available for PinBot Premium", "Premium", MessageBoxButtons.OK);
        return;
      }
      if (acc == null || !acc.Connected)
      {
        MessageBox.Show("Please login first", "Warning", MessageBoxButtons.OK);
        return;
      }
      if (acc.boards.Count <= 0)
      {
        MessageBox.Show("Your account has no boards.", "Warning", MessageBoxButtons.OK);
        return;
      }
      UsersMappingSimple bm = new UsersMappingSimple(acc, acc.users_follow);
      bm.ShowDialog();
      acc.users_follow = bm.users;
    }
    private void btnFollowEditCats_Click(object sender, EventArgs e)
    {
      if (acc == null || !acc.Connected)
      {
        MessageBox.Show("Please login first", "Warning", MessageBoxButtons.OK);
        return;
      }
      if (acc.boards.Count <= 0)
      {
        MessageBox.Show("Your account has no boards.", "Warning", MessageBoxButtons.OK);
        return;
      }
      ManageCategories bm = new ManageCategories(acc, acc.FollowCategories);
      bm.ShowDialog();
      acc.FollowCategories = bm.cats;
    }
    private void chkFollowIgnoreCriteria_CheckedChanged(object sender, EventArgs e)
    {
      grpFollowAlgorithm.Enabled = !chkFollowIgnoreCriteria.Checked;
    }


    //Unfollow
    private void btnUnfollowStart_Click(object sender, EventArgs e)
    {
      if (btnUnfollowStart.Text == "start")
      {
        if (acc == null || !acc.Connected)
        {
          MessageBox.Show("Please login first", "Warning", MessageBoxButtons.OK);
          return;
        }

        if (chkUnfollowNonFollower.Checked)
        {
          if (MessageBox.Show("Unfollowing the non-followers can take a long time to start. The more followers you have, the longer it will take. This processes can take a few minutes, up to few hours to start. Do you wish to continue?", "Starting", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) != DialogResult.Yes)
            return;
        }

        if (chkUnfollowScheduled.Checked && dtpUnfollowScheduled.Value <= DateTime.Now)
        {
          MessageBox.Show("Please select a future date/time", "Warning", MessageBoxButtons.OK);
          stopUnfollow();
          return;
        }

        uf = new unfollow(acc,
                          new ContinuousSettings(chkUnfollowContinuousRun.Checked,
                                                 tpUnfollowDelayFrom.Value,
                                                 tpUnfollowDelayTo.Value),
                          chkUnfollowScheduled.Checked
                            ? (DateTime?)dtpUnfollowScheduled.Value
                            : null);

        if (int.Parse(txtUnfollow_FollowersMax.Text) >= 0)
          uf.algo_MaxFollowers = int.Parse(txtUnfollow_FollowersMax.Text);

        if (int.Parse(txtUnfollow_FollowersMin.Text) >= 0)
          uf.algo_MinFollowers = int.Parse(txtUnfollow_FollowersMin.Text);

        if (int.Parse(txtUnfollow_FollowingMax.Text) >= 0)
          uf.algo_MaxFollowing = int.Parse(txtUnfollow_FollowingMax.Text);

        if (int.Parse(txtUnfollow_FollowingMin.Text) >= 0)
          uf.algo_MinFollowing = int.Parse(txtUnfollow_FollowingMin.Text);

        if (int.Parse(txtUnfollow_BoardsMin.Text) >= 0)
          uf.algo_MinBoards = int.Parse(txtUnfollow_BoardsMin.Text);

        if (int.Parse(txtUnfollow_BoardsMax.Text) >= 0)
          uf.algo_MaxBoards = int.Parse(txtUnfollow_BoardsMax.Text);

        if (int.Parse(txtUnfollow_PinsMin.Text) >= 0)
          uf.algo_MinPins = int.Parse(txtUnfollow_PinsMin.Text);

        if (int.Parse(txtUnfollow_PinsMax.Text) >= 0)
          uf.algo_MaxPins = int.Parse(txtUnfollow_PinsMax.Text);

        uf.algo_UnfollowIfHasWebsite = chkUnfollow_hasWebsite.Checked;
        uf.algo_UnfollowIfPartner = chkUnfollow_isPartner.Checked;
        uf.algo_UnfollowIfNotFollowingMe = chkUnfollowNonFollower.Checked;
        uf.StartUnfollow_thread();

        btnUnfollowStart.Text = "stop";
        grpUnfollowSettings.Enabled = false;
        grpUnfollowAlgorithm.Enabled = false;
      }
      else
      {
        stopUnfollow();
      }
    }
    void stopUnfollow()
    {
      btnUnfollowStart.Enabled = false;
      if (uf != null)
      {
        uf.Abort = true;
        while (uf.Active)
        {
          Application.DoEvents();
        }
        uf = null;
      }

      grpUnfollowSettings.Enabled = true;
      grpUnfollowAlgorithm.Enabled = true;

      btnUnfollowStart.Enabled = true;
      btnUnfollowStart.Text = "start";
    }
    private void btnUnfollowSettings_Click(object sender, EventArgs e)
    {
      if (acc == null || !acc.Connected)
      {
        MessageBox.Show("Please login first", "Warning", MessageBoxButtons.OK);
        return;
      }
      SettingsForm s = new SettingsForm(acc.setting_unfollow, "Unfollows max:");
      s.ShowDialog();
      acc.setting_unfollow = s.s;
    }


    //Repin
    private void btnStartRepinScrape_Click(object sender, EventArgs e)
    {

      if (btnStartRepinScrape.Text == "start scrape")
      {
        if (acc == null || !acc.Connected)
        {
          MessageBox.Show("Please login first", "Warning", MessageBoxButtons.OK);
          return;
        }
        if (acc.boards.Count <= 0)
        {
          MessageBox.Show("Your account has no boards.", "Warning", MessageBoxButtons.OK);
          return;
        }
        if (
                acc.boards_category_mapped_repin.Values.SelectMany(x => x).Count() < 1
            &&
                acc.usersboards_mapped_repin.Values.SelectMany(x => x).Count() < 1
            )
        {
          MessageBox.Show("No boards mapped to category", "Warning", MessageBoxButtons.OK);
          return;
        }

        grpRepinSettings.Enabled = grpRepinScrapeSettings.Enabled = false;
        btnStartRepin.Enabled = false;

        rp = new repin(acc);
        rp.StartRepinScrape_thread();

        btnStartRepinScrape.Text = "stop scrape";
      }
      else
      {
        stopRepinScrape();
      }
    }
    private void btnRepinStart_Click(object sender, EventArgs e)
    {
      if (btnStartRepin.Text == "start repin")
      {
        if (acc == null || !acc.Connected)
        {
          MessageBox.Show("Please login first", "Warning", MessageBoxButtons.OK);
          return;
        }
        if (acc.boards.Count <= 0)
        {
          MessageBox.Show("Your account has no boards.", "Warning", MessageBoxButtons.OK);
          return;
        }
        if (
                rdbRepinAutopilot.Checked
            &&
                acc.boards_category_mapped_repin.Values.SelectMany(x => x).Count() < 1
            &&
                acc.usersboards_mapped_repin.Values.SelectMany(x => x).Count() < 1
            )
        {
          MessageBox.Show("No boards mapped to category", "Warning", MessageBoxButtons.OK);
          return;
        }

        if (chkRepinScheduled.Checked && dtpRepinScheduled.Value <= DateTime.Now)
        {
          MessageBox.Show("Please select a future date/time", "Warning", MessageBoxButtons.OK);
          stopRepin();
          return;
        }

        grpRepinSettings.Enabled = grpRepinScrapeSettings.Enabled = false;
        btnStartRepinScrape.Enabled = false;

        rp = new repin(acc,
                       rdbRepinAutopilot.Checked,
                       new ContinuousSettings(chkRepinContinousRun.Checked, tpRepinDelayFrom.Value, tpRepinDelayTo.Value),
                       chkRepinScheduled.Checked ? (DateTime?)dtpRepinScheduled.Value : null);

        if (rdbRepinManual.Checked)
        {
          rp.StartRepin_thread();
        }
        else if (rdbRepinAutopilot.Checked)
        {
          rp.StartRepin_thread();
          rp.StartRepinScrape_thread();
        }
        btnStartRepin.Text = "stop repin";
      }
      else
      {
        stopRepin();
      }
    }
    private void btnRepinEditQueue_Click(object sender, EventArgs e)
    {
      if (acc == null || !acc.Connected)
      {
        MessageBox.Show("Please login first", "Warning", MessageBoxButtons.OK);
        return;
      }
      ManageQueue mq = new ManageQueue(acc, TRIAL, true);
      mq.Show(this);
      mq.TopMost = false;
    }
    void stopRepinScrape()
    {
      btnStartRepinScrape.Enabled = false;
      if (rp != null)
      {
        rp.Abort = true;
        while (rp.Active)
        {
          Application.DoEvents();
          rp.SetKeepPoll(false);
        }
        rp = null;

      }

      grpRepinSettings.Enabled = grpRepinScrapeSettings.Enabled = true;
      btnStartRepin.Enabled = true;

      btnStartRepinScrape.Enabled = true;
      btnStartRepinScrape.Text = "start scrape";
    }
    void stopRepin()
    {
      btnStartRepin.Enabled = false;
      if (rp != null)
      {
        rp.Abort = true;
        while (rp.Active)
        {
          Application.DoEvents();
          rp.SetKeepPoll(false);
        }
        rp = null;

      }

      grpRepinSettings.Enabled = grpRepinScrapeSettings.Enabled = true;
      btnStartRepinScrape.Enabled = true;

      btnStartRepin.Enabled = true;
      btnStartRepin.Text = "start repin";
    }
    private void btnRepinBoardMapping_Click(object sender, EventArgs e)
    {
      if (acc == null || !acc.Connected)
      {
        MessageBox.Show("Please login first", "Warning", MessageBoxButtons.OK);
        return;
      }
      if (acc.boards.Count <= 0)
      {
        MessageBox.Show("Your account has no boards.", "Warning", MessageBoxButtons.OK);
        return;
      }
      BoardMapping bm = new BoardMapping(acc, acc.boards_category_mapped_repin);
      bm.ShowDialog();
      acc.boards_category_mapped_repin = bm.mapped;
    }
    private void btnRepinEditUserMapping_Click(object sender, EventArgs e)
    {
      if (acc == null || !acc.Connected)
      {
        MessageBox.Show("Please login first", "Warning", MessageBoxButtons.OK);
        return;
      }
      if (acc.boards.Count <= 0)
      {
        MessageBox.Show("Your account has no boards.", "Warning", MessageBoxButtons.OK);
        return;
      }
      UsersMapping bm = new UsersMapping(acc, acc.usersboards_mapped_repin);
      bm.ShowDialog();
      acc.usersboards_mapped_repin = bm.mapped;
    }

    private void rdbRepinManual_CheckedChanged(object sender, EventArgs e)
    {
      btnStartRepinScrape.Enabled = rdbRepinManual.Checked;
      numRepinScrapes.Enabled = rdbRepinManual.Checked;
    }

    private void rdbRepinAutopilot_CheckedChanged(object sender, EventArgs e)
    {
      btnStartRepinScrape.Enabled = !rdbRepinAutopilot.Checked;
      numRepinScrapes.Enabled = rdbRepinManual.Checked;
    }

    private void chkRepinScheduled_CheckedChanged(object sender, EventArgs e)
    {
      dtpRepinScheduled.Visible = chkRepinScheduled.Checked;
    }

    private void chkRepinContinousRun_CheckedChanged(object sender, EventArgs e)
    {
      tpRepinDelayFrom.Visible = tpRepinDelayTo.Visible = chkRepinContinousRun.Checked;
    }

    private void chkLikeScheduled_CheckedChanged(object sender, EventArgs e)
    {
      dtpLikeScheduled.Visible = chkLikeScheduled.Checked;
    }

    private void chkLikeContinuousRun_CheckedChanged(object sender, EventArgs e)
    {
      tpLikeDelayFrom.Visible = tpLikeDelayTo.Visible = chkLikeContinuousRun.Checked;
    }

    private void chkInviteScheduled_CheckedChanged(object sender, EventArgs e)
    {
      dtpInviteScheduled.Visible = chkInviteScheduled.Checked;
    }

    private void chkInviteContinuousRun_CheckedChanged(object sender, EventArgs e)
    {
      tpInviteDelayFrom.Visible = tpInviteDelayTo.Visible = chkInviteContinuousRun.Checked;
    }

    private void chkFollowScheduled_CheckedChanged(object sender, EventArgs e)
    {
      dtpFollowScheduled.Visible = chkFollowScheduled.Checked;
    }

    private void chkFollowContinuousRun_CheckedChanged(object sender, EventArgs e)
    {
      tpFollowDelayFrom.Visible = tpFollowDelayTo.Visible = chkFollowContinuousRun.Checked;
    }

    private void chkUnfollowScheduled_CheckedChanged(object sender, EventArgs e)
    {
      dtpUnfollowScheduled.Visible = chkUnfollowScheduled.Checked;
    }

    private void chkUnfollowContinuousRun_CheckedChanged(object sender, EventArgs e)
    {
      tpUnfollowDelayFrom.Visible = tpUnfollowDelayTo.Visible = chkUnfollowContinuousRun.Checked;
    }

    private void btnRepinSettings_Click(object sender, EventArgs e)
    {
      if (acc == null || !acc.Connected)
      {
        MessageBox.Show("Please login first", "Warning", MessageBoxButtons.OK);
        return;
      }
      SettingsForm s = new SettingsForm(acc.setting_repin, "Repins max:");
      s.ShowDialog();
      acc.setting_repin = s.s;
    }
    private void numRepinScrapes_ValueChanged(object sender, EventArgs e)
    {
      if (acc != null)
        acc.numRepinScrapes = (int)numRepinScrapes.Value;
    }


    //Like
    private void btnLikeStart_Click(object sender, EventArgs e)
    {
      if (btnLikeStart.Text == "start")
      {
        if (acc == null || !acc.Connected)
        {
          MessageBox.Show("Please login first", "Warning", MessageBoxButtons.OK);
          return;
        }

        if (
                acc.users_like.Count < 1
            &&
                acc.LikeCategories.Count < 1
            )
        {
          MessageBox.Show("No users or categories defined", "Warning", MessageBoxButtons.OK);
          return;
        }

        if (chkLikeScheduled.Checked && dtpLikeScheduled.Value <= DateTime.Now)
        {
          MessageBox.Show("Please select a future date/time", "Warning", MessageBoxButtons.OK);
          stopLike();
          return;
        }

        grpLikeSettings.Enabled = false;
        lk = new like(acc,
                      new ContinuousSettings(chkLikeContinuousRun.Checked,
                                             tpLikeDelayFrom.Value,
                                             tpLikeDelayTo.Value),
                      chkLikeScheduled.Checked ? (DateTime?)dtpLikeScheduled.Value : null);

        lk.start_thread();

        btnLikeStart.Text = "stop";
      }
      else
      {
        stopLike();
      }
    }
    void stopLike()
    {
      btnLikeStart.Enabled = false;
      if (lk != null)
      {
        lk.Abort = true;
        while (lk.Active)
        {
          Application.DoEvents();
        }
        lk = null;

      }

      grpLikeSettings.Enabled = true;

      btnLikeStart.Enabled = true;
      btnLikeStart.Text = "start";
    }
    private void btnLikeSettings_Click(object sender, EventArgs e)
    {
      if (acc == null || !acc.Connected)
      {
        MessageBox.Show("Please login first", "Warning", MessageBoxButtons.OK);
        return;
      }
      SettingsForm s = new SettingsForm(acc.setting_like, "Likes max:");
      s.ShowDialog();
      acc.setting_like = s.s;
    }
    private void btnLikeEditCats_Click(object sender, EventArgs e)
    {
      if (acc == null || !acc.Connected)
      {
        MessageBox.Show("Please login first", "Warning", MessageBoxButtons.OK);
        return;
      }
      if (acc.boards.Count <= 0)
      {
        MessageBox.Show("Your account has no boards.", "Warning", MessageBoxButtons.OK);
        return;
      }
      ManageCategories bm = new ManageCategories(acc, acc.LikeCategories);
      bm.ShowDialog();
      acc.LikeCategories = bm.cats;
    }
    private void btnLikeEditUsers_Click(object sender, EventArgs e)
    {
      if (acc == null || !acc.Connected)
      {
        MessageBox.Show("Please login first", "Warning", MessageBoxButtons.OK);
        return;
      }
      if (acc.boards.Count <= 0)
      {
        MessageBox.Show("Your account has no boards.", "Warning", MessageBoxButtons.OK);
        return;
      }
      UsersMappingSimple bm = new UsersMappingSimple(acc, acc.users_like);
      bm.ShowDialog();
      acc.users_like = bm.users;
    }



    //Invite
    private void btnInviteStart_Click(object sender, EventArgs e)
    {
      if (btnInviteStart.Text == "start")
      {
        if (acc == null || !acc.Connected)
        {
          MessageBox.Show("Please login first", "Warning", MessageBoxButtons.OK);
          return;
        }
        if (acc.boards.Count <= 0)
        {
          MessageBox.Show("Your account has no boards.", "Warning", MessageBoxButtons.OK);
          return;
        }


        acc.boards_category_mapped_invite.Clear();
        foreach (KeyValuePair<string, string> kv in lstInviteBoards.SelectedItems)
        {
          acc.boards_category_mapped_invite.Add(kv.Key, null);
        }

        if (acc.boards_category_mapped_invite.Count <= 0)
        {
          MessageBox.Show("No boards selected", "Warning", MessageBoxButtons.OK);
          return;
        }

        if (chkInviteScheduled.Checked && dtpInviteScheduled.Value <= DateTime.Now)
        {
          MessageBox.Show("Please select a future date/time", "Warning", MessageBoxButtons.OK);
          stopInvite();
          return;
        }

        grpInviteSettings.Enabled = false;
        iv = new invites(acc,
                         new ContinuousSettings(chkInviteContinuousRun.Checked,
                                                tpInviteDelayFrom.Value,
                                                tpInviteDelayTo.Value),
                         chkLikeScheduled.Checked ? (DateTime?)dtpInviteScheduled.Value : null);
        iv.start_thread();

        btnInviteStart.Text = "stop";
      }
      else
      {
        stopInvite();
      }
    }
    void stopInvite()
    {
      btnInviteStart.Enabled = false;
      if (iv != null)
      {
        iv.Abort = true;
        while (iv.Active)
        {
          Application.DoEvents();
        }
        iv = null;
      }
      grpInviteSettings.Enabled = true;
      btnInviteStart.Enabled = true;
      btnInviteStart.Text = "start";
    }
    private void btnInviteSettings_Click(object sender, EventArgs e)
    {
      if (acc == null || !acc.Connected)
      {
        MessageBox.Show("Please login first", "Warning", MessageBoxButtons.OK);
        return;
      }
      SettingsForm s = new SettingsForm(acc.setting_invite, "Invites max:");
      s.ShowDialog();
      acc.setting_invite = s.s;
    }



    //misc
    private void toolStripSplitButton1_ButtonClick(object sender, EventArgs e)
    {
      try
      {
        if (File.Exists("PinBot Manual.pdf"))
        {
          Process.Start("PinBot Manual.pdf");
        }
        else
        {
          using (WebClient client = new WebClient())
          {
            client.DownloadFile("http://healzer.com/pinbot/PinBot Manual.pdf", "./PinBot Manual.pdf");
          }
          Process.Start("PinBot Manual.pdf");
        }
      }
      catch { MessageBox.Show("Oops! Something went wrong.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }
    }
    private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      Process.Start("");
    }
    private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      Process.Start("");
    }
    private void lblUpdate_Click(object sender, EventArgs e)
    {
      try
      {
        if (MessageBox.Show("New version available!\nThe updater will start and all PinBot instances will be closed.\nDo you wish to continue?", "Update", MessageBoxButtons.YesNoCancel) == DialogResult.Yes)
        {
          System.Diagnostics.Process.Start("updater.exe");
          saveSerial();
          Environment.Exit(Environment.ExitCode);
        }
      }
      catch
      {
        try
        {
          File.Delete("updater.exe");
          Thread.Sleep(1000);
          if (!File.Exists("./updater.exe"))
          {
            using (WebClient client = new WebClient())
            {
              client.DownloadFile("http://healzer.com/pinbot/updater.exe", "./updater.exe");
            }
          }
        }
        catch
        {
          MessageBox.Show("FATAL updater error #1583. Please contact support.", "FATAL ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
      }

    }
    private void clickTheBlueQuestionmarkToOpenTheManualToolStripMenuItem_Click(object sender, EventArgs e)
    {
      try
      {
        if (File.Exists("PinBot Manual.pdf"))
        {
          Process.Start("PinBot Manual.pdf");
        }
        else
        {
          using (WebClient client = new WebClient())
          {
            client.DownloadFile("http://healzer.com/pinbot/PinBot Manual.pdf", "./PinBot Manual.pdf");
          }
          Process.Start("PinBot Manual.pdf");
        }
      }
      catch { MessageBox.Show("Oops! Something went wrong.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }
    }
    private void visitWebsiteToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Process.Start("http://healzer.com/pinbot/");
    }

    private string strImportantMessage;

    void check_importantMessage()
    {
      bool run = true;
      while (run)
      {
        try
        {
          strImportantMessage = http.GET("http://healzer.com/pinbot/important.txt", "", null, null, null);
          if (strImportantMessage.Length > 1)
            tslNotification.Visible = true;
          else
            tslNotification.Visible = false;
        }
        catch (Exception ex) { if (debug) MessageBox.Show(ex.StackTrace); }
        Thread.Sleep(1800000);
      }
    }

    private void tslNotification_Click(object sender, EventArgs e)
    {
      MessageBox.Show(strImportantMessage, "Important", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void fAQToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Process.Start("http://healzer.com/pinbot/faq.php");
    }

    private void visitBlogToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Process.Start("http://healzer.com/pinbot/premium/");
    }

    private void whatsNewToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Process.Start("http://healzer.com/pinbot/premium/whats-new/");
    }

    //account

    private void btnAccountReloadBoards_Click(object sender, EventArgs e)
    {
      try
      {

        if (acc == null || !acc.Connected)
        {
          MessageBox.Show("Please login first", "Warning", MessageBoxButtons.OK);
          return;
        }

        this.Cursor = Cursors.WaitCursor;
        Thread t = new Thread(acc.loadBoards);
        t.Start();
        while (!acc.boardsChecked)
          Application.DoEvents();

        fillBoards();
        this.Cursor = Cursors.Default;

      }
      catch
      {
        MessageBox.Show("Fatal error. Try restarting PinBot, login and retry.", "Fatal error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
      finally
      {
        this.Cursor = Cursors.Default;
      }
    }

    private void btnAccountRefreshStats_Click(object sender, EventArgs e)
    {
      try
      {
        if (acc == null || !acc.Connected)
        {
          MessageBox.Show("Please login first", "Warning", MessageBoxButtons.OK);
          return;
        }

        this.Cursor = Cursors.WaitCursor;
        acc.LoadStats();
        this.Cursor = Cursors.Default;
      }
      catch
      {
        MessageBox.Show("ERROR loading stats. Please try again later or contact support.", "Fatal error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
      finally
      {
        this.Cursor = Cursors.Default;
      }
    }

    private void dtpRepinScheduled_ValueChanged(object sender, EventArgs e)
    {
      if (acc != null)
        acc.scheduledRepin = dtpRepinScheduled.Value;
    }

    private void dtpScheduled_ValueChanged(object sender, EventArgs e)
    {
      if (acc != null)
        acc.scheduledPin = dtpScheduled.Value;
    }

    private void dtpLikeScheduled_ValueChanged(object sender, EventArgs e)
    {
      if (acc != null)
        acc.scheduledLike = dtpLikeScheduled.Value;
    }

    private void dtpInviteScheduled_ValueChanged(object sender, EventArgs e)
    {
      if (acc != null)
        acc.scheduledInvite = dtpInviteScheduled.Value;
    }

    private void dtpFollowScheduled_ValueChanged(object sender, EventArgs e)
    {
      if (acc != null)
        acc.scheduledFollow = dtpFollowScheduled.Value;
    }

    private void dtpUnfollowScheduled_ValueChanged(object sender, EventArgs e)
    {
      if (acc != null)
        acc.scheduledUnfollow = dtpUnfollowScheduled.Value;
    }
  }
}
