using FitVision.Data;
using FitVision.Modul2.Models;
using FitVision.Modul2.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static FitVision.Modul2.Controllers.AkcijaController;

namespace FitVision.Modul2.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class KorpaController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public KorpaController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        //[HttpPost]
        //public ActionResult Dodaj(int korisnik_id)
        //{
        //    Korpa? korpa;
        //
        //    korpa = new Korpa();
        //    _dbContext.Add(korpa);
        //
        //    korpa.DatumKreiranja = DateTime.Now;
        //    korpa.korisnikID = korisnik_id;
        //    _dbContext.SaveChanges();
        //    return Ok(korpa.Id);
        //}

        [HttpPost]
        public ActionResult Snimi(int korisnikId, int dostavljacId)
        {
            Korpa? korpa;
            if(dostavljacId == 1)
            {
                korpa = new Korpa();
                _dbContext.Add(korpa);
            }
            else
            {
                korpa = _dbContext.Korpa.FirstOrDefault(k => k.korisnikID == korisnikId);
            }
            korpa.DatumKreiranja = DateTime.Now;
            korpa.korisnikID = korisnikId;
            korpa.dostavljacID = dostavljacId;

            _dbContext.SaveChanges();
            return Ok(korpa.Id);
        }
        [HttpGet]
        public ActionResult<List<KorpaDodajVM>> GetAll()
        {
            var data = _dbContext.Korpa
                .Select(k => new KorpaDodajVM
                {
                    id = k.Id,
                    datumKreiranja = k.DatumKreiranja,
                    korisnikId = k.korisnikID
                });

            return data.Take(100).ToList();
        }
        [HttpGet]
        public ActionResult GetByKorisnik(int korisnik_id)
        {
            var data = _dbContext.Korpa.Find(korisnik_id);
            return Ok(data.Id);
        }
        [HttpPost]
        public ActionResult Obrisi()
        {
            var korpe = _dbContext.Korpa.ToList();

            _dbContext.RemoveRange(korpe);
            _dbContext.SaveChanges();
            return Ok(korpe);
        }
    }
}
