using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using Codeplex.Data;
using Deepleo.Weixin.SDK.Entities;

namespace Deepleo.Weixin.SDK
{
    /// <summary>
    /// 发送(主动)客服消息
    /// </summary>
    public class ReplayActiveMessageAPI
    {
        /// <summary>
        /// 发送文本消息
        /// </summary>
        /// <param name="access_token">调用接口凭证</param>
        /// <param name="touser">普通用户openid</param>
        /// <param name="content">文本消息内容</param>
        /// <returns></returns>
        public static bool RepayText(string access_token, string touser, string content)
        {
            var builder = new StringBuilder();
            builder.Append("{")
                .Append('"' + "touser" + '"' + ":").Append(touser)
                .Append('"' + "msgtype" + '"' + ":").Append("text")
                .Append('"' + "text" + '"' + ":")
                .Append("{")
                .Append('"' + "content" + '"' + ":").Append(content)
                .Append("}")
                .Append("}");
            var client = new HttpClient();
            return client.PostAsync(string.Format("https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token={0}", access_token), new StringContent(builder.ToString())).Result.IsSuccessStatusCode;
        }

        /// <summary>
        /// 发送图片消息
        /// </summary>
        /// <param name="access_token">调用接口凭证</param>
        /// <param name="touser">普通用户openid</param>
        /// <param name="media_id">发送的图片的媒体ID</param>
        /// <returns></returns>
        public static bool RepayImage(string access_token, string touser, string media_id)
        {
            var builder = new StringBuilder();
            builder.Append("{")
                .Append('"' + "touser" + '"' + ":").Append(touser)
                .Append('"' + "msgtype" + '"' + ":").Append("image")
                .Append('"' + "image" + '"' + ":")
                .Append("{")
                .Append('"' + "media_id" + '"' + ":").Append(media_id)
                .Append("}")
                .Append("}");
            var client = new HttpClient();
            return client.PostAsync(string.Format("https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token={0}", access_token), new StringContent(builder.ToString())).Result.IsSuccessStatusCode;
        }
        /// <summary>
        /// 发送语音消息
        /// </summary>
        /// <param name="access_token">调用接口凭证</param>
        /// <param name="touser">普通用户openid</param>
        /// <param name="media_id">发送的语音的媒体ID</param>
        /// <returns></returns>
        public static bool RepayVoice(string access_token, string touser, string media_id)
        {
            var builder = new StringBuilder();
            builder.Append("{")
                .Append('"' + "touser" + '"' + ":").Append(touser)
                .Append('"' + "msgtype" + '"' + ":").Append("voice")
                .Append('"' + "voice" + '"' + ":")
                .Append("{")
                .Append('"' + "media_id" + '"' + ":").Append(media_id)
                .Append("}")
                .Append("}");
            var client = new HttpClient();
            return client.PostAsync(string.Format("https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token={0}", access_token), new StringContent(builder.ToString())).Result.IsSuccessStatusCode;
        }
        /// <summary>
        /// 发送视频消息
        /// </summary>
        /// <param name="access_token">调用接口凭证</param>
        /// <param name="touser">普通用户openid</param>
        /// <param name="media_id">发送的视频的媒体ID</param>
        /// <param name="thumb_media_id">缩略图的媒体ID</param>
        /// <param name="title">视频消息的标题</param>
        /// <param name="description">视频消息的描述</param>
        /// <returns></returns>
        public static bool RepayVedio(string access_token, string touser, string media_id, string thumb_media_id, string title, string description)
        {
            var builder = new StringBuilder();
            builder.Append("{")
                .Append('"' + "touser" + '"' + ":").Append(touser)
                .Append('"' + "msgtype" + '"' + ":").Append("video")
                .Append('"' + "video" + '"' + ":")
                .Append("{")
                .Append('"' + "media_id" + '"' + ":").Append(media_id).Append(",")
                .Append('"' + "thumb_media_id" + '"' + ":").Append(thumb_media_id).Append(",")
                .Append('"' + "title" + '"' + ":").Append(title).Append(",")
                .Append('"' + "description" + '"' + ":").Append(description)
                .Append("}")
                .Append("}");
            var client = new HttpClient();
            return client.PostAsync(string.Format("https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token={0}", access_token), new StringContent(builder.ToString())).Result.IsSuccessStatusCode;
        }

