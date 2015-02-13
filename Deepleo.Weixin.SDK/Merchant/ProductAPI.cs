using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using Codeplex.Data;

namespace Deepleo.Weixin.SDK.Shop
{
    /// <summary>
    /// 商品管理
    /// </summary>
    public class ProductAPI
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="content">
        /// 参见官方文档
        /// </param>
        /// <returns>
        /// {
        ///"errcode": 0,
        ///"errmsg": "success",
        ///"product_id": "pDF3iYwktviE3BzU3BKiSWWi9Nkw"
        /// }
        /// </returns>
        public static dynamic Create(string access_token, dynamic content)
        {
            var client = new HttpClient();
            var result = client.PostAsync(string.Format("https://api.weixin.qq.com/merchant/create?access_token={0}", access_token),
                         new StringContent(DynamicJson.Serialize(content))).Result;
            return DynamicJson.Parse(result.Content.ReadAsStringAsync().Result);
        }
        /// <summary>
        /// 删除商品
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="product_id">商品ID</param>
        /// <returns>
        /// {
        ///"errcode":0,
        ///"errmsg":"success"
        ///}
        ///</returns>
        public static dynamic Delete(string access_token, string product_id)
        {
            var client = new HttpClient();
            var content = new StringBuilder();
            content.Append("{")
                   .Append('"' + "product_id" + '"' + ": " + '"' + product_id + '"')
                   .Append("}");

            var result = client.PostAsync(string.Format("https://api.weixin.qq.com/merchant/del?access_token={0}", access_token),
                         new StringContent(content.ToString())).Result;
            return DynamicJson.Parse(result.Content.ReadAsStringAsync().Result);
        }
        /// <summary>
        /// 修改商品
        /// 
        /// 备注：
        /// 1.product_id表示要更新的商品的ID，其他字段说明请参考增加商品接口。
        ///2.从未上架的商品所有信息均可修改，否则商品的名称(name)、商品分类(category)、商品属性(property)这三个字段不可修改。
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="content"></param>
        /// <returns>
        /// {
        /// "errcode":0,
        /// "errmsg":"success"
        /// }
        /// </returns>
        public static dynamic Update(string access_token, dynamic content)
        {
            var client = new HttpClient();
            var result = client.PostAsync(string.Format("https://api.weixin.qq.com/merchant/update?access_token={0}", access_token),
                         new StringContent(DynamicJson.Serialize(content))).Result;
            return DynamicJson.Parse(result.Content.ReadAsStringAsync().Result);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="product_id"></param>
        /// <returns>
        /// 商品详细信息,
        /// 具体请参见官方文档</returns>
        public static dynamic Get(string access_token, string product_id)
        {
            var client = new HttpClient();
            var content = new StringBuilder();
            content.Append("{")
                   .Append('"' + "product_id" + '"' + ": " + '"' + product_id + '"')
                   .Append("}");
            var result = client.PostAsync(string.Format("https://api.weixin.qq.com/merchant/get?access_token={0}", access_token),
                         new StringContent(content.ToString())).Result;
            return DynamicJson.Parse(result.Content.ReadAsStringAsync().Result);
        }

        /// <summary>
        /// 获取指定状态的所有商品
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="status">
        /// 商品状态(0-全部, 1-上架, 2-下架)
        /// </param>
        /// <returns>
        /// 商品列表信息,
        /// 具体请参见官方文档</returns>
        public static dynamic GetByStatus(string access_token, int status)
        {
            var client = new HttpClient();
            var content = new StringBuilder();
            content.Append("{")
                   .Append('"' + "status" + '"' + ": " + status)
                   .Append("}");
            var result = client.PostAsync(string.Format("https://api.weixin.qq.com/merchant/getbystatus?access_token={0}", access_token),
                         new StringContent(content.ToString())).Result;
            return DynamicJson.Parse(result.Content.ReadAsStringAsync().Result);
        }
        /// <summary>
        /// 商品上下架
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="product_id">商品id</param>
        /// <param name="status">
        /// 商品上下架标识(0-下架, 1-上架)
        /// </param>
        /// <returns>
        /// {
        ///"errcode":0,
        ///"errmsg":"success"
        ///}
        /// </returns>
        public static dynamic UpdateStatus(string access_token, string product_id, int status)
        {
            var client = new HttpClient();
            var content = new StringBuilder();
            content.Append("{")
                   .Append('"' + "product_id" + '"' + ": " + '"' + product_id + '"').Append(",")
                   .Append('"' + "status" + '"' + ": " + status)
                   .Append("}");
            var result = client.PostAsync(string.Format("https://api.weixin.qq.com/merchant/modproductstatus?access_token={0}", access_token),
                         new StringContent(content.ToString())).Result;
            return DynamicJson.Parse(result.Content.ReadAsStringAsync().Result);
        }
        /// <summary>
        /// 获取指定分类的所有子分类
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="cate_id"></param>
        /// <returns></returns>
        public static dynamic GetSubByCategory(string access_token, string cate_id)
        {
            var client = new HttpClient();
            var content = new StringBuilder();
            content.Append("{")
                   .Append('"' + "cate_id" + '"' + ": " + cate_id)
                   .Append("}");
            var result = client.PostAsync(string.Format("https://api.weixin.qq.com/merchant/category/getsub?access_token={0}", access_token),
                         new StringContent(content.ToString())).Result;
            return DynamicJson.Parse(result.Content.ReadAsStringAsync().Result);
        }
        /// <summary>
        /// 获取指定子分类的所有SKU
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="cate_id">分类id</param>
        /// <returns></returns>
        public static dynamic GetSKUByCategory(string access_token, string cate_id)
        {
            var client = new HttpClient();
            var content = new StringBuilder();
            content.Append("{")
                   .Append('"' + "cate_id" + '"' + ": " + cate_id)
                   .Append("}");
            var result = client.PostAsync(string.Format("https://api.weixin.qq.com/merchant/category/getsku?access_token={0}", access_token),
                         new StringContent(content.ToString())).Result;
            return DynamicJson.Parse(result.Content.ReadAsStringAsync().Result);
        }
        /// <summary>
        /// 获取指定分类的所有属性
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="cate_id"></param>
        /// <returns></returns>
        public static dynamic GetPropertiesByCategory(string access_token, string cate_id)
        {
            var client = new HttpClient();
            var content = new StringBuilder();
            content.Append("{")
                   .Append('"' + "cate_id" + '"' + ": " + cate_id)
                   .Append("}");
            var result = client.PostAsync(string.Format("https://api.weixin.qq.com/merchant/category/getproperty?access_token={0}", access_token),
                         new StringContent(content.ToString())).Result;
            return DynamicJson.Parse(result.Content.ReadAsStringAsync().Result);
        }
    }
}
