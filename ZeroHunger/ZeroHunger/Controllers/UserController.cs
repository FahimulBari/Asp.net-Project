using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZeroHunger.Context;
using ZeroHunger.Models;

namespace ZeroHunger.Controllers
{
    public class UserController : Controller
    {

        public ActionResult Home()
        {
            if(Session["user"] == null) return RedirectToAction("Login", "Public");
            return View();
            
        }

        public ActionResult Dashboard()
        {
            if (Session["user"] == null) return RedirectToAction("Login", "Public");

            User user = new User();
            user = (User)Session["user"];

            if (user.UserType == "Admin")
                {
                    MyDatabase db = new MyDatabase();
                    return View(db.tasks.ToList());
                }

                if (user.UserType == "Restaurant")
                {
                    MyDatabase db = new MyDatabase();
                    var task = (from t in db.tasks
                                where t.Restaurant == user.Name
                                select t);
                    return View(task.ToList());
                }

                else
                {
                    MyDatabase db = new MyDatabase();
                    var task = (from t in db.tasks
                                where t.Employee == user.Name
                                select t);
                    return View(task.ToList());
                }
            
        }

        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Login","Public");
        }

    }
}