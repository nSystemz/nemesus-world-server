using System;
using System.Collections.Generic;
using System.Text;

namespace NemesusWorld.Models
{
    [PetaPoco.TableName("vehicleshop")]
    [PetaPoco.PrimaryKey("id")]
    class VehicleShop
    {
        public int id { get; set; }
        public int bizzid { get; set; }
        public string vehiclename { get; set; }
        public int price { get; set; }
        public int maxspeed { get; set; }
        public float maxacceleration { get; set; }
        public float maxbraking { get; set; }
        public float maxhandling { get; set; }
        public int trunk { get; set; }
        public int fuel { get; set; }

        public VehicleShop()
        {
            id = 0;
            bizzid = 0;
            vehiclename = "n/A";
            price = 0;
            maxspeed = 0;
            maxacceleration = 0.0f;
            maxhandling = 0.0f;
            maxbraking = 0.0f;
            trunk = 0;
            fuel = 0;
        }
    }
}
