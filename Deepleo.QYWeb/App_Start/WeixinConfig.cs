using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Deepleo.Weixin.QYSDK.Helpers;

namespace Deepleo.QYWeb
{
    public class WeixinConfig
    {
        public static string CorpID { private set; get; }
        public static string CorpSecret { private set; get; }
        public static string Token { private set; get; }
        public static string EncodingAESKey { private set; get; }
        public static TokenHelper TokenHelper { private set; get; }

        public static void Register()
        {
            CorpID = System.Configuration.ConfigurationManager.AppSettings["CorpID"];
            CorpSecret = System.Configuration.ConfigurationManager.AppSettings["CorpSecret"];
            Token = System.Configuration.ConfigurationManager.AppSettings["Token"];
            EncodingAESKey = System.Configuration.ConfigurationManager.AppSettings["EncodingAESKey"];
            var openJSSDK = int.Parse(System.Configuration.ConfigurationManager.AppSettings["OpenJSSDK"]) > 0;
            TokenHelper = new TokenHelper(6000, CorpID, CorpSecret, openJSSDK);
            TokenHelper.Run();
        }
    }
}