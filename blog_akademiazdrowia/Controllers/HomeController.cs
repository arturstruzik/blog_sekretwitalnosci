using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace blog_akademiazdrowia.Controllers
{
    public class HomeController : Controller
    {
        sekretwitalnosciContext db = new sekretwitalnosciContext();

        public ActionResult Index()
        {
            return View(db.Articles.ToList<Articles>());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}