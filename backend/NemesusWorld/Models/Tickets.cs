using System;
using System.Collections.Generic;
using System.Text;

namespace NemesusWorld.Models
{
    class Tickets
    {
        public int id { get; set; }
        public string user { get; set; }
        public string title { get; set; }
        public string prio { get; set; }
        public string text { get; set; }
        public int timestamp { get; set; }
        public string admin { get; set; }
        public int status { get; set; }

        public Tickets()
        {
            id = -1;
            user = "";
            title = "";
            prio = "";
            text = "";
            timestamp = 1609462861;
            admin = "";
            status = 0;
        }
    }
}
