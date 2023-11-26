using Microsoft.EntityFrameworkCore;
using ProiectPrezentaOnline.Models;

namespace ProiectPrezentaOnline.Data
{
    public class DatabaseContext(DbContextOptions<DatabaseContext> contextOptions) : DbContext(contextOptions)
    {
        public DbSet<Utilizator> Utilizatori { get; set; }
        public DbSet<Curs> Cursuri { get; set; }
        public DbSet<Laborator> Laboratoare { get; set; }
        public DbSet<PrezentaCurs> PrezenteCursuri { get; set; }
        public DbSet<PrezentaLaborator> PrezenteLaboratoare { get; set; }
    }
}
