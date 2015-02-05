using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Deepleo.Weixin.SDK.Helpers;

namespace Deepleo.Weixin.SDK.Pay
{
    public class PayUtil
    {
        /// <summary>
        /// 生成POST的xml数据字符串
        /// </summary>
        /// <param name="postdataDict">>参与生成的参数列表</param>
        /// <param name="sign">签名</param>
        /// <returns></returns>
        public static string GeneralPostdata(IDictionary<string, string> postdataDict, string sign)
        {
            var sb2 = new StringBuilder();
            sb2.Append("<xml>");
            foreach (var sA in postdataDict.OrderBy(x => x.Key))//参数名ASCII码从小到大排序（字典序）；
            {
                sb2.Append("<" + sA.Key + ">")
                   .Append(Util.Transfer(sA.Value))//参数值用XML转义即可，CDATA标签用于说明数据不被XML解析器解析。 
                   .Append("</" + sA.Key + ">");
            }
            sb2.Append("<sign>").Append(sign).Append("</sign>");
            sb2.Append("</xml>");
            return sb2.ToString();
        }

        /// <summary>
        /// 将ErrorCode翻译成文字
        /// </summary>
        /// <param name="errorcode">err_code</param>
        /// <returns></returns>
        public static string ExplainErrorcode(string err_code)
        {
            switch (err_code)
            {
                case "NOAUTH":
                    return "商户无此接口权限";
                case "NOTENOUGH":
                    return "余额不足";
                case "ORDERPAID":
                    return "商户订单已支付";
                case "ORDERCLOSED":
                    return "订单已关闭";
                case "ORDERREVERSED":
                    return "订单已撤销";

                case "BANKERROR":
                    return "银行系统异常";
                case "USERPAYING":
                    return "用户支付中，需要输入密码";
                case "AUTH_CODE_INVALID":
                    return "授权码检验错误";
                case "BUYER_MISMATCH":
                    return "支付帐号错误";
                case "AUTHCODEEXPIRE":
                    return "二维码已过期，请用户在微信上刷新后再试";

                case "NOTSUPORTCARD":
                    return "不支持卡类型";
                case "SYSTEMERROR":
                    return "系统错误";
                case "APPID_NOT_EXIST":
                    return "APPID不存在";
                case "MCHID_NOT_EXIST":
                    return "MCHID不存在";
                case "APPID_MCHID_NOT_MATCH":
                    return "appid和mch_id不匹配";

                case "LACK_PARAMS":
                    return "缺少参数";
                case "OUT_TRADE_NO_USED":
                    return "商户订单号重复";
                case "SIGNERROR":
                    return "签名错误";
                case "XML_FORMAT_ERROR":
                    return "XML格式错误";
                case "REQUIRE_POST_METHOD":
                    return "请使用post方法";

                case "POST_DATA_EMPTY":
                    return "post数据为空";
                case "NOT_UTF8":
                    return "编码格式错误";
                case "ORDERNOTEXIST":
                    return "查询系统中不存在此交易订单号";
                case "INVALID_TRANSACTIONID":
                    return "无效transaction_id";
                case "PARAM_ERROR":
                    return "参数错误";

                default:
                    return err_code;
            }
        }
    }
}
