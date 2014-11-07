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
using Deepleo.Weixin.SDK.Entities;

namespace Deepleo.Web
{
    public class WeixinExecutor : IWeixinExecutor
    {
        public WeixinExecutor()
        {
        }

        /// <summary>
        /// 说明：带TODO字眼的代码段，需要开发者自行按照自己的业务逻辑实现
        /// </summary>
        /// <param name="message"></param>
        /// <returns>已经打包成xml的用于回复用户的消息包</returns>
        public string Execute(WeixinMessage message)
        {
            var result = "";
            var domain = "http://www.weixinsdk.net/";//请更改成你的域名
            var openId = message.Body.FromUserName.Value;
            var myUserName = message.Body.ToUserName.Value;
            //这里需要调用TokenHelper获取Token的，省略了。
            switch (message.Type)
            {
                case WeixinMessageType.Text:
                    string userMessage = message.Body.Content.Value;
                    result = ReplayPassiveMessageAPI.RepayText(openId, myUserName, "欢迎使用，您输入了：" + userMessage);
                    break;
                case WeixinMessageType.Event:
                    string eventType = message.Body.Event.Value.ToLower();
                    string eventKey = message.Body.EventKey.Value;
                    switch (eventType)
                    {
                        case "subscribe"://用户未关注时，进行关注后的事件推送
                            #region 首次关注
                            if (!string.IsNullOrEmpty(eventKey))
                            {
                                var qrscene = eventKey.Replace("qrscene_", "");//此为场景二维码的场景值
                                result = ReplayPassiveMessageAPI.RepayText(openId, myUserName, "欢迎订阅，场景值：" + qrscene);
                            }
                            else
                            {
                                result = ReplayPassiveMessageAPI.RepayText(openId, myUserName, "欢迎订阅");
                            }
                            #endregion
                            break;
                        case "unsubscribe"://取消关注
                            #region 取消关注
                            result = ReplayPassiveMessageAPI.RepayText(openId, myUserName, "欢迎再来");
                            #endregion
                            break;
                        case "scan":// 用户已关注时的事件推送
                            #region 已关注扫码事件
                            if (!string.IsNullOrEmpty(eventKey))
                            {
                                var qrscene = eventKey.Replace("qrscene_", "");//此为场景二维码的场景值
                                result = ReplayPassiveMessageAPI.RepayText(openId, myUserName, "欢迎使用，场景值：" + qrscene);
                            }
                            else
                            {
                                result = ReplayPassiveMessageAPI.RepayText(openId, myUserName, "欢迎使用");
                            }
                            #endregion
                            break;
                        case "location"://上报地理位置事件
                            #region 上报地理位置事件
                            var lat = message.Body.Latitude.Value.ToString();
                            var lng = message.Body.Longitude.Value.ToString();
                            var pcn = message.Body.Precision.Value.ToString();
                            //TODO:在此处将经纬度记录在数据库
                            #endregion
                            break;
                        case "voice"://语音消息
                            #region 语音消息
                            //A：已开通语音识别权限的公众号
                            var userVoice = message.Body.Recognition.Value;//用户语音消息文字
                            result = ReplayPassiveMessageAPI.RepayText(openId, myUserName, "您说:" + userVoice);

                            //B：未开通语音识别权限的公众号
                            var userVoiceMediaId = message.Body.MediaId.Value;//media_id
                            //TODO:调用自定义的语音识别程序识别用户语义

                            #endregion
                            break;
                        case "image"://图片消息
                            #region 图片消息
                            var userImage = message.Body.PicUrl.Value;//用户语音消息文字
                            result = ReplayPassiveMessageAPI.RepayNews(openId, myUserName, new WeixinNews
                            {
                                Title = "您刚才发送了图片消息",
                                PicUrl = string.Format("{0}/Images/ad.jpg", domain),
                                Description = "点击查看图片",
                                Url = userImage
                            });
                            #endregion
                            break;
                        case "click"://自定义菜单事件
                            #region 自定义菜单事件
                            switch (eventKey)
                            {
                                case "myaccount"://CLICK类型事件举例
                                    #region 我的账户
                                    result = ReplayPassiveMessageAPI.RepayNews(openId, myUserName, new List<WeixinNews>()
                                    {
                                        new WeixinNews{
                                            Title="我的帐户",
                                            Url=string.Format("{0}/user?openId={1}",domain,openId),
                                            Description="点击查看帐户详情",
                                            PicUrl=string.Format("{0}/Images/ad.jpg",domain)
                                        },
                                    });
                                    #endregion
                                    break;
                                case "www.weixinsdk.net"://VIEW类型事件举例，注意：点击菜单弹出子菜单，不会产生上报。
                                    //TODO:后台处理逻辑
                                    break;
                                default:
                                    result = ReplayPassiveMessageAPI.RepayText(openId, myUserName, "没有响应菜单事件");
                                    break;
                            }
                            #endregion
                            break;
                    }
                    break;
                default:
                    result = ReplayPassiveMessageAPI.RepayText(openId, myUserName, string.Format("未处理消息类型:{0}", message.Type));
                    break;
            }
            return result;
        }
    }
}