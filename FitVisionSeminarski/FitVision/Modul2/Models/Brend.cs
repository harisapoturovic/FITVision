using System.ComponentModel.DataAnnotations;

namespace FitVision.Modul2.Models
{
    public class Brend
    {

        [Key]
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }
    }
}
