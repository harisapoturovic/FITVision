using FitVision.Data;
using FitVision.Modul2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitVision.Modul1_TestniPodaci.Controller
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class TestniPodaciController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public TestniPodaciController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        [HttpGet]
        public ActionResult Count()
        {
            Dictionary<string, int> data = new Dictionary<string, int>();
            data.Add("Drzava", _dbContext.Drzava.Count());
            data.Add("Gradova", _dbContext.Grad.Count());
            data.Add("Admina", _dbContext.Admin.Count());
            data.Add("Korisnika", _dbContext.Korisnik.Count());

            return Ok(data);
        }

        [HttpPost]
        public ActionResult Obrisi()
        {
            var drzave = _dbContext.Drzava.ToList();
            var gradovi = _dbContext.Grad.ToList();
            var admin = _dbContext.Admin.ToList();
            var korsnik = _dbContext.Korisnik.ToList();
           

            _dbContext.Drzava.RemoveRange(drzave);
            _dbContext.Grad.RemoveRange(gradovi);
            _dbContext.Admin.RemoveRange(admin);
            _dbContext.Korisnik.RemoveRange(korsnik);
            _dbContext.SaveChanges();
            return Count();
        }

        [HttpPost]
        public ActionResult Generisi()
        {
            var drzave = new List<Drzava>();

            drzave.Add(new Drzava { Naziv = "Bosna i Hercegovina", Skracenica="BiH"});
            drzave.Add(new Drzava { Naziv = "Hrvatska", Skracenica = "Hrv" });
            drzave.Add(new Drzava { Naziv = "Srbija", Skracenica = "Srb" });
            drzave.Add(new Drzava { Naziv = "Crna Gora", Skracenica = "CG" });


            var gradovi = new List<Grad>();

            gradovi.Add(new Grad { Naziv = "Sarajevo", PostanskiBroj = 71000, drzava = drzave[0] });
            gradovi.Add(new Grad { Naziv = "Mostar", PostanskiBroj = 88000, drzava = drzave[0] });
            gradovi.Add(new Grad { Naziv = "Zenica", PostanskiBroj = 72000, drzava = drzave[0] });

            gradovi.Add(new Grad { Naziv = "Zagreb", PostanskiBroj = 10000, drzava = drzave[1] });
            gradovi.Add(new Grad { Naziv = "Split", PostanskiBroj = 21000, drzava = drzave[1] });

            gradovi.Add(new Grad { Naziv = "Beograd", PostanskiBroj = 104101, drzava = drzave[2] });
            gradovi.Add(new Grad { Naziv = "Novi Sad", PostanskiBroj = 400107, drzava = drzave[2] });

            gradovi.Add(new Grad { Naziv = "Podgorica", PostanskiBroj = 81000, drzava = drzave[3] });
            gradovi.Add(new Grad { Naziv = "Herceg Novi", PostanskiBroj = 85340, drzava = drzave[3] });

            var admini = new List<Admin>();
            admini.Add(new Admin
            {
                Ime = "Denis",
                Prezime = "Music",
                JMBG = "1224536640",
                grad = gradovi[2],
                Adresa = "Marsala Tita bb",
                Email = "example@edu.ba",
                Spol = "Muški",
                Telefon = "036785147",
                DatumRodjenja = new DateTime(1996, 7, 7),
                DatumZaposlenja = new DateTime(2019, 3, 3),
                StrucnaSprema = "Softverski inžinjer",


                KorisnickoIme = "denis",
                Lozinka = "test"

            });

            var korisnici = new List<Korisnik>();
            korisnici.Add(new Korisnik
            {
                Ime = "Jasmin",
                Prezime = "Azemovic",
                JMBG = "1336513",
                grad = gradovi[2],
                Adresa = "Marsala Tita bb",
                Email = "primjer@edu.ba",
                Spol = "Muški",
                Telefon = "062145681",
                DatumRodjenja = new DateTime(1994, 8, 8),
                DatumPolasaka = new DateTime(2021, 4, 4),
                Visina = "1.75",
                Tezina = "75",

                KorisnickoIme = "jasmin",
                Lozinka = "test"
            }); ;

            


            _dbContext.AddRange(drzave);
            _dbContext.AddRange(gradovi);
            _dbContext.AddRange(admini);
            _dbContext.AddRange(korisnici);
            
            _dbContext.SaveChanges();

            return Count();
        }

    }
}
