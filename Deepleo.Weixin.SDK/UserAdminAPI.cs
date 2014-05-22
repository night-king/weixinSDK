/*--------------------------------------------------------------------------
* UserAdminAPI.cs
 *Auth:deepleo
* Date:2013.12.31
* Email:2586662969@qq.com
*--------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using Codeplex.Data;

namespace Deepleo.Weixin.SDK
{
    /// <summary>
    ///  对应微信API的 "用户管理"
    ///  注意：
    ///  1.以下API未实现 ：
    ///    "分组管理接口"，“网页授权获取用户基本信息”
    ///  2.获取用户"地理位置"API 见AcceptMessageAPI实现，
    ///     “地理位置”获取方式有两种：一种是仅在进入会话时上报一次，一种是进入会话后每隔5秒上报一次，公众号可以在公众平台网站中设置。
    /// </summary>
    public class UserAdminAPI
    {
        /// <summary>
        /// 获取用户基本信息
        /// </summary>
        /// <param name="token"></param>
        /// <param name="openId"></param>
        /// <returns></returns>
        public static dynamic GetInfo(string token, string openId)
        {
            var client = new HttpClient();
            var result = client.GetAsync(string.Format("https://api.weixin.qq.com/cgi-bin/user/info?access_token={0}&openid={1}", token, openId)).Result;
            if (!result.IsSuccessStatusCode) return null;
            return DynamicJson.Parse(result.Content.ReadAsStringAsync().Result);
        }

        /// <summary>
        /// 获取订阅者信息
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static dynamic GetSubscribes(string token)
        {
            var client = new HttpClient();
            var result = client.GetAsync(string.Format("https://api.weixin.qq.com/cgi-bin/user/get?access_token={0}", token)).Result;
            if (!result.IsSuccessStatusCode) return null;
            return DynamicJson.Parse(result.Content.ReadAsStringAsync().Result);
        }

        /// <summary>
        /// 获取订阅者信息
        /// </summary>
        /// <param name="token"></param>
        /// <param name="nextOpenId"></param>
        /// <returns></returns>
        public static dynamic GetSubscribes(string token, string nextOpenId)
        {
            var client = new HttpClient();
            var result = client.GetAsync(string.Format("https://api.weixin.qq.com/cgi-bin/user/get?access_token={0}&next_openid={1}", token, nextOpenId)).Result;
            if (!result.IsSuccessStatusCode) return null;
            return DynamicJson.Parse(result.Content.ReadAsStringAsync().Result);
        }
    }
}
