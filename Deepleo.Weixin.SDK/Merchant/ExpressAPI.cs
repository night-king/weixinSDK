using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using Codeplex.Data;

namespace Deepleo.Weixin.SDK.Merchant
{
    /// <summary>
    /// 邮费模板管理接口
    /// </summary>
    public class ExpressAPI
    {
        /// <summary>
        /// 增加邮费模板
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="content">
        /// 邮费模版，具体请参见官方文档
        /// </param>
        /// <returns>
        /// {
        ///  "errcode": 0,
        /// "errmsg": "success"， 
        /// "template_id": 123456
        /// }
        /// </returns>
        public static dynamic Add(string access_token, dynamic content)
        {
            var client = new HttpClient();
            var result = client.PostAsync(string.Format("https://api.weixin.qq.com/merchant/express/add?access_token={0}", access_token),
                         new StringContent(DynamicJson.Serialize(content))).Result;
            return DynamicJson.Parse(result.Content.ReadAsStringAsync().Result);
        }
        /// <summary>
        /// 删除邮费模板
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="template_id">邮费模版id</param>
        /// <returns>
        /// {
        /// "errcode": 0,
        ///"errmsg": "success"
        ///}
        /// </returns>
        public static dynamic Delete(string access_token, string template_id)
        {
            var client = new HttpClient();
            var content = new StringBuilder();
            content.Append("{")
                   .Append('"' + "template_id" + '"' + ": " + template_id)
                   .Append("}");
            var result = client.PostAsync(string.Format("https://api.weixin.qq.com/merchant/express/del?access_token={0}", access_token),
                         new StringContent(content.ToString())).Result;
            return DynamicJson.Parse(result.Content.ReadAsStringAsync().Result);
        }

        /// <summary>
        /// 修改邮费模板
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="template_id">邮费模板ID</param>
        /// <param name="delivery_template">邮费模板信息(字段说明详见增加邮费模板)</param>
        /// <returns>
        /// {
        /// "errcode": 0,
        ///"errmsg": "success"
        ///}
        /// </returns>
        public static dynamic Update(string access_token, int template_id, dynamic delivery_template)
        {
            var client = new HttpClient();
            var content = new StringBuilder();
            content.Append("{")
                   .Append('"' + "template_id" + '"' + ": " + template_id).Append(",")
                   .Append('"' + "delivery_template" + '"' + ": ").Append(DynamicJson.Serialize(delivery_template))
                   .Append("}");
            var result = client.PostAsync(string.Format("https://api.weixin.qq.com/merchant/express/del?access_token={0}", access_token),
                         new StringContent(content.ToString())).Result;
            return DynamicJson.Parse(result.Content.ReadAsStringAsync().Result);
        }
        /// <summary>
        ///获取指定ID的邮费模板
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="template_id">邮费模板ID</param>
        /// <returns>邮费模版详细信息，详细请参见官方文档</returns>
        public static dynamic GetById(string access_token, int template_id)
        {
            var client = new HttpClient();
            var content = new StringBuilder();
            content.Append("{")
                   .Append('"' + "template_id" + '"' + ": " + template_id)
                   .Append("}");
            var result = client.PostAsync(string.Format("https://api.weixin.qq.com/merchant/express/del?access_token={0}", access_token),
                         new StringContent(content.ToString())).Result;
            return DynamicJson.Parse(result.Content.ReadAsStringAsync().Result);
        }

        /// <summary>
        /// 获取所有邮费模板
        /// </summary>
        /// <param name="access_token"></param>
        /// <returns></returns>
        public static dynamic GetAll(string access_token)
        {
            var client = new HttpClient();
            var result = client.GetAsync(string.Format("https://api.weixin.qq.com/merchant/express/getall?access_token={0}", access_token)).Result;
            return DynamicJson.Parse(result.Content.ReadAsStringAsync().Result);
        }

    }
}
