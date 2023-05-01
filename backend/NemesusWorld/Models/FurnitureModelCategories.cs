using System;
using System.Collections.Generic;
using System.Text;

namespace NemesusWorld.Models
{
    [PetaPoco.TableName("furniturecategories")]
    [PetaPoco.PrimaryKey("id")]
    class FurnitureModelCategories
    {
        public int id { get; set; }
        public string name { get; set; }

        public FurnitureModelCategories()
        {
            id = 0;
            name = "";
        }
    }
}
