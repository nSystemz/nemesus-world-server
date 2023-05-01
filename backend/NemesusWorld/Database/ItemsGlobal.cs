using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace NemesusWorld.Database
{
    class ItemsGlobal
    {
        public int ownerid { get; set; }
        public string owneridentifier { get; set; }
        public int itemid { get; set; }
        public string hash { get; set; }
        public string description { get; set; }
        public int amount { get; set; }
        public int type { get; set; }
        public int weight { get; set; }
        public string props { get; set; }
        public float posx { get; set; }
        public float posy { get; set; }
        public float posz { get; set; }
        public uint dimension { get; set; }
        public int lastupdate { get; set; }
        public GTANetworkAPI.Object objectHandle { get; set; }
        public GTANetworkAPI.TextLabel textHandle { get; set; }

        public ItemsGlobal()
        {
            ownerid = 0;
            owneridentifier = "n/A";
            itemid = 0;
            hash = "n/A";
            description = "n/A";
            amount = 0;
            weight = 0;
            type = 0;
            props = "n/A";
            posx = 0.0f;
            posy = 0.0f;
            posz = 0.0f;
            dimension = 0;
            lastupdate = 1609462861;
            objectHandle = null;
            textHandle = null;
        }
    }
}
