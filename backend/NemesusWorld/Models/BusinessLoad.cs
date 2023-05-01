using System;
using System.Collections.Generic;
using System.Text;

namespace NemesusWorld.Models
{
    [PetaPoco.TableName("business")]
    [PetaPoco.PrimaryKey("id")]
    class BusinessLoad
    {
        public int id { get; set; }
        public float posx { get; set; }
        public float posy { get; set; }
        public float posz { get; set; }
        public int price { get; set; }
        public string name { get; set; }
        public string name2 { get; set; }
        public string owner { get; set; }
        public int funds { get; set; }
        public int products { get; set; }
        public float multiplier { get; set; }
        public int cash { get; set; }
        public int govcash { get; set; }
        public int prodprice { get; set; }
        public int blipid { get; set; }
        public int buyproducts { get; set; }
        public int selled { get; set; }
        public int getmoney { get; set; }
        public int elec { get; set; }
        [PetaPoco.ResultColumn]
        public string elecname { get; set; }


        public BusinessLoad()
        {
            id = 0;
            posx = 0.0f;
            posy = 0.0f;
            posz = 0.0f;
            price = 0;
            name = "Business";
            name2 = "Business";
            owner = "n/A";
            funds = 0;
            products = 2000;
            multiplier = 1.0f;
            cash = 0;
            govcash = 0;
            prodprice = 0;
            blipid = 0;
            buyproducts = 0;
            selled = 0;
            getmoney = 25000;
            elec = 50;
            elecname = "n/A";
        }
    }
}
