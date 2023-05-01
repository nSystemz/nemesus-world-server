using System;
using System.Collections.Generic;
using System.Text;

namespace NemesusWorld.Models
{
    [PetaPoco.TableName("invoices")]
    [PetaPoco.PrimaryKey("id")]
    class Invoices
    {
        public int id { get; set; }
        public int value { get; set; }
        public string empfänger { get; set; }
        public string modus { get; set; }
        public string text { get; set; }
        public string banknumber { get; set; }
        public int timestamp { get; set; }

        public Invoices()
        {
            id = 0;
            value = 0;
            empfänger = "n/A";
            modus = "Privatrechnung";
            text = "n/A";
            banknumber = "n/A";
            timestamp = 1609462861;
        }
    }
}
