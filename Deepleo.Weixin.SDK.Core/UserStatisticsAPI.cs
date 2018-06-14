/*--------------------------------------------------------------------------
* UserStatisticsAPI.cs
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
    /// 数据统计接口=>用户分析数据接口
    /// </summary>
    public class UserStatisticsAPI
    {
        /// <summary>
        /// 获取用户增减数据
        /// 最大时间跨度：7
        /// begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="begin_date">获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错</param>
        /// <param name="end_date">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        /// <returns></returns>
        public static dynamic GetUserSummary(string access_token, DateTime begin_date, DateTime end_date)
        {
            var url = string.Format("https://api.weixin.qq.com/datacube/getusersummary?access_token={0}", access_token);
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
        /// 获取累计用户数据
        /// 最大时间跨度：7
        /// begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="begin_date">获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错</param>
        /// <param name="end_date">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        /// <returns></returns>
        public static dynamic GetUserCumulate(string access_token, DateTime begin_date, DateTime end_date)
        {
            var url = string.Format("https://api.weixin.qq.com/datacube/getusersummary?access_token={0}", access_token);
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
        /// 解释用户的渠道user_source
        /// </summary>
        /// <param name="user_source"></param>
        /// <returns></returns>
        public static string ExplainUserSource(int user_source)
        {
            switch (user_source)
            {
                case 0:
                    return "其他";
                case 30:
                    return "扫二维码";
                case 17:
                    return "名片分享";
                case 35:
                    return "搜号码（微信添加朋友页的搜索）";
                case 39:
                    return "查询微信公众帐号 ";
                case 43:
                    return "图文页右上角菜单";
                default:
                    return "";
            }
        }
    }
}
