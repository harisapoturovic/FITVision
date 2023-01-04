using FitVision.Data;
using FitVision.Modul2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitVision.Modul2.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class ForumTemaController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public ForumTemaController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public class ForumTemaGetAllVM
        {
            public int id { get; set; }
            public string tema { get; set; }
            public string pitanje { get; set; }
            public string datum_kreiranja { get; set; }
            public string autor { get; set; }
        }


        [HttpGet]
        public ActionResult<List<ForumTemaGetAllVM>> GetAll()
        {
            var data = _dbContext.ForumTema
                .Include(f => f.korisnickiNalog).Select(f=>new ForumTemaGetAllVM()
                {
                    id=f.ID,
                    tema=f.Tema,
                    pitanje=f.Pitanje,
                    datum_kreiranja=f.DatumKreiranja.ToString("yyyy-dd-MM"),
                    autor=f.korisnickiNalog.KorisnickoIme
                });

            return data.Take(100).ToList();
        }


        public class ForumTemaAddVM
        {
            public string tema { get; set; }
            public string pitanje { get; set; }
            public int korsinciki_nalog_id { get; set; }

        }

        [HttpPost]
        public ActionResult Dodaj([FromBody] ForumTemaAddVM x)
        {
            ForumTema tema  = new ForumTema();
            tema.Tema = x.tema;
            tema.Pitanje=x.pitanje;
            tema.DatumKreiranja = DateTime.Now;
            tema.korisnickiNalogID = x.korsinciki_nalog_id;

            _dbContext.ForumTema.Add(tema);
            _dbContext.SaveChanges();
            return Ok(x);
        }

        [HttpPost]
        public ActionResult Obrisi(int id)
        {
            ForumTema tema = _dbContext.ForumTema.FirstOrDefault(p => p.ID == id);
            if (tema == null)
            {
                return BadRequest("ne postoji ta tema");
            }
            _dbContext.Remove(tema);
            _dbContext.SaveChanges();
            return new JsonResult(true);
        }
    }
}
