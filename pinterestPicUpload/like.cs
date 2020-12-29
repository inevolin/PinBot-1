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
  class like
  {
    private CookieContainer c = new CookieContainer();
    private account acc;
    private string app_version = "", CSRFToken = "";
    private bool active = false;
    private bool abort = false;
    private int timeout_min = 60;
    private int timeout_max = 300;
    string category = "";
    private List<string> categories = new List<string>();
    private List<string> users_to_like = new List<string>();
    private List<Pin> pins_pipeline = new List<Pin>();
    private int liked = 0;
    private int maxLikes = 50;
    private int errors = 0;
    private int ERRORS_MAX = 30;
    private DateTime time;
    private ContinuousSettings _continuousSettings;

    public like(account acc, ContinuousSettings continuousSettings, DateTime? scheduled) : this (acc)
    {
      time = scheduled ?? DateTime.MinValue;
      _continuousSettings = continuousSettings;
    }

    public like(account acc)
    {
      this.acc = acc;
      this.c = acc.c;
      this.CSRFToken = acc.CSRFtoken;
      this.app_version = acc.App_version;

      this.timeout_max = acc.setting_like.TimeoutMax;
      this.timeout_min = acc.setting_like.TimeoutMin;
      this.maxLikes = acc.setting_like.Max;

      categories = acc.LikeCategories == null ? new List<string>() : acc.LikeCategories;
      users_to_like = acc.users_like == null ? new List<string>() : acc.users_like;

      usedPins();
    }

    public bool Active { get { return active; } set { active = value; } }
    public bool Abort { get { return abort; } set { abort = value; } }
    public int Timeout_min { get { return timeout_min; } set { timeout_min = value; } }
    public int Timeout_max { get { return timeout_max; } set { timeout_max = value; } }
    public int MaxLikes { get { return maxLikes; } set { maxLikes = value; } }

    public void start_thread()
    {
      Thread t = new Thread(start_like);
      t.Start();

      t = new Thread(start_like_custom);
      t.Start();

      t = new Thread(pipeline);
      t.Start();

      //pipeline
    }

    List<string> pins_exclude = new List<string>();
    bool excludedPin(Pin pin)
    {
      if (pins_exclude.Contains("id:" + pin.id))
        return true;
      else
        return false;
    }
    void excludePin(Pin pin)
    {
      if (pins_exclude.Contains("id:" + pin.id)) return;
      pins_exclude.Add("id:" + pin.id);
      using (FileStream fs = new FileStream("./exclude_likes.txt", FileMode.Append, FileAccess.Write))
      using (StreamWriter sw = new StreamWriter(fs))
      {
        string s = "id:" + pin.id;
        sw.WriteLine(s);
      }
    }
    void usedPins()
    {

      if (!File.Exists("./exclude_likes.txt"))
        using (var fileStream = File.Create("./exclude_likes.txt")) { }

      using (var reader = new StreamReader("./exclude_likes.txt"))
      {
        string line;
        while ((line = reader.ReadLine()) != null)
        {
          pins_exclude.Add(line);
        }
      }
    }

    //pipeline
    int pipline_max = 10;
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
          while (pins_pipeline.Count < 1)
          {
            Thread.Sleep(1000);
            if (liked >= maxLikes && maxLikes > 0)
            {
              if (_continuousSettings.ContinuousRun)
              {
                liked = 0;

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

          if (liked >= maxLikes && maxLikes > 0)
          {
            if (_continuousSettings.ContinuousRun)
            {
              liked = 0;

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

          pins_pipeline.Shuffle();
          Pin u = pins_pipeline[0];
          pins_pipeline.Remove(u);

          doLike(u);
          excludePin(u);
        }
        catch { }
      }
    }

    private void start_like()
    {
      active = true;
      while (time > DateTime.Now)
      {
        if (abort) { active = false; return; }

        status = "scheduled...";
        Thread.Sleep(5000);
      }
      if (categories.Count < 1)
        return;

      Random r = new Random();
      Dictionary<string, string> dic = new Dictionary<string, string>(); // category and bookmark
      foreach (string s in categories)
        if (!dic.ContainsKey(s))
          dic.Add(s, "");


      category = dic.ElementAt(r.Next(0, dic.Count)).Key;
      string bookmark = dic[category];

      status = "";
      while (active)
      {
        try
        {
          while (pins_pipeline.Count >= pipline_max)
          {
            Thread.Sleep(1000);
            if (liked >= maxLikes && maxLikes > 0)
            {
              if (_continuousSettings.ContinuousRun)
              {
                liked = 0;

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

          if (abort) { active = false; return; }


          string RS = "";
          if (dic[category].Length < 1)
            RS = getNextPage_Search(true, null);
          else
          {
            bookmark = dic[category];
            RS = getNextPage_Search(false, bookmark);
          }
          bookmark = getBookmark(RS);
          dic[category] = bookmark;



          int scrollsMax = 5;
          int liked_session = 0;
          while ((bookmark = getBookmark(RS)).Length > 5 && scrollsMax > 0 && liked_session < 5)
          {
            List<Pin> pins = new List<Pin>();
            pins = parsePins(RS);

            for (int i = 0; i < pins.Count; i++)
            {

              if (liked >= maxLikes && maxLikes > 0)
              {
                if (_continuousSettings.ContinuousRun)
                {
                  liked = 0;

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

              Pin p = pins[i];

              if (abort) { active = false; return; }

              if (excludedPin(p))
                continue;

              // if ( ! p.liked_by_me)
              //if (doLike(p))
              pins_pipeline.Add(p);
              ++liked_session;

              while (pins_pipeline.Count >= pipline_max)
              {
                Thread.Sleep(1000);
                if (liked >= maxLikes && maxLikes > 0)
                {
                  if (_continuousSettings.ContinuousRun)
                  {
                    liked = 0;

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

              if (liked >= maxLikes && maxLikes > 0)
              {
                if (_continuousSettings.ContinuousRun)
                {
                  liked = 0;

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

            if (abort) { active = false; return; }

            if (scrollsMax > 0)
            {
              RS = getNextPage_Search(false, bookmark);
              bookmark = getBookmark(RS);
            }
            dic[category] = bookmark;

            if (abort) { active = false; return; }
            Thread.Sleep(r.Next(1000, 2000));
            --scrollsMax;
          }


        }
        catch { }
      }
    }
    private void start_like_custom()
    {
      active = true;

      while (time > DateTime.Now)
      {
        if (abort) { active = false; return; }

        status = "scheduled...";
        Thread.Sleep(5000);
      }

      if (users_to_like.Count < 1)
        return;

      Random r = new Random();
      Dictionary<string, string> custom_like = new Dictionary<string, string>(); //  /user/board/ and bookmark
      Dictionary<string, string> custom_board_map = new Dictionary<string, string>(); //  /user/board/ and boardID
      foreach (string s in users_to_like)
        if (!custom_like.ContainsKey(s))
          custom_like.Add(s, "");


      category = custom_like.ElementAt(r.Next(0, custom_like.Count)).Key;
      string bookmark = custom_like[category];


      while (active)
      {
        try
        {

          while (pins_pipeline.Count >= pipline_max)
          {
            Thread.Sleep(1000);
            if (liked >= maxLikes && maxLikes > 0)
            {
              if (_continuousSettings.ContinuousRun)
              {
                liked = 0;

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

          if (abort) { active = false; return; }


          Match m = Regex.Match(category, "/(.+?)/(.+?)/");
          string user = m.Groups[1].Value;
          string boardname = m.Groups[2].Value;
          string boardID = "";


          string RS = "";
          if (custom_like[category].Length < 1)
            RS = getNextPage_Custom(true, null, category, boardID, user);
          else
          {
            bookmark = custom_like[category];
            RS = getNextPage_Custom(false, bookmark, category, boardID, user);
          }
          bookmark = getBookmark(RS);
          custom_like[category] = bookmark;

          if (!custom_board_map.ContainsKey(category))
          {
            boardID = getBoardID(RS);
            custom_board_map.Add(category, boardID);
          }
          else
            boardID = custom_board_map[category];

          if (boardID == "") { custom_board_map.Remove(category); continue; }



          int scrollsMax = 5;
          int liked_session = 0;
          while ((bookmark = getBookmark(RS)).Length > 5 && scrollsMax > 0 && liked_session < 5)
          {
            List<Pin> pins = new List<Pin>();
            pins = parsePins(RS);

            for (int i = 0; i < pins.Count; i++)
            {

              if (liked >= maxLikes && maxLikes > 0)
              {
                if (_continuousSettings.ContinuousRun)
                {
                  liked = 0;

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

              Pin p = pins[i];

              if (abort) { active = false; return; }

              if (excludedPin(p))
                continue;

              // if ( ! p.liked_by_me)
              //if (doLike(p))
              pins_pipeline.Add(p);
              ++liked_session;

              while (pins_pipeline.Count >= pipline_max)
              {
                Thread.Sleep(1000);
                if (liked >= maxLikes && maxLikes > 0)
                {
                  if (_continuousSettings.ContinuousRun)
                  {
                    liked = 0;

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


              if (liked >= maxLikes && maxLikes > 0)
              {
                if (_continuousSettings.ContinuousRun)
                {
                  liked = 0;

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

            if (abort) { active = false; return; }

            --scrollsMax;
            if (scrollsMax > 0)
            {
              RS = getNextPage_Custom(false, bookmark, category, boardID, user);
              bookmark = getBookmark(RS);
            }
            custom_like[category] = bookmark;
            if (abort) { active = false; return; }

          }
        }
        catch { }
      }
    }
    List<Pin> parsePins(string RS)
    {
      if (liked >= maxLikes && maxLikes > 0)
      {
        if (_continuousSettings.ContinuousRun)
        {
          liked = 0;

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
      //pattern += "\"type\": \"pin\"(.+?)"; // [1]
      //pattern += "\"board\": {\"layout\": \"default\" \"name\": \"(.+?)\" \"privacy\": \"public\" \"url\": \"(.+?)\" \"owner\": {\"id\": \"(\\d+?)\"} \"type\": \"board\" \"id\": \"(\\d+?)\"(.+?)"; // [2 board_name] ; 3 board_url  ;  [ 4 user_uploader_id]  ; 5 board_id  ;  [6]
      //pattern += "\"236x\": {\"url\": \"(.+?)\""; // 7 : url image
      //pattern += "(.+?)} \"id\": \"(\\d+?)\"";  // 9 : pinID
      //pattern += "(.+?)\"description\": \"(.+?)\""; // 11 : desc
      //pattern += "(.+?)\"link\": \"(.+?)\""; // 13 : link
      //pattern += "(.+?)\"is_repin\": (.+?) "; // 15
      //pattern += "(.+?)\"liked_by_me\": false "; // 17
      //pattern += "(.+?)\"is_uploaded\": (.+?) "; // 19
      //pattern += "(.+?)\"repin_count\": (\\d+?) \""; // 21

      pattern = "\"object_description\": \"(.+?)\" \"object_image_url\": \"(.+?)\" \"object_id\": \"(\\d+?)\"";

      List<Pin> pins = new List<Pin>();
      foreach (Match m in Regex.Matches(RS, pattern, RegexOptions.Multiline))
      {
        if (abort) { active = false; return null; }
        if (liked >= maxLikes && maxLikes > 0)
        {
          if (_continuousSettings.ContinuousRun)
          {
            liked = 0;

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

        Pin p = new Pin();
        //p.board_url = m.Groups[3].ToString().Replace("\\","");
        //p.boardID = m.Groups[5].ToString();
        //p.url = m.Groups[7].ToString();
        p.id = m.Groups[3].ToString();
        p.description = m.Groups[1].ToString();
        //p.link = m.Groups[7].ToString();
        //p.is_repin = m.Groups[15].ToString().Equals("true") ? true : false;
        //p.liked_by_me = m.Groups[7].ToString().Equals("true") ? true : false;
        //p.is_upload = m.Groups[19].ToString().Equals("true") ? true : false;
        //p.repin_count = int.Parse(m.Groups[21].ToString());

        Match n = Regex.Match(RS, "\"liked\": (....) \"class_name\": \"likeSmall\" \"pin_id\": \"" + p.id + "\"");
        if (n.Groups[1].Value.ToString().Equals("true"))
          continue;

        if (!(pins.Any(x => x.id == p.id))) //prevent dubs
          pins.Add(p);


        if (abort) { active = false; return null; }

      }
      return pins;
    }
    bool doLike(Pin p)
    {
      try
      {
        if (abort) { active = false; return false; }
        //post
        string pinid = p.id.ToString();
        string desc = p.description;
        string link = p.link;
        StringBuilder strBuilder = new StringBuilder();

        strBuilder.AppendLine(HttpUtility.UrlEncode("source_url=/search/pins/?q=" + category) + "&data=" + HttpUtility.UrlEncode("{\"options\":{\"pin_id\":\"" + pinid + "\"},\"context\":{}}") + "&module_path=" + HttpUtility.UrlEncode("App()>SearchPage(resource=BaseSearchResource(constraint_string=null, show_scope_selector=true, restrict=null, query=" + category + ", scope=pins))>SearchPageContent(resource=SearchResource(layout=null, places=false, constraint_string=null, show_scope_selector=true, scope=pins, query=" + category + "))>Grid(resource=SearchResource(layout=null, places=false, constraint_string=null, show_scope_selector=true, scope=pins, query=" + category + "))>GridItems(resource=SearchResource(layout=null, places=false, constraint_string=null, show_scope_selector=true, scope=pins, query=" + category + "))>Pin(resource=PinResource(id=" + pinid + "))>PinLikeButton(liked=false, class_name=likeSmall, pin_id=" + pinid + ", has_icon=true, text=Like, show_text=false, ga_category=like)"));
        byte[] hdrbytes = System.Text.Encoding.UTF8.GetBytes(strBuilder.ToString());
        List<byte[]> data = new List<byte[]>();
        data.Add(hdrbytes);
        List<string> headers = new List<string> { "X-NEW-APP: 1", "X-APP-VERSION: " + app_version, "X-Requested-With: XMLHttpRequest", "X-CSRFToken: " + CSRFToken, "Accept-Encoding: gzip,deflate,sdch", "Accept-Language: en-US,en;q=0.8", "Origin: http://www.pinterest.com" };
        if (abort) { active = false; return false; }
        string RS = http.POST("http://www.pinterest.com/resource/PinLikeResource2/create/", "http://www.pinterest.com/search/pins/?q=" + category, "application/x-www-form-urlencoded; charset=UTF-8", data, headers, c, acc.proxy, "application/json, text/javascript, */*; q=0.01");
        if (abort) { active = false; return false; }
        if (!RS.Contains("[[[ERROR]]]"))
        {
          Console.WriteLine("liked:  " + pinid);
          ++liked;
          if (liked >= maxLikes && maxLikes > 0)
          {
            if (_continuousSettings.ContinuousRun)
            {
              liked = 0;

              var randomDelay = Helper.GetRandomTimeSpan(_continuousSettings.DelayFrom,
                                                           _continuousSettings.DelayTo);
              while (randomDelay > TimeSpan.Zero)
              {
                status = string.Format("delayed for {0:D2}:{1:D2}:{2:D2}", randomDelay.Hours, randomDelay.Minutes, randomDelay.Seconds);
                randomDelay -= new TimeSpan(0, 0, 1);
                Thread.Sleep(new TimeSpan(0, 0, 1));
                if (abort) { active = false; return false; }
              }
              status = "";
            }
            else
            {
              status = "max!"; active = false; return false;
            }
          }
          Random r = new Random();
          int sleepP = r.Next(timeout_min * 1000, timeout_max * 1000);
          while (sleepP > 0)
          {
            Thread.Sleep(100); sleepP -= 100;
            if (abort) { active = false; return false; }
          }
          return true;
        }
        else
        {
          ++errors;
          if (errors > ERRORS_MAX) { abort = true; }
        }
      }
      catch { }
      return false;
    }

    string getNextPage_Search(bool first, string bookmark)
    {

      if (first)
        return http.GET("http://www.pinterest.com/search/pins/?q=" + HttpUtility.UrlEncode(category), "http://www.pinterest.com/", c, null, acc.proxy).Replace(",", "");

      string url = "http://www.pinterest.com/resource/SearchResource/get/?source_url=";
      string referral = "http://www.pinterest.com/search/pins/?q=" + HttpUtility.UrlEncode(category);
      string json = HttpUtility.UrlEncode("/search/pins/?q=" + HttpUtility.UrlEncode(category) + "&rs=ac&len=7") + "&data=" + HttpUtility.UrlEncode("{\"options\":{\"layout\":null,\"places\":false,\"constraint_string\":null,\"show_scope_selector\":true,\"scope\":\"pins\",\"query\":\"" + HttpUtility.UrlEncode(category) + "\",\"bookmarks\":[\"" + bookmark + "\"]},\"context\":{},\"module\":{\"name\":\"GridItems\",\"options\":{\"scrollable\":true,\"show_grid_footer\":false,\"centered\":true,\"reflow_all\":true,\"virtualize\":true,\"item_options\":{\"show_pinner\":true,\"show_pinned_from\":false,\"show_board\":true},\"layout\":\"fixed_height\",\"track_item_impressions\":true}},\"render_type\":3,\"error_strategy\":1}"); ;
      json = Regex.Replace(json, "%\\d(\\w)", m => m.ToString().ToUpper());
      url += json;
      url += "&_=" + (GetTime() + 1);

      List<string> headers = new List<string>();
      headers.Add("X-NEW-APP: 1");
      headers.Add("X-APP-VERSION: " + app_version);
      headers.Add("X-Requested-With: XMLHttpRequest");
      headers.Add("Accept-Language: en-gb,en;q=0.5");
      headers.Add("Accept-Encoding: gzip, deflate");
      string accept = "application/json, text/javascript, */*; q=0.01";
      string RS = http.GET(url, referral, c, headers, acc.proxy, accept).Replace(",", "").Replace("\\\"", "\"");

      Random r = new Random();
      int sleepP = r.Next(1000, 2000);
      while (sleepP > 0)
      {
        Thread.Sleep(100); sleepP -= 100;
        if (abort) { active = false; return null; }
      }

      return RS;
    }
    string getNextPage_Custom(bool first, string bookmark, string loc, string boardID, string user)
    {

      if (first)
        return http.GET("http://www.pinterest.com" + loc, "http://www.pinterest.com/", c, null, acc.proxy).Replace(",", "");



      string url = "http://www.pinterest.com/resource/BoardFeedResource/get/?source_url=";
      string referral = "http://www.pinterest.com/search/pins/?q=" + HttpUtility.UrlEncode(category);
      string json = HttpUtility.UrlEncode(loc) + "&data=" + HttpUtility.UrlEncode("{\"options\":{\"board_id\":\"" + boardID + "\",\"board_url\":\"" + loc + "\",\"page_size\":null,\"prepend\":true,\"access\":[\"write\",\"delete\"],\"board_layout\":\"default\",\"bookmarks\":[\"" + bookmark + "\"]},\"context\":{},\"module\":{\"name\":\"GridItems\",\"options\":{\"scrollable\":true,\"show_grid_footer\":true,\"centered\":true,\"reflow_all\":true,\"virtualize\":true,\"show_while_loading\":false,\"item_options\":{\"show_pinner\":true,\"show_pinned_from\":true,\"show_board\":false,\"squish_giraffe_pins\":false},\"layout\":\"variable_height\"}},\"render_type\":3,\"error_strategy\":1}") + "&module_path=" + HttpUtility.UrlEncode("UserProfilePage(resource=UserResource(username=" + user + "))>UserProfileContent(resource=UserResource(username=" + user + "))>UserBoards()>Grid(resource=ProfileBoardsResource(username=" + user + "))>GridItems(resource=ProfileBoardsResource(username=" + user + "))>Board(show_board_context=false, component_type=1, show_user_icon=false, resource=BoardResource(board_id=" + boardID + "))"); ;
      json = Regex.Replace(json, "%\\d(\\w)", mx => mx.ToString().ToUpper());
      url += json;
      url += "&_=" + (GetTime() + 1);

      List<string> headers = new List<string>();
      headers.Add("X-NEW-APP: 1");
      headers.Add("X-APP-VERSION: " + app_version);
      headers.Add("X-Requested-With: XMLHttpRequest");
      headers.Add("Accept-Language: en-gb,en;q=0.5");
      headers.Add("Accept-Encoding: gzip, deflate");
      string accept = "application/json, text/javascript, */*; q=0.01";
      string RS = http.GET(url, referral, c, headers, acc.proxy, accept).Replace(",", "").Replace("\\\"", "\"");

      Random r = new Random();
      int sleepP = r.Next(1000, 2000);
      while (sleepP > 0)
      {
        Thread.Sleep(100); sleepP -= 100;
        if (abort) { active = false; return null; }
      }

      return RS;
    }
    string getBoardID(string RS)
    {
      //string RS = http.GET("http://www.pinterest.com/" + username + "/" + boardname + "/", "http://www.pinterest.com/" + username + "/", c, null, acc.proxy).Replace(",", "");
      Match m = Regex.Match(RS, "\"board_id\": \"(\\d+?)\"");
      return m.Groups[1].ToString();
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
    struct Pin
    {
      //public string board_url, boardID, url;
      public string link, description, id;
      //public int repin_count;
      //public bool is_repin, liked_by_me, is_upload;
      //public Image img;
    }

    string status = "";
    override public string ToString()
    {
      return "likes: " + liked + "  " + status;
    }


  }
}
