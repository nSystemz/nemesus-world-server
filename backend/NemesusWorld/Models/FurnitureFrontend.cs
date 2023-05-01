using System;
using System.Collections.Generic;
using System.Text;

namespace NemesusWorld.Models
{
    class FurnitureFrontend
    {
        public int id { get; set; }
        public string name { get; set; }
        public string position { get; set; }
        public int price { get; set; }

        public FurnitureFrontend()
        {
            id = 0;
            name = "n/A";
            position = "0.0|0.0|0.0|0.0|0.0|0.0|0";
            price = 0;
        }
    }
}
