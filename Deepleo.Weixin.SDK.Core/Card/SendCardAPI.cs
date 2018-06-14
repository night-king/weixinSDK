using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using Codeplex.Data;
using Deepleo.Weixin.SDK.Helpers;

namespace Deepleo.Weixin.SDK.Card
{
    /// <summary>
    /// 卡券投放接口
    /// </summary>
    public class SendCardAPI
    {
        /// <summary>
        /// 创建二维码
        /// 创建卡券后，商户可通过接口生成一张卡券二维码供用户扫码后添加卡券到卡包。
        ///自定义code的卡券调用接口时，post数据中需指定code，非自定义code不需指定，
        ///指定openid同理。指定后的二维码只能被扫描领取一次。
        ///
        /// 获取二维码ticket后，开发者可用ticket换取二维码图片详情
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="action">
        /// 数据示意：
        /// {
        /// "action_name":"QR_CARD",
        /// "action_info":{
        /// "card":{
        /// "card_id":"pFS7Fjg8kV1IdDz01r4SQwMkuCKc",
        /// "code":"198374613512",
        /// "openid":"oFS7Fjl0WsZ9AMZqrI80nbIq8xrA",
        /// "expire_seconds":"1800"，
        /// "is_unique_code":false,
        /// "outer_id": 1
        /// }
        /// }
        /// }</param>
        /// <returns>二维码图片地址</returns>
        public static string CreateQrcode(string access_token, dynamic action)
        {
            var url = string.Format("https://api.weixin.qq.com/card/qrcode/create?access_token={0}", access_token);
            var client = new HttpClient();
            var result = client.PostAsync(url, new StringContent(DynamicJson.Serialize(action))).Result;
            if (result.IsSuccessStatusCode) return string.Empty;
            var ticket = DynamicJson.Parse(result.Content.ReadAsStringAsync().Result).ticket;
            return string.Format("https://mp.weixin.qq.com/cgi-bin/showqrcode?ticket={0}", ticket);
        }

        /// <summary>
        /// 获取api_ticket
        /// api_ticket是用于调用微信JSAPI的临时票据，有效期为7200秒，通过access_token来获取。
        ///注：由于获取api_ticket的api调用次数非常有限，频繁刷新api_ticket会导致api调用受
        ///限，影响自身业务，开发者需在自己的服务存储与更新api_ticket。
        /// </summary>
        /// <param name="access_token">BasicAPI获取的access_token,也可以通过TokenHelper获取</param>
        /// <returns>
        /// {
        ///"errcode":0,
        ///"errmsg":"ok",
        ///"ticket":"bxLdikRXVbTPdHSM05e5u5sUoXNKdvsdshFKA",
        ///"expires_in":7200
        ///}</returns>
        public static dynamic GetTickect(string access_token)
        {
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token={0}&type=wx_card", access_token);
            var client = new HttpClient();
            var result = client.GetAsync(url).Result;
            if (!result.IsSuccessStatusCode) return string.Empty;
            var jsTicket = DynamicJson.Parse(result.Content.ReadAsStringAsync().Result);
            return jsTicket;
        }


        /// <summary>
        /// 签名算法
        /// </summary>
        /// <param name="api_ticket">api_ticket</param>
        /// <param name="card_id">生成卡券时获得的card_id</param>
        /// <param name="timestamp">时间戳，
        /// signature中的timestamp和card_ext中的timestamp必须保持一致。
        /// 商户生成从1970年1月1日00:00:00至今的秒数,即当前的时间,且最终需要转换为字符串形式;由商户生成后传入。</param>
        /// <param name="code">指定的卡券code码，只能被领一次。use_custom_code字段为true的卡券必须填写，非自定义code不必填写。</param>
        /// <param name="openid">指定领取者的openid，只有该用户能领取。bind_openid字段为true的卡券必须填写，非自定义code不必填写。</param>
        ///  <param name="balance">红包余额，以分为单位。红包类型（LUCKY_MONEY）必填、其他卡券类型不必填写。</param>
        /// <returns></returns>
        public static string GetSignature(string api_ticket, string card_id, long timestamp, string code, string openid, string balance)
        {
            var stringADict = new Dictionary<string, string>();
            stringADict.Add("api_ticket", api_ticket);
            stringADict.Add("card_id", card_id);
            stringADict.Add("timestamp", timestamp.ToString());
            stringADict.Add("code", code);
            stringADict.Add("openid", openid);
            stringADict.Add("balance", balance);
            var string1Builder = new StringBuilder();
            foreach (var va in stringADict.OrderBy(x => x.Value))//将api_ticket、timestamp、card_id、code、openid、balance的value值进行字符串的字典序排序。
            {
                string1Builder.Append(va.Value);
            }
            var signature = Util.Sha1(string1Builder.ToString());
            return signature;
        }

    }
}
