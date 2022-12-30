namespace FitVision.Modul2.Controllers
{
    public partial class ProizvodController
    {
        public class ProizvodGetVM
        {
            public int id { get; set; }
            public string naziv { get; set; }
            public float jedinicna_cijena { get; set; }
            public string sastav { get; set; }
            public string jedinicna_mjera { get; set; }
            public int zaliha { get; set; }
            public string slika { get; set; }

            public string pod_kategorija { get; set; }
            public int pod_kategorija_id { get; set; }

            public string brend { get; set; }
            public int brend_id { get; set; }
        }
    }
}
