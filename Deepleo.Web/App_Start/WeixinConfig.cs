using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Deepleo.Weixin.SDK.Helpers;

namespace Deepleo.Web
{
    public class WeixinConfig
    {
        public static string Token { private set; get; }
        public static string EncodingAESKey { private set; get; }
        public static string AppID { private set; get; }
        public static string AppSecret { private set; get; }
        public static string PartnerKey { private set; get; }
        public static string mch_id { private set; get; }
        public static string device_info { private set; get; }
        public static string spbill_create_ip { private set; get; }
        public static void Register()
        {

            Token = System.Configuration.ConfigurationManager.AppSettings["Token"];
            EncodingAESKey = System.Configuration.ConfigurationManager.AppSettings["EncodingAESKey"];
            AppID = System.Configuration.ConfigurationManager.AppSettings["AppID"];
            AppSecret = System.Configuration.ConfigurationManager.AppSettings["AppSecret"];
            PartnerKey = System.Configuration.ConfigurationManager.AppSettings["PartnerKey"];
            mch_id = System.Configuration.ConfigurationManager.AppSettings["mch_id"];
            device_info = System.Configuration.ConfigurationManager.AppSettings["device_info"];
            spbill_create_ip = System.Configuration.ConfigurationManager.AppSettings["spbill_create_ip"];
        }
    }
}