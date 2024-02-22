using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer.Server;
using ProiectPrezentaOnline.Extensions;
using ProiectPrezente.Database;
using ProiectPrezente.Models;
using ProiectPrezente.Models.Classes;
using ProiectPrezente.Models.Users;
using System.Globalization;

namespace ProiectPrezente.Controllers
{
    public class AdminController(DatabaseContext _databaseContext) : Controller
    {
        // GET: ~/Admin
        public IActionResult Index()
        {
            if (!IsAuthenticated())
            {
                return RedirectToAction(actionName: "Index", controllerName: "Home");
            }
            return View();
        }

        // GET: ~/Admin/ManageStudents
        public IActionResult ManageStudents()
        {
            if (!IsAuthenticated())
            {
                return RedirectToAction(actionName: "Index", controllerName: "Home");
            }
            List<User> allStudents = [.. _databaseContext.Users.Where(c => c.UserType == UserType.Student).Include(c => c.UserDetails)];
            return View(allStudents);
        }

        // GET: ~/Admin/ManageClasses
        public IActionResult ManageClasses()
        {
            if (!IsAuthenticated())
            {
                return RedirectToAction(actionName: "Index", controllerName: "Home");
            }
            List<Class> listOfClasses =
                [.. _databaseContext.Classes.Include(navigationPropertyPath: currentClass => currentClass.Teachers)];
            List<User> listOfTeachers =
                [.. _databaseContext.Users.Include(navigationPropertyPath: currentTeacher => currentTeacher.UserDetails)
                    .Where(predicate: currentUser => currentUser.UserType == UserType.Professor)];
            return View(model: new ManageClassesViewModel(listOfClasses: listOfClasses, listOfTeachers: listOfTeachers));
        }

        // GET: ~/Admin/ManageProfessors
        public IActionResult ManageProfessors()
        {
            if (!IsAuthenticated())
            {
                return RedirectToAction(actionName: "Index", controllerName: "Home");
            }
            return View();
        }


        // GET: ~/Admin/DisplayCoursesAndAttendances?studentID={StudentID:long}
        public IActionResult DisplayCoursesAndAttendances(long studentID)
        {
            if (!IsAuthenticated())
            {
                return RedirectToAction(actionName: "Index", controllerName: "Home");
            }
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            User foundStudent = _databaseContext.Users
                .Include(navigationPropertyPath: currentStudent => currentStudent.UserDetails)
                .Include(navigationPropertyPath: currentStudent => currentStudent.Attendances)
                .Include(navigationPropertyPath: currentStudent => currentStudent.Enrollments)
                    .ThenInclude(navigationPropertyPath: currentEnrollment => currentEnrollment.Class)
                .SingleOrDefault(predicate: currentStudent => currentStudent.UserID == studentID);
            List<Class> listOfClasses = _databaseContext.Classes.ToList();
#pragma warning disable CS8604 // Possible null reference argument.
            return View(model: new CoursesAndAttendancesModel(foundStudent: foundStudent, listOfClasses: listOfClasses));
#pragma warning restore CS8604 // Possible null reference argument.
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
        }

        public IActionResult CreateStudent()
        {
            if (!IsAuthenticated())
            {
                return RedirectToAction(actionName: "Index", controllerName: "Home");
            }
            return View();
        }

        [HttpPost]
        public IActionResult CreateStudent(User newUserRequest)
        {
            _databaseContext.Users.Add(entity: newUserRequest);
            _databaseContext.SaveChanges();
            return RedirectToAction(actionName: "ManageStudents");
        }

        public IActionResult ModifyStudent(long studentID)
        {
            if (!IsAuthenticated())
            {
                return RedirectToAction(actionName: "Index", controllerName: "Home");
            }
            User? foundUser = _databaseContext.Users.Include(navigationPropertyPath: studentEntity => studentEntity.UserDetails)
                .SingleOrDefault(currentUser => currentUser.UserID == studentID);
            if (foundUser == null)
            {
                NotFound();
            }
            return View(viewName: "ModifyStudent", model: foundUser);
        }

