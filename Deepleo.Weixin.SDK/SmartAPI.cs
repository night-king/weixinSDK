/*--------------------------------------------------------------------------
* SmartAPI.cs
 *Auth:deepleo
* Date:2015.01.15
* Email:2586662969@qq.com
 * Website:http://www.weixinsdk.net
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
    /// 智能接口
    /// http://mp.weixin.qq.com/wiki/index.php?title=%E8%AF%AD%E4%B9%89%E7%90%86%E8%A7%A3
    /// </summary>
    public class SmartAPI
    {
        /// <summary>
        /// 语义理解
        /// </summary>
        /// <param name="access_token">根据appid和appsecret获取到的token</param>
        /// <param name="query">输入文本串</param>
        /// <param name="category">需要使用的服务类型，多个用“，”隔开，不能为空</param>
        /// <param name="latitude">纬度坐标，与经度同时传入；与城市二选一传入</param>
        /// <param name="longitude">经度坐标，与纬度同时传入；与城市二选一传入</param>
        /// <param name="city">城市名称，与经纬度二选一传入</param>
        /// <param name="region">区域名称，在城市存在的情况下可省；与经纬度二选一传入</param>
        /// <param name="appid">公众号唯一标识，用于区分公众号开发者</param>
        /// <param name="uid">用户唯一id（非开发者id），用户区分公众号下的不同用户（建议填入用户openid），如果为空，则无法使用上下文理解功能。appid和uid同时存在的情况下，才可以使用上下文理解功能。</param>
        /// <returns></returns>
        public static dynamic Semantic(string access_token, string query, string category, string latitude, string longitude, string city, string region, string appid, string uid)
        {
            var builder = new StringBuilder();
            builder
                .Append("{")
                .Append('"' + "query" + '"' + ":").Append(query).Append(",")
                .Append('"' + "category" + '"' + ":").Append(category).Append(",")
                .Append('"' + "latitude" + '"' + ":").Append(latitude).Append(",")
                .Append('"' + "longitude" + '"' + ":").Append(longitude).Append(",")
                .Append('"' + "city" + '"' + ":").Append(city).Append(",")
                .Append('"' + "region" + '"' + ":").Append(region).Append(",")
                .Append('"' + "appid" + '"' + ":").Append(appid).Append(",")
                .Append('"' + "uid" + '"' + ":").Append(uid).Append(",")
                .Append("}");
            var client = new HttpClient();
            var result = client.PostAsync(string.Format("https://api.weixin.qq.com/semantic/semproxy/search?access_token={0}", access_token), new StringContent(builder.ToString())).Result;
            return DynamicJson.Parse(result.Content.ReadAsStringAsync().Result);
        }
    }
}
