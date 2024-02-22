using ProiectPrezente.Models.Users;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProiectPrezente.Models.Classes
{
    [Table(name: "CLASS_TEACHERS")]
    public class ClassTeacher
    {
        [Column(name: "CLASS_ID")]
        public long ClassID { get; set; }
        public Class Class { get; set; }
        [Column(name: "PROFESSOR_ID")]
        public long ProfessorID { get; set; }
        public User Professor { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public ClassTeacher()
        {

        }

        public ClassTeacher(long classID, Class foundClass, long professorID, User foundProfessor)
        {
            Class = foundClass;
            ClassID = classID;
            Professor = foundProfessor;
            ProfessorID = professorID;
        }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    }
}
