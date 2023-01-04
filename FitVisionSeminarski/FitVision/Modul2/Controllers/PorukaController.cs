using FitVision.Data;
using FitVision.Modul2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitVision.Modul2.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class PorukaController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public PorukaController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }


        [HttpGet]
        public ActionResult<List<Poruka>> GetAll()
        {
            var data = _dbContext.Poruka
                .Include(p => p.korisnickiNalog).AsQueryable();

            return data.Take(100).ToList();
        }

        [HttpGet]
        public ActionResult<List<Poruka>> GetByKorsinikId(int id)
        {
            List<Poruka> poruke = _dbContext.Poruka.Include(p => p.korisnickiNalog).Where(p => p.korisnickiNalog.ID == id).ToList();

            return poruke;
        }


        public class PorukaAddVM
        {
            public string naslov { get; set; }
            public string sadrzaj { get; set; }
            public int korsinciki_nalog_id { get; set; }

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
    }
}
