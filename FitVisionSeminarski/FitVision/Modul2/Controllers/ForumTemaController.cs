using FitVision.Data;
using FitVision.Modul2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

            _dbContext.Add(tema);
            _dbContext.SaveChanges();
            return Ok(x);
        }
    }
}
