using System;
using System.Collections.Generic;
using System.Text;

namespace NemesusWorld.Models
{
    class Taxi
    {
        public string name { get; set; }
        public int id { get; set; }
        public int playerid { get; set; }
        public string text { get; set; }
        public string nummer { get; set; }
        public string standort { get; set; }
        public string done { get; set; }

        public Taxi()
        {
            name = "";
            id = -1;
            playerid = -1;
            text = "";
            nummer = "";
            standort = "";
            done = "n/A";
        }
    }
}
