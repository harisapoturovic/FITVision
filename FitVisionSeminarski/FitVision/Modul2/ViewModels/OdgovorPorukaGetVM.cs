using FitVision.Modul0_Autentifikacija.Models;
using FitVision.Modul2.ViewModels;

namespace FitVision.Modul2.Controllers
{
    public partial class PorukaController
    {
        public class OdgovorPorukaGetVM
        {
            public string naslov { get; set; }
            public string sadrzaj { get; set; }
            public string datum_kreiranja { get; set; }
            public KorisnickiNalog korisnicki_nalog { get; set; }
            public List<OdgovorGetVM> odgovori { get; set; }
        }
    }
}
