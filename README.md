WeixinSDK.net使用帮助

1.使用对象
本API目前只针对微信公众平台开发者文档所有API进行包装：http://mp.weixin.qq.com/wiki/home/index.html
不支持微信企业号和移动端API.
也就是微信服务号和订阅号的开发。
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
(3)BasicAPI.cs                                         对应微信API的 "基础支持"
(4)CustomMenuAPI.cs                            对应微信API的 "自定义菜单管理"
(5)InterfaceStatisticsAPI.cs                       对应微信API的 "数据统计接口" =>"接口分析数据接口"

(6)MessageStatisticsAPI.cs                        对应微信API的 "数据统计接口" =>"消息分析数据接口"
(7)MutliServiceAPI.cs                              对应微信API的 "多客服功能"
(8)NewsStatisticsAPI.cs                            对应微信API的 "数据统计接口" =>"图文分析数据接口"
(9)PromotionAPI.cs                                 对应微信API的 "帐号管理"
(10)ReplayActiveMessageAPI.cs               对应微信API的 "发送消息”=》"客服接口"=>"发送(主动)客服消息"

(11)ReplayPassiveMessageAPI.cs              对应微信API的 "发送消息”=》"被动回复消息"
(12)ServiceAdminAPI.cs                           对应微信API的 "多客服功能"=>"客服管理"
(13)SmartAPI.cs                                       对应微信API的 "智能接口"
(14)TemplateMessageAPI.cs                     对应微信API的 "发送消息”=》"模板消息接口"
(15)UserAdminAPI.cs                              对应微信API的 "用户管理"
    注意：获取用户地理位置在接受Location事件时获取，解析地址位置数据包在AcceptMessageAPI.Parse(string message)方法中，详细见:(2)AcceptMessageAPI.cs 。
                网页授权获取用户基本信息和获取用户网络状态（JS）接口暂时不包含在本SDK中。
(16)UserStatisticsAPI.cs                          对应微信API的 "数据统计接口" =>"用户分析数据接口"

微信支付、微信JS接口、微信小店接口、微信卡券、设备功能接口暂未开发。

5.问题帮助
如果开发者遇到开发问题或者遇到SDK的bug，请到
1）官方QQ群：173564082 
2）论坛：http://www.weixinsdk.net/指定板块交流解答
3）作者QQ：2586662969

6.注意事项
1）由于本人时间和经历有限，SDK并未对调用参数做严格验证，也没有对API返回内容做过分解析，所以请开发者在开发过程中还是您需要参阅官方文档的详细描述。
2）微信现在还不是很成熟，如果遇到调用失败的问题，请查看官方公告或者多试几次。
3）微信公众平台可以申请测试号，如果您的产品已经上线，但是发布新功能还没有在生产环境中验证过，申请测试号是不错的选择。
4）微信自带开发者问答系统，上面都是前车之鉴，有一定参考价值。

7.源代码托管：https://github.com/night-king/weixinSDK
