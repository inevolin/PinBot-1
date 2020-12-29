using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Windows.Forms;

namespace PinBot
{
  class follow : followers
  {
    string category = "";
    private List<string> Categories = new List<string>();
    private List<user> users_to_follow = new List<user>();
    private int followed = 0;
    private int maxFollows = 50;
    private int errors = 0;
    private int thread_boards = 0, thread_users = 0;
    private int ERRORS_MAX = 30;
    private DateTime time;
    private ContinuousSettings _continuousSettings;

    public enum types { BOARDS, PEOPLE };

    public follow(account acc, ContinuousSettings continuousSettings, DateTime? scheduled)
      : this(acc)
    {
      time = scheduled ?? DateTime.MinValue;
      _continuousSettings = continuousSettings;
    }

    public follow(account acc)
      : base(acc)
    {
      this.timeout_max = acc.setting_follow.TimeoutMax;
      this.timeout_min = acc.setting_follow.TimeoutMin;
      this.maxFollows = acc.setting_follow.Max;

      Categories = acc.FollowCategories == null ? new List<string>() : acc.FollowCategories;
    }

    public void StartFollow_thread()
    {
      Thread t = new Thread(StartScrapeBoards);
      t.Start();

      t = new Thread(StartScrapeUsers);
      t.Start();

      t = new Thread(StartFollowCustom);
      t.Start();

      t = new Thread(pipeline);
      t.Start();
    }

