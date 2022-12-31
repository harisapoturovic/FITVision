using System.ComponentModel.DataAnnotations.Schema;

namespace FitVision.Modul2.Models
{
    public class KorpaProizvod
    {

        public int Id { get; set; }
        public int Kolicina { get; set; }
        public float Cijena { get; set; }
        public string Popust { get; set; }

        [ForeignKey(nameof(proizvod))]
        public int proizvodID { get; set; }
        public Proizvod proizvod { get; set; }

        [ForeignKey(nameof(korpa))]
        public int korpaID { get; set; }
        public Korpa korpa { get; set; }

    }
}
