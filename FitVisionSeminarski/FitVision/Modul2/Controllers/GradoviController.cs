using FitVision.Data;
using FitVision.Modul2.Models;
using FitVision.Modul2.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitVision.Modul2.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class GradoviController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public GradoviController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<List<GradGetAllVM>> GetAll()
        {
            var data = _dbContext.Grad
                .Select(g => new GradGetAllVM
                {
                    id = g.ID,
                    naziv = g.Naziv + "-" + g.drzava.Skracenica,
                    postanskiBroj=g.PostanskiBroj                    

                });
            return data.Take(100).ToList();
        }

        [HttpPost]
        public Grad Snimi([FromBody] GradSnimiVM x)
        {
            Grad? objekat;

            if (x.id == 0)
            {
                objekat = new Grad();
                _dbContext.Add(objekat);
            }
            else
            {
                objekat = _dbContext.Grad.Find(x.id);
            }

            objekat.drzavaID = x.drzavaID;
            objekat.Naziv = x.naziv;
            objekat.PostanskiBroj = x.postanskiBroj;

            _dbContext.SaveChanges();
            return objekat;
        }


        [HttpGet]
        public ActionResult GetByDrzava(int drzava_id)
        {
            var data = _dbContext.Grad.Where(x => x.drzavaID == drzava_id)
                .OrderBy(s => s.Naziv)
                .Select(s => new GradGetAllVM
                {
                    id = s.ID,
                    naziv = s.Naziv + " - " + s.drzava.Skracenica,
                    postanskiBroj=s.PostanskiBroj
                })
                .AsQueryable();
            return Ok(data.Take(100).ToList());
        }

        [HttpPost("{id}")]
        public ActionResult Obrisi(int id)
        {
            Grad? grad = _dbContext.Grad.Find(id);

            if (grad == null)
                return BadRequest("pogresan ID");

            _dbContext.Remove(grad);

            _dbContext.SaveChanges();
            return Ok(grad);
        }
    }
}
