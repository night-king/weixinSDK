using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Deepleo.Web.Services;
using System.Web.Caching;

namespace Deepleo.Web.Attribute
{
    /// <summary>
    /// 微信OAuth
    /// </summary>
    public class WeixinOAuthAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var domain = System.Configuration.ConfigurationManager.AppSettings["Domain"];
            var target_uri = filterContext.RequestContext.HttpContext.Request.Url.PathAndQuery;
            var identity = filterContext.HttpContext.User.Identity;
            if (!identity.IsAuthenticated)
            {
                var userAgent = filterContext.RequestContext.HttpContext.Request.UserAgent;
                var redirect_uri = string.Format("{0}/OAuth/Callback", domain);//这里需要完整url地址，对应Controller里面的OAuthController的Callback
                var scope = "snsapi_userinfo";
                var state = Math.Abs(DateTime.Now.ToBinary()).ToString();//state保证唯一即可,可以用其他方式生成
                //这里为了实现简单，将state和target_uri保存在Cache中，并设置过期时间为2分钟。您可以采用其他方法!!!
                HttpContext.Current.Cache.Add(state, target_uri, null, DateTime.Now.AddMinutes(2), TimeSpan.Zero, CacheItemPriority.Normal, null);
                LogWriter.Default.WriteInfo(string.Format("begin weixin oauth: scope: {0}, redirect_uri: {1} , state: {2} , user agent: {3} ", scope, redirect_uri, state, userAgent));
                var weixinOAuth2Url = string.Format(
                         "https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri={1}&response_type=code&scope={2}&state={3}#wechat_redirect",
                          WeixinConfig.AppID, redirect_uri, scope, state);
                filterContext.Result = new RedirectResult(weixinOAuth2Url);
            }
            else
            {
                base.OnAuthorization(filterContext);
            }
        }
    }
}