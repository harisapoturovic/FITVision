using FitVision.Modul0_Autentifikacija.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitVision.Modul2.Models
{
    public class Poruka
    {
        public int ID { get; set; }
        public string Naslov { get; set; }
        public string Sadrzaj { get; set; }
        public DateTime DatumKreiranja { get; set; }

        [ForeignKey(nameof(korisnickiNalog))]
        public int korisnickiNalogID { get; set; }
        public KorisnickiNalog korisnickiNalog { get; set; }
    }
}
