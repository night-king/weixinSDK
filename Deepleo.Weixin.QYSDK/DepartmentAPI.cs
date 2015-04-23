/*--------------------------------------------------------------------------
* UserAdminAPI.cs
 *Auth:deepleo
* Date:2013.12.31
* Email:2586662969@qq.com
* Website:http://www.weixinsdk.net
*--------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using Codeplex.Data;

namespace Deepleo.Weixin.QYSDK
{
    /// <summary>
    ///  管理部门
    ///  http://qydev.weixin.qq.com/wiki/index.php?title=%E7%AE%A1%E7%90%86%E9%83%A8%E9%97%A8
    ///  部门的最大层级为15层，每个部门的直属成员上限为1000个；出于安全考虑，某些接口需要在管理端有明确的授权。
    /// </summary>
    public class DepartmentAPI
    {
        /// <summary>
        /// 创建部门
        /// 权限说明:管理员须拥有“维护通讯录”的接口权限，以及父部门的管理权限。
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="name">部门名称。长度限制为1~64个字符</param>
        /// <param name="order">在父部门中的次序值。order值小的排序靠前。</param>
        /// <param name="parentid">父亲部门id。根部门id为1</param>
        /// <param name="id">部门ID。用指定部门ID新建部门，不指定此参数时，则自动生成</param>
        /// <returns></returns>
        public static bool Create(string access_token, string name, string order, int parentid = 1, int id = 0)
        {
            var builder = new StringBuilder();
            builder.Append("{")
                .Append('"' + "name" + '"' + ":").Append(name)
                .Append('"' + "parentid" + '"' + ":").Append(parentid)
                .Append('"' + "order" + '"' + ":").Append(order);
            if (id > 0)
            {
                builder.Append('"' + "id" + '"' + ":").Append(id);
            }
            builder.Append("}");
            var client = new HttpClient();
            var result = client.PostAsync(string.Format("https://qyapi.weixin.qq.com/cgi-bin/department/create?access_token={0}", access_token), new StringContent(builder.ToString())).Result;
            return DynamicJson.Parse(result.Content.ReadAsStringAsync().Result).errcode == 0;
        }

        /// <summary>
        /// 更新部门
        /// 权限说明: 管理员须拥有“维护通讯录”的接口权限，以及该部门的管理权限。
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="order"></param>
        /// <param name="parentid"></param>
        /// <returns></returns>
        public static bool Update(string access_token, int id, string name, string order, int parentid)
        {
            var builder = new StringBuilder();
            builder.Append("{")
                .Append('"' + "id" + '"' + ":").Append(id)
                .Append('"' + "name" + '"' + ":").Append(name)
                .Append('"' + "parentid" + '"' + ":").Append(parentid)
                .Append('"' + "order" + '"' + ":").Append(order)
                .Append("}");
            var client = new HttpClient();
            var result = client.PostAsync(string.Format("https://qyapi.weixin.qq.com/cgi-bin/department/update?access_token={0}", access_token), new StringContent(builder.ToString())).Result;
            return DynamicJson.Parse(result.Content.ReadAsStringAsync().Result).errcode == 0;
        }

        /// <summary>
        /// 删除部门
        /// 权限说明: 管理员须拥有“维护通讯录”的接口权限，以及该部门的管理权限。
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool Delete(string access_token, int id)
        {
            var client = new HttpClient();
            var result = client.GetAsync(string.Format("https://qyapi.weixin.qq.com/cgi-bin/department/delete?access_token={0}&id={1}", access_token, id)).Result;
            return DynamicJson.Parse(result.Content.ReadAsStringAsync().Result).errcode == 0;
        }

        /// <summary>
        /// 获取部门列表
        /// 权限说明: 管理员须拥有’获取部门列表’的接口权限，以及对部门的查看权限。
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="id">部门ID。获取指定部门ID下的子部门</param>
        /// <returns>
        /// {
        /// "errcode": 0,
        ///  "errmsg": "ok",
        ///  "department": [
        ///   {
        ///       "id": 2,
        ///     "name": "广州研发中心",
        ///     "parentid": 1,
        ///     "order": 10
        ///  },
        ///  {
        ///      "id": 3
        ///      "name": "邮箱产品部",
        ///     "parentid": 2,
        ///     "order": 40
        ///  }
        ///  ]
        /// }
        /// </returns>
        public static bool GetList(string access_token, int id)
        {
            var client = new HttpClient();
            var result = client.GetAsync(string.Format("https://qyapi.weixin.qq.com/cgi-bin/department/list?access_token={0}&id={1}", access_token, id)).Result;
            return DynamicJson.Parse(result.Content.ReadAsStringAsync().Result);
        }

    }
}
