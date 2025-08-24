using System.Web.Mvc;

namespace AutoPartsStore.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login() => View();

        [HttpPost]
        public ActionResult Login(string login, string password)
        {
            if (login == "admin" && password == "111111")
            {
                Session["Role"] = "admin";
                return RedirectToAction("Index", "Staff");
            }
            if (login == "user" && password == "123456")
            {
                Session["Role"] = "user";
                return RedirectToAction("Create", "Application");
            }
            ViewBag.Error = "Невірний логін або пароль.";
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
    }
}