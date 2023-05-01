using System;
using System.Collections.Generic;
using System.Text;

namespace NemesusWorld.Models
{
    [PetaPoco.TableName("Standingorder")]
    [PetaPoco.PrimaryKey("id")]
    class Standingorder
    {
        public int id { get; set; }
        public int ownercharid { get; set; }
        public string bankfrom { get; set; }
        public string bankto { get; set; }
        public int bankvalue { get; set; }
        public string banktext { get; set; }
        public int days { get; set; }
        public int daysrun { get; set; }
        public int timestamp { get; set; }

        public Standingorder()
        {
            id = 0;
            ownercharid = 0;
            bankfrom = "n/A";
            bankto = "n/A";
            bankvalue = 0;
            banktext = "n/A";
            days = 0;
            daysrun = 0;
            timestamp = 1609462861;
        }
    }
}
