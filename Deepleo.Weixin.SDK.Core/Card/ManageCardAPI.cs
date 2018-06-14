using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using Codeplex.Data;

namespace Deepleo.Weixin.SDK.Card
{
    /// <summary>
    /// 卡券管理接口
    /// </summary>
    public class ManageCardAPI
    {
        /// <summary>
        /// 删除卡券
        /// 删除卡券接口允许商户删除任意一类卡券。删除卡券后，该卡券对应已生成的领取用二维码、添加到卡包JSAPI均会失效。
        /// 注意：如用户在商家删除卡券前已领取一张或多张该卡券依旧有效。即删除卡券不能删除已被用户领取，保存在微信客户端中的卡券。
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="card_id">卡券ID</param>
        /// <returns>{
        ///"errcode":0,
        ///"errmsg":"ok"
        ///}</returns>
        public static string DeleteCard(string access_token, string card_id)
        {
            var url = string.Format("https://api.weixin.qq.com/card/delete?access_token={0}", access_token);
            var client = new HttpClient();
            var sb = new StringBuilder();
            sb.Append("{")
                .Append('"' + "card_id" + '"' + ":").Append(card_id)
                .Append("}");
            var result = client.PostAsync(url, new StringContent(sb.ToString())).Result;
            if (result.IsSuccessStatusCode) return string.Empty;
            return DynamicJson.Parse(result.Content.ReadAsStringAsync().Result);
        }

        /// <summary>
        /// 查询code
        /// 
        /// 调用查询code接口可获取code的有效性（非自定义code），该code对应的用户openid、卡券有效期等信息。
        /// 自定义code（use_custom_code为true）的卡券调用接口时，post数据中需包含card_id，非自定义code不需上报。
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="code">要查询的序列号</param>
        /// <param name="card_id">要消耗序列号所述的card_id，生成券时use_custom_code填写true时必填。非自定义code不必填写。</param>
        /// <returns>{
        ///"errcode":0,
        ///"errmsg":"ok",
        ///"openid":"oFS7Fjl0WsZ9AMZqrI80nbIq8xrA",
        ///"card":{
        ///"card_id":"pFS7Fjg8kV1IdDz01r4SQwMkuCKc",
        ///"begin_time":1404205036,
        ///"end_time":1404205036,
        ///}
        ///}
        ///
        ///注：固定时长有效期会根据用户实际领取时间转换，如用户2013年10月1日领取，固定时长有效期为90天，即有效时间为2013年10月1日-12月29日有效。
        /// </returns>
        public static string GetCardCode(string access_token, string code, string card_id = "")
        {
            var url = string.Format("https://api.weixin.qq.com/card/get?access_token={0}", access_token);
            var client = new HttpClient();
            var sb = new StringBuilder();
            sb.Append("{")
              .Append('"' + "code" + '"' + ":").Append(code);
            if (!string.IsNullOrEmpty(card_id))
            {
                sb.Append('"' + "card_id" + '"' + ":").Append(card_id);
            }
            sb.Append("}");
            var result = client.PostAsync(url, new StringContent(sb.ToString())).Result;
            if (result.IsSuccessStatusCode) return string.Empty;
            return DynamicJson.Parse(result.Content.ReadAsStringAsync().Result);
        }

        /// <summary>
        /// 批量查询卡列表
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="offset">查询卡列表的起始偏移量，从0开始，即offset:5是指从从列表里的第六个开始读取。</param>
        /// <param name="count">需要查询的卡片的数量（数量最大50）</param>
        /// <returns>{
        /// "errcode":0,
        /// "errmsg":"ok",
        /// "card_id_list":["ph_gmt7cUVrlRk8swPwx7aDyF-pg"],
        /// "total_num":1
        /// }</returns>
        public static string BatchGetCard(string access_token, int offset, int count)
        {
            var url = string.Format("https://api.weixin.qq.com/card/batchget?access_token={0}", access_token);
            var client = new HttpClient();
            var sb = new StringBuilder();
            sb.Append("{")
              .Append('"' + "offset" + '"' + ":").Append(offset)
              .Append('"' + "count" + '"' + ":").Append(count)
              .Append("}");
            var result = client.PostAsync(url, new StringContent(sb.ToString())).Result;
            if (result.IsSuccessStatusCode) return string.Empty;
            return DynamicJson.Parse(result.Content.ReadAsStringAsync().Result);
        }

