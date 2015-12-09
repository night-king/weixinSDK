using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Deepleo.Weixin.SDK;
using System.Net.Http;
using System.IO;

namespace Deepleo.Web.Controllers
{
    public class MediaController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.access_token = WeixinConfig.TokenHelper.GetToken();
            return View();
        }

        [HttpPost]
        public JsonResult Upload()
        {
            var file = Request.Files[0];
            var extension = Path.GetExtension(file.FileName);
            var media_id = MaterialAPI.UploadTempMedia(WeixinConfig.TokenHelper.GetToken(), "image", file.FileName, file.InputStream);
            if (string.IsNullOrEmpty(media_id))
            {
                return Json(new
                {
                    errcode = 0,
                    errMsg = "上传失败",
                });
            }
            var fileName = DateTime.Now.ToBinary().ToString() + extension;
            var savePath = Server.MapPath("~/" + fileName);
            try
            {
                MaterialAPI.DownloadTempMedia(savePath, WeixinConfig.TokenHelper.GetToken(), media_id);
                var domain = System.Configuration.ConfigurationManager.AppSettings["Domain"];
                return Json(new
                {
                    errcode = 1,
                    errMsg = "上传下载成功",
                    media_id = media_id,
                    localPath = Url.Content(string.Format("{0}/{1}", domain, fileName)),
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    errcode = 0,
                    errMsg = "上传失败," + ex.Message,
                });
            }
        }
    }
}
