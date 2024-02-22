using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProiectPrezente.Models.Classes
{
    [Table(name: "CLASSES")]
    public class Class
    {
        [Column(name: "CLASS_ID")]
        public long ClassID { get; set; }

        [Column(name: "CLASS_NAME")]
        public string Name { get; set; }

        [Column(name: "CLASS_TYPE")]
        [EnumDataType(enumType: typeof(ClassType))]
        public ClassType Type { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Class()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {

        }

        public Class(string classType, string className)
        {
            Type = classType == "COURSE" ? ClassType.COURSE : ClassType.LABORATORY;
            Name = className;
        }

        public Class(long classID, string name, ClassType type, ICollection<Enrollment> enrollments, ICollection<ClassTeacher> teachers, ICollection<Attendance> attendances)
        {
            ClassID = classID;
            Name = name;
            Type = type;
            Enrollments = enrollments;
            Teachers = teachers;
            Attendances = attendances;
        }

        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
        public ICollection<ClassTeacher> Teachers { get; set; } = new List<ClassTeacher>();
        public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();
    }
}
