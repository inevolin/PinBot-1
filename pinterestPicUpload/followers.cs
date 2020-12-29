using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace PinBot
{
    class followers
    {
        protected CookieContainer c = new CookieContainer();
        protected account acc;
        protected string app_version = "", CSRFToken = "";
        protected bool active = false;
        protected bool abort = false;
        protected int timeout_min = 60;
        protected int timeout_max = 300;

        public bool Active { get { return active; } set { active = value; } }
        public bool Abort { get { return abort; } set { abort = value; } }

        public followers(account acc)
        {
            this.acc = acc;
            this.c = acc.c;
            this.CSRFToken = acc.CSRFtoken;
            this.app_version = acc.App_version;
        }

        public user setUserData(user u, string referral)
        {
            if (abort) { active = false; return null; }
            string RS = http.GET("http://www.pinterest.com/" + u.Username + "/", referral, c, null, acc.proxy).Replace(",", "");
            string pattern = "";
            pattern += "\"last_name\": \"";
            pattern += @"(?:(.|\n)*?)"; // 1

            pattern += "\"following_count\": (\\d+?) \""; //2
            pattern += @"(?:(.|\n)*?)";

            pattern += "\"like_count\": (\\d+?) \""; //4
            pattern += @"(?:(.|\n)*?)";

            pattern += "\"id\": \"(\\d+?)\" \""; //6
            pattern += @"(?:(.|\n)*?)";

            pattern += "\"explicitly_followed_by_me\": (.+?) "; //8
            pattern += @"(?:(.|\n)*?)";

            pattern += "\"location\": \"(.+?)\" \""; //10
            pattern += @"(?:(.|\n)*?)";

            pattern += "\"follower_count\": (\\d+?) \""; //12
            pattern += @"(?:(.|\n)*?)";

            pattern += "\"is_partner\": (.+?) \""; //14
            pattern += @"(?:(.|\n)*?)";

            pattern += "\"board_count\": (\\d+?) \""; //16
            pattern += @"(?:(.|\n)*?)";

            pattern += "twitter_url\": (.+?) \""; //18
            pattern += @"(?:(.|\n)*?)";

            pattern += "facebook_url\": (.+?) \""; //20
            pattern += @"(?:(.|\n)*?)";

            pattern += "\"pin_count\": (\\d+?) \""; //22
            pattern += @"(?:(.|\n)*?)";

            pattern += "\"website_url\": (.+?) \""; //24

            Match m = Regex.Match(RS, pattern, RegexOptions.Multiline);

            u.IntFollowing = int.Parse(m.Groups[2].ToString());
            u.IntLikes = int.Parse(m.Groups[4].ToString());
            u.Userid = m.Groups[6].ToString();
            u.Following = m.Groups[8].ToString() == "true" ? true : false;
            u.Location = m.Groups[10].ToString();
            u.IntFollowers = int.Parse(m.Groups[12].ToString());
            u.Partner = bool.Parse(m.Groups[14].ToString());
            u.IntBoards = int.Parse(m.Groups[16].ToString());
            u.TW = m.Groups[18].ToString();
            u.FB = m.Groups[20].ToString();
            u.IntPins = int.Parse(m.Groups[22].ToString());
            u.Website = m.Groups[24].ToString();

            return u;
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
    }
}
