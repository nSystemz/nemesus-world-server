using NemesusWorld.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace NemesusWorld.Models
{
    [PetaPoco.TableName("smartphonemessages")]
    [PetaPoco.PrimaryKey("id")]
    class Messages
    {
        public int id { get; set; }
        public string tomessage { get; set; }
        public string frommessage { get; set; }
        public string text { get; set; }
        public int timestamp { get; set; }

        public Messages()
        {
            tomessage = "n/A";
            frommessage = "n/A";
            text = "n/A";
            timestamp = Helper.UnixTimestamp();
        }
    }
}
