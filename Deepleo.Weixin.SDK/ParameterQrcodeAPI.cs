/*--------------------------------------------------------------------------
* ParameterQrcodeAPI.cs
 *Auth:deepleo
* Date:2014.05.05
* Email:2586662969@qq.com
*--------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using Codeplex.Data;

namespace Deepleo.Weixin.SDK
{

    public class ParameterQrcodeAPI
    {
        public static string QR_LIMIT_SCENE = "QR_LIMIT_SCENE";
        public static string QR_SCENE = "QR_SCENE";

        /// <summary>
        /// 返回场景微信二维码地址
        /// </summary>
        /// <param name="token"></param>
        /// <param name="type">二维码类型，QR_SCENE为临时,QR_LIMIT_SCENE为永久</param>
        /// <param name="scene_id">场景值ID，临时二维码时为32位非0整型，永久二维码时最大值为100000（目前参数只支持1--100000）</param>
        /// <returns></returns>
        public static string Create(string token, string type, int scene_id)
        {
            var client = new HttpClient();
            var content = new StringBuilder();
            content.Append("{");
            var action_name = "QR_LIMIT_SCENE";
            if (type == QR_SCENE)
            {
                action_name = QR_SCENE;
                content.Append('"' + "expire_seconds" + '"' + ":1800,");
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
            else
            {
                return "";
            }
        }
    }
}
