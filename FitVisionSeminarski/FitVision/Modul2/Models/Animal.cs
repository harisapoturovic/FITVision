using System.ComponentModel.DataAnnotations;

namespace FitVision.Modul2.Models
{
    public class Animal
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
