using System;
using System.Collections.Generic;
using System.Text;

namespace NemesusWorld.Models
{
    [PetaPoco.TableName("smartphones")]
    [PetaPoco.PrimaryKey("id")]
    [PetaPoco.ExplicitColumns]
    class Smartphone 
    {
        [PetaPoco.Column]
        public int id { get; set; }
        [PetaPoco.Column]
        public string phonenumber { get; set; }
        [PetaPoco.Column]
        public string phoneprops { get; set; }
        [PetaPoco.Column]
        public string contacts { get; set; }
        [PetaPoco.Column]
        public int akku { get; set; }
        [PetaPoco.Column]
        public int prepaid { get; set; }
        [PetaPoco.Column]
        public string owner { get; set; }

    public Smartphone()
        {
            id = 0;
            phonenumber = "n/A";
            phoneprops = "n/A";
            contacts = "";
            akku = 48;
            prepaid = 55;
            owner = "n/A";
        }
    }
}
