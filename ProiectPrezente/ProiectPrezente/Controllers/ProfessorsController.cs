using Microsoft.AspNetCore.Mvc;

namespace ProiectPrezente.Controllers
{
    public class ProfessorsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
