using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProiectPrezente.Models.Users
{
    [Table(name: "USER_DETAILS")]
    public class UserDetails
    {
        [Key]
        [Column(name: "DETAILS_ID")]
        public long DetailsID { get; set; }

        [Column(name: "FIRST_NAME")]
        [Display(Name = "Nume")]
        public required string FirstName { get; set; }

        [Column(name: "LAST_NAME")]
        [Display(Name = "Prenume")]
        public required string LastName { get; set; }

        [Column(name: "GENDER")]
        [Display(Name = "Gen")]
        public required string Gender { get; set; }

        [Column(name: "EMAIL")]
        [Display(Name = "E-Mail")]
        public required string Email { get; set; }

        [Column(name: "PHONE_NUMBER")]
        [Display(Name = "Număr de Telefon")]
        public required string PhoneNumber { get; set; }
        public long UserID { get; set; }
        //public User User { get; set; } = null!;
    }
}
