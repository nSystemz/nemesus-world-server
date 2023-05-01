using System;
using System.Collections.Generic;
using System.Text;

namespace NemesusWorld.Models
{
    [PetaPoco.TableName("policefile")]
    [PetaPoco.PrimaryKey("id")]
    class Policefile
    {
        public int id { get; set; }
        public string name { get; set; }
        public string police { get; set; }
        public string text { get; set; }
        public int timestamp { get; set; }
        public int commentary { get; set; }

        public Policefile()
        {
            id = 0;
            name = "n/A";
            police = "n/A";
            text = "n/A";
            timestamp = 1609462861;
            commentary = 0;
        }
    }
}
