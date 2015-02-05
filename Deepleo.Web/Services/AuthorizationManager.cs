using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Deepleo.Web.Services
{
    public class AuthorizationManager
    {
        public static void SetTicket(bool remeberMe, int version, string identity, string displayName)
        {
            FormsAuthentication.SetAuthCookie(identity, remeberMe);
            if (string.IsNullOrEmpty(displayName))//displayName为空会导致cookies写入失败
            {
                displayName = "匿名";
            }
            var authTicket = new FormsAuthenticationTicket(
                version,
                identity,
                DateTime.Now,
                DateTime.Now.AddDays(remeberMe ? 30 : 1),
                remeberMe,
                displayName);
            string encrytedTicket = FormsAuthentication.Encrypt(authTicket);
            HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrytedTicket);
            HttpContext.Current.Response.Cookies.Add(authCookie);
        }
    }
}