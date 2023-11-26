using Microsoft.AspNetCore.Mvc;
using ProiectPrezentaOnline.Models;

namespace ProiectPrezentaOnline.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
