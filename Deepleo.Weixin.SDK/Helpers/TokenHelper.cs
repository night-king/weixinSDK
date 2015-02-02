using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using Deepleo.Weixin.SDK;
using Deepleo.Weixin.SDK.JSSDK;

namespace Deepleo.Weixin.SDK.Helpers
{
    /// <summary>
    /// 由于微信AccessToken，7200s过期，故需要在7200s后重新获取新的有效Token。
    /// ps:每次都重新生成新的Token会比较慢（大概需要500ms）。
    /// </summary>
    public class TokenHelper
    {
        public int Interval { private set; get; }
        public bool Status { private set; get; }
        public DateTime LastRunDateTime { private set; get; }
        public string AppId { private set; get; }
        public string AppSecrect { private set; get; }
        public bool IsOpenJSTickect { private set; get; }

        /// <summary>
        /// 建议小于7200s,比如6000s
        /// </summary>
        /// <param name="IntervalInSeconds"></param>
        /// <param name="appId"></param>
        /// <param name="appSecrect"></param>
        /// <param name="isOpenJSTickect">是否打开JSSDK生成Tickect功能
        /// false:无法通过GetJSTickect获取JSTickect
        /// </param>
        public TokenHelper(int IntervalInSeconds, string appId, string appSecrect, bool isOpenJSTickect = false)
        {
            Interval = IntervalInSeconds;
            AppId = appId;
            AppSecrect = appSecrect;
            Status = false;
            LastRunDateTime = DateTime.MinValue;
            IsOpenJSTickect = isOpenJSTickect;
        }

        public void Run()
        {
            if (Status) throw new Exception(string.Format("Token Manager is already running."));
            Status = true;
            refreshToken();
            _timer = new System.Timers.Timer(Interval * 1000);
            _timer.Elapsed += delegate
            {
                refreshToken();
                if (IsOpenJSTickect)
                {
                    refreshJSTickect();
                }
            };
            _timer.Start();
        }
        public void Close()
        {
            if (!Status) throw new Exception(string.Format("Token Manager is already closed."));
            _timer.Stop();
            _timer.Dispose();
            _timer = null;
            Status = false;
        }

        private string _token;
        private string _jsTickect;

        private System.Timers.Timer _timer = null;
        public event EventHandler<EventArgs> TokenChangedEvent;
        public event EventHandler<EventArgs> JSTickectChangedEvent;
        public event EventHandler<ThreadExceptionEventArgs> ErrorEvent;

        /// <summary>
        /// 获取access_token
        /// 刷新access_token后，JSTickect会自动刷新
        /// </summary>
        /// <param name="isForceRefresh">是否强制刷新，建议不要频繁刷新</param>
        /// <returns></returns>
        public string GetToken(bool isForceRefresh = false)
        {
            if (isForceRefresh)
            {
                refreshToken();
                if (IsOpenJSTickect) refreshJSTickect();
            }
            return _token;
        }

        /// <summary>
        /// 使用JSSDK权限签名算法jsapi_ticket
        /// </summary>
        /// <param name="isForceRefresh">是否强制刷新，建议不要频繁刷新</param>
        /// <returns></returns>
        public string GetJSTickect(bool isForceRefresh = false)
        {
            if (!IsOpenJSTickect) return string.Empty;
            if (string.IsNullOrEmpty(_token)) refreshToken();
            if (string.IsNullOrEmpty(_jsTickect)) refreshJSTickect();
            else if (isForceRefresh) refreshJSTickect();
            return _jsTickect;
        }

        private void refreshToken()
        {
            if (!Status) return;
            LastRunDateTime = DateTime.Now;
            try
            {
                string newToken = BasicAPI.GetAccessToken(AppId, AppSecrect).access_token;
                _token = newToken;
                if (TokenChangedEvent != null) TokenChangedEvent(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                if (ErrorEvent != null) ErrorEvent(this, new ThreadExceptionEventArgs(ex));
            }
        }

        private void refreshJSTickect()
        {
            if (!Status) return;
            LastRunDateTime = DateTime.Now;
            try
            {
                string newTickect = JSAPI.GetTickect(_token).ticket;
                _jsTickect = newTickect;
                if (JSTickectChangedEvent != null) JSTickectChangedEvent(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                if (ErrorEvent != null) ErrorEvent(this, new ThreadExceptionEventArgs(ex));
            }

        }
    }
}