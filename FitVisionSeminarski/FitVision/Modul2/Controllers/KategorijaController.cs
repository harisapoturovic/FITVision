using FitVision.Data;
using FitVision.Modul2.Models;
using FitVision.Modul2.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitVision.Modul2.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class KategorijaController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        public KategorijaController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<List<KategorijaGetAllVM>> GetAll()
        {
            var data = _dbContext.Kategorija
                .Select(k => new KategorijaGetAllVM
                {
                    id = k.Id,
                    naziv = k.Naziv,
                    opis=k.Opis
                });
            return data.Take(100).ToList();
        }

        [HttpPost]
        public ActionResult Snimi([FromBody] KategorijaGetAllVM x)
        {
            Kategorija? objekat;

            if (x.id == 0)
            {
                objekat = new Kategorija();
                _dbContext.Add(objekat);
            }
            else
            {
                objekat = _dbContext.Kategorija.Find(x.id);
            }

            objekat.Naziv = x.naziv;
            objekat.Opis = x.opis;

            _dbContext.SaveChanges();
            return Ok(objekat);
        }

        [HttpPost("{id}")]
        public ActionResult Obrisi(int id)
        {
            Kategorija? kategorija = _dbContext.Kategorija.Find(id);

            if (kategorija == null)
                return BadRequest("pogresan ID");

            _dbContext.Remove(kategorija);

            _dbContext.SaveChanges();
            return Ok(kategorija);
        }
    }
}