        [HttpPost]
        public IActionResult ModifyStudent(User modifiedUserRequest)
        {
            if (!IsAuthenticated())
            {
                return RedirectToAction(actionName: "Index", controllerName: "Home");
            }
            User? foundUser = _databaseContext.Users.Include(navigationPropertyPath: studentEntity => studentEntity.UserDetails)
                .SingleOrDefault(currentUser => currentUser.UserID == modifiedUserRequest.UserID);
            if (foundUser == null)
            {
                NotFound();
            }
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            foundUser.Username = modifiedUserRequest.Username;
            foundUser.Password = modifiedUserRequest.Password;
            foundUser.UserDetails.FirstName = modifiedUserRequest.UserDetails.FirstName;
            foundUser.UserDetails.LastName = modifiedUserRequest.UserDetails.LastName;
            foundUser.UserDetails.Gender = modifiedUserRequest.UserDetails.Gender;
            foundUser.UserDetails.Email = modifiedUserRequest.UserDetails.Email;
            foundUser.UserDetails.PhoneNumber = modifiedUserRequest.UserDetails.PhoneNumber;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            _databaseContext.SaveChanges();
            return RedirectToAction(actionName: "ManageStudents");
        }

        public IActionResult DeleteStudent(long studentID)
        {
            if (!IsAuthenticated())
            {
                return RedirectToAction(actionName: "Index", controllerName: "Home");
            }
            User? foundUser = _databaseContext.Users.SingleOrDefault(currentUser => currentUser.UserID == studentID);
            if (foundUser == null)
            {
                return NotFound();
            }
            _databaseContext.Users.Remove(entity: foundUser);
            _databaseContext.SaveChanges();
            return RedirectToAction(actionName: "ManageStudents");
        }

        public IActionResult DeleteEnrollment(long studentID, long classID)
        {
            if (!IsAuthenticated())
            {
                return RedirectToAction(actionName: "Index", controllerName: "Home");
            }
            Enrollment? foundEnrollment = _databaseContext.Enrollments.FirstOrDefault
                (
                    predicate: currentEnrollment => currentEnrollment.StudentID == studentID
                        && currentEnrollment.ClassID == classID
                );
            if (foundEnrollment == null)
            {
                return NotFound();
            }
            _databaseContext.Enrollments.Remove(entity: foundEnrollment);
            _databaseContext.Attendances.RemoveRange
                (
                    entities: _databaseContext.Attendances.Where
                        (
                            predicate: currentAttendance => currentAttendance.StudentID == studentID
                                && currentAttendance.ClassID == classID
                        )
                );
            _databaseContext.SaveChanges();
            return RedirectToAction
                (
                    actionName: "DisplayCoursesAndAttendances",
                    routeValues: new { studentID = foundEnrollment.StudentID }
                );
        }

        public IActionResult EnrollStudent(long studentID, long classID)
        {
            if (!IsAuthenticated())
            {
                return RedirectToAction(actionName: "Index", controllerName: "Home");
            }
            _databaseContext.Enrollments.Add(new Enrollment(studentID: studentID, classID: classID));
            _databaseContext.SaveChanges();
            return RedirectToAction
                (
                    actionName: "DisplayCoursesAndAttendances",
                    routeValues: new { studentID }
                );
        }

        [HttpPost]
        public IActionResult ModifyAttendance(long attendanceID, long studentID)
        {
            if (!IsAuthenticated())
            {
                return RedirectToAction(actionName: "Index", controllerName: "Home");
            }
            Attendance? foundAttendance = _databaseContext.Attendances
                .FirstOrDefault
                    (
                        predicate: currentAttendance => currentAttendance.AttendanceID == attendanceID
                    );
            if (foundAttendance == null)
            {
                return NotFound();
            }
            foundAttendance.IsPresent = !foundAttendance.IsPresent;
            _databaseContext.SaveChanges();
            return RedirectToAction
                (
                    actionName: "DisplayCoursesAndAttendances",
                    routeValues: new { studentID }
                );
        }

        public IActionResult DeleteAttendance(long attendanceID, long studentID)
        {
            if (!IsAuthenticated())
            {
                return RedirectToAction(actionName: "Index", controllerName: "Home");
            }
            Attendance? foundAttendance = _databaseContext.Attendances.FirstOrDefault
                (
                    predicate: currentAttendance => currentAttendance.StudentID == studentID
                        && currentAttendance.AttendanceID == attendanceID
                );
            if (foundAttendance == null)
            {
                return NotFound();
            }
            _databaseContext.Attendances.Remove(entity: foundAttendance);
            _databaseContext.SaveChanges();
            return RedirectToAction
                (
                    actionName: "DisplayCoursesAndAttendances",
                    routeValues: new { studentID = foundAttendance.StudentID }
                );
        }

