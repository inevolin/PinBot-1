using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Windows.Forms;

namespace PinBot
{
  class invites
  {
    private CookieContainer c = new CookieContainer();
    private account acc;
    private string app_version = "", CSRFToken = "";
    private bool active = false;
    private bool abort = false;
    private int timeout_min = 60;
    private int timeout_max = 300;
    string category = "";
    static List<string> categories = new List<string>();
    private int invited = 0;
    private int maxInvites = 50;
    private string boardID = "";
    //private int errors = 0;
    private DateTime time;
    private ContinuousSettings _continuousSettings;

    public invites(account acc, ContinuousSettings continuousSettings, DateTime? scheduled)
      : this(acc)
    {
      time = scheduled ?? DateTime.MinValue;
      _continuousSettings = continuousSettings;
    }

    public invites(account acc)
    {
      this.acc = acc;
      this.c = acc.c;
      this.CSRFToken = acc.CSRFtoken;
      this.app_version = acc.App_version;

      this.timeout_max = acc.setting_invite.TimeoutMax;
      this.timeout_min = acc.setting_invite.TimeoutMin;
      this.maxInvites = acc.setting_invite.Max;

      usedPins();
    }

    public bool Active { get { return active; } set { active = value; } }
    public bool Abort { get { return abort; } set { abort = value; } }
    public int Timeout_min { get { return timeout_min; } set { timeout_min = value; } }
    public int Timeout_max { get { return timeout_max; } set { timeout_max = value; } }
    public int MaxRepins { get { return maxInvites; } set { maxInvites = value; } }
    public static List<string> Categories { get { return categories; } set { categories = value; } }
    public string BoardID { get { return boardID; } set { boardID = value; } }


