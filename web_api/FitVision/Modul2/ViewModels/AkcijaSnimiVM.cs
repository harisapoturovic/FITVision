namespace FitVision.Modul2.Controllers
{
    public partial class AkcijaController
    {
        public class AkcijaSnimiVM
        {
            public int id { get; set; }
            public string naziv { get; set; }
            public int iznos { get; set; }
            public string datum_pocetka { get; set; }
            public string datum_zavrsetka { get; set; }

        }
    }
}
