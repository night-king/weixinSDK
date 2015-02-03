/*--------------------------------------------------------------------------
* ReplayPassiveMessageAPI.cs
 *Auth:deepleo
* Date:2015.01.15
* Email:2586662969@qq.com
* Website:http://www.weixinsdk.net
*--------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Deepleo.Weixin.SDK.Entities;
using Deepleo.Weixin.SDK.Helpers;

namespace Deepleo.Weixin.SDK
{
    /// <summary>
    /// 发送被动响应消息
    /// 注意：
    /// 1.回复图片等多媒体消息时需要预先上传多媒体文件到微信服务器，只支持认证服务号。
    /// 2.微信服务器在五秒内收不到响应会断掉连接，并且重新发起请求，总共重试三次，如果在调试中，发现用户无法收到响应的消息，可以检查是否消息处理超时。
    /// 3.假如服务器无法保证在五秒内处理并回复，必须直接回复空串，微信服务器不会对此作任何处理，并且不会发起重试。。这种情况下，可以使用客服消息接口进行异步回复。
    /// http://mp.weixin.qq.com/wiki/index.php?title=%E5%8F%91%E9%80%81%E8%A2%AB%E5%8A%A8%E5%93%8D%E5%BA%94%E6%B6%88%E6%81%AF
    /// </summary>
    public class ReplayPassiveMessageAPI
    {
        /// <summary>
        /// 回复文本消息
        /// </summary>
        /// <param name="toUserName"></param>
        /// <param name="fromUserName"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string RepayText(string toUserName, string fromUserName, string content)
        {
            return string.Format("<xml><ToUserName><![CDATA[{0}]]></ToUserName>" +
                                                   "<FromUserName><![CDATA[{1}]]></FromUserName>" +
                                                   "<CreateTime>{2}</CreateTime>" +
                                                   "<MsgType><![CDATA[text]]></MsgType>" +
                                                   "<Content><![CDATA[{3}]]></Content></xml>",
                                                   toUserName, fromUserName, Util.CreateTimestamp(), content);
        }

        /// <summary>
        /// 回复单图文消息
        /// </summary>
        /// <param name="toUserName"></param>
        /// <param name="fromUserName"></param>
        /// <param name="news"></param>
        /// <returns></returns>
        public static string RepayNews(string toUserName, string fromUserName, WeixinNews news)
        {
            var builder = new StringBuilder();
            builder.Append(string.Format("<xml><ToUserName><![CDATA[{0}]]></ToUserName>" +
            "<FromUserName><![CDATA[{1}]]></FromUserName>" +
            "<CreateTime>{2}</CreateTime>" +
            "<MsgType><![CDATA[news]]></MsgType>" +
            "<ArticleCount>{3}</ArticleCount><Articles>",
             toUserName, fromUserName,
             Util.CreateTimestamp(),
            1));
            builder.Append(string.Format("<item><Title><![CDATA[{0}]]></Title>" +
                "<Description><![CDATA[{1}]]></Description>" +
                "<PicUrl><![CDATA[{2}]]></PicUrl>" +
                "<Url><![CDATA[{3}]]></Url>" +
                "</item>",
               news.title, news.description, news.picurl, news.url
             ));
            builder.Append("</Articles></xml>");
            return builder.ToString();
        }

        /// <summary>
        /// 回复多图文消息
        /// </summary>
        /// <param name="toUserName"></param>
        /// <param name="fromUserName"></param>
        /// <param name="news"></param>
        /// <returns></returns>
        public static string RepayNews(string toUserName, string fromUserName, List<WeixinNews> news)
        {
            var builder = new StringBuilder();
            builder.Append(string.Format("<xml><ToUserName><![CDATA[{0}]]></ToUserName>" +
            "<FromUserName><![CDATA[{1}]]></FromUserName>" +
            "<CreateTime>{2}</CreateTime>" +
            "<MsgType><![CDATA[news]]></MsgType>" +
            "<ArticleCount>{3}</ArticleCount><Articles>",
             toUserName, fromUserName,
             Util.CreateTimestamp(),
             news.Count
                ));
            foreach (var c in news)
            {
                builder.Append(string.Format("<item><Title><![CDATA[{0}]]></Title>" +
                    "<Description><![CDATA[{1}]]></Description>" +
                    "<PicUrl><![CDATA[{2}]]></PicUrl>" +
                    "<Url><![CDATA[{3}]]></Url>" +
                    "</item>",
                   c.title, c.description, c.picurl, c.url
                 ));
            }
            builder.Append("</Articles></xml>");
            return builder.ToString();
        }
        /// <summary>
        /// 回复图片消息
        /// </summary>
        /// <param name="toUserName"></param>
        /// <param name="fromUserName"></param>
        /// <param name="media_id">已经上传到微信服务器的图片media_id</param>
        /// <returns></returns>
        public static string ReplayImage(string toUserName, string fromUserName, string media_id)
        {
            return string.Format("<xml><ToUserName><![CDATA[{0}]]></ToUserName>" +
                                                   "<FromUserName><![CDATA[{1}]]></FromUserName>" +
                                                   "<CreateTime>{2}</CreateTime>" +
                                                   "<MsgType><![CDATA[image]]></MsgType>" +
                                                   "<Image><MediaId><![CDATA[{3}]]></MediaId></Image></xml>",
                                                   toUserName, fromUserName, Util.CreateTimestamp(), media_id);
        }
        /// <summary>
        /// 回复语音消息
        /// </summary>
        /// <param name="toUserName"></param>
        /// <param name="fromUserName"></param>
        /// <param name="media_id">已经上传到微信服务器的语音media_id</param>
        /// <returns></returns>
        public static string ReplayVoice(string toUserName, string fromUserName, string media_id)
        {
            return string.Format("<xml><ToUserName><![CDATA[{0}]]></ToUserName>" +
                                                   "<FromUserName><![CDATA[{1}]]></FromUserName>" +
                                                   "<CreateTime>{2}</CreateTime>" +
                                                   "<MsgType><![CDATA[voice]]></MsgType>" +
                                                   "<Voice><MediaId><![CDATA[{3}]]></MediaId></Voice></xml>",
                                                   toUserName, fromUserName, Util.CreateTimestamp(), media_id);
        }
        /// <summary>
        /// 回复视频消息
        /// </summary>
        /// <param name="toUserName"></param>
        /// <param name="fromUserName"></param>
        /// <param name="media_id">已经上传到微信服务器的视频media_id</param>
        /// <param name="title">视频标题</param>
        /// <param name="description">视频文字说明</param>
        /// <returns></returns>
        public static string ReplayVedio(string toUserName, string fromUserName, string media_id, string title, string description)
        {
            return string.Format("<xml><ToUserName><![CDATA[{0}]]></ToUserName>" +
                                                   "<FromUserName><![CDATA[{1}]]></FromUserName>" +
                                                   "<CreateTime>{2}</CreateTime>" +
                                                   "<MsgType><![CDATA[video]]></MsgType>" +
                                                   "<Video><MediaId><![CDATA[{3}]]></MediaId>" +
                                                   "<Title><![CDATA[{4}]]></Title>" +
                                                   "<Description><![CDATA[{5}]]></Description></Video></xml>",
                                                   toUserName, fromUserName, Util.CreateTimestamp(), media_id, title, description);
        }

        /// <summary>
        /// 回复音乐消息
        /// </summary>
        /// <param name="toUserName"></param>
        /// <param name="fromUserName"></param>
        /// <param name="title">音乐标题</param>
        /// <param name="description">音乐描述</param>
        /// <param name="musicUrl">音乐链接</param>
        /// <param name="hqMusicUrl">高质量音乐链接，WIFI环境优先使用该链接播放音乐</param>
        /// <param name="thumb_media_id">缩略图的媒体id，通过上传多媒体文件，得到的id</param>
        /// <returns></returns>
        public static string ReplayMusic(string toUserName, string fromUserName, string title, string description, string musicUrl, string hqMusicUrl, string thumb_media_id)
        {
            return string.Format("<xml><ToUserName><![CDATA[{0}]]></ToUserName>" +
                                                   "<FromUserName><![CDATA[{1}]]></FromUserName>" +
                                                   "<CreateTime>{2}</CreateTime>" +
                                                   "<MsgType><![CDATA[music]]></MsgType>" +
                                                   "<Music>" +
                                                   "<Title><![CDATA[{3}]]></Title>" +
                                                   "<Description><![CDATA[{4}]]></Description>" +
                                                   "<MusicUrl><![CDATA[{5}]]></MusicUrl>" +
                                                   "<HQMusicUrl><![CDATA[{6}]]></HQMusicUrl>" +
                                                   "<ThumbMediaId><![CDATA[{7}]]></ThumbMediaId>" +
                                                   "</Music></xml>",
                                                   toUserName, fromUserName, Util.CreateTimestamp(), title, description, musicUrl, hqMusicUrl, thumb_media_id);
        }
    }
}
