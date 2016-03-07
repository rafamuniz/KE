using System.Web.Mvc;

namespace KarmicEnergy.Web.Controllers
{
    public class HomeController : Controller
    {
        [Authorize()]
        public ActionResult Index()
        {
            return View();
        }
    }
}