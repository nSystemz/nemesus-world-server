using System;
using System.Collections.Generic;
using System.Text;

namespace NemesusWorld.Models
{
    [PetaPoco.TableName("groups")]
    [PetaPoco.PrimaryKey("id")]
    class Groups
    {
        public int id { get; set; }
        public string name { get; set; }
        public int created { get; set; }
        public string banknumber { get; set; }
        public int leader { get; set; }
        public int members { get; set; }
        public int status { get; set; }
        public string licenses { get; set; }
        public int provision { get; set; }
        public int maxplusvehicles { get; set; }
        public int service { get; set; }
        [PetaPoco.ResultColumn]
        public string number { get; set; }
        [PetaPoco.ResultColumn]
        public string numbername { get; set; }
        [PetaPoco.ResultColumn]
        public string leadername { get; set; }

        public Groups()
        {
            id = 0;
            name = "n/A";
            created = 1609462861;
            banknumber = "n/A";
            leader = -1;
            members = 0;
            status = 0;
            licenses = "0|0|0|0|0|0|0|0|0";
            provision = 0;
            maxplusvehicles = 0;
            service = 0;
            number = "";
            numbername = "";
            leadername = "";
        }
    }
}
