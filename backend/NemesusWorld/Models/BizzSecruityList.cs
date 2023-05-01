using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace NemesusWorld.Models
{
    class BizzSecruityList
    {
        public int id { get; set; }
        public string name { get; set; }
        public int money { get; set; }
        public int bizz { get; set; }
        public bool isbizz { get; set; }

        public BizzSecruityList()
        {
            id = 0;
            name = "";
            money = 0;
            bizz = 0;
            isbizz = false;
        }
    }
}
