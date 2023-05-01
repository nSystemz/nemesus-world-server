using System;
using System.Collections.Generic;
using System.Text;

namespace NemesusWorld.Models
{
    [PetaPoco.TableName("services")]
    [PetaPoco.PrimaryKey("id")]
    class Services
    {
        [PetaPoco.Column]
        public int id { get; set; }
        [PetaPoco.Column]
        public int groupid { get; set; }
        [PetaPoco.Column]
        public string text { get; set; }
        [PetaPoco.ResultColumn]
        public string name { get; set; }
        [PetaPoco.ResultColumn]
        public string number { get; set; }

        public Services()
        {
            id = 0;
            groupid = -1;
            text = "";
            name = "";
            number = "";
        }
    }
}
