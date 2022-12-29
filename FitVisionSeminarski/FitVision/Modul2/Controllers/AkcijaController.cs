using FitVision.Data;
using FitVision.Modul2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitVision.Modul2.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class AkcijaController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public AkcijaController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }


        public class AkcijaGetVM
        {
            public int id { get; set; }
            public string naziv { get; set; }
            public int iznos { get; set; }
            public string datum_pocetka { get; set; }
            public string datum_zavrsetka { get; set; }
            public List<Proizvod> proizvodi { get; set; }

        }

        [HttpGet]
        public ActionResult<List<AkcijaGetVM>> GetAll()
        {
            var data = _dbContext.Akcija
                .Select(p => new AkcijaGetVM
                {
                    id = p.ID,
                    iznos = p.Iznos,
                    naziv=p.Naziv,
                    datum_pocetka = p.DatumPocetka.ToString("yyyy-dd-MM"),
                    datum_zavrsetka = p.DatumZavrsetka.ToString("yyyy-dd-MM"),
                    proizvodi = p.Proizvodi

                });

            return data.Take(100).ToList();

        }
    }
}
