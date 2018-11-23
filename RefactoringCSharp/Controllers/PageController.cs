using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using RefactoringCSharp;

namespace Dave.RefactoringCSharp.Controllers
{
    public class PageController : Controller
    {
        public ActionResult ORM()
        {
            //ViewBag.ConnectionString = ConfigurationManager.ConnectionStrings["DavesJokes"].ToString();

            //System.Threading.Thread.Sleep(2000);
            // Read from a database
            //List<Joke> listOfJokes;
            //using (var context = new DavesJokes())
            //{
            //    listOfJokes = context.Jokes.Take(5).ToList();
            //}
            //return View(listOfJokes);
            return View();
        }
    }
}