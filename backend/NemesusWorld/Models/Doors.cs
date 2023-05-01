using System;
using System.Collections.Generic;
using System.Text;

namespace NemesusWorld.Models
{
    [PetaPoco.TableName("doors")]
    [PetaPoco.PrimaryKey("id")]
    class Doors
    {
        public int id { get; set; }
        public string hash { get; set; }
        public float posx { get; set; }
        public float posy { get; set; }
        public float posz { get; set; }
        public int dimension { get; set; }
        public bool toogle { get; set; }
        public string props { get; set; }
        public bool save { get; set; }

        public Doors()
        {
            id = 0;
            hash = "n/A";
            posx = 0.0f;
            posy = 0.0f;
            posz = 0.0f;
            dimension = 0;
            toogle = false;
            props = "";
            save = false;
        }
    }
}
