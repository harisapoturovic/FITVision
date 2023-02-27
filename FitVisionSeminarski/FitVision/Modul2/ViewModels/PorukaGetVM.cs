namespace FitVision.Modul2.Controllers
{
    public partial class PorukaController
    {
        public class PorukaGetVM
        {
            public int id { get; set; }
            public string naslov { get; set; }
            public string sadrzaj { get; set; }
            public string datum_kreiranja { get; set; }
            public string korisnik { get; set; }
        }
    }
}
