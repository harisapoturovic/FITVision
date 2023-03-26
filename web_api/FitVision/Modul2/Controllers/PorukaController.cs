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
    public partial class PorukaController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public PorukaController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<List<PorukaGetVM>> GetAll()
        {
            var data = _dbContext.Poruka
                .Include(p => p.korisnickiNalog).Select(p=>new PorukaGetVM()
                {
                    id=p.ID,
                    naslov=p.Naslov,
                    sadrzaj=p.Sadrzaj,
                    datum_kreiranja=p.DatumKreiranja.ToString("yyyy-dd-MM"),
                    korisnik=p.korisnickiNalog.KorisnickoIme
                });

            return data.Take(100).ToList();
        }

        [HttpGet]
        public ActionResult<List<PorukaGetVM>> GetByKorsinikId(int id)
        {
            List<PorukaGetVM> poruke = _dbContext.Poruka.Include(p => p.korisnickiNalog).Where(p => p.korisnickiNalog.ID == id).Select(p => new PorukaGetVM()
            {
                id = p.ID,
                naslov = p.Naslov,
                sadrzaj = p.Sadrzaj,
                datum_kreiranja = p.DatumKreiranja.ToString("yyyy-dd-MM"),
                korisnik = p.korisnickiNalog.korisnik.Ime + ' ' + p.korisnickiNalog.korisnik.Prezime
            }).ToList(); 

            return poruke;
        }

        [HttpGet]
        public ActionResult<List<PorukaGetVM>> GetByAdminId(int id)
        {
            List<PorukaGetVM> poruke = _dbContext.Poruka.Include(p => p.korisnickiNalog).Where(p => p.korisnickiNalog.ID == id).Select(p => new PorukaGetVM()
            {
                id = p.ID,
                naslov = p.Naslov,
                sadrzaj = p.Sadrzaj,
                datum_kreiranja = p.DatumKreiranja.ToString("yyyy-dd-MM"),
                korisnik = p.korisnickiNalog.admin.Ime + ' ' + p.korisnickiNalog.admin.Prezime
            }).ToList(); ;

            return poruke;
        }

        [HttpGet]
        public ActionResult<List<PorukaGetVM>> GetByAdmin(int id)
        {
            List<PorukaGetVM> poruke = _dbContext.Poruka.Include(p => p.korisnickiNalog).Where(p => p.korisnickiNalog.ID!=id).Select(p => new PorukaGetVM()
            {
                id = p.ID,
                naslov = p.Naslov,
                sadrzaj = p.Sadrzaj,
                datum_kreiranja = p.DatumKreiranja.ToString("yyyy-dd-MM"),
                korisnik = p.korisnickiNalog.admin.Ime + ' ' + p.korisnickiNalog.admin.Prezime
            }).ToList(); ;

            return poruke;
        }

        [HttpGet]
        public ActionResult<List<PorukaGetVM>> GetByKorisnik(int id)
        {
            List<PorukaGetVM> poruke = _dbContext.Poruka.Include(p => p.korisnickiNalog).Where(p => p.korisnickiNalog.ID != id).Select(p => new PorukaGetVM()
            {
                id = p.ID,
                naslov = p.Naslov,
                sadrzaj = p.Sadrzaj,
                datum_kreiranja = p.DatumKreiranja.ToString("yyyy-dd-MM"),
                korisnik = p.korisnickiNalog.korisnik.Ime + ' ' + p.korisnickiNalog.korisnik.Prezime
            }).ToList(); ;

            return poruke;
        }


        [HttpGet]
        public ActionResult<OdgovorPorukaGetVM> GetPorukaOdgovori(int id)
        {
            var poruka = _dbContext.Poruka.Include(k => k.korisnickiNalog).FirstOrDefault(p => p.ID == id);
            if (poruka == null)
                return BadRequest("ne postoji poruka");
            var obj = new OdgovorPorukaGetVM();

            obj.naslov = poruka.Naslov;
            obj.sadrzaj = poruka.Sadrzaj;
            obj.datum_kreiranja = poruka.DatumKreiranja.ToString("yyyy-MM-dd");
            obj.korisnicki_nalog = poruka.korisnickiNalog;
            obj.odgovori = _dbContext.Odgovor.Where(O => O.poruka_id == poruka.ID).Select(
                z => new OdgovorGetVM()
                {
                    id = z.ID,
                    sadrzaj = z.Sadrzaj,
                    datum_kreiranja = z.DatumKreiranja.ToString("yyyy-MM-dd"),
                    admin_name = z.AdminIme
                }
                ).ToList();

            return obj;

        }

        [HttpPost]
        public ActionResult Dodaj([FromBody] PorukaAddVM x)
        {
            Poruka poruka = new Poruka();
            poruka.Naslov = x.naslov;
            poruka.Sadrzaj = x.sadrzaj;
            poruka.DatumKreiranja = DateTime.Now;
            poruka.korisnickiNalogID = x.korsinciki_nalog_id;

            _dbContext.Add(poruka);
            _dbContext.SaveChanges();
            return Ok(x);
        }


        [HttpPost]
        public ActionResult Obrisi(int id)
        {
            Poruka poruka = _dbContext.Poruka.FirstOrDefault(p => p.ID == id);
            if (poruka == null)
            {
                return BadRequest("ne postoji ta poruka");
            }
            _dbContext.Remove(poruka);
            _dbContext.SaveChanges();
            return new JsonResult(true);
        }
    }
}
