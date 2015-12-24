using System.Collections.Generic;
using System.Data.Entity;
using System.Web.Mvc;
using System.Linq;

namespace RefactoringCSharp.Controllers
{
    public class PageController : Controller
    {
        public ActionResult ORM()
        {
            // Read from a database
            List<Joke> listOfJokes;
            using (var context = new DavesJokes())
            {
                listOfJokes = context.Jokes.Take(5).ToList();
            }
            return View(listOfJokes);
        }
    }
}