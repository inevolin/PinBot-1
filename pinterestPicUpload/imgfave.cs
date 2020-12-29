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
    class imgfave : scrape
    {

        public imgfave(int limit, account acc, Watermark watermark_settings, string strPinDescURL, string strSourceURL, bool autoPilot, bool TRIAL, int count_checked) : base(limit, acc, watermark_settings, strPinDescURL, strSourceURL, autoPilot, TRIAL, count_checked) { }

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

                    string url = "http://imgfave.com/search/" + HttpUtility.UrlEncode(category) + "/page:" + page;
                    string RS = http.GET(url, "", c, null, null);
                    if (RS.Contains("No results found</div>")) { abort = true; status = "nothing found"; }
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
                        pin.boardID = boardID;
                        getPinDetails(RS, ref pin);
                        genDescription(ref pin);
                        if (pin.description.Length <= 0) pin.description = ".";

                        if ( ! excludePin(((Bitmap)pin.pic)) )
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
            string pat = "<div id=\"item_(?:(.|\\n)+?)";
            pat += "data-href=\"(.+?)\"(.*?)</div>"; //image url

            foreach (Match m in Regex.Matches(RS, pat, RegexOptions.Multiline))
            {
                Pin pin = new Pin();
                pin.url = m.Groups[2].ToString();
                pin.tags = new List<string>();
                pin.tags.Add(m.Groups[3].ToString());
                t_pins.Add(pin);
            }

            for (int i = 0; i < t_pins.Count; i++) //FLEKHEBEFNBEIUZNFIZE XD goodnight
            {
                pat = "class=\"image_tag_link\"(.+?)\\>(.+?)\\<"; //tags

                RS = t_pins[i].tags[0];
                t_pins[i].tags.Clear();
                foreach (Match m in Regex.Matches(RS, pat, RegexOptions.Multiline))
                {
                    if (m.Groups[2] != null) t_pins[i].tags.Add(m.Groups[2].ToString());
                }
            }

            return t_pins;
        }
        void getPinDetails(string RS, ref Pin pin)
        {
            string url = pin.url;
            HttpWebRequest httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
            if (null != null)
                httpWebRequest.Proxy = null;
            HttpWebResponse httpWebReponse = (HttpWebResponse)httpWebRequest.GetResponse();
            Stream stream = httpWebReponse.GetResponseStream();
            pin.pic = Image.FromStream(stream);

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

            int rr = 0;
            int c = pin.tags.ToArray().Length;
            rr = r.Next( c > 0 ? 1 : 2, 3);
            if (rr == 1)
            {
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
        }

        

        public override string ToString()
        {
            return status;
        }


    }
}
