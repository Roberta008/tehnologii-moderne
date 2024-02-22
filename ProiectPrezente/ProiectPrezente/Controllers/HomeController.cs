using Microsoft.AspNetCore.Mvc;
using ProiectPrezente.Database;

namespace ProiectPrezente.Controllers
{
#pragma warning disable CS9113 // Parameter is unread.
    // Controllerul principal al aplicației, responsabil pentru paginile principale și de contact
    // Prin intermediul constructorului, se primesc opțiunile de configurare a logger-ului și contextul bazei de date
    public class HomeController(ILogger<HomeController> appLogger, DatabaseContext databaseContext) : Controller
#pragma warning restore CS9113 // Parameter is unread.
    {
        // GET: ~/
        public IActionResult Index()
        {
            // Returnează vizualizarea asociată paginii principale (Index)
            return View();
        }

        // GET: ~/Home/Contact
        public IActionResult Contact()
        {
            // Returnează vizualizarea asociată paginii de contact (Contact)
            return View();
        }
    }
}
