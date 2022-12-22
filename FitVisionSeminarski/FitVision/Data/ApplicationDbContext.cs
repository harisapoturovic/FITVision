using FitVision.Modul0_Autentifikacija.Models;
using FitVision.Modul2.Models;
using Microsoft.EntityFrameworkCore;

namespace FitVision.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<KorisnickiNalog> KorisnickiNalog { get; set; }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<Korisnik> Korisnik { get; set; }
        public DbSet<Grad> Grad { get; set; }
        public DbSet<Drzava> Drzava { get; set; }
        public DbSet<AutentifikacijaToken> AutentifikacijaToken { get; set; }
        public DbSet<Oprema> Oprema { get; set; }
        public DbSet<TipOpreme> TipOpreme { get; set; }

    }
}
