using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using Codeplex.Data;

namespace Deepleo.Weixin.SDK.Merchant
{
    /// <summary>
    /// 货架管理接口
    /// </summary>
    public class ShelfAPI
    {
        /// <summary>
        /// 增加货架
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="content">货架详情，参见官方文档
        /// 注意：货架有五个控件，每个控件post的数据都不一样。
        /// </param>
        /// <returns>
        /// {
        ///"errcode":0,
        ///"errmsg":"success",
        ///"shelf_id": 12
        ///}
        /// </returns>
        public static dynamic Add(string access_token, dynamic content)
        {
            var client = new HttpClient();
            var result = client.PostAsync(string.Format("https://api.weixin.qq.com/merchant/shelf/add?access_token={0}", access_token),
                         new StringContent(DynamicJson.Serialize(content))).Result;
            return DynamicJson.Parse(result.Content.ReadAsStringAsync().Result);
        }
        /// <summary>
        /// 删除货架
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="shelf_id">货架ID</param>
        /// <returns>
        /// {
        /// "errcode":0,
        ///"errmsg":"success"
        ///}
        /// </returns>
        public static dynamic Delete(string access_token, int shelf_id)
        {
            var client = new HttpClient();
            var content = new StringBuilder();
            content.Append("{")
                   .Append('"' + "shelf_id" + '"' + ": " + shelf_id)
                   .Append("}");
            var result = client.PostAsync(string.Format("https://api.weixin.qq.com/merchant/shelf/del?access_token={0}", access_token),
                         new StringContent(content.ToString())).Result;
            return DynamicJson.Parse(result.Content.ReadAsStringAsync().Result);
        }

        /// <summary>
        /// 修改货架
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="shelf_id">货架ID</param>
        /// <param name="shelf_data">货架详情(字段说明详见增加货架)</param>
        /// <param name="shelf_banner">货架banner(图片需调用图片上传接口获得图片Url填写至此，否则修改货架失败)</param>
        /// <param name="shelf_name">货架名称</param>
        /// <returns>
        /// <returns>
        /// {
        /// "errcode":0,
        ///"errmsg":"success"
        ///}
        /// </returns>
        public static dynamic Update(string access_token, int shelf_id, dynamic shelf_data, string shelf_banner, string shelf_name)
        {
            var client = new HttpClient(); var content = new StringBuilder();
            content.Append("{")
                   .Append('"' + "shelf_id" + '"' + ": " + shelf_id).Append(",")
                   .Append('"' + "shelf_data" + '"' + ": " + DynamicJson.Serialize(shelf_data)).Append(",")
                   .Append('"' + "shelf_banner" + '"' + ": " + '"' + shelf_banner + '"').Append(",")
                   .Append('"' + "shelf_name" + '"' + ": " + '"' + shelf_name + '"')
                  .Append("}");
            var result = client.PostAsync(string.Format("https://api.weixin.qq.com/merchant/shelf/mod?access_token={0}", access_token),
                         new StringContent(content.ToString())).Result;
            return DynamicJson.Parse(result.Content.ReadAsStringAsync().Result);
        }

        /// <summary>
        /// 获取所有货架
        /// </summary>
        /// <param name="access_token"></param>
        /// <returns></returns>
        public static dynamic GetAll(string access_token)
        {
            var client = new HttpClient(); var content = new StringBuilder();
            var result = client.GetAsync(string.Format("https://api.weixin.qq.com/merchant/shelf/getall?access_token={0}", access_token)).Result;
            return DynamicJson.Parse(result.Content.ReadAsStringAsync().Result);
        }
        /// <summary>
        /// 根据货架ID获取货架信息
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="shelf_id">货架ID</param>
        /// <returns>
        /// {
        /// "errcode": 0,
        /// "errmsg": "success",
        /// "shelf_info": {
        /// 	"module_infos": [...]
        /// },
        /// "shelf_banner": "http://mmbiz.qpic.cn/mmbiz/4whpV1VZl2ibp2DgDXiaic6WdflMpNdInS8qUia2BztlPu1gPlCDLZXEjia2qBdjoLiaCGUno9zbs1UyoqnaTJJGeEew/0",
        /// "shelf_name": "新建货架",
        /// "shelf_id": 97
        /// }
        /// </returns>
        public static dynamic GetById(string access_token, int shelf_id)
        {
            var client = new HttpClient(); 
            var content = new StringBuilder();
            content.Append("{")
                   .Append('"' + "shelf_id" + '"' + ": " + shelf_id)
                  .Append("}");
            var result = client.PostAsync(string.Format("https://api.weixin.qq.com/merchant/shelf/getbyid?access_token={0}", access_token),
                         new StringContent(content.ToString())).Result;
            return DynamicJson.Parse(result.Content.ReadAsStringAsync().Result);
        }
    }
}
