WeixinSDK.net使用帮助

1.使用对象

本API目前只针对微信公众平台开发者文档所有API进行包装：http://mp.weixin.qq.com/wiki/home/index.html

也就是微信服务号和订阅号的开发,不支持微信企业号和移动端API。

但是不排除企业号和移动端API共有部分的调用，对于这部分本SDK不保证其能调用成功。

2.API包装的核心思想

运用Dynamic(.net 4.0以及以上版本支持)在程序中传递微信所需的结构化（xml,json）对象，减少大量代码，实现轻量级。
让您可以像Python一样书写代码。

3.SDK优势

(1)轻量级，摒弃了大量对微信API调用过程中的传递对象的包装，代码至少减少1/2，和同级.net版本的微信SDK相比是最轻量级的。

(2)结构清晰，和官方API列表一一对应，便于开发者快速定位。

(3)简单灵活，只包装官方最底层的，最原始的API，便于开发者自由发挥。

4.API目录说明

(1)AcceptMessageAPI.cs                         对应微信API的 "接收消息"
    注意：验证消息真实性在BasicAPI的CheckSignature中.
                接收消息文字、图片、视频、音频、位置、链接和事件消息都包装在AcceptMessageAPI.Parse(string message)方法中，
                因为他们都可以归为消息，所以只需要提供一个统一的解析方法即可。当然，这个方法返回的是一个动态对象。
                接受语音识别消息，只是在返回的语音消息xml数据中多了Recognition（注：需要公众号有接收语音识别结果的权限）
                
(2)AdvanceMassReplayMessageAPI.cs     对应微信API的 "发送消息”=》"高级群发消息接口"

(3)BasicAPI.cs                                 对应微信API的 "基础支持"

(4)CustomMenuAPI.cs                            对应微信API的 "自定义菜单管理"

(5)InterfaceStatisticsAPI.cs                   对应微信API的 "数据统计接口" =>"接口分析数据接口"

(6)MessageStatisticsAPI.cs                     对应微信API的 "数据统计接口" =>"消息分析数据接口"

(7)MutliServiceAPI.cs                          对应微信API的 "多客服功能"

(8)NewsStatisticsAPI.cs                        对应微信API的 "数据统计接口" =>"图文分析数据接口"

(9)PromotionAPI.cs                             对应微信API的 "帐号管理"

(10)ReplayActiveMessageAPI.cs                  对应微信API的 "发送消息”=》"客服接口"=>"发送(主动)客服消息"

(11)ReplayPassiveMessageAPI.cs                 对应微信API的 "发送消息”=》"被动回复消息"

(12)ServiceAdminAPI.cs                         对应微信API的 "多客服功能"=>"客服管理"

(13)SmartAPI.cs                                对应微信API的 "智能接口"

(14)TemplateMessageAPI.cs                      对应微信API的 "发送消息”=》"模板消息接口"

(15)UserAdminAPI.cs                            对应微信API的 "用户管理"

注意：获取用户地理位置在接受Location事件时获取，解析地址位置数据包在AcceptMessageAPI.Parse(string message)方法中，详细见:(2)AcceptMessageAPI.cs 。
网页授权获取用户基本信息和获取用户网络状态（JS）接口暂时不包含在本SDK中。

(16)UserStatisticsAPI.cs                        对应微信API的 "数据统计接口" =>"用户分析数据接口"

(17)JSSDK/JSAPI.cs                              对应微信API的 "微信JS接口" =>"微信JS-SDK说明文档"   =>"附录1-JS-SDK使用权限签名算法"

(18)微信支付
     
    Pay/WxPayAPI.cs                             对应微信支付API =>公共API

    Pay/WxMicroPayAP.cs                         对应微信支付API =>被扫支付
    
    Pay/WxBizPayAP.cs                           对应微信支付API =>扫码原生支付
    
    官方文档：http://pay.weixin.qq.com/wiki/doc/api/index.php

其中微信内网页支付 Demo已经在SDK中实现，具体请参考：

Deepleo.Web/Controllers/WXPayController.cs

Deepleo.Web/Controllers/JSSDKController.cs/Pay

Deepleo.Web/Views/JSSDK/Pay.cshtml

(19)OAuth2API.cs                                 对应微信API的 用户管理=》 "网页授权获取用户基本信息”

(20)微信卡券

     Card/CreateCardAPI.cs  创建卡券接口
     
     Card/SendCardAPI.cs    卡券投放接口
     
     Card/UseCardAPI.cs     卡券核销接口

     Card/ManageCardAPI.cs  卡券管理接口

     Card/Special           特殊卡票接口
     
     Card/TestWhiteAPI.cs   设置测试用户白名单

