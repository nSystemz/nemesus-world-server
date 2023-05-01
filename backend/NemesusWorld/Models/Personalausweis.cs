using System;
using System.Collections.Generic;
using System.Text;

namespace NemesusWorld.Models
{
    class Personalausweis
    {
        public int id { get; set; }
        public string name { get; set; }
        public string birthday { get; set; }
        public string size { get; set; }
        public string from { get; set; }
        public string eyecolor { get; set; }

        public Personalausweis()
        {
            id = 0;
            name = "";
            birthday = "";
            size = "";
            from = "";
            eyecolor = "";
        }

    }
}
