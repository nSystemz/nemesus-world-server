using System;
using GTANetworkAPI;
using Object = GTANetworkAPI.Object;

namespace NemesusWorld.Models
{
    [PetaPoco.TableName("drugs")]
    [PetaPoco.PrimaryKey("id")]
    public class DrugsPlants
    {
        public int id { get; set; }
        public int owner { get; set; }
        public int value { get; set; }
        public int time { get; set; }
        public int vw { get; set; }
        public int water { get; set; }
        public string posx { get; set; }
        public string posy { get; set; }
        public string posz { get; set; }
        public string posa { get; set; }
        public string drugname { get; set; }
        [PetaPoco.ResultColumn]
        public TextLabel textLabel { get; set; }
        [PetaPoco.ResultColumn]
        public Object obj { get; set; }

        public DrugsPlants()
        {
            id = 0;
            owner = -1;
            value = 0;
            time = 60;
            vw = 0;
            water = 100;
            posx = "0.0";
            posy = "0.0";
            posz = "0.0";
            posa = "0.0";
            drugname = "Marihuana";
            textLabel = null;
            obj = null;
        }
    }
}

