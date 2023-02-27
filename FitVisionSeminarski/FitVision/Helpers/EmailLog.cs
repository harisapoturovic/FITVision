using FitVision.Modul0_Autentifikacija.Models;
using FitVision.Modul2.Models;

namespace FitVision.Helpers
{
    public class EmailLog
    {
        public static void uspjesnoLogiranKorisnik(KorisnickiNalog logiraniKorisnik, HttpContext httpContext)
        {
            //slanje maila pri loginu admina
            if (logiraniKorisnik.isAdmin)
            {
                EmailSender.Posalji(logiraniKorisnik.admin.Email, "Logiran admin", $"Login info {DateTime.Now}");
            }
        }

        public static void noviKorisnik(Korisnik korisnik, HttpContext httpContext)
        {
            if (!korisnik.isAktiviran)
            {
                // iz requesta saznamo adresu servera
                var Request = httpContext.Request;
                var location = $"{Request.Scheme}://{Request.Host}";


                string url = location + "/korisnik/Aktivacija/" + korisnik.aktivacijaGUID;
                string poruka = $"Postovani/a {korisnik.Ime}, <br> Link za aktivaciju <a href='{url}'>{url}</a>... {DateTime.Now}";
                //slanje maila pri aktivaciji korisnika
                EmailSender.Posalji(korisnik.Email, "Aktivacija korisnika", poruka, true);

            }
        }
    }
}
