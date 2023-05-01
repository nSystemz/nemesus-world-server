using System;
namespace NemesusWorld.Models
{
    [PetaPoco.TableName("paydays")]
    [PetaPoco.PrimaryKey("id")]
    public class PaydayList
    {
        public int id { get; set; }
        public int characterid { get; set; }
        public string text { get; set; }
        public int timestamp { get; set; }
        public int total { get; set; }

        public PaydayList()
        {
            id = 0;
            characterid = 0;
            text = "";
            timestamp = 0;
            total = 0;
        }
    }
}

