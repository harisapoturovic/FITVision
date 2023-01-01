using FitVision.Data;
using FitVision.Migrations;
using FitVision.Modul2.Models;
using FitVision.Modul2.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static FitVision.Modul2.Controllers.AkcijaController;

namespace FitVision.Modul2.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class KorpaProizvodController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public KorpaProizvodController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<List<KPGetAllVM>> GetAll()
        {
            var data = _dbContext.KorpaProizvod
                .Select(kp => new KPGetAllVM
                {
                    id = kp.Id,
                    kolicina= kp.Kolicina,
                    cijena= kp.Cijena,
                    popust=kp.Popust,
                    proizvodID=kp.proizvodID,
                    korpaID=kp.korpaID
                });

            return data.Take(100).ToList();
        }

        [HttpPost]
        public ActionResult Snimi([FromBody] KPGetAllVM x)
        {

            KorpaProizvod? kp;
            if (x.id == 0)
            {
                kp = new KorpaProizvod();
                _dbContext.Add(kp);
            }
            else
            {
                kp = _dbContext.KorpaProizvod.FirstOrDefault(p => p.Id == x.id);
                if (kp == null)
                    return BadRequest("Proizvod ne postoji");
            }

            kp.Kolicina = x.kolicina;
            kp.Cijena=x.cijena;
            kp.Popust=x.popust; 
            kp.korpaID=x.korpaID;
            kp.proizvodID = x.proizvodID;

            _dbContext.SaveChanges();
            return Ok(x);
        }

        [HttpPost("{id}")]
        public ActionResult Obrisi(int id)
        {
            KorpaProizvod? kp = _dbContext.KorpaProizvod.Find(id);
            if (kp == null) return BadRequest("Pogresan ID");

            _dbContext.Remove(kp);
            _dbContext.SaveChanges();
            return Ok(kp);
        }

        [HttpGet]
        public ActionResult GetByKorpa(int korpa_id)
        {
            var data = _dbContext.KorpaProizvod.Where(x => x.korpaID == korpa_id)
                .OrderBy(s => s.korpaID)
                .Select(s => new KPGetByKorpaVM
                {
                    id = s.Id,
                   kolicina=s.Kolicina,
                   popust=s.Popust,
                   cijena=s.Cijena,
                   proizvodID=s.proizvodID,
                   korpaID=s.korpaID,
                   nazivProizvoda=s.proizvod.Naziv,
                   jedinicnaMjera=s.proizvod.JedinicnaMjera,
                   slika=s.proizvod.Slika
                })
                .AsQueryable();
            return Ok(data.Take(100).ToList());
        }

        [HttpPost]
        public ActionResult DodajProizvod(int korpaId, int proizvdId, int kolicina)
        {
            Korpa? korpa = _dbContext.Korpa.Find(korpaId);
            if (korpa == null) 
                return BadRequest("korpa ne postoji");

            Proizvod? proizvod = _dbContext.Proizvod.Find(proizvdId);
            if (proizvod == null)
                return BadRequest("proizvod ne postoji");

            var akcije = _dbContext.Akcija.ToList();
            int popust=0;
            foreach (var a in akcije)
            {
                foreach (var p in a.Proizvodi)
                {
                    if (p.ID == proizvdId)
                        popust = a.Iznos;
                }                
            }

            KorpaProizvod kp = new KorpaProizvod()
            {
                Popust=popust,
                Kolicina=kolicina,
                Cijena=proizvod.JedinicnaCijena*kolicina,
                korpaID=korpaId,
                proizvodID=proizvdId
            };
            
            _dbContext.Add(kp);
            _dbContext.SaveChanges();
            return Ok(kp);
        }
    }
}
