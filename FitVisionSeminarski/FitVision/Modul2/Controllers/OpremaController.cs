using FitVision.Data;
using FitVision.Helpers.AutentifikacijaAutorizacija;
using FitVision.Modul2.Models;
using FitVision.Modul2.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace FitVision.Modul2.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class OpremaController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public OpremaController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<List<OpremaGetVM>> GetAll()
        {
            var data = _dbContext.Oprema
                .Select(o => new OpremaGetVM
                {
                    id = o.ID,
                    naziv = o.Naziv,
                    broj = o.Broj,
                    slika = o.Slika,
                    opis= o.Opis,
                    tipOpreme=o.tipOpreme.Naziv,
                    tip_opreme_id=o.tipOpremeID
                  
                });
            return data.Take(100).ToList();
        }


        [HttpPost]
        public ActionResult Snimi([FromBody] OpremaSnimiVM x)
        {
            Oprema? oprema;
            if (x.id == 0)
            {
                oprema = new Oprema();
                _dbContext.Add(oprema);
            }
            else
            {
                oprema = _dbContext.Oprema.FirstOrDefault(o => o.ID == x.id);
                if (oprema == null)
                    return BadRequest("Oprema ne postoji");
            }
            oprema.Naziv = x.naziv;
            oprema.Broj = x.broj;
            oprema.Slika = x.slika;
            oprema.Opis = x.opis;
            oprema.tipOpremeID = x.tip_opreme_id;
           

            _dbContext.SaveChanges();
            return Ok(x);
        }

        [HttpPost("{id}")]
        public ActionResult Obrisi(int id)
        {
            Oprema? oprema = _dbContext.Oprema.Find(id);

            if (oprema == null)
                return BadRequest("pogresan ID");

            _dbContext.Remove(oprema);

            _dbContext.SaveChanges();
            return Ok(oprema);
        }
    }
}
