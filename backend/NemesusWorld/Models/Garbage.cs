using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace NemesusWorld.Models
{
    class Garbage : Script
    {
        public bool created { get; set; }
        public GTANetworkAPI.Object objectHandle { get; set; }
        public Vector3 position { get; set; }

        public Garbage()
        {
            created = false;
            objectHandle = null;
            position = null;
        }
    }
}
