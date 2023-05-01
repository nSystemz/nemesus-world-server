using NemesusWorld.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace NemesusWorld.Models
{
    [PetaPoco.TableName("smartphonecalls")]
    [PetaPoco.PrimaryKey("id")]
    class PhoneCalls
    {
        public int id { get; set; }
        public string tonumber { get; set; }
        public string fromnumber { get; set; }
        public int timestamp { get; set; }

        public PhoneCalls()
        {
            id = 0;
            tonumber = "";
            fromnumber = "";
            timestamp = Helper.UnixTimestamp();
        }
    }
}
