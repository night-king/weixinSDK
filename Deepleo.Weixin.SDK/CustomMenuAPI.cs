/*--------------------------------------------------------------------------
* CustomMenu.cs
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
    /// 对应微信API的 "自定义菜单”
    /// 注意：自定义菜单事件推送接口见：AcceptMessageAPI
    /// </summary>
    public class CustomMenuAPI
    {
        /// <summary>
        /// 自定义菜单创建接口
        /// </summary>
        /// <param name="token"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static bool Create(string token, string content)
        {
            var client = new HttpClient();
            var result = client.PostAsync(string.Format("https://api.weixin.qq.com/cgi-bin/menu/create?access_token={0}", token), new StringContent(content)).Result;
            return DynamicJson.Parse(result.Content.ReadAsStringAsync().Result).errcode == 0;
        }

        /// <summary>
        /// 自定义菜单查询接口
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static dynamic Query(string token)
        {
            var client = new HttpClient();
            var result = client.GetAsync(string.Format("https://api.weixin.qq.com/cgi-bin/menu/get?access_token={0}", token)).Result;
            if (!result.IsSuccessStatusCode) return null;
            return DynamicJson.Parse(result.Content.ReadAsStringAsync().Result);
        }

        /// <summary>
        /// 自定义菜单删除接口
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static bool Delete(string token)
        {
            var client = new HttpClient();
            var result = client.GetAsync(string.Format("https://api.weixin.qq.com/cgi-bin/menu/delete?access_token={0}", token)).Result;
            if (!result.IsSuccessStatusCode) return false;
            return DynamicJson.Parse(result.Content.ReadAsStringAsync().Result).errmsg == "ok";
        }

    }
}
