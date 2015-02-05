/*--------------------------------------------------------------------------
* TemplateMessageAPI.cs
 *Auth:deepleo
* Date:2015.01.15
* Email:2586662969@qq.com
 * Website:http://www.weixinsdk.net
*--------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using Codeplex.Data;

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

        /// <summary>
        /// 设置所属行业
        /// 设置行业可在MP中完成，每月可修改行业1次
        /// 行业代码查询,请登录微信公众号后台查看
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="industry_id1">行业代码</param>
        /// <param name="industry_id2">行业代码</param>
        /// <returns>官方api未给出返回内容,应该是errcode=0就表示成功</returns>
        public static dynamic SetIndustry(string access_token, string industry_id1, string industry_id2)
        {
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/template/api_set_industry?access_token={0}", access_token);
            var client = new HttpClient();
            var builder = new StringBuilder();
            builder
                .Append("{")
                .Append('"' + "industry_id1" + '"' + ":").Append(industry_id1).Append(",")
                .Append('"' + "industry_id2" + '"' + ":").Append(industry_id2)
                .Append("}");
            var result = client.PostAsync(url, new StringContent(builder.ToString())).Result;
            return DynamicJson.Parse(result.Content.ReadAsStringAsync().Result);
        }
        /// <summary>
        /// 获得模板ID
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="template_id_short">模板库中模板的编号</param>
        /// <returns> {"errcode":0,"errmsg":"ok","template_id":"Doclyl5uP7Aciu-qZ7mJNPtWkbkYnWBWVja26EGbNyk"}
        /// </returns>
        public static dynamic GetTemplates(string access_token, string template_id_short)
        {
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/template/api_add_template?access_token={0}", access_token);
            var client = new HttpClient();
            var builder = new StringBuilder();
            builder
                .Append("{")
                .Append('"' + "template_id_short" + '"' + ":").Append(template_id_short)
                .Append("}");
            var result = client.PostAsync(url, new StringContent(builder.ToString())).Result;
            return DynamicJson.Parse(result.Content.ReadAsStringAsync().Result);
        }
        /// <summary>
        /// 发送模板消息
        /// 在模版消息发送任务完成后，微信服务器会将是否送达成功作为通知，发送到开发者中心中填写的服务器配置地址中。
        /// 参见WeixinExecutor.cs TEMPLATESENDJOBFINISH Event
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="content">模板消息体,由于模板众多,且结构不一，请开发者自行按照模板自行构建模板消息体,模板消息体为json字符串,请登录微信公众号后台查看</param>
        /// <returns>  { "errcode":0,"errmsg":"ok", "msgid":200228332}
        /// </returns>
        public static dynamic SendTemplateMessage(string access_token, dynamic content)
        {
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/message/template/send?access_token={0}", access_token);
            var client = new HttpClient();
            var result = client.PostAsync(url, new StringContent(DynamicJson.Serialize(content))).Result;
            return DynamicJson.Parse(result.Content.ReadAsStringAsync().Result);
        }

    }
}
