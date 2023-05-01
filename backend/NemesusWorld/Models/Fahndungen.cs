using System;
using System.Collections.Generic;
using System.Text;

namespace NemesusWorld.Models
{
    [PetaPoco.TableName("fahndungen")]
    [PetaPoco.PrimaryKey("id")]
    class Fahndungen
    {
        public int id { get; set; }
        public string text { get; set; }
        public int status { get; set; }
        public string creator { get; set; }
        public string editor { get; set; }
        public int timestamp { get; set; }

        public void Fahndung()
        {
            id = 0;
            text = "";
            status = 0;
            creator = "n/A";
            editor = "n/A";
            timestamp = 1609462861;
        }
    }
}