        /// <summary>
        /// 发送音乐消息
        /// </summary>
        /// <param name="access_token">调用接口凭证</param>
        /// <param name="touser">普通用户openid</param>
        /// <param name="musicurl">音乐链接</param>
        /// <param name="hqmusicurl">高品质音乐链接，wifi环境优先使用该链接播放音乐</param>
        /// <param name="thumb_media_id">缩略图的媒体ID</param>
        /// <param name="title">音乐标题</param>
        /// <param name="description">音乐描述</param>
        /// <returns></returns>
        public static bool RepayMusic(string access_token, string touser, string musicurl, string hqmusicurl, string thumb_media_id, string title, string description)
        {
            var builder = new StringBuilder();
            builder.Append("{")
                .Append('"' + "touser" + '"' + ":").Append(touser)
                .Append('"' + "msgtype" + '"' + ":").Append("music")
                .Append('"' + "music" + '"' + ":")
                .Append("{")
                .Append('"' + "title" + '"' + ":").Append(title).Append(",")
                .Append('"' + "description" + '"' + ":").Append(description).Append(",")
                .Append('"' + "musicurl" + '"' + ":").Append(musicurl).Append(",")
                .Append('"' + "hqmusicurl" + '"' + ":").Append(hqmusicurl).Append(",")
                .Append('"' + "thumb_media_id" + '"' + ":").Append(thumb_media_id).Append(",")
                .Append("}")
                .Append("}");
            var client = new HttpClient();
            return client.PostAsync(string.Format("https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token={0}", access_token), new StringContent(builder.ToString())).Result.IsSuccessStatusCode;
        }
        /// <summary>
        /// 回复单图文消息
        /// </summary>
        /// <param name="access_token">调用接口凭证</param>
        /// <param name="touser">普通用户openid</param>
        /// <param name="news"></param>
        /// <returns></returns>
        public static bool RepayNews(string access_token, string touser, WeixinNews news)
        {
            var builder = new StringBuilder();
            builder.Append("{")
                .Append('"' + "touser" + '"' + ":").Append(touser)
                .Append('"' + "msgtype" + '"' + ":").Append("news")
                .Append('"' + "news" + '"' + ":")
                .Append("{").Append('"' + "articles" + '"' + ":").Append("[")
                   .Append("{")
                   .Append('"' + "title" + '"' + ":").Append(news.Title).Append(",")
                   .Append('"' + "description" + '"' + ":").Append(news.Description).Append(",")
                   .Append('"' + "url" + '"' + ":").Append(news.Url).Append(",")
                   .Append('"' + "picurl" + '"' + ":").Append(news.PicUrl)
                   .Append("}")
                .Append("]").Append("}");
            var client = new HttpClient();
            return client.PostAsync(string.Format("https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token={0}", access_token), new StringContent(builder.ToString())).Result.IsSuccessStatusCode;
        }

        /// <summary>
        /// 回复多图文消息
        /// </summary>
        /// <param name="access_token">调用接口凭证</param>
        /// <param name="touser">普通用户openid</param>
        /// <param name="news"></param>
        /// <returns></returns>
        public static bool RepayNews(string access_token, string touser, List<WeixinNews> news)
        {
            var builder = new StringBuilder();
            builder.Append("{")
                .Append('"' + "touser" + '"' + ":").Append(touser)
                .Append('"' + "msgtype" + '"' + ":").Append("news")
                .Append('"' + "news" + '"' + ":")
                .Append("{").Append('"' + "articles" + '"' + ":").Append("[");
            for (int i = 0; i < news.Count; i++)
            {
                var n = news[i];
                builder.Append("{")
                       .Append('"' + "title" + '"' + ":").Append(n.Title).Append(",")
                       .Append('"' + "description" + '"' + ":").Append(n.Description).Append(",")
                       .Append('"' + "url" + '"' + ":").Append(n.Url).Append(",")
                       .Append('"' + "picurl" + '"' + ":").Append(n.PicUrl)
                       .Append("}");
                if (i != news.Count - 1) builder.Append(",");
            }
            builder.Append("]").Append("}");
            var client = new HttpClient();
            return client.PostAsync(string.Format("https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token={0}", access_token), new StringContent(builder.ToString())).Result.IsSuccessStatusCode;
        }
    }
}
