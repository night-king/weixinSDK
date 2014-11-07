using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Deepleo.Weixin.SDK
{
    /// <summary>
    /// 高级群发消息接口
    /// http://mp.weixin.qq.com/wiki/index.php?title=%E9%AB%98%E7%BA%A7%E7%BE%A4%E5%8F%91%E6%8E%A5%E5%8F%A3
    /// 说明：
    /// 1.订阅号提供了每天一条的群发权限，为服务号提供每月（自然月）4条的群发权限。
    /// 2.对于某些具备开发能力的公众号运营者，可以通过高级群发接口，实现更灵活的群发能力。
    /// 注意：
    /// 1、该接口暂时仅提供给已微信认证的服务号
    /// 2、虽然开发者使用高级群发接口的每日调用限制为100次，但是用户每月只能接收4条，请小心测试
    /// 3、无论在公众平台网站上，还是使用接口群发，用户每月只能接收4条群发消息，多于4条的群发将对该用户发送失败。
    /// 4、具备微信支付权限的公众号，在使用高级群发接口上传、群发图文消息类型时，可使用<a>标签加入外链
    /// </summary>
    public class AdvanceMassReplayMessageAPI
    {

    }
}
