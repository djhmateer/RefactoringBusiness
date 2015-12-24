using System.Web.Mvc;

namespace RefactoringCSharp.Controllers
{
    public class PageController : Controller
    {
        public ActionResult ORM()
        {
            // Goal here is to read from a database

            return View();
        }
    }
}