    public void start_thread()
    {
      Thread t = new Thread(Start_Invite);
      t.Start();
    }
    void Start_Invite()
    {
      active = true;
      while (time > DateTime.Now)
      {
        if (abort) { active = false; return; }

        status = "scheduled...";
        Thread.Sleep(5000);
      }
      status = "";
      while (active)
      {
        try
        {
          Random r = new Random();

          if (abort) { active = false; return; }

          List<string> dic = new List<string>();
          foreach (string k in acc.boards_category_mapped_invite.Keys)
          {
            if (k.Length > 0)
              dic.Add(k);
          }
          boardID = dic.ElementAt(r.Next(0, dic.Count));


          string RS = getNextPage_Search(true, null);

          string pat = "<meta property=\"followers\" name=\"followers\" content=\"(\\d+)\" data-app>";
          Match m = Regex.Match(RS, pat);
          if (int.Parse(m.Groups[1].Value.ToString()) <= 0)
          {
            status = "end!"; active = false; return;
          }


          string bookmark = null;
          int scrollsMax = 20;
          while ((bookmark = getBookmark(RS)).Length > 5 && scrollsMax > 0)
          {
            List<user> users = new List<user>();
            users = parseUsers(RS);
            for (int i = 0; i < users.Count; i++)
            {
              if (abort) { active = false; return; }

              user u = users[i];

              if (excludeInvite(ref u))
                continue;

              if (invited >= maxInvites && maxInvites > 0)
              {
                if (_continuousSettings.ContinuousRun)
                {
                  invited = 0;

                  var randomDelay = Helper.GetRandomTimeSpan(_continuousSettings.DelayFrom,
                                                               _continuousSettings.DelayTo);
                  while (randomDelay > TimeSpan.Zero)
                  {
                    status = string.Format("delayed for {0:D2}:{1:D2}:{2:D2}", randomDelay.Hours, randomDelay.Minutes, randomDelay.Seconds);
                    randomDelay -= new TimeSpan(0, 0, 1);
                    Thread.Sleep(new TimeSpan(0, 0, 1));
                    if (abort) { active = false; return; }
                  }
                  status = "";
                }
                else
                {
                  status = "max!"; active = false; return;
                }
              }

              doInvite(u);

              if (invited >= maxInvites && maxInvites > 0)
              {
                if (_continuousSettings.ContinuousRun)
                {
                  invited = 0;

                  var randomDelay = Helper.GetRandomTimeSpan(_continuousSettings.DelayFrom,
                                                               _continuousSettings.DelayTo);
                  while (randomDelay > TimeSpan.Zero)
                  {
                    status = string.Format("delayed for {0:D2}:{1:D2}:{2:D2}", randomDelay.Hours, randomDelay.Minutes, randomDelay.Seconds);
                    randomDelay -= new TimeSpan(0, 0, 1);
                    Thread.Sleep(new TimeSpan(0, 0, 1));
                    if (abort) { active = false; return; }
                  }
                  status = "";
                }
                else
                {
                  status = "max!"; active = false; return;
                }
              }
              // avoidingIterations = true;
            }

            if (abort) { active = false; return; }
            if (invited >= maxInvites && maxInvites > 0)
            {
              if (_continuousSettings.ContinuousRun)
              {
                invited = 0;

                var randomDelay = Helper.GetRandomTimeSpan(_continuousSettings.DelayFrom,
                                                             _continuousSettings.DelayTo);
                while (randomDelay > TimeSpan.Zero)
                {
                  status = string.Format("delayed for {0:D2}:{1:D2}:{2:D2}", randomDelay.Hours, randomDelay.Minutes, randomDelay.Seconds);
                  randomDelay -= new TimeSpan(0, 0, 1);
                  Thread.Sleep(new TimeSpan(0, 0, 1));
                  if (abort) { active = false; return; }
                }
                status = "";
              }
              else
              {
                status = "max!"; active = false; return;
              }
            }

            RS = getNextPage_Search(false, bookmark);

            if (abort) { active = false; return; }
            Thread.Sleep(r.Next(1000, 2000));
            --scrollsMax;
          }

          if (bookmark == null || bookmark.Length <= 5)
          {
            status = "end!"; active = false; return;
          }

        }
        catch
        {
          Random r = new Random();
          int sleepP = r.Next(5000, 10000);
          while (sleepP > 0)
          {
            Thread.Sleep(100); sleepP -= 100;
            if (abort) { active = false; return; }
          }
        }
      }
    }
    List<user> parseUsers(string RS)
    {
      if (invited >= maxInvites && maxInvites > 0)
      {
        if (_continuousSettings.ContinuousRun)
        {
          invited = 0;

          var randomDelay = Helper.GetRandomTimeSpan(_continuousSettings.DelayFrom,
                                                       _continuousSettings.DelayTo);
          while (randomDelay > TimeSpan.Zero)
          {
            status = string.Format("delayed for {0:D2}:{1:D2}:{2:D2}", randomDelay.Hours, randomDelay.Minutes, randomDelay.Seconds);
            randomDelay -= new TimeSpan(0, 0, 1);
            Thread.Sleep(new TimeSpan(0, 0, 1));
            if (abort) { active = false; return null; }
          }
          status = "";
        }
        else
        {
          status = "max!"; active = false; return null;
        }
      }
      if (abort) { active = false; return null; }

      string pattern = "";
      pattern += "\"username\": \"(.+?)\"(.+?)\"id\": \"(\\d+?)\"";

      List<user> users = new List<user>();
      foreach (Match m in Regex.Matches(RS.Substring(RS.LastIndexOf("\"errorStrategy\": 1 \"data\": [{"), RS.Length - RS.LastIndexOf("\"errorStrategy\": 1 \"data\": [{")), pattern, RegexOptions.Multiline))
      {
        if (abort) { active = false; return null; }

        user u = new user();
        u.Username = m.Groups[1].ToString();
        u.Userid = m.Groups[3].ToString();

        if (u.Username.Equals(acc.Username)) continue;
        bool dub = false;
        foreach (user uu in users)
        {
          if (u.Userid.Equals(uu.Userid))
          { dub = true; break; }
        }
        if (!dub)
          users.Add(u);

      }
      return users;

    }
    // bool avoidingIterations = true;
    void doInvite(user p)
    {
      try
      {
        if (abort) { active = false; return; }

        StringBuilder strBuilder = new StringBuilder();
        strBuilder.AppendLine(HttpUtility.UrlEncode("source_url=/" + acc.boards_url_names[boardID] + "/") + "&data=" + HttpUtility.UrlEncode("{\"options\":{\"board_id\":\"" + boardID + "\",\"invited_user_id\":\"" + p.Userid + "\"},\"context\":{}}") + "&module_path=" + HttpUtility.UrlEncode("Modal()>BoardEdit(resource=BoardResource(board_id=" + boardID + "))>BoardInviteForm()>ui.TypeaheadField(name=invite, exclude_non_followers=true, prefetch_on_focus=true, view_type=userSelect, populate_on_result_highlight=true, search_delay=0, placeholder=Type a name or email, tags=board_invites, template=userSelect)"));
        byte[] hdrbytes = System.Text.Encoding.UTF8.GetBytes(strBuilder.ToString());
        List<byte[]> data = new List<byte[]>();
        data.Add(hdrbytes);
        List<string> headers = new List<string> { "X-NEW-APP: 1", "X-APP-VERSION: " + app_version, "X-Requested-With: XMLHttpRequest", "X-CSRFToken: " + CSRFToken, "Accept-Encoding: gzip, deflate", "Accept-Language: en-gb,en;q=0.5", "Pragma: no-cache", "Cache-Control: no-cache" };
        if (abort) { active = false; return; }
        string RS = http.POST("http://www.pinterest.com/resource/BoardInviteResource/create/", "http://www.pinterest.com/" + acc.boards_url_names[boardID] + "/", "application/x-www-form-urlencoded; charset=UTF-8", data, headers, c, acc.proxy, "application/json, text/javascript, */*; q=0.01");

        if (abort) { active = false; return; }

        if (RS.Contains("[[[ERROR]]]")) //Expecting: You can't invite this person until you're following each other.
        {

          //++errors;
          //if (errors > ERRORS_MAX) { abort = true; }


          /*doFollowUser(p);
          if (avoidingIterations)
          {
              avoidingIterations = false;
              doInvite(p);
          }*/
          Random r = new Random();
          int sleepP = r.Next(10000, 20000);
          while (sleepP > 0)
          {
            Thread.Sleep(100); sleepP -= 100;
            if (abort) { active = false; return; }
          }
        }
        else
        {
          Console.WriteLine("invited:  " + p.Userid);
          ++invited;
          if (invited >= maxInvites && maxInvites > 0)
          {
            if (_continuousSettings.ContinuousRun)
            {
              invited = 0;

              var randomDelay = Helper.GetRandomTimeSpan(_continuousSettings.DelayFrom,
                                                           _continuousSettings.DelayTo);
              while (randomDelay > TimeSpan.Zero)
              {
                status = string.Format("delayed for {0:D2}:{1:D2}:{2:D2}", randomDelay.Hours, randomDelay.Minutes, randomDelay.Seconds);
                randomDelay -= new TimeSpan(0, 0, 1);
                Thread.Sleep(new TimeSpan(0, 0, 1));
                if (abort) { active = false; return; }
              }
              status = "";
            }
            else
            {
              status = "max!"; active = false; return;
            }
          }

          Random r = new Random();
          int sleepP = r.Next(timeout_min * 1000, timeout_max * 1000);
          while (sleepP > 0)
          {
            Thread.Sleep(100); sleepP -= 100;
            if (abort) { active = false; return; }
          }
        }
      }
      catch { }
    }
    string getNextPage_Search(bool first, string bookmark)
    {
      if (first)
        return http.GET("http://www.pinterest.com/" + acc.boards_url_names[boardID] + "/followers/", "http://www.pinterest.com/", c, null, acc.proxy).Replace(",", "");

      string url = "http://www.pinterest.com/resource/SearchResource/get/?source_url=";
      string referral = "http://www.pinterest.com/" + acc.boards_url_names[boardID] + "/followers/";
      string json = HttpUtility.UrlEncode("/" + acc.boards_url_names[boardID] + "/followers/") + "&data=" + HttpUtility.UrlEncode("{\"options\":{\"layout\":null,\"places\":null,\"constraint_string\":null,\"show_scope_selector\":true,\"scope\":\"boards\",\"query\":\"" + category + "\",\"bookmarks\":[\"" + bookmark + "\"]},\"context\":{},\"module\":{\"name\":\"GridItems\",\"options\":{\"scrollable\":true,\"show_grid_footer\":false,\"centered\":true,\"reflow_all\":true,\"virtualize\":true,\"item_options\":{\"show_pinner\":true,\"show_pinned_from\":false,\"show_board\":true},\"layout\":\"fixed_height\",\"track_item_impressions\":true}},\"render_type\":3,\"error_strategy\":1}"); ;
      json = Regex.Replace(json, "%\\d(\\w)", m => m.ToString().ToUpper());
      url += json;
      url += "&_=" + (GetTime() + 1);

      List<string> headers = new List<string>();
      headers.Add("X-NEW-APP: 1");
      headers.Add("X-APP-VERSION: " + app_version);
      headers.Add("X-Requested-With: XMLHttpRequest");
      headers.Add("Accept-Language: en-gb,en;q=0.5");
      headers.Add("Accept-Encoding: gzip, deflate");
      string RS = http.GET(url, referral, c, headers, acc.proxy).Replace(",", "").Replace("\\\"", "\"");

      Random r = new Random();
      int sleepP = r.Next(1000, 2000);
      while (sleepP > 0)
      {
        Thread.Sleep(100); sleepP -= 100;
        if (abort) { active = false; return null; }
      }

      return RS;
    }


