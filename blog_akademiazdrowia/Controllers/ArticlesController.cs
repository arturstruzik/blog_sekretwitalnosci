using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using blog_akademiazdrowia;
using System.IO;
using System.Drawing;
using blog_akademiazdrowia.Assets.Sorces;

namespace blog_akademiazdrowia.Controllers
{
    public class ArticlesController : Controller
    {
        private sekretwitalnosciContext db = new sekretwitalnosciContext();

        // GET: Articles
        public ActionResult Index()
        {
            return View(db.Articles.ToList());
        }

        // GET: Articles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Articles articles = db.Articles.Find(id);
            if (articles == null)
            {
                return HttpNotFound();
            }
            return View(articles);
        }

        // GET: Articles/Create
        public ActionResult Create()
        {
            ViewBag.LayoutType = new List<string>() { "s-picture", "l-picture", "xxl-picture" };
            ViewBag.Categories = new List<string>() { "TCM", "Matka&Dziecko", "Na zdrowie", "Przepisy", "Książki" };
            return View();
        }

        // POST: Articles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([Bind(Include = "LayoutType,Category,Title,ShortContent,Content,ImgPath,ImgMiniPath,Tags")] Articles articles, IEnumerable<HttpPostedFileBase> files)
        {
            var filepath = String.Empty;
            var fileNameList = new List<string>();

            foreach (var file in files)
            {
                if (null != file && file.ContentLength > 0)
                {
                    filepath = Path.Combine(
                        HttpContext.Server.MapPath("~/Assets/img/"),
                        Path.GetFileName(file.FileName)
                    );
                    file.SaveAs(filepath);
                    fileNameList.Add(filepath);
                }
            }
            
            ImageHelper.PrepareImage(fileNameList[1], articles.LayoutType);

            if (ModelState.IsValid)
            {
                articles.ImgPath = fileNameList[0];
                articles.ImgMiniPath = fileNameList[1];
                articles.Date = DateTime.Now;
                db.Articles.Add(articles);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(articles);
        }

        // GET: Articles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Articles articles = db.Articles.Find(id);
            if (articles == null)
            {
                return HttpNotFound();
            }
            return View(articles);
        }

        // POST: Articles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ArticleId,LayoutType,Category,Title,ShortContent,Content,ImgPath,ImgMiniPath,Tags,Date")] Articles articles)
        {
            if (ModelState.IsValid)
            {
                db.Entry(articles).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(articles);
        }

        // GET: Articles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Articles articles = db.Articles.Find(id);
            if (articles == null)
            {
                return HttpNotFound();
            }
            return View(articles);
        }

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Articles articles = db.Articles.Find(id);
            db.Articles.Remove(articles);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
