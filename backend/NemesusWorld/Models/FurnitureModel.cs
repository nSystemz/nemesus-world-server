using System;
using System.Collections.Generic;
using System.Text;

namespace NemesusWorld.Models
{
    [PetaPoco.TableName("furniture")]
    [PetaPoco.PrimaryKey("id")]
    class FurnitureModel
    {
        public int id { get; set; }
        public string hash { get; set; }
        public string name { get; set; }
        public int categorie { get; set; }
        public int extra { get; set; }
        public long price { get; set; }

        public FurnitureModel()
        {
            id = 0;
            hash = "0";
            name = "";
            categorie = 0;
            extra = 0;
            price = 0;
        }
    }
}
