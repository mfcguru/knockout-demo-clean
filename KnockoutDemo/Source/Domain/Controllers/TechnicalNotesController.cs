using Microsoft.AspNetCore.Mvc;

namespace KnockoutDemo.Source.Domain.Controllers
{
    public class TechnicalNotesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
