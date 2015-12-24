using System;
using System.IO;
using System.Runtime.Caching;
using System.Web;
using System.Web.Caching;
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
            Cache cache = HttpRuntime.Cache;

            // time cache for testing
            string timeToDisplay;
            var timeFromCache = cache["time"];
            if (timeFromCache == null)
            {
                cache["time"] = DateTime.Now;
                timeToDisplay = DateTime.Now.ToString();
            }
            else timeToDisplay = timeFromCache + " Cache Hit!";
            ViewBag.Time = timeToDisplay;


            // get buildVersion from Cache if available
            string buildVersion = cache["BuildVersion"] as string;
            if (string.IsNullOrEmpty(buildVersion))
            {
                using (StreamReader sr = System.IO.File.OpenText(Server.MapPath("~/BuildVersion.txt")))
                {
                    buildVersion = sr.ReadToEnd();
                    cache.Insert("BuildVersion", buildVersion, new CacheDependency(Server.MapPath("~/BuildVersion.txt")));
                }
            }
            else buildVersion += " Cache Hit!";
            ViewBag.BuildVersion = buildVersion;

            // Use the general MemoryCache (so not just for ASP.NET)
            MemoryCache oc = MemoryCache.Default;
            // Expire items from the oc cache
            var policy = new CacheItemPolicy { AbsoluteExpiration = new DateTimeOffset(DateTime.Now.AddSeconds(10)) };
            string timeToDisplayOC = (string)oc.Get("timeOC");
            if (string.IsNullOrEmpty(timeToDisplayOC))
            {
                bool result = oc.Add("timeOC", DateTime.Now.ToString(), policy);
                timeToDisplayOC = DateTime.Now.ToString();
            }
            else
            {
                timeToDisplayOC += " Cache Hit!";
            }
            ViewBag.TimeToDisplayOC = timeToDisplayOC;

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
    }
}