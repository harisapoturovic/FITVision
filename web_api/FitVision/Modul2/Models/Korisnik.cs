    using FitVision.Modul0_Autentifikacija.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitVision.Modul2.Models
{
    [Table("Korisnik")]
    public class Korisnik:KorisnickiNalog
    {
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public DateTime DatumRodjenja { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }
        public string Adresa { get; set; }
        public string JMBG { get; set; }
        public string Spol { get; set; }
        public string Visina { get; set; }
        public string Tezina { get; set; }
        public DateTime DatumPolasaka { get; set; }


        [ForeignKey(nameof(grad))]
        public int gradid { get; set; }
        public Grad grad { get; set; }
    }
}
