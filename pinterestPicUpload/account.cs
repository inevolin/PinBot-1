using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Windows.Forms;

namespace PinBot
{
  [Serializable]
  public class account
  {

    public string VERSION;

    public CookieContainer c = new CookieContainer();
    public string App_version = "", CSRFtoken = "", Username = "", Request_id = "";
    public DateTime lastLogin;
    public bool lastLoginProxy = false;

    public Dictionary<string, string> boards = new Dictionary<string, string>(); // board_id, board_name
    public Dictionary<string, string> boards_url_names = new Dictionary<string, string>(); //  board_id, board_url

    public Dictionary<string, List<string>> boards_category_mapped_pin = new Dictionary<string, List<string>>();
    public Dictionary<string, List<string>> boards_category_mapped_repin = new Dictionary<string, List<string>>();
    public Dictionary<string, List<string>> boards_category_mapped_invite = new Dictionary<string, List<string>>();
    public Dictionary<string, List<string>> usersboards_mapped_repin = new Dictionary<string, List<string>>();

    public List<string> users_follow = new List<string>();
    public List<string> users_like = new List<string>();
    public List<string> FollowCategories = new List<string>();
    public List<string> LikeCategories = new List<string>();

    public Settings setting_pin, setting_repin, setting_like, setting_invite, setting_follow, setting_unfollow;

    public Follow_settings follow_settings, unfollow_settings;

    public string txtPinWebsiteURL, txtScrapeWebsiteURL, txtPinSourceURL, txtScrapeSourceURL;
    public int numPinSourceURL, numPinWebsiteInDesc, numScrapeWebsiteInDesc, numScrapeSourceURL;
    public int numRepinScrapes;

    public string login = "", password = "";
    public string Error = null;
    public bool Connected;
    public WebProxy proxy;

    public DateTime scheduledPin;
    public DateTime scheduledRepin;
    public DateTime scheduledLike;
    public DateTime scheduledInvite;
    public DateTime scheduledFollow;
    public DateTime scheduledUnfollow;

    public void redef()
    {

      if (boards == null) boards = new Dictionary<string, string>();
      if (boards_url_names == null) boards_url_names = new Dictionary<string, string>();
      if (boards_category_mapped_pin == null) boards_category_mapped_pin = new Dictionary<string, List<string>>();
      if (boards_category_mapped_repin == null) boards_category_mapped_repin = new Dictionary<string, List<string>>();
      if (boards_category_mapped_invite == null) boards_category_mapped_invite = new Dictionary<string, List<string>>();
      if (usersboards_mapped_repin == null) usersboards_mapped_repin = new Dictionary<string, List<string>>();

      if (users_follow == null) users_follow = new List<string>();
      if (users_like == null) users_like = new List<string>();
      if (FollowCategories == null) FollowCategories = new List<string>();
      if (LikeCategories == null) LikeCategories = new List<string>();

      if (c == null) c = new CookieContainer();

      if (txtPinSourceURL == null) txtPinSourceURL = "";
      if (txtScrapeSourceURL == null) txtScrapeSourceURL = "";

      if (numPinSourceURL < 0) numPinSourceURL = 10;
      if (numPinWebsiteInDesc < 0) numPinWebsiteInDesc = 10;
      if (numScrapeWebsiteInDesc < 0) numScrapeWebsiteInDesc = 10;
      if (numScrapeSourceURL < 0) numScrapeSourceURL = 10;

      if (numRepinScrapes < 1) numRepinScrapes = 20;

      if (follow_settings == null) follow_settings = new Follow_settings();
      if (unfollow_settings == null) unfollow_settings = new Follow_settings();

      if (stats == null) stats = new Stats();

      if (scheduledPin == null || scheduledPin < DateTime.Now) scheduledPin = DateTime.Now + new TimeSpan(3, 0, 0);
      if (scheduledRepin == null || scheduledRepin < DateTime.Now) scheduledRepin = DateTime.Now + new TimeSpan(3, 0, 0);
      if (scheduledLike == null || scheduledLike < DateTime.Now) scheduledLike = DateTime.Now + new TimeSpan(3, 0, 0);
      if (scheduledFollow == null || scheduledFollow < DateTime.Now) scheduledFollow = DateTime.Now + new TimeSpan(3, 0, 0);
      if (scheduledUnfollow == null || scheduledUnfollow < DateTime.Now) scheduledUnfollow = DateTime.Now + new TimeSpan(3, 0, 0);
      if (scheduledInvite == null || scheduledInvite < DateTime.Now) scheduledInvite = DateTime.Now + new TimeSpan(3, 0, 0);
    }

