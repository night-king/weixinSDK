using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using Codeplex.Data;

namespace Deepleo.Weixin.SDK.Card.Special
{
    /// <summary>
    /// 特殊卡票接口=> 会员卡
    /// 商户调用接口创建会员卡获取card_id，并将会员卡下发给用户，用户领取后需激活/绑定update会员卡编号及积分信息。会员卡暂不支持转赠。
    /// </summary>
    public class MemberCardAPI
    {

        /// <summary>
        /// 激活/绑定会员卡支持以下两种方式：
        ///1.用户点击卡券内“bind_old_card_url”、“activate_url”跳转商户自定义的H5页面，填写相关身份认证信息后，商户调用接口，完成激活/绑定。
        ///2.商户已知用户身份或无需进行绑定等操作，用户领取会员卡后，商户后台即时调用“激活/绑定会员卡”接口update会员卡编号及积分信息。
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="memeber_card">
        /// {
        ///"init_bonus": 100,
        ///"init_balance": 200,
        ///"membership_number": "AAA00000001",
        ///"code":"12312313",
        ///"card_id":"xxxx_card_id"
        ///}
        ///或
        ///{
        ///"bonus": “www.xxxx.com”,
        ///"balance":“www.xxxx.com”,
        ///"membership_number": "AAA00000001",
        ///"code":"12312313",
        ///"card_id":"xxxx_card_id"
        ///}
        /// </param>
        /// <returns>
        /// {
        ///"errcode":0,
        ///"errmsg":"ok"
        ///}
        ///</returns>
        public static dynamic ActivateCard(string access_token, dynamic memeber_card)
        {
            var url = string.Format("https://api.weixin.qq.com/card/membercard/activate?access_token={0}", access_token);
            var client = new HttpClient();
            var result = client.PostAsync(url, new StringContent(DynamicJson.Serialize(memeber_card))).Result;
            if (result.IsSuccessStatusCode) return string.Empty;
            return DynamicJson.Parse(result.Content.ReadAsStringAsync().Result);
        }

        /// <summary>
        /// 会员卡交易
        /// 会员卡交易后每次积分及余额变更需通过接口通知微信，便于后续消息通知及其他扩展功能。
        /// </summary>
        /// <param name="code">要消耗的序列号</param>
        /// <param name="add_bonus">需要变更的积分，扣除积分用“-“表示。</param>
        /// <param name="record_bonus">商家自定义积分消耗记录，不超过14个汉字。</param>
        /// <param name="add_balance">需要变更的余额，扣除金额用“-”表示。单位为分</param>
        /// <param name="record_balance">商家自定义金额消耗记录，不超过14个汉字。</param>
        /// <param name="card_id">要消耗序列号所述的card_id。自定义code的会员卡必填。</param>
        /// <returns>
        /// {
        ///"errcode":0,
        ///"errmsg":"ok",
        ///"result_bonus":100,
        ///"result_balance":200
        ///"openid":"oFS7Fjl0WsZ9AMZqrI80nbIq8xrA"
        ///}
        /// </returns>
        public static dynamic UpdateUserCard(string access_token, string code, int add_bonus, string record_bonus, int add_balance, string record_balance, string card_id)
        {
            var url = string.Format("https://api.weixin.qq.com/card/membercard/updateuser?access_token={0}", access_token);
            var client = new HttpClient();
            var sb = new StringBuilder();
            sb.Append("{")
              .Append('"' + "code" + '"' + ":").Append(code)
              .Append('"' + "card_id" + '"' + ":").Append(card_id)
              .Append('"' + "record_bonus" + '"' + ":").Append(record_bonus)
              .Append('"' + "add_bonus" + '"' + ":").Append(add_bonus)
              .Append('"' + "add_balance" + '"' + ":").Append(add_balance)
              .Append('"' + "record_balance" + '"' + ":").Append(record_balance)
              .Append("}");
            var result = client.PostAsync(url, new StringContent(sb.ToString())).Result;
            if (result.IsSuccessStatusCode) return string.Empty;
            return DynamicJson.Parse(result.Content.ReadAsStringAsync().Result);
        }
    }
}
