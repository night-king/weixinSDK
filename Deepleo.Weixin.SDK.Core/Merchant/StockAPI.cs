using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using Codeplex.Data;

namespace Deepleo.Weixin.SDK.Merchant
{
    /// <summary>
    /// 库存管理接口
    /// </summary>
    public class StockAPI
    {
        /// <summary>
        /// 增加库存
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="product_id">商品ID</param>
        /// <param name="sku_info">sku信息,格式"id1:vid1;id2:vid2",如商品为统一规格，则此处赋值为空字符串即可</param>
        /// <param name="quantity">增加的库存数量</param>
        /// <returns>
        /// {
        /// "errcode":0,
        /// "errmsg":"success"
        /// }
        /// </returns>
        public static dynamic Add(string access_token, string product_id, string sku_info, int quantity)
        {
            var client = new HttpClient();
            var content = new StringBuilder();
            content.Append("{")
                   .Append('"' + "product_id" + '"' + ": " + '"' + product_id + '"').Append(",")
                   .Append('"' + "sku_info" + '"' + ": " + '"' + product_id + '"').Append(",")
                   .Append('"' + "quantity" + '"' + ": " + quantity).Append(",")
                   .Append("}");
            var result = client.PostAsync(string.Format("https://api.weixin.qq.com/merchant/stock/add?access_token={0}", access_token),
                         new StringContent(content.ToString())).Result;
            return DynamicJson.Parse(result.Content.ReadAsStringAsync().Result);
        }
        /// <summary>
        /// 减少库存
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="product_id">商品ID</param>
        /// <param name="sku_info">sku信息,格式"id1:vid1;id2:vid2"</param>
        /// <param name="quantity">减少的库存数量</param>
        /// <returns>
        /// {
        /// "errcode":0,
        /// "errmsg":"success"
        /// }
        /// </returns>
        public static dynamic Reduce(string access_token, string product_id, string sku_info, int quantity)
        {
            var client = new HttpClient();
            var content = new StringBuilder();
            content.Append("{")
                   .Append('"' + "product_id" + '"' + ": " + '"' + product_id + '"').Append(",")
                   .Append('"' + "sku_info" + '"' + ": " + '"' + product_id + '"').Append(",")
                   .Append('"' + "quantity" + '"' + ": " + quantity).Append(",")
                   .Append("}");
            var result = client.PostAsync(string.Format("https://api.weixin.qq.com/merchant/stock/reduce?access_token={0}", access_token),
                         new StringContent(content.ToString())).Result;
            return DynamicJson.Parse(result.Content.ReadAsStringAsync().Result);
        }
    }
}
