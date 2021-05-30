using Microsoft.AspNetCore.Mvc;

namespace MyCrm.UI.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}