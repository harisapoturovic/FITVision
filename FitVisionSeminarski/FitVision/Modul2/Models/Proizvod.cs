using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitVision.Modul2.Models
{
    public class Proizvod
    {
        [Key]
        public int ID { get; set; }
        public string Naziv { get; set; }
        public float JedinicnaCijena { get; set; }
        public string Sastav { get; set; }
        public string JedinicnaMjera { get; set; }
        public int Zaliha { get; set; }
        public string Slika { get; set; }

        [ForeignKey(nameof(pod_kategorija))]
        public int pod_kategorijaid { get; set; }
        public Podkategorija pod_kategorija { get; set; }

        [ForeignKey(nameof(brend))]
        public int brendid { get; set; }
        public Brend brend { get; set; }
    }
}
