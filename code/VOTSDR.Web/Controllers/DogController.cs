using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VOTSDR.Web.Controllers
{
    public class DogController : Controller
    {
        public ActionResult Available()
        {
            return View();
        }

        public ActionResult SuccessStories()
        {
            return View();
        }
    }
}
