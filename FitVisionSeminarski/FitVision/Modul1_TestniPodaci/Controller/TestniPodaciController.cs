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
            data.Add("Kategorija", _dbContext.Korisnik.Count());
            return Ok(data);
        }

        [HttpPost]
        public ActionResult Obrisi()
        {
            var drzave = _dbContext.Drzava.ToList();
            var gradovi = _dbContext.Grad.ToList();
            var admin = _dbContext.Admin.ToList();
            var korsnik = _dbContext.Korisnik.ToList();
            var kategorija = _dbContext.Kategorija.ToList();


            _dbContext.Drzava.RemoveRange(drzave);
            _dbContext.Grad.RemoveRange(gradovi);
            _dbContext.Admin.RemoveRange(admin);
            _dbContext.Korisnik.RemoveRange(korsnik);
            _dbContext.Kategorija.RemoveRange(kategorija);
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

            var kategorije = new List<Kategorija>();

            kategorije.Add(new Kategorija { Naziv = "Dodaci prehrani", Opis= "Dodaci prehrani su, kako im i samo ime kaže, pripravci koji služe da nutritivno pojačaju vašu redovitu prehranu. Zbog smanjene kvalitete namirnica i užurbanog načina života, redovitom prehranom ne unosimo dovoljno mikro i makronutrijenata pa su proizvodi u ovoj kategoriji osmišljeni da zadovolje vaše prehrambene potrebe. Dodaci prehrani pomažu u očuvanju vašeg zdravlja i ostvarenju sportskih ciljeva." });
            kategorije.Add(new Kategorija { Naziv = "Hrana", Opis = "Ako pazite što jedete i želite da vam hrana bude i izvor energije, ali i temelj zdravlja, tada ćete u ovoj kategoriji naći gotovo sve što trebate u svakodnevnom jelovniku. Proizvodima iz ove kategorije poboljšajte nutritivnu vrijednost vaših obroka, osigurajte si kvalitetnu prehranu i doprinesite zdravlju kako fizičkom, tako i mentalnom." });
            kategorije.Add(new Kategorija { Naziv = "Shakeri i boce", Opis = "Ako se bavite bilo kakvim sportom, uvijek na trening nosite vodu. Dakle, potrebna vam je dobra bočica za vodu, odnosno bidon. Na kraju, ako koristite dodatke prehrani koji su često u obliku praha, morate ih u nečemu moći dobro promućkati da se razbiju sve grudice. Tu nastupa shaker. Bidone i shakere možete pronaći u ovoj kategoriji i odabrati nešto za sebe." });
            kategorije.Add(new Kategorija { Naziv = "Rekviziti", Opis = "U ovoj kategoriji ćete pronaći različite rekvizite za različite tipove treninga, no namijenjene svima: mladima ili starijim osobama, muškarcima i ženama, amaterima i vježbačkim početnicima, rekreativnim vježbačima ili profesionalnim sportašima. Bez obzira u kakvoj ste trenutnoj formi, u ovoj kategoriji ćete pronaći rekvizite koji će vam pomoći da formu podignete na jednu višu stepenicu." });


            _dbContext.AddRange(drzave);
            _dbContext.AddRange(gradovi);
            _dbContext.AddRange(admini);
            _dbContext.AddRange(korisnici);
            _dbContext.AddRange(kategorije);

            _dbContext.SaveChanges();

            return Count();
        }

    }
}
