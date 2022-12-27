using System.ComponentModel.DataAnnotations;

namespace FitVision.Modul2.Models
{
    public class Kategorija
    {
        [Key]
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }
    }
}
