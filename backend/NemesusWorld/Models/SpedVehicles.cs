using System;
using System.Collections.Generic;
using System.Text;

namespace NemesusWorld.Models
{
    [PetaPoco.TableName("spedvehicles")]
    [PetaPoco.PrimaryKey("id")]
    class SpedVehicles
    {
        public int id { get; set; }
        public string name { get; set; }
        public int capa { get; set; }
        public int skill { get; set; }
        public string skilltext { get; set; }

        public SpedVehicles()
        {
            id = 0;
            name = "n/A";
            capa = 0;
            skill = 0;
            skilltext = "n/A";
        }
    }
}