(21) 微信小店(<a href="http://mp.weixin.qq.com/wiki/8/703923b7349a607f13fb3100163837f0.html">微信商铺API手册V1.15</a>)
    
     Merchant/ProductAPI.cs  商品管理接口
     
     Merchant/ExpressAPI.cs  邮费模板管理接口
     
     Merchant/StockAPI.cs    库存管理接口
     
     Merchant/GroupAPI.cs    分组管理接口
     
     Merchant/ShelfAPI.cs    货架管理接口
     
     Merchant/OrderAPI.cs    订单管理接口
     
     Merchant/CommonAPI.cs   功能接口

设备功能接口暂未开发。

5.demo
 Deepleo.Web项目，请编译后发布到服务器，并修改web.config(appid,appsecrect,Token,EncodingAESKey)， 在后台将服务地址改为：[域名]/weixin。demo在线演示请关注微信公众号：

(1) 鸣创软件(订阅号所有可以实现的功能演示,)
    <img src="http://weixinsdk.net/data/attachment/forum/201502/02/102815etfqqqfvj9tdtjz4.jpg" style="width:100px; height:100px;"/>

(2) 慢做菜(基于WeixinSDK实现的菜谱查询服务)
     <img src="http://weixinsdk.net/data/attachment/forum/201502/02/102818c29jxbepe2nbjm2n.jpg" style="width:100px; height:100px;"/>

(3) http://weixinsdk.deepleo.com/jssdk (js-sdk演示,需要在微信中打开， 由于本公众号只是未认证订阅号，故请对照后台权限测试)


6.问题帮助

1）API返回的Dynamic对象应该如何使用？

    答：如果您调用API，return的是
   
    a.由DynamicJson.Parse转换而来，您可以用.[属性名称] 访问到该属性的值；
   
    譬如 BasicAPI.cs中GetAccessToken：
   
    var token = DynamicJson.Parse(result.Content.ReadAsStringAsync().Result);
   
    那么调用时：string newToken = BasicAPI.GetAccessToken(AppId, AppSecrect).access_token;
   
    b.如果是DynamicXml转换而来的，您可以用.[属性名称].Value 访问到该属性的值；
   
    譬如 AcceptMessageAPI.cs中Parse:
   
    msg.Body = new DynamicXml(message);
   
    string msgType = msg.Body.MsgType.Value;

2) demo项目发布之前如何配置？

    答：需要修改web.config里appSettings节点下关于网站配置和微信相关配置，如果不需要的功能不用填写。
   
        读取这些配置的类在App_start/WeixinConfig.cs中，您可以根据自己的业务逻辑更改实现方式。
       
        譬如，如果您一个网站需要管理多个微信公众号的情况。

3）遇到其他问题该如何解决?

    答：如果开发者遇到开发问题或者遇到SDK的bug，请到

     a.官方QQ群：173564082 

     b.论坛：http://www.weixinsdk.net/

     c.作者QQ：2586662969

7.注意事项

1）由于本SDK只是简单包装了官方API，API调用有诸多限制（譬如权限限制，次数限制），所以开发者还是需要仔细阅读官方文档。

2）微信现在还不是很成熟，如果遇到调用失败的问题，请查看官方公告或者多试几次。

3）微信公众平台可以申请测试号，如果您的产品已经上线，但是发布新功能还没有在生产环境中验证过，申请测试号是不错的选择。

4）微信自带开发者问答系统，上面都是前车之鉴，有一定参考价值。


8.源代码托管：https://github.com/night-king/weixinSDK


9.Copyright and license

Code and documentation copyright 2011-2015. Code released under the MIT license. Docs released under Creative Commons.

10.建议加入官方QQ群第一时间获取API更新的最新动态：173564082

11.郑重申明

本SDK不收费，坚持开源，没有未公开的api，项目中的代码是我自己开发过的项目积累。如果在使用中遇到什么问题，请到官方QQ群或者论坛中提问，直接加我私人QQ提问的概不做答。如果发现bug，请到https://github.com/night-king/weixinSDK/issues 提出，我会尽快解决，并发布论坛QQ群群邮件、论坛bug栏目、以及QQ群通知。

12.捐助

如果这个项目对您有用，我们欢迎各方任何形式的捐助，也包括参与到项目代码更新或意见反馈中来。谢谢！资金捐助：

  <img src="http://weixinsdk.net/data/attachment/forum/201504/10/143139l7bw4jbtj5317tzd.jpg" style="width:100px; height:100px;"/>

