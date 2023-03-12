namespace FitVision.Modul3_SignalR
{
    public interface IPorukeHub
    {
        Task PosaljiPoruku(string message);
    }
}
