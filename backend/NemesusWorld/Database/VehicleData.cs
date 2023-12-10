using System;
using System.Collections.Generic;
using System.Text;

namespace NemesusWorld.Database
{
    [PetaPoco.TableName("vehicles")]
    [PetaPoco.PrimaryKey("id")]
    class VehicleData
    {
        public int id { get; set; }
        public string position { get; set; }
        public string owner { get; set; }
        public string vehiclename { get; set; }
        public string ownname { get; set; }
        public string plate { get; set; }
        public string health { get; set; }
        public int battery { get; set; }
        public int status { get; set; }
        public int engine { get; set; }
        public float kilometre { get; set; }
        public int tuev { get; set; }
        public int oel { get; set; }
        public float fuel { get; set; }
        public int rang { get; set; }
        public string tuning { get; set; }
        public string garage { get; set; }
        public string sync { get; set; }
        public string color { get; set; }
        public int products { get; set; }
        public int vlock { get; set; }
        public string doors { get; set; }
        public string windows { get; set; }
        public int towed { get; set; }

        public VehicleData()
        {
            id = 0;
            position = "0.0|0.0|0.0|0.0|0";
            owner = "n/A";
            vehiclename = "n/A";
            ownname = "n/A";
            plate = "n/A";
            health = "1000|1000|1000";
            battery = 100;
            status = 1;
            engine = 0;
            kilometre = 0.0f;
            tuev = 0;
            oel = 100;
            fuel = 0f;
            rang = 1;
            tuning = "n/A";
            garage = "n/A";
            sync = "0,0,0,0,0,0,0";
            color = "0,0,-1,-1";
            products = 0;
            vlock = 0;
            doors = "[false,false,false,false,false,false]";
            windows = "[false,false,false,false]";
            towed = 0;
        }
    }
}
