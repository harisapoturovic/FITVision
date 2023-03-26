namespace FitVision.Hub
{
    public interface IMessageHubClient
    {
        Task SendToAdmin(List<string> message);
    }
}
