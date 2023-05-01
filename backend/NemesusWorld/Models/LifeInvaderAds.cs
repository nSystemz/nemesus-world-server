using System;
using System.Collections.Generic;
using System.Text;

namespace NemesusWorld.Models
{
    [PetaPoco.TableName("lifeinvaderads")]
    [PetaPoco.PrimaryKey("id")]
    class LifeInvaderAds
    {
        public int id { get; set; }
        public string text { get; set; }
        public string name { get; set; }
        public string number { get; set; }
        public string status { get; set; }
        public string editor { get; set; }
        public int timestamp { get; set; }

        public LifeInvaderAds()
        {
            id = 0;
            text = "";
            name = "";
            number = "";
            status = "Offen";
            editor = "n/A";
            timestamp = 0;
        }
    }
}
