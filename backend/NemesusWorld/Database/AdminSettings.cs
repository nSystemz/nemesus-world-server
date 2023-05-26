using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace NemesusWorld.Database
{
    [PetaPoco.TableName("adminsettings")]
    [PetaPoco.PrimaryKey("id")]
    class AdminSettings
    {
        public int id { get; set; }
        public string adminpassword { get; set; }
        public int server_created { get; set; }
        public int punishments { get; set; }
        public int voicerp { get; set; }
        public int govvalue { get; set; }
        public int changelogcd { get; set; }
        public int towedcash { get; set; }
        public int lsteuer { get; set; }
        public int gsteuer { get; set; }
        public float ksteuer { get; set; }
        public string groupsettings { get; set; }
        [PetaPoco.ResultColumn]
        public string[] grouparray { get; set; }
        public int adprice { get; set; }
        public int adcount { get; set; }
        public int admoney { get; set; }
        public int dailyguesslimit { get; set; }
        //Nametag | 0 = Nur Admins haben Nametags, 1 = Name unbekannt außer du bist befreundet mit jemanden, 2 alle haben Nametags
        public int nametag { get; set; }

        public AdminSettings()
        {
            id = 0;
            adminpassword = "";
            server_created = 1609462861;
            punishments = 0;
            voicerp = 0;
            govvalue = 0;
            changelogcd = 1609462861;
            towedcash = 250;
            lsteuer = 7;
            gsteuer = 5;
            ksteuer = 0.12f;
            groupsettings = "3750,1500,55000,65000,55000,45000,42500";
            grouparray = null;
            adprice = 1500;
            adcount = 0;
            admoney = 0;
            dailyguesslimit = 0;
            nametag = 0;
        }
    }
}
