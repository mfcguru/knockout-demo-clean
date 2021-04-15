using Microsoft.AspNetCore.Mvc;

namespace KnockoutDemo.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
