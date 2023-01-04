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
        public DbSet<Kategorija> Kategorija { get; set; }
        public DbSet<Brend> Brend { get; set; }
        public DbSet<Podkategorija> Podkategorija { get; set; }
        public DbSet<Proizvod> Proizvod { get; set; }
        public DbSet<Akcija> Akcija { get; set; }
        public DbSet<Korpa> Korpa { get; set; }
        public DbSet<KorpaProizvod> KorpaProizvod { get; set; }
        public DbSet<Poruka> Poruka { get; set; }
        public DbSet<Odgovor> Odgovor { get; set; }


    }
}
