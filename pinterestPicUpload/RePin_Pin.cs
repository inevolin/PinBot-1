using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace PinBot
{
    [Serializable]
    public class RePin_Pin
    {
        //pin by search
        public string url, link, description, id, category, boardID; //public int repin_count;
        public bool is_repin, liked_by_me, is_upload;
        public Image img;
        public string img_url;

        //pin by users
        public string part, user_boardID;
        public bool userSearch;

        public int DB_ID;
    }
}
