using ProiectPrezente.Models.Classes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProiectPrezente.Models.Users
{
    // Atribuirea numelui tabelului pentru entitatea Student în baza de date
    [Table(name: "USERS")]
    public class User
    {
        private const int MAX_USERNAME_LENGTH = 50;
        private const int MIN_PASSWORD_LENGTH = 8;

        // Identificatorul unic al studentului în tabel
        [Key, Column(name: "USER_ID")]
        public long UserID { get; set; }

        // Numele de utilizator al studentului
        [Column(name: "USERNAME")]
        [Required(ErrorMessage = "Numele de utilizator nu poate lipsi!")]
        [MaxLength(length: MAX_USERNAME_LENGTH)]
        [Display(Name = "Nume Utilizator")]
        public string Username { get; set; }

        // Parola asociată studentului
        [Column(name: "PASSWORD")]
        [Required(ErrorMessage = "Parola nu poate lipsi!")]
        [MinLength(length: MIN_PASSWORD_LENGTH, ErrorMessage = "Parola trebuie să conțină minim 8 caractere!")]
        [Display(Name = "Parolă")]
        public string Password { get; set; }

        [Column(name: "USER_TYPE")]
        [EnumDataType(enumType: typeof(UserType))]
        public UserType UserType { get; set; } = UserType.Student;
        public UserDetails UserDetails { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
        public ICollection<ClassTeacher> ClassesTaught { get; set; } = new List<ClassTeacher>();
        public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();
    }
}
