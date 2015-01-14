/*--------------------------------------------------------------------------
* MessageStatisticsAPI.cs
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
    /// 数据统计接口=>消息分析数据接口
    /// </summary>
    public class MessageStatisticsAPI
    {
        /// <summary>
        /// 获取消息发送概况数据
        /// 最大时间跨度：7
        /// begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="begin_date">获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错</param>
        /// <param name="end_date">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        /// <returns></returns>
        public static dynamic GetUpStreamMsg(string access_token, DateTime begin_date, DateTime end_date)
        {
            var url = string.Format("https://api.weixin.qq.com/datacube/getupstreammsg?access_token={0}", access_token);
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
        /// 获取消息分送分时数据
        /// 最大时间跨度：1
        /// begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="begin_date">获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错</param>
        /// <param name="end_date">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        /// <returns></returns>
        public static dynamic GetUpStreamMsgHour(string access_token, DateTime begin_date, DateTime end_date)
        {
            var url = string.Format("https://api.weixin.qq.com/datacube/getupstreammsghour?access_token={0}", access_token);
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
        /// 获取消息发送周数据
        /// 最大时间跨度：30
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="begin_date">获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错</param>
        /// <param name="end_date">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        /// <returns></returns>
        public static dynamic GetUpStreamMsgWeek(string access_token, DateTime begin_date, DateTime end_date)
        {
            var url = string.Format("https://api.weixin.qq.com/datacube/getupstreammsgweek?access_token={0}", access_token);
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
        /// 获取消息发送月数据
        /// 最大时间跨度：30
        /// begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="begin_date">获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错</param>
        /// <param name="end_date">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        /// <returns></returns>
        public static dynamic GetUpStreamMsgMonth(string access_token, DateTime begin_date, DateTime end_date)
        {
            var url = string.Format("https://api.weixin.qq.com/datacube/getupstreammsgmonth?access_token={0}", access_token);
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
        /// 获取消息发送分布数据
        /// 最大时间跨度：15
        /// begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="begin_date">获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错</param>
        /// <param name="end_date">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        /// <returns></returns>
        public static dynamic GetUpStreamMsgDist(string access_token, DateTime begin_date, DateTime end_date)
        {
            var url = string.Format("https://api.weixin.qq.com/datacube/getupstreammsgdist?access_token={0}", access_token);
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
        /// 获取消息发送分布周数据
        /// 最大时间跨度：30
        /// begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="begin_date">获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错</param>
        /// <param name="end_date">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        /// <returns></returns>
        public static dynamic GetUpStreamMsgDistWeek(string access_token, DateTime begin_date, DateTime end_date)
        {
            var url = string.Format("https://api.weixin.qq.com/datacube/getupstreammsgdistweek?access_token={0}", access_token);
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
        /// 获取消息发送分布月数据
        /// 最大时间跨度：30
        /// begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="begin_date">获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错</param>
        /// <param name="end_date">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        /// <returns></returns>
        public static dynamic GetUpStreamMsgDistMonth(string access_token, DateTime begin_date, DateTime end_date)
        {
            var url = string.Format("https://api.weixin.qq.com/datacube/getupstreammsgdistmonth?access_token={0}", access_token);
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
        /// 解释消息类型msg_type
        /// </summary>
        /// <param name="user_source"></param>
        /// <returns></returns>
        public static string ExplainMsgType(int msg_type)
        {
            switch (msg_type)
            {
                case 1:
                    return "文字";
                case 2:
                    return "图片";
                case 3:
                    return "语音";
                case 4:
                    return "视频";
                case 6:
                    return "第三方应用消息（链接消息）";
                default:
                    return "";
            }
        }


        /// <summary>
        /// 解释消息类型msg_type
        /// </summary>
        /// <param name="user_source"></param>
        /// <returns></returns>
        public static string ExplainCountInterval(int count_interval)
        {
            switch (count_interval)
            {
                case 0:
                    return "0";
                case 1:
                    return "1-5";
                case 2:
                    return "6-10";
                case 3:
                    return "10次以上";
                default:
                    return "";
            }
        }
    }
}
