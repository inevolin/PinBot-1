using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinBot
{
    class archive
    {/*
      * 
      * 
      * 
       public void clearDB()
        {
            try
            {
                m_dbConnection.Open();
                string sql = "delete from tags";
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                command.ExecuteNonQuery();
                m_dbConnection.Close();
            }
            catch { }
        }
        
        public void insert(string db, string cols, string vals)
        {
            m_dbConnection.Open();
            string sql;
            sql = "insert into " + db + " (" + cols + ") values (" + vals + ")";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            m_dbConnection.Close();
        }
        public Dictionary<string, int> selectAll(string category, int limit = 0)
        {
            Dictionary<string, int> tags = new Dictionary<string, int>();
            m_dbConnection.Open();
            string sql = "select name, count(*) as c from tags where category='" + category + "' GROUP BY name ORDER BY c DESC";
            sql += (limit == 0) ? "" : " LIMIT " + limit;
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader sqlreader = command.ExecuteReader();
            while (sqlreader.Read())
            {
                string s = sqlreader["name"].ToString();
                string i = sqlreader["c"].ToString();
                tags.Add(s, int.Parse(i));
            }
            m_dbConnection.Close();
            return tags;
        }
        
       // foreach (KeyValuePair<string, int> f in a)
         //   Console.WriteLine(f.Key + " \t " + f.Value);

      * 
      * 
      * 
      * 
      * 
        bool old_upload(string image_url, string desc, string boardID)
        {
            string image_name = Path.GetFileName(image_url);

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create("http://www.pinterest.com/upload-image/?img=" + HttpUtility.UrlEncode(image_name));

            req.ContentType = "multipart/form-data; boundary=" + http.boundary;
            req.Method = "POST";
            req.KeepAlive = true;
            req.Referer = "https://www.pinterest.com/";
            req.UserAgent = ("Mozilla/5.0 (Windows NT 6.1; WOW64; rv:28.0) Gecko/20100101 Firefox/28.0");
            req.Accept = ("");
            req.Headers.Add("X-Requested-With: XMLHttpRequest");
            req.Headers.Add("X-CSRFToken: " + CSRFToken);
            req.Headers.Add("X-File-Name:" + HttpUtility.UrlEncode(image_name));
            req.Headers.Add("Accept-Encoding: gzip,deflate,sdch");
            req.Headers.Add("Accept-Language: en-US,en;q=0.8");
            req.CookieContainer = c;
            req.ServicePoint.Expect100Continue = false;
            ServicePointManager.Expect100Continue = false;
            req.AutomaticDecompression = DecompressionMethods.GZip;


            StringBuilder strBuilder = new StringBuilder();


            strBuilder.AppendLine("--" + http.boundary);
            strBuilder.AppendLine("Content-Disposition: form-data; name=\"img\"; filename=\"" + image_name + "\"");
            strBuilder.Append("Content-Type: image/jpeg");
            strBuilder.AppendLine();
            strBuilder.AppendLine();


            byte[] hdrbytes = System.Text.Encoding.UTF8.GetBytes(strBuilder.ToString());
            byte[] filedata = null;
            using (BinaryReader breader = new BinaryReader(File.OpenRead(image_url)))
                filedata = breader.ReadBytes((int)breader.BaseStream.Length);
            byte[] endboundarybytes = System.Text.Encoding.ASCII.GetBytes(Environment.NewLine + "--" + http.boundary + "--" + Environment.NewLine);

            Stream stream = req.GetRequestStream();
            stream.Write(hdrbytes, 0, hdrbytes.Length);
            stream.Write(filedata, 0, filedata.Length);
            stream.Write(endboundarybytes, 0, endboundarybytes.Length);
            stream.Close();

            HttpWebResponse res = (HttpWebResponse)req.GetResponse();
            StreamReader reader = new StreamReader(res.GetResponseStream());
            string RS = reader.ReadToEnd();

            reader.Close();
            req.Abort();
            stream.Close();


            string x1 = "\"image_url\": \"";
            string x2 = "\", \"success\"";
            string uploaded_url = RS.Substring(RS.IndexOf(x1) + x1.Length, RS.IndexOf(x2) - RS.IndexOf(x1) - x1.Length);







            req = (HttpWebRequest)WebRequest.Create("http://www.pinterest.com/resource/PinResource/create/");
            req.ContentType = "application/x-www-form-urlencoded";
            req.Method = "POST";
            req.KeepAlive = true;
            req.Referer = "https://www.pinterest.com/login/";
            req.UserAgent = ("Mozilla/5.0 (iPad; U; CPU OS 3_2 like Mac OS X; en-us) AppleWebKit/531.21.10 (KHTML, like Gecko) Version/4.0.4 Mobile/7B334b Safari/531.21.10");
            req.Accept = ("application/json, text/javascript; q=0.01");
            req.Headers.Add("X-Requested-With: XMLHttpRequest");
            req.Headers.Add("X-CSRFToken: " + CSRFToken);
            req.Headers.Add("X-NEW-APP: 1");
            req.CookieContainer = c;
            req.ServicePoint.Expect100Continue = false;
            ServicePointManager.Expect100Continue = false;
            req.AutomaticDecompression = DecompressionMethods.GZip;


            string login = "source_url=/&data={\"options\":{\"board_id\":\"" + boardID + "\",\"description\":\"" + HttpUtility.UrlEncode(desc) + "\",\"link\":\"\",\"share_twitter\":false,\"image_url\":\"" + uploaded_url + "\",\"method\":\"uploaded\"},\"context\":{\"app_version\":\"" + app_version + "\"}}&module_path=PinUploader(show_title=true, shrinkToFit=true)#Modal(module=PinCreate())";
            byte[] headerbytes = Encoding.ASCII.GetBytes(login);

            stream = req.GetRequestStream();
            stream.Write(headerbytes, 0, headerbytes.Length);
            stream.Close();
            res = (HttpWebResponse)req.GetResponse();
            stream = res.GetResponseStream();
            reader = new StreamReader(stream);
            RS = reader.ReadToEnd();

            res.Close();
            req.Abort();
            stream.Close();
            reader.Close();

            if (RS.Contains("\"error\": null}}"))
            { connected = true; uploaded++; return true; }
            else
            { connected = false; return false; }

        }


        public static DialogResult InputBox(string title, string promptText, ref string value)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            label.Text = promptText;
            textBox.Text = value;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            return dialogResult;
        }


        void load_boards()
        {
            cboBoards.Items.Add("autumn outfits");
            cboBoards.Items.Add("summer outfits");
            cboBoards.Items.Add("winter outfits");
            cboBoards.Items.Add("lingerie");
            cboBoards.Items.Add("jewelry");
            cboBoards.Items.Add("accessories");
        }
       */
    }
}