    List<string> users_exclude = new List<string>();
    bool excludeInvite(ref user u)
    {
      if (users_exclude.Contains("Userid:" + u.Userid + "\t" + "Boardid:" + boardID)) return true;
      users_exclude.Add("Userid:" + u.Userid + "\t" + "Boardid:" + boardID);
      using (FileStream fs = new FileStream("./exclude_invites.txt", FileMode.Append, FileAccess.Write))
      using (StreamWriter sw = new StreamWriter(fs))
      {
        string s = "Userid:" + u.Userid + "\t" + "Boardid:" + boardID;
        sw.WriteLine(s);
      }
      return false;
    }
    void usedPins()
    {

      if (!File.Exists("./exclude_invites.txt"))
        using (var fileStream = File.Create("./exclude_invites.txt")) { }

      using (var reader = new StreamReader("./exclude_invites.txt"))
      {
        string line;
        while ((line = reader.ReadLine()) != null)
        {
          users_exclude.Add(line);
        }
      }
    }



    void doFollowUser(user u)
    {
      try
      {
        if (abort) { active = false; return; }
        //post
        string userid = u.Userid;
        string username = u.Username;
        StringBuilder strBuilder = new StringBuilder();
        strBuilder.AppendLine("source_url=/" + username + "/&data={\"options\":{\"user_id\":\"" + userid + "\"},\"context\":{}}&module_path=App()>UserProfilePage(resource=UserResource(username=" + username + "))>UserProfileContent(resource=UserResource(username=" + username + "))>Grid(resource=UserFollowingResource(username=" + username + "))>GridItems(resource=UserFollowingResource(username=" + username + "))>User(resource=UserResource(username=" + username + "))>UserFollowButton(followed=false, class_name=gridItem, unfollow_text=Unfollow, follow_ga_category=user_follow, unfollow_ga_category=user_unfollow, disabled=false, is_me=false, follow_class=default, log_element_type=62, text=Follow, user_id=" + userid + ", follow_text=Follow, color=default)");
        byte[] hdrbytes = System.Text.Encoding.UTF8.GetBytes(strBuilder.ToString());
        List<byte[]> data = new List<byte[]>();
        data.Add(hdrbytes);
        List<string> headers = new List<string> { "X-NEW-APP: 1", "X-APP-VERSION: " + app_version, "X-Requested-With: XMLHttpRequest", "X-CSRFToken: " + CSRFToken, "Accept-Encoding: gzip, deflate", "Accept-Language: en-gb,en;q=0.5", "Pragma: no-cache", "Cache-Control: no-cache" };
        if (abort) { active = false; return; }
        string RS = http.POST("http://www.pinterest.com/resource/UserFollowResource/create/", "http://www.pinterest.com/" + username + "/", "application/x-www-form-urlencoded; charset=UTF-8", data, headers, c, acc.proxy, "application/json, text/javascript, */*; q=0.01");
        if (abort) { active = false; return; }
        Console.WriteLine("followed for invite:  " + username);
        Random r = new Random();
        int sleepP = r.Next(5000, 10000);
        while (sleepP > 0)
        {
          Thread.Sleep(100); sleepP -= 100;
          if (abort) { active = false; return; }
        }
      }
      catch { }
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
    private string status = "";
    override public string ToString()
    {
      return "invited: " + invited + " " + status;
    }
  }
}
