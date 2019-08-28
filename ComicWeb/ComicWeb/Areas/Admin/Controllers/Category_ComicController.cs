using ComicWeb.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ComicWeb.Areas.Admin.Controllers
{
    public class Category_ComicController : Controller
    {
        ComicWebEntities db = new ComicWebEntities();
        // GET: Admin/Category_Comic
        public ActionResult Index()
        {
            var Ca_Co = db.Category_Comic.Include(a => a.Category).Include(b => b.Comic);
            return View(Ca_Co.ToList());
        }

        //Xem chi tiết thông tin
        public ActionResult Details(int? id, string id2)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category_Comic category_Comic = db.Category_Comic.Find(id,id2);
            if (category_Comic == null)
            {
                return HttpNotFound();
            }
            return View(category_Comic);
        }

        //Thêm thông tin
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Categoryname");
            ViewBag.ComicID = new SelectList(db.Comics, "ComicID", "Comicname");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Category_Comic category_Comic)
        {
            if (ModelState.IsValid)
            {
                db.Category_Comic.Add(category_Comic);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Categoryname", category_Comic.CategoryID);
            ViewBag.ComicID = new SelectList(db.Comics, "ComicID", "Comicname", category_Comic.ComicID);
            return View(category_Comic);
        }
        ////Sửa thông tin
        //public ActionResult Edit(int? id, string id2)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Category_Comic category_Comic = db.Category_Comic.Find(id, id2);
        //    if (category_Comic == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Categoryname", category_Comic.CategoryID);
        //    ViewBag.ComicID = new SelectList(db.Comics, "ComicID", "Comicname", category_Comic.ComicID);
        //    return View(category_Comic);
        //}


        //[HttpPost]
        //public ActionResult Edit(Category_Comic category_Comic)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(category_Comic).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Categoryname", category_Comic.CategoryID);
        //    ViewBag.ComicID = new SelectList(db.Comics, "ComicID", "Comicname", category_Comic.ComicID);
        //    return View(category_Comic);
        //}

        //Xóa thông tin
        public ActionResult Delete(int? id, string id2)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category_Comic category_Comic = db.Category_Comic.Find(id, id2);
            if (category_Comic == null)
            {
                return HttpNotFound();
            }
            return View(category_Comic);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult Delete(int id, string id2)
        {
            Category_Comic category_Comic = db.Category_Comic.Find(id, id2);
            db.Category_Comic.Remove(category_Comic);
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