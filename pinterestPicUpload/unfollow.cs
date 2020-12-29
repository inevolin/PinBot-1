using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;

namespace PinBot
{
  class unfollow : followers
  {
    private int unfollowed = 0;
    public int maxUnfollows = 50;
    private int errors = 0;
    private int ERRORS_MAX = 30;
    private DateTime time;
    private ContinuousSettings _continuousSettings;

    List<string> myFollowers = new List<string>();//usernames

    public unfollow(account acc, ContinuousSettings continuousSettings, DateTime? scheduled)
      : this(acc)
    {
      time = scheduled ?? DateTime.MinValue;
      _continuousSettings = continuousSettings;
    }

    public unfollow(account acc)
      : base(acc)
    {
      this.timeout_max = acc.setting_unfollow.TimeoutMax;
      this.timeout_min = acc.setting_unfollow.TimeoutMin;
      this.maxUnfollows = acc.setting_unfollow.Max;
    }

    public void StartUnfollow_thread()
    {
      Thread t = new Thread(StartUnfollow);
      t.Start();
    }
    void StartUnfollow()
    {
      active = true;

      while (time > DateTime.Now)
      {
        if (abort) { active = false; return; }

        status = "scheduled...";
        Thread.Sleep(5000);
      }

      loadMyFollowers();
      status = "";
      while (active)
      {
        try
        {
          if (abort) { active = false; return; }
          string RS = getNextPage(true, null);

          string bookmark = null;
          int scrollsMax = 20;
          while ((bookmark = getBookmark(RS)).Length > 5 && scrollsMax > 0)
          {
            if (unfollowed >= maxUnfollows && maxUnfollows > 0)
            {
              if (_continuousSettings.ContinuousRun)
              {
                unfollowed = 0;

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

            unfollowUsers(RS);

            RS = getNextPage(false, bookmark);

            if (unfollowed >= maxUnfollows && maxUnfollows > 0)
            {
              if (_continuousSettings.ContinuousRun)
              {
                unfollowed = 0;

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

            Random r = new Random();
            Thread.Sleep(r.Next(1000, 2000));
            --scrollsMax;
          }
        }
        catch
        {
          Random r = new Random();
          int sleepP = r.Next(1000, 4000);
          while (sleepP > 0)
          {
            Thread.Sleep(100); sleepP -= 100;
            if (abort) { active = false; return; }
          }
        }
      }

    }
    void loadMyFollowers()
    {
      try
      {
        string bookmark = null;
        if (algo_UnfollowIfNotFollowingMe)
        {
          if (abort) { active = false; return; }
          string RSS = getNextPage_followers(true, bookmark);

          while ((bookmark = getBookmark(RSS)).Length > 5)
          {
            if (abort) { active = false; return; }
            string pattern = "";
            pattern += "<a href=\"/(.+?)/\" class=\"(userWrapper|boardLinkWrapper)\""; //1  ( username )

            List<user> users = new List<user>();
            foreach (Match m in Regex.Matches(RSS, pattern, RegexOptions.Multiline))
            {
              if (abort) { active = false; return; }
              myFollowers.Add(m.Groups[1].ToString());
            }
            Thread.Sleep(1500);
            RSS = getNextPage_followers(false, bookmark);
          }
        }
      }
      catch { }
    }

    string getNextPage(bool first, string bookmark) // list eg:  following
    {


      string url = "";
      string referral = "http://www.pinterest.com/" + acc.Username + "/following/";
      string json = "";
      if (first)
      {
        url = "http://www.pinterest.com/resource/UserFollowingResource/get/?source_url=";

        json = HttpUtility.UrlEncode("/" + acc.Username + "/following/") + "&data=" + HttpUtility.UrlEncode("{\"options\":{\"username\":\"" + acc.Username + "\"},\"context\":{},\"module\":{\"name\":\"Grid\",\"options\":{\"scrollable\":true,\"show_grid_footer\":false,\"centered\":true,\"reflow_all\":true,\"virtualize\":true,\"item_options\":{},\"layout\":\"fixed_height\"}},\"render_type\":1,\"error_strategy\":0}") + "&module_path=" + HttpUtility.UrlEncode("App()>UserProfilePage(resource=UserResource(username=" + acc.Username + "))>UserProfileContent(resource=UserResource(username=" + acc.Username + "))>FollowingSwitcher()>Button(class_name=navScopeBtn, text=Pinners, element_type=a, rounded=false)");
      }
      else
      {
        url = "http://www.pinterest.com/resource/UserFollowingResource/get/?source_url=";

        json = HttpUtility.UrlEncode("/" + acc.Username + "/following/") + "&data=" + HttpUtility.UrlEncode("{\"options\":{\"username\":\"" + acc.Username + "\",\"bookmarks\":[\"" + bookmark + "\"]},\"context\":{},\"module\":{\"name\":\"GridItems\",\"options\":{\"scrollable\":true,\"show_grid_footer\":false,\"centered\":true,\"reflow_all\":true,\"virtualize\":true,\"layout\":\"fixed_height\"}},\"render_type\":3,\"error_strategy\":1}");
      }



      json = Regex.Replace(json, "%\\d(\\w)", m => m.ToString().ToUpper());
      url += json;
      url += "&_=" + (GetTime() + 1);

      List<string> headers = new List<string>();
      headers.Add("X-NEW-APP: 1");
      headers.Add("X-APP-VERSION: " + app_version);
      headers.Add("X-Requested-With: XMLHttpRequest");
      headers.Add("Accept-Language: en-gb,en;q=0.5");
      headers.Add("Accept-Encoding: gzip, deflate");
      if (abort) { active = false; return null; }
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
    string getNextPage_followers(bool first, string bookmark) // list eg:  following
    {
      if (first)
        return http.GET("http://www.pinterest.com/" + acc.Username + "/followers/", "http://www.pinterest.com/" + acc.Username + "/", c, null, acc.proxy).Replace(",", "");

      string url = "http://www.pinterest.com/resource/UserFollowersResource/get/?source_url=";
      string referral = "http://www.pinterest.com/" + acc.Username + "/followers/";
      string json = HttpUtility.UrlEncode("/" + acc.Username + "/followers/") + "&data=" + HttpUtility.UrlEncode("{\"options\":{\"username\":\"" + acc.Username + "\",\"bookmarks\":[\"" + bookmark + "\"]},\"context\":{},\"module\":{\"name\":\"GridItems\",\"options\":{\"scrollable\":true,\"show_grid_footer\":false,\"centered\":true,\"reflow_all\":true,\"virtualize\":true,\"layout\":\"fixed_height\"}},\"render_type\":3,\"error_strategy\":1}");
      json = Regex.Replace(json, "%\\d(\\w)", m => m.ToString().ToUpper());
      url += json;
      url += "&_=" + (GetTime() + 1);

      List<string> headers = new List<string>();
      headers.Add("X-NEW-APP: 1");
      headers.Add("X-APP-VERSION: " + app_version);
      headers.Add("X-Requested-With: XMLHttpRequest");
      headers.Add("Accept-Language: en-gb,en;q=0.5");
      headers.Add("Accept-Encoding: gzip, deflate");
      if (abort) { active = false; return null; }
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
    void unfollowUsers(string RS)
    {
      if (unfollowed >= maxUnfollows && maxUnfollows > 0)
      {
        if (_continuousSettings.ContinuousRun)
        {
          unfollowed = 0;

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

      string pattern = "";
      pattern += "<a\\shref=\"\\/((?!find_friends).+?)\\/\"\\sclass=\"userWrapper\" data-element-type=\"64\""; //1  ( username )

      foreach (Match m in Regex.Matches(RS, pattern))
      {
        if (unfollowed >= maxUnfollows && maxUnfollows > 0)
        {
          if (_continuousSettings.ContinuousRun)
          {
            unfollowed = 0;

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

        user u = new user();
        u.Username = m.Groups[1].ToString();
        if (abort) { active = false; return; }
        u = setUserData(u, "http://www.pinterest.com/" + acc.Username + "/following/");

        if (algo_UnfollowIfNotFollowingMe && !myFollowers.Contains(u.Username) && myFollowers.Count > 0)
        {
          if (abort) { active = false; return; }
          doUnfollowUser(u);
        }
        else
        {
          if (!userAlgo(u))
            continue;
          if (abort) { active = false; return; }
          doUnfollowUser(u);
        }

      }
    }

    public int algo_MaxFollowers, algo_MinFollowers;
    public int algo_MaxFollowing, algo_MinFollowing;
    public int algo_MaxPins, algo_MinPins;
    public int algo_MaxBoards, algo_MinBoards;
    public bool algo_UnfollowIfHasWebsite, algo_UnfollowIfPartner, algo_UnfollowIfNotFollowingMe;
    bool userAlgo(user u)
    {

      if (!u.Following)
        return false;
      if (u.IntFollowers < algo_MinFollowers || u.IntFollowers > algo_MaxFollowers)
        return true;
      if (u.IntFollowing < algo_MinFollowing || u.IntFollowing > algo_MaxFollowing)
        return true;
      if (u.IntPins < algo_MinPins || u.IntPins > algo_MaxPins)
        return true;
      if (u.IntBoards < algo_MinBoards || u.IntBoards > algo_MaxBoards)
        return true;

      if (u.Website != null && algo_UnfollowIfHasWebsite)
        return true;
      if (u.Partner && algo_UnfollowIfPartner)
        return true;
      if (!u.Following_me && algo_UnfollowIfNotFollowingMe)
        return true;

      return false;
    }
    void doUnfollowUser(user u)
    {
      try
      {
        if (abort) { active = false; return; }
        //post
        string userid = u.Userid;
        string username = u.Username;
        StringBuilder strBuilder = new StringBuilder();
        
        strBuilder.AppendLine("source_url=/" + acc.Username + "/following/&data={\"options\":{\"user_id\":\"" + userid + "\"},\"context\":{}}&module_path=App()>UserProfilePage(resource=UserResource(username=" + acc.Username + "))>UserProfileContent(resource=UserResource(username=" + acc.Username + "))>Grid(resource=UserFollowingResource(username=" + acc.Username + "))>GridItems(resource=UserFollowingResource(username=" + acc.Username + "))>User(resource=UserResource(username=" + username + "))>UserFollowButton(followed=true, class_name=gridItem, unfollow_text=Unfollow, follow_ga_category=user_follow, unfollow_ga_category=user_unfollow, disabled=false, is_me=false, follow_class=default, log_element_type=62, text=Unfollow, user_id=" + userid + ", follow_text=Follow, color=dim)");
        byte[] hdrbytes = System.Text.Encoding.UTF8.GetBytes(strBuilder.ToString());
        List<byte[]> data = new List<byte[]>();
        data.Add(hdrbytes);
        List<string> headers = new List<string> { "X-NEW-APP: 1", "X-APP-VERSION: " + app_version, "X-Requested-With: XMLHttpRequest", "X-CSRFToken: " + CSRFToken, "Accept-Encoding: gzip, deflate", "Accept-Language: en-gb,en;q=0.5", "Pragma: no-cache", "Cache-Control: no-cache" };
        if (abort) { active = false; return; }
        string RS = http.POST("http://www.pinterest.com/resource/UserFollowResource/delete/", "http://www.pinterest.com/" + acc.Username + "/following/", "application/x-www-form-urlencoded; charset=UTF-8", data, headers, c, acc.proxy, "application/json, text/javascript, */*; q=0.01");
        if (abort) { active = false; return; }
        if (!RS.Contains("[[[ERROR]]]"))
        {
          Console.WriteLine("unfollowed:  " + username);
          ++unfollowed;

          if (unfollowed >= maxUnfollows && maxUnfollows > 0)
          {
            if (_continuousSettings.ContinuousRun)
            {
              unfollowed = 0;

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

          Random r = new Random();
          int sleepP = r.Next(timeout_min * 1000, timeout_max * 1000);
          while (sleepP > 0)
          {
            Thread.Sleep(100); sleepP -= 100;
            if (abort) { active = false; return; }
          }
        }
        else
        {
          ++errors;
          if (errors > ERRORS_MAX) { abort = true; status = "ERROR!"; }
        }
      }
      catch { }
    }


    string status = "";
    override public string ToString()
    {
      return "unfollowed: " + unfollowed + "  " + status;
    }

  }
}