/*

 using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace PinBot
{
    class bluefly
    {
        CookieContainer c;
        public bluefly(CookieContainer c)
        {
            this.c = c;
        }

        void dl(string url)
        {
            string RS = http.GET(url, "", c, null);

            string pattern = "";

            if (url.Contains("bluefly.com"))
            {
                //http://www.bluefly.com/sauipe-swimwear-summer-top/PRODUCT_FEED/331933801/detail.fly
                List<string> imgs = new List<string>();
                pattern += "largeimage: '(.+?)'";
                foreach (Match m in Regex.Matches(RS, pattern, RegexOptions.Multiline))
                {
                    imgs.Add(HttpUtility.HtmlDecode(m.Groups[1].Value.ToString()));

                }

                pattern = "<h3>designer's notes</h3>(?:(.|\n)*?)<p>(.*?)</p>";
                Match match = Regex.Match(RS, pattern, RegexOptions.Singleline);
                string description = match.Groups[2].Value.ToString().Replace("  ", "").Replace("\t", "").Replace("\n\n", "\n");

                pattern = "<h2 class=\"product-name\" itemprop=\"name\">(.*?)</h2>";
                match = Regex.Match(RS, pattern, RegexOptions.Singleline);
                string name = match.Groups[1].Value.ToString().Trim();


                string newFileName = ".\\prods.csv";
                string clientDetails = "_________________" + Environment.NewLine + name + Environment.NewLine + description + Environment.NewLine + url + Environment.NewLine + Environment.NewLine + Environment.NewLine;
                if (!File.Exists(newFileName))
                {
                    string clientHeader = "name" + "," + "description" + "," + "url" + Environment.NewLine;

                    File.WriteAllText(newFileName, clientHeader);
                }
                File.AppendAllText(newFileName, clientDetails);


                int i = 0;
                foreach (string s in imgs)
                {
                    string localFilename = ".\\" + name + " (" + (i++) + ").jpg";
                    while (File.Exists(localFilename))
                        localFilename = ".\\" + name + " (" + (i++) + ").jpg";
                    using (WebClient client = new WebClient())
                    {
                        client.DownloadFile(s, localFilename);
                    }
                }
            }

        }


    }
}


*/