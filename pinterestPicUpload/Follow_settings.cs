using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinBot
{
    [Serializable]
    public class Follow_settings
    {
        public int followersMin, followersMax;
        public int followingMin, followingMax;
        public int boardsMin, boardsMax;
        public int pinsMin, pinsMax;

        //unfollow
        public bool isPartner, hasWebsite, notFollowingYou;

        //follow
        public bool hasTW, hasFB;

        public bool ignoreCriteria;
    }
}
