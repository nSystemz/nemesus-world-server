using System;
using System.Collections.Generic;
using System.Text;

namespace NemesusWorld.Models
{
    class Lics
    {
        public int id { get; set; }
        public string name { get; set; }
        public string birthday { get; set; }

        public Lics()
        {
            id = 0;
            name = "";
            birthday = "";
        }
    }
}
