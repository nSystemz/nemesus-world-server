using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace NemesusWorld.Models
{
    class FurnitureSetHouse : Script
    {
        public int id { get; set; }
        public int house { get; set; }
        public int extra { get; set; }
        public string name { get; set; }
        public string hash { get; set; }
        public string position { get; set; }
        public string props { get; set; }
        public int price { get; set; }
        public GTANetworkAPI.Object objectHandle { get; set; }

        public FurnitureSetHouse()
        {
            id = 0;
            house = 0;
            extra = 0;
            name = "n/A";
            hash = "n/A";
            position = "0.0|0.0|0.0|0.0|0.0|0.0|0";
            props = "n/A";
            price = 0;
            objectHandle = null;
        }
    }
}
