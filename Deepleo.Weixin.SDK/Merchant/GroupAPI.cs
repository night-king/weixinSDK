using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using Codeplex.Data;

namespace Deepleo.Weixin.SDK.Merchant
{
    /// <summary>
    /// 分组管理接口
    /// </summary>
    public class GroupAPI
    {
        /// <summary>
        /// 增加分组
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="content">
        /// {
        ///  "group_detail" : {
        ///   "group_name": "测试分组", 
        ///   "product_list" : [
        ///       "pDF3iY9cEWyMimNlKbik_NYJTzYU", 
        ///      "pDF3iY4kpZagQfwJ_LVQBaOC-LsM"
        ///  ]
        ///  }
        /// }
        /// 
        /// group_name	分组名称
        /// product_list	商品ID集合
        /// </param>
        /// <returns>
        /// {
        /// "errcode":0,
        /// "errmsg":"success",
        /// "group_id": 19
        ///}
        /// </returns>
        public static dynamic Add(string access_token, dynamic content)
        {
            var client = new HttpClient();
            var result = client.PostAsync(string.Format("https://api.weixin.qq.com/merchant/group/add?access_token={0}", access_token),
                         new StringContent(DynamicJson.Serialize(content))).Result;
            return DynamicJson.Parse(result.Content.ReadAsStringAsync().Result);
        }
        /// <summary>
        /// 删除分组
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="group_id">分组ID</param>
        /// <returns>
        /// {
        /// "errcode":0,
        /// "errmsg":"success"
        /// }
        /// </returns>
        public static dynamic Delete(string access_token, int group_id)
        {
            var client = new HttpClient();
            var content = new StringBuilder();
            content.Append("{")
                   .Append('"' + "group_id" + '"' + ": " + group_id)
                   .Append("}");
            var result = client.PostAsync(string.Format("https://api.weixin.qq.com/merchant/group/del?access_token={0}", access_token),
                         new StringContent(content.ToString())).Result;
            return DynamicJson.Parse(result.Content.ReadAsStringAsync().Result);
        }
        /// <summary>
        /// 修改分组属性
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="group_id"></param>
        /// <param name="group_name"></param>
        /// <returns>
        /// {
        ///  "errcode":0,
        /// "errmsg":"success"
        /// }
        /// </returns>
        public static dynamic UpdateProperty(string access_token, int group_id, string group_name)
        {
            var client = new HttpClient();
            var content = new StringBuilder();
            content.Append("{")
                   .Append('"' + "group_id" + '"' + ": " + group_id).Append(",")
                   .Append('"' + "group_name" + '"' + ": " + '"' + group_name + '"')
                   .Append("}");
            var result = client.PostAsync(string.Format("https://api.weixin.qq.com/merchant/group/propertymod?access_token={0}", access_token),
                         new StringContent(content.ToString())).Result;
            return DynamicJson.Parse(result.Content.ReadAsStringAsync().Result);
        }
        /// <summary>
        /// 修改分组商品
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="group_id"></param>
        /// <param name="product">分组商品信息, 数据示例：
        /// [
        /// {
        /// 	"product_id": "pDF3iY-CgqlAL3k8Ilz-6sj0UYpk",
        /// 	"mod_action": 1
        /// }, 
        /// {
        /// 	"product_id": "pDF3iY-RewlAL3k8Ilz-6sjsepp9",
        /// 	"mod_action": 0
        /// }, 
        /// ]
        /// </param>
        /// <returns>
        /// {
        ///  "errcode":0,
        /// "errmsg":"success"
        /// }
        /// </returns>
        public static dynamic UpdateProduct(string access_token, int group_id, dynamic product)
        {
            var client = new HttpClient();
            var content = new StringBuilder();
            content.Append("{")
                   .Append('"' + "group_id" + '"' + ": " + group_id).Append(",")
                   .Append('"' + "product" + '"' + ": ").Append(DynamicJson.Serialize(product))
                   .Append("}");
            var result = client.PostAsync(string.Format("https://api.weixin.qq.com/merchant/group/productmod?access_token={0}", access_token),
                         new StringContent(content.ToString())).Result;
            return DynamicJson.Parse(result.Content.ReadAsStringAsync().Result);
        }
        /// <summary>
        /// 获取所有分组
        /// </summary>
        /// <param name="access_token"></param>
        /// <returns>
        /// {
        /// "errcode": 0,
        /// "errmsg": "success",
        /// "groups_detail": [
        /// 	{
        /// 	  "group_id": 200077549,
        /// 	  "group_name": "最新上架"
        /// 	},
        /// 	{
        ///   "group_id": 200079772,
        ///   "group_name": "全球热卖"
        /// }
        /// ]
        /// }
        /// </returns>
        public static dynamic GetAll(string access_token)
        {
            var client = new HttpClient();
            var result = client.GetAsync(string.Format("https://api.weixin.qq.com/merchant/group/getall?access_token={0}", access_token)).Result;
            return DynamicJson.Parse(result.Content.ReadAsStringAsync().Result);
        }

        /// <summary>
        /// 根据分组ID获取分组信息
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="group_id">分组ID</param>
        /// <returns>
        /// {
        ///"errcode": 0,
        ///"errmsg": "success",
        ///"group_detail": {
        ///	"group_id": 200077549,
        ///	"group_name": "最新上架",
        ///"product_list": 
        ///[
        ///  "pDF3iYzZoY-Budrzt8O6IxrwIJAA",
        ///  "pDF3iY3pnWSGJcO2MpS2Nxy3HWx8",
        ///  "pDF3iY33jNt0Dj3M3UqiGlUxGrio"
        ///]
        ///}
        ///}
        /// </returns>
        public static dynamic GetById(string access_token, int group_id)
        {
            var client = new HttpClient();
            var content = new StringBuilder();
            content.Append("{")
                   .Append('"' + "group_id" + '"' + ": " + group_id).Append(",")
                   .Append("}");
            var result = client.PostAsync(string.Format("https://api.weixin.qq.com/merchant/group/getbyid?access_token={0}", access_token),
                         new StringContent(content.ToString())).Result;
            return DynamicJson.Parse(result.Content.ReadAsStringAsync().Result);
        }

    }
}
