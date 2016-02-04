using System;
using System.Collections.Generic;
using System.Runtime.Caching;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using CacheItemPriority = System.Web.Caching.CacheItemPriority;

namespace RefactoringCSharp.Controllers
{
    // Enumeration
    public enum WebsiteStatus
    {
        NotLoggedIn = 0,
        NormalUser = 1,
        Administrator = 2
    }

    // Class
    public class HomeController : Controller
    {
        // String constant
        const string EndOfWelcomeMessage = "Refactoring!";

        // Fields **what other names**
        Random _randomiser = new Random();
        IList<string> _startOfWelcomeMessages;

        // Property
        public string Name { get; set; }


        // Constructor
        public HomeController()
        {
            _startOfWelcomeMessages = new List<string> { "Welcome to", "Hello from", "Rocking out with" };
            int thing = (int)WebsiteStatus.Administrator;
        }

        // Method
        public ActionResult Index()
        {
            // Local variable
            int age = 32;
            var index = _randomiser.Next(_startOfWelcomeMessages.Count);
            ViewBag.Message = _startOfWelcomeMessages[index] + " " + EndOfWelcomeMessage;
            return View();
        }

        // Method with a parameter **what other names ie pass a parameter??**
        public void DoSomething(int age)
        { }

        public ActionResult About()
        {
            ViewBag.BuildVersion = typeof(MvcApplication).Assembly.GetName().Version;

            var cache = HttpRuntime.Cache;

            // time cache for testing - expires after 5s
            string timeToDisplay;
            var timeFromCache = cache["time"];
            if (timeFromCache == null)
            {
                cache.Add("time", DateTime.Now.ToString(), null, DateTime.Now.AddSeconds(5), Cache.NoSlidingExpiration, CacheItemPriority.Default, null);
                timeToDisplay = (string)cache["time"];
            }
            else timeToDisplay = timeFromCache + " Cache Hit!";
            ViewBag.Time = timeToDisplay;

            // time cache for testing - expires after 3s of cache not being hit
            string timeToDisplaySliding;
            var timeFromCacheSliding = cache["timeSliding"];
            if (timeFromCacheSliding == null)
            {
                //cache["time"] = DateTime.Now;
                cache.Add("timeSliding", DateTime.Now.ToString(), null, Cache.NoAbsoluteExpiration, new TimeSpan(0, 0, 0, 3), CacheItemPriority.Default, null);
                timeToDisplaySliding = (string)cache["timeSliding"];
            }
            else timeToDisplaySliding = timeFromCacheSliding + " Cache Hit!";
            ViewBag.TimeToDisplaySliding = timeToDisplaySliding;


            // Use the general MemoryCache (so not just for ASP.NET)
            MemoryCache oc = MemoryCache.Default;
            // Expire item from the oc cache to 10s
            var policy = new CacheItemPolicy { AbsoluteExpiration = new DateTimeOffset(DateTime.Now.AddSeconds(10)) };
            string timeToDisplayOC = (string)oc.Get("timeOC");
            if (string.IsNullOrEmpty(timeToDisplayOC))
            {
                oc.Add("timeOC", DateTime.Now.ToString(), policy);
                timeToDisplayOC = DateTime.Now.ToString();
            }
            else timeToDisplayOC += " Cache Hit!";

            ViewBag.TimeToDisplayOC = timeToDisplayOC;

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        public ActionResult Summary()
        {
            return View();
        }
    }
}