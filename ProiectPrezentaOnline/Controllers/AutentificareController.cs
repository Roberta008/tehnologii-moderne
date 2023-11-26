using Microsoft.AspNetCore.Mvc;
using ProiectPrezentaOnline.Data;
using ProiectPrezentaOnline.Extensions;
using ProiectPrezentaOnline.Models;

namespace ProiectPrezentaOnline.Controllers
{
    public class AutentificareController(DatabaseContext databaseContext) : Controller
    {
        // Aici o sa ajunga cand ne deconectam!
        public IActionResult Index()
        {
            ISession sesiuneCurenta = HttpContext.Session;
            if (sesiuneCurenta != null)
            {
                // Daca avem sesiune si utilizator o sa ne redirectionam catre pagina Index din HomeController!
                Utilizator? utilizatorAutentificat = sesiuneCurenta.GetObject<Utilizator>("UtilizatorAutentificat");
                if (utilizatorAutentificat != null)
                {
                    sesiuneCurenta.Remove("UtilizatorAutentificat");
                    return RedirectToAction(actionName: "Index", controllerName: "Home");
                }
            }
            return View();
        }

        // Cand trimitem informatii la Baza de Date (POST) ca sa cautam un Utilizator daca e autentificat sau nu
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(Utilizator obiectPrimit)
        {
            // O sa faca un SELECT pe baza de date AUTOMAT (databaseContext), FirstOrDefault returneaza primul obiect care respecta cerinta X sau NULL
            Utilizator? utilizatorGasit = databaseContext.Utilizatori.FirstOrDefault
            (
                predicate: obiectUtilizator =>
                    obiectUtilizator.NumeUtilizator.Equals(obiectPrimit.NumeUtilizator)
                        && obiectUtilizator.Parola.Equals(obiectPrimit.Parola)
            );

            // Daca ne-am autentificat cu succes, salvam utilizatorul in sesiune (pentru a salva starea)
            if (utilizatorGasit != null)
            {
                ISession sesiuneCurenta = HttpContext.Session;
                sesiuneCurenta.SetObject("UtilizatorAutentificat", utilizatorGasit);

                // Preluam prezentele la curs ale utilizatorului
                List<PrezentaCurs> prezenteCurs =
                [
                    .. databaseContext.PrezenteCursuri.Where
                    (
                        predicate: prezentaCurenta => prezentaCurenta.IDStudent == utilizatorGasit.Id
                    ),
                ];
                // Pentru fiecare prezenta ii atribuim cursul (pentru ca nu facea asta automat din anumite motive)
                prezenteCurs.ForEach
                (
                    action: prezentaCurenta =>
                    {
                        Curs cursCurent = databaseContext.Cursuri.First(predicate: cursCurent => cursCurent.ID == prezentaCurenta.IDCurs);
                        prezentaCurenta.Curs = cursCurent;
                    }
                );
                // Preluam prezentele la laboratoare si facem acelasi lucru ca si mai sus cu laboratoarele
                List<PrezentaLaborator> prezenteLaboratoare =
                [
                    .. databaseContext.PrezenteLaboratoare.Where
                    (
                        predicate: prezentaCurenta => prezentaCurenta.IDStudent == utilizatorGasit.Id
                    ),
                ];
                prezenteLaboratoare.ForEach
                (
                    action: prezentaCurenta =>
                    {
                        Laborator laboratorCurent = databaseContext.Laboratoare.First(predicate: laboratorCurent => laboratorCurent.ID == prezentaCurenta.IDLaborator);
                        prezentaCurenta.Laborator = laboratorCurent;
                    }
                );
                // Setam un obiect care contine listele cu prezente la cursuri si laboratoare in sesiune (ca sa le avem)
                sesiuneCurenta.SetObject("PrezenteStudent", new Prezente(prezenteCurs, prezenteLaboratoare));
                // Din TempData vom prelua mesajele ca sa le afisam cu toastr (chestia aia cu notificarile din colt, sus) - vezi in _Notifications.cs
                TempData["Success"] = "Te-ai conectat cu succes!";
                // Si ne redirectam catre Index (din Student)
                return RedirectToAction
                (
                    actionName: "Index",
                    controllerName: "Student"
                );
            }
            TempData["Warning"] = "Credentialele de autentificare sunt gresite!";
            return View(obiectPrimit);
        }
    }
}
