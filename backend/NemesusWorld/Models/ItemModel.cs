using System;
using System.Collections.Generic;
using System.Text;

namespace NemesusWorld.Models
{
    [PetaPoco.TableName("itemmodels")]
    [PetaPoco.PrimaryKey("id")]
    class ItemModel
    {
        public int id { get; set; }
        public string hash { get; set; }
        public string description { get; set; }
        public int type { get; set; }
        public int weight { get; set; }

        public ItemModel()
        {
            id = 0;
            hash = "n/A";
            description = "n/A";
            type = 0;
            weight = 0;
        }
    }
}
