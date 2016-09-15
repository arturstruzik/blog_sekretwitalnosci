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

        [HttpGet]
        public ActionResult Index()
        {
            return View(db.Articles.ToList<Articles>());
        }

        [HttpPost]
        public ActionResult Index(string value)
        {
            var a = Request.Params.GetValues("query").First().
                Split( new char[]{' ',',','.'}, 30).
                Where(s => !string.IsNullOrWhiteSpace(s)).
                Distinct().ToList();
            
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