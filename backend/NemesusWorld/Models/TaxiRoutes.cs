using System;
using System.Collections.Generic;
using System.Text;

namespace NemesusWorld.Models
{
    [PetaPoco.TableName("taxiroutes")]
    [PetaPoco.PrimaryKey("id")]
    class TaxiRoutes
    {
        public int id { get; set; }
        public string routes { get; set; }

        public TaxiRoutes()
        {
            id = 0;
            routes = "";
        }
    }
}
