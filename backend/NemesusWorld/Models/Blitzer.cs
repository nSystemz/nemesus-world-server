using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace NemesusWorld.Models
{
    [PetaPoco.TableName("speedcameras")]
    [PetaPoco.PrimaryKey("id")]
    class Blitzer
    {
        [PetaPoco.Column]
        public int id { get; set; }
        [PetaPoco.Column]
        public string who { get; set; }
        [PetaPoco.Column]
        public string from { get; set; }
        [PetaPoco.Column]
        public int maxspeed { get; set; }
        [PetaPoco.Column]
        public float heading { get; set; }
        [PetaPoco.Column]
        public string position { get; set; }
        [PetaPoco.Ignore]
        public GTANetworkAPI.Object speedobject { get; set; }
        [PetaPoco.Ignore]
        public GTANetworkAPI.ColShape colshape { get; set; }

        public Blitzer()
        {
            id = 0;
            who = "";
            from = "";
            maxspeed = 100;
            heading = 0;
            position = "";
            speedobject = null;
            colshape = null;
        }
    }
}
