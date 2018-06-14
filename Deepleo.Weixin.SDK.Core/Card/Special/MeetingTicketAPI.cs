using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using Codeplex.Data;

namespace Deepleo.Weixin.SDK.Card.Special
{
    /// <summary>
    /// 特殊卡票接口=> 会议门票
    /// 商户调用接口创建会员卡获取card_id，并将会员卡下发给用户，用户领取后需激活/绑定update会员卡编号及积分信息。会员卡暂不支持转赠。
    /// </summary>
    public class MeetingTicketAPI
    {
        /// <summary>
        /// 更新会议门票接口
        /// 支持调用“更新会议门票”接口update入场时间、区域、座位等信息。
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="tickect">
        /// {
        /// "code":"717523732898",
        /// "card_id":"pXch-jvdwkJjY7evUFV-sGsoMl7A",
        /// "zone":"C区",
        /// "entrance": "东北门",
        /// "seat_number": "2排15号"
        /// }
        /// </param>
        /// <returns>
        /// {
        ///"errcode":0,
        ///"errmsg":"ok"
        ///}
        ///</returns>
        public static dynamic UpdateUser(string access_token, dynamic tickect)
        {
            var url = string.Format("https://api.weixin.qq.com/card/meetingticket/updateuser?access_token={0}", access_token);
            var client = new HttpClient();
            var result = client.PostAsync(url, new StringContent(DynamicJson.Serialize(tickect))).Result;
            if (result.IsSuccessStatusCode) return string.Empty;
            return DynamicJson.Parse(result.Content.ReadAsStringAsync().Result);
        }

    }
}
