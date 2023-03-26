using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitVision.Modul2.Models
{
    public class Podkategorija
    {
        [Key]
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }
        public string? Slika { get; set; }
        [ForeignKey(nameof(Kategorija))]
        public int KategorijaId { get; set; }
        public Kategorija kategorija { get; set; }
    }
}
