using System;
using System.Collections.Generic;
using System.Text;

namespace NemesusWorld.Models
{
    [PetaPoco.TableName("whitelist")]
    [PetaPoco.PrimaryKey("id")]
    class Whitelist
    {
        public int id { get; set; }
        public string name { get; set; }
        public ulong socialclubid { get; set; }
        public int timestamp { get; set; }

        public Whitelist()
        {
            id = 0;
            name = "";
            socialclubid = 10;
            timestamp = 1609462861;
        }
    }
}
