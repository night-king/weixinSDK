/*--------------------------------------------------------------------------
* WeixinExecutor.cs
 *Auth:deepleo
* Date:2013.12.31
* Email:2586662969@qq.com
*--------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Deepleo.Weixin.SDK;
using System.Text;
using System.Text.RegularExpressions;
using Deepleo.Weixin.SDK.Helpers;

namespace Deepleo.Web
{
    public class WeixinExecutor : IWeixinExecutor
    {
        public WeixinExecutor()
        {
        }

        public string Execute(WeixinMessage message)
        {
            var result = "";
            string openId = message.Body.FromUserName.Value;
            var myUserName = message.Body.ToUserName.Value;
            //这里需要调用TokenHelper获取Token的，省略了。
            switch (message.Type)
            {
                case WeixinMessageType.Text:
                    string userMessage = message.Body.Content.Value;
                    result = RepayText(openId, myUserName, "欢迎使用");
                    break;
                case WeixinMessageType.Event:
                    string eventType = message.Body.Event.Value.ToLower();
                    string eventKey = message.Body.EventKey.Value;
                    switch (eventType)
                    {
                        case "subscribe":
                            result = RepayText(openId, myUserName, "欢迎订阅");
                            break;
                        case "unsubscribe":
                            result = RepayText(openId, myUserName, "欢迎再来");
                            break;
                        case "scan":
                            result = RepayText(openId, myUserName, "欢迎使用");
                            break;
                        case "location"://用户进入应用时记录用户地理位置
                            #region location
                            var lat = message.Body.Latitude.Value.ToString();
                            var lng = message.Body.Longitude.Value.ToString();
                            var pcn = message.Body.Precision.Value.ToString();
                            //在此处将经纬度记录在数据库
                            #endregion
                            break;
                        case "click":
                            switch (eventKey)//refer to： Recources/menu.json
                            {
                                case "myaccount":
                                    #region 我的账户
                                    result = RepayText(openId, myUserName, "我的账户.");
                                    #endregion
                                    break;
                                default:
                                    result = string.Format("<xml><ToUserName><![CDATA[{0}]]></ToUserName>" +
                                         "<FromUserName><![CDATA[{1}]]></FromUserName>" +
                                         "<CreateTime>{2}</CreateTime>" +
                                         "<MsgType><![CDATA[text]]></MsgType>" +
                                         "<Content><![CDATA[{3}]]></Content>" + "</xml>",
                                         openId, myUserName, DateTime.Now.ToBinary(), "没有响应菜单事件");
                                    break;
                            }
                            break;
                    }
                    break;
                default:
                    result = string.Format("<xml><ToUserName><![CDATA[{0}]]></ToUserName>" +
                                         "<FromUserName><![CDATA[{1}]]></FromUserName>" +
                                         "<CreateTime>{2}</CreateTime>" +
                                         "<MsgType><![CDATA[text]]></MsgType>" +
                                         "<Content><![CDATA[{3}]]></Content>" + "</xml>",
                                         openId, myUserName, DateTime.Now.ToBinary(), string.Format("未处理消息类型:{0}", message.Type));
                    break;
            }
            return result;
        }

        private string RepayText(string toUserName, string fromUserName, string content)
        {
            return string.Format("<xml><ToUserName><![CDATA[{0}]]></ToUserName>" +
                                                   "<FromUserName><![CDATA[{1}]]></FromUserName>" +
                                                   "<CreateTime>{2}</CreateTime>" +
                                                   "<MsgType><![CDATA[text]]></MsgType>" +
                                                   "<Content><![CDATA[{3}]]></Content>" + "</xml>",
                                                   toUserName, fromUserName, DateTime.Now.ToBinary(), content);
        }
        private string RepayNews(string toUserName, string fromUserName, List<WeixinNews> news)
        {
            var couponesBuilder = new StringBuilder();
            couponesBuilder.Append(string.Format("<xml><ToUserName><![CDATA[{0}]]></ToUserName>" +
            "<FromUserName><![CDATA[{1}]]></FromUserName>" +
            "<CreateTime>{2}</CreateTime>" +
            "<MsgType><![CDATA[news]]></MsgType>" +
            "<ArticleCount>{3}</ArticleCount><Articles>",
             toUserName, fromUserName,
             DateTime.Now.ToBinary(),
             news.Count
                ));
            foreach (var c in news)
            {
                couponesBuilder.Append(string.Format("<item><Title><![CDATA[{0}]]></Title>" +
                    "<Description><![CDATA[{1}]]></Description>" +
                    "<PicUrl><![CDATA[{2}]]></PicUrl>" +
                    "<Url><![CDATA[{3}]]></Url>" +
                    "</item>",
                   c.Title, c.Description, c.PicUrl, c.Url
                 ));
            }
            couponesBuilder.Append("</Articles></xml>");
            return couponesBuilder.ToString();
        }

    }
    public class WeixinNews
    {
        public string Title { set; get; }
        public string Description { set; get; }
        public string PicUrl { set; get; }
        public string Url { set; get; }
    }
}