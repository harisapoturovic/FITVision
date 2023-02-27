using FitVision.Data;
using FitVision.Helpers.AutentifikacijaAutorizacija;
using FitVision.Modul2.Models;
using FitVision.Modul2.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static FitVision.Modul2.Controllers.ProizvodController;

namespace FitVision.Modul2.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class DostavljacController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public DostavljacController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }


        [HttpGet]
        [Autorizacija(true, false)]
        public ActionResult<List<DostavljacGetVM>> GetAll()
        {
            var data = _dbContext.Dostavljac
            .Select(p => new DostavljacGetVM
            {
                id = p.Id,
                naziv = p.Naziv,
                email = p.Email,
                telefon = p.Telefon,
                cijenaDostave = p.CijenaDostave
            });
            return data.Take(100).ToList();
        }

        [HttpGet]
        public float GetByDostavljacID(int id)
        {
            var d = _dbContext.Dostavljac.Find(id);
            return d.CijenaDostave;
        }
    }
}