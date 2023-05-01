using System;
using System.Collections.Generic;
using System.Text;

namespace NemesusWorld.Models
{
    class GarbageRoutes
    {
        public int id { get; set; }
        public string name { get; set; }
        public string routes { get; set; }

        public GarbageRoutes()
        {
            id = 0;
            name = "";
            routes = "";
        }
    }
}
