using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using Codeplex.Data;

namespace Deepleo.Weixin.QYSDK
{
    public class BasicAPI
    {
        /// <summary>
        /// 检查签名是否正确:
        /// http://mp.weixin.qq.com/wiki/index.php?title=%E6%8E%A5%E5%85%A5%E6%8C%87%E5%8D%97
        /// </summary>
        /// <param name="signature"></param>
        /// <param name="timestamp"></param>
        /// <param name="nonce"></param>
        /// <param name="token">AccessToken</param>
        /// <returns>
        /// true: check signature success
        /// false: check failed, 非微信官方调用!
        /// </returns>
        public static bool CheckSignature(string signature, string timestamp, string nonce, string token, out string ent)
        {
            var arr = new[] { token, timestamp, nonce }.OrderBy(z => z).ToArray();
            var arrString = string.Join("", arr);
            var sha1 = System.Security.Cryptography.SHA1.Create();
            var sha1Arr = sha1.ComputeHash(Encoding.UTF8.GetBytes(arrString));
            StringBuilder enText = new StringBuilder();
            foreach (var b in sha1Arr)
            {
                enText.AppendFormat("{0:x2}", b);
            }
            ent = enText.ToString();
            return signature == enText.ToString();
        }

        /// <summary>
        /// 获取AccessToken
        /// http://qydev.weixin.qq.com/wiki/index.php?title=%E4%B8%BB%E5%8A%A8%E8%B0%83%E7%94%A8
        /// </summary>
        /// <param name="grant_type"></param>
        /// <param name="appid"></param>
        /// <param name="secrect"></param>
        /// <returns>
        /// {
        ///"access_token": "accesstoken000001",
        ///}</returns>
        public static dynamic GetAccessToken(string corpid, string secret)
        {
            var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/gettoken?corpid={0}&corpsecret={1}", corpid, secret);
            var client = new HttpClient();
            var result = client.GetAsync(url).Result;
            if (!result.IsSuccessStatusCode) return string.Empty;
            var token = DynamicJson.Parse(result.Content.ReadAsStringAsync().Result);
            return token;
        }

        /// <summary>
        /// 二次验证
        /// http://qydev.weixin.qq.com/wiki/index.php?title=%E6%88%90%E5%91%98%E5%85%B3%E6%B3%A8%E4%BC%81%E4%B8%9A%E5%8F%B7
        /// 注意：url的域名必须设置为企业小助手的可信域名
        /// 权限说明: 管理员须拥有userid对应成员的管理权限。
        /// </summary>
        /// <param name="access_token">调用接口凭证</param>
        /// <param name="userid">成员UserID</param>
        /// <returns></returns>
        public static bool AuthAgain(string access_token, string userid)
        {
            var client = new HttpClient();
            var result = client.GetAsync(string.Format("https://qyapi.weixin.qq.com/cgi-bin/user/authsucc?access_token={0}&userid={1}", access_token, userid)).Result;
            return DynamicJson.Parse(result.Content.ReadAsStringAsync().Result).errcode == 0;
        }

    }
}
