using System;
using System.Collections.Generic;
using System.Text;

namespace NemesusWorld.Models
{
    [PetaPoco.TableName("bank")]
    [PetaPoco.PrimaryKey("id")]
    class Bank
    {
        public int id { get; set; }
        public string banknumber { get; set; }
        public int bankvalue { get; set; }
        public string pincode { get; set; }
        public int ownercharid { get; set; }
        public int groupid { get; set; }
        public int banktype { get; set; }

        public Bank()
        {
            id = 0;
            banknumber = "n/A";
            bankvalue = 0;
            pincode = "0000";vvvvvvv
            ownercharid = 0;
            groupid = 0;
            banktype = 0;
        }
    }
}
