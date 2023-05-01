using System;
using System.Collections.Generic;
using System.Text;

namespace NemesusWorld.Models
{
    [PetaPoco.TableName("houseinteriors")]
    [PetaPoco.PrimaryKey("id")]
    class HouseInteriorModel
    {
        public int id { get; set; }
        public string ipl { get; set; }
        public float posx { get; set; }
        public float posy { get; set; }
        public float posz { get; set; }
        public int classify { get; set; }

        public HouseInteriorModel()
        {
            id = 1;
            ipl = "n/A";
            posx = 0.0f;
            posy = 0.0f;
            posz = 0.0f;
            classify = 1;
        }
    }
}
