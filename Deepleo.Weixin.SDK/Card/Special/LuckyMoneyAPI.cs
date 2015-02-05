using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using Codeplex.Data;

namespace Deepleo.Weixin.SDK.Card.Special
{
    /// <summary>
    /// 特殊卡票接口=> 红包
    /// 商户调用接口创建会员卡获取card_id，并将会员卡下发给用户，用户领取后需激活/绑定update会员卡编号及积分信息。会员卡暂不支持转赠。
    /// </summary>
    public class LuckyMoneyAPI
    {
        /// <summary>
        /// 更新红包金额
        /// 支持领取红包后通过调用“更新红包”接口update红包余额。
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="code">红包的序列号</param>
        /// <param name="card_id">自定义code的卡券必填。非自定义code可不填。</param>
        /// <param name="balance">红包余额</param>
        /// <returns>
        /// {
        ///"errcode":0,
        ///"errmsg":"ok"
        ///}
        ///</returns>
        public static dynamic UpdateUserBalance(string access_token, string code, string card_id, int balance)
        {
            var url = string.Format("https://api.weixin.qq.com/card/luckymoney/updateuserbalance?access_token={0}", access_token);
            var client = new HttpClient();
            var sb = new StringBuilder();
            sb.Append("{")
              .Append('"' + "code" + '"' + ":").Append(code)
              .Append('"' + "card_id" + '"' + ":").Append(card_id)
              .Append('"' + "balance" + '"' + ":").Append(balance)
              .Append("}");
            var result = client.PostAsync(url, new StringContent(sb.ToString())).Result;
            if (result.IsSuccessStatusCode) return string.Empty;
            return DynamicJson.Parse(result.Content.ReadAsStringAsync().Result);
        }

    }
}
