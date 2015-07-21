/*--------------------------------------------------------------------------
* ServiceAdminAPI.cs
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
using System.IO;

namespace Deepleo.Weixin.SDK
{
    /// <summary>
    ///  客服管理
    ///  http://mp.weixin.qq.com/wiki/9/6fff6f191ef92c126b043ada035cc935.html
    /// </summary>
    public class ServiceAdminAPI
    {
        /// <summary>
        /// 获取客服基本信息
        /// </summary>
        /// <param name="access_token"></param>
        /// <returns>success: {"errcode" : 0,"errmsg" : "ok",}； 其他代码请使用ExplainCode解析返回代码含义 </returns>
        public static dynamic GetServiceInfo(string access_token)
        {
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/customservice/getkflist?access_token={0}", access_token);
            var client = new HttpClient();
            var result = client.GetAsync(url).Result;
            return DynamicJson.Parse(result.Content.ReadAsStringAsync().Result);
        }

        /// <summary>
        /// 获取在线客服接待信息
        /// kf_account	完整客服账号，格式为：账号前缀@公众号微信号
        /// status	客服在线状态 1：pc在线，2：手机在线。若pc和手机同时在线则为 1+2=3
        /// kf_id	客服工号
        /// auto_accept	客服设置的最大自动接入数
        /// accepted_case	客服当前正在接待的会话数
        /// </summary>
        /// <param name="access_token"></param>
        /// <returns>success: {"errcode" : 0,"errmsg" : "ok",}； 其他代码请使用ExplainCode解析返回代码含义 </returns>
        public static dynamic GetOnlineServiceInfo(string access_token)
        {
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/customservice/getonlinekflist?access_token={0}", access_token);
            var client = new HttpClient();
            var result = client.GetAsync(url).Result;
            return DynamicJson.Parse(result.Content.ReadAsStringAsync().Result);
        }

        /// <summary>
        /// 添加客服账号
        /// 开发者通过本接口可以为公众号添加客服账号，每个公众号最多添加10个客服账号。
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="kf_account">完整客服账号，格式为：账号前缀@公众号微信号，账号前缀最多10个字符，必须是英文或者数字字符。如果没有公众号微信号，请前往微信公众平台设置。</param>
        /// <param name="nickname">客服昵称，最长6个汉字或12个英文字符</param>
        /// <param name="pswmd5">客服账号登录密码，格式为密码明文的32位加密MD5值</param>
        /// <returns>success: {"errcode" : 0,"errmsg" : "ok",}； 其他代码请使用ExplainCode解析返回代码含义 </returns>
        public static dynamic AddService(string access_token, string kf_account, string nickname, string pswmd5)
        {
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/media/uploadnews?access_token={0}", access_token);
            var builder = new StringBuilder();
            builder
                .Append("{")
                .Append('"' + "kf_account" + '"' + ":").Append(kf_account).Append(",")
                .Append('"' + "nickname" + '"' + ":").Append(nickname).Append(",")
                .Append('"' + "password" + '"' + ":").Append(pswmd5)
                .Append("}");
            var client = new HttpClient();
            var result = client.PostAsync(url, new StringContent(builder.ToString())).Result;
            return DynamicJson.Parse(result.Content.ReadAsStringAsync().Result);
        }

        /// <summary>
        /// 设置客服信息
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="kf_account">完整客服账号，格式为：账号前缀@公众号微信号，账号前缀最多10个字符，必须是英文或者数字字符。如果没有公众号微信号，请前往微信公众平台设置。</param>
        /// <param name="nickname">客服昵称，最长6个汉字或12个英文字符</param>
        /// <param name="pswmd5">客服账号登录密码，格式为密码明文的32位加密MD5值</param>
        /// <returns>success: {"errcode" : 0,"errmsg" : "ok",}； 其他代码请使用ExplainCode解析返回代码含义 </returns>
        public static dynamic UpdateService(string access_token, string kf_account, string nickname, string pswmd5)
        {
            var url = string.Format("https://api.weixin.qq.com/customservice/kfaccount/update?access_token={0}", access_token);
            var builder = new StringBuilder();
            builder
                .Append("{")
                .Append('"' + "kf_account" + '"' + ":").Append(kf_account).Append(",")
                .Append('"' + "nickname" + '"' + ":").Append(nickname).Append(",")
                .Append('"' + "password" + '"' + ":").Append(pswmd5)
                .Append("}");
            var client = new HttpClient();
            var result = client.PostAsync(url, new StringContent(builder.ToString())).Result;
            return DynamicJson.Parse(result.Content.ReadAsStringAsync().Result);
        }

        /// <summary>
        /// 上传客服头像
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="kf_account">完整客服账号，格式为：账号前缀@公众号微信号</param>
        /// <param name="icon"></param>
        /// <returns>success: {"errcode" : 0,"errmsg" : "ok",}； 其他代码请使用ExplainCode解析返回代码含义 </returns>
        public static dynamic UploadIcon(string access_token, string kf_account, string icon)
        {
            var url = string.Format("http://api.weixin.qq.com/customservice/kfacount/uploadheadimg?access_token={0}&kf_account={1}", access_token, kf_account);
            var client = new HttpClient();
            var result = client.PostAsync(url, new StreamContent(new FileStream(icon, FileMode.Open, FileAccess.Read)));
            if (!result.Result.IsSuccessStatusCode) return string.Empty;
            var media = DynamicJson.Parse(result.Result.Content.ReadAsStringAsync().Result);
            return media.media_id;
        }
        /// <summary>
        /// 删除客服账号
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="kf_account"></param>
        /// <returns>success: {"errcode" : 0,"errmsg" : "ok",}； 其他代码请使用ExplainCode解析返回代码含义 </returns>
        public static dynamic DeleteService(string access_token, string kf_account)
        {
            var url = string.Format("https://api.weixin.qq.com/customservice/kfaccount/del?access_token={0}&kf_account={1}", access_token, kf_account);
            var client = new HttpClient();
            var result = client.GetAsync(url).Result;
            return DynamicJson.Parse(result.Content.ReadAsStringAsync().Result);
        }

        /// <summary>
        /// 解释客服管理接口返回码说明
        /// </summary>
        /// <param name="opercode"></param>
        /// <returns></returns>
        public static string ExplainCode(int code)
        {
            switch (code)
            {
                case 0:
                    return "成功(no error)";
                case 61451:
                    return "参数错误(invalid parameter)";
                case 61452:
                    return "无效客服账号(invalid kf_account)";
                case 61453:
                    return "账号已存在(kf_account exsited)";
                case 61454:
                    return "账号名长度超过限制(前缀10个英文字符)(invalid kf_acount length)";
                case 61455:
                    return "账号名包含非法字符(英文+数字)(illegal character in kf_account)";
                case 61456:
                    return "账号个数超过限制(10个客服账号)(kf_account count exceeded)";
                case 61457:
                    return "无效头像文件类型(invalid file type)";
                default:
                    return "";
            }
        }
    }
}
