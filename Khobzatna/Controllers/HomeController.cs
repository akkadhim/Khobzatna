using Khobzatna.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Khobzatna.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (TempData["MessageHelper"] != null)
            {
                MessageHelper message = (MessageHelper)TempData["MessageHelper"];
                return View(message);
            }
            return View();
        }
    }
}