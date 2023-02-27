using FitVision.Data;
using FitVision.Helpers;
using FitVision.Helpers.AutentifikacijaAutorizacija;
using FitVision.Modul2.Models;
using FitVision.Modul2.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitVision.Modul2.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class KorisnikController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public KorisnikController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpGet("{guid}")]
        public ActionResult Aktivacija(string guid)
        {
            var korisnik = _dbContext.Korisnik.FirstOrDefault(s => s.aktivacijaGUID == guid);
            if (korisnik != null)
            {
                korisnik.isAktiviran = true;
                _dbContext.SaveChanges();
                return Redirect("http://localhost:4200/korisnici");
            }

            return BadRequest("pogresan URL");
        }

        [HttpGet]
        [Autorizacija(false, true)]
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
                datum_rodjenja = korisnik.DatumRodjenja.ToString("yyyy-MM-dd"),

                visina = korisnik.Visina,
                tezina = korisnik.Tezina,
                datum_polaska = korisnik.DatumPolasaka.ToString("yyyy-MM-dd"),
                
                korisnickoIme = korisnik.KorisnickoIme,
                lozinka = korisnik.Lozinka

            };
            return korisnikGetVM;
        }

        [HttpPost]
        [Autorizacija(false, true)] //admin dodaje korisnike
        public ActionResult Snimi([FromBody] KorisnikAddVM x)
        {       
            Korisnik? korisnik;
            if(x.id== 0)
            {
                korisnik = new Korisnik();
                korisnik.aktivacijaGUID = Guid.NewGuid().ToString();
            }
            else
            {
                korisnik=_dbContext.Korisnik.FirstOrDefault(k=>k.ID==x.id);
                if (korisnik == null)
                    return BadRequest("Korisnik ne postoji");
            }
            korisnik.ID=x.id;
            korisnik.Ime=x.ime;
            korisnik.Prezime=x.prezime;
            korisnik.DatumRodjenja=DateTime.Parse(x.datum_rodjenja); 
            korisnik.Email=x.email;
            korisnik.Adresa = x.adresa;
            korisnik.JMBG=x.jmbg;
            korisnik.Spol=x.spol;
            korisnik.Telefon=x.telefon;
            korisnik.gradid = x.grad_ID;

            korisnik.Visina=x.visina;
            korisnik.Tezina=x.tezina;
            korisnik.DatumPolasaka=DateTime.Parse(x.datum_polaska);

            korisnik.KorisnickoIme = x.korisnickoIme;
            korisnik.Lozinka=x.lozinka;

                
            //provjera da li postoje korsnici koji imaju isto korIme i lozinku
               
            if (x.id == 0)
            {
                List<Admin> adminList = _dbContext.Admin.ToList();
                foreach (var a in adminList)
                {
                    if (korisnik.Lozinka == a.Lozinka && korisnik.KorisnickoIme == a.KorisnickoIme)
                        return BadRequest("Korisnicko ime i lozinka vec postoje");
                }

                 List<Korisnik> korisnici = _dbContext.Korisnik.ToList();
                foreach (var k in korisnici)
                {
                    if (korisnik.KorisnickoIme == k.KorisnickoIme && korisnik.Lozinka == k.Lozinka)
                        return BadRequest("Korisnicko ime i lozinka vec postoje");
                }

                _dbContext.Korisnik.Add(korisnik);
            }
            EmailLog.noviKorisnik(korisnik, HttpContext);

            _dbContext.SaveChanges();
            return Ok(korisnik);
        }

        [HttpPost("{id}")]
        [Autorizacija(false, true)]
        public ActionResult Obrisi(int id)
        {
            Korisnik? korisnik = _dbContext.Korisnik.Find(id);

            if (korisnik == null)
                return BadRequest("pogresan ID");

            _dbContext.Remove(korisnik);

            _dbContext.SaveChanges();
            return Ok(korisnik);
        }
    }
}