    public account()
    {
      setting_pin = new Settings(60, 300, 50);
      setting_repin = new Settings(60, 300, 50);
      setting_like = new Settings(60, 300, 50);
      setting_invite = new Settings(60, 300, 50);
      setting_follow = new Settings(60, 300, 50);
      setting_unfollow = new Settings(60, 300, 50);
      setting_pin = new Settings(60, 300, 50);
    }
    public void log_in(string login, string password)
    {
      this.login = login;
      this.password = password;
      Thread thread = new Thread(new ThreadStart(log_in));
      thread.Start();
    }
    void log_in()
    {
      List<string> headers = new List<string>();
      string RS = http.GET("https://www.pinterest.com/login/", "https://www.pinterest.com", c, headers, this.proxy);

      Match m = Regex.Match(RS, "\"app_version\": \"(.+?)\"");
      App_version = m.Groups[1].ToString();

      getToken(c);

      headers = new List<string>();
      headers.Add("Accept-Language: en-gb,en;q=0.5");
      headers.Add("X-NEW-APP: 1");
      headers.Add("X-APP-VERSION: " + App_version);
      headers.Add("Accept-Encoding: gzip, deflate");
      headers.Add("Cache-Control: no-cache");
      headers.Add("Pragma: no-cache");
      headers.Add("X-Requested-With: XMLHttpRequest");
      headers.Add("X-CSRFToken: " + CSRFtoken);

      string strLogin = "source_url=" + HttpUtility.UrlEncode("/login/") + "&data=" + HttpUtility.UrlEncode("{\"options\":{\"username_or_email\":\"" + login + "\",\"password\":\"" + password.Replace(@"\", "\\\\") + "\"},\"context\":{}}") + "&module_path=" + HttpUtility.UrlEncode("App()>LoginPage()>Login()>Button(class_name=primary, text=Log in, type=submit, size=large)");

      List<byte[]> data = new List<byte[]>() { Encoding.ASCII.GetBytes(strLogin) };
      RS = http.POST("https://www.pinterest.com/resource/UserSessionResource/create/", "https://www.pinterest.com/login/", "application/x-www-form-urlencoded; charset=UTF-8", data, headers, c, this.proxy, "application/json, text/javascript, */*; q=0.01");


      if (!RS.Contains("\"error\": null}}"))
      {
        Error = "Login failed!";
      }
      else
      {
        if (proxy != null) lastLoginProxy = true;
        loadBoards();
        lastLogin = DateTime.Now;

      }


    }
    public void loadBoards()
    {
      try
      {


        List<string> default_headers = new List<string> { "X-Requested-With: XMLHttpRequest", "X-CSRFToken: " + CSRFtoken, "Accept-Encoding: gzip,deflate,sdch", "Accept-Language: en-US,en;q=0.8", "Cache-Control: max-age=0" };
        string RS = http.GET("https://www.pinterest.com", "", c, default_headers, this.proxy);

        getToken(c);

        Match match = Regex.Match(RS, "\"username\": \"(.+?)\"");
        Username = match.Groups[1].Value;

        match = Regex.Match(RS, "\"request_identifier\": \"(\\d+?)\"");
        Request_id = match.Groups[1].Value;

        default_headers = new List<string> { "Accept-Encoding: gzip,deflate,sdch", "Accept-Language: en-US,en;q=0.8", "Cache-Control: max-age=0" };
        RS = http.GET("http://www.pinterest.com/" + Username + "/", "", c, default_headers, this.proxy);
        LoadStats(RS);

        getToken(c);

        if (Regex.IsMatch(RS, "[^\\\"]Due to strange activity\\\""))
        {
          Connected = false;
          Error = "Account limited. Password reset required!";
          return;
        }


        boards.Clear();
        boards_url_names.Clear();

        RS = getNextPage_Search(true, null);
        string bookmark = null;

        while ((bookmark = getBookmark(RS)).Length > 5)
        {
          addToBoards(RS);
          RS = getNextPage_Search(false, bookmark);
        }
        addToBoards(RS);
        excludeRemovedBoards();

        Connected = (boards.Count > 0) ? true : false;
        if (!Connected)
        {
          Error = "No boards found!";
          return;
        }

      }
      catch { Error = "FATAL Error: #acc_LB"; }
      boardsChecked = true;
    }
    void addToBoards(string RS)
    {
      string pattern1 = "\"public\" \"url\": \"\\\\/(.+?)\\\\/(.+?)\\\\/\"(.+?)\"id\": \"(\\d+?)\" \"name\": \"(.+?)\"";
      string pattern2 = "\"public\" \"url\": \"/(.+?)/(.+?)/\"(.+?)\"id\": \"(\\d+?)\" \"name\": \"(.+?)\"";
      foreach (Match matches in Regex.Matches(RS, pattern1))
      {
        if (!boards.ContainsKey(matches.Groups[4].Value))
          boards.Add(matches.Groups[4].Value, matches.Groups[5].Value);
        if (!boards_url_names.ContainsKey(matches.Groups[4].Value))
          boards_url_names.Add(matches.Groups[4].Value, matches.Groups[1].Value + "/" + matches.Groups[2].Value);
      }
      foreach (Match matches in Regex.Matches(RS, pattern2))
      {
        if (!boards.ContainsKey(matches.Groups[4].Value))
          boards.Add(matches.Groups[4].Value, matches.Groups[5].Value);
        if (!boards_url_names.ContainsKey(matches.Groups[4].Value))
          boards_url_names.Add(matches.Groups[4].Value, matches.Groups[1].Value + "/" + matches.Groups[2].Value);
      }
    }
    void excludeRemovedBoards()
    {

      List<string> toDelete = new List<string>();
      foreach (KeyValuePair<string, List<string>> kv in boards_category_mapped_pin)
      {
        if (!boards.ContainsKey(kv.Key))
          toDelete.Add(kv.Key);
      }
      foreach (string k in toDelete)
      {
        boards_category_mapped_pin.Remove(k);
      }


      toDelete = new List<string>();
      foreach (KeyValuePair<string, List<string>> kv in boards_category_mapped_repin)
      {
        if (!boards.ContainsKey(kv.Key))
          toDelete.Add(kv.Key);
      }
      foreach (string k in toDelete)
      {
        boards_category_mapped_pin.Remove(k);
      }


      toDelete = new List<string>();
      foreach (KeyValuePair<string, List<string>> kv in boards_category_mapped_invite)
      {
        if (!boards.ContainsKey(kv.Key))
          toDelete.Add(kv.Key);
      }
      foreach (string k in toDelete)
      {
        boards_category_mapped_pin.Remove(k);
      }


      toDelete = new List<string>();
      foreach (KeyValuePair<string, List<string>> kv in usersboards_mapped_repin)
      {
        if (!boards.ContainsKey(kv.Key))
          toDelete.Add(kv.Key);
      }
      foreach (string k in toDelete)
      {
        boards_category_mapped_pin.Remove(k);
      }

    }
    string getNextPage_Search(bool first, string bookmark) //first load  |  search page (true) else followers/following page
    {

      string url, referral, json, RS;
      List<string> headers = new List<string>();

      /*
         if (first)
          return http.GET("http://www.pinterest.com/" + Username + "/boards/", "http://www.pinterest.com/", c, null, this.proxy).Replace(",", "");
      */
      if (first)
      {
        url = "http://www.pinterest.com/resource/UserResource/get/?source_url=";
        referral = "http://www.pinterest.com/" + Username + "/";
        json = HttpUtility.UrlEncode("/" + Username + "/") + "&data=" + HttpUtility.UrlEncode("{\"options\":{\"username\":\"" + Username + "\",\"invite_code\":null},\"context\":{},\"module\":{\"name\":\"UserProfileContent\",\"options\":{\"tab\":\"boards\"}},\"render_type\":1,\"error_strategy\":0}"); ;
        json = Regex.Replace(json, "%\\d(\\w)", m => m.ToString().ToUpper());
        url += json;
        url += "&_=" + (GetTime() + 1);

        headers.Add("X-NEW-APP: 1");
        headers.Add("X-APP-VERSION: " + App_version);
        headers.Add("X-Requested-With: XMLHttpRequest");
        headers.Add("Accept-Language: en-gb,en;q=0.5");
        headers.Add("Accept-Encoding: gzip, deflate");
        RS = http.GET(url, referral, c, headers, this.proxy);
        RS = RS.Replace(",", "");

        return RS;
      }

      url = "http://www.pinterest.com/resource/ProfileBoardsResource/get/?source_url=";
      referral = "http://www.pinterest.com/" + Username + "/";
      json = HttpUtility.UrlEncode("/" + Username + "/") + "&data=" + HttpUtility.UrlEncode("{\"options\":{\"field_set_key\":\"grid_item\",\"username\":\"" + Username + "\",\"bookmarks\":[\"" + bookmark + "\"]},\"context\":{},\"module\":{\"name\":\"GridItems\",\"options\":{\"scrollable\":true,\"show_grid_footer\":false,\"centered\":true,\"reflow_all\":true,\"virtualize\":true,\"item_options\":{\"show_board_context\":false,\"view_type\":\"boardCoverImage\",\"show_user_icon\":false},\"layout\":\"fixed_height\"}},\"render_type\":3,\"error_strategy\":1}"); ;
      json = Regex.Replace(json, "%\\d(\\w)", m => m.ToString().ToUpper());
      url += json;
      url += "&_=" + (GetTime() + 1);


      headers.Add("X-NEW-APP: 1");
      headers.Add("X-APP-VERSION: " + App_version);
      headers.Add("X-Requested-With: XMLHttpRequest");
      headers.Add("Accept-Language: en-gb,en;q=0.5");
      headers.Add("Accept-Encoding: gzip, deflate");
      RS = http.GET(url, referral, c, headers, this.proxy);
      LoadStats(RS);
      RS = RS.Replace(",", "");
      /*Random r = new Random();
      int sleepP = r.Next(1000,2000);
      while (sleepP > 0)
      {
          Thread.Sleep(100); sleepP -= 100;
          if (abort) { active = false; return null; }
      }*/

      return RS;
    }
    public string getBookmark(string RS)
    {
      string bookmark = "";
      Match match = Regex.Match(RS, "\"bookmarks\": \\[\"[^\\-end](.*?)\"\\]", RegexOptions.Multiline);
      bookmark = match.Groups[0].ToString().Replace("\"bookmarks\": [\"", "").Replace("\"]", "");
      return bookmark;
    }
    public static Int64 GetTime()
    {
      Int64 retval = 0;
      var st = new DateTime(1970, 1, 1);
      TimeSpan t = (DateTime.Now.ToUniversalTime() - st);
      retval = (Int64)(t.TotalMilliseconds + 0.5);
      return retval;
    }
    public void getToken(CookieContainer c)
    {
      for (int z = 0; z < c.GetCookies(new Uri("http://pinterest.com")).Count; z++)
      {
        string cookie = c.GetCookies(new Uri("http://pinterest.com"))[z].ToString();
        if (cookie.Contains("csrftoken"))
        {
          CSRFtoken = cookie.Replace("csrftoken=", "").Trim();
        }
      }
    }


    public bool boardsChecked = false; //for login from session


    public static string hardwareID()
    {
      NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
      string hardwareID = "";
      string sMacAddress = "";
      string userID = Environment.UserName;
      foreach (NetworkInterface adapter in nics)
      {
        if (sMacAddress == String.Empty)// only return MAC Address from first card  
        {
          IPInterfaceProperties properties = adapter.GetIPProperties();
          sMacAddress = adapter.GetPhysicalAddress().ToString();
        }
      }
      hardwareID = ("" + sMacAddress + "" + userID).GetHashCode().ToString();
      Console.WriteLine(hardwareID);

      //MessageBox.Show(userID + Environment.NewLine + sMacAddress);

      return hardwareID;
    }

    public Stats stats;
    public void LoadStats(string RS = "")
    {
      try
      {
        if (RS == "")
        {
          string url = "http://www.pinterest.com/" + this.Username + "/";
          string referral = "http://www.pinterest.com/" + this.Username + "/";
          List<string> headers = new List<string>();
          headers.Add("Accept-Language: en-gb,en;q=0.5");
          headers.Add("Accept-Encoding: gzip, deflate");
          RS = http.GET(url, referral, c, headers, this.proxy);
        }

        string pat = "";
        pat += "\"pin_count\": (\\d+?),(?:.+?)";
        pat += "\"follower_count\": (\\d+?),(?:.+?)";
        pat += "\"following_count\": (\\d+?),(?:.+?)";
        pat += "\"like_count\": (\\d+?),(?:.+?)";
        pat += "\"board_count\": (\\d+?),";

        Match m = Regex.Match(RS, pat);
        if (stats == null) stats = new Stats();
        stats.pins = int.Parse(m.Groups[1].Value.ToString());
        stats.followers = int.Parse(m.Groups[2].Value.ToString());
        stats.following = int.Parse(m.Groups[3].Value.ToString());
        stats.likes = int.Parse(m.Groups[4].Value.ToString());
        stats.boards = int.Parse(m.Groups[5].Value.ToString());
      }
      catch { }
    }
  }

}
