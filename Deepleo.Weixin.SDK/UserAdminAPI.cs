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

namespace Deepleo.Weixin.SDK
{
    /// <summary>
    ///  用户管理
    ///  http://mp.weixin.qq.com/wiki/index.php?title=%E5%88%86%E7%BB%84%E7%AE%A1%E7%90%86%E6%8E%A5%E5%8F%A3
    ///  注意：上报地理位置以推送XML数据包（时间推送）到开发者填写的URL来实现（WeixinExecutor里面）。
    /// </summary>
    public class UserAdminAPI
    {
        /// <summary>
        /// 创建分组
        /// 注意：一个公众账号，最多支持创建500个分组
        /// </summary>
        /// <param name="access_token">调用接口凭证</param>
        /// <param name="name">分组名字（30个字符以内）</param>
        /// <returns></returns>
        public static bool CreateGroup(string access_token, string name)
        {
            var builder = new StringBuilder();
            builder.Append("{")
                .Append('"' + "group" + '"' + ":")
                .Append("{")
                .Append('"' + "name" + '"' + ":").Append(name)
                .Append("}")
                .Append("}");
            var client = new HttpClient();
            var result = client.PostAsync(string.Format("https://api.weixin.qq.com/cgi-bin/groups/create?access_token={0}", access_token), new StringContent(builder.ToString())).Result;
            return DynamicJson.Parse(result.Content.ReadAsStringAsync().Result).errcode == 0;
        }
        /// <summary>
        /// 查询所有分组
        /// </summary>
        /// <param name="access_token">调用接口凭证</param>
        /// <returns></returns>
        public static dynamic GetGroups(string access_token)
        {
            var client = new HttpClient();
            var result = client.PostAsync(string.Format("https://api.weixin.qq.com/cgi-bin/groups/get?access_token={0}", access_token), new StringContent("")).Result;
            if (!result.IsSuccessStatusCode) return null;
            return DynamicJson.Parse(result.Content.ReadAsStringAsync().Result);
        }

        /// <summary>
        /// 查询用户所在分组
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="openid"></param>
        /// <returns>不为空时，表示查询成功，否则查询失败</returns>
        public static string GetUserGroup(string access_token, string openid)
        {
            var builder = new StringBuilder();
            builder.Append("{").Append('"' + "openid" + '"' + ":").Append(openid).Append("}");
            var client = new HttpClient();
            var result = client.PostAsync(string.Format("https://api.weixin.qq.com/cgi-bin/groups/getid?access_token={0}", access_token), new StringContent(builder.ToString())).Result;
            if (!result.IsSuccessStatusCode) return string.Empty;
            return DynamicJson.Parse(result.Content.ReadAsStringAsync().Result).groupid;
        }
        /// <summary>
        /// 修改分组名
        /// </summary>
        /// <param name="access_token">调用接口凭证</param>
        /// <param name="id">分组id，由微信分配</param>
        /// <param name="name">分组名字（30个字符以内）</param>
        /// <returns></returns>
        public static bool UpdateGroup(string access_token, string id, string name)
        {
            var builder = new StringBuilder();
            builder.Append("{")
                .Append('"' + "group" + '"' + ":")
                .Append("{")
                .Append('"' + "id" + '"' + ":").Append(id).Append(",")
                .Append('"' + "name" + '"' + ":").Append(name)
                .Append("}")
                .Append("}");
            var client = new HttpClient();
            var result = client.PostAsync(string.Format("https://api.weixin.qq.com/cgi-bin/groups/update?access_token={0}", access_token), new StringContent(builder.ToString())).Result;
            return DynamicJson.Parse(result.Content.ReadAsStringAsync().Result).errcode == 0;
        }
        /// <summary>
        /// 移动用户分组
        /// </summary>
        /// <param name="access_token">调用接口凭证</param>
        /// <param name="openid">用户唯一标识符</param>
        /// <param name="to_groupid">分组id</param>
        /// <returns></returns>
        public static bool MoveGroup(string access_token, string openid, string to_groupid)
        {
            var builder = new StringBuilder();
            builder.Append("{")
                .Append('"' + "openid" + '"' + ":").Append(openid).Append(",")
                .Append('"' + "to_groupid" + '"' + ":").Append(to_groupid)
                .Append("}");
            var client = new HttpClient();
            var result = client.PostAsync(string.Format("https://api.weixin.qq.com/cgi-bin/groups/members/update?access_token={0}", access_token), new StringContent(builder.ToString())).Result;
            return DynamicJson.Parse(result.Content.ReadAsStringAsync().Result).errcode == 0;
        }
        /// <summary>
        /// 设置备注名
        /// </summary>
        /// <param name="access_token">调用接口凭证</param>
        /// <param name="openid">用户唯一标识符</param>
        /// <param name="remark">新的备注名，长度必须小于30字符</param>
        /// <returns></returns>
        public static bool SetRemark(string access_token, string openid, string remark)
        {
            var builder = new StringBuilder();
            builder.Append("{")
                .Append('"' + "openid" + '"' + ":").Append(openid).Append(",")
                .Append('"' + "remark" + '"' + ":").Append(remark)
                .Append("}");
            var client = new HttpClient();
            var result = client.PostAsync(string.Format("https://api.weixin.qq.com/cgi-bin/user/info/updateremark?access_token={0}", access_token), new StringContent(builder.ToString())).Result;
            return DynamicJson.Parse(result.Content.ReadAsStringAsync().Result).errcode == 0;
        }
      
        /// <summary>
        /// 获取用户基本信息（包括UnionID机制）
        /// 注意：如果开发者有在多个公众号，或在公众号、移动应用之间统一用户帐号的需求，需要前往微信开放平台（open.weixin.qq.com）绑定公众号后，才可利用UnionID机制来满足上述需求。
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="openId"></param>
        /// <returns>UnionID机制的返回值中将包含“unionid”</returns>
        public static dynamic GetInfo(string access_token, string openId)
        {
            var client = new HttpClient();
            var result = client.GetAsync(string.Format("https://api.weixin.qq.com/cgi-bin/user/info?access_token={0}&openid={1}&lang=zh_CN", access_token, openId)).Result;
            if (!result.IsSuccessStatusCode) return null;
            return DynamicJson.Parse(result.Content.ReadAsStringAsync().Result);
        }

        /// <summary>
        /// 获取关注者列表
        /// </summary>
        /// <param name="token"></param>
        /// <param name="nextOpenId">第一个拉取的OPENID，不填默认从头开始拉取</param>
        /// <returns></returns>
        public static dynamic GetSubscribes(string access_token, string nextOpenId)
        {
            var client = new HttpClient();
            var result = client.GetAsync(string.Format("https://api.weixin.qq.com/cgi-bin/user/get?access_token={0}&next_openid={1}", access_token, nextOpenId)).Result;
            if (!result.IsSuccessStatusCode) return null;
            return DynamicJson.Parse(result.Content.ReadAsStringAsync().Result);
        }
    }
}
