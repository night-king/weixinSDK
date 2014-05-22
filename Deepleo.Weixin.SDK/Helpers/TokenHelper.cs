using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using Deepleo.Weixin.SDK;

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="IntervalInSeconds">建议小于7200s,比如3600s</param>
        /// <param name="appId"></param>
        /// <param name="appSecrect"></param>
        public TokenHelper(int IntervalInSeconds, string appId, string appSecrect)
        {
            Interval = IntervalInSeconds;
            AppId = appId;
            AppSecrect = appSecrect;
            Status = false;
            LastRunDateTime = DateTime.MinValue;
        }
        public void Run()
        {
            if (Status) throw new Exception(string.Format("Token Manager is already running."));
            Status = true;
            refreshToken();
            _timer = new System.Timers.Timer(Interval*1000);
            _timer.Elapsed += delegate
            {
                refreshToken();
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
        private System.Timers.Timer _timer = null;
        public event EventHandler<EventArgs> TokenChangedEvent;
        public event EventHandler<ThreadExceptionEventArgs> ErrorEvent;
        public string GetToken(bool isRefresh)
        {
            if (isRefresh) refreshToken();
            return _token;
        }

        private void refreshToken()
        {
            if (!Status) return;
            LastRunDateTime = DateTime.Now;
            try
            {
                string newToken = BasicAPI.GetAccessToken(AppId, AppSecrect).access_token;
                _token=newToken;
            }
            catch(Exception ex) 
            {
                if (ErrorEvent != null) ErrorEvent(this, new ThreadExceptionEventArgs(ex));
            }
            if (TokenChangedEvent != null) TokenChangedEvent(this, EventArgs.Empty);
        }
    }
}