using System;
using System.IO;
using System.Runtime.Caching;
using System.Web.Caching;
using System.Web;
using System.Web.Mvc;

namespace RefactoringCSharp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            // get buildVersion from Cache if available
            if (HttpContext.Current.Cache["time"] == null)
            {
                System.Web.HttpContext.Current.Cache["time"] = DateTime.Now;
            }

            ViewBag.Time = ((DateTime)System.Web.HttpContext.Current.Cache["time"]).ToString();

            //ViewBag.BuildVersion = fileContent;

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}