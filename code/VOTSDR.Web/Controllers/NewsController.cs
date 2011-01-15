using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VOTSDR.Web.Controllers
{
    public class NewsController : Controller
    {
        public ActionResult List()
        {
            return View();
        }
    }
}