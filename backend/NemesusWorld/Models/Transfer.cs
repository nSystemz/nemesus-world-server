using System;
using System.Collections.Generic;
using System.Text;

namespace NemesusWorld.Models
{
    [PetaPoco.TableName("transfer")]
    [PetaPoco.PrimaryKey("id")]
    class Transfer
    {
        public int id { get; set; }
        public string bankfrom { get; set; }
        public string bankto { get; set; }
        public string banktext { get; set; }
        public int bankvalue { get; set; }
        public string bankname { get; set; }

        public Transfer()
        {
            id = 0;
            bankfrom = "n/A";
            bankto = "n/A";
            banktext = "n/A";
            bankvalue = 0;
            bankname = "n/A";
        }
    }
}
