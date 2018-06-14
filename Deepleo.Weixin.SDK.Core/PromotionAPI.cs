/*--------------------------------------------------------------------------
* PromotionAPI.cs
 *Auth:deepleo
* Date:Date:2015.01.15
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
    public enum ParameterQrcodeType
    {
        QR_LIMIT_SCENE = 1,
        QR_SCENE = 2
    }
    /// <summary>
    /// 帐号管理
    /// http://mp.weixin.qq.com/wiki/index.php?title=%E7%94%9F%E6%88%90%E5%B8%A6%E5%8F%82%E6%95%B0%E7%9A%84%E4%BA%8C%E7%BB%B4%E7%A0%81
    /// </summary>
    public class PromotionAPI
    {
        /// <summary>
        /// 生成带参数的二维码
        /// </summary>
        /// <param name="token"></param>
        /// <param name="type">二维码类型，QR_SCENE为临时,QR_LIMIT_SCENE为永久</param>
        /// <param name="scene_id">场景值ID，临时二维码时为32位非0整型，永久二维码时最大值为100000（目前参数只支持1--100000）</param>
        /// <returns>返回场景二维码的微信服务器地址</returns>
        public static string CreateParameterQrcode(string token, ParameterQrcodeType type, int scene_id,int days)
        {
            var client = new HttpClient();
            var content = new StringBuilder();
            content.Append("{");
            var action_name = "QR_LIMIT_SCENE";
            if (type == ParameterQrcodeType.QR_SCENE)
            {
                action_name = "QR_SCENE";
                content.Append('"' + "expire_seconds" + '"' + ":").Append(new TimeSpan(days,0,0,0,0).TotalSeconds).Append(",");
            }
            content
            .Append('"' + "action_name" + '"' + ": " + '"' + action_name + '"' + ",")
            .Append('"' + "action_info" + '"' + ": " + "{" + '"' + "scene" + '"' + ":{" + '"' + "scene_id" + '"' + ":" + scene_id.ToString() + "}}}");

            var result = client.PostAsync(
                string.Format("https://api.weixin.qq.com/cgi-bin/qrcode/create?access_token={0}", token),
                new StringContent(content.ToString())).Result;
            if (result.IsSuccessStatusCode)
            {
                var ticket = DynamicJson.Parse(result.Content.ReadAsStringAsync().Result).ticket;
                return string.Format("https://mp.weixin.qq.com/cgi-bin/showqrcode?ticket={0}", ticket);
            }
            return "";
        }

        /// <summary>
        /// 将一条长链接转成短链接。
        /// 主要使用场景： 开发者用于生成二维码的原链接（商品、支付二维码等）太长导致扫码速度和成功率下降，
        /// 将原长链接通过此接口转成短链接再生成二维码将大大提升扫码速度和成功率。
        /// </summary>
        /// <param name="access_token">调用接口凭证</param>
        /// <param name="long_url">需要转换的长链接，支持http://、https://、weixin://wxpay 格式的url</param>
        /// <param name="action">默认long2short，代表长链接转短链接</param>
        /// <returns></returns>
        public static string ShortUrl(string access_token, string long_url, string action = "long2short")
        {
            var builder = new StringBuilder();
            builder
                .Append("{")
                .Append('"' + "action" + '"' + ":").Append(action).Append(",")
                .Append('"' + "long_url" + '"' + ":").Append(long_url)
                .Append("}");
            var client = new HttpClient();
            var result = client.PostAsync(string.Format("https://api.weixin.qq.com/cgi-bin/shorturl?access_token={0}", access_token), new StringContent(builder.ToString())).Result;
            return DynamicJson.Parse(result.Content.ReadAsStringAsync().Result).errcode == 0;
        }
    }
}
