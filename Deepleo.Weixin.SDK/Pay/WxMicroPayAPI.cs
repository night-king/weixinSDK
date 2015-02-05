using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;

namespace Deepleo.Weixin.SDK.Pay
{
    /// <summary>
    /// 微信支付 => 被扫支付 
    /// http://pay.weixin.qq.com/wiki/doc/api/index.php?chapter=5_1
    /// </summary>
    public class WxMicroPayAPI
    {

        /// <summary>
        /// 提交被扫支付API
        /// http://pay.weixin.qq.com/wiki/doc/api/index.php?chapter=5_5
        /// 1.应用场景:收银员使用扫码设备读取微信用户刷卡授权码以后，二维码或条码信息传送至商户收银台，由商户收银台或者商户后台调用该接口发起支付。
        ///是否需要证书:不需要。
        /// <param name="appid">(必填) String(32) 微信分配的公众账号ID</param>
        /// <param name="mch_id">(必填) String(32) 微信支付分配的商户号</param>
        /// <param name="device_info"> String(32) 微信支付分配的终端设备号，商户自定义</param>
        /// <param name="nonce_str">(必填) String(32) 随机字符串,不长于32位</param>
        /// <param name="body">(必填) String(32) 商品描述 商品或支付单简要描</param>
        /// <param name="detail"> String(8192) 商品详情  商品名称明细列表</param>
        /// <param name="attach"> String(127) 附加数据，在查询API和支付通知中原样返回，该字段主要用于商户携带订单的自定义数据</param>
        /// <param name="out_trade_no">(必填) String(32) 商家订单ID,32个字符内、可包含字母, 其他说明见第4.2节商户订单号:http://pay.weixin.qq.com/wiki/doc/api/index.php?chapter=4_2 </param>
        /// <param name="fee_type">符合ISO 4217标准的三位字母代码，默认人民币：CNY，其他值列表详见第4.2节货币类型: http://pay.weixin.qq.com/wiki/doc/api/index.php?chapter=4_2 </param>
        /// <param name="total_fee">(必填) Int 订单总金额，只能为整数，详见第4.2节支付金额:http://pay.weixin.qq.com/wiki/doc/api/index.php?chapter=4_2 </param>
        /// <param name="spbill_create_ip">(必填) String(32)终端IP APP和网页支付提交用户端IP，Native支付填调用微信支付API的机器IP。</param>
        /// <param name="time_start">String(14) 订单生成时间，格式为yyyyMMddHHmmss，如2009年12月25日9点10分10秒表示为20091225091010。</param>
        /// <param name="time_expire">String(14) 订单失效时间，格式为yyyyMMddHHmmss，如2009年12月27日9点10分10秒表示为20091227091010。</param>
        /// <param name="goods_tag">String(32) 商品标记，代金券或立减优惠功能的参数，说明详见第10节代金券或立减优惠：http://pay.weixin.qq.com/wiki/doc/api/index.php?chapter=10_1 </param>
        /// <param name="auth_code">String(128) 授权码 扫码支付授权码，设备读取用户微信中的条码或者二维码信息 </param>
        /// <param name="partnerKey">API密钥</param>
        /// <returns>返回json字符串，格式参见：http://pay.weixin.qq.com/wiki/doc/api/index.php?chapter=5_5 </returns>
        public static dynamic Submit(string appid, string mch_id, string device_info,
                                     string nonce_str, string body, string detail, string attach,
                                     string out_trade_no, string fee_type, int total_fee, string spbill_create_ip,
                                     string time_start, string time_expire, string goods_tag,
                                     string auth_code,
                                     string partnerKey)
        {
            var stringADict = new Dictionary<string, string>();
            stringADict.Add("appid", appid);
            stringADict.Add("mch_id", mch_id);
            stringADict.Add("device_info", device_info);
            stringADict.Add("nonce_str", nonce_str);
            stringADict.Add("body", body);
            stringADict.Add("attach", attach);
            stringADict.Add("out_trade_no", out_trade_no);
            stringADict.Add("fee_type", fee_type);
            stringADict.Add("total_fee", total_fee.ToString());
            stringADict.Add("spbill_create_ip", spbill_create_ip);
            stringADict.Add("time_start", time_start);
            stringADict.Add("time_expire", time_expire);
            stringADict.Add("goods_tag", goods_tag);
            stringADict.Add("auth_code", auth_code);
            var sign = WxPayAPI.Sign(stringADict, partnerKey);//生成签名字符串
            var postdata = PayUtil.GeneralPostdata(stringADict, sign);
            var url = "https://api.mch.weixin.qq.com/pay/micropay";
            var client = new HttpClient();
            var result = client.PostAsync(url, new StringContent(postdata)).Result;
            if (!result.IsSuccessStatusCode) return string.Empty;
            return new DynamicXml(result.Content.ReadAsStringAsync().Result);
        }

        /// <summary>
        /// 撤销支付API
        /// http://pay.weixin.qq.com/wiki/doc/api/index.php?chapter=5_6
        /// 应用场景:支付交易返回失败或支付系统超时，调用该接口撤销交易。如果此订单用户支付失败，微信支付系统会将此订单关闭；如果用户支付成功，微信支付系统会将此订单资金退还给用户。
        /// 注意：7天以内的交易单可调用撤销，其他正常支付的单如需实现相同功能请调用申请退款API。提交支付交易后调用【查询订单API】，没有明确的支付结果再调用【撤销订单API】。
        /// 请求需要双向证书
        /// </summary>
        /// <param name="appid">(必填) String(32) 微信分配的公众账号ID</param>
        /// <param name="mch_id">(必填) String(32) 微信支付分配的商户号</param>
        /// <param name="transaction_id"> String(32) 微信订单号 优先使用</param>
        /// <param name="out_trade_no">String(32) 商户订单号 </param>
        /// <param name="nonce_str">(必填) String(32) 随机字符串,不长于32位</param>
        /// <param name="partnerKey">API密钥</param>
        /// <returns></returns>
        public static dynamic ReverseSubmit(string appid, string mch_id, string transaction_id,
                                  string out_trade_no, string nonce_str,
                                  string partnerKey)
        {
            var stringADict = new Dictionary<string, string>();
            stringADict.Add("appid", appid);
            stringADict.Add("mch_id", mch_id);
            stringADict.Add("transaction_id", transaction_id);
            stringADict.Add("out_trade_no", out_trade_no);
            stringADict.Add("nonce_str", nonce_str);
            var sign = WxPayAPI.Sign(stringADict, partnerKey);//生成签名字符串
            var postdata = PayUtil.GeneralPostdata(stringADict, sign);
            var url = "https://api.mch.weixin.qq.com/secapi/pay/reverse";
            var client = new HttpClient();
            var result = client.PostAsync(url, new StringContent(postdata)).Result;
            if (!result.IsSuccessStatusCode) return string.Empty;
            return new DynamicXml(result.Content.ReadAsStringAsync().Result);
        }
    }
}
