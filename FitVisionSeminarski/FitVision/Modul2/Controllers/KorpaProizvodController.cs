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
    }
}
