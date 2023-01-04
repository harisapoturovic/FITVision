using FitVision.Data;
using FitVision.Modul0_Autentifikacija.Models;
using FitVision.Modul2.Models;
using FitVision.Modul2.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitVision.Modul2.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class OdgovorController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public OdgovorController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<List<Odgovor>> GetAll()
        {
            var data = _dbContext.Odgovor;
            return data.Take(100).ToList();
        }


      


        public class OdgovorAddVM
        {
            public string sadrzaj { get; set; }
            public string admin_name { get; set; }
            public int poruka_id { get; set; }
        }

        [HttpPost]
        public ActionResult Dodaj([FromBody] OdgovorAddVM x)
        {
            Odgovor odgovor = new Odgovor();
            odgovor.Sadrzaj = x.sadrzaj;
            odgovor.DatumKreiranja = DateTime.Now;
            odgovor.AdminIme = x.admin_name;
            odgovor.poruka_id = x.poruka_id;

            _dbContext.Add(odgovor);
            _dbContext.SaveChanges();

            return new JsonResult(true);

        }
    }
}
