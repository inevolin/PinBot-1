using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace PinBot
{
    class scrape
    {

        public bool active = false;
        public bool abort = false;
        public bool autoPilot = false;
        public bool weheartit = false, imgfave = false;//list of selected sources
        private bool TRIAL;

        private weheartit w;
        private imgfave imgf;
        
        protected string strPinDescURL;
        protected string app_version = "", CSRFToken = "";
        protected string category = "";
        protected string boardID = "";
        protected string strSourceURL = "";
        public string status;

        protected CookieContainer c = new CookieContainer();
        protected account acc;
        protected Watermark watermark_settings;
        
        protected List<string> categories = new List<string>();
        protected List<string> pins_exclude = new List<string>();

        protected int limit = 0;
        protected int count_checked;

        public int scraped = 0;
        static int source_url_spread = 0;
        static protected int websiteInDesc_spread = 0;

        //Added new scrape source:
        /* *
            Edit constructor
            Edit start()
            Edit stop()
            Edit ToString()
         * */

        public scrape(int limit, account acc, Watermark watermark_settings, string strPinDescURL, string strSourceURL, bool autoPilot, bool TRIAL, int count_checked = 0)
        {
            this.limit = limit;
            this.acc = acc;
            this.c = acc.c;
            this.CSRFToken = acc.CSRFtoken;
            this.app_version = acc.App_version;
            this.watermark_settings = watermark_settings;
            this.strPinDescURL = strPinDescURL;
            this.autoPilot = autoPilot;
            this.count_checked = count_checked;
            this.TRIAL = TRIAL;
            this.strSourceURL = strSourceURL;

            if (autoPilot && TRIAL)
            {
                weheartit = true;
                this.count_checked = 1;
            }
            else if (autoPilot)
            {
                weheartit = true;
                imgfave = true;
                //...
                this.count_checked = 2;
            }
        }

        
        public void start()
        {
            status = "scraping";
            active = true;
            if (weheartit)
            {
                w = new weheartit(limit, acc, watermark_settings, strPinDescURL, strSourceURL, autoPilot, TRIAL, count_checked);
                w.start_thread();
            }
            if (imgfave)
            {
                imgf = new imgfave(limit, acc, watermark_settings, strPinDescURL, strSourceURL, autoPilot, TRIAL, count_checked);
                imgf.start_thread();
            }
        }
        public void stop()
        {
            if (w != null)
            {
                w.abort = true;
                while (w.active)
                    Application.DoEvents();
                w = null;
            }

            if (imgf != null)
            {
                imgf.abort = true;
                while (imgf.active)
                    Application.DoEvents();
                imgf = null;
            }

            status = "";
            active = false;
        }


        public string getInQueue()
        {
            SQLiteConnection m_dbConnection = new SQLiteConnection(@"Data Source=db;Version=3;New=False;Compress=True;");
            m_dbConnection.Open();
            string sql = "select count(*) as c from pins";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader sqlreader = command.ExecuteReader();
            string i = "";
            while (sqlreader.Read())
            {
                i = sqlreader["c"].ToString();
            }
            m_dbConnection.Close();
            return i;
        }
        protected bool excludePin( Bitmap image )
        {
            
            try {

                System.Security.Cryptography.HashAlgorithm ha = System.Security.Cryptography.HashAlgorithm.Create();

                byte[] hash_b = new byte[0];
                using (MemoryStream stream = new MemoryStream())
                {
                    image.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                    stream.Close();
                    hash_b = stream.ToArray();
                }

                string hash = "";
                using (SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider())
                {
                    hash = Convert.ToBase64String(sha1.ComputeHash(hash_b));
                }

                if (pins_exclude.Contains(hash)) return true;
                pins_exclude.Add(hash);

                using (FileStream fs = new FileStream("./exclude_pins.txt", FileMode.Append, FileAccess.Write))
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.WriteLine(hash);
                }

            }
            catch { }

            return false;
        }
        protected void usedPins()
        {
            if (!File.Exists("./exclude_pins.txt"))
                using (var fileStream = File.Create("./exclude_pins.txt")) { }

            using (var reader = new StreamReader("./exclude_pins.txt"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    pins_exclude.Add(line);
                }
            }
        }
        protected void savePin(ref Pin pin)
        {

            try
            {
                string d = ".\\imgs\\";

                if (!System.IO.Directory.Exists(d))
                    System.IO.Directory.CreateDirectory(d);

                string ex = ".png";
                Bitmap bmp = new Bitmap(pin.pic);
                string saveFilePath = bmp.GetHashCode().ToString();
                Random r = new Random();
                while (System.IO.File.Exists(d + saveFilePath + ex))
                    saveFilePath += r.Next(0, 1000000);
                bmp.Save(d + saveFilePath + ex, System.Drawing.Imaging.ImageFormat.Png);


                string source_url = "";
                if (autoPilot)
                {
                    if (acc.numPinSourceURL > 0 && strSourceURL.Length > 0)
                    {
                        source_url_spread += acc.numPinSourceURL == 1 ? 1 : (acc.numPinSourceURL);
                        if (source_url_spread >= 100)
                        {
                            source_url = strSourceURL.Replace("'", "");
                            source_url_spread = 0;
                        }
                    }
                }
                else
                {
                    if (acc.numScrapeSourceURL > 0 && strSourceURL.Length > 0)
                    {
                        source_url_spread += acc.numScrapeSourceURL == 1 ? 1 : (acc.numScrapeSourceURL);
                        if (source_url_spread >= 100)
                        {
                            source_url = strSourceURL.Replace("'", "");
                            source_url_spread = 0;
                        }
                    }
                }

                SQLiteConnection m_dbConnection = new SQLiteConnection(@"Data Source=db;Version=3;New=False;Compress=True;");
                m_dbConnection.Open();
                string sql = "insert into  pins  (path, category, description, tags, boardID, source_url) values ('" + d + saveFilePath + ex + "','" + pin.category.Replace("'", "''") + "','" + pin.description.Replace("'", "''") + "','" + string.Join(",", pin.tags.ToArray()).Replace("'", "''") + "', '" + pin.boardID + "', '" + source_url + "')";
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                command.ExecuteNonQuery();
                m_dbConnection.Close();


                ++scraped;

            }
            catch { }

        }
        public override string ToString()
        {
            string s = "";
            int i = 0;
            if (weheartit && w != null)
                i += w.scraped;
            if (imgfave && imgf != null)
                i += imgf.scraped;
            s = (i >= limit * count_checked) ? "(" + i + ")" + " max!" : "(" + i + ")";
            return status + " " + s;
        }
        protected Image addWatermark(Image img)
        {

            Image imageBackground = new Bitmap(img);
            Image imageOverlay = new Bitmap(watermark_settings.image);

            Image image = new Bitmap(imageBackground.Width, imageBackground.Height);
            using (Graphics gr = Graphics.FromImage(image))
            {
                gr.DrawImage(imageBackground, new Point(0, 0));

                if (watermark_settings.randomWatermark)
                {
                    Random r = new Random();
                    switch (watermark_settings.positions[r.Next(0, watermark_settings.positions.Count)])
                    {
                        case "TOPLEFT":
                            if (watermark_settings.safeWatermark && !isSafe(image, imageOverlay)) break;
                            gr.DrawImage(imageOverlay, new Point(0, 0));
                            break;
                        case "TOPRIGHT":
                            if (watermark_settings.safeWatermark && !isSafe(image, imageOverlay)) break;
                            gr.DrawImage(imageOverlay, new Point(image.Width - imageOverlay.Width, 0));
                            break;
                        case "BOTLEFT":
                            if (watermark_settings.safeWatermark && !isSafe(image, imageOverlay)) break;
                            gr.DrawImage(imageOverlay, new Point(0, image.Height - imageOverlay.Height));
                            break;
                        case "BOTRIGHT":
                            if (watermark_settings.safeWatermark && !isSafe(image, imageOverlay)) break;
                            gr.DrawImage(imageOverlay, new Point(image.Width - imageOverlay.Width, image.Height - imageOverlay.Height));
                            break;
                        case "MID":
                            if (watermark_settings.safeWatermark && !isSafe(image, imageOverlay)) break;
                            gr.DrawImage(imageOverlay, new Point(image.Width / 2 - imageOverlay.Width / 2, image.Height / 2 - imageOverlay.Height / 2));
                            break;
                    }
                }
                else
                {
                    for (int i = 0; i < watermark_settings.positions.Count; i++)
                    {

                        switch (watermark_settings.positions[i])
                        {
                            case "TOPLEFT":
                                if (watermark_settings.safeWatermark && !isSafe(image, imageOverlay)) break;
                                gr.DrawImage(imageOverlay, new Point(0, 0));
                                break;
                            case "TOPRIGHT":
                                if (watermark_settings.safeWatermark && !isSafe(image, imageOverlay)) break;
                                gr.DrawImage(imageOverlay, new Point(image.Width - imageOverlay.Width, 0));
                                break;
                            case "BOTLEFT":
                                if (watermark_settings.safeWatermark && !isSafe(image, imageOverlay)) break;
                                gr.DrawImage(imageOverlay, new Point(0, image.Height - imageOverlay.Height));
                                break;
                            case "BOTRIGHT":
                                if (watermark_settings.safeWatermark && !isSafe(image, imageOverlay)) break;
                                gr.DrawImage(imageOverlay, new Point(image.Width - imageOverlay.Width, image.Height - imageOverlay.Height));
                                break;
                            case "MID":
                                if (watermark_settings.safeWatermark && !isSafe(image, imageOverlay)) break;
                                gr.DrawImage(imageOverlay, new Point(image.Width / 2 - imageOverlay.Width / 2, image.Height / 2 - imageOverlay.Height / 2));
                                break;
                        }
                    }
                }

            }
            return image;
        }
        protected bool isSafe(Image bg, Image w)
        {
            if (w.Width * 3 >= bg.Width) return false;
            if (w.Height * 3 >= bg.Height) return false;

            return true;

        }
        protected bool pause()
        {
            status = "scraping (" + scraped + ")";

            if (autoPilot && int.Parse(getInQueue()) >= 5)
            { return true; }

            if (!autoPilot && scraped >= limit)
                return true;

            return false;

        }

        protected struct Pin
        {
            public string entryId, url, category, description;
            public List<string> tags;
            public Image pic;
            public string boardID;
        }
    }
}
