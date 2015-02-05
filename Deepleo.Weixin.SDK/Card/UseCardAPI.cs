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
    /// 卡券核销接口
    /// </summary>
    public class UseCardAPI
    {
        /// <summary>
        /// 消耗code
        /// 消耗code接口是核销卡券的唯一接口，仅支持核销有效期内的卡券，否则会返回错误码invalidtime。
        /// 自定义code（use_custom_code为true）的优惠券，在code被核销时，必须调用此接口。用于将用户客户端的code状态变更。
        /// 自定义code的卡券调用接口时，post数据中需包含card_id，非自定义code不需上报。
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="code">要消耗序列号，创建卡券时use_custom_code填写true时必填。非自定义code不必填写。</param>
        /// <param name="card_id">卡券ID</param>
        /// <returns>
        /// {
        ///"errcode":0,
        ///"errmsg":"ok",
        ///"card":{"card_id":"pFS7Fjg8kV1IdDz01r4SQwMkuCKc"},
        ///"openid":"oFS7Fjl0WsZ9AMZqrI80nbIq8xrA"
        ///}
        ///
        /// 错误码，0：正常，40099：该code已被核销
        ///</returns>
        public static string Consume(string access_token, string code, string card_id)
        {
            var url = string.Format("https://api.weixin.qq.com/card/code/consume?access_token={0}", access_token);
            var client = new HttpClient();
            var sb = new StringBuilder();
            sb.Append("{")
                .Append('"' + "code" + '"' + ":").Append(code)
                .Append('"' + "card_id" + '"' + ":").Append(card_id)
                .Append("}");
            var result = client.PostAsync(url, new StringContent(sb.ToString())).Result;
            if (result.IsSuccessStatusCode) return string.Empty;
            return DynamicJson.Parse(result.Content.ReadAsStringAsync().Result);
        }

        /// <summary>
        /// 签名算法
        /// </summary>
        /// <param name="api_ticket">api_ticket</param>
        /// <param name="app_id">公众号appID</param>
        /// <param name="location_id">门店信息</param>
        /// <param name="time_stamp">时间戳</param>
        /// <param name="nonce_str">随机字符串</param>
        ///  <param name="card_id">生成卡券时获得的card_id</param>
        ///  <param name="card_type">卡券类型</param>
        /// <returns>
        /// {
        /// "err_msg":"choose_card:ok",//choose_card:ok选取卡券成功
        /// "choose_card_info":[
        /// {
        /// "card_id":"p3G6It08WKRgR0hyV3hHVb6pxrPQ",
        /// "encrypt_code":"XXIzTtMqCxwOaawoE91+VJdsFmv7b8g0VZIZkqf4GWA60Fzpc8ksZ/5ZZ0DVkXdE"
        /// ]
        /// }
        /// }
        /// </returns>
        public static string GetSignature(string api_ticket, string app_id, string location_id, string time_stamp, string nonce_str, string card_id, string card_type)
        {
            var stringADict = new Dictionary<string, string>();
            stringADict.Add("api_ticket", api_ticket);
            stringADict.Add("app_id", app_id);
            stringADict.Add("location_id", location_id);
            stringADict.Add("timestamp", location_id.ToString());
            stringADict.Add("nonce_str", nonce_str);
            stringADict.Add("card_id", card_id);
            stringADict.Add("card_type", card_type);
            var string1Builder = new StringBuilder();
            foreach (var va in stringADict.OrderBy(x => x.Value))//将pi_ticket、app_id、location_id、times_tamp、nonce_str、card_id、card_type的value值进行字符串的字典序排序。
            {
                string1Builder.Append(va.Value);
            }
            var signature = Util.Sha1(string1Builder.ToString());
            return signature;
        }

        /// <summary>
        /// code解码接口
        /// code解码接口支持两种场景：
        ///1.商家获取choos_card_info后，将card_id和encrypt_code字段通过解码接口，获取真实code。
        ///2.卡券内跳转外链的签名中会对code进行加密处理，通过调用解码接口获取真实code。
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="encrypt_code">通过choose_card_info获取的加密字符串</param>
        /// <returns>
        /// {
        ///"errcode":0,
        ///"errmsg":"ok",
        ///"code":"751234212312"
        ///}
        /// </returns>
        public static string DecryptCode(string access_token, string encrypt_code)
        {
            var url = string.Format("https://api.weixin.qq.com/card/code/decrypt?access_token={0}", access_token);
            var client = new HttpClient();
            var sb = new StringBuilder();
            sb.Append("{")
                .Append('"' + "encrypt_code" + '"' + ":").Append(encrypt_code)
                .Append("}");
            var result = client.PostAsync(url, new StringContent(sb.ToString())).Result;
            if (result.IsSuccessStatusCode) return string.Empty;
            return DynamicJson.Parse(result.Content.ReadAsStringAsync().Result);
        }

    }
}
