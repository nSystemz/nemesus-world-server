using System;

namespace NemesusWorld.Models
{
    public class Payday
    {
        public string modus { get; set; }
        public string setting { get; set; }
        public int value { get; set; }

        public Payday()
        {
            modus = "Privat";
            setting = "Fraktionslohn";
            value = 0;
        }
    }
}

