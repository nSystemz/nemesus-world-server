using System;
using System.Collections.Generic;
using System.Text;

namespace NemesusWorld.Models
{
    [PetaPoco.TableName("shopitems")]
    [PetaPoco.PrimaryKey("id")]
    class ShopItems
    {
        public int id { get; set; }
        public string shoplabel { get; set; }
        public string itemname { get; set; }
        public int itemprice { get; set; }
        [PetaPoco.ResultColumn]
        public int itemamount { get; set; }

        public ShopItems()
        {
            id = 0;
            shoplabel = "n/A";
            itemname = "n/A";
            itemprice = 0;
            itemamount = -1;
        }
    }
}
