using Microsoft.AspNetCore.SignalR;

namespace FitVision.Hub
{
    public class MessageHub: Hub<IMessageHubClient>
    {
        public async Task SendToAdmin(List<string> message)
        {
            await Clients.All.SendToAdmin(message);
        }
    }
}
