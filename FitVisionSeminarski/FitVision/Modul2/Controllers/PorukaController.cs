using FitVision.Data;
using FitVision.Modul2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitVision.Modul2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PorukaController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public PorukaController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
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
