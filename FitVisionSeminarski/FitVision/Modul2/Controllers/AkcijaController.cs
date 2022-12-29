using FitVision.Data;
using FitVision.Modul2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
                    datum_pocetka = p.DatumPocetka.ToString("yyyy-MM-dd"),
                    datum_zavrsetka = p.DatumZavrsetka.ToString("yyyy-MM-dd"),
                    proizvodi = p.Proizvodi

                });

            return data.Take(100).ToList();

        }

        public class AkcijaSnimiVM
        {
            public int id { get; set; }
            public string naziv { get; set; }
            public int iznos { get; set; }
            public string datum_pocetka { get; set; }
            public string datum_zavrsetka { get; set; }

        }

        [HttpPost]
        public ActionResult Snimi([FromBody] AkcijaSnimiVM x)
        {

            Akcija? akcija;
            if (x.id == 0)
            {
                akcija = new Akcija();
                _dbContext.Add(akcija);
            }
            else
            {
                akcija = _dbContext.Akcija.FirstOrDefault(p => p.ID == x.id);
                if (akcija == null)
                    return BadRequest("Proizvod ne postoji");
            }

            akcija.Naziv = x.naziv;
            akcija.Iznos = x.iznos;
            akcija.DatumPocetka = DateTime.Parse(x.datum_pocetka);
            akcija.DatumZavrsetka = DateTime.Parse(x.datum_zavrsetka);

            _dbContext.SaveChanges();
            return Ok(x);
        }

        [HttpPost("{id}")]
        public ActionResult Obrisi(int id)
        {
            Akcija? akcija = _dbContext.Akcija.Find(id);
            if (akcija == null) return BadRequest("Pogresan ID");

            _dbContext.Remove(akcija);
            _dbContext.SaveChanges();
            return Ok(akcija);
        }

        [HttpPost]
        public ActionResult DodajProizvod(int akcijaId, int proizvdId)
        {
            List<Akcija> akcijas = _dbContext.Akcija.Include(a => a.Proizvodi).ToList();
            Akcija? akcija = akcijas.FirstOrDefault(a => a.ID == akcijaId);
            if (akcija == null) return BadRequest("akcija ne postoji");

            Proizvod? proizvod = _dbContext.Proizvod.Find(proizvdId);
            if (proizvod == null)
                return BadRequest("proizvod ne postoji");



            foreach (var pro in akcija.Proizvodi)
            {
                if (pro.ID == proizvod.ID)
                    return BadRequest("Proizvod je vec dodan");
            }

            akcija.Proizvodi.Add(proizvod);


            _dbContext.SaveChanges();
            return Ok(akcija);
        }


        [HttpPost]
        public ActionResult UkloniProizvod(int akcijaId, int proizvdId)
        {

            Akcija? akcija = _dbContext.Akcija.Include(a => a.Proizvodi).FirstOrDefault(a => a.ID == akcijaId);
            if (akcija == null) return BadRequest("akcija ne postoji");

            Proizvod? proizvod = akcija.Proizvodi.FirstOrDefault(p => p.ID == proizvdId);
            if (proizvod == null)
                return BadRequest("proizvod ne postoji u ovoj akciji");

            akcija.Proizvodi.Remove(proizvod);

            _dbContext.SaveChanges();
            return Ok(akcija);

        }
    }
}