    int pipeline_max = 20;//users_to_follow
    void pipeline()
    {
      active = true;

      while (time > DateTime.Now)
      {
        if (abort) { active = false; return; }

        status = "scheduled...";
        Thread.Sleep(5000);
      }

      while (active)
      {
        try
        {
          while (users_to_follow.Count < 5) // !!
          {
            Thread.Sleep(1000);

            if (followed >= maxFollows && maxFollows > 0)
            {
              if (_continuousSettings.ContinuousRun)
              {
                followed = 0;

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
            if (abort) { active = false; return; }
          }

          if (followed >= maxFollows && maxFollows > 0)
          {
            if (_continuousSettings.ContinuousRun)
            {
              followed = 0;

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
          if (abort) { active = false; return; }

          users_to_follow.Shuffle();
          user u = users_to_follow[0];
          users_to_follow.Remove(u);
          //if (userAlgo(u))
          {
            if (u.isBoard)
            {
              string boardID = getBoardIDbyName(u.Board_user_board.Replace(u.Board_username + "/", ""), u.Board_username);
              doFollowBoard(u, boardID);
            }
            else
              doFollowUser(u);

            if (followed >= maxFollows && maxFollows > 0)
            {
              if (_continuousSettings.ContinuousRun)
              {
                followed = 0;

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
            if (abort) { active = false; return; }
          }
        }
        catch { }
      }
    }

    //main following
    void StartScrapeBoards()
    {
      active = true;
      while (time > DateTime.Now)
      {
        if (abort) { active = false; return; }

        status = "scheduled...";
        Thread.Sleep(5000);
      }
      if (Categories.Count < 1)
        return;

      status = "";
      while (active)
      {
        try
        {
          Random r = new Random();

          if (abort) { active = false; return; }
          if (followed >= maxFollows && maxFollows > 0)
          {
            if (_continuousSettings.ContinuousRun)
            {
              followed = 0;

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

          category = Categories[r.Next(0, Categories.Count)];
          string RS = getNextPage_Search(true, null, types.BOARDS);

          string bookmark = null;
          int scrollsMax = 20;
          while ((bookmark = getBookmark(RS)).Length > 5 && scrollsMax > 0)
          {

            while (users_to_follow.Count >= pipeline_max)
            {
              Thread.Sleep(1000);
              if (abort) { active = false; return; }
            }


            if (followed >= maxFollows && maxFollows > 0)
            {
              if (_continuousSettings.ContinuousRun)
              {
                followed = 0;

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
            if (abort) { active = false; return; }



            Thread t = new Thread(() => followBoards(RS, true));
            t.IsBackground = true;
            t.Start();
            ++thread_boards;

            while (thread_boards >= 1)
              Thread.Sleep(1000);


            if (followed >= maxFollows && maxFollows > 0)
            {
              if (_continuousSettings.ContinuousRun)
              {
                followed = 0;

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
            if (abort) { active = false; return; }


            while (users_to_follow.Count >= pipeline_max)
            {
              Thread.Sleep(1000);
              if (abort) { active = false; return; }
            }


            RS = getNextPage_Search(false, bookmark, types.BOARDS);

            if (abort) { active = false; return; }
            if (followed >= maxFollows && maxFollows > 0)
            {
              if (_continuousSettings.ContinuousRun)
              {
                followed = 0;

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

            //Thread.Sleep(r.Next(1000, 2000));
            --scrollsMax;
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
    void StartScrapeUsers()
    {
      active = true;

      while (time > DateTime.Now)
      {
        if (abort) { active = false; return; }

        status = "scheduled...";
        Thread.Sleep(5000);
      }

      if (Categories.Count < 1)
        return;

      while (active)
      {
        try
        {
          Random r = new Random();

          if (abort) { active = false; return; }
          if (followed >= maxFollows && maxFollows > 0)
          {
            if (_continuousSettings.ContinuousRun)
            {
              followed = 0;

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

          category = Categories[r.Next(0, Categories.Count)];
          string RS = getNextPage_Search(true, null, types.PEOPLE);

          string bookmark = null;
          int scrollsMax = 20;
          while ((bookmark = getBookmark(RS)).Length > 5 && scrollsMax > 0)
          {

            while (users_to_follow.Count >= pipeline_max)
            {
              Thread.Sleep(1000);
              if (abort) { active = false; return; }
            }


            if (followed >= maxFollows && maxFollows > 0)
            {
              if (_continuousSettings.ContinuousRun)
              {
                followed = 0;

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
            if (abort) { active = false; return; }


            Thread t = new Thread(() => followUsers(RS, true));
            t.IsBackground = true;
            ++thread_users;

            while (thread_users >= 1)
              Thread.Sleep(1000);

            if (followed >= maxFollows && maxFollows > 0)
            {
              if (_continuousSettings.ContinuousRun)
              {
                followed = 0;

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
            if (abort) { active = false; return; }


            while (users_to_follow.Count >= pipeline_max)
            {
              Thread.Sleep(1000);
              if (abort) { active = false; return; }
            }


            RS = getNextPage_Search(false, bookmark, types.PEOPLE);

            if (abort) { active = false; return; }
            if (followed >= maxFollows && maxFollows > 0)
            {
              if (_continuousSettings.ContinuousRun)
              {
                followed = 0;

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

            //Thread.Sleep(r.Next(1000, 2000));
            --scrollsMax;
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
    string getNextPage_Search(bool first, string bookmark, types type) //first load  |  search page (true) else followers/following page
    {
      string sType = "";
      if (type == types.BOARDS)
        sType = "boards";
      else if (type == types.PEOPLE)
        sType = "people";

      if (first)
        return http.GET("http://www.pinterest.com/search/" + sType + "/?q=" + category, "http://www.pinterest.com/", c, null, acc.proxy).Replace(",", "");

      string url = "http://www.pinterest.com/resource/SearchResource/get/?source_url=";
      string referral = "http://www.pinterest.com/search/" + sType + "/?q=" + category;
      string json = HttpUtility.UrlEncode("/search/" + sType + "/?q=" + category) + "&data=" + HttpUtility.UrlEncode("{\"options\":{\"layout\":null,\"places\":null,\"constraint_string\":null,\"show_scope_selector\":true,\"scope\":\"boards\",\"query\":\"" + category + "\",\"bookmarks\":[\"" + bookmark + "\"]},\"context\":{},\"module\":{\"name\":\"GridItems\",\"options\":{\"scrollable\":true,\"show_grid_footer\":false,\"centered\":true,\"reflow_all\":true,\"virtualize\":true,\"item_options\":{\"show_pinner\":true,\"show_pinned_from\":false,\"show_board\":true},\"layout\":\"fixed_height\",\"track_item_impressions\":true}},\"render_type\":3,\"error_strategy\":1}"); ;
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

      /*Random r = new Random();
      int sleepP = r.Next(1000,2000);
      while (sleepP > 0)
      {
          Thread.Sleep(100); sleepP -= 100;
          if (abort) { active = false; return null; }
      }*/

      return RS;
    }

    //follow followers on custom defined usernames
    void StartFollowCustom()
    {
      active = true;

      while (time > DateTime.Now)
      {
        if (abort) { active = false; return; }

        status = "scheduled...";
        Thread.Sleep(5000);
      }

      if (acc.users_follow.Count < 1)
        return;

      Dictionary<string, string> custom_users = new Dictionary<string, string>(); //user and last bookmark
      foreach (string s in acc.users_follow)
        if (!custom_users.ContainsKey(s))
          custom_users.Add(s, "");

      Random r = new Random();

      user u = null;

      while (active)
      {
        try
        {
          string user = custom_users.ElementAt(r.Next(0, custom_users.Count)).Key;
          if ((custom_users.Count == 1 && u == null) || custom_users.Count > 1)
          {
            Match m = Regex.Match(user, "/(.+?)/(.+?)/");
            u = new user();
            u.Username = m.Groups[1].ToString();
            u = setUserData(u, "http://www.pinterest.com/");
          }

          string RS = "";
          string bookmark = null;
          if (custom_users[user].Length < 1)
            RS = getNextPage_Users(true, null, u, "followers");
          else
          {
            bookmark = custom_users[user];
            RS = getNextPage_Users(false, bookmark, u, "followers");
          }

          bookmark = getBookmark(RS);
          custom_users[user] = bookmark;

          int scrollsMax = 5;
          while ((bookmark = getBookmark(RS)).Length > 5 && scrollsMax > 0)
          {

            while (users_to_follow.Count >= pipeline_max)
            {
              Thread.Sleep(1000);
              if (abort) { active = false; return; }
            }

            if (followed >= maxFollows && maxFollows > 0)
            {
              if (_continuousSettings.ContinuousRun)
              {
                followed = 0;

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
            if (abort) { active = false; return; }

            followUsers(RS, false);

            if (followed >= maxFollows && maxFollows > 0)
            {
              if (_continuousSettings.ContinuousRun)
              {
                followed = 0;

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
            if (abort) { active = false; return; }

            --scrollsMax;

            if (scrollsMax > 0)
            {
              RS = getNextPage_Users(false, bookmark, u, "followers");
              bookmark = getBookmark(RS);
            }
            custom_users[user] = bookmark;
          }
        }
        catch { }
      }
    }

    //following the followers : potentials of searched ones
    void followTheFollowers(user u)
    {
      try
      {
        string RS = getNextPage_Users(true, null, u, "followers");

        string bookmark = null;
        int scrollsMax = 20;
        while ((bookmark = getBookmark(RS)).Length > 5 && scrollsMax > 0)
        {

          while (users_to_follow.Count >= pipeline_max)
          {
            Thread.Sleep(1000);
            if (abort) { active = false; return; }
          }

          if (followed >= maxFollows && maxFollows > 0)
          {
            if (_continuousSettings.ContinuousRun)
            {
              followed = 0;

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
          if (abort) { active = false; return; }

          followUsers(RS, false);

          if (followed >= maxFollows && maxFollows > 0)
          {
            if (_continuousSettings.ContinuousRun)
            {
              followed = 0;

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
          if (abort) { active = false; return; }

          RS = getNextPage_Users(false, bookmark, u, "followers");
          --scrollsMax;
        }
      }
      catch { }
    }
    string getNextPage_Users(bool first, string bookmark, user u, string list) // list eg:  followers
    {
      /*if (first)
          return http.GET("http://www.pinterest.com/" + u.Username + "/" +  list + "/", "http://www.pinterest.com/", c, null, acc.proxy).Replace(",", "");
      */
      string url, referral, json, RS;
      List<string> headers = new List<string>();

      if (first)
      {

        // http://www.pinterest.com/resource/UserResource/get/?source_url=/oscarprgirl/&data={"options":{"username":"oscarprgirl"},"context":{},"module":{"name":"UserProfileContent","options":{"tab":"boards"}},"render_type":1,"error_strategy":0}&_=1416253936523

        url = "http://www.pinterest.com/resource/UserResource/get/?source_url=";
        referral = "http://www.pinterest.com/" + u.Username + "/";
        json = HttpUtility.UrlEncode("/" + u.Username + "/" + list + "/") + "&data=" + HttpUtility.UrlEncode("{\"options\":{\"username\":\"" + u.Username + "\"},\"context\":{},\"module\":{\"name\":\"UserProfileContent\",\"options\":{\"tab\":\"" + list + "\"}},\"render_type\":1,\"error_strategy\":0}"); ;
        json = Regex.Replace(json, "%\\d(\\w)", m => m.ToString().ToUpper());
        url += json;
        url += "&_=" + (GetTime() + 1);

        headers.Add("X-NEW-APP: 1");
        headers.Add("X-APP-VERSION: " + app_version);
        headers.Add("X-Requested-With: XMLHttpRequest");
        headers.Add("Accept-Language: en-gb,en;q=0.5");
        headers.Add("Accept-Encoding: gzip, deflate");
        RS = http.GET(url, referral, c, headers, acc.proxy);
        RS = RS.Replace(",", "");

        return RS;
      }

      url = "http://www.pinterest.com/resource/UserFollowersResource/get/?source_url=";
      referral = "http://www.pinterest.com/" + u.Username + "/" + list + "/";
      json = HttpUtility.UrlEncode("/" + u.Username + "/" + list + "/") + "&data=" + HttpUtility.UrlEncode("{\"options\":{\"username\":\"" + u.Username + "\",\"bookmarks\":[\"" + bookmark + "\"]},\"context\":{},\"module\":{\"name\":\"GridItems\",\"options\":{\"scrollable\":true,\"show_grid_footer\":false,\"centered\":true,\"reflow_all\":true,\"virtualize\":true,\"layout\":\"fixed_height\"}},\"render_type\":3,\"error_strategy\":1}") + "&module_path=" + HttpUtility.UrlEncode("FollowerCount(url=/" + u.Username + "/followers/, class_name=\"\", element_type=a, text=" + u.IntFollowers + " Followers, borderless=true, resource=UserResource(username=" + u.Username + "))"); ;
      json = Regex.Replace(json, "%\\d(\\w)", m => m.ToString().ToUpper());
      url += json;
      url += "&_=" + (GetTime() + 1);


      headers.Add("X-NEW-APP: 1");
      headers.Add("X-APP-VERSION: " + app_version);
      headers.Add("X-Requested-With: XMLHttpRequest");
      headers.Add("Accept-Language: en-gb,en;q=0.5");
      headers.Add("Accept-Encoding: gzip, deflate");
      if (abort) { active = false; return null; }
      RS = http.GET(url, referral, c, headers, acc.proxy).Replace(",", "").Replace("\\\"", "\"");

      /*Random r = new Random();
      int sleepP = r.Next(1000, 2000);
      while (sleepP > 0)
      {
          Thread.Sleep(100); sleepP -= 100;
          if (abort) { active = false; return null; }
      }*/

      return RS;
    }

    //following boards based on boards-type-lists
    List<user> followBoards(string RS, bool goDeep)
    {
      List<user> users = new List<user>();
      try
      {
        if (followed >= maxFollows && maxFollows > 0)
        {
          if (_continuousSettings.ContinuousRun)
          {
            followed = 0;

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
        pattern += "<a href=\"/(.+?)/\" class=\"boardLinkWrapper\""; //1  (  username/boardname )

        foreach (Match m in Regex.Matches(RS, pattern, RegexOptions.Multiline))
        {
          while (users_to_follow.Count >= pipeline_max)
          {
            Thread.Sleep(1000);
            if (abort) { active = false; return null; }
          }

          if (abort) { active = false; return null; }
          if (followed >= maxFollows && maxFollows > 0)
          {
            if (_continuousSettings.ContinuousRun)
            {
              followed = 0;

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

          string user_board = m.Groups[1].ToString();
          string username = user_board.Substring(0, user_board.IndexOf("/"));

          user u = new user();
          u.Username = username;
          u.isBoard = true;
          u.Board_user_board = user_board;
          u.Board_username = username;

          u = setUserData(u, "http://www.pinterest.com/search/boards/?q=" + category);
          if (goDeep)
            followTheFollowers(u); // follow this user's followers
          users.Add(u);
          if (!userAlgo(u))
            continue;

          users_to_follow.Add(u);
          while (users_to_follow.Count >= pipeline_max)
          {
            Thread.Sleep(1000);
            if (abort) { active = false; return null; }
          }

          if (followed >= maxFollows && maxFollows > 0)
          {
            if (_continuousSettings.ContinuousRun)
            {
              followed = 0;

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

        }
      }
      catch { }
      finally { --thread_boards; }
      return users;
    }
    string getBoardIDbyName(string boardname, string username)
    {
      string RS = http.GET("http://www.pinterest.com/" + username + "/" + boardname + "/", "http://www.pinterest.com/" + username + "/", c, null, acc.proxy).Replace(",", "");
      Match m = Regex.Match(RS, "\"board_id\": \"(\\d+?)\"");
      return m.Groups[1].ToString();
    }
    void doFollowBoard(user u, string boardid)
    {
      try
      {
        if (abort) { active = false; return; }
        //post
        string userid = u.Userid;
        string username = u.Username;
        StringBuilder strBuilder = new StringBuilder();
        strBuilder.AppendLine("source_url=/" + username + "/&data={\"options\":{\"board_id\":\"" + boardid + "\"},\"context\":{}}&module_path=App()>UserProfilePage(resource=UserResource(username=" + username + "))>UserProfileContent(resource=UserResource(username=" + username + "))>UserBoards()>Grid(resource=ProfileBoardsResource(username=" + username + "))>GridItems(resource=ProfileBoardsResource(username=" + username + "))>Board(resource=BoardResource(board_id=" + boardid + "))>BoardFollowButton(board_id=" + boardid + ", followed=false, class_name=boardFollowUnfollowButton, unfollow_text=Unfollow, follow_ga_category=board_follow, unfollow_ga_category=board_unfollow, disabled=false, color=default, log_element_type=37, text=Follow, user_id=" + userid + ", follow_text=Follow, follow_class=default, is_my_board=null)");
        byte[] hdrbytes = System.Text.Encoding.UTF8.GetBytes(strBuilder.ToString());
        List<byte[]> data = new List<byte[]>();
        data.Add(hdrbytes);
        List<string> headers = new List<string> { "X-NEW-APP: 1", "X-APP-VERSION: " + app_version, "X-Requested-With: XMLHttpRequest", "X-CSRFToken: " + CSRFToken, "Accept-Encoding: gzip, deflate", "Accept-Language: en-gb,en;q=0.5", "Pragma: no-cache", "Cache-Control: no-cache" };
        if (abort) { active = false; return; }
        string RS = http.POST("http://www.pinterest.com/resource/BoardFollowResource/create/", "http://www.pinterest.com/" + username + "/", "application/x-www-form-urlencoded; charset=UTF-8", data, headers, c, acc.proxy, "application/json, text/javascript, */*; q=0.01");
        if (abort) { active = false; return; }
        if (!RS.Contains("[[[ERROR]]]"))
        {
          Console.WriteLine("followed:  " + username);
          ++followed;
          if (followed >= maxFollows && maxFollows > 0)
          {
            if (_continuousSettings.ContinuousRun)
            {
              followed = 0;

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
        else if (RS.Contains("You've reached your following limit"))
        {
          MessageBox.Show("You've reached your following limit.");
          abort = true; status = "ABORT!";
        }
        else
        {
          ++errors;
          if (errors > ERRORS_MAX) { abort = true; status = "ERROR!"; }
        }
      }
      catch { }
    }

    //following users based on users-type-lists
    List<user> followUsers(string RS, bool goDeep)
    {
      List<user> users = new List<user>();
      try
      {
        if (followed >= maxFollows && maxFollows > 0)
        {
          if (_continuousSettings.ContinuousRun)
          {
            followed = 0;

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


        users = followUsers_pattern1(RS, goDeep);
        users = followUsers_pattern2(RS, goDeep);


      }
      catch { }
      finally { --thread_users; }
      return users;
    }
    List<user> followUsers_pattern1(string RS, bool goDeep)
    {
      List<user> users = new List<user>();
      string pattern = "";
      pattern += "<a href=\\\\\"/(.+?)/\\\\\" class=\\\\\"(userWrapper|boardLinkWrapper)\\\\\""; //1  ( username )

      foreach (Match m in Regex.Matches(RS, pattern, RegexOptions.Multiline))
      {
        while (users_to_follow.Count >= pipeline_max)
        {
          Thread.Sleep(1000);
          if (abort) { active = false; return null; }
        }

        if (abort) { active = false; return null; }
        if (followed >= maxFollows && maxFollows > 0)
        {
          if (_continuousSettings.ContinuousRun)
          {
            followed = 0;

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

        user u = new user();
        u.Username = m.Groups[1].ToString();

        u = setUserData(u, "http://www.pinterest.com/search/boards/?q=" + category);
        if (goDeep)
          followTheFollowers(u); // follow this user's followers
        users.Add(u);
        if (!userAlgo(u))
          continue;

        users_to_follow.Add(u);
        while (users_to_follow.Count >= pipeline_max)
        {
          Thread.Sleep(1000);
          if (abort) { active = false; return null; }
        }

        if (abort) { active = false; return null; }
        if (followed >= maxFollows && maxFollows > 0)
        {
          if (_continuousSettings.ContinuousRun)
          {
            followed = 0;

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
      }
      return users;
    }
    List<user> followUsers_pattern2(string RS, bool goDeep)
    {
      List<user> users = new List<user>();
      string pattern = "";
      pattern += "<a href=\"/(.+?)/\" class=\"(userWrapper|boardLinkWrapper)\""; //1  ( username )

      foreach (Match m in Regex.Matches(RS, pattern, RegexOptions.Multiline))
      {
        while (users_to_follow.Count >= pipeline_max)
        {
          Thread.Sleep(1000);
          if (abort) { active = false; return null; }
        }

        if (abort) { active = false; return null; }
        if (followed >= maxFollows && maxFollows > 0)
        {
          if (_continuousSettings.ContinuousRun)
          {
            followed = 0;

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

        user u = new user();
        u.Username = m.Groups[1].ToString();

        u = setUserData(u, "http://www.pinterest.com/search/boards/?q=" + category);
        if (goDeep)
          followTheFollowers(u); // follow this user's followers
        users.Add(u);
        if (!userAlgo(u))
          continue;

        users_to_follow.Add(u);
        while (users_to_follow.Count >= pipeline_max)
        {
          Thread.Sleep(1000);
          if (abort) { active = false; return null; }
        }

        if (abort) { active = false; return null; }
        if (followed >= maxFollows && maxFollows > 0)
        {
          if (_continuousSettings.ContinuousRun)
          {
            followed = 0;

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
      }
      return users;
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
        if (!RS.Contains("[[[ERROR]]]"))
        {
          Console.WriteLine("followed:  " + username);
          ++followed;
          if (followed >= maxFollows && maxFollows > 0)
          {
            if (_continuousSettings.ContinuousRun)
            {
              followed = 0;

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
        else if (RS.Contains("You've reached your following limit"))
        {
          MessageBox.Show("You've reached your following limit.");
          abort = true; status = "ABORT!";
        }
        else
        {
          ++errors;
          if (errors > ERRORS_MAX) { abort = true; status = "ERROR!"; }
        }
      }
      catch { }
    }

    //methods
    public int algo_MaxFollowers, algo_MinFollowers;
    public int algo_MaxFollowing, algo_MinFollowing;
    public int algo_MaxPins, algo_MinPins;
    public int algo_MaxBoards, algo_MinBoards;
    public bool algo_MustHaveWebsite, algo_MustBePartner;
    public bool algo_MustHaveFB, algo_MustHaveTW;
    public bool ignoreCriteria = false;
    bool userAlgo(user u)
    {
      if (ignoreCriteria)
        return true;

      if (u.Following)
        return false;
      if (u.IntFollowers < algo_MinFollowers || u.IntFollowers > algo_MaxFollowers)
        return false;
      if (u.IntFollowing < algo_MinFollowing || u.IntFollowing > algo_MaxFollowing)
        return false;
      if (u.IntPins < algo_MinPins || u.IntPins > algo_MaxPins)
        return false;
      if (u.IntBoards < algo_MinBoards || u.IntBoards > algo_MaxBoards)
        return false;
      if (u.Website == null && algo_MustHaveWebsite)
        return false;
      if (!u.Partner && algo_MustBePartner)
        return false;
      if ((u.FB == null && algo_MustHaveFB) && (u.TW == null && algo_MustHaveTW))
        return false;

      return true;
    }

    string status = "";
    override public string ToString()
    {
      return "followed: " + followed + "  " + status;
    }


  }
}
