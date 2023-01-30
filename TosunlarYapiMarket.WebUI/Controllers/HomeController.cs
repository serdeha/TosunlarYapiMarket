using Microsoft.AspNetCore.Mvc;

namespace TosunlarYapiMarket.WebUI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
