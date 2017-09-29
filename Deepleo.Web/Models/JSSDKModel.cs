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

        public string userAgent { set; get; }
        public string userVersion { set; get; }

        public string body { set; get; }
        public string detail { set; get; }
        public string attach { set; get; }
        public decimal price { set; get; }
        public int total_fee { set; get; }
        public string product_id { set; get; }
        public string goods_tag { set; get; }
        public string openid { set; get; }

        public string prepay_id { set; get; }
        public string trade_type { set; get; }
        public string sign { set; get; }
        public string return_code { set; get; }
        public string return_msg { set; get; }

        public string err_code { set; get; }
        public string err_code_des { set; get; }
        /// <summary>
        /// 支付成功后的跳转页面
        /// </summary>
        public string success_redict_url { set; get; }

    }
}