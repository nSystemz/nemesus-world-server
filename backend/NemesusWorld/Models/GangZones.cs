using System;
using System.Collections.Generic;

namespace NemesusWorld.Models
{
    [PetaPoco.TableName("gangzones")]
    [PetaPoco.PrimaryKey("id")]
    public class GangZones
    {
        public int id { get; set; }
        public string name { get; set; }
        public string value { get; set; }
        public string posx { get; set; }
        public string posy { get; set; }
        public string posz { get; set; }
        public string heading { get; set; }
        public int color { get; set; }
        public int radius { get; set; }
        public string percentages { get; set; }
        public int things { get; set; }

        public GangZones()
        {
            id = 0;
            name = "Gangzone-"+id;
            value = "Materialien";
            percentages = "";
            posx = "0.0";
            posy = "0.0";
            posz = "0.0";
            heading = "0.0";
            color = 40;
            radius = 50;
            percentages = "n/A";
            things = 0;
        }
    }
}

