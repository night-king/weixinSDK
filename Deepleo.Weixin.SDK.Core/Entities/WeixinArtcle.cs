using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Deepleo.Weixin.SDK.Entities
{
    /// <summary>
    /// 回复用户的消息类型
    /// </summary>
    public enum WeixinArtcleType
    {
        News, //图文消息
        Text, //文本
        Voice, //语音
        Image, //图片
        Video, //视频
        Music, //音乐
    }
    /// <summary>
    /// 图文消息
    /// </summary>
    public class WeixinArtcle
    {
        /// <summary>
        /// 图文消息缩略图的media_id，可以在基础支持-上传多媒体文件接口中获得
        /// </summary>
        public string thumb_media_id { set; get; }

        /// <summary>
        /// 图文消息的作者
        /// </summary>
        public string author { set; get; }

        /// <summary>
        /// 图文消息的标题
        /// </summary>
        public string title { set; get; }

        /// <summary>
        /// 在图文消息页面点击“阅读原文”后的页面
        /// </summary>
        public string content_source_url { set; get; }

        /// <summary>
        /// 图文消息页面的内容，支持HTML标签
        /// </summary>
        public string content { set; get; }

        /// <summary>
        /// 图文消息的描述
        /// </summary>
        public string digest { set; get; }

        /// <summary>
        /// 是否显示封面，1为显示，0为不显示
        /// </summary>
        public string show_cover_pic { set; get; }
    }
}
