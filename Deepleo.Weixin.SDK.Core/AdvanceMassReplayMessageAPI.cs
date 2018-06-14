/*--------------------------------------------------------------------------
* AdvanceMassReplayMessageAPI.cs
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
using Deepleo.Weixin.SDK.Entities;
using Codeplex.Data;

namespace Deepleo.Weixin.SDK
{
    /// <summary>
    /// 高级群发消息接口
    /// http://mp.weixin.qq.com/wiki/index.php?title=%E9%AB%98%E7%BA%A7%E7%BE%A4%E5%8F%91%E6%8E%A5%E5%8F%A3
    /// 说明：
    /// 1.订阅号提供了每天一条的群发权限，为服务号提供每月（自然月）4条的群发权限。
    /// 2.对于某些具备开发能力的公众号运营者，可以通过高级群发接口，实现更灵活的群发能力。
    /// 注意：
    /// 1、该接口暂时仅提供给已微信认证的服务号
    /// 2、虽然开发者使用高级群发接口的每日调用限制为100次，但是用户每月只能接收4条，请小心测试
    /// 3、无论在公众平台网站上，还是使用接口群发，用户每月只能接收4条群发消息，多于4条的群发将对该用户发送失败。
    /// 4、具备微信支付权限的公众号，在使用高级群发接口上传、群发图文消息类型时，可使用<a>标签加入外链
    /// </summary>
    public class AdvanceMassReplayMessageAPI
    {
        /// <summary>
        /// 上传图文消息素材【订阅号与服务号认证后均可用】
        /// thumb_media_id:图文消息缩略图的media_id，可以在基础支持-上传多媒体文件接口中获得
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="articles">图文消息，一个图文消息支持1到10条图文</param>
        /// <returns>success: { "type":"news","media_id":"CsEf3ldqkAYJAU6EJeIkStVDSvffUJ54vqbThMgplD-VJXXof6ctX5fI6-aYyUiQ", "created_at":1391857799}</returns>
        public static dynamic UploadArtcles(string access_token, List<WeixinArtcle> articles)
        {
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/media/uploadnews?access_token={0}", access_token);
            var client = new HttpClient();
            var result = client.PostAsync(url, new StringContent(DynamicJson.Serialize(articles))).Result;
            return DynamicJson.Parse(result.Content.ReadAsStringAsync().Result);
        }

        /// <summary>
        /// 回复复视频消息里面的media_id不是基础支持接口返回的media_id，这里需要给基础支持的media添加title和description
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="media_id"></param>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <returns>success:{"type":"video","media_id":"IhdaAQXuvJtGzwwc0abfXnzeezfO0NgPK6AQYShD8RQYMTtfzbLdBIQkQziv2XJc","created_at":1398848981}
        /// </returns>
        public static dynamic UploadVideo(string access_token, string media_id, string title, string description)
        {
            var url = string.Format("https://file.api.weixin.qq.com/cgi-bin/media/uploadvideo?access_token={0}", access_token);
            var client = new HttpClient();
            var builder = new StringBuilder();
            builder
                .Append("{")
                .Append('"' + "media_id" + '"' + ":").Append(media_id).Append(",")
                .Append('"' + "title" + '"' + ":").Append(title)
                .Append('"' + "description" + '"' + ":").Append(description)
                .Append("}");
            var result = client.PostAsync(url, new StringContent(builder.ToString())).Result;
            return DynamicJson.Parse(result.Content.ReadAsStringAsync().Result);
        }

        /// <summary>
        /// 根据分组进行群发【订阅号与服务号认证后均可用】
        /// 请注意：在返回成功时，意味着群发任务提交成功，并不意味着此时群发已经结束，
        /// 所以，仍有可能在后续的发送过程中出现异常情况导致用户未收到消息，如消息有时会进行审核、服务器不稳定等。
        /// 此外，群发任务一般需要较长的时间才能全部发送完毕，请耐心等待。
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="content">图文消息,语音,图片,视频:media_id; 文本:文本消息</param>
        /// <param name="type"></param>
        /// <param name="group_id">群发到的分组的group_id，参加用户管理中用户分组接口，若is_to_all值为true，可不填写group_id</param>
        /// <param name="is_to_all">用于设定是否向全部用户发送，值为true或false，选择true该消息群发给所有用户，选择false可根据group_id发送给指定群组的用户</param>
        /// <returns>success:{"errcode":0,"errmsg":"send job submission success","msg_id":34182 }</returns>
        public static dynamic Replay(string access_token, string content, WeixinArtcleType type, string group_id, bool is_to_all = false)
        {
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/message/mass/sendall?access_token={0}", access_token);
            var client = new HttpClient();
            var builder = new StringBuilder();
            builder.Append("{")
                   .Append('"' + "filter" + '"' + ":")
                                           .Append("{")
                                           .Append('"' + "is_to_all" + '"' + ":").Append(is_to_all).Append(",")
                                           .Append('"' + "group_id" + '"' + ":").Append(group_id)
                                           .Append("},");

            switch (type)
            {
                case WeixinArtcleType.News:
                    builder.Append('"' + "mpnews" + '"' + ":")
                                           .Append("{")
                                           .Append('"' + "media_id" + '"' + ":").Append(content)
                                           .Append("},")
                              .Append('"' + "msgtype" + '"' + ":").Append("mpnews");
                    break;
                case WeixinArtcleType.Text:
                    builder.Append('"' + "text" + '"' + ":")
                                           .Append("{")
                                           .Append('"' + "content" + '"' + ":").Append(content)
                                           .Append("},")
                              .Append('"' + "msgtype" + '"' + ":").Append("text");
                    break;
                case WeixinArtcleType.Voice:
                    builder.Append('"' + "voice" + '"' + ":")
                                           .Append("{")
                                           .Append('"' + "media_id" + '"' + ":").Append(content)
                                           .Append("},")
                              .Append('"' + "msgtype" + '"' + ":").Append("voice");
                    break;
                case WeixinArtcleType.Image:
                    builder.Append('"' + "image" + '"' + ":")
                                           .Append("{")
                                           .Append('"' + "media_id" + '"' + ":").Append(content)
                                           .Append("},")
                              .Append('"' + "msgtype" + '"' + ":").Append("image");
                    break;
                case WeixinArtcleType.Video:
                    builder.Append('"' + "mpvideo" + '"' + ":")
                                           .Append("{")
                                           .Append('"' + "media_id" + '"' + ":").Append(content)
                                           .Append("},")
                              .Append('"' + "msgtype" + '"' + ":").Append("mpvideo");
                    break;
            }
            builder.Append("}");
            var result = client.PostAsync(url, new StringContent(builder.ToString())).Result;
            return DynamicJson.Parse(result.Content.ReadAsStringAsync().Result);
        }

        /// <summary>
        /// 根据OpenID列表群发【订阅号不可用，服务号认证后可用】
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="content">图文消息,语音,图片,视频:media_id; 文本:文本消息</param>
        /// <param name="type"></param>
        /// <param name="touser"></param>
        /// <returns>success:{"errcode":0,"errmsg":"send job submission success","msg_id":34182}</returns>
        public static dynamic ReplayOpenids(string access_token, string content, WeixinArtcleType type, IEnumerable<string> touser, string videoTitle, string videoDesc)
        {
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/message/mass/send?access_token={0}", access_token);
            var client = new HttpClient();
            var builder = new StringBuilder();
            builder.Append("{")
                   .Append('"' + "touser" + '"' + ":")
                   .Append("[");
            foreach (var t in touser)
            {
                builder.Append('"' + t + '"').Append(",");
            }
            builder.Append("],");

            switch (type)
            {
                case WeixinArtcleType.News:
                    builder.Append('"' + "mpnews" + '"' + ":")
                                           .Append("{")
                                           .Append('"' + "media_id" + '"' + ":").Append(content)
                                           .Append("},")
                              .Append('"' + "msgtype" + '"' + ":").Append("mpnews");
                    break;
                case WeixinArtcleType.Text:
                    builder.Append('"' + "text" + '"' + ":")
                                           .Append("{")
                                           .Append('"' + "content" + '"' + ":").Append(content)
                                           .Append("},")
                              .Append('"' + "msgtype" + '"' + ":").Append("text");
                    break;
                case WeixinArtcleType.Voice:
                    builder.Append('"' + "voice" + '"' + ":")
                                           .Append("{")
                                           .Append('"' + "media_id" + '"' + ":").Append(content)
                                           .Append("},")
                              .Append('"' + "msgtype" + '"' + ":").Append("voice");
                    break;
                case WeixinArtcleType.Image:
                    builder.Append('"' + "image" + '"' + ":")
                                           .Append("{")
                                           .Append('"' + "media_id" + '"' + ":").Append(content)
                                           .Append("},")
                              .Append('"' + "msgtype" + '"' + ":").Append("image");
                    break;
                case WeixinArtcleType.Video:
                    builder.Append('"' + "video" + '"' + ":")
                                           .Append("{")
                                           .Append('"' + "media_id" + '"' + ":").Append(content).Append(",")
                                           .Append('"' + "title" + '"' + ":").Append(videoTitle).Append(",")
                                           .Append('"' + "description" + '"' + ":").Append(videoDesc)
                                           .Append("},")
                              .Append('"' + "msgtype" + '"' + ":").Append("video");
                    break;
            }
            builder.Append("}");
            var result = client.PostAsync(url, new StringContent(builder.ToString())).Result;
            return DynamicJson.Parse(result.Content.ReadAsStringAsync().Result);
        }
        /// <summary>
        /// 请注意，只有已经发送成功的消息才能删除删除消息只是将消息的图文详情页失效，已经收到的用户，还是能在其本地看到消息卡片。 
        /// 另外，删除群发消息只能删除图文消息和视频消息，其他类型的消息一经发送，无法删除。
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="msg_id">发送出去的消息ID</param>
        /// <returns>success: {"errcode":0,"errmsg":"ok"}</returns>
        public static dynamic Delete(string access_token, string msg_id)
        {
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/message/mass/delete?access_token={0}", access_token);
            var client = new HttpClient();
            var builder = new StringBuilder();
            builder.Append("{")
                   .Append('"' + "msg_id" + '"' + ":").Append(msg_id)
                   .Append("}");
            var result = client.PostAsync(url, new StringContent(builder.ToString()));
            if (!result.Result.IsSuccessStatusCode) return string.Empty;
            return DynamicJson.Parse(result.Result.Content.ReadAsStringAsync().Result);
        }
      
        /// <summary>
        /// 预览接口【订阅号与服务号认证后均可用】
        /// 开发者可通过该接口发送消息给指定用户，在手机端查看消息的样式和排版。
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="openid">接收消息用户对应该公众号的openid</param>
        /// <param name="content">图文消息,语音,图片,视频:media_id(与根据分组群发中的media_id相同); 文本:文本消息</param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static dynamic Preview(string access_token, string openid, string content,WeixinArtcleType type)
        {
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/message/mass/preview?access_token={0}", access_token);
            var client = new HttpClient();
            var builder = new StringBuilder();
            builder.Append("{")
                   .Append('"' + "touser" + '"' + ":")
                   .Append(openid).Append(",");
            switch (type)
            {
                case WeixinArtcleType.News:
                    builder.Append('"' + "mpnews" + '"' + ":")
                                           .Append("{")
                                           .Append('"' + "media_id" + '"' + ":").Append(content)
                                           .Append("},")
                              .Append('"' + "msgtype" + '"').Append("mpnews");
                    break;
                case WeixinArtcleType.Text:
                    builder.Append('"' + "text" + '"' + ":")
                                           .Append("{")
                                           .Append('"' + "content" + '"' + ":").Append(content)
                                           .Append("},")
                              .Append('"' + "msgtype" + '"').Append("text");
                    break;
                case WeixinArtcleType.Voice:
                    builder.Append('"' + "voice" + '"' + ":")
                                           .Append("{")
                                           .Append('"' + "media_id" + '"' + ":").Append(content)
                                           .Append("},")
                              .Append('"' + "msgtype" + '"').Append("voice");
                    break;
                case WeixinArtcleType.Image:
                    builder.Append('"' + "image" + '"' + ":")
                                           .Append("{")
                                           .Append('"' + "media_id" + '"' + ":").Append(content)
                                           .Append("},")
                              .Append('"' + "msgtype" + '"').Append("image");
                    break;
                case WeixinArtcleType.Video:
                    builder.Append('"' + "video" + '"' + ":")
                                           .Append("{")
                                           .Append('"' + "media_id" + '"' + ":").Append(content)
                                           .Append("},")
                              .Append('"' + "msgtype" + '"').Append("video");
                    break;
            }
            builder.Append("}");
            var result = client.PostAsync(url, new StringContent(builder.ToString())).Result;
            return DynamicJson.Parse(result.Content.ReadAsStringAsync().Result);
        }

        /// <summary>
        /// 查询群发消息发送状态【订阅号与服务号认证后均可用】
        /// 由于群发任务提交后，群发任务可能在一定时间后才完成，因此，群发接口调用时，仅会给出群发任务是否提交成功的提示，
        /// 
        /// 若群发任务提交成功，则在群发任务结束时，会向开发者在公众平台填写的开发者URL（callback URL）推送事件。
        /// 参见 WeixinExecutor.cs: MASSSENDJOBFINISH Event
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="msg_id"></param>
        /// <returns>success: { "msg_id":201053012,"msg_status":"SEND_SUCCESS"}
        /// “send success”或“send fail”或“err(num)” 
        ///send success时，也有可能因用户拒收公众号的消息、系统错误等原因造成少量用户接收失败。
        ///err(num)是审核失败的具体原因，可能的情况如下：err(10001)涉嫌广告, err(20001)涉嫌政治, err(20004)涉嫌社会,
        ///err(20002)涉嫌色情, err(20006)涉嫌违法犯罪,err(20008)涉嫌欺诈, err(20013)涉嫌版权, err(22000)涉嫌互推(互相宣传), err(21000)涉嫌其他
        /// </returns>
        public static dynamic QueryStatus(string access_token, string msg_id)
        {
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/message/mass/get?access_token={0}", access_token);
            var client = new HttpClient();
            var builder = new StringBuilder();
            builder.Append("{")
                   .Append('"' + "msg_id" + '"' + ":").Append(msg_id)
                   .Append("}");
            var result = client.PostAsync(url, new StringContent(builder.ToString()));
            if (!result.Result.IsSuccessStatusCode) return string.Empty;
            return DynamicJson.Parse(result.Result.Content.ReadAsStringAsync().Result);
        }

    }
}
