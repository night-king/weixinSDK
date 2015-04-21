/*--------------------------------------------------------------------------
* ReplayActiveMessageAPI.cs
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
        /// <param name="kf_account">完整客服账号，格式为：账号前缀@公众号微信号</param>
        /// <returns></returns>
        public static bool RepayText(string access_token, string touser, string content, string kf_account = null)
        {
            var builder = new StringBuilder();
            builder.Append("{")
                .Append('"' + "touser" + '"' + ":").Append('"' + touser + '"').Append(",")
                .Append('"' + "msgtype" + '"' + ":").Append('"' + "text" + '"').Append(",")
                .Append('"' + "text" + '"' + ":")
                .Append("{")
                .Append('"' + "content" + '"' + ":").Append('"' + content + '"')
                .Append("}");
            if (!string.IsNullOrEmpty(kf_account))
            {
                builder.Append(",");
                builder.Append('"' + "customservice" + '"' + ":")
                       .Append("{")
                       .Append('"' + "kfaccount" + '"' + ":").Append('"' + kf_account + '"')
                       .Append("}");
            }
            builder.Append("}");
            var client = new HttpClient();
            return client.PostAsync(string.Format("https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token={0}", access_token), new StringContent(builder.ToString())).Result.IsSuccessStatusCode;
        }

        /// <summary>
        /// 发送图片消息
        /// </summary>
        /// <param name="access_token">调用接口凭证</param>
        /// <param name="touser">普通用户openid</param>
        /// <param name="media_id">发送的图片的媒体ID</param>
        /// <param name="kf_account">完整客服账号，格式为：账号前缀@公众号微信号</param>
        /// <returns></returns>
        public static bool RepayImage(string access_token, string touser, string media_id, string kf_account = null)
        {
            var builder = new StringBuilder();
            builder.Append("{")
                .Append('"' + "touser" + '"' + ":").Append('"' + touser + '"').Append(",")
                .Append('"' + "msgtype" + '"' + ":").Append('"' + "image" + '"').Append(",")
                .Append('"' + "image" + '"' + ":")
                .Append("{")
                .Append('"' + "media_id" + '"' + ":").Append('"' + media_id + '"')
                .Append("}");
            if (!string.IsNullOrEmpty(kf_account))
            {
                builder.Append(",");
                builder.Append('"' + "customservice" + '"' + ":")
                       .Append("{")
                       .Append('"' + "kfaccount" + '"' + ":").Append('"' + kf_account + '"')
                       .Append("}");
            }
            builder.Append("}");
            var client = new HttpClient();
            return client.PostAsync(string.Format("https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token={0}", access_token), new StringContent(builder.ToString())).Result.IsSuccessStatusCode;
        }
        /// <summary>
        /// 发送语音消息
        /// </summary>
        /// <param name="access_token">调用接口凭证</param>
        /// <param name="touser">普通用户openid</param>
        /// <param name="media_id">发送的语音的媒体ID</param>
        /// <param name="kf_account">完整客服账号，格式为：账号前缀@公众号微信号</param>
        /// <returns></returns>
        public static bool RepayVoice(string access_token, string touser, string media_id, string kf_account = null)
        {
            var builder = new StringBuilder();
            builder.Append("{")
                .Append('"' + "touser" + '"' + ":").Append('"' + touser + '"').Append(",")
                .Append('"' + "msgtype" + '"' + ":").Append('"' + "voice" + '"').Append(",")
                .Append('"' + "voice" + '"' + ":")
                .Append("{")
                .Append('"' + "media_id" + '"' + ":").Append('"' + media_id + '"')
                .Append("}");
            if (!string.IsNullOrEmpty(kf_account))
            {
                builder.Append(",");
                builder.Append('"' + "customservice" + '"' + ":")
                       .Append("{")
                       .Append('"' + "kfaccount" + '"' + ":").Append('"' + kf_account + '"')
                       .Append("}");
            }
            builder.Append("}");
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
        /// <param name="kf_account">完整客服账号，格式为：账号前缀@公众号微信号</param>
        /// <returns></returns>
        public static bool RepayVedio(string access_token, string touser, string media_id, string thumb_media_id, string title, string description, string kf_account = null)
        {
            var builder = new StringBuilder();
            builder.Append("{")
                .Append('"' + "touser" + '"' + ":").Append('"' + touser + '"').Append(",")
                .Append('"' + "msgtype" + '"' + ":").Append('"' + "video" + '"').Append(",")
                .Append('"' + "video" + '"' + ":")
                .Append("{")
                .Append('"' + "media_id" + '"' + ":").Append('"' + media_id + '"').Append(",")
                .Append('"' + "thumb_media_id" + '"' + ":").Append('"' + thumb_media_id + '"').Append(",")
                .Append('"' + "title" + '"' + ":").Append('"' + title + '"').Append(",")
                .Append('"' + "description" + '"' + ":").Append('"' + description + '"')
                .Append("}");
            if (!string.IsNullOrEmpty(kf_account))
            {
                builder.Append(",");
                builder.Append('"' + "customservice" + '"' + ":")
                       .Append("{")
                       .Append('"' + "kfaccount" + '"' + ":").Append('"' + kf_account + '"')
                       .Append("}");
            }
            builder.Append("}");
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
        /// <param name="kf_account">完整客服账号，格式为：账号前缀@公众号微信号</param>
        /// <returns></returns>
        public static bool RepayMusic(string access_token, string touser, string musicurl, string hqmusicurl, string thumb_media_id, string title, string description, string kf_account = null)
        {
            var builder = new StringBuilder();
            builder.Append("{")
                .Append('"' + "touser" + '"' + ":").Append('"' + touser + '"').Append(",")
                .Append('"' + "msgtype" + '"' + ":").Append('"' + "music" + '"').Append(",")
                .Append('"' + "music" + '"' + ":")
                .Append("{")
                .Append('"' + "title" + '"' + ":").Append('"' + title + '"').Append(",")
                .Append('"' + "description" + '"' + ":").Append('"' + description + '"').Append(",")
                .Append('"' + "musicurl" + '"' + ":").Append('"' + musicurl + '"').Append(",")
                .Append('"' + "hqmusicurl" + '"' + ":").Append('"' + hqmusicurl + '"').Append(",")
                .Append('"' + "thumb_media_id" + '"' + ":").Append('"' + thumb_media_id + '"').Append(",")
                .Append("}");
            if (!string.IsNullOrEmpty(kf_account))
            {
                builder.Append(",");
                builder.Append('"' + "customservice" + '"' + ":")
                       .Append("{")
                       .Append('"' + "kfaccount" + '"' + ":").Append('"' + kf_account + '"')
                       .Append("}");
            }
            builder.Append("}");
            var client = new HttpClient();
            return client.PostAsync(string.Format("https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token={0}", access_token), new StringContent(builder.ToString())).Result.IsSuccessStatusCode;
        }
        /// <summary>
        /// 回复单图文消息
        /// </summary>
        /// <param name="access_token">调用接口凭证</param>
        /// <param name="touser">普通用户openid</param>
        /// <param name="news"></param>
        /// <param name="kf_account">完整客服账号，格式为：账号前缀@公众号微信号</param>
        /// <returns></returns>
        public static bool RepayNews(string access_token, string touser, WeixinNews news, string kf_account = null)
        {
            var builder = new StringBuilder();
            builder.Append("{")
                .Append('"' + "touser" + '"' + ":").Append('"' + touser + '"').Append(",")
                .Append('"' + "msgtype" + '"' + ":").Append('"' + "news" + '"').Append(",")
                .Append('"' + "news" + '"' + ":")
                .Append("{")
                   .Append('"' + "articles" + '"' + ":")
                     .Append("[")
                     .Append("{")
                     .Append('"' + "title" + '"' + ":").Append('"' + news.title + '"').Append(",")
                     .Append('"' + "description" + '"' + ":").Append('"' + news.description + '"').Append(",")
                     .Append('"' + "url" + '"' + ":").Append('"' + news.url + '"').Append(",")
                     .Append('"' + "picurl" + '"' + ":").Append('"' + news.picurl + '"')
                     .Append("}")
                   .Append("]")
                .Append("}");
            if (!string.IsNullOrEmpty(kf_account))
            {
                builder.Append(",");
                builder.Append('"' + "customservice" + '"' + ":")
                       .Append("{")
                       .Append('"' + "kfaccount" + '"' + ":").Append('"' + kf_account + '"')
                       .Append("}");
            }
            builder.Append("}");
            var client = new HttpClient();
            return client.PostAsync(string.Format("https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token={0}", access_token), new StringContent(builder.ToString())).Result.IsSuccessStatusCode;
        }

        /// <summary>
        /// 回复多图文消息
        /// </summary>
        /// <param name="access_token">调用接口凭证</param>
        /// <param name="touser">普通用户openid</param>
        /// <param name="news"></param>
        /// <param name="kf_account">完整客服账号，格式为：账号前缀@公众号微信号</param>
        /// <returns></returns>
        public static bool RepayNews(string access_token, string touser, List<WeixinNews> news, string kf_account = null)
        {
            var builder = new StringBuilder();
            builder.Append("{")
                .Append('"' + "touser" + '"' + ":").Append('"' + touser + '"').Append(",")
                .Append('"' + "msgtype" + '"' + ":").Append('"' + "news" + '"').Append(",")
                .Append('"' + "news" + '"' + ":")
                .Append("{").Append('"' + "articles" + '"' + ":").Append("[");
            for (int i = 0; i < news.Count; i++)
            {
                var n = news[i];
                builder.Append("{")
                       .Append('"' + "title" + '"' + ":").Append('"' + n.title + '"').Append(",")
                       .Append('"' + "description" + '"' + ":").Append('"' + n.description + '"').Append(",")
                       .Append('"' + "url" + '"' + ":").Append('"' + n.url + '"').Append(",")
                       .Append('"' + "picurl" + '"' + ":").Append('"' + n.picurl + '"')
                       .Append("}");
                if (i != news.Count - 1) builder.Append(",");
            }
            builder.Append("]")
                   .Append("}");
            if (!string.IsNullOrEmpty(kf_account))
            {
                builder.Append(",");
                builder.Append('"' + "customservice" + '"' + ":")
                       .Append("{")
                       .Append('"' + "kfaccount" + '"' + ":").Append('"' + kf_account + '"')
                       .Append("}");
            }
            builder.Append("}");
            var client = new HttpClient();
            return client.PostAsync(string.Format("https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token={0}", access_token), new StringContent(builder.ToString())).Result.IsSuccessStatusCode;
        }
    }
}
