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
            data.Add("Brendova", _dbContext.Brend.Count());
            data.Add("Podkategorija", _dbContext.Podkategorija.Count());
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
            var brendovi = _dbContext.Brend.ToList();
            var podkategorija = _dbContext.Podkategorija.ToList();


            _dbContext.Drzava.RemoveRange(drzave);
            _dbContext.Grad.RemoveRange(gradovi);
            _dbContext.Admin.RemoveRange(admin);
            _dbContext.Korisnik.RemoveRange(korsnik);
            _dbContext.Kategorija.RemoveRange(kategorija);
            _dbContext.Brend.RemoveRange(brendovi);
            _dbContext.Podkategorija.RemoveRange(podkategorija);
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

            var brendovi = new List<Brend>();

            brendovi.Add(new Brend { Naziv = "Biona", Opis= "Biona organic brend je nastao pod palicom Windmill Organics Ltd, britanske tvrtke koja u portfelju ima organske brendove 100% sigurnog porijekla, visoke kvalitete, etičke proizvodnje u skladu sa svim visokim ekološkim standardima. Biona organic proizvodi nastaju bez korištenja pesticida, herbicida i GMO biljaka. Svi proizvodi su visoke kvalitete, potpuno prirodni i primjereni vegetarijancima." });
            brendovi.Add(new Brend { Naziv = "Nutrigold ", Opis= "Nutrigold je brend koji je nastao iz istraživanja potreba kupaca. Cilj im je prihvatljivim cijenama, kvalitetom i pouzdanošću doprijeti u sva kućanstva i zadovoljiti različite prehrambene potrebe ljudi. Nutrigold je brend koji je pobornicima zdrave prehrane približio zdravlje i kroz velik izbor namirnica i proizvoda im dao mogućnost odabira i svijest da je zdrava prehrana cjenovno prihvatljiva, ukusna i kvalitetna." });
            brendovi.Add(new Brend { Naziv = "HealthyCo ", Opis= "HealthyCo je švedski brend prehrambenih namirnica koje će donijeti zadovoljstvo vašem nepcu, a očuvati vaše zdravlje i vitku liniju. HealthyCo razvija, proizvodi i distribuira namirnice s naglaskom na organsko porijeklo, reducirani udio šećera, zdravlje i moderan dizajn, po prihvatljivim cijenama. HealthyCo zadovoljava zahtjeve modernog i dobro informiranog kupca koji od proizvoda traži organsko porijeklo, zdravstvenu prihvatljivost i funkcionalnost, uz važnu stavku - cjenovnu pristupačnost." });

            var podkategorije = new List<Podkategorija>();
            //dodaci prehrani
            podkategorije.Add(new Podkategorija { Naziv = "Proteini", Opis = "Proteini ili bjelančevine su mikronutrijenti koji su ljudskom organizmu potrebni za preživljavanje i normalno funkcioniranje. Odaberite whey protein, kazein protein, goveđi protein, biljni protein ili napredne proteinske mješavine, a okus odaberite po vlastitom ukusu kako biste uživali u svakom proteinskom napitku kojeg si napravite, u bilo koje doba dana.", kategorija = kategorije[0] });
            podkategorije.Add(new Podkategorija { Naziv = "Kreatin ", Opis = "Kreatin je najkorišteniji suplement na svijetu, a za to postoje i jasni razlozi koji se nalaze u njegovoj učinkovitosti i djelovanju.\r\n\r\nZnanstveno je potvrđeno da kreatin povisuje tjelesne performanse u uzastopnim kratkotrajnim vježbama visokog intenziteta. Osim toga, studije su potvrdile da kreatin ima pozitivan utjecaj na brzinu oporavka nakon treninga ili druge vrste fizičke aktivnosti, smanjenje rizika od ozljeda, regulaciju tjelesne temperature, te zaštitu živčanih stanica.", kategorija = kategorije[0] });
            podkategorije.Add(new Podkategorija { Naziv = "Pre-workout proizvodi ", Opis = "Pre-workout proizvodi se, kako im i samo ime kaže, uzimaju neposredno prije treninga kako bi vam dali energije, snage, fokusa i izdržljivosti za napore koji slijede.\r\n\r\nOd proizvođača do proizvođača, sastavi pre-workout dodataka prehrani se razlikuju, ali cilj im je svima isti: podići razinu energije u vrijeme treninga i učiniti vježbača spremnijim za trening i napore, a time i poboljšati rezultat fizičke aktivnosti.", kategorija = kategorije[0] });
            //hrana
            podkategorije.Add(new Podkategorija { Naziv = "Proteinske čokoladice i napici", Opis = "Proteinske čokoladice i napici posebno su dobro rješenje kad idete na izlet ili put i želite uza se imati brzi i ukusan obrok kojim nećete pokvariti dijetu, a opet – pojest ćete nešto ukusno i nutritivno bogato. Današnje moderne generacija proteinskih čokoladica i napitaka su okusom potpuno jednake onima napravljenih od šećera i umjetnih aroma pa će i oni najizbirljiviji moći uživati u pravom okusu čokoladice bez ikakve zamjerke.", kategorija = kategorije[1] });
            podkategorije.Add(new Podkategorija { Naziv = "Slatki i slani snackovi ", Opis = "Kako doživljavate pojam „snack“? Je li to grickalica ili međuobrok? Za koji god odgovor se odlučite u ovoj kategoriji ćete naći nešto za svoj snack. Jer ono što se od snacka očekuje je: ekonomičnost, maleno pakiranje, dobar okus i kvalitetna nutritivna vrijednost, a upravo to zadovoljavaju svi artikli u ovoj kategoriji.", kategorija = kategorije[1] });

            _dbContext.AddRange(drzave);
            _dbContext.AddRange(gradovi);
            _dbContext.AddRange(admini);
            _dbContext.AddRange(korisnici);
            _dbContext.AddRange(kategorije);
            _dbContext.AddRange(brendovi);
            _dbContext.AddRange(podkategorije);

            _dbContext.SaveChanges();

            return Count();
        }

    }
}
