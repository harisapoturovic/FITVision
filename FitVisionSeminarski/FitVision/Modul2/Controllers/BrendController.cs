using FitVision.Data;
using FitVision.Modul2.Models;
using FitVision.Modul2.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitVision.Modul2.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class BrendController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        public BrendController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<List<BrendGetAllVM>> GetAll()
        {
            var data = _dbContext.Brend
                .Select(k => new BrendGetAllVM
                {
                    id = k.Id,
                    naziv = k.Naziv,
                    opis=k.Opis,
                    slika=k.Slika,
                    brend_jel_selektovan = false
                });
            return data.Take(100).ToList();
        }

        [HttpPost]
        public ActionResult Snimi([FromBody] BrendGetAllVM x)
        {
            Brend? objekat;

            if (x.id == 0)
            {
                objekat = new Brend();
                _dbContext.Add(objekat);
            }
            else
            {
                objekat = _dbContext.Brend.Find(x.id);
            }

            objekat.Naziv = x.naziv;
            objekat.Opis = x.opis;
            objekat.Slika = x.slika;

            _dbContext.SaveChanges();
            return Ok(objekat);
        }

        [HttpPost("{id}")]
        public ActionResult Obrisi(int id)
        {
            Brend? brend = _dbContext.Brend.Find(id);

            if (brend == null)
                return BadRequest("pogresan ID");

            _dbContext.Remove(brend);

            _dbContext.SaveChanges();
            return Ok(brend);
        }
    }
}
