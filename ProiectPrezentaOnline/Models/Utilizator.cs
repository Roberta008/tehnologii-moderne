using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProiectPrezentaOnline.Models
{
    public class Utilizator
    {
        [Key]
        public int Id { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        [Required]
        public string Nume { get; set; }

        [Required]
        public string Prenume { get; set; }

        [Required(ErrorMessage = "Numele de Utilizator nu are voie sa lipseasca!")]
        [DisplayName("Nume de Utilizator")]
        public string NumeUtilizator { get; set; }

        [Required(ErrorMessage ="Parola nu are voie sa lipseasca")] 
        [MinLength(length: 12, ErrorMessage = "Parola trebuie sa contina minimum 12 caractere!")]
        public string Parola { get; set; }

        [Required, Phone]
        public string NumarDeTelefon { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    }
}
