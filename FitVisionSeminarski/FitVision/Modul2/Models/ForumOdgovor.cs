using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FitVision.Modul2.Models
{
    public class ForumOdgovor
    {
        [Key]
        public int ID { get; set; }
        public string Odgovor { get; set; }
        public DateTime DatumKreiranja { get; set; }
        public string AutorIme { get; set; }


        [ForeignKey(nameof(forumTema))]
        public int forumTema_id { get; set; }
        public ForumTema forumTema { get; set; }
    }
}
