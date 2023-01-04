using System.ComponentModel.DataAnnotations;

namespace FitVision.Modul2.Models
{
    public class Korpa
    {
        [Key]
        public int Id { get; set; }
        public DateTime DatumKreiranja { get; set; }
        
    }
}
