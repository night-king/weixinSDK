/*--------------------------------------------------------------------------
* AcceptMessageAPI.cs
 *Auth:deepleo
* Date:2013.12.31
* Email:2586662969@qq.com
 * Website:http://www.weixinsdk.net
*--------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Dynamic;
using System.Xml.Linq;
using System.Xml;
using Codeplex.Data;
using Tencent;

namespace Deepleo.Weixin.SDK
{
    /// <summary>
    ///  对应微信API的 "接收消息"
    /// </summary>
    public class AcceptMessageAPI
    {
        /// <summary>
        /// 解析微信服务器推送的消息
        /// http://mp.weixin.qq.com/wiki/index.php?title=%E6%8E%A5%E6%94%B6%E6%99%AE%E9%80%9A%E6%B6%88%E6%81%AF
        /// http://mp.weixin.qq.com/wiki/index.php?title=%E6%8E%A5%E6%94%B6%E4%BA%8B%E4%BB%B6%E6%8E%A8%E9%80%81
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static WeixinMessage Parse(string message)
        {
            var msg = new WeixinMessage();
            msg.Body = new DynamicXml(message);
            string msgType = msg.Body.MsgType.Value;
            switch (msgType)
            {
                case "text":
                    msg.Type = WeixinMessageType.Text;
                    break;
                case "image":
                    msg.Type = WeixinMessageType.Image;
                    break;
                case "voice":
                    msg.Type = WeixinMessageType.Voice;
                    break;
                case "video":
                    msg.Type = WeixinMessageType.Video;
                    break;
                case "location":
                    msg.Type = WeixinMessageType.Location;
                    break;
                case "link":
                    msg.Type = WeixinMessageType.Link;
                    break;
                case "event":
                    msg.Type = WeixinMessageType.Event;
                    break;
                default: throw new Exception("does not support this message type:" + msgType);
            }
            return msg;
        }

    }
}
