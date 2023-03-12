using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace FitVision.Modul3_SignalR
{
    public class PorukeHub : Hub<IPorukeHub>
    {
        public async Task PosaljiPoruku(string message)
        {
            await Clients.All.PosaljiPoruku(message);
        }
    }
}
