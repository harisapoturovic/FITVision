using FitVision.Modul2.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FitVision.Modul0_Autentifikacija.Models
{
    [Table("KorisnickiNalog")]
    public class KorisnickiNalog
    {
        [Key]
        public int ID { get; set; }
        public string KorisnickoIme { get; set; }
        [JsonIgnore]
        public string Lozinka { get; set; }

        [JsonIgnore]
        public Admin? admin => this as Admin;
        [JsonIgnore]
        public Korisnik? korisnik => this as Korisnik;


        public bool isAdmin => admin != null;
        public bool isKorisnik => korisnik != null;

        //za aktivaciju korisnika preko maila
        public bool isAktiviran { get; set; }
        public string? aktivacijaGUID { get; set; }
    }
}
