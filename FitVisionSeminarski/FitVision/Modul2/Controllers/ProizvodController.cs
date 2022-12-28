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
    public class ProizvodController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public ProizvodController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }


        public class ProizvodSnimiVM
        {
            public int id { get; set; }
            public string naziv { get; set; }
            public int jedinicna_cijena { get; set; }
            public string sastav { get; set; }
            public string jedinicna_mjera { get; set; }
            public int zaliha { get; set; }
            public string slika { get; set; }

            public int pod_kategorija_id { get; set; }
            public int brend_id { get; set; }
        }

        [HttpPost]
        public ActionResult Snimi([FromBody] ProizvodSnimiVM  x)
        {
           
            Proizvod? proizvod;
            if(x.id== 0)
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
            proizvod.Zaliha= x.zaliha;
            proizvod.brendid = x.brend_id;
            proizvod.pod_kategorijaid = x.pod_kategorija_id;

            _dbContext.SaveChanges();
            return Ok(x);
        }
    }
}
