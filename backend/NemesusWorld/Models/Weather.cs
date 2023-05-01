using System;
using System.Collections.Generic;
using System.Text;

namespace NemesusWorld.Utils
{
    class Weather
    {
        public int id { get; set; }
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }

        public Weather()
        {
            id = 0;
            main = "";
            description = "";
            icon = "";
        }
    }
}
