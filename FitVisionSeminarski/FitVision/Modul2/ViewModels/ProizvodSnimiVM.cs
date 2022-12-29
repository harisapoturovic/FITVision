namespace FitVision.Modul2.Controllers
{
    public partial class ProizvodController
    {
        public class ProizvodSnimiVM
        {
            public int id { get; set; }
            public string naziv { get; set; }
            public int jedinicna_cijena { get; set; }
            public string sastav { get; set; }
            public string jedinicna_mjera { get; set; }
            public int zaliha { get; set; }
            public string slika { get; set; }

            public int pod_kategorija_id { get; set; }
            public int brend_id { get; set; }
        }
    }
}
