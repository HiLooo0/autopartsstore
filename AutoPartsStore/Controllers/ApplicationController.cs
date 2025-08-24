using System;
using System.Linq;
using System.Web.Mvc;
using AutoPartsStore.Data;
using AutoPartsStore.Models;

namespace AutoPartsStore.Controllers
{
    public class ApplicationController : Controller
    {
        private HRContext db = new HRContext();

        public ActionResult Create()
        {
            if (Session["Role"]?.ToString() != "user")
                return RedirectToAction("Login", "Account");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken] 
        public ActionResult Create(Application app)
        {
            if (Session["Role"]?.ToString() != "user")
                return RedirectToAction("Login", "Account");

            if (ModelState.IsValid)
            {
                app.SubmissionDate = DateTime.Now;

                db.Applications.Add(app);
                db.SaveChanges();
                TempData["Message"] = "Заявку успішно подано!";
                return RedirectToAction("Create");
            }

            return View(app);
        }

        public ActionResult Inbox(int page = 1)
        {
            if (Session["Role"]?.ToString() != "admin")
                return RedirectToAction("Login", "Account");

            int pageSize = 5;
            var totalCount = db.Applications.Count();

            var applications = db.Applications
                .OrderBy(a => a.SubmissionDate) 
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            return View(applications);
        }

        [HttpPost]
        public ActionResult Approve(int id)
        {
            if (Session["Role"]?.ToString() != "admin")
                return RedirectToAction("Login", "Account");

            var app = db.Applications.Find(id);
            if (app != null)
            {
                var emp = new Employee
                {
                    FullName = app.FullName,
                    Position = app.DesiredPosition,
                    Department = app.Department,

                    CreatedAt = DateTime.Now
                };

                db.Employees.Add(emp);
                db.Applications.Remove(app);
                db.SaveChanges();
                TempData["Info"] = $"Заявку {app.FullName} прийнято і додано до працівників.";
            }

            return RedirectToAction("Inbox");
        }

        [HttpPost]
        public ActionResult Reject(int id)
        {
            if (Session["Role"]?.ToString() != "admin")
                return RedirectToAction("Login", "Account");

            var app = db.Applications.Find(id);
            if (app != null)
            {
                db.Applications.Remove(app);
                db.SaveChanges();
                TempData["Info"] = $"Заявку {app.FullName} відхилено.";
            }

            return RedirectToAction("Inbox");
        }
    }
}