using System;
using System.Collections.Generic;
using System.Text;

namespace NemesusWorld.Models
{
    class BusRoutes
    {
        public int id { get; set; }
        public string name { get; set; }
        public string routes { get; set; }
        public string advert { get; set; }
        public int skill { get; set; }

        public BusRoutes()
        {
            id = 0;
            name = "";
            routes = "";
            advert = "";
            skill = 1;
        }
    }
}
