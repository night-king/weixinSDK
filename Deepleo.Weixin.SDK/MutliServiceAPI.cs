/*--------------------------------------------------------------------------
* MutliServiceAPI.cs
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
using Deepleo.Weixin.SDK.Helpers;

namespace Deepleo.Weixin.SDK
{
    /// <summary>
    /// 多客服功能
    /// http://mp.weixin.qq.com/wiki/5/ae230189c9bd07a6b221f48619aeef35.html
    /// 开发者可根据用户发给公众号的消息内容，选择是转发给客服还是直接回复,
    /// 如果是转发给客服，调用本ＡＰＩ创建客服消息后回传给微信服务器即可
    /// PC客户端自定义插件接口无ＡＰＩ包装!!!
    /// </summary>
    public class MutliServiceAPI
    {
        /// <summary>
        /// 构建多客服消息，用于回复微信服务器提交过来的用户消息
        /// 
     
        /// </summary>
        /// <param name="toUserName">发送方帐号（一个OpenID）</param>
        /// <param name="fromUserName">开发者微信号</param>
        /// <returns></returns>
        public static string BuildTransferCustomerServiceMessage(string toUserName, string fromUserName)
        {
            return string.Format("<xml>" +
           "<ToUserName><![CDATA[{0}]]></ToUserName>" +
           "<FromUserName><![CDATA[{1}]]></FromUserName>" +
           "<CreateTime>{2}</CreateTime>" +
           "<MsgType><![CDATA[transfer_customer_service]]></MsgType>" +
           "</xml>", toUserName, Util.CreateTimestamp(), fromUserName);
        }

        /// <summary>
        /// 构建消息转发到指定客服的多客服消息，用于回复微信服务器提交过来的用户消息
        /// </summary>
        /// <param name="toUserName">发送方帐号（一个OpenID）</param>
        /// <param name="fromUserName">开发者微信号</param>
        /// <param name="kfAccount">指定会话接入的客服账号</param>
        /// <returns></returns>
        public static string BuildTransferCustomerServiceMessage(string toUserName, string fromUserName, string kfAccount)
        {
            return string.Format("<xml>" +
           "<ToUserName><![CDATA[{0}]]></ToUserName>" +
           "<FromUserName><![CDATA[{1}]]></FromUserName>" +
           "<CreateTime>{2}</CreateTime>" +
           "<MsgType><![CDATA[transfer_customer_service]]></MsgType>" +
           "<TransInfo><KfAccount><![CDATA[{3}]]></KfAccount></TransInfo>" +
           "</xml>", toUserName, Util.CreateTimestamp(), fromUserName, kfAccount);
        }

        /// <summary>
        /// 获取客服聊天记录接口
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="openid"></param>
        /// <param name="starttime"></param>
        /// <param name="endtime"></param>
        /// <param name="pagesize"></param>
        /// <param name="pageindex"></param>
        /// <returns></returns>
        public static dynamic GetChatRecord(string access_token, string openid, int starttime, int endtime, int pagesize, int pageindex)
        {
            var builder = new StringBuilder();
            builder
                .Append("{")
                .Append('"' + "starttime" + '"' + ":").Append(starttime).Append(",")
                .Append('"' + "endtime" + '"' + ":").Append(endtime).Append(",")
                .Append('"' + "openid" + '"' + ":").Append(openid).Append(",")
                .Append('"' + "pagesize" + '"' + ":").Append(pagesize).Append(",")
                .Append('"' + "pageindex" + '"' + ":").Append(pageindex)
                .Append("}");
            var client = new HttpClient();
            var result = client.PostAsync(string.Format("https://api.weixin.qq.com/cgi-bin/customservice/getrecord?access_token={0}", access_token), new StringContent(builder.ToString())).Result;
            return DynamicJson.Parse(result.Content.ReadAsStringAsync().Result);
        }
     
        /// <summary>
        /// 解释聊天记录的opercode的含义
        /// </summary>
        /// <param name="opercode"></param>
        /// <returns></returns>
        public static string ExplainOpercode(int opercode)
        {
            switch (opercode)
            {
                case 1000:
                    return "创建未接入会话";
                case 1001:
                    return "接入会话";
                case 1002:
                    return "主动发起会话";
                case 1004:
                    return "关闭会话";
                case 1005:
                    return "抢接会话";
                case 2001:
                    return "公众号收到消息";
                case 2002:
                    return "客服发送消息";
                case 2003:
                    return "客服收到消息";
                default:
                    return "";
            }
        }
    }
}
