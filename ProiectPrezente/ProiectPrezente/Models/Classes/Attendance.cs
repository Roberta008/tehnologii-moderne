using ProiectPrezente.Models.Users;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProiectPrezente.Models.Classes
{
    [Table(name: "ATTENDANCES")]
    public class Attendance
    {
        [Column(name: "ATTENDANCE_ID")]
        public long AttendanceID { get; set; }
        [Column(name: "DATE")]
        public DateTime Date { get; set; }
        [Column(name: "IS_PRESENT")]
        public bool IsPresent { get; set; }
        [Column(name: "CLASS_ID")]
        public long ClassID { get; set; }
        public Class Class { get; set; }
        [Column(name: "STUDENT_ID")]
        public long StudentID { get; set; }
        public User Student { get; set; }

        // Parameterless constructor
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Attendance()
        {
            
        }

        public Attendance(DateTime attendanceDate, bool wasPresent, long classID, long studentID)
        {
            Date = attendanceDate;
            IsPresent = wasPresent;
            ClassID = classID;
            StudentID = studentID;
        }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    }
}
