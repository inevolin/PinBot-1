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
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Windows.Forms;

namespace PinBot
{
  static class CustomExtension
  {
    public static void Shuffle<T>(this IList<T> list)
    {
      Random rng = new Random();
      int n = list.Count;
      while (n > 1)
      {
        n--;
        int k = rng.Next(n + 1);
        T value = list[k];
        list[k] = list[n];
        list[n] = value;
      }
    }
  }

  class repin
  {
    private Dictionary<string, object> continueing = new Dictionary<string, object>();
    private CookieContainer c = new CookieContainer();
    private account acc;
    private string app_version = "", CSRFToken = "";
    private bool active = false;
    private bool abort = false;
    private int timeout_min = 60;
    private int timeout_max = 300;
    private int repinned = 0;
    private int maxRepins = 50;
    private int errors = 0;
    private int ERRORS_MAX = 30;
    private DateTime _scheduled;
    private ContinuousSettings _continuousSettings;

    private bool autopilot;
    private int scrapes = 0;
    private int scrapesMax = 20;

    public bool Active { get { return active; } set { active = value; } }
    public bool Abort { get { return abort; } set { abort = value; } }


    private int KeepPoll = 0;
    public void SetKeepPoll(bool val)
    {
      KeepPoll = (val == true) ? 1 : 2;
    }

    public repin(account acc)
    {
      this.acc = acc;
      this.c = acc.c;
      this.CSRFToken = acc.CSRFtoken;
      this.app_version = acc.App_version;

      this.timeout_max = acc.setting_repin.TimeoutMax;
      this.timeout_min = acc.setting_repin.TimeoutMin;
      this.maxRepins = acc.setting_repin.Max;
      this.scrapesMax = acc.numRepinScrapes;

      usedPins();
    }

    public repin(account acc, bool autopilot, ContinuousSettings continuousSettings, DateTime? scheduled)
      : this(acc)
    {
      this.autopilot = autopilot;
      _continuousSettings = continuousSettings;
      _scheduled = scheduled ?? DateTime.MinValue;
    }

    public void StartRepinScrape_thread()
    {
      Thread t = new Thread(StartScrape);
      t.Start();
    }
    public void StartRepin_thread()
    {
      Thread t = new Thread(do_repins);
      t.Start();
    }

