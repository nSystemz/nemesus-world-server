using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace NemesusWorld.Models
{
    [PetaPoco.TableName("cctvs")]
    [PetaPoco.PrimaryKey("id")]
    class CCTV
    {
        [PetaPoco.Column]
        public int id { get; set; }
        [PetaPoco.Column]
        public string who { get; set; }
        [PetaPoco.Column]
        public string from { get; set; }
        [PetaPoco.Column]
        public string position { get; set; }
        [PetaPoco.Column]
        public float heading { get; set; }
        [PetaPoco.Ignore]
        public GTANetworkAPI.Object cctvobject { get; set; }

        public CCTV()
        {
            id = 0;
            who = "";
            from = "";
            position = "";
            heading = 0.0f;
            cctvobject = null;
        }
    }
}