using System;
using System.Linq;
using System.Web.Mvc;
using AutoPartsStore.Data;
using AutoPartsStore.Models;

namespace AutoPartsStore.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult About()
        {
            ViewBag.Title = "Про сайт";
            return View();
        }
    }
}

