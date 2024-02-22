using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProiectPrezentaOnline.Extensions;
using ProiectPrezente.Database;
using ProiectPrezente.Models.Users;

namespace ProiectPrezente.Controllers
{
    // Constructorul care primește un obiect DatabaseContext prin injecție de dependențe
    public class AuthenticationController(DatabaseContext _databaseContext) : Controller
    {
        // GET: ~/Authentication/
        // Acțiunea pentru afișarea paginii de autentificare
        public IActionResult Index()
        {
            // Daca suntem autentificați nu vom putea accesa din URL ~/Authentication
            User? authenticatedUser = HttpContext.Session.GetObject<User>(cheieObiect: "authenticatedUser");
            if (authenticatedUser != null)
            {
                return RedirectToAction(actionName: "Index", controllerName: "Home");
            }
            return View();
        }

        // POST: ~/Authentication/Login
        [HttpPost]
        // Acțiunea pentru procesarea datelor de autentificare la submit-ul formularului
        public IActionResult Login([Bind("Username, Password")] User studentInfo)
        {
            // Căutarea studentului în baza de date pe baza informațiilor primite
            User? foundUser = _databaseContext.Users
                .Include
                    (
                        navigationPropertyPath: currentUser => currentUser.UserDetails
                    )
                .SingleOrDefault
                    (
                        predicate: currentStudent =>
                        currentStudent.Username.Equals(studentInfo.Username) 
                            && currentStudent.Password.Equals(studentInfo.Password)
                    );
            // Verificarea dacă studentul a fost găsit sau nu
            if (foundUser == null)
            {
                // Setarea unui mesaj de eroare în ViewBag pentru a fi afișat în view
                ViewBag.Message = "Numele de utilizator sau parola introduse sunt incorecte.";
                // Redirecționarea către pagina de autentificare cu mesajul de eroare
                return View(viewName: "Index");
            }

            // Salvarea obiectului Student în sesiunea HTTP după autentificare
            HttpContext.Session.SetObject(cheieObiect: "authenticatedUser", obiectNeserializat: foundUser);
            // Redirecționarea către pagina principală după autentificare reușită
            return (foundUser.UserType == UserType.Admin)
                ? RedirectToAction(actionName: "Index", controllerName: "Admin")
                : (foundUser.UserType == UserType.Professor)
                    ? RedirectToAction(actionName: "Index", controllerName: "Professors")
                    : RedirectToAction(actionName: "Index", controllerName: "Students");
        }

        // GET ~/Authentication/Disconnect
        // Acțiunea pentru deconectare a utilizatorului
        public IActionResult Disconnect()
        {
            // Curățarea întregii sesiuni la deconectare
            HttpContext.Session.Clear();
            // Redirecționarea către pagina principală (Index) după deconectare
            return RedirectToAction(actionName: "Index", controllerName: "Home");
        }
    }
}
