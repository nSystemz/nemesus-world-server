using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace NemesusWorld.Models
{
    class ATMSpots
    {
        public int id { get; set; }
        public Vector3 position { get; set; }
        public int value { get; set; }
        public int dist { get; set; }

        public ATMSpots()
        {
            id = 0;
            position = null;
            value = 50000;
            dist = 0;
        }
    }
}
