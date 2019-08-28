using ComicWeb.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace ComicWeb.Controllers
{
    public class HomeController : Controller
    {
        ComicWebEntities db = new ComicWebEntities();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            Session["userid"] = null;
            Session["username"] = null;
            Session["fullname"] = null;
            Session["avatar"] = null;
            return View();
        }

        [HttpPost]
        public ActionResult Login(Administrators L)
        {
            var Admin = db.Administrators.SingleOrDefault(x => x.Username == L.Username && x.Passwords == L.Passwords && x.isAdmin == 1 && x.Allowed == 1);
            if (Admin != null)
            {
                Session["userid"] = Admin.UserID;
                Session["username"] = Admin.Username;
                Session["fullname"] = Admin.Fullname;
                Session["avatar"] = Admin.Avatar;
                Session["IsAdmin"] = Admin.isAdmin;
                return Redirect("/Admin/Home/Index");
            }
            var user = db.Administrators.SingleOrDefault(x => x.Username == L.Username && x.Passwords == L.Passwords && x.Allowed == 1);
            if (user != null)
            {
                Session["userid"] = user.UserID;
                Session["username"] = user.Username;
                Session["fullname"] = user.Fullname;
                Session["avatar"] = user.Avatar;
                //Session["IsAdmin"] = null;
                return Redirect("/Home/Index");
            }
            var user1 = db.Administrators.SingleOrDefault(x => x.Username == L.Username && x.Passwords == L.Passwords);
            if (user1 != null)
            {
                ViewBag.error = "Tài Khoản của bạn đã bị khóa";
            }
            return View();
        }

        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registration(Administrators R)
        {
            if (ModelState.IsValid)
            {
                Administrator user = new Administrator();
                user.Fullname = R.Fullname;
                user.Username = R.Username;
                user.Passwords = R.Passwords;
                user.Email = R.Email;
                user.isAdmin = 0;
                user.Allowed = 1;
                user.Avatar = "boy.png";
                db.Administrators.Add(user);
                db.SaveChanges();
                return RedirectToAction("Login");
            }
            return View();
        }
    }
}