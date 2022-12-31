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

        [HttpPost]
        public ActionResult Dodaj()
        {
            Korpa? korpa;

            korpa = new Korpa();
            _dbContext.Add(korpa);

            korpa.DatumKreiranja = DateTime.Now;
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
                    datumKreiranja=k.DatumKreiranja
                });

            return data.Take(100).ToList();
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
