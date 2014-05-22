/*--------------------------------------------------------------------------
* SendMessageAPI.cs
 *Auth:deepleo
* Date:2013.12.31
* Email:2586662969@qq.com
*--------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Codeplex.Data;
using System.Net;
using System.Net.Http;

namespace Deepleo.Weixin.SDK
{
    /// <summary>
    /// 对应微信API的 "发送消息”
    /// </summary>
    public class SendMessageAPI
    {
        /// <summary>
        /// 被动回复消息
        /// </summary>
        /// <param name="message">微信服务器推送的消息</param>
        /// <param name="executor">用户自定义的消息执行者</param>
        /// <returns></returns>
        public static string Relay(WeixinMessage message, IWeixinExecutor executor)
        {
            return executor.Execute(message);
        }

        /// <summary>
        /// 主动发送客服消息
        /// http://mp.weixin.qq.com/wiki/index.php?title=%E5%8F%91%E9%80%81%E5%AE%A2%E6%9C%8D%E6%B6%88%E6%81%AF
        /// 当用户主动发消息给公众号的时候
        /// 开发者在一段时间内（目前为24小时）可以调用客服消息接口，在24小时内不限制发送次数。
        /// </summary>
        /// <param name="token"></param>
        /// <param name="msg">json格式的消息，具体格式请参考微信官方API</param>
        /// <returns></returns>
        public static bool Send(string token, string msg)
        {
            var client = new HttpClient();
            var task = client.PostAsync(string.Format("https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token={0}", token), new StringContent(msg)).Result;
            return task.IsSuccessStatusCode;
        }
    }
}
