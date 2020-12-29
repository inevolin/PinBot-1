using System;
using System.Collections.Generic;
using System.Data.SQLite;
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
    class weheartit : scrape
    {
        

        public weheartit(int limit, account acc, Watermark watermark_settings, string strPinDescURL, string strSourceURL, bool autoPilot, bool TRIAL, int count_checked) : base(limit, acc, watermark_settings, strPinDescURL, strSourceURL, autoPilot, TRIAL, count_checked) { }

        public void start_thread()
        {
            active = true;

            Thread thread = new Thread(start_scrape);
            thread.Start();
        }


        private void start_scrape()
        {
            int page = 1;

            usedPins(); // fill the already seen pins

            while (active)
            {
                status = "scraping";
                try
                {
                    Random r = new Random();
                    Dictionary<string, List<string>> dic = new Dictionary<string, List<string>>();
                    foreach (KeyValuePair<string, List<string>> k in acc.boards_category_mapped_pin)
                    {
                        if (k.Value.Count > 0)
                            dic.Add(k.Key, k.Value);
                    }
                    KeyValuePair<string, List<string>> kv = dic.ElementAt(r.Next(0, dic.Count));
                    categories = kv.Value;
                    boardID = kv.Key;


                    if (abort) { active = false; return; }


                    category = categories.ElementAt(r.Next(0, categories.Count));
                    string url = "http://weheartit.com/search/entries?&query=" + category + "&page=" + page;
                    string RS = http.GET(url, "", c, null,null);
                    if (RS.Contains("<h2>Nothing Found</h2>")) { abort = true; status = "nothing found"; }
                    if (abort) { active = false; return; }

                    List<Pin> temp_pins = getPins(RS);

                    for (int i = 0; i < temp_pins.Count; i++)
                    {
                        if (limit > 0)
                        {
                            while (pause())
                            {
                                if (abort) { active = false; return; }
                                Thread.Sleep(1000);
                            }
                        }

                        if (abort) { active = false; return; }

                        Pin pin = temp_pins.ElementAt(i);

                        pin.category = category;
                        RS = http.GET("http://weheartit.com/entry/" + pin.entryId + "/", "http://weheartit.com/?page=" + page, c, null, null);
                        if (RS.Contains("[[[ERROR]]]")) continue;

                        if (abort) { active = false; return; }
                        getPinDetails(RS, ref pin);
                        if (abort) { active = false; return; }

                        pin.boardID = boardID;
                        genDescription(ref pin);

                        if (!excludePin(((Bitmap)pin.pic)))
                            savePin(ref pin);//THE MAGIC HAPPENS HERE

                    }

                    ++page;
                    if (page > 30)
                        page = 1;

                }
                catch { Thread.Sleep(1000); }
            }
        }

        List<Pin> getPins(string RS)
        {
            List<Pin> t_pins = new List<Pin>();
            //string pat = "<div class=\"entry\"(?:(.|\\n)+?)";
            //pat += "data-entry-id=\"(\\d+?)\"(?:(.|\\n)+?)"; //entry id
            //pat += "data-uploader-username=\"(.+?)\"(?:(.|\\n)+?)"; //username
            //pat += "src=\"(.+?)\"(?:(.|\\n)+?)"; //image url
            //pat += "/entry/group/(\\d+?)\"(?:(.|\\n)+?)";
            //pat += "<span>(\\d+?)</span><i>hearts</i>"; //#hearts
            string pat = "<div class=\"entry\"(?:(.|\n)+?)/entry/(\\d+?)/(?:(.|\n)+?)src=\"(.+?)\"";

            foreach (Match m in Regex.Matches(RS, pat, RegexOptions.Multiline))
            {
                Pin pin = new Pin();
                pin.entryId = m.Groups[2].ToString();
                //pin.username = m.Groups[4].ToString();
                pin.url = m.Groups[4].ToString(); //only thumb --"
                //pin.groupId = m.Groups[8].ToString();
                //pin.likes = int.Parse(m.Groups[10].ToString());
                t_pins.Add(pin);
            }
            return t_pins;
        }
        void getPinDetails(string RS, ref Pin pin)
        {
            string pat;

            try
            {
                pat = "<meta content=\"(.+?)\" itemprop=\"contentUrl\" />";
                Match match = Regex.Match(RS, pat, RegexOptions.Multiline);
                pin.url = match.Groups[1].ToString();

                string url = pin.url;
                HttpWebRequest httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
                if (null != null)
                    httpWebRequest.Proxy = null;
                HttpWebResponse httpWebReponse = (HttpWebResponse)httpWebRequest.GetResponse();
                Stream stream = httpWebReponse.GetResponseStream();
                pin.pic = Image.FromStream(stream);
            }
            catch { }

            /*try { 
                 pat = ">(\\d+?) collection[s\"]";
                Match match = Regex.Match(RS, pat, RegexOptions.Singleline);
                pin.collections = int.Parse(match.Groups[1].ToString());
            }
            catch { }*/

            try
            {
                pat = "Description: <span class=\"light\">(.+?)</span>";
                Match m = Regex.Match(RS, pat, RegexOptions.Multiline);
                pin.description = m.Groups[1].Value.ToString();
                //pat = "<span class=\"light\">(?!<)(.*)(?!</a>)</li>";
                //pin.descriptions = new List<string>();
                /*foreach (Match m in Regex.Matches(RS, pat, RegexOptions.Multiline))
                {
                    string s = HttpUtility.HtmlDecode(m.Groups[1].ToString().Replace("?", ""));
                    pin.descriptions.Add(s);
                }*/
            }
            catch { }

            try
            {
                pat = "class=\"tag\"(.+?)>(.+?)</a>";
                pin.tags = new List<string>();
                foreach (Match m in Regex.Matches(RS, pat, RegexOptions.Multiline))
                {
                    string s = HttpUtility.HtmlDecode(m.Groups[2].ToString().Replace("?", ""));
                    pin.tags.Add(s);
                }
            }
            catch { }

            //return pin;

        }
        bool algo(ref Pin pin)
        {
            if (watermark_settings != null)
                pin.pic = addWatermark(pin.pic);


            return true;

        }
        void genDescription(ref Pin pin)
        {
            Random r = new Random();

            //pin.description = pin.descriptions[r.Next(0,pin.descriptions.Count)];
            int rr = 0;

            if (autoPilot)
            {
                if (strPinDescURL.Length > 0 && acc.numPinWebsiteInDesc > 0)
                {
                    websiteInDesc_spread += acc.numPinWebsiteInDesc == 1 ? 1 : (acc.numPinWebsiteInDesc);
                    if (websiteInDesc_spread >= 100)
                    {
                        rr = r.Next(0, 2);
                        string[] divs = { " | ", " ", " , ", " . ", "  @ " };
                        string dv = divs[r.Next(0, divs.Length)];
                        if (rr == 0)
                            pin.description = strPinDescURL + dv + pin.description;
                        else
                            pin.description = pin.description + dv + strPinDescURL;

                        websiteInDesc_spread = 0;
                    }
                }
            }
            else
            {
                if (strPinDescURL.Length > 0 && acc.numScrapeWebsiteInDesc > 0)
                {
                    websiteInDesc_spread += acc.numScrapeWebsiteInDesc == 1 ? 1 : (acc.numScrapeWebsiteInDesc);
                    if (websiteInDesc_spread >= 100)
                    {
                        rr = r.Next(0, 2);
                        string[] divs = { " | ", " ", " , ", " . ", "  @ " };
                        string dv = divs[r.Next(0, divs.Length)];
                        if (rr == 0)
                            pin.description = strPinDescURL + dv + pin.description;
                        else
                            pin.description = pin.description + dv + strPinDescURL;

                        websiteInDesc_spread = 0;
                    }
                }
            }


            rr = r.Next(1, 3);
            if (rr == 1)
            {
                int c = pin.tags.ToArray().Length;
                int z = r.Next(1, c);
                List<string> temp_tags = new List<string>();
                string tag = pin.tags[r.Next(c)];
                for (int i = 0; i < z; i++)
                {
                    if (temp_tags.Contains(tag))
                        continue;
                    temp_tags.Add(tag);
                    int y = r.Next(0, 2);
                    if (y == 0)
                    {
                        pin.description += " " + tag;
                    }
                    else if (y == 1)
                    {
                        pin.description += " #" + tag;
                    }

                    tag = pin.tags[r.Next(c)];
                    if (temp_tags.Contains(tag))
                        continue;

                    if (!(i == z - 1))
                    {
                        y = r.Next(0, 3);
                        if (y == 0)
                            pin.description += " ";
                        else if (y == 1)
                            pin.description += " - ";
                        else if (y == 2)
                            pin.description += ", ";
                    }

                }
            }
            else if (rr == 2)
            {
                List<string> chars = new List<string>(new string[] { "✿", "☺", "☻", "☂", "✍", "✌", "☮", "✔", "★", "☆", "♦", "♣", "♠", "♥", "❤", "♪", "♫", "♯", "∽", "❁", "♕", "♡", "❂", "✦", "❃" });
                //int c = chars.ToArray().Length;
                int z = r.Next(1, 5);
                for (int i = 0; i < z; i++)
                {
                    pin.description += " " + chars[r.Next(z)];
                    if (!(i == z - 1))
                    {
                        int y = r.Next(0, 3);
                        if (y == 0)
                            pin.description += " ";
                        else if (y == 1)
                            pin.description += "";
                        else if (y == 2)
                            pin.description += ".";
                    }
                }
            }
        }

        public override string ToString()
        {
            return status;
        }

        
    }
}
