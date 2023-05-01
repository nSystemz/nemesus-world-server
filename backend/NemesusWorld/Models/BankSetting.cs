using System;
using System.Collections.Generic;
using System.Text;

namespace NemesusWorld.Models
{
    [PetaPoco.TableName("banksetting")]
    [PetaPoco.PrimaryKey("id")]
    class BankSetting
    {
        public int id { get; set; }
        public string banknumber { get; set; }
        public string setting { get; set; }
        public int value { get; set; }
        public string name { get; set; }
        public int timestamp { get; set; }

        public BankSetting()
        {
            id = 0;
            banknumber = "n/A";
            setting = "n/A";
            value = 0;
            name = "n/A";
            timestamp = 1609462861;
        }
    }
}
