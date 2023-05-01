using System;
using System.Collections.Generic;
using System.Text;

namespace NemesusWorld.Models
{
    [PetaPoco.TableName("documents")]
    [PetaPoco.PrimaryKey("id")]
    class Documents
    {
        public int id { get; set; }
        public string title { get; set; }
        public string link { get; set; }
        public string owner { get; set; }
        public int timestamp { get; set; }
        public string creator { get; set; }

        public Documents()
        {
            id = 0;
            title = "n/A";
            link = "n/A";
            owner = "n/A";
            timestamp = 1609462861;
            creator = "n/A";
        }
    }
}
