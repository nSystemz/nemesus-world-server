using System;
namespace NemesusWorld.Models
{
    public class GangzoneProps
    {
        public int groupid { get; set; }
        public string groupname { get; set; }
        public int respect { get; set; }

        public GangzoneProps()
        {
            groupid = -1;
            groupname = "";
            respect = 0;
        }
    }
}

