using System.ComponentModel.DataAnnotations;

namespace FitVision.Modul2.Models
{
    public class Dostavljac
    {
        [Key]
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string? Telefon { get; set; }
        public string? Email { get; set; }
        public float CijenaDostave { get; set; }
    }
}
