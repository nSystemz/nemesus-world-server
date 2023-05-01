using System;
using System.Collections.Generic;
using System.Text;

namespace NemesusWorld.Models
{
    [PetaPoco.TableName("factions")]
    [PetaPoco.PrimaryKey("id")]
    class FactionsModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string tag { get; set; }
        public int leader { get; set; }
        public int created { get; set; }
        public long bankvalue { get; set; }
        public int members { get; set; }
        [PetaPoco.ResultColumn]
        public string number { get; set; }
        [PetaPoco.ResultColumn]
        public string numbername { get; set; }

        public FactionsModel()
        {
            id = 0;
            name = "";
            tag = "";
            leader = -1;
            created = 1609462861;
            bankvalue = 0;
            members = 0;
            number = "";
            numbername = "";
        }
    }
}
