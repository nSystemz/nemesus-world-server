using GTANetworkAPI;

namespace NemesusWorld.Models
{
    class DoorsColshapes : Script
    {
        public int doorid { get; set; }
        public ColShape doorshape { get; set; }

        public DoorsColshapes()
        {
            doorid = 0;
            doorshape = null;
        }
    }
}
