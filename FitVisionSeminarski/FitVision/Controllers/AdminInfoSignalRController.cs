using FitVision.Data;
using FitVision.Hub;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace FitVision.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminInfoSignalRController : ControllerBase
    {
        private IHubContext<MessageHub, IMessageHubClient> messageHub;
        private readonly ApplicationDbContext _dbContext;

        public AdminInfoSignalRController(ApplicationDbContext dbContext, IHubContext<MessageHub, IMessageHubClient> _messageHub)
        {
            messageHub = _messageHub;
            this._dbContext = dbContext;
        }



        [HttpPost]
        public string Get()
        {
            List<string> data = new List<string>();
            data.Add("Broj korisnika: " + _dbContext.Korisnik.Count().ToString());
            data.Add("Broj stavki opreme " + _dbContext.Oprema.Count().ToString());
            data.Add("Broj proizvoda " + _dbContext.Proizvod.Count().ToString());
            data.Add("Broj admina " + _dbContext.Admin.Count().ToString());
            messageHub.Clients.All.SendToAdmin(data);
            return "Offers sent successfully to all users!";
        }
    }
}
