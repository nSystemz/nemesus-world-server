using System;
using System.Collections.Generic;
using System.Text;

namespace NemesusWorld.Models
{
    [PetaPoco.TableName("groups_members")]
    [PetaPoco.PrimaryKey("id")]
    class GroupsMembers
    {
        public int id { get; set; }
        public int groupsid { get; set; }
        public int charid { get; set; }
        public int rang { get; set; }
        public int duty_time { get; set; }
        public int payday { get; set; }
        public int payday_day { get; set; }
        public int payday_since { get; set; }
        public int since { get; set; }
        [PetaPoco.Ignore]
        public string name { get; set; }
        [PetaPoco.Ignore]
        public int online { get; set; }

        public GroupsMembers()
        {
            id = 0;
            groupsid = 0;
            charid = 0;
            rang = 1;
            duty_time = 0;
            payday = 0;
            payday_day = 0;
            payday_since = 0;
            since = 1609462861;
            name = "n/A";
            online = 0;
        }
    }
}
