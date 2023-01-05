using FitVision.Data;
using FitVision.Modul2.Models;
using FitVision.Modul2.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitVision.Modul2.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class ForumOdgovorController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public ForumOdgovorController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<List<ForumOdgovorGetVM>> GetAll()
        {
            var data = _dbContext.ForumOdgovor.Select(o => new ForumOdgovorGetVM()
            {
                id = o.ID,
                odgovor = o.Odgovor,
                autor_name = o.AutorIme,
                datum_kreiranja = o.DatumKreiranja.ToString("yyyy-dd-MM")
            });
            return data.Take(100).ToList();
        }

        public class ForumOdgovorAddVM
        {
            public string odgovor { get; set; }
            public string autor_name { get; set; }
            public int forum_tema_id { get; set; }
        }

        [HttpPost]
        public ActionResult Dodaj([FromBody] ForumOdgovorAddVM x)
        {
            ForumOdgovor forumOdgovor= new ForumOdgovor();
            forumOdgovor.Odgovor = x.odgovor;
            forumOdgovor.DatumKreiranja = DateTime.Now;
            forumOdgovor.AutorIme = x.autor_name;
            forumOdgovor.forumTema_id = x.forum_tema_id;

            _dbContext.Add(forumOdgovor);
            _dbContext.SaveChanges();

            return new JsonResult(true);

        }

        [HttpPost]
        public ActionResult Obrisi(int id)
        {
            ForumOdgovor forumOdgovor=_dbContext.ForumOdgovor.FirstOrDefault(f => f.ID==id);
            if (forumOdgovor == null)
                return BadRequest("ne postoji forum odgovor");

            _dbContext.Remove(forumOdgovor);
            _dbContext.SaveChanges();
            return new JsonResult(true);
        }
    }
}
