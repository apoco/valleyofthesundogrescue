using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VOTSDR.Web.Controllers
{
    public class ImageController : Controller
    {
        public ActionResult Image(Guid id)
        {
            return File(
                HttpContext.Server.MapPath(
                    string.Format("~/App_Data/Images/{0}.jpg", id)),
                "image/jpeg");
        }
    }
}