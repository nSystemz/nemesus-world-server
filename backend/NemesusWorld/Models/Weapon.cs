using System;
using System.Collections.Generic;
using System.Text;

namespace NemesusWorld.Models
{
    [PetaPoco.TableName("weapons")]
    [PetaPoco.PrimaryKey("id")]
    class Weapon
    {
        public int id { get; set; }
        public string ident { get; set; }
        public string name { get; set; }
        public string shop { get; set; }
        public string weaponname { get; set; }
        public int timestamp { get; set; }

        public Weapon()
        {
            id = 0;
            ident = "";
            name = "";
            shop = "";
            weaponname = "";
            timestamp = 1609462861;
        }
    }
}
