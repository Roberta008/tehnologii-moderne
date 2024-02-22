using ProiectPrezente.Models.Classes;

namespace ProiectPrezente.Models
{
    public class StudentsIndexViewModel(List<Enrollment> enrolledClasses, List<Attendance> userAttendances)
    {
        public List<Enrollment> EnrolledClasses { get; set; } = enrolledClasses;
        public List<Attendance> UserAttendances { get; set; } = userAttendances;
    }
}
