using ProiectPrezente.Models.Classes;
using ProiectPrezente.Models.Users;

namespace ProiectPrezente.Models
{
    public class CoursesAndAttendancesModel(User foundStudent, List<Class> listOfClasses)
    {
        public User User { get; set; } = foundStudent;

        public List<Class> Classes { get; set; } = listOfClasses;
    }
}
