using System;
using System.Collections.Generic;
using System.Text;

namespace NemesusWorld.Models
{
    [PetaPoco.TableName("globalitems")]
    [PetaPoco.PrimaryKey("id")]
    class ItemsGlobalModel
    {
        public int id { get; set; }
        public string json { get; set; }

        public ItemsGlobalModel()
        {
            json = "n/A";
        }
    }
}
