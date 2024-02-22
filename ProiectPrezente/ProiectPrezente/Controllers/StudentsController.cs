using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProiectPrezentaOnline.Extensions;
using ProiectPrezente.Database;
using ProiectPrezente.Models;
using ProiectPrezente.Models.Classes;
using ProiectPrezente.Models.Users;

namespace ProiectPrezente.Controllers
{
    public class StudentsController(DatabaseContext _databaseContext) : Controller
    {
        public IActionResult Index()
        {
            if (!IsAuthenticated())
            {
                return RedirectToAction(actionName: "Index", controllerName: "Home");
            }
            User? authenticatedUser = HttpContext.Session.GetObject<User>(cheieObiect: "authenticatedUser");
            List<Enrollment> enrolledClasses =
                [.. _databaseContext.Enrollments
                    .Include(navigationPropertyPath: currentEnrollment => currentEnrollment.Class)
                    .Where
                        (
                            predicate: currentEnrollment => currentEnrollment.StudentID == authenticatedUser.UserID
                        )
                ];
            List<Attendance> userAttendances = 
                [.. _databaseContext.Attendances
                    .Where
                        (
                            predicate: currentAttendance => currentAttendance.StudentID == authenticatedUser.UserID
                        )
                ];
            
            return View
                (
                    model: new StudentsIndexViewModel
                        (
                            enrolledClasses: enrolledClasses,
                            userAttendances: userAttendances
                        )
                );
        }

        private bool IsAuthenticated()
        {
            User? authenticatedUser = HttpContext.Session.GetObject<User>(cheieObiect: "authenticatedUser");
            return authenticatedUser != null;
        }
    }
}
