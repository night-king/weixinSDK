using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;

namespace Deepleo.Weixin.SDK.Pay
{
    /// <summary>
    /// 微信支付 => 扫码原生支付 
    /// http://pay.weixin.qq.com/wiki/doc/api/index.php?chapter=6_1
    /// 开发前，商户必须在公众平台后台设置支付回调URL。URL实现的功能：接收用户扫码后微信支付系统回调的productid和openid
    /// </summary>
    public class WxBizPayAPI
    {

        /// <summary>
        /// 提交被扫支付API
        /// http://pay.weixin.qq.com/wiki/doc/api/index.php?chapter=6_4
        /// 应用场景:　用户扫描商户展示在各种场景的二维码进行支付。
        /// <param name="appid">(必填) String(32) 微信分配的公众账号ID</param>
        /// <param name="mch_id">(必填) String(32) 微信支付分配的商户号</param>
        /// <param name="time_stamp"> String(32) 微信支付分配的终端设备号，商户自定义</param>
        /// <param name="nonce_str">(必填) String(32) 随机字符串,不长于32位</param>
        /// <param name="product_id">(必填) String(32) 商品描述 商品或支付单简要描</param>
        /// <param name="partnerKey">API密钥</param>
        /// <returns>返回支付链接，开发者需要自己生成相应的二维码展示给用户 </returns>
        public static string Mode1(string appid, string mch_id, string time_stamp,
                                     string nonce_str, string product_id,
                                     string partnerKey)
        {
            var stringADict = new Dictionary<string, string>();
            stringADict.Add("appid", appid);
            stringADict.Add("mch_id", mch_id);
            stringADict.Add("time_stamp", time_stamp);
            stringADict.Add("nonce_str", nonce_str);
            stringADict.Add("product_id", product_id);
            var sign = WxPayAPI.Sign(stringADict, partnerKey);//生成签名字符串
            return string.Format("weixin://wxpay/bizpayurl?sign={0}&appid={1}&mch_id={2}&product_id={3}&time_stamp={4}&nonce_str={5}"
                                , sign, appid, mch_id, product_id, time_stamp, nonce_str
                                );
        }

        /// <summary>
        /// 提交被扫支付API
        /// http://pay.weixin.qq.com/wiki/doc/api/index.php?chapter=6_4
        /// 模式二与模式一相比，流程更为简单，不依赖设置的回调支付URL。
        /// 商户后台系统先调用微信支付的统一下单接口，微信后台系统返回链接参数code_url，商户后台系统将code_url值生成二维码图片，用户使用微信客户端扫码后发起支付。
        /// 注意：code_url有效期为2小时，过期后扫码不能再发起支付。
        /// <param name="code_url">(必填) 商户后台系统先调用微信支付的统一下单接口，微信后台系统返回链接参数code_url</param>
        /// <returns>返回支付链接，开发者需要自己生成相应的二维码展示给用户 </returns>
        public static string Mode2(string code_url)
        {
            return string.Format("weixin://wxpay/bizpayurl?sr={0}" , code_url);
        }
    }
}
