using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace wepp_app_v0.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Bienvenido al Sistema de Asignación y Control de Recursos";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
