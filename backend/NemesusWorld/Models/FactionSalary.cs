using System;
using System.Collections.Generic;
using System.Text;

namespace NemesusWorld.Models
{
    [PetaPoco.TableName("factionsalary")]
    [PetaPoco.PrimaryKey("id")]
    class FactionSalary
    {
        public int id { get; set; }
        public int factionid { get; set; }
        public int rang1 { get; set; }
        public int rang2 { get; set; }
        public int rang3 { get; set; }
        public int rang4 { get; set; }
        public int rang5 { get; set; }
        public int rang6 { get; set; }
        public int rang7 { get; set; }
        public int rang8 { get; set; }
        public int rang9 { get; set; }
        public int rang10 { get; set; }
        public int rang11 { get; set; }
        public int rang12 { get; set; }


        public FactionSalary()
        {
            id = 0;
            factionid = 0;
            rang1 = 0;
            rang2 = 0;
            rang3 = 0;
            rang4 = 0;
            rang5 = 0;
            rang6 = 0;
            rang7 = 0;
            rang8 = 0;
            rang9 = 0;
            rang10 = 0;
            rang11 = 0;
            rang12 = 0;
        }
    }
}
