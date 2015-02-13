using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using Codeplex.Data;

namespace Deepleo.Weixin.SDK.Card
{
    /// <summary>
    /// 设置测试用户白名单
    /// </summary>
    public class TestWhiteAPI
    {
        /// <summary>
        /// 设置测试用户白名单
        /// 由于卡券有审核要求，为方便公众号调试，可以设置一些测试帐号，这些帐号可领取未通过审核的卡券，体验整个流程。
        /// 注：同时支持“openid”、“username”两种字段设置白名单，总数上限为10个。
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="testwhitelist">
        /// {
        ///"openid":[ //测试的openid列表
        ///"o1Pj9jmZvwSyyyyyyBa4aULW2mA",
        ///"o1Pj9jmZvxxxxxxxxxULW2mA"
        ///],
        ///"username":[ //测试的微信号列表
        ///"afdvvf",
        ///"abcd"
        ///]
        ///}
        /// </param>
        /// <returns></returns>
        public static dynamic Set(string access_token, dynamic testwhitelist)
        {
            var url = string.Format("https://api.weixin.qq.com/card/testwhitelist/set?access_token={0}", access_token);
            var client = new HttpClient();
            var result = client.PostAsync(url, new StringContent(DynamicJson.Serialize(testwhitelist))).Result;
            if (result.IsSuccessStatusCode) return string.Empty;
            return DynamicJson.Parse(result.Content.ReadAsStringAsync().Result);
        }
    }
}
