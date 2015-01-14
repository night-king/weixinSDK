/*--------------------------------------------------------------------------
* NewsStatisticsAPI.cs
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
    /// 数据统计接口=>图文分析数据接口
    /// </summary>
    public class NewsStatisticsAPI
    {
        /// <summary>
        /// 获取图文群发每日数据
        /// 最大时间跨度：1
        /// begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="begin_date">获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错</param>
        /// <param name="end_date">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        /// <returns></returns>
        public static dynamic GetArtcleSummary(string access_token, DateTime begin_date, DateTime end_date)
        {
            var url = string.Format("https://api.weixin.qq.com/datacube/getarticlesummary?access_token={0}", access_token);
            var builder = new StringBuilder();
            builder
                .Append("{")
                .Append('"' + "begin_date" + '"' + ":").Append(begin_date.ToString("yyyy-MM-dd")).Append(",")
                .Append('"' + "end_date" + '"' + ":").Append(end_date.ToString("yyyy-MM-dd"))
                .Append("}");
            var client = new HttpClient();
            var result = client.PostAsync(url, new StringContent(builder.ToString())).Result;
            return DynamicJson.Parse(result.Content.ReadAsStringAsync().Result);
        }
        /// <summary>
        /// 获取图文群发总数据
        /// 最大时间跨度：1
        /// begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="begin_date">获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错</param>
        /// <param name="end_date">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        /// <returns></returns>
        public static dynamic GetArtcleTotal(string access_token, DateTime begin_date, DateTime end_date)
        {
            var url = string.Format("https://api.weixin.qq.com/datacube/getarticletotal?access_token={0}", access_token);
            var builder = new StringBuilder();
            builder
                .Append("{")
                .Append('"' + "begin_date" + '"' + ":").Append(begin_date.ToString("yyyy-MM-dd")).Append(",")
                .Append('"' + "end_date" + '"' + ":").Append(end_date.ToString("yyyy-MM-dd"))
                .Append("}");
            var client = new HttpClient();
            var result = client.PostAsync(url, new StringContent(builder.ToString())).Result;
            return DynamicJson.Parse(result.Content.ReadAsStringAsync().Result);
        }
        /// <summary>
        /// 获取图文统计数据
        /// 最大时间跨度：3
        /// begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="begin_date">获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错</param>
        /// <param name="end_date">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        /// <returns></returns>
        public static dynamic GetUserRead(string access_token, DateTime begin_date, DateTime end_date)
        {
            var url = string.Format("https://api.weixin.qq.com/datacube/getuserread?access_token={0}", access_token);
            var builder = new StringBuilder();
            builder
                .Append("{")
                .Append('"' + "begin_date" + '"' + ":").Append(begin_date.ToString("yyyy-MM-dd")).Append(",")
                .Append('"' + "end_date" + '"' + ":").Append(end_date.ToString("yyyy-MM-dd"))
                .Append("}");
            var client = new HttpClient();
            var result = client.PostAsync(url, new StringContent(builder.ToString())).Result;
            return DynamicJson.Parse(result.Content.ReadAsStringAsync().Result);
        }
        /// <summary>
        /// 获取图文统计分时数据
        /// 最大时间跨度：1
        /// begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="begin_date">获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错</param>
        /// <param name="end_date">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        /// <returns></returns>
        public static dynamic GetUserReadHour(string access_token, DateTime begin_date, DateTime end_date)
        {
            var url = string.Format("https://api.weixin.qq.com/datacube/getuserreadhour?access_token={0}", access_token);
            var builder = new StringBuilder();
            builder
                .Append("{")
                .Append('"' + "begin_date" + '"' + ":").Append(begin_date.ToString("yyyy-MM-dd")).Append(",")
                .Append('"' + "end_date" + '"' + ":").Append(end_date.ToString("yyyy-MM-dd"))
                .Append("}");
            var client = new HttpClient();
            var result = client.PostAsync(url, new StringContent(builder.ToString())).Result;
            return DynamicJson.Parse(result.Content.ReadAsStringAsync().Result);
        }
        /// <summary>
        /// 获取图文分享转发数据
        /// 最大时间跨度：7
        /// begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="begin_date">获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错</param>
        /// <param name="end_date">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        /// <returns></returns>
        public static dynamic GetUserShare(string access_token, DateTime begin_date, DateTime end_date)
        {
            var url = string.Format("https://api.weixin.qq.com/datacube/getusershare?access_token={0}", access_token);
            var builder = new StringBuilder();
            builder
                .Append("{")
                .Append('"' + "begin_date" + '"' + ":").Append(begin_date.ToString("yyyy-MM-dd")).Append(",")
                .Append('"' + "end_date" + '"' + ":").Append(end_date.ToString("yyyy-MM-dd"))
                .Append("}");
            var client = new HttpClient();
            var result = client.PostAsync(url, new StringContent(builder.ToString())).Result;
            return DynamicJson.Parse(result.Content.ReadAsStringAsync().Result);
        } 
        /// <summary>
        /// 获取图文分享转发分时数据
        /// 最大时间跨度：1
        /// begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="begin_date">获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错</param>
        /// <param name="end_date">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        /// <returns></returns>
        public static dynamic GetUserShareHour(string access_token, DateTime begin_date, DateTime end_date)
        {
            var url = string.Format("https://api.weixin.qq.com/datacube/getusersharehour?access_token={0}", access_token);
            var builder = new StringBuilder();
            builder
                .Append("{")
                .Append('"' + "begin_date" + '"' + ":").Append(begin_date.ToString("yyyy-MM-dd")).Append(",")
                .Append('"' + "end_date" + '"' + ":").Append(end_date.ToString("yyyy-MM-dd"))
                .Append("}");
            var client = new HttpClient();
            var result = client.PostAsync(url, new StringContent(builder.ToString())).Result;
            return DynamicJson.Parse(result.Content.ReadAsStringAsync().Result);
        }  
        /// <summary>
        /// 解释用户分享的场景share_scene
        /// </summary>
        /// <param name="user_source"></param>
        /// <returns></returns>
        public static string ExplainShareScene(int share_scene)
        {
            switch (share_scene)
            {
                case 1:
                    return "好友转发";
                case 2:
                    return "朋友圈";
                case 3:
                    return "腾讯微博";
                case 255:
                    return "其他";
                default:
                    return "";
            }
        }

    }
}
