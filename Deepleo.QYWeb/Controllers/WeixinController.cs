using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using Deepleo.Weixin.QYSDK;
using System.Xml;
using Tencent;
using Deepleo.QYWeb.Services;

namespace Deepleo.QYWeb.Controllers
{
    public class WeixinController : Controller
    {
        public WeixinController()
        {

        }

        /// <summary>
        /// 微信后台验证地址（使用Get），微信后台的“接口配置信息”的Url
        /// </summary>
        [HttpGet]
        [ActionName("Index")]
        public ActionResult Get(string msg_signature, string timestamp, string nonce, string echostr)
        {
            var token = WeixinConfig.Token;//微信企业号后台设置的Token
            if (string.IsNullOrEmpty(token)) return Content("请先设置Token！");
            var ent = "";
            if (!BasicAPI.CheckSignature(msg_signature, timestamp, nonce, token, out ent))
            {
                return Content("参数错误！");
            }
            return Content(echostr); //返回随机字符串则表示验证通过
        }

        /// <summary>
        /// 用户发送消息后，微信平台自动Post一个请求到这里，并等待响应XML。
        /// </summary>
        [HttpPost]
        [ActionName("Index")]
        public ActionResult Post(string msg_signature, string timestamp, string nonce, string echostr)
        {
            var token = WeixinConfig.Token;//微信企业号后台设置的Token
            if (string.IsNullOrEmpty(token)) return Content("请先设置Token！");
            var ent = "";
            if (!BasicAPI.CheckSignature(msg_signature, timestamp, nonce, token, out ent))
            {
                return Content("参数错误！");
            }
            WeixinMessage message = null;
            var safeMode = Request.QueryString.Get("encrypt_type") == "aes";
            var wxBizMsgCrypt = new WXBizMsgCrypt(WeixinConfig.Token, WeixinConfig.EncodingAESKey, WeixinConfig.CorpID);
            var sReplyEchoStr = "";
            var verifyResult = wxBizMsgCrypt.VerifyURL(msg_signature, timestamp, nonce, echostr, ref sReplyEchoStr);
            if (verifyResult != 0)
            {
                LogWriter.Default.WriteError(string.Format("failed to Verify URL,return {0} ", verifyResult));
                return Content("failed to Verify URL！");
            }
            using (var streamReader = new StreamReader(Request.InputStream))
            {
                var decryptMsg = string.Empty;
                var msg = streamReader.ReadToEnd();

                #region 解密
                if (safeMode)
                {
                    var ret = wxBizMsgCrypt.DecryptMsg(msg_signature, timestamp, nonce, msg, ref decryptMsg);
                    if (ret != 0)//解密失败
                    {
                        //TODO：开发者解密失败的业务处理逻辑
                        //注意：本demo用log4net记录此信息，你可以用其他方法
                        LogWriter.Default.WriteError(string.Format("decrypt message return {0}, request body {1}", ret, msg));
                    }
                }
                else
                {
                    decryptMsg = msg;
                }
                #endregion

                message = AcceptMessageAPI.Parse(decryptMsg);
            }
            var response = new WeixinExecutor().Execute(message);
            var encryptMsg = string.Empty;

            #region 加密
            if (safeMode)
            {
                var ret = wxBizMsgCrypt.EncryptMsg(response, timestamp, nonce, ref encryptMsg);
                if (ret != 0)//加密失败
                {
                    //TODO：开发者加密失败的业务处理逻辑
                    LogWriter.Default.WriteError(string.Format("encrypt message return {0}, response body {1}", ret, response));
                }
            }
            else
            {
                encryptMsg = response;
            }
            #endregion

            return new ContentResult
            {
                Content = encryptMsg,
                ContentType = "text/xml",
                ContentEncoding = System.Text.UTF8Encoding.UTF8
            };
        }

    }
}
