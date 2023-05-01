using System;
using System.Collections.Generic;
using System.Text;

namespace NemesusWorld.Models
{
    [PetaPoco.TableName("factionsrangs")]
    [PetaPoco.PrimaryKey("id")]
    class FactionRangs
    {
        public int id { get; set; }
        public int factionid { get; set; }
        public string rang1 { get; set; }
        public string rang2 { get; set; }
        public string rang3 { get; set; }
        public string rang4 { get; set; }
        public string rang5 { get; set; }
        public string rang6 { get; set; }
        public string rang7 { get; set; }
        public string rang8 { get; set; }
        public string rang9 { get; set; }
        public string rang10 { get; set; }
        public string rang11 { get; set; }
        public string rang12 { get; set; }


        public FactionRangs()
        {
            id = 0;
            factionid = 0;
            rang1 = "n/A";
            rang2 = "n/A";
            rang3 = "n/A";
            rang4 = "n/A";
            rang5 = "n/A";
            rang6 = "n/A";
            rang7 = "n/A";
            rang8 = "n/A";
            rang9 = "n/A";
            rang10 = "n/A";
            rang11 = "n/A";
            rang12 = "n/A";
        }
    }
}
