using System;
using System.Collections.Generic;
using System.Data.SQLite;
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
  class upload
  {
    private CookieContainer c = new CookieContainer();
    private string app_version = "", CSRFToken = "", request_id = "";
    private int maxUploads = 50;
    private int timeout_min = 30;
    private int timeout_max = 300;
    private int uploaded = 0;
    private DateTime _scheduledTime;
    private ContinuousSettings _continuousSettings;
    private bool active = false;
    private bool abort = false;
    private account acc;
    private int errors = 0;
    private int ERRORS_MAX = 30;

    public bool Active { get { return active; } set { active = value; } }
    public bool Abort { get { return abort; } set { abort = value; } }
    public int MaxUploads { get { return maxUploads; } set { maxUploads = value; } }
    public int Timeout_min { get { return timeout_min; } set { timeout_min = value; } }
    public int Timeout_max { get { return timeout_max; } set { timeout_max = value; } }

    bool debug = false;

    public upload(account acc, ContinuousSettings ss)
    {
      this.c = acc.c;
      this.CSRFToken = acc.CSRFtoken;
      this.app_version = acc.App_version;
      this.request_id = acc.Request_id;
      this.acc = acc;

      this.timeout_max = acc.setting_pin.TimeoutMax;
      this.timeout_min = acc.setting_pin.TimeoutMin;
      this.maxUploads = acc.setting_pin.Max;

      _continuousSettings = ss;
    }

    public upload(account acc, ContinuousSettings ss, DateTime scheduled) : this (acc, ss)
    {
      _scheduledTime = scheduled;
    }

    public void start_thread()
    {
      active = true;
      Thread thread = new Thread(new ThreadStart(start));
      thread.Start();
    }
    void start()
    {
      while (_scheduledTime > DateTime.Now)
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

          string path = "", desc = "", boardID = "", category = "", source_url = "";
          SQLiteConnection m_dbConnection = new SQLiteConnection(@"Data Source=db;Version=3;New=False;Compress=True;");
          m_dbConnection.Open();
          string sql = "select path, description, boardID, category, source_url from pins ORDER BY path LIMIT 1 ";
          SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
          SQLiteDataReader sqlreader = command.ExecuteReader();
          int count = 0;
          while (sqlreader.Read())
          {
            path = sqlreader["path"].ToString();
            desc = sqlreader["description"].ToString();
            boardID = sqlreader["boardID"].ToString();
            category = sqlreader["category"].ToString();
            source_url = sqlreader["source_url"].ToString();
            ++count;
          }
          m_dbConnection.Close();

          if (count <= 0)
          { Thread.Sleep(5000); status = "idle"; continue; }

          status = "pinning...";

          if (!File.Exists(path) || desc == "")
          {
            //MessageBox.Show("1");
            delete(path);
            continue;
          }

          List<string> hdrs = new List<string>();
          hdrs.Add("X-NEW-APP: 1");
          hdrs.Add("X-APP-VERSION: " + app_version);
          hdrs.Add("X-Requested-With: XMLHttpRequest");
          hdrs.Add("Accept-Encoding: gzip,deflate,sdch");
          hdrs.Add("Accept-Language: en-US,en;q=0.8");
          string url = "http://www.pinterest.com/resource/NoopResource/get/?source_url=%2F&data=%7B%22options%22%3A%7B%7D%2C%22context%22%3A%7B%7D%2C%22module%22%3A%7B%22name%22%3A%22PinUploader%22%2C%22options%22%3A%7B%22show_title%22%3Atrue%2C%22shrinkToFit%22%3Atrue%7D%7D%2C%22render_type%22%3A1%2C%22error_strategy%22%3A0%7D&module_path=App()%3EHeader()%3EDropdownButton()%3EDropdown()%3EAddPin()%3EShowModalButton(module%3DPinUploader)&_=" + (followers.GetTime() + 1);
          // string RS = http.GET(url, "https://www.pinterest.com", c, hdrs,acc);
          string RS;

          string boundary = http.getBoundary();
          StringBuilder sb1 = new StringBuilder();
          sb1.AppendLine("--" + boundary);
          sb1.AppendLine("Content-Disposition: form-data; name=\"img\"; filename=\"" + Path.GetFileName(path) + "\"");
          string ext = Path.GetExtension(path).Replace(".", "");
          ext = ext.Equals("jpg") ? "jpeg" : ext;
          sb1.Append("Content-Type: image/" + ext);
          sb1.AppendLine();
          sb1.AppendLine();
          byte[] headerbytes = System.Text.Encoding.UTF8.GetBytes(sb1.ToString());
          byte[] filedata = null;
          using (BinaryReader breader = new BinaryReader(File.OpenRead(path)))
            filedata = breader.ReadBytes((int)breader.BaseStream.Length);
          byte[] endboundarybytes = System.Text.Encoding.ASCII.GetBytes(Environment.NewLine + "--" + boundary + "--" + Environment.NewLine);
          List<byte[]> lb = new List<byte[]>();
          lb.Add(headerbytes);
          lb.Add(filedata);
          lb.Add(endboundarybytes);
          hdrs = new List<string>();
          hdrs.Add("Accept-Encoding: gzip,deflate,sdch");
          hdrs.Add("Accept-Language: en-gb,en;q=0.8");
          hdrs.Add("X-Requested-With: XMLHttpRequest");
          hdrs.Add("X-File-Name: " + Path.GetFileName(path));
          hdrs.Add("Cache-Control: no-cache");
          hdrs.Add("X-CSRFToken: " + CSRFToken);
          hdrs.Add("Origin: http://www.pinterest.com");


          if (abort) { active = false; return; }



          RS = http.POST("http://www.pinterest.com/upload-image/?img=" + HttpUtility.UrlEncode(Path.GetFileName(path)), "http://www.pinterest.com/", "multipart/form-data; boundary=" + boundary, lb, hdrs, c, acc.proxy, "*/*");


          if (RS.Contains("[[[ERROR]]]"))
          {
            //MessageBox.Show("2");
            if (errors > ERRORS_MAX) { abort = true; }
            ++errors; delete(path); continue;
          }


          string pattern = "http(?:s*?)://(.+?)\",";
          if (!Regex.IsMatch(RS, pattern))
          {
            //MessageBox.Show("3");
            if (errors > ERRORS_MAX) { abort = true; }
            ++errors; delete(path); continue;
          }
          Match match = Regex.Match(RS, pattern, RegexOptions.Singleline);
          string upload_url = "http://" + match.Groups[1].Value.ToString();

          if (upload_url.Length <= 0) continue;

          if (abort) { active = false; return; }





          lb = new List<byte[]>();
          string hd = "source_url=" + HttpUtility.UrlEncode("/") + "&data=" + HttpUtility.UrlEncode("{\"options\":{\"board_id\":\"" + boardID + "\",\"description\":\"" + desc + "\",\"link\":\"\",\"image_url\":\"" + upload_url + "\",\"method\":\"uploaded\"},\"context\":{}}") + "&module_path=PinUploader" + HttpUtility.UrlEncode("(show_title=true, shrinkToFit=true)#Modal(module=PinCreate())");



          hd = Regex.Replace(hd, "%\\d(\\w)", m => m.ToString().ToUpper());
          headerbytes = Encoding.ASCII.GetBytes(hd);
          lb.Add(headerbytes);

          hdrs = new List<string>();
          hdrs.Add("Accept-Encoding: gzip, deflate");
          hdrs.Add("Accept-Language: en-gb,en;q=0.5");
          hdrs.Add("X-NEW-APP: 1");
          hdrs.Add("X-Requested-With: XMLHttpRequest");
          hdrs.Add("X-CSRFToken: " + CSRFToken);
          hdrs.Add("X-APP-VERSION: " + app_version);
          hdrs.Add("Cache-Control: no-cache");
          hdrs.Add("Pragma: no-cache");
          hdrs.Add("Origin: http://www.pinterest.com");


          if (abort) { active = false; return; }

          RS = http.POST("http://www.pinterest.com/resource/PinResource/create/", "http://www.pinterest.com/", "application/x-www-form-urlencoded; charset=UTF-8", lb, hdrs, c, acc.proxy, "application/json, text/javascript, */*; q=0.01");

          if (RS.Contains("[[[ERROR]]]"))
          {
            //MessageBox.Show("4");
            if (errors > ERRORS_MAX) { abort = true; }
            ++errors; delete(path); continue;
          }

          if (abort) { active = false; return; }


          Random r = new Random();
          if (RS.Contains("\"error\": null}"))
          {
            //MessageBox.Show("5");
            delete(path);
            Console.WriteLine("Uploaded" + category);
            ++uploaded;
          }
          else
          {
            Console.WriteLine("ERROR: upload");
          }


          if (source_url.Length > 0)
          {
            string pinid = Regex.Match(RS, " \"id\": \"(\\d+?)\"}, \"error\"").Groups[1].ToString();
            if (pinid.Length > 0)
            {
              hd = "source_url=" + HttpUtility.UrlEncode("/" + acc.Username + "/pins/") + "&data=" + HttpUtility.UrlEncode("{\"options\":{\"board_id\":\"" + boardID + "\",\"description\":\"" + desc + "\",\"link\":\"" + source_url + "\",\"id\":\"" + pinid + "\"},\"context\":{}}") + "&module_path=" + HttpUtility.UrlEncode("App()>UserProfilePage(resource=UserResource(username=" + acc.Username + "))>UserProfileContent(resource=UserResource(username=" + acc.Username + "))>Grid(resource=UserPinsResource(username=" + acc.Username + "))>GridItems(resource=UserPinsResource(username=" + acc.Username + "))>Pin(resource=PinResource(id=" + pinid + "))>ShowModalButton(module=PinEdit)#Modal(module=PinEdit(resource=PinResource(id=" + pinid + ")))");

              lb = new List<byte[]>();
              headerbytes = Encoding.ASCII.GetBytes(hd);
              lb.Add(headerbytes);

              RS = http.POST("http://www.pinterest.com/resource/PinResource/update/ ", "http://www.pinterest.com/pin/" + pinid + "/", "application/x-www-form-urlencoded; charset=UTF-8", lb, hdrs, c, acc.proxy, "application/json, text/javascript, */*; q=0.01");
            }
          }


          status = "timeout";

          int sleepP = r.Next(timeout_min * 1000, timeout_max * 1000);
          while (sleepP > 0)
          {
            Thread.Sleep(100); sleepP -= 100;
            if (abort) { active = false; return; }
          }

          if (maxUploads > 0 && uploaded >= maxUploads)
          {
            if (_continuousSettings.ContinuousRun)
            {
              uploaded = 0;

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
              status = "max!";
              abort = true;
              active = false;
              return;
            }
          }
        }
        catch (Exception ex) { if (debug) MessageBox.Show(ex.StackTrace); Thread.Sleep(5000); }
      }
    }

    private string status;
    override public string ToString()
    {
      return "pinned: " + uploaded + "  " + status;
    }



    void delete(string path)
    {
      SQLiteConnection m_dbConnection = new SQLiteConnection(@"Data Source=db;Version=3;New=False;Compress=True;");
      m_dbConnection.Open();
      string sql = "delete from pins where path='" + path + "'";
      SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
      command.ExecuteNonQuery();
      m_dbConnection.Close();
      File.Delete(path);
    }

  }
}
