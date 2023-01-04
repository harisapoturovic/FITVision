using System.ComponentModel.DataAnnotations;

namespace FitVision.Modul2.Models
{
    public class Akcija
    {
        [Key]
        public int ID { get; set; }
        public string Naziv { get; set; }
        public int Iznos { get; set; }
        public DateTime DatumPocetka { get; set; }
        public DateTime DatumZavrsetka { get; set; }

        public List<Proizvod> Proizvodi { get; set; } = new List<Proizvod>();// treba incijalizirati proizvode 
    }
}
