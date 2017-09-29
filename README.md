WeixinSDK.net使用帮助

1.使用对象

微信公众平台：http://mp.weixin.qq.com/wiki/home/index.html

Deepleo.Weixin.SDK是SDK源代码

Deepleo.Web是一个asp.net mvc的demo  Online: http://demo.weixinsdk.net

2.核心思想

运用Dynamic(.net 4.0以及以上版本支持)在程序中传递微信所需的结构化（xml,json）对象，减少大量代码，实现轻量级。
让您可以像Python一样书写代码。

3.疑难问题

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


2）遇到其他问题该如何解决?

    答：如果开发者遇到开发问题或者遇到SDK的bug，请到

     a.官方QQ群：478753523

     b.官方网站：http://www.weixinsdk.net/

     c.作者QQ：2586662969
     
     d.官方微信公众号:
     
     <img src="https://github.com/night-king/weixinSDK/blob/master/%E4%BD%A0%E6%83%B3%E8%A6%81%E7%9A%84%E9%83%BD%E5%9C%A8%E8%BF%99%E9%87%8C.jpg"  style="width:100px;height:100px;"/>
     

4.源代码唯一托管地址：https://github.com/night-king/weixinSDK


5.Copyright and license

Code and documentation copyright 2011-2015. Code released under the MIT license. Docs released under Creative Commons.




