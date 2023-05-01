using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace NemesusWorld.Models
{
    class PoliceProps
    {
        public int id { get; set; }
        public GTANetworkAPI.Object obj { get; set; }

        public PoliceProps()
        {
            id = 0;
            obj = null;
        }
    }
}
