using System;
using System.Collections.Generic;
using System.Text;

namespace NemesusWorld.Models
{
    [PetaPoco.TableName("bankfile")]
    [PetaPoco.PrimaryKey("id")]
    class BankFile
    {
        public int id { get; set; }
        public string bankfrom { get; set; }
        public string bankto { get; set; }
        public string banktext { get; set; }
        public int bankvalue { get; set; }
        public int banktime { get; set; }

        public BankFile()
        {
            id = 0;
            bankfrom = "n/A";
            bankto = "n/A";
            banktext = "n/A";
            bankvalue = 0;
            banktime = 1609462861;
        }
    }
}
