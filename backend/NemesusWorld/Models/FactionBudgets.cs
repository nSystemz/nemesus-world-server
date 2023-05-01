using System;
using System.Collections.Generic;
using System.Text;

namespace NemesusWorld.Models
{
    class FactionBudgets
    {
        public int id { get; set; }
        public int faction { get; set; }
        public int budget { get; set; }

        public FactionBudgets()
        {
            id = 0;
            faction = 0;
            budget = 100000;
        }
    }
}
