using FitVision.Data;
using FitVision.Hub;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace FitVision.Modul3_SignalR
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignalRController : ControllerBase
    {

        private readonly IHubContext<PorukeHub, IPorukeHub> _porukeHub;
        private readonly ApplicationDbContext _db;
        public SignalRController(IHubContext<PorukeHub, IPorukeHub> porukeHub, ApplicationDbContext db)
        {
            _porukeHub = porukeHub;
            _db = db;
        }

        [HttpPost]
        public ActionResult Get()
        {
            string poruka = "Pogledajte novu opremu koju ćete moći koristiti u centru već od sutra :)";
            _porukeHub.Clients.All.PosaljiPoruku(poruka);
            return Ok();
        }

    }
}

