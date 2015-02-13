using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using Codeplex.Data;
using Deepleo.Weixin.SDK.Helpers;

namespace Deepleo.Weixin.SDK.Merchant
{
    /// <summary>
    /// 订单管理
    /// </summary>
    public class OrderAPI
    {
        /// <summary>
        /// 根据订单ID获取订单详情
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="order_id">订单ID</param>
        /// <returns>订单详情，具体请参见官方文档</returns>
        public static dynamic GetById(string access_token, string order_id)
        {
            var client = new HttpClient(); var content = new StringBuilder();
            content.Append("{")
                   .Append('"' + "order_id" + '"' + ": " + '"' + order_id + '"')
                  .Append("}");
            var result = client.PostAsync(string.Format("https://api.weixin.qq.com/merchant/order/getbyid?access_token={0}", access_token),
                         new StringContent(content.ToString())).Result;
            return DynamicJson.Parse(result.Content.ReadAsStringAsync().Result);
        }

        /// <summary>
        ///据订单状态/创建时间获取订单详情
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="status">订单状态(不带该字段-全部状态, 2-待发货, 3-已发货, 5-已完成, 8-维权中, )</param>
        /// <param name="begintime">订单创建时间起始时间(不带该字段则不按照时间做筛选)</param>
        /// <param name="endtime">订单创建时间终止时间(不带该字段则不按照时间做筛选)</param>
        /// <returns>订单详情,参见官方文档</returns>
        public static dynamic GetByFilter(string access_token, int status, long begintime = 0, long endtime = 0)
        {
            var client = new HttpClient(); var content = new StringBuilder();
            content.Append("{")
                   .Append('"' + "status" + '"' + ": " + status);
            if (begintime > 0 || endtime > 0)
            {
                content.Append(",");
                if (begintime <= 0) begintime = DateTime.MinValue.ToTimestamp();
                if (endtime <= 0) endtime = DateTime.Now.ToTimestamp();
                content.Append('"' + "begintime" + '"' + ": " + begintime).Append(",");
                if (endtime <= 0) endtime = DateTime.Now.ToTimestamp();
                content.Append('"' + "endtime" + '"' + ": " + endtime);
            }
            content.Append("}");
            var result = client.PostAsync(string.Format("https://api.weixin.qq.com/merchant/order/getbyfilter?access_token={0}", access_token),
                         new StringContent(content.ToString())).Result;
            return DynamicJson.Parse(result.Content.ReadAsStringAsync().Result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="order_id">订单ID</param>
        /// <param name="need_delivery">商品是否需要物流(0-不需要，1-需要，无该字段默认为需要物流)</param>
        /// <param name="is_others">是否为6.4.5表之外的其它物流公司(0-否，1-是，无该字段默认为不是其它物流公司)
        /// 6.4.5附：物流公司ID
        /// 物流公司	ID
        /// =============================
        /// 邮政EMS	Fsearch_code
        /// 申通快递	002shentong
        /// 中通速递	066zhongtong
        /// 圆通速递	056yuantong
        /// 天天快递	042tiantian
        /// 顺丰速运	003shunfeng
        /// 韵达快运	059Yunda
        /// 宅急送	064zhaijisong
        /// 汇通快运	020huitong
        /// 易迅快递	zj001yixun
        /// ============================
        /// </param>
        /// <param name="delivery_company">
        /// 物流公司ID(参考《物流公司ID》；
        /// 当need_delivery为0时，可不填本字段；
        /// 当need_delivery为1时，该字段不能为空；
        /// 当need_delivery为1且is_others为1时，本字段填写其它物流公司名称)</param>
        /// <param name="delivery_track_no">
        /// 运单ID(
        /// 当need_delivery为0时，可不填本字段；
        /// 当need_delivery为1时，该字段不能为空；
        /// )
        /// </param>
        /// <returns>
        /// {
        ///"errcode": 0,
        ///"errmsg": "success"
        ///}
        ///</returns>
        public static dynamic SetDelivery(string access_token, string order_id, int need_delivery, int is_others, string delivery_track_no = "", string delivery_company = "")
        {
            var client = new HttpClient(); var content = new StringBuilder();
            content.Append("{")
                   .Append('"' + "order_id" + '"' + ": " + '"' + order_id + '"').Append(",")
                   .Append('"' + "need_delivery" + '"' + ": " + need_delivery).Append(",")
                   .Append('"' + "is_others" + '"' + ": " + is_others);
            if (need_delivery == 1)
            {
                content.Append(",")
                       .Append('"' + "delivery_company" + '"' + ": " + '"' + delivery_company + '"').Append(",")
                       .Append('"' + "delivery_track_no" + '"' + ": " + '"' + delivery_track_no + '"');
            }
            content.Append("}");
            var result = client.PostAsync(string.Format("https://api.weixin.qq.com/merchant/order/setdelivery?access_token={0}", access_token),
                         new StringContent(content.ToString())).Result;
            return DynamicJson.Parse(result.Content.ReadAsStringAsync().Result);
        }

        /// <summary>
        /// 关闭订单
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="order_id">订单ID</param>
        /// <returns>
        /// {
        ///"errcode": 0,
        ///"errmsg": "success"
        ///}
        ///</returns>
        public static dynamic Close(string access_token, string order_id)
        {
            var client = new HttpClient(); var content = new StringBuilder();
            content.Append("{")
                   .Append('"' + "order_id" + '"' + ": " + '"' + order_id + '"')
                  .Append("}");
            var result = client.PostAsync(string.Format("https://api.weixin.qq.com/merchant/order/close?access_token={0}", access_token),
                         new StringContent(content.ToString())).Result;
            return DynamicJson.Parse(result.Content.ReadAsStringAsync().Result);
        }

    }
}
