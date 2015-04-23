/*--------------------------------------------------------------------------
* WeixinExecutor.cs
 *Auth:deepleo
* Date:2013.12.31
* Email:2586662969@qq.com
*--------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Deepleo.Weixin.QYSDK;
using System.Text;
using System.Text.RegularExpressions;
using Deepleo.Weixin.QYSDK.Helpers;
using Deepleo.Weixin.QYSDK.Entities;
using Deepleo.QYWeb.Services;

namespace Deepleo.QYWeb
{
    public class WeixinExecutor : IWeixinExecutor
    {
        public WeixinExecutor()
        {
        }

        /// <summary>
        /// 说明：带TODO字眼的代码段，需要开发者自行按照自己的业务逻辑实现
        /// </summary>
        /// <param name="message"></param>
        /// <returns>已经打包成xml的用于回复用户的消息包</returns>
        public string Execute(WeixinMessage message)
        {
            var result = "";
            var domain = System.Configuration.ConfigurationManager.AppSettings["Domain"];//请更改成你的域名
            var openId = message.Body.FromUserName.Value;
            var myUserName = message.Body.ToUserName.Value;
            return result;
        }
    }
}