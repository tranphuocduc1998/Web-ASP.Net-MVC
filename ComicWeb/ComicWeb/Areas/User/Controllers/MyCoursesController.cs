using ComicWeb.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ComicWeb.Areas.User.Controllers
{
    public class MyCoursesController : Controller
    {
        ComicWebEntities db = new ComicWebEntities();
        // GET: User/MyCourses
        public ActionResult Index()
        {
            Administrator user = db.Administrators.Find(Session["userid"]);
            return View(user);
        }

        public ActionResult user_out()
        {
            return Redirect("/Home/Login");
        }
    }
}