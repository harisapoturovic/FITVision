using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitVision.Modul2.Models
{
    public class Korpa
    {
        [Key]
        public int Id { get; set; }
        public DateTime DatumKreiranja { get; set; }
        [ForeignKey(nameof(korisnik))]
        public int korisnikID { get; set; }
        public Korisnik korisnik { get; set; }

    }
}