        /// <summary>
        /// 查询卡券详情
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="card_id">卡券ID</param>
        /// <returns>
        /// 返回结果示意
        ///｛"errcode":0,
        ///"errmsg":"ok",
        ///"card":{
        ///"card_type": "GROUPON",
        ///"groupon":{
        ///"base_info":{
        ///"status":1,
        ///"id":"p1Pj9jr90_SQRaVqYI239Ka1erkI",
        ///"logo_url":
        ///"http://www.supadmin.cn/uploads/allimg/120216/1_120216214725_1.jpg",
        ///"appid":"wx588def6b0089dd48",
        ///"code_type":"CODE_TYPE_TEXT",
        ///"brand_name":"海底捞",
        ///"title":"132元双人火锅套餐",
        ///"sub_title":"",
        ///"date_info":{
        ///"type":1,
        ///"begin_timestamp":1397577600,
        ///"end_timestamp":1399910400
        ///},
        ///"color":"#3373bb",
        ///"notice":"使用时向服务员出示此券",
        ///"service_phone":"020-88888888",
        ///"description":"不可与其他优惠同享\n如需团购券发票，请在消费时向商户提出\n店内均可使用，仅限堂食\n餐前不可打包，餐后未吃完，可打包\n本团购券不限人数，建议2人使用，超过建议人数须另收酱料费5元/位\n本单谢绝自带酒水饮料",
        ///     "use_limit":1,
        ///"get_limit":3,
        ///"can_share":true,
        ///"location_id_list": [123,12321,345345]
        ///"url_name_type":"URL_NAME_TYPE_RESERVATION",
        ///"custom_url":"http://www.qq.com",
        ///"source":"大众点评"
        ///"sku":{
        ///"quantity":0
        ///}
        ///},
        ///"deal_detail":"以下锅底2选1（有菌王锅、麻辣锅、大骨锅、番茄锅、清补凉锅、酸菜鱼锅可选）：\n大锅1份12元\n小锅2份16元\n以下菜品2选1\n特级肥牛1份30元\n洞庭鮰鱼卷1份20元\n其他\n鲜菇猪肉滑1份18元\n金针菇1份16元\n黑木耳1份9元\n娃娃菜1份8元\n冬瓜1份6元\n火锅面2个6元\n欢乐畅饮2位12元\n自助酱料2位10元",
        ///}
        ///}
        ///}
        ///}
        ///<returns>
        public static string GetCard(string access_token, string card_id)
        {
            var url = string.Format("https://api.weixin.qq.com/card/get?access_token={0}", access_token);
            var client = new HttpClient();
            var sb = new StringBuilder();
            sb.Append("{")
              .Append('"' + "card_id" + '"' + ":").Append(card_id)
              .Append("}");
            var result = client.PostAsync(url, new StringContent(sb.ToString())).Result;
            if (result.IsSuccessStatusCode) return string.Empty;
            return DynamicJson.Parse(result.Content.ReadAsStringAsync().Result);
        }
        /// <summary>
        /// 更改code
        /// 为确保转赠后的安全性，微信允许自定义code的商户对已下发的code进行更改。
        ///注：为避免用户疑惑，建议仅在发生转赠行为后（发生转赠后，微信会通过事件推送的方式告知商户被转赠的卡券code）对用户的code进行更改。
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="card_id">卡券ID</param>
        /// <param name="code">卡券的code编码</param>
        /// <param name="new_code">新的卡券code编码</param>
        /// <returns>
        /// {
        ///"errcode":0,
        ///"errmsg":"ok"
        ///}</returns>
        public static string UpdateCardCode(string access_token, string code, string card_id, string new_code)
        {
            var url = string.Format("https://api.weixin.qq.com/card/code/update?access_token={0}", access_token);
            var client = new HttpClient();
            var sb = new StringBuilder();
            sb.Append("{")
              .Append('"' + "code" + '"' + ":").Append(code)
              .Append('"' + "card_id" + '"' + ":").Append(card_id)
              .Append('"' + "new_code" + '"' + ":").Append(new_code)
              .Append("}");
            var result = client.PostAsync(url, new StringContent(sb.ToString())).Result;
            if (result.IsSuccessStatusCode) return string.Empty;
            return DynamicJson.Parse(result.Content.ReadAsStringAsync().Result);
        }
        /// <summary>
        /// 设置卡券失效接口
        /// 为满足改票、退款等异常情况，可调用卡券失效接口将用户的卡券设置为失效状态。
        /// 注：设置卡券失效的操作不可逆，即无法将设置为失效的卡券调回有效状态，商家须慎重调用该接口。
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="code">需要设置为失效的code</param>
        /// <param name="card_id">自定义code的卡券必填。非自定义code的卡券不填。</param>
        /// <returns>
        /// {
        ///"errcode":0,
        ///"errmsg":"ok"
        ///}
        ///</returns>
        public static string UnavailableCardCode(string access_token, string code, string card_id = "")
        {
            var url = string.Format("https://api.weixin.qq.com/card/code/unavailable?access_token={0}", access_token);
            var client = new HttpClient();
            var sb = new StringBuilder();
            sb.Append("{")
              .Append('"' + "code" + '"' + ":").Append(code);
            if (!string.IsNullOrEmpty(card_id))
            {
                sb.Append('"' + "card_id" + '"' + ":").Append(card_id);
            }
            sb.Append("}");
            var result = client.PostAsync(url, new StringContent(sb.ToString())).Result;
            if (result.IsSuccessStatusCode) return string.Empty;
            return DynamicJson.Parse(result.Content.ReadAsStringAsync().Result);
        }

