using System;
using System.Collections.Generic;
using System.Text;

namespace NemesusWorld.Models
{
    [PetaPoco.TableName("tattoos")]
    [PetaPoco.PrimaryKey("id")]
    class Tattoos
    {
        public int id { get; set; }
        public int characterid { get; set; }
        public string name { get; set; }
        public string dlcname { get; set; }
        public int zoneid { get; set; }

        public Tattoos()
        {
            id = 0;
            characterid = 0;
            name = "";
            dlcname = "";
            zoneid = 0;
        }
    }
}