    void StartRepin()
    {
      active = true;
      while (active)
      {
        try
        {

          if (abort) { active = false; return; }


        }
        catch
        { }
      }
    }
    void StartScrape()
    {
      active = true;
      while (active)
      {
        try
        {

          if (abort) { active = false; return; }

          Random r = new Random();
          int RS_source = r.Next(0, 2);

          if (RS_source == 0 && acc.boards_category_mapped_repin.Values.SelectMany(x => x).Count() > 0)
          {
            addQueue_Search();
          }
          else if (acc.usersboards_mapped_repin.Values.SelectMany(x => x).Count() > 0)
          {
            addQueue_Users();
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


    void addQueue_Search()
    {
      string boardID = "";
      Random r = new Random();

      string prev_category = "";
      string category = "";

      prev_category = getRandCategory(out category, out boardID, prev_category);
      string RS = getNextPage_Search(category);

      if (RS.Contains("[[[ERROR]]]"))
        return;

      string bookmark = null;
      int scrollsMax = 20;
      int added_current_session = 0; //as long as less than 5 pins added, keep going next page
      while ((bookmark = getBookmark(RS)).Length > 5 && scrollsMax > 0 && added_current_session < 5)
      {
        if (!continueing.ContainsKey(category))
          continueing.Add(category, bookmark);
        else
          continueing[category] = bookmark;

        if (repinned >= maxRepins && maxRepins > 0)
        {
          if (_continuousSettings.ContinuousRun)
          {
            repinned = 0;

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

        List<RePin_Pin> pins = new List<RePin_Pin>();
        pins = parsePins(RS);

        int i = 0;
        while (i < pins.Count && added_current_session < 5)
        {
          if (repinned >= maxRepins && maxRepins > 0)
          {
            if (_continuousSettings.ContinuousRun)
            {
              repinned = 0;

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

          RePin_Pin p = pins[i++];
          p.category = category;
          p.boardID = boardID;
          p.userSearch = false;

          if (excludePin(p))
            continue;

          //if (autopilot)
          //     doRepin_Search(p);
          // else
          {
            HttpWebRequest httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(p.img_url);
            if (acc.proxy != null)
              httpWebRequest.Proxy = acc.proxy;
            HttpWebResponse httpWebReponse = (HttpWebResponse)httpWebRequest.GetResponse();
            Stream stream = httpWebReponse.GetResponseStream();
            p.img = Image.FromStream(stream);

            //repins.Add(p);
            addPinDB(p);

            ++scrapes;
            if (!autopilot && scrapes >= scrapesMax) { status = "max!"; active = false; return; }
            else if (autopilot)
            {
              while (pause())
              {
                if (abort) { active = false; return; }
                Thread.Sleep(1000);
              }
            }

          }
          ++added_current_session;
          if (repinned >= maxRepins && maxRepins > 0)
          {
            if (_continuousSettings.ContinuousRun)
            {
              repinned = 0;

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
        Thread.Sleep(r.Next(1000, 2000));
        --scrollsMax;

        RS = getNextPage_Search(category);

      }
    }

    private bool pause()
    {
      if (autopilot && GetInQueue() >= 5)
      { return true; }

      return false;

    }

    void addQueue_Users()
    {
      UserBoardMapped prev_user = new UserBoardMapped();
      UserBoardMapped user = new UserBoardMapped();

      string boardID = "";
      Random r = new Random();


      prev_user = getRandUser(out user, out boardID, prev_user);
      if (continueing.ContainsKey(user.mappedID))
        user = (UserBoardMapped)continueing[user.mappedID];

      string RS = getNextPage_User(ref user);

      if (RS.Contains("[[[ERROR]]]"))
        return;

      string bookmark = null;
      int scrollsMax = 20;
      int added_current_session = 0; //as long as less than 5 pins added, keep going next page
      while ((bookmark = getBookmark(RS)).Length > 5 && scrollsMax > 0 && added_current_session < 5)
      {
        user.bookmark = bookmark;

        if (!continueing.ContainsKey(user.mappedID))
          continueing.Add(user.mappedID, user);
        else
          continueing[user.mappedID] = user;


        if (repinned >= maxRepins && maxRepins > 0)
        {
          if (_continuousSettings.ContinuousRun)
          {
            repinned = 0;

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

        List<RePin_Pin> pins = new List<RePin_Pin>();
        pins = parsePins(RS);

        int i = 0;
        while (i < pins.Count && added_current_session < 5)
        {
          if (repinned >= maxRepins && maxRepins > 0)
          {
            if (_continuousSettings.ContinuousRun)
            {
              repinned = 0;

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

          RePin_Pin p = pins[i++];
          p.user_boardID = user.user_boardID;
          p.part = user.mappedID;
          p.boardID = boardID;
          p.userSearch = true;

          if (excludePin(p))
            continue;

          //if (autopilot)
          //   doRepin_Users(p);
          // else
          {
            HttpWebRequest httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(p.img_url);
            if (acc.proxy != null)
              httpWebRequest.Proxy = acc.proxy;
            HttpWebResponse httpWebReponse = (HttpWebResponse)httpWebRequest.GetResponse();
            Stream stream = httpWebReponse.GetResponseStream();
            p.img = Image.FromStream(stream);

            //repins.Add(p);
            addPinDB(p);

            ++scrapes;
            if (!autopilot && scrapes >= scrapesMax) { status = "max!"; active = false; return; }
            else if (autopilot)
            {
              while (pause())
              {
                if (abort) { active = false; return; }
                Thread.Sleep(1000);
              }
            }

          }
          ++added_current_session;

          if (repinned >= maxRepins && maxRepins > 0)
          {
            if (_continuousSettings.ContinuousRun)
            {
              repinned = 0;

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
        Thread.Sleep(r.Next(1000, 2000));
        --scrollsMax;

        RS = getNextPage_User(ref user);

        bookmark = getBookmark(RS);
        if (bookmark.Length > 5)
        {
          user.bookmark = bookmark;
          if (!continueing.ContainsKey(user.mappedID))
            continueing.Add(user.mappedID, user);
          else
            continueing[user.mappedID] = user;
        }

      }
    }
    string getRandCategory(out string category, out string boardID, string prev_category)
    {
      Random r = new Random();
      Dictionary<string, List<string>> dic = new Dictionary<string, List<string>>();
      foreach (KeyValuePair<string, List<string>> k in acc.boards_category_mapped_repin)
      {
        if (k.Value.Count > 0)
          dic.Add(k.Key, k.Value);
      }
      KeyValuePair<string, List<string>> kv = dic.ElementAt(r.Next(0, dic.Count));
      List<string> categories = new List<string>();
      categories = kv.Value;
      boardID = kv.Key;
      category = categories[r.Next(0, categories.Count)];
      while (category.Equals(prev_category) && dic.Count > 1)
      {
        kv = dic.ElementAt(r.Next(0, dic.Count));
        categories = kv.Value;
        boardID = kv.Key;
        category = categories[r.Next(0, categories.Count)];
      }
      prev_category = category;
      return prev_category;
    }
    UserBoardMapped getRandUser(out UserBoardMapped user, out string boardID, UserBoardMapped prev_user)
    {
      Random r = new Random();

      List<UserBoardMapped> dic = new List<UserBoardMapped>();
      foreach (KeyValuePair<string, List<string>> kv in acc.usersboards_mapped_repin)
      {
        foreach (string s in kv.Value)
        {
          UserBoardMapped temp_user = new UserBoardMapped();
          temp_user.mappedID = s;
          temp_user.boardID = kv.Key;
          dic.Add(temp_user);
        }

      }

      user = dic.ElementAt(r.Next(0, dic.Count));
      while (user.mappedID.Equals(prev_user.mappedID) && dic.Count > 1)
      {
        user = dic.ElementAt(r.Next(0, dic.Count));
      }
      boardID = user.boardID;
      prev_user = user;
      return prev_user;
    }


    void do_repins()
    {
      active = true;
      while (_scheduled > DateTime.Now)
      {
        if (abort) { active = false; return; }

        status = "scheduled...";
        Thread.Sleep(5000);
      }
      while (active)
      {
        try
        {
          if (abort) { active = false; return; }

          SQLiteConnection m_dbConnection = new SQLiteConnection(@"Data Source=db;Version=3;New=False;Compress=True;");
          m_dbConnection.Open();
          string sql = "select pin, ID from repins LIMIT 1 ";
          SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
          SQLiteDataReader sqlreader = command.ExecuteReader();

          if (!sqlreader.Read())
          { Thread.Sleep(5000); status = "idle"; continue; }

          BinaryFormatter b = new BinaryFormatter();
          var bytes = Convert.FromBase64String(sqlreader["pin"].ToString());
          MemoryStream mss = new MemoryStream(bytes);
          RePin_Pin p = (RePin_Pin)b.Deserialize(mss);
          p.DB_ID = int.Parse(sqlreader["ID"].ToString());
          sqlreader.Close();
          m_dbConnection.Close();

          try
          {
            if (abort) { active = false; return; }


            if (p.userSearch)
              doRepin_Users(p);
            else
              doRepin_Search(p);

            --scrapes;

            //if count >= max stop ???
            if (abort) { active = false; return; }
          }
          catch
          {
            Thread.Sleep(5000);
          }

          if (abort) { active = false; return; }
        }
        catch { }
      }
    }

    void addPinDB(RePin_Pin p)
    {
      try
      {
        MemoryStream ms = new MemoryStream();
        BinaryFormatter b = new BinaryFormatter();
        b.Serialize(ms, p);
        string obj = Convert.ToBase64String(ms.ToArray());

        SQLiteConnection m_dbConnection = new SQLiteConnection(@"Data Source=db;Version=3;New=False;Compress=True;");
        m_dbConnection.Open();
        string sql = "insert into  repins  (pin) values ('" + obj + "')";
        SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
        command.ExecuteNonQuery();
        m_dbConnection.Close();

        ms.Close();
      }
      catch { }
    }
    void removePinDB(RePin_Pin p)
    {
      try
      {
        SQLiteConnection m_dbConnection = new SQLiteConnection(@"Data Source=db;Version=3;New=False;Compress=True;");
        m_dbConnection.Open();
        string sql = "DELETE from repins WHERE ID = '" + p.DB_ID + "'";
        SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
        command.ExecuteNonQuery();
        m_dbConnection.Close();
      }
      catch { }
    }
    List<RePin_Pin> parsePins(string RS)
    {
      if (repinned >= maxRepins && maxRepins > 0)
      {
        if (_continuousSettings.ContinuousRun)
        {
          repinned = 0;

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



      /*pattern += ""; // 
      pattern += "\"236x\": {\"url\": \"(.+?)\""; // 2 : url image
      pattern += "(.+?)} \"id\": \"(\\d+?)\"";  // 4 : pinID
      pattern += "(.+?)\"description\": \"(.+?)\""; // 6 : desc
      pattern += "(.+?)\"link\": \"(.+?)\""; // 8 : link
      pattern += "(.+?)\"is_repin\": (.+?) "; // 10
      pattern += "(.+?)\"liked_by_me\": (.+?) "; // 12
      pattern += "(.+?)\"is_uploaded\": (.+?) "; // 14
      pattern += "(.+?)\"repin_count\": (\\d+?) \""; // 16*/

      RS = RS.Substring(RS.IndexOf("\"bookmarks\":"), RS.Length - RS.IndexOf("\"bookmarks\":"));

      //string pattern = "\"data\": \\{(.+?)\"type\": \"pin\"(.+?)\"236x\": \\{\"url\": \"(.+?)\"(.+?)\\}(.+?)\"id\": \"(\\d+?)\"(.+?)\\}(.+?)\"description\": \"(.+?)\"(.+?)\"link\": \"(.*?)\" \"is_repin\": (.+?) \"liked_by_me\": (.+?) \"is_uploaded\": (.+?) \"";
      //string pattern = "\\{\"url\": \"(.+?)\"(.+?)\\}(.+?)\"id\": \"(\\d+?)\"(.+?)\"description\": \"(.+?)\"(.+?)\"link\": \"(.*?)\" \"is_repin\": (.+?) \"is_uploaded\": (.+?) \"";
      string pattern = "\\{\"url\": \"(.+?)\"(.+?)\\}(.+?)\"id\": \"(\\d+?)\"(.+?)\"description\": \"(.+?)\"(.+?)\"link\": \"(.*?)\"";

      List<RePin_Pin> pins = new List<RePin_Pin>();
      foreach (Match m in Regex.Matches(RS, pattern, RegexOptions.Singleline))
      {
        if (abort) { active = false; return null; }
        if (repinned >= maxRepins && maxRepins > 0)
        {
          if (_continuousSettings.ContinuousRun)
          {
            repinned = 0;

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

        RePin_Pin p = new RePin_Pin();
        p.url = m.Groups[1].ToString();
        p.id = m.Groups[4].ToString();
        p.description = m.Groups[6].ToString().Replace("\\/", "/").Replace("\\", "\\\\");
        p.link = m.Groups[8].ToString().Replace("\\/", "/");
        //p.is_repin = m.Groups[9].ToString().Equals("true") ? true : false ;
        //p.liked_by_me = m.Groups[13].ToString().Equals("true") ? true : false;
        //p.is_upload = m.Groups[10].ToString().Equals("true") ? true : false;

        //p.repin_count = int.Parse(m.Groups[17].ToString());

        if (!(pins.Any(x => x.id == p.id))) //prevent dubs
        {
          //if (!autopilot)
          // {
          string url = p.url;
          url = url.Replace("\\", "");
          p.img_url = url;
          // }
          pins.Add(p);
        }

        if (abort) { active = false; return null; }

      }
      return pins;
    }
    void doRepin_Search(RePin_Pin p)
    {
      try
      {
        if (abort) { active = false; return; }
        //post
        string pinid = p.id.ToString();
        string desc = p.description.Replace("\\/", "/").Replace("\\","\\\\");
        string link = p.link.Replace("\\/", "/");
        string cat = HttpUtility.UrlPathEncode(p.category);
        StringBuilder strBuilder = new StringBuilder();

        strBuilder.AppendLine("source_url=" + HttpUtility.UrlEncode("/search/pins/?q=" + cat + "&rs=ac&len=3") + "&data=" + HttpUtility.UrlEncode("{\"options\":{\"board_id\":\"" + p.boardID + "\",\"description\":\"" + desc + "\",\"link\":\"" + link + "\",\"is_video\":false,\"pin_id\":\"" + pinid + "\"},\"context\":{}}") + "&module_path=" + HttpUtility.UrlEncode("App()>SearchPage(resource=BaseSearchResource(constraint_string=null, show_scope_selector=true, restrict=null, scope=pins, query=" + cat + "))>SearchPageContent(resource=SearchResource(layout=null, places=false, constraint_string=null, show_scope_selector=true, scope=pins, query=" + cat + "))>Grid(resource=SearchResource(layout=null, places=false, constraint_string=null, show_scope_selector=true, scope=pins, query=" + cat + "))>GridItems(resource=SearchResource(layout=null, places=false, constraint_string=null, show_scope_selector=true, query=" + cat + ", scope=pins))>Pin(resource=PinResource(id=" + pinid + "))>ShowModalButton(module=PinCreate)#Modal(module=PinCreate(resource=PinResource(id=" + pinid + ")))"));
        byte[] hdrbytes = System.Text.Encoding.UTF8.GetBytes(strBuilder.ToString());
        List<byte[]> data = new List<byte[]>();
        data.Add(hdrbytes);
        List<string> headers = new List<string> { "X-NEW-APP: 1", "X-APP-VERSION: " + app_version, "X-Requested-With: XMLHttpRequest", "X-CSRFToken: " + CSRFToken, "Accept-Encoding: gzip,deflate,sdch", "Accept-Language: en-US,en;q=0.8", "Pragma: no-cache", "Cache-Control: no-cache" };
        if (abort) { active = false; return; }
        string RS = http.POST("http://www.pinterest.com/resource/RepinResource/create/", "http://www.pinterest.com/search/pins/?q=" + cat + "&rs=ac&len=3", "application/x-www-form-urlencoded; charset=UTF-8", data, headers, c, acc.proxy, "application/json, text/javascript, */*; q=0.01");
        if (abort) { active = false; return; }
        if (!RS.Contains("[[[ERROR]]]"))
        {
          removePinDB(p);
          Console.WriteLine("repinned:  " + pinid);
          status = "";
          ++repinned;
          if (repinned >= maxRepins && maxRepins > 0)
          {
            if(_continuousSettings.ContinuousRun)
            {
              repinned = 0;

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
        else if (RS.Contains("You don't have permission to edit this board."))
          removePinDB(p);
        else if (RS.Contains("Something went wrong while trying to repin this Pin."))
          removePinDB(p);
        else
        {
          ++errors;
          if (errors > ERRORS_MAX) { abort = true; status = "ERROR!"; }
        }
      }
      catch { }
    }
    void doRepin_Users(RePin_Pin p)
    {
      try
      {
        if (abort) { active = false; return; }
        //post
        string pinid = p.id.ToString();
        string desc = (p.description.Replace("\\/", "/"));
        string link = p.link;
        string cat = HttpUtility.UrlPathEncode(p.category);
        StringBuilder strBuilder = new StringBuilder();

        string part = p.part;
        Match m = Regex.Match(part, "/(.+?)/(.+?)/");
        string username = m.Groups[1].Value.ToString();
        string slug = m.Groups[2].Value.ToString();

        strBuilder.AppendLine("source_url=" + HttpUtility.UrlEncode(part) + "&data=" + HttpUtility.UrlEncode("{\"options\":{\"board_id\":\"" + p.boardID + "\",\"description\":\"" + desc + "\",\"link\":\"" + link + "\",\"is_video\":false,\"pin_id\":\"" + pinid + "\"},\"context\":{}}") + "&module_path=" + HttpUtility.UrlEncode("App()>BoardPage(resource=BoardResource(username=" + username + ", slug=" + slug + "))>Grid(resource=BoardFeedResource(board_id=" + p.user_boardID + ", board_url=" + part + ", page_size=null, prepend=true, access=, board_layout=default))>GridItems(resource=BoardFeedResource(board_id=" + p.user_boardID + ", board_url=" + part + ", page_size=null, prepend=true, access=, board_layout=default))>Pin(resource=PinResource(id=" + pinid + "))>ShowModalButton(module=PinCreate)#Modal(module=PinCreate(resource=PinResource(id=" + pinid + ")))"));


        byte[] hdrbytes = System.Text.Encoding.UTF8.GetBytes(strBuilder.ToString());
        List<byte[]> data = new List<byte[]>();
        data.Add(hdrbytes);
        List<string> headers = new List<string> { "X-NEW-APP: 1", "X-APP-VERSION: " + app_version, "X-Requested-With: XMLHttpRequest", "X-CSRFToken: " + CSRFToken, "Accept-Encoding: gzip,deflate,sdch", "Accept-Language: en-US,en;q=0.8" };
        if (abort) { active = false; return; }
        string RS = http.POST("http://www.pinterest.com/resource/RepinResource/create/", "http://www.pinterest.com" + p.part, "application/x-www-form-urlencoded; charset=UTF-8", data, headers, c, acc.proxy, "application/json, text/javascript, */*; q=0.01");
        if (abort) { active = false; return; }
        if (!RS.Contains("[[[ERROR]]]"))
        {
          removePinDB(p);
          Console.WriteLine("repinned:  " + pinid);
          status = "";
          ++repinned;
          if (repinned >= maxRepins && maxRepins > 0)
          {
            if (_continuousSettings.ContinuousRun)
            {
              repinned = 0;

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
        else if (RS.Contains("You don't have permission to edit this board."))
          removePinDB(p);
        else if (RS.Contains("Something went wrong while trying to repin this Pin."))
          removePinDB(p);
        else
        {
          ++errors;
          if (errors > ERRORS_MAX) { abort = true; status = "ERROR!"; }
        }
      }
      catch { }
    }
    string getNextPage_Search(string category)
    {
      if (!continueing.ContainsKey(category))
        return http.GET("http://www.pinterest.com/search/pins/?q=" + category, "http://www.pinterest.com/", c, null, acc.proxy).Replace(",", "");
      string bookmark = (string)continueing[category];
      string url = "http://www.pinterest.com/resource/SearchResource/get/?source_url=";
      string referral = "http://www.pinterest.com/search/pins/?q=" + category;
      string json = HttpUtility.UrlEncode("/search/pins/?q=" + category) + "&data=" + HttpUtility.UrlEncode("{\"options\":{\"layout\":null,\"places\":false,\"constraint_string\":null,\"show_scope_selector\":true,\"query\":\"" + category + "\",\"scope\":\"pins\",\"bookmarks\":[\"" + bookmark + "\"]},\"context\":{},\"module\":{\"name\":\"GridItems\",\"options\":{\"scrollable\":true,\"show_grid_footer\":true,\"centered\":true,\"reflow_all\":true,\"virtualize\":true,\"item_options\":{\"show_pinner\":true,\"show_pinned_from\":false,\"show_board\":true},\"layout\":\"variable_height\",\"track_item_impressions\":true}},\"render_type\":3,\"error_strategy\":1}") + "&module_path=" + HttpUtility.UrlEncode("App()>Header()>ui.SearchForm()>ui.TypeaheadField(enable_recent_queries=true, resource_name=TypeaheadResource, name=q, tags=autocomplete, class_name=inHeader, prefetch_on_focus=true, value=\"\", view_type=search, populate_on_result_highlight=true, search_delay=0, search_on_focus=true, placeholder=Search)"); ;
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
    string getNextPage_User(ref UserBoardMapped ret)
    {
      string url = "", json = "";
      string mapped_username = ret.mappedID.Split('/').ToArray()[1];
      List<string> headers = new List<string>();
      string referral = "http://www.pinterest.com" + ret.mappedID;

      if (!continueing.ContainsKey(ret.mappedID))
      {
        //http://www.pinterest.com/resource/UserResource/get/?source_url=&_=1416837499235 

        json = "http://www.pinterest.com/resource/UserResource/get/?source_url=";
        json += HttpUtility.UrlEncode(ret.mappedID) + "&data=" + HttpUtility.UrlEncode("{\"options\":{\"username\":\"" + mapped_username + "\"},\"context\":{},\"module\":{\"name\":\"UserProfileContent\",\"options\":{\"tab\":\"pins\"}},\"render_type\":1,\"error_strategy\":0}");
        json = Regex.Replace(json, "%\\d(\\w)", m => m.ToString().ToUpper());
        url += json;
        url += "&_=" + (GetTime() + 1);
        //ret.RS = http.GET(url, "http://www.pinterest.com/", c, null, acc.proxy).Replace(",", "");

        headers.Add("X-NEW-APP: 1");
        headers.Add("X-APP-VERSION: " + app_version);
        headers.Add("X-Requested-With: XMLHttpRequest");
        headers.Add("Accept-Language: en-gb,en;q=0.5");
        headers.Add("Accept-Encoding: gzip, deflate");
        ret.RS = http.GET(url, referral, c, headers, acc.proxy, "application/json, text/javascript, */*; q=0.01").Replace(",", "").Replace("\\\"", "\"");

        //ret.RS = http.GET("http://www.pinterest.com" + ret.mappedID, "http://www.pinterest.com/", c, null, acc.proxy).Replace(",", "");
        Match mt = Regex.Match(ret.RS, "\"board_id\": \"(\\d+?)\"");
        string user_boardID = mt.Groups[1].Value.ToString();
        ret.user_boardID = user_boardID;
        return ret.RS;
      }


      string bookmark = ((UserBoardMapped)continueing[ret.mappedID]).bookmark;

      url = "http://www.pinterest.com/resource/UserPinsResource/get/?source_url=";

      //string json = HttpUtility.UrlEncode(ret.mappedID) + "&data=" + HttpUtility.UrlEncode("{\"options\":{\"board_id\":\"" + ret.user_boardID + "\",\"board_url\":\"/melissaburic/good-reads/\",\"page_size\":null,\"prepend\":true,\"access\":[],\"board_layout\":\"default\",\"bookmarks\":[\"" + bookmark + "\"]},\"context\":{},\"module\":{\"name\":\"GridItems\",\"options\":{\"scrollable\":true,\"show_grid_footer\":true,\"centered\":true,\"reflow_all\":true,\"virtualize\":true,\"show_while_loading\":false,\"item_options\":{\"show_pinner\":false,\"show_pinned_from\":true,\"show_board\":false,\"squish_giraffe_pins\":false},\"layout\":\"variable_height\"}},\"render_type\":3,\"error_strategy\":1}"); ;
      json = HttpUtility.UrlEncode(ret.mappedID) + "&data=" + HttpUtility.UrlEncode("{\"options\":{\"username\":\"" + mapped_username + "\",\"bookmarks\":[\"" + bookmark + "\"]},\"context\":{},\"module\":{\"name\":\"GridItems\",\"options\":{\"scrollable\":true,\"show_grid_footer\":true,\"centered\":true,\"reflow_all\":true,\"virtualize\":true,\"item_options\":{\"show_pinner\":false,\"show_pinned_from\":false,\"show_board\":true,\"squish_giraffe_pins\":false},\"layout\":\"variable_height\"}},\"render_type\":3,\"error_strategy\":1}");
      json = Regex.Replace(json, "%\\d(\\w)", m => m.ToString().ToUpper());
      url += json;
      url += "&_=" + (GetTime() + 1);


      headers.Add("X-NEW-APP: 1");
      headers.Add("X-APP-VERSION: " + app_version);
      headers.Add("X-Requested-With: XMLHttpRequest");
      headers.Add("Accept-Language: en-gb,en;q=0.5");
      headers.Add("Accept-Encoding: gzip, deflate");
      ret.RS = http.GET(url, referral, c, headers, acc.proxy, "application/json, text/javascript, */*; q=0.01").Replace(",", "").Replace("\\\"", "\"");

      Random r = new Random();
      int sleepP = r.Next(1000, 2000);
      while (sleepP > 0)
      {
        Thread.Sleep(100); sleepP -= 100;
        if (abort) { active = false; return null; }
      }

      return ret.RS;
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

    List<string> pins_exclude = new List<string>();
    bool excludePin(RePin_Pin pin)
    {
      if (pins_exclude.Contains("id:" + pin.id)) return true;
      pins_exclude.Add("id:" + pin.id);
      using (FileStream fs = new FileStream("./exclude_repins.txt", FileMode.Append, FileAccess.Write))
      using (StreamWriter sw = new StreamWriter(fs))
      {
        string s = "id:" + pin.id;
        sw.WriteLine(s);
      }
      return false;
    }
    void usedPins()
    {

      if (!File.Exists("./exclude_repins.txt"))
        using (var fileStream = File.Create("./exclude_repins.txt")) { }

      using (var reader = new StreamReader("./exclude_repins.txt"))
      {
        string line;
        while ((line = reader.ReadLine()) != null)
        {
          pins_exclude.Add(line);
        }
      }
    }

    string status = "";
    private int GetInQueue()
    {
      SQLiteConnection m_dbConnection = new SQLiteConnection(@"Data Source=db;Version=3;New=False;Compress=True;");
      m_dbConnection.Open();
      string sql = "select count(*) as c from repins";
      SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
      SQLiteDataReader sqlreader = command.ExecuteReader();
      string i = "";
      while (sqlreader.Read())
      {
        i = sqlreader["c"].ToString();
      }
      m_dbConnection.Close();
      return int.Parse(i);
    }
    override public string ToString()
    {
      return "repins: " + repinned + "  queue: " + GetInQueue() + "  " + status;
    }

    struct UserBoardMapped
    {
      public string RS, user_boardID, mappedID, boardID, bookmark;
    }
  }
}
