using System;
using System.Collections.Generic;
using System.Text;

namespace NemesusWorld.Models
{
    [PetaPoco.TableName("logs")]
    [PetaPoco.PrimaryKey("id")]
    class Logs
    {
        public int id { get; set; }
        public string loglabel { get; set; }
        public string text { get; set; }
        public int timestamp { get; set; }

        public Logs()
        {
            id = 0;
            loglabel = "n/A";
            text = "n/A";
            timestamp = 1609462861;
        }
    }
}
