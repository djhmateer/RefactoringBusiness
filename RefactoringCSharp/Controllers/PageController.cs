using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace RefactoringCSharp.Controllers
{
    public class PageController : Controller
    {
        public ActionResult ORM()
        {
            //ViewBag.ConnectionString = ConfigurationManager.ConnectionStrings["DavesJokes"].ToString();

            // Read from a database
            List<Joke> listOfJokes;
            using (var context = new DavesJokes())
            {
                listOfJokes = context.Jokes.Take(5).ToList();
            }
            return View(listOfJokes);
            //return View();
        }
    }
}