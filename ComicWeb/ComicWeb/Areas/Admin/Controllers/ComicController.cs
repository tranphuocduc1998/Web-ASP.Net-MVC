using ComicWeb.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ComicWeb.Areas.Admin.Controllers
{
    public class ComicController : Controller
    {
        ComicWebEntities db = new ComicWebEntities();
        // GET: Admin/Comic
        public ActionResult Index()
        {
            return View(db.Comics.ToList());
        }

        //Thêm mới thông tin
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.UserID = new SelectList(db.Administrators, "UserID", "Username");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Comic comic, HttpPostedFileBase fileload)
        {
            if(comic.ViewNo==null)
            {
                comic.ViewNo = 0;
            }
            comic.CreateDate = DateTime.Now;
            if (fileload != null)
            {
                var fileinfo = Path.GetFileName(fileload.FileName);
                var path = Path.Combine(Server.MapPath("/Picture/Comic_Img/"), fileinfo);
                if (System.IO.File.Exists(path))
                {
                    comic.Picture = fileload.FileName;
                    db.Comics.Add(comic);
                    db.SaveChanges();
                }
                else
                {
                    fileload.SaveAs(path);
                    comic.Picture = fileload.FileName;
                    db.Comics.Add(comic);
                    db.SaveChanges();
                }
            }
            else
            {
                ViewBag.Error = "Bạn chưa chọn ảnh cho truyện!";
                return View();
            }
            return Redirect("Index");
        }

        //Sửa thông tin
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comic comic = db.Comics.Find(id);
            if (comic == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserID = new SelectList(db.Administrators, "UserID", "Username", comic.UserID);
            return View(comic);
        }

        [HttpPost]
        public ActionResult Edit(Comic comic, HttpPostedFileBase fileload)
        {
            if (ModelState.IsValid)
            {
                if (fileload != null)
                {
                    var fileinfo = Path.GetFileName(fileload.FileName);
                    var path = Path.Combine(Server.MapPath("/Picture/Avatar/"), fileinfo);
                    if (System.IO.File.Exists(path))
                    {
                        comic.Picture = fileload.FileName;
                        db.Entry(comic).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    else
                    {
                        fileload.SaveAs(path);
                        comic.Picture = fileload.FileName;
                        db.Entry(comic).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                else
                {
                    db.Entry(comic).State = EntityState.Modified;
                    db.SaveChanges();
                }
                ViewBag.UserID = new SelectList(db.Administrators, "UserID", "Username", comic.UserID);
                return RedirectToAction("Index");
            }
            return View(comic);
        }

        //Xem chi tiết thông tin
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comic comic = db.Comics.Find(id);
            if (comic == null)
            {
                return HttpNotFound();
            }
            return View(comic);
        }

        //Xóa thông tin
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comic comic = db.Comics.Find(id);
            if (comic == null)
            {
                return HttpNotFound();
            }
            return View(comic);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Comic comic = db.Comics.Find(id);
            db.Comics.Remove(comic);
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