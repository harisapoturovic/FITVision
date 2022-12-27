using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitVision.Modul2.Models
{
    public class Oprema
    {
        [Key]
        public int ID { get; set; }
        public string Naziv { get; set; }
        public int Broj { get; set; }
        public string Slika { get; set; }
        public string Opis { get; set; }

        [ForeignKey(nameof(tipOpreme))]
        public int tipOpremeID { get; set; }
        public TipOpreme tipOpreme { get; set; }
    
     }
}
