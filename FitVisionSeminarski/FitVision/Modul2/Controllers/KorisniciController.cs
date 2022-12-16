using FitVision.Data;
using FitVision.Modul2.Models;
using FitVision.Modul2.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitVision.Modul2.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class KorisniciController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public KorisniciController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public class KorisnikGetVM
        {
            public int id { get; set; }
            public string ime { get; set; }
            public string prezime { get; set; }
            public int godina_rodjenja { get; set; }
            public string telefon { get; set; }
            public string email { get; set; }
            public string adresa { get; set; }
            public string jmbg { get; set; }
            public string spol { get; set; }
            public string grad { get; set; }
            public string visina { get; set; }
            public string tezina { get; set; }
            public string datum_polasaka { get; set; }
            
        }

        [HttpGet]
        public ActionResult<List<KorisnikGetVM>> GetAll()
        {
            var data = _dbContext.Korisnik
                .Include(k => k.grad.drzava)
                .Select(k => new KorisnikGetVM
                {
                    id = k.ID,
                    ime = k.Ime,
                    prezime = k.Prezime,
                    godina_rodjenja = k.DatumRodjenja.Year,
                    telefon = k.Telefon,
                    email = k.Email,
                    adresa = k.Adresa,
                    spol = k.Spol,
                    jmbg = k.JMBG,
                    visina = k.Visina,
                    tezina = k.Tezina,
                    datum_polasaka = k.DatumPolasaka.ToShortDateString(),
                    grad = k.grad.Naziv + "(" + k.grad.drzava.Naziv + ")"

                });
            return data.Take(100).ToList();
        }



        public class KorisnikAddVM
        {
            public int id { get; set; }
            public string ime { get; set; }
            public string prezime { get; set; }
            public DateTime datum_rodjenja { get; set; }
            public string telefon { get; set; }
            public string email { get; set; }
            public string adresa { get; set; }
            public string jmbg { get; set; }
            public string spol { get; set; }
            public int grad_ID { get; set; }
            public string visina { get; set; }
            public string tezina { get; set; }
            public DateTime datum_polasaka { get; set; }
           

            public string korisnickoIme { get; set; }
            public string lozinka { get; set; }
        }

        [HttpGet]
        public ActionResult<KorisnikAddVM> GetById(int id)
        {
            Korisnik korisnik = _dbContext.Korisnik.FirstOrDefault(k => k.ID == id);
            if (korisnik == null)
                return BadRequest("Ne postoji korisnik");
            KorisnikAddVM korisnikGetVM = new KorisnikAddVM
            {
                id = korisnik.ID,
                ime = korisnik.Ime,
                prezime = korisnik.Prezime,
                telefon = korisnik.Telefon,
                email = korisnik.Email,
                adresa = korisnik.Adresa,
                spol = korisnik.Spol,
                jmbg = korisnik.JMBG,
                grad_ID = korisnik.gradid,
                datum_rodjenja = korisnik.DatumRodjenja,

                visina= korisnik.Visina,
                tezina= korisnik.Tezina,
                datum_polasaka= korisnik.DatumPolasaka,
                
                korisnickoIme = korisnik.KorisnickoIme,
                lozinka = korisnik.Lozinka

            };
            return korisnikGetVM;
        }

        [HttpPost]
        public ActionResult Snimi([FromBody] KorisnikAddVM x)
        {
           
            Korisnik? korisnik;
            if(x.id== 0) 
                korisnik=new Korisnik();
            else
            {
                korisnik=_dbContext.Korisnik.FirstOrDefault(k=>k.ID==x.id);
                if (korisnik == null)
                    return BadRequest("Korisnik ne postoji");
            }
            korisnik.ID=x.id;
            korisnik.Ime=x.ime;
            korisnik.Prezime=x.prezime;
            korisnik.DatumRodjenja=x.datum_rodjenja; 
            korisnik.Email=x.email;
            korisnik.Adresa = x.adresa;
            korisnik.JMBG=x.jmbg;
            korisnik.Spol=x.spol;
            korisnik.Telefon=x.telefon;
            korisnik.gradid = x.grad_ID;

            korisnik.Visina=x.visina;
            korisnik.Tezina=x.tezina;
            korisnik.DatumPolasaka=x.datum_polasaka;

            korisnik.KorisnickoIme = x.korisnickoIme;
            korisnik.Lozinka=x.lozinka;

            if (x.id == 0)
            {
                List<Admin> adminList = _dbContext.Admin.ToList();
                foreach (var a in adminList)
                {
                    if (korisnik.Lozinka == a.Lozinka && korisnik.KorisnickoIme == a.KorisnickoIme)
                        return BadRequest("Korisnicko ime i lozinka vec postoje");
                }

                List<Korisnik> korisniks = _dbContext.Korisnik.ToList();
                foreach (var k in korisniks)
                {
                    if (korisnik.KorisnickoIme == k.KorisnickoIme && korisnik.Lozinka == k.Lozinka)
                        return BadRequest("Korisnicko ime i lozinka vec postoje");
                }

                //provjera da li postoje korsnici koji imaju isto korIme i lozinku
                _dbContext.Korisnik.Add(korisnik);
            }


            
            _dbContext.SaveChanges();
            return Ok("Uspjesno snimanje");
        }
    }
}

