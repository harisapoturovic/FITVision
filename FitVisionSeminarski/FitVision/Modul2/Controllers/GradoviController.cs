using FitVision.Data;
using FitVision.Modul2.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitVision.Modul2.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class GradoviController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public GradoviController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<List<GradGetVM>> GetAll()
        {
            var data = _dbContext.Grad
                .Select(g => new GradGetVM
                {
                    id = g.ID,
                    opis = g.Naziv + "-" + g.drzava.Naziv

                });
            return data.Take(100).ToList();
        }
    }
}
