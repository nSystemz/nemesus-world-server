using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace NemesusWorld.Models
{
    class TaxiBots
    {
        public int id { get; set; }
        public string from { get; set; }
        public string to { get; set; }
        public int money { get; set; }
        public Vector3 v1 { get; set; }
        public Vector3 v2 { get; set; }

        public TaxiBots()
        {
            id = 0;
            from = "";
            to = "";
            money = 0;
            v1 = null;
            v2 = null;
        }
    }
}
