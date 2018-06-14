using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Deepleo.Weixin.SDK.Helpers;
using Codeplex.Data;
using System.IO;

namespace Deepleo.Weixin.SDK.Merchant
{
    /// <summary>
    ///功能接口
    /// </summary>
    public class CommonAPI
    {
        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="fileName">图片名称,如1.jpg</param>
        /// <param name="inputStream">图片名称,如1.jpg</param>
        /// <returns>
        /// {
        ///"errcode":0,
        ///"errmsg":"success", 
        ///"image_url": "http://mmbiz.qpic.cn/mmbiz/4whpV1VZl2ibl4JWwwnW3icSJGqecVtRiaPxwWEIr99eYYL6AAAp1YBo12CpQTXFH6InyQWXITLvU4CU7kic4PcoXA/0"
        ///}
        /// </returns>
        public static dynamic UploadImg(string access_token, string fileName,Stream inputStream)
        {
            var url = string.Format("https://api.weixin.qq.com/merchant/common/upload_img?access_token={0}&filename={1}", access_token, fileName);
            var returnText = Util.HttpRequestPost(url, "filename", fileName, inputStream);
            return DynamicJson.Parse(returnText);
        }
    }
}
