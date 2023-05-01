using System;
using System.Collections.Generic;
using System.Text;

namespace NemesusWorld.Models
{
    class TicketAnswers
    {
        public int id { get; set; }
        public int ticketid { get; set; }
        public string user { get; set; }
        public string text { get; set; }
        public int timestamp { get; set; }

        public TicketAnswers()
        {
            id = -1;
            user = "";
            text = "";
            timestamp = 1609462861;
        }
    }
}
