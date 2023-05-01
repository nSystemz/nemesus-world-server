using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace NemesusWorld.Models
{
    class SpikeStrip
    {
        public int id { get; set; }
        public GTANetworkAPI.Object spikeobject { get; set; }
        public GTANetworkAPI.ColShape colshape { get; set; }
        public SpikeStrip()
        {
            id = 0;
            spikeobject = null;
            colshape = null;
        }
    }
}
