using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Text;

namespace NemesusWorld.Utils
{
    class HTTP
    {
        public static byte[] Post(string uri, NameValueCollection pairs)
        {
            using (WebClient webclient = new WebClient())
            {
                return webclient.UploadValues(uri, pairs);
            }
        }
    }
}