        /// <summary>
        /// 查询卡券详情
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="new_card">
        /// {
        ///"card_id": "xxxxxxxxxxxxx",
        ///"member_card":{
        ///"base_info":{
        ///"logo_url":
        ///"http:\/\/www.supadmin.cn\/uploads\/allimg\/120216\/1_120216214725_1.jpg",
        ///"color":"Color010",
        ///"notice":"使用时向服务员出示此券",
        ///"service_phone":"020-88888888",
        ///"description":"不可与其他优惠同享\n如需团购券发票，请在消费时向商户提出\n店内均可使用，仅限堂食\n餐前不可打包，餐后未吃完，可打包\n本团购券不限人数，建议2人使用，超过建议人数须另收酱料费5元/位\n本单谢绝自带酒水饮料"
        ///"location_id_list": [123,12321,345345]
        ///},
        ///"bonus_cleared":"aaaaaaaaaaaaaa",
        ///"bonus_rules":"aaaaaaaaaaaaaa",
        ///"prerogative":""
        ///}
        ///}
        /// </param>
        /// <returns>
        /// {
        ///"errcode":0,
        ///"errmsg":"ok"
        ///}
        ///</returns>
        public static string UpdateCard(string access_token, dynamic new_card)
        {
            var url = string.Format("https://api.weixin.qq.com/card/update?access_token={0}", access_token);
            var client = new HttpClient();
            var result = client.PostAsync(url, new StringContent(DynamicJson.Serialize(new_card))).Result;
            if (result.IsSuccessStatusCode) return string.Empty;
            return DynamicJson.Parse(result.Content.ReadAsStringAsync().Result);
        }

        /// <summary>
        /// 库存修改接口
        /// 增减某张卡券的库存。
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="card_id">卡券ID</param>
        /// <param name="increase_stock_value">增加多少库存，可以不填或填0</param>
        /// <param name="reduce_stock_value">减少多少库存，可以不填或填0</param>
        /// <returns>
        /// {
        ///"errcode":0,
        ///"errmsg":"ok"
        ///}
        ///</returns>
        public static string ModifyCardStock(string access_token, string card_id, int increase_stock_value, int reduce_stock_value)
        {
            var url = string.Format("https://api.weixin.qq.com/card//modifystock?access_token={0}", access_token);
            var client = new HttpClient();
            var sb = new StringBuilder();
            sb.Append("{")
              .Append('"' + "card_id" + '"' + ":").Append(card_id)
             .Append('"' + "increase_stock_value" + '"' + ":").Append(increase_stock_value)
             .Append('"' + "reduce_stock_value" + '"' + ":").Append(reduce_stock_value)
             .Append("}");
            var result = client.PostAsync(url, new StringContent(sb.ToString())).Result;
            if (result.IsSuccessStatusCode) return string.Empty;
            return DynamicJson.Parse(result.Content.ReadAsStringAsync().Result);
        }

    }
}
