using System.ComponentModel.DataAnnotations;

namespace FitVision.Modul2.Models
{
    public class Oprema
    {
        [Key]
        public int ID { get; set; }
        public string Naziv { get; set; }
        public int Broj { get; set; }
        public string Slika { get; set; }
    }
}
