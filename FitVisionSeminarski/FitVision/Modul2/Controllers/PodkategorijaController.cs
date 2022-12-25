using FitVision.Data;
using FitVision.Modul2.Models;
using FitVision.Modul2.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitVision.Modul2.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class PodkategorijaController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        public PodkategorijaController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<List<PodkategorijaGetAllVM>> GetAll()
        {
            var data = _dbContext.Podkategorija
                .Select(k => new PodkategorijaGetAllVM
                {
                    id = k.Id,
                    naziv = k.Naziv,
                    opis = k.Opis,
                    kategorija_id=k.KategorijaId
                });
            return data.Take(100).ToList();
        }

        [HttpPost]
        public ActionResult Snimi([FromBody] PodkategorijaSnimiVM x)
        {
            Podkategorija? objekat;

            if (x.id == 0)
            {
                objekat = new Podkategorija();
                _dbContext.Add(objekat);
            }
            else
            {
                objekat = _dbContext.Podkategorija.Find(x.id);
            }

            objekat.Naziv = x.naziv;
            objekat.Opis = x.opis;
            objekat.KategorijaId = x.kategorija_id;

            _dbContext.SaveChanges();
            return Ok(objekat);
        }

        [HttpGet]
        public ActionResult GetByKategorija(int kategorija_id)
        {
            var data = _dbContext.Podkategorija.Where(x => x.KategorijaId == kategorija_id)
                .OrderBy(s => s.Naziv)
                .Select(s => new PodkategorijaGetAllVM
                {
                    id = s.Id,
                    naziv = s.Naziv,
                    opis = s.Opis,
                    kategorija_id=s.KategorijaId
                })
                .AsQueryable();
            return Ok(data.Take(100).ToList());
        }

        [HttpPost("{id}")]
        public ActionResult Obrisi(int id)
        {
            Podkategorija? podkategorija = _dbContext.Podkategorija.Find(id);

            if (podkategorija == null)
                return BadRequest("pogresan ID");

            _dbContext.Remove(podkategorija);

            _dbContext.SaveChanges();
            return Ok(podkategorija);
        }
    }
}

