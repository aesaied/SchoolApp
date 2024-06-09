using Microsoft.AspNetCore.Mvc;

namespace SchoolApp.Controllers
{
    public class EnrollmentsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
