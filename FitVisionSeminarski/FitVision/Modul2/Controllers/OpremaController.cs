using FitVision.Data;
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
           

            _dbContext.SaveChanges();
            return Ok(x);
        }
    }
}
