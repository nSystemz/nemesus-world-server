using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace NemesusWorld.Database
{
    class Items
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
        public int misc { get; set; }

        public Items()
        {
            ownerid = 0;
            owneridentifier = "n/A";
            itemid = 0;
            hash = "n/A";
            description = "n/A";
            amount = 0;
            type = 0;
            weight = 0;
            props = "n/A";
            misc = 0;
        }
    }
}