        [HttpPost]
        public void CreateAttendance(long classID, long studentID, string attendanceDate, bool wasPresent)
        {
            if (!IsAuthenticated())
            {
                RedirectToAction(actionName: "Index", controllerName: "Home");
            }
            DateTime parsedDate = DateTime.ParseExact
                (
                    s: attendanceDate,
                    format: "dd MMMM yyyy HH:mm",
                    provider: CultureInfo.GetCultureInfo(name: "ro-RO")
                );
            Attendance newAttendance = new
                (
                    attendanceDate: parsedDate,
                    wasPresent: wasPresent,
                    classID: classID,
                    studentID: studentID
                );
            _databaseContext.Attendances.Add(entity: newAttendance);
            _databaseContext.SaveChanges();
        }

        public IActionResult DeleteClassTeacher(long classID, long teacherID)
        {
            if (!IsAuthenticated())
            {
                return RedirectToAction(actionName: "Index", controllerName: "Home");
            }
            ClassTeacher? foundProfessor = _databaseContext.ClassTeachers
                .FirstOrDefault
                    (
                        predicate: currentClassTeacher => currentClassTeacher.ClassID == classID && currentClassTeacher.ProfessorID == teacherID
                    );
            if (foundProfessor == null)
            {
                return BadRequest();
            }
            _databaseContext.ClassTeachers.Remove(entity: foundProfessor);
            _databaseContext.SaveChanges();
            return RedirectToAction(actionName: "ManageClasses");
        }

        [HttpPost]
        public void AddProfessorToClass(long classID, long professorID)
        {
            string CONNECTION_STRING = @"DATA SOURCE=DESKTOP-TL2AJOD\SQLEXPRESS;INITIAL CATALOG=PrezenteOnline;TRUSTED_CONNECTION=true;TrustServerCertificate=True";
            using SqlConnection sqlConnection = new(connectionString: CONNECTION_STRING);
            sqlConnection.Open();
            string insertQuery = $"""
                INSERT INTO [dbo].[CLASS_TEACHERS] ([CLASS_ID], [PROFESSOR_ID])
                VALUES
                      (@ClassID, @ProfessorID)
                """;
            using SqlCommand sqlCommand = new(cmdText: insertQuery, connection: sqlConnection);
            _ = sqlCommand.Parameters.AddWithValue(parameterName: "@ClassID", value: classID);
            _ = sqlCommand.Parameters.AddWithValue(parameterName: "@ProfessorID", value: professorID);
            _ = sqlCommand.ExecuteNonQuery();
        }

        public IActionResult DeleteClass(long classID)
        {
            Class? foundClass = _databaseContext.Classes
                .FirstOrDefault
                    (
                        predicate: currentClass => currentClass.ClassID == classID
                    );
            if (foundClass == null)
            {
                return BadRequest();
            }
            _databaseContext.Classes.Remove(entity: foundClass);
            _databaseContext.SaveChanges();
            return RedirectToAction(actionName: "ManageClasses");
        }

        [HttpPost]
        public void AddClass(string classType, string className)
        {
            Class newClass = new(classType: classType, className: className);
            _databaseContext.Classes.Add(entity: newClass);
            _databaseContext.SaveChanges();
        }

        public void ModifyClassName(long classID, string newClassName)
        {
            Class? foundClass = _databaseContext.Classes
                .FirstOrDefault
                    (
                        predicate: currentClass => currentClass.ClassID == classID
                    );
            if (foundClass == null)
            {
                return;
            }
            foundClass.Name = newClassName;
            _databaseContext.Classes.Update(entity: foundClass);
            _databaseContext.SaveChanges();
        }

        private bool IsAuthenticated()
        {
            User? authenticatedUser = HttpContext.Session.GetObject<User>(cheieObiect: "authenticatedUser");
            return authenticatedUser != null;
        }

    }
}
