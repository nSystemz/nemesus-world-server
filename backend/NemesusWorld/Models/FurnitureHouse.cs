using System;
using System.Collections.Generic;
using System.Text;

namespace NemesusWorld.Models
{
    [PetaPoco.TableName("furniturehouse")]
    [PetaPoco.PrimaryKey("id")]
    class FurnitureHouse
    {
        public int id { get; set; }
        public int house { get; set; }
        public int extra { get; set; }
        public string name { get; set; }
        public string hash { get; set; }
        public string position { get; set; }
        public string props { get; set; }
        public int price { get; set; }

        public FurnitureHouse()
        {
            id = 0;
            house = 0;
            extra = 0;
            name = "n/A";
            hash = "n/A";
            position = "0.0|0.0|0.0|0.0|0.0|0.0|0";
            props = "n/A";
            price = 0;
        }
    }
}
