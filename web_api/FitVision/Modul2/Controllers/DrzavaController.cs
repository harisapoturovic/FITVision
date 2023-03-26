using FitVision.Data;
using FitVision.Modul2.Models;
using FitVision.Modul2.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitVision.Modul2.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class DrzavaController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        public DrzavaController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<List<DrzavaGetAllVM>> GetAll()
        {
            var data = _dbContext.Drzava
                .Select(d => new DrzavaGetAllVM
                {
                    id = d.ID,
                    naziv = d.Naziv,
                    skracenica=d.Skracenica

                });
            return data.Take(100).ToList();
        }

        [HttpPost]
        public Drzava Snimi([FromBody] DrzavaSnimiVM x)
        {
            Drzava? objekat;

            if (x.id == 0)
            {
                objekat = new Drzava();
                _dbContext.Add(objekat);
            }
            else
            {
                objekat = _dbContext.Drzava.Find(x.id);
            }

            objekat.Naziv = x.naziv;
            objekat.Skracenica = x.skracenica;

            _dbContext.SaveChanges();
            return objekat;
        }

        [HttpPost("{id}")]
        public ActionResult Obrisi(int id)
        {
            Drzava? drzava = _dbContext.Drzava.Find(id);

            if (drzava == null)
                return BadRequest("pogresan ID");

            _dbContext.Remove(drzava);

            _dbContext.SaveChanges();
            return Ok(drzava);
        }
    }
}
