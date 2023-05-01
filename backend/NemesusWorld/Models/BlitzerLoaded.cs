using System;
using System.Collections.Generic;
using System.Text;

namespace NemesusWorld.Models
{
    class BlitzerLoaded
    {
        public int id { get; set; }
        public string who { get; set; }
        public int maxspeed { get; set; }
        public string from { get; set; }
        public string position { get; set; }

        public BlitzerLoaded()
        {
            id = 0;
            who = "";
            maxspeed = 50;
            from = "";
            position = "";
        }
    }
}
