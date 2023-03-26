using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FitVision.Modul2.Models
{
    public class Odgovor
    {
        [Key]
        public int ID { get; set; }
        public string Sadrzaj { get; set; }
        public DateTime DatumKreiranja { get; set; }
        public string AdminIme { get; set; }


        [ForeignKey(nameof(poruka))]
        public int poruka_id { get; set; }
        public Poruka poruka { get; set; }
    }
}
