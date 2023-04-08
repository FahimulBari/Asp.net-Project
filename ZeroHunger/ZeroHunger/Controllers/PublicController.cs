using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using ZeroHunger.Context;
using ZeroHunger.Models;

namespace ZeroHunger.Controllers
{
    public class PublicController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            if (Session["user"] != null)
            {
                return RedirectToAction("Home","User");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginData loginData)
        {
            if (ModelState.IsValid)
            {
                //User Search
                MyDatabase db = new MyDatabase();
                var user = (from u in db.users
                            where u.Id.Equals(loginData.Id)
                            && u.Password.Equals(loginData.password)
                            select u).SingleOrDefault();

                //User Check
                if (user != null)
                {
                    Session["user"] = user;
                    return RedirectToAction("Home", "User", user);
                }
            }

            return View();
        }

        public ActionResult SignupSuccessful()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Signup()
        {
            if (Session["user"] != null)
            {
                return RedirectToAction("Home", "User");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Signup(User user)
        {
            if (ModelState.IsValid)
            {
                var db = new MyDatabase();
                var userdata = db.users.Add(user);
                db.SaveChanges();
                TempData["Id"] = userdata.Id;
                return RedirectToAction("SignupSuccessful","Public");
            }

            return View();
        }
    }
}