using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Deepleo.Weixin.SDK.Helpers;
using Codeplex.Data;
using System.Net.Http;
using System.IO;

namespace Deepleo.Weixin.SDK.Card
{
    /// <summary>
    /// 创建卡券接口
    /// </summary>
    public class CreateCardAPI
    {
        /// <summary>
        ///上传LOGO接口
        ///开发者需调用该接口上传商户图标至微信服务器，获取相应logo_url，用于卡券创建。
        ///注意事项
        ///1.上传的图片限制文件大小限制1MB，像素为300*300，支持JPG格式。
        ///2.调用接口获取的logo_url进支持在微信相关业务下使用，否则会做相应处理。
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="file"></param>
        /// <returns>返回上传后路径</returns>
        public static string UploadLogo(string access_token, string fileName, Stream inputStream)
        {
            var url = string.Format("http://api.weixin.qq.com/cgi-bin/uploadimg?access_token={0}", access_token);
            var returnMessage = DynamicJson.Parse(Util.HttpRequestPost(url, "buffer", fileName, inputStream));
            if (returnMessage.errcode != 0) return string.Empty;
            return returnMessage.url;
        }

        /// <summary>
        /// 批量导入门店信息
        /// 接口说明
        ///支持商户调用该接口批量导入/新建门店信息，获取门店ID。
        ///注：通过该接口导入的门店信息将进入门店审核流程，审核期间可正常使用。若导入的
        ///门店信息未通过审核，则会被剔除出门店列表。
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="location_list">门店列表
        /// 数据示意：
        /// {"location_list":[
        ///{
        ///"business_name":"麦当劳",
        ///"branch_name":"赤岗店",
        ///"province":"广东省",
        ///"city":"广州市",
        ///"district":"海珠区",
        ///"address":"中国广东省广州市海珠区艺苑路11号",
        ///"telephone":"020-89772059",
        ///"category":"房产小区",
        ///"longitude":"115.32375",
        ///"latitude":"25.097486"
        ///},
        ///{
        ///"business_name":"麦当劳",
        ///"branch_name":"珠江店",
        ///"province":"广东省",
        ///"city":"广州市",
        ///"district":"海珠区",
        ///"address":"中国广东省广州市海珠区艺苑路12号",
        ///"telephone":"020-89772059",
        ///"category":"房产小区",
        ///"longitude":"113.32375",
        ///"latitude":"23.097486"
        ///}]}</param>
        /// <returns>
        /// {
        /// "errcode":0,
        /// "errmsg":"ok",
        /// "location_id_list":[271262077,-1]
        ///}
        ///其中location_id_list中的 -1 表示失败
        /// </returns>
        public static dynamic BatchAddLocation(string access_token, dynamic location_list)
        {
            var url = string.Format("https://api.weixin.qq.com/card/location/batchadd?access_token={0}", access_token);
            var client = new HttpClient();
            var result = client.PostAsync(url, new StringContent(DynamicJson.Serialize(location_list))).Result;
            if (!result.IsSuccessStatusCode) return null;
            return DynamicJson.Parse(result.Content.ReadAsStringAsync().Result);
        }
        /// <summary>
        /// 拉取门店列表
        /// 获取在公众平台上申请创建及API导入的门店列表，用于创建卡券
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="offset">偏移量，0开始</param>
        /// <param name="count">拉取数量,为0时默认拉取全部门店。</param>
        /// <returns>
        /// 返回数据示意：
        /// { "errcode":0,
        ///"errmsg":"ok",
        ///"location_list":[
        ///{
        ///"location_id":“493”,
        ///"business_name":"steventaohome",
        ///"phone":"020-12345678",
        ///"address":"广东省广州市番禺区广东省广州市番禺区南浦大道",
        ///"longitude":113.280212402,
        ///"latitude":23.0350666046
        ///},
        ///{
        ///"location_id":“468”,
        ///"business_name":"TIT创意园B4",
        ///"phone":"020-12345678",
        ///"address":"广东省广州市海珠区",
        ///"longitude":113.325248718,
        ///"latitude":23.1008300781
        ///}
        ///],
        ///"count":2
        ///}
        /// </returns>
        public static dynamic BatchGetLocation(string access_token, int offset, int count)
        {
            var url = string.Format("https://api.weixin.qq.com/card/location/batchget??access_token={0}", access_token);
            var client = new HttpClient();
            var sb = new StringBuilder();
            sb.Append("{")
                .Append('"' + "offset" + '"' + ":").Append(offset)
                .Append('"' + "count" + '"' + ":").Append(count)
                .Append("}");
            var result = client.PostAsync(url, new StringContent(sb.ToString())).Result;
            if (!result.IsSuccessStatusCode) return null;
            return DynamicJson.Parse(result.Content.ReadAsStringAsync().Result);
        }

        /// <summary>
        /// 获取颜色列表接口
        /// 获得卡券的最新颜色列表，用于卡券创建。
        /// </summary>
        /// <param name="access_token"></param>
        /// <returns>
        /// 返回数据结果示意：
        /// {
        ///"errcode":0,
        ///"errmsg":"ok",
        ///"colors":[
        ///{"name":"Color010","value":"#55bd47"},
        ///{"name":"Color020","value":"#10ad61"},
        ///{"name":"Color030","value":"#35a4de"},
        ///{"name":"Color040","value":"#3d78da"},
        ///{"name":"Color050","value":"#9058cb"},
        ///{"name":"Color060","value":"#de9c33"},
        ///{"name":"Color070","value":"#ebac16"},
        ///{"name":"Color080","value":"#f9861f"},
        ///{"name":"Color081","value":"#f08500"},
        ///{"name":"Color090","value":"#e75735"},
        ///{"name":"Color100","value":"#d54036"},
        ///{"name":"Color101","value":"#cf3e36"}
        ///]
        ///}</returns>
        public static dynamic GetColors(string access_token)
        {
            var url = string.Format("https://api.weixin.qq.com/card/getcolors?access_token={0}", access_token);
            var client = new HttpClient();
            var result = client.GetAsync(url).Result;
            if (!result.IsSuccessStatusCode) return null;
            return DynamicJson.Parse(result.Content.ReadAsStringAsync().Result);
        }

        /// <summary>
        /// 创建卡券
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="card">
        /// 数据示意：
        /// {"card":{
        /// "card_type":"GROUPON",
        /// "groupon":{
        /// "base_info":{
        /// "logo_url":
        /// "http:\/\/www.supadmin.cn\/uploads\/allimg\/120216\/1_120216214725_1.jpg",
        /// "brand_name":"海底捞",
        /// "code_type":"CODE_TYPE_TEXT",
        /// "title":"132元双人火锅套餐",
        /// "sub_title":"",
        /// "color":"Color010",
        /// "notice":"使用时向服务员出示此券",
        /// "service_phone":"020-88888888",
        /// "description":"不可与其他优惠同享\n如需团购券发票，请在消费时向商户提出\n店内均可
        ///                使用，仅限堂食\n餐前不可打包，餐后未吃完，可打包\n本团购券不限人数，建议2人使用，超过建议人
        ///                数须另收酱料费5元/位\n本单谢绝自带酒水饮料",
        /// "date_info":{
        /// "type":1,
        /// "begin_timestamp":1397577600,
        /// "end_timestamp":1422724261
        /// },
        /// "sku":{
        /// "quantity":50000000
        /// },
        ///  "get_limit":3,
        /// "use_custom_code":false,
        /// "bind_openid":false,
        /// "can_share":true,
        /// "can_give_friend":true,
        /// "location_id_list": [123,12321,345345],
        /// "url_name_type":"URL_NAME_TYPE_RESERVATION",
        /// "custom_url":"http://www.qq.com",
        /// "source":"大众点评"
        ///   },
        /// "deal_detail":"以下锅底2选1（有菌王锅、麻辣锅、大骨锅、番茄锅、清补凉锅、酸菜鱼锅可
        /// 选）：\n大锅1份12元\n小锅2份16元\n以下菜品2选1\n特级肥牛1份30元\n洞庭鮰鱼卷1份
        /// 20元\n其他\n鲜菇猪肉滑1份18元\n金针菇1份16元\n黑木耳1份9元\n娃娃菜1份8元\n冬
        /// 瓜1份6元\n火锅面2个6元\n欢乐畅饮2位12元\n自助酱料2位10元"}
        /// }
        /// }
        /// 具体参数意义，请参见官方文档。
        /// </param>
        /// <returns>
        /// {
        ///"errcode":0,
        ///"errmsg":"ok",
        ///"card_id":"p1Pj9jr90_SQRaVqYI239Ka1erkI"
        /// }</returns>
        public static dynamic CreateCard(string access_token, dynamic card)
        {
            var url = string.Format("https://api.weixin.qq.com/card/create?access_token={0}", access_token);
            var client = new HttpClient();
            var result = client.PostAsync(url, new StringContent(DynamicJson.Serialize(card))).Result;
            if (!result.IsSuccessStatusCode) return null;
            return DynamicJson.Parse(result.Content.ReadAsStringAsync().Result);
        }

        /// <summary>
        /// 为检测跳转外链请求来自微信，会在URL参数里加上签名。
        /// </summary>
        /// <param name="appscret">appscret</param>
        /// <param name="encrypt_code">指定的卡券code码，只能被领一次。</param>
        /// <param name="card_id">创建卡券时获得的卡券ID</param>
        /// <returns></returns>
        public static string SignCustomUrl(string appscret, string encrypt_code, string card_id)
        {
            var stringADict = new Dictionary<string, string>();
            stringADict.Add("appscret", appscret);
            stringADict.Add("card_id", card_id);
            stringADict.Add("encrypt_code", encrypt_code);
            var sb = new StringBuilder();
            foreach (var sA in stringADict.OrderBy(x => x.Key))//参数名ASCII码从小到大排序（字典序）；
            {
                if (string.IsNullOrEmpty(sA.Value)) continue;//参数的值为空不参与签名；
                sb.Append(sA.Key).Append("=").Append(sA.Value).Append("&");
            }
            var string1 = sb.ToString();
            string1 = string1.Remove(string1.Length - 1, 1);
            return Util.Sha1(string1, "UTF-8");//对stringSignTemp进行MD5运算，再将得到的字符串所有字符转换为大写，得到sign值signValue。 
        }


    }
}
