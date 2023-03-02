using FitVision.Data;
using FitVision.Modul2.Models;
using FitVision.Modul2.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitVision.Modul2.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public partial class ProizvodController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public ProizvodController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<List<ProizvodGetVM>> GetAll()
        {
            var data = _dbContext.Proizvod
                .Select(p => new ProizvodGetVM
                {
                    id = p.ID,
                    naziv = p.Naziv,
                    sastav = p.Sastav,
                    zaliha = p.Zaliha,
                    jedinicna_cijena = ((int)p.JedinicnaCijena),
                    jedinicna_mjera = p.JedinicnaMjera,
                    slika = p.Slika,
                    pod_kategorija_id = p.pod_kategorijaid,
                    pod_kategorija = p.pod_kategorija.Naziv,
                    brend_id = p.brendid,
                    brend = p.brend.Naziv,
                    kategorija=p.pod_kategorija.kategorija.Naziv

                });
            return data.Take(100).ToList();
        }

        [HttpPost]
        public ActionResult Snimi([FromBody] ProizvodSnimiVM x)
        {

            Proizvod? proizvod;
            if (x.id == 0)
            {
                proizvod = new Proizvod();
                _dbContext.Add(proizvod);
            }
            else
            {
                proizvod = _dbContext.Proizvod.FirstOrDefault(p => p.ID == x.id);
                if (proizvod == null)
                    return BadRequest("Proizvod ne postoji");
            }


            proizvod.Naziv = x.naziv;
            proizvod.Sastav = x.sastav;
            proizvod.JedinicnaCijena = x.jedinicna_cijena;
            proizvod.JedinicnaMjera = x.jedinicna_mjera;
            proizvod.Slika = x.slika;
            proizvod.Zaliha = x.zaliha;
            proizvod.brendid = x.brend_id;
            proizvod.pod_kategorijaid = x.pod_kategorija_id;

            _dbContext.SaveChanges();
            return Ok(x);
        }


        [HttpPost("{id}")]
        public ActionResult Obrisi(int id)
        {
            /*Oprema? oprema = _dbContext.Oprema.Find(id);

            if (oprema == null)
                return BadRequest("pogresan ID");

            _dbContext.Remove(oprema);

            _dbContext.SaveChanges();
            return Ok(oprema);*/
            Proizvod? proizvod = _dbContext.Proizvod.Find(id);
            if (proizvod == null) return BadRequest("Pogresan ID");

            _dbContext.Remove(proizvod);
            _dbContext.SaveChanges();
            return Ok(proizvod);
        }

        [HttpGet]
        public ActionResult<List<ProizvodGetVM>> GetByPodkatID(int id)
        {
            var proizvodi = _dbContext.Proizvod.Where(x => x.pod_kategorijaid == id).Select
                (p=> new ProizvodGetVM
                {
                    id = p.ID,
                    naziv = p.Naziv,
                    sastav = p.Sastav,
                    zaliha = p.Zaliha,
                    jedinicna_cijena = ((int)p.JedinicnaCijena),
                    jedinicna_mjera = p.JedinicnaMjera,
                    slika = p.Slika,
                    pod_kategorija_id = p.pod_kategorijaid,
                    pod_kategorija = p.pod_kategorija.Naziv,
                    brend_id = p.brendid,
                    brend = p.brend.Naziv,
                    kategorija = p.pod_kategorija.kategorija.Naziv
                });
            return proizvodi.Take(100).ToList();
        }

        [HttpGet]
        public ActionResult<List<ProizvodGetVM>> GetByBrendID(int id)
        {
            var proizvodi = _dbContext.Proizvod.Where(x => x.brendid == id).Select
                (p => new ProizvodGetVM
                {
                    id = p.ID,
                    naziv = p.Naziv,
                    sastav = p.Sastav,
                    zaliha = p.Zaliha,
                    jedinicna_cijena = ((int)p.JedinicnaCijena),
                    jedinicna_mjera = p.JedinicnaMjera,
                    slika = p.Slika,
                    pod_kategorija_id = p.pod_kategorijaid,
                    pod_kategorija = p.pod_kategorija.Naziv,
                    brend_id = p.brendid,
                    brend = p.brend.Naziv,
                    kategorija = p.pod_kategorija.kategorija.Naziv
                });
            return proizvodi.Take(100).ToList();
        }


        [HttpGet]
        public Proizvod GetByProizvodID(int id)
        {
            Proizvod proizvod = _dbContext.Proizvod.Find(id);
            return proizvod;
   
        }

    }
}
