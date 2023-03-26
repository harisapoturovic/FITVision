using FitVision.Data;
using FitVision.Modul2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitVision.Modul2.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class TipOpremeController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public TipOpremeController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public class TipOpremeSnimiVM
        {
            public int id { get; set; }
            public string naziv { get; set; }
        }

        public class TipOpremeGetVM
        {
            public int id { get; set; }
            public string naziv { get; set; }
        }

        [HttpGet]
        public ActionResult<List<TipOpremeGetVM>> GetAll()
        {
            var data = _dbContext.TipOpreme
                .Select(o => new TipOpremeGetVM
                {
                    id = o.ID,
                    naziv = o.Naziv,

                });
            return data.Take(100).ToList();
        }

        [HttpPost]
        public ActionResult Snimi([FromBody] TipOpremeSnimiVM x )
        {
            TipOpreme? tipOpreme;
            if(x.id== 0)
            {
                tipOpreme=new TipOpreme();
                _dbContext.Add(tipOpreme);
            }
            else
            {
                tipOpreme = _dbContext.TipOpreme.FirstOrDefault(t => t.ID == x.id);
                if (tipOpreme == null)
                    return BadRequest("Ne postoji tip opreme");
            }
            tipOpreme.ID = x.id;
            tipOpreme.Naziv= x.naziv;
            _dbContext.SaveChanges();
            return Ok(tipOpreme);
                   
        }

        [HttpPost("{id}")]
        public ActionResult Obrisi(int id)
        {
            TipOpreme? tipOpreme = _dbContext.TipOpreme.Find(id);
            if (tipOpreme == null)
                return BadRequest("pogrešan ID");
            _dbContext.Remove(tipOpreme);
            _dbContext.SaveChanges();
            return Ok(tipOpreme);
        }
    }
}
