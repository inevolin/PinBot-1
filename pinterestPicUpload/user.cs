using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PinBot
{
    public class user
    {
        string username, userid;
        bool following, following_me; //am I following this person?
        bool partner;
        string fb, tw; // has FB, Twitter
        string location;
        string website;
        int intPins, intFollowers, intFollowing, intLikes, intBoards;


        public string Username { get { return username; } set { username = value; } }
        public string Location { get { return location; } set { location = value; } }
        public string Website { get { return website; } set { website = value; } }
        public string Userid { get { return userid; } set { userid = value; } }
        public string FB { get { return fb; } set { fb = value; } } //facebook
        public string TW { get { return tw; } set { tw = value; } } //twitter

        public int IntPins { get { return intPins; } set { intPins = value; } }
        public int IntFollowers { get { return intFollowers; } set { intFollowers = value; } }
        public int IntFollowing { get { return intFollowing; } set { intFollowing = value; } }
        public int IntLikes { get { return intLikes; } set { intLikes = value; } }
        public int IntBoards { get { return intBoards; } set { intBoards = value; } }

        public bool Following { get { return following; } set { following = value; } }
        public bool Partner { get { return partner; } set { partner = value; } }
        public bool Following_me { get { return following_me; } set { following_me = value; } }

        public bool isBoard;
        public string Board_user_board;
        public string Board_username;
    }
}
