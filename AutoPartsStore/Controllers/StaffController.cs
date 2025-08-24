using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AutoPartsStore.Data;
using AutoPartsStore.Models;

namespace AutoPartsStore.Controllers
{
    public class StaffController : Controller
    {
        private HRContext db = new HRContext();

        private bool IsAdmin() => Session["Role"]?.ToString() == "admin";

        public ActionResult Index(string department, string position, DateTime? startDate, DateTime? endDate, int page = 1)
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");

            int pageSize = 5; 

            var employees = db.Employees.AsQueryable();

            if (!string.IsNullOrEmpty(department))
            {
                employees = employees.Where(e => e.Department == department);
            }
            if (!string.IsNullOrEmpty(position))
            {
                employees = employees.Where(e => e.Position == position);
            }
            if (startDate.HasValue)
            {
                employees = employees.Where(e => e.CreatedAt >= startDate.Value);
            }
            if (endDate.HasValue)
            {
                var inclusiveEndDate = endDate.Value.AddDays(1);
                employees = employees.Where(e => e.CreatedAt < inclusiveEndDate);
            }

            var totalCount = employees.Count();
            var pagedEmployees = employees
                .OrderByDescending(e => e.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            ViewBag.Department = department;
            ViewBag.Position = position;
            ViewBag.StartDate = startDate;
            ViewBag.EndDate = endDate;
            ViewBag.Departments = db.Employees.Select(e => e.Department).Distinct().OrderBy(d => d).ToList();
            ViewBag.Positions = db.Employees.Select(e => e.Position).Distinct().OrderBy(p => p).ToList();

            return View(pagedEmployees);
        }

        public ActionResult Details(int? id)
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var emp = db.Employees.Find(id);
            if (emp == null) return HttpNotFound();
            return View(emp);
        }

        public ActionResult Create()
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee emp)
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");
            if (ModelState.IsValid)
            {

                db.Employees.Add(emp);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(emp);
        }

        public ActionResult Edit(int? id)
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var emp = db.Employees.Find(id);
            if (emp == null) return HttpNotFound();
            return View(emp);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Employee emp)
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");
            if (ModelState.IsValid)
            {
                db.Entry(emp).State = System.Data.Entity.EntityState.Modified;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(emp);
        }

        public ActionResult Delete(int? id)
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var emp = db.Employees.Find(id);
            if (emp == null) return HttpNotFound();
            return View(emp);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var emp = db.Employees.Find(id);
            if (emp != null)
            {
                db.Employees.Remove(emp);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}