using System;
using System.Collections.Generic;
using System.Text;

namespace NemesusWorld.Models
{
    class Dispatch
    {
        public int id { get; set; }
        public string text { get; set; }
        public string name { get; set; }
        public string phonenumber { get; set; }
        public int to { get; set; }
        public int timestamp { get; set; }
        public string position { get; set; }
        public string editor { get; set; }
        public string status { get; set; }

        public Dispatch()
        {
            id = 0;
            text = "";
            name = "";
            phonenumber = "";
            to = 0;
            timestamp = 1609462861;
            position = "";
            editor = "n/A";
            status = "Offen";
        }
    }
}
