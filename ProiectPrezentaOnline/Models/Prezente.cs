namespace ProiectPrezentaOnline.Models
{
    public class Prezente
    {
        public Prezente()
        {
            PrezenteCursuri = [];
            PrezenteLaboratoare = [];
        }
        public Prezente(List<PrezentaCurs> prezenteCursuri, List<PrezentaLaborator> prezenteLaboratoare)
        {
            PrezenteCursuri = prezenteCursuri;
            PrezenteLaboratoare = prezenteLaboratoare;
        }
        public List<PrezentaCurs> PrezenteCursuri { get; set; }
        public List<PrezentaLaborator> PrezenteLaboratoare { get; set; }
    }
}
