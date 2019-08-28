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
    public class AdministratorController : Controller
    {
        ComicWebEntities db = new ComicWebEntities();
        // GET: Admin/Administrator
        public ActionResult Index()
        {
            return View(db.Administrators.ToList());
        }

        //Thêm mới thông tin
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Administrator administrator, HttpPostedFileBase fileload)
        {
            if (fileload != null)
            {
                var fileinfo = Path.GetFileName(fileload.FileName);
                var path = Path.Combine(Server.MapPath("/Picture/Avatar/"), fileinfo);
                if (System.IO.File.Exists(path))
                {
                    administrator.Avatar = fileload.FileName;
                    db.Administrators.Add(administrator);
                    db.SaveChanges();
                }
                else
                {
                    fileload.SaveAs(path);
                    administrator.Avatar = fileload.FileName;
                    db.Administrators.Add(administrator);
                    db.SaveChanges();
                }
            }
            else
            {
                administrator.Avatar = "boy.png";
                db.Administrators.Add(administrator);
                db.SaveChanges();
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
            Administrator administrator = db.Administrators.Find(id);
            if (administrator == null)
            {
                return HttpNotFound();
            }
            return View(administrator);
        }

        [HttpPost]
        public ActionResult Edit(Administrator administrator, HttpPostedFileBase fileload)
        {
            if (ModelState.IsValid)
            {
                if (fileload != null)
                {
                    var fileinfo = Path.GetFileName(fileload.FileName);
                    var path = Path.Combine(Server.MapPath("/Picture/Avatar/"), fileinfo);
                    if (System.IO.File.Exists(path))
                    {
                        administrator.Avatar = fileload.FileName;
                        db.Entry(administrator).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    else
                    {
                        fileload.SaveAs(path);
                        administrator.Avatar = fileload.FileName;
                        db.Entry(administrator).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                else
                {
                    db.Entry(administrator).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            return View(administrator);
        }

        //Xem chi tiết thông tin
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Administrator administrator = db.Administrators.Find(id);
            if (administrator == null)
            {
                return HttpNotFound();
            }
            return View(administrator);
        }


        //Xóa thông tin
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Administrator administrator = db.Administrators.Find(id);
            if (administrator == null)
            {
                return HttpNotFound();
            }
            return View(administrator);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Administrator administrator = db.Administrators.Find(id);
            db.Administrators.Remove(administrator);
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