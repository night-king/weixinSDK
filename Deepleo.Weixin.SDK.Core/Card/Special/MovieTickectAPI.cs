using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using Codeplex.Data;

namespace Deepleo.Weixin.SDK.Card.Special
{
    /// <summary>
    /// 特殊卡票接口=> 电影票
    /// 电影票券主要分为以下两种：
    /// 1、电影票兑换券，归属于团购券。
    /// 2、选座电影票，在购买时需要选定电影、场次、座位，具备较强的时效性和特殊性，此类电影票券即文档中的电影票。
    /// 使用场景：用户点击商户H5页面提供的JSAPI（添加到卡包JSAPI）后，商户根据用户
    /// 电影票信息，调用接口创建卡券，获取card_id后，将带有唯一code的电影票下发给用户，
    /// 用户领取后通过接口（更新电影票）update用户选座信息。
    /// </summary>
    public class MovieTickectAPI
    {

        /// <summary>
        /// 更新电影票
        /// 领取电影票后通过调用“更新电影票”接口update电影信息及用户选座信息。
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="tickect">
        /// {
        ///"code": "277217129962",
        ///"card_id":"p1Pj9jr90_SQRaVqYI239Ka1erkI",
        ///"ticket_class":"4D",
        ///"show_time":1408493192,
        ///"duration"：120,
        ///"screening_room":"5号影厅",
        ///"seat_number":["5排14号", "5排15号"]
        ///   }
        /// </param>
        /// <returns>
        /// {
        ///"errcode":0,
        ///"errmsg":"ok"
        ///}
        ///</returns>
        public static dynamic UpdateUser(string access_token, dynamic tickect)
        {
            var url = string.Format("https://api.weixin.qq.com/card/movieticket/updateuser?access_token={0}", access_token);
            var client = new HttpClient();
            var result = client.PostAsync(url, new StringContent(DynamicJson.Serialize(tickect))).Result;
            if (result.IsSuccessStatusCode) return string.Empty;
            return DynamicJson.Parse(result.Content.ReadAsStringAsync().Result);
        }

    }
}
