using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using Codeplex.Data;

namespace Deepleo.Weixin.SDK.Card.Special
{
    /// <summary>
    /// 特殊卡票接口=> 飞机票
    /// 飞机票与其他卡券相比具有更强的时效性和特殊性，故机票生成后无需经过微信审核，即时生效。
    /// 机票使用场景主要分为以下两种：
    /// 1、通过微信购买后直接添加至卡包，可值机时段由卡包在线办理登机牌。
    /// 2、在微信商户（一般为航空公司）公众号内完成值机后，添加至微信卡包。
    /// 第一种场景：用户点击商户H5页面“添加至卡包”的JSAPI（2.2添加到卡包JSAPI）后，商户根据用户机票信息，调用接口创建卡券（1.1创建卡券接口），获取card_id后，
    ///  将机票下发给用户。在可值机时段，用户点击商户指定的URL在线办理登机牌。办理成功后，商户调用选座接口（6.3.1在线选座接口），将值机信息同步。
    /// 第二种场景：用户点击商户H5页面提供的JSAPI（2.2添加到卡包JSAPI）后，商户根据用户机票信息，调用接口生成卡券（1.1创建卡券接口），获取card_id后，将机票下发
    /// 给用户。并立即调用选座接口（6.3.1.在线选座接口），将值机信息同步。
    /// </summary>
    public class AirTickectAPI
    {

        /// <summary>
        /// 更新电影票
        /// 领取电影票后通过调用“更新电影票”接口update电影信息及用户选座信息。
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="tickect">
        /// {
        ///"code":"198374613512",
        ///"card_id":"p1Pj9jr90_SQRaVqYI239Ka1erkI",
        ///"passenger_name":"乘客姓名",//乘客姓名，上限为15个汉字。
        ///"class":"舱等",//舱等，如头等舱等，上限为5个汉字。
        ///"seat":"座位号",//乘客座位号。
        ///"etkt_bnr":"电子客票号",//电子客票号，上限为14个数字
        ///"qrcode_data":"二维码数据",//乘客用于值机的二维码字符串，微信会通过此数据为用户生成值机用的二维码。
        ///"is_cancel ":false//填写true或false。true代表取消，如填写true上述字段（如calss等）均不做判断，机票返回未值机状态，乘客可重新值机。默认填写false
        ///   }
        /// </param>
        /// <returns>
        /// {
        ///"errcode":0,
        ///"errmsg":"ok"
        ///}
        ///</returns>
        public static dynamic CheckinBoardingPass(string access_token, dynamic tickect)
        {
            var url = string.Format("https://api.weixin.qq.com/card/boardingpass/checkin?access_token={0}", access_token);
            var client = new HttpClient();
            var result = client.PostAsync(url, new StringContent(DynamicJson.Serialize(tickect))).Result;
            if (result.IsSuccessStatusCode) return string.Empty;
            return DynamicJson.Parse(result.Content.ReadAsStringAsync().Result);
        }

    }
}
