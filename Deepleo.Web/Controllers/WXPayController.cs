using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using Deepleo.Weixin.SDK.Helpers;
using System.Collections.Specialized;
using Deepleo.Web.Services;
using Deepleo.Weixin.SDK.Pay;
using System.IO;
using Codeplex.Data;
using Deepleo.Web.Attribute;
using Deepleo.Web.Models;

namespace Deepleo.Web.Controllers
{
    [WeixinOAuthAuthorize]
    /// <summary>
    /// 微信支付
    /// </summary>
    public class WXPayController : Controller
    {
        public ActionResult Index()
        {
            try
            {
                var out_trade_no = Guid.NewGuid().ToString("N");
                var domain = System.Configuration.ConfigurationManager.AppSettings["Domain"];
                var body = "商品描述";
                var detail = "商品详情";
                var attach = "";
                var product_id = "1";
                var openid = User.Identity.Name;
                var goods_tag = "";
                var fee_type = "CNY";
                var total_fee = 100;
                var trade_type = "JSAPI";
                var appId = System.Configuration.ConfigurationManager.AppSettings["AppId"];
                var nonceStr = Util.CreateNonce_str();
                var timestamp = Util.CreateTimestamp();
                var success_redict_url = string.Format("{0}/WxPay/Success", domain);
                var url = domain + Request.Url.PathAndQuery;
                var userAgent = Request.UserAgent;
                var userVersion = userAgent.Substring(userAgent.LastIndexOf("MicroMessenger/") + 15, 3);//微信版本号高于或者等于5.0才支持微信支付
                var spbill_create_ip = (trade_type == "APP" || trade_type == "NATIVE") ? Request.UserHostName : WeixinConfig.spbill_create_ip;
                var time_start = DateTime.Now.ToString("yyyyMMddHHmmss");
                var time_expire = DateTime.Now.AddHours(1).ToString("yyyyMMddHHmmss");//默认1个小时订单过期，开发者可自定义其他超时机制，原则上微信订单超时时间不超过2小时

                var notify_url = string.Format("{0}/WeixinPay/Notify", domain);//与下面的Notify对应，开发者可自定义其他url地址
                var partnerKey = WeixinConfig.PartnerKey;
                var mch_id = WeixinConfig.mch_id;
                var device_info = WeixinConfig.device_info;
                var returnMsg = "";
                var returnXml = "";
                var content = WxPayAPI.UnifiedOrder(
                              appId, mch_id, device_info, nonceStr,
                              body, detail, attach, out_trade_no, fee_type, total_fee, spbill_create_ip, time_start, time_expire,
                              goods_tag, notify_url, trade_type, product_id, openid, partnerKey, out returnMsg, out returnXml);
                var prepay_id = "";
                var sign = "";
                var return_code = "";
                var return_msg = "";
                var err_code = "";
                var err_code_des = "";
                var isUnifiedOrderSuccess = false;
                if (content.return_code.Value == "SUCCESS")
                {
                    sign = content.sign.Value;
                    return_code = content.result_code.Value;
                    return_msg = "";
                    if (content.result_code.Value == "SUCCESS")
                    {
                        prepay_id = content.prepay_id.Value;
                        sign = WxPayAPI.SignPay(appId, timestamp.ToString(), nonceStr, prepay_id, partnerKey);
                        trade_type = content.trade_type.Value;

                        LogWriter.Default.WriteError(string.Format("{0}'s prepay_id is ", out_trade_no, prepay_id));

                        isUnifiedOrderSuccess = true;
                    }
                    else
                    {
                        err_code = content.err_code.Value;
                        err_code_des = content.err_code_des.Value;
                    }
                }
                else
                {
                    return_code = content.return_code.Value;
                    return_msg = content.return_msg.Value;
                }
                if (!isUnifiedOrderSuccess)
                {
                    return RedirectToAction("Failed", new { msg = "(代码:101)服务器下单失败，请重试" });
                }
                var model = new JSPayModel
                {
                    appId = appId,
                    nonceStr = nonceStr,
                    timestamp = timestamp,
                    userAgent = userAgent,
                    userVersion = userVersion,
                    attach = "",
                    body = body,
                    detail = detail,
                    openid = openid,
                    product_id = out_trade_no,
                    goods_tag = goods_tag,
                    price = total_fee,
                    total_fee = total_fee,
                    prepay_id = prepay_id,
                    trade_type = trade_type,
                    sign = sign,
                    return_code = return_code,
                    return_msg = return_msg,
                    err_code = err_code,
                    err_code_des = err_code_des,
                    success_redict_url = success_redict_url
                };
                return View(model);
            }
            catch (Exception ex)
            {
                LogWriter.Default.WriteError(ex.Message);
                return RedirectToAction("Failed", new { msg = "(代码:200)" + ex.Message });
            }
        }
        public ActionResult Success()
        {
            return View();
        }

        public ActionResult Failed(string msg)
        {
            ViewBag.msg = msg;
            return View();
        }

        /// <summary>
        /// 公共API => 支付结果通用通知
        /// http://pay.weixin.qq.com/wiki/doc/api/index.php?chapter=9_7
        /// 微信支付回调,不需要证书 
        /// 
        /// 应用场景 
        /// 支付完成后，微信会把相关支付结果和用户信息发送给商户，商户需要接收处理，并返回应答。 
        /// 对后台通知交互时，如果微信收到商户的应答不是成功或超时，微信认为通知失败，微信会通过一定的策略（如30分钟共8次）定期重新发起通知，尽可能提高通知的成功率，但微信不保证通知最终能成功。 
        /// 由于存在重新发送后台通知的情况，因此同样的通知可能会多次发送给商户系统。商户系统必须能够正确处理重复的通知。 
        /// 推荐的做法是，当收到通知进行处理时，首先检查对应业务数据的状态，判断该通知是否已经处理过，如果没有处理过再进行处理，如果处理过直接返回结果成功。在对业务数据进行状态检查和处理之前，要采用数据锁进行并发控制，以避免函数重入造成的数据混乱。 
        /// 技术人员可登进微信商户后台扫描加入接口报警群。 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Notify()
        {
            var form = Request.QueryString;
            var sPara = GetRequestPost(form);
            if (sPara.Count <= 0)
            {
                throw new ArgumentNullException();
            }
            LogWriter.Default.WriteInfo(form.ToString());//记录请求关键信息到日志中去
            if (sPara["return_code"] == "SUCCESS" && sPara["return_code"] == "SUCCESS")
            {
                var sign_type = sPara["sign_type"];
                var sign = sPara["sign"];
                var signValue = WxPayAPI.Sign(sPara, WeixinConfig.PartnerKey);
                bool isVerify = sign == signValue;
                if (isVerify)
                {
                    var out_trade_no = sPara["out_trade_no"];

                    //TODO 商户处理订单逻辑： 1.注意交易单不要重复处理；2.注意判断返回金额


                    //TODO:postData中携带该次支付的用户相关信息，这将便于商家拿到openid，以便后续提供更好的售后服务，譬如：微信公众好通知用户付款成功。如果不提供服务则可以删除此代码
                    var openid = sPara["openid"];


                    return Content("success");
                }
                return Content("fail");
            }
            else
            {
                return Content("fail");
            }
        }

        /// <summary>
        /// 微信退款完成后的回调
        /// </summary>
        /// <returns></returns>
        public ActionResult Refund()
        {
            var requestContent = "";
            using (StreamReader sr = new StreamReader(Request.InputStream))
            {
                requestContent = sr.ReadToEnd();
            }
            LogWriter.Default.WriteInfo(requestContent);//记录请求关键信息到日志中去
            bool isVerify = false;
            var sPara = GetRequestPostByXml(requestContent);
            if (sPara.Count <= 0)
            {
                throw new ArgumentNullException();
            }
            var sign = sPara["sign"];
            var retcode = sPara["retcode"];
            if (retcode != "0")
            {
                isVerify = false;
            }
            else
            {
                var signValue = WxPayAPI.Sign(sPara, WeixinConfig.PartnerKey);
                isVerify = sign == signValue;
            }
            if (!isVerify)
            {
                return Content("fail");
            }
            //TODO:商户处理订单逻辑

            return Content("success");

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="xmlString"></param>
        /// <returns></returns>
        private Dictionary<string, string> GetRequestPostByXml(string xmlString)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            System.Xml.XmlDocument document = new System.Xml.XmlDocument();
            document.LoadXml(xmlString);

            var nodes = document.ChildNodes[1].ChildNodes;

            foreach (System.Xml.XmlNode item in nodes)
            {
                dic.Add(item.Name, item.InnerText);
            }
            return dic;
        }

        /// <summary>
        /// 并以“参数名=参数值”的形式组成数组
        /// </summary>
        /// <returns>request回来的信息组成的数组</returns>
        private SortedDictionary<string, string> GetRequestPost(NameValueCollection form)
        {
            int i = 0;
            SortedDictionary<string, string> sArray = new SortedDictionary<string, string>();

            // Get names of all forms into a string array.
            String[] requestItem = form.AllKeys;

            for (i = 0; i < requestItem.Length; i++)
            {
                sArray.Add(requestItem[i], HttpUtility.UrlDecode(form[requestItem[i]], Encoding.UTF8));
            }

            return sArray;
        }
    }
}
