using System.Web.Mvc;

namespace Movies.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Movie database app.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "The movies company.";

            return View();
        }
    }
}