using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Deepleo.Weixin.SDK
{
    /// <summary>
    /// 模板消息接口
    /// http://mp.weixin.qq.com/wiki/index.php?title=%E6%A8%A1%E6%9D%BF%E6%B6%88%E6%81%AF%E6%8E%A5%E5%8F%A3
    /// 模板消息仅用于公众号向用户发送重要的服务通知，只能用于符合其要求的服务场景中，如信用卡刷卡通知，商品购买成功通知等。不支持广告等营销类消息以及其它所有可能对用户造成骚扰的消息。
    ///关于使用规则，请注意：
    ///1、所有服务号都可以在功能->添加功能插件处看到申请模板消息功能的入口，但只有认证后的服务号才可以申请模板消息的使用权限并获得该权限；
    ///2、需要选择公众账号服务所处的2个行业，每月可更改1次所选行业；
    ///3、在所选择行业的模板库中选用已有的模板进行调用；
    ///4、每个账号可以同时使用10个模板。
    ///5、当前每个模板的日调用上限为10000次。
    /// </summary>
    public class TemplateMessageAPI
    {

    }
}
