using System;
using System.Collections.Generic;
using System.Text;

namespace NemesusWorld.Models
{
    [PetaPoco.TableName("fahndungskommentare")]
    [PetaPoco.PrimaryKey("id")]
    class FahndungsKommentare
    {
        public int id { get; set; }
        public int fahndungsid { get; set; }
        public string text { get; set; }
        public string creator { get; set; }
        public int timestamp { get; set; }

        public FahndungsKommentare()
        {
            id = 0;
            fahndungsid = 0;
            text = "";
            creator = "n/A";
            timestamp = 1609462861;
        }
    }
}
