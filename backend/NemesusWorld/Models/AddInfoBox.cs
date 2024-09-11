using GTANetworkAPI;
using NemesusWorld.Utils;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace NemesusWorld.Models
{
    class AddInfoBox
    {
        public int id { get; set; }
        public Vector3 position { get; set; }
        public GTANetworkAPI.TextLabel label { get; set; }
        public GTANetworkAPI.Marker marker { get; set; }
        public String creator { get; set; }
        public int created { get; set; }

        public AddInfoBox()
        {
            id = 0;
            creator = "n/A";
            created = Helper.UnixTimestamp();
        }
    }
}
