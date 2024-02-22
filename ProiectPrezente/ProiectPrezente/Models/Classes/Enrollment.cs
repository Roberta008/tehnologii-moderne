using ProiectPrezente.Models.Users;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProiectPrezente.Models.Classes
{
    [Table(name: "ENROLLMENTS")]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public class Enrollment(long studentID, long classID)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
        [Column(name: "STUDENT_ID")]
        public long StudentID { get; set; } = studentID;
        public User Student { get; set; }
        [Column(name: "CLASS_ID")]
        public long ClassID { get; set; } = classID;
        public Class Class { get; set; }
    }
}
