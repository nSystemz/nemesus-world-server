using System;
using System.Collections.Generic;
using System.Text;

namespace NemesusWorld.Models
{
    class FactionMember
    {
        public int charid { get; set; }
        public String name { get; set; }
        public int membersince { get; set; }
        public int dutytime { get; set; }
        public int rang { get; set; }
        public int online { get; set; }
        public int swat { get; set; }

        public FactionMember()
        {
            charid = -1;
            name = "";
            membersince = 1609462861;
            dutytime = 0;
            rang = 0;
            online = 0;
            swat = 0;
        }
    }
}
