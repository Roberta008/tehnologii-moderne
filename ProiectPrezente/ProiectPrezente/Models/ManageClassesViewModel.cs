using ProiectPrezente.Models.Classes;
using ProiectPrezente.Models.Users;

namespace ProiectPrezente.Models
{
    public class ManageClassesViewModel(List<Class> listOfClasses, List<User> listOfTeachers)
    {
        public List<Class> Classes { get; set; } = listOfClasses;
        public List<User> Professors { get; set; } = listOfTeachers;
    }
}
