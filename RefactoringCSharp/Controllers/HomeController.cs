using System;
using System.IO;
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
            HttpContext context = System.Web.HttpContext.Current;

            string timeToDisplay;
            var timeFromCache = context.Cache["time"];
            if (timeFromCache == null)
            {
                context.Cache["time"] = DateTime.Now;
                timeToDisplay = DateTime.Now.ToString();
            }
            else
            {
                timeToDisplay = timeFromCache + " Cache Hit!";
            }

            ViewBag.Time = timeToDisplay;


            // get buildVersion from Cache if available
            string buildVersion = context.Cache["BuildVersion"] as string;
            if (string.IsNullOrEmpty(buildVersion))
            {
                using (StreamReader sr = System.IO.File.OpenText(Server.MapPath("~/BuildVersion.txt")))
                {
                    buildVersion = sr.ReadToEnd();
                    context.Cache.Insert("BuildVersion", buildVersion,
                        new System.Web.Caching.CacheDependency(Server.MapPath("~/SampleFile.txt")));
                }
            }
            else
            {
                buildVersion += " Cache Hit!";
            }

            ViewBag.BuildVersion = buildVersion;
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}