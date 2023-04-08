using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZeroHunger.Context;
using ZeroHunger.Models;
using static System.Data.Entity.Infrastructure.Design.Executor;

namespace ZeroHunger.Controllers
{
    public class TaskController : Controller
    {
        [HttpGet]
        public ActionResult AddTask()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddTask(Task task)
        {
            if (ModelState.IsValid)
            {
                var db = new MyDatabase();
                db.tasks.Add(task);
                db.SaveChanges();
            }

            return View();
        }

        [HttpGet]
        public ActionResult EmployeeAssign(int Id)
        {
            if (Session["user"] == null) return RedirectToAction("Login", "Public");

            var db = new MyDatabase();
            var task = (from s in db.tasks
                              where s.Id == Id
                              select s).SingleOrDefault();

            if (task == null) return Content("Task Not Found!!");
            return View(task);
        }        
        
        [HttpPost]
        public ActionResult EmployeeAssign(Task newTask)
        {
            var db = new MyDatabase();
            var oldTask = (from s in db.tasks
                              where s.Id == newTask.Id
                              select s).SingleOrDefault();
            oldTask.Employee = newTask.Employee;
            oldTask.Status = "In-Progress";
            db.SaveChanges();
            return RedirectToAction("Dashboard","User");
        }

        public ActionResult TaskDone(int Id)
        {
            var db = new MyDatabase();
            var task = (from t in db.tasks
                           where t.Id == Id
                           select t).SingleOrDefault();

            task.Status = "Done";
            db.SaveChanges();
            return RedirectToAction("Dashboard", "User");
        }
    }
}