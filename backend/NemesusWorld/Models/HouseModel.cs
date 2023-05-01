using System;
using System.Collections.Generic;
using System.Text;

namespace NemesusWorld.Models
{
    [PetaPoco.TableName("houses")]
    [PetaPoco.PrimaryKey("id")]
    class HouseModel
    {
        public int id { get; set; }
        public float posx { get; set; }
        public float posy { get; set; }
        public float posz { get; set; }
        public int dimension { get; set; }
        public int price { get; set; }
        public int interior { get; set; }
        public string owner { get; set; }
        public int status { get; set; }
        public int tenants { get; set; }
        public int rental { get; set; }
        public int locked { get; set; }
        public int noshield { get; set; }
        public string streetname { get; set; }
        public int blip { get; set; }
        public int housegroup { get; set; }
        public int stock { get; set; }
        public int stockprice { get; set; }
        public int classify { get; set; }
        public int elec { get; set; }
        [PetaPoco.ResultColumn]
        public string elecname { get; set; }

        public HouseModel()
        {
            id = 0;
            posx = 0.0f;
            posy = 0.0f;
            posz = 0.0f;
            dimension = 0;
            price = 0;
            interior = 0;
            owner = "n/A";
            status = 0;
            tenants = 0;
            rental = 0;
            locked = 0;
            noshield = 0;
            streetname = "n/A";
            blip = 40;
            housegroup = -1;
            stock = 0;
            stockprice = 30;
            classify = 0;
            elec = 50;
            elecname = "n/A";
        }
    }
}
