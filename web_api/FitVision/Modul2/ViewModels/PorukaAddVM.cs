namespace FitVision.Modul2.Controllers
{
    public partial class PorukaController
    {
        public class PorukaAddVM
        {
            public string naslov { get; set; }
            public string sadrzaj { get; set; }
            public int korsinciki_nalog_id { get; set; }

        }
    }
}
