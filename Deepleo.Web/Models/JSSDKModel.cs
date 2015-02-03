using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Deepleo.Web.Models
{
    public class JSSDKModel
    {
        public string appId { set; get; }
        public long timestamp { set; get; }
        public string nonceStr { set; get; }
        public string jsapiTicket { set; get; }
        public string signature { set; get; }
        public string shareUrl { set; get; } 
        public string shareImg { set; get; }
        public string string1 { set; get; }
    }
    public class JSPayModel
    {
        public string appId { set; get; }
        public long timestamp { set; get; }
        public string nonceStr { set; get; }
        public string jsapiTicket { set; get; }
        public string signature { set; get; }
        public string string1 { set; get; }

        public string userAgent { set; get; }
        public string userVersion { set; get; }

    }
}