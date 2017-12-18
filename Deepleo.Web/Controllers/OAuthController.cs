using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Deepleo.Web.Attribute;
using Deepleo.Weixin.SDK;
using Deepleo.Web.Services;
using System.Threading;
using Deepleo.Web.CommandServiceReference;

namespace Deepleo.Web.Controllers
{
    public class OAuthController : Controller
    {

        [WeixinOAuthAuthorize]
        public ActionResult Index()
        {
            //记录access_token
            var appId = WeixinConfig.AppID;
            var appSecret = WeixinConfig.AppSecret;
            using (CommanderServiceClient client = new CommanderServiceClient())
            {
                var access_token = client.GetAccessToken(appId, appSecret, false);
                LogWriter.Default.WriteError("access_token is: " + access_token);
            }
            return View();
        }

        [HttpGet]
        public ActionResult Callback()
        {
            var code = Request.QueryString.Get("code");
            if (string.IsNullOrEmpty(code))//没有code表示授权失败
            {
                return RedirectToAction("Failed", "OAuth");
            }
            var state = Request.QueryString.Get("state");
            var cache_status = System.Web.HttpContext.Current.Cache.Get(state);
            var redirect_url = cache_status == null ? "/" : cache_status.ToString();//没有获取到state,就跳转到首页
            var access_token_scope = "";
            double expires_in = 0;
            var access_token = "";
            var openId = "";
            var token = OAuth2API.GetAccessToken(code, WeixinConfig.AppID, WeixinConfig.AppSecret);
            dynamic userinfo;

            var refreshAccess_token = OAuth2API.RefreshAccess_token(token.refresh_token, WeixinConfig.AppID);
            access_token = refreshAccess_token.access_token;//通过code换取的是一个特殊的网页授权access_token，与基础支持中的access_token（该access_token用于调用其他接口）不同。
            openId = refreshAccess_token.openid;
            access_token_scope = refreshAccess_token.scope;
            expires_in = refreshAccess_token.expires_in;
            userinfo = OAuth2API.GetUserInfo(access_token, openId);//snsapi_userinfo,可以用户在未关注公众号的情况下获取用户基本信息

            //写入cookies
            AuthorizationManager.SetTicket(true, 1, openId, userinfo.nickname);
            Thread.Sleep(500);//暂停半秒钟，以等待IOS设置Cookies的延迟
            LogWriter.Default.WriteInfo(string.Format("OAuth success: identity: {0} , name: {1} , redirect_rul:{2} , expires_in: {3}s ", openId, userinfo.nickname, redirect_url, expires_in));
            return new RedirectResult(redirect_url, true);
        }

        public ActionResult Failed()
        {
            ViewBag.message = "OAuth失败，您拒绝了授权申请或者公众好号没有此权限.";
            return View();
        }
    }
}
