using System;
using System.Collections.Generic;
using System.Text;

namespace NemesusWorld.Models
{
    class SmartphoneProps
    {
        public string phonenumber { get; set; }
        public int wallpaper { get; set; }
        public bool shownumber { get; set; }
        public int phonestatus { get; set; }
        public int notification { get; set; }
        public int ringtone { get; set; }
        public int messagecount { get; set; }
        public int phonecalls { get; set; }
        public bool notificationmessages { get; set; }

        public SmartphoneProps()
        {
            phonenumber = "";
            wallpaper = 2;
            shownumber = false;
            phonestatus = 0;
            notification = 1;
            ringtone = 1;
            messagecount = 0;
            phonecalls = 0;
            notificationmessages = true;
        }
    }
}
