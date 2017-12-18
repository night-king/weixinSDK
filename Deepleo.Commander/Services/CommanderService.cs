using Codeplex.Data;
using Deepleo.Weixin.SDK;
using Deepleo.Weixin.SDK.JSSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Deepleo.Commander.Services
{
    [ServiceContract]
    public interface ICommanderService
    {
        /// <summary>
        /// 获取微信公众号、小程序access_token
        /// </summary>
        /// <param name="id">微信公众号、小程序id</param>
        /// <param name="force"></param>
        /// <returns></returns>
        [OperationContract]
        string GetAccessToken(string appid, string appSecret, bool force);

        /// <summary>
        /// 获取微信公众号jsticket
        /// </summary>
        /// <param name="appid"></param>
        /// <param name="appSecret"></param>
        /// <param name="force"></param>
        /// <returns></returns>
        [OperationContract]
        string GetJsTicket(string appid, string appSecret, bool force);

    }

    public class CommanderService : ICommanderService
    {
        /// <summary>
        ///access_token expire time 
        /// </summary>
        public const int ACCESS_TOKEN_EXPIRE_SECONDS = 7000;

        /// <summary>
        /// cache 
        /// </summary>
        private static ObjectCache cache = MemoryCache.Default;

        class weixin_token
        {
            public string access_token { set; get; }
            public string jssdk_ticket { set; get; }

        }
        public string GetAccessToken(string appid, string appSecret, bool force)
        {
            try
            {
                var access_token = "";
                var jssdk_ticket = "";
                if (force && cache.Contains(appid))
                {
                    cache.Remove(appid);
                }
                if (!cache.Contains(appid))
                {
                    access_token = BasicAPI.GetAccessToken(appid, appSecret).access_token;
                    var js = JSAPI.GetTickect(access_token);
                    jssdk_ticket = js.ticket;
                    var json = DynamicJson.Serialize(new weixin_token { access_token = access_token, jssdk_ticket = jssdk_ticket });
                    var policy = new CacheItemPolicy() { AbsoluteExpiration = DateTime.Now.AddSeconds(ACCESS_TOKEN_EXPIRE_SECONDS) };
                    cache.Set(appid, json, policy);
                }
                else
                {
                    var weixin_token = DynamicJson.Parse(cache.Get(appid).ToString());
                    access_token = weixin_token.access_token;
                    jssdk_ticket = weixin_token.jssdk_ticket;
                }
                AppLogManager.Write(string.Format("appid:{0}, access_token: {1}, jssdk_ticket: {2}", appid, access_token, jssdk_ticket));
                return access_token;
            }
            catch (Exception ex)
            {
                LogWriter.Default.WriteError(ex);
                return string.Empty;
            }
        }

        public string GetJsTicket(string appid, string appSecret, bool force)
        {
            try
            {
                var access_token = "";
                var jssdk_ticket = "";
                if (force && cache.Contains(appid))
                {
                    cache.Remove(appid);
                }
                if (!cache.Contains(appid))
                {
                    access_token = BasicAPI.GetAccessToken(appid, appSecret).access_token;
                    var js = JSAPI.GetTickect(access_token);
                    jssdk_ticket = js.ticket;
                    var json = DynamicJson.Serialize(new weixin_token { access_token = access_token, jssdk_ticket = jssdk_ticket });
                    var policy = new CacheItemPolicy() { AbsoluteExpiration = DateTime.Now.AddSeconds(ACCESS_TOKEN_EXPIRE_SECONDS) };
                    cache.Set(appid, json, policy);
                }
                else
                {
                    var weixin_token = DynamicJson.Parse(cache.Get(appid).ToString());
                    access_token = weixin_token.access_token;
                    jssdk_ticket = weixin_token.jssdk_ticket;
                }
                AppLogManager.Write(string.Format("appid:{0}, access_token: {1}, jssdk_ticket: {2}", appid, access_token, jssdk_ticket));
                return jssdk_ticket;
            }
            catch (Exception ex)
            {
                LogWriter.Default.WriteError(ex);
                return string.Empty;
            }
        }
      
    }
}
