using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProiectPrezentaOnline.Models
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public class PrezentaLaborator
    {
        [Key] public int ID { get; set; }
        public int? IDStudent { get; set; }
        public int? IDLaborator { get; set; }
        [Required] public DateTime Data { get; set; }
        [Required] public bool Prezent { get; set; }
        [ForeignKey("IDStudent")] public Utilizator Student { get; set; }
        [ForeignKey("IDLaborator")] public Laborator Laborator { get; set; }
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
