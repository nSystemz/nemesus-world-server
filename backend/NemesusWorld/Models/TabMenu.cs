using System;
using System.Collections.Generic;
using System.Text;

namespace NemesusWorld.Models
{
    class TabMenu
    {
        public int id { get; set; }
        public string name { get; set; }
        public bool admin { get; set; }
        public int level { get; set; }
        public int ping { get; set; }

        public TabMenu()
        {
            id = 0;
            name = "n/A";
            admin = false;
            level = 1;
            ping = 25;
        }
    }
}
