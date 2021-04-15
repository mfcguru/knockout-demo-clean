using Microsoft.AspNetCore.Mvc;

namespace KnockoutDemo.Source.Domain.Controllers
{
    public class ReportController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
