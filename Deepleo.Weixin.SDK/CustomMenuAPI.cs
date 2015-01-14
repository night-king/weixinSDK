/*--------------------------------------------------------------------------
* CustomMenu.cs
 *Auth:deepleo
* Date:2013.12.31
* Email:2586662969@qq.com
 * Website:http://www.weixinsdk.net
*--------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using Codeplex.Data;

namespace Deepleo.Weixin.SDK
{
    /// <summary>
    /// "自定义菜单”
    /// http://mp.weixin.qq.com/wiki/index.php?title=%E8%87%AA%E5%AE%9A%E4%B9%89%E8%8F%9C%E5%8D%95%E5%88%9B%E5%BB%BA%E6%8E%A5%E5%8F%A3
    /// 注意：自定义菜单事件推送接口见：AcceptMessageAPI
    /// 创建自定义菜单后，由于微信客户端缓存，需要24小时微信客户端才会展现出来，测试时可以尝试取消关注公众账号后再次关注，则可以看到创建后的效果。
    /// 自定义菜单种类如下：
    /// 1、click：点击推事件
    /// 2、view：跳转URL
    /// 3、scancode_push：扫码推事件
    /// 4、scancode_waitmsg：扫码推事件且弹出“消息接收中”提示框
    /// 5、pic_sysphoto：弹出系统拍照发图
    /// 6、pic_photo_or_album：弹出拍照或者相册发图
    /// 7、pic_weixin：弹出微信相册发图器
    /// 8、location_select：弹出地理位置选择器
    /// </summary>
    public class CustomMenuAPI
    {
        /// <summary>
        /// 自定义菜单创建接口
        /// </summary>
        /// <param name="token"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static bool Create(string token, string content)
        {
            var client = new HttpClient();
            var result = client.PostAsync(string.Format("https://api.weixin.qq.com/cgi-bin/menu/create?access_token={0}", token), new StringContent(content)).Result;
            return DynamicJson.Parse(result.Content.ReadAsStringAsync().Result).errcode == 0;
        }

        /// <summary>
        /// 自定义菜单查询接口
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static dynamic Query(string token)
        {
            var client = new HttpClient();
            var result = client.GetAsync(string.Format("https://api.weixin.qq.com/cgi-bin/menu/get?access_token={0}", token)).Result;
            if (!result.IsSuccessStatusCode) return null;
            return DynamicJson.Parse(result.Content.ReadAsStringAsync().Result);
        }

        /// <summary>
        /// 自定义菜单删除接口
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static bool Delete(string token)
        {
            var client = new HttpClient();
            var result = client.GetAsync(string.Format("https://api.weixin.qq.com/cgi-bin/menu/delete?access_token={0}", token)).Result;
            if (!result.IsSuccessStatusCode) return false;
            return DynamicJson.Parse(result.Content.ReadAsStringAsync().Result).errmsg == "ok";
        }

    }
}
