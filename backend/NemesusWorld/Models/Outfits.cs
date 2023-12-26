using System;
using System.Collections.Generic;
using System.Text;

namespace NemesusWorld.Models
{
    [PetaPoco.TableName("outfits")]
    [PetaPoco.PrimaryKey("id")]
    class Outfits
    {
        public int id { get; set; }
        public string name { get; set; }
        public string owner { get; set; }
        public string json1 { get; set; }
        public string json2 { get; set; }
        public string category1 { get; set; }
        public string category2 { get; set; }

        public Outfits()
        {
            id = 0;
            name = "";
            owner = "";
            json1 = "";
            json2 = "";
            category1 = "n/A";
            category2 = "n/A";
        }
    }
}
