using System;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using AutoPartsStore.Models;

namespace AutoPartsStore
{
    public class Global : HttpApplication
    {
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            string lang = "uk-UA"; // мова за замовчуванням

            HttpCookie cookie = HttpContext.Current.Request.Cookies["lang"];
            if (cookie != null && !string.IsNullOrEmpty(cookie.Value))
            {
                lang = cookie.Value;
            }

            var culture = new System.Globalization.CultureInfo(lang);
            System.Threading.Thread.CurrentThread.CurrentCulture = culture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = culture;
        }


        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(System.Web.Routing.RouteTable.Routes);
        }
    }
}
