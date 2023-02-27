using FitVision.Modul0_Autentifikacija.Models;
using FitVision.Modul2.Models;

namespace FitVision.Helpers
{
    public class EmailLog
    {
        public static void uspjesnoLogiranKorisnik(KorisnickiNalog logiraniKorisnik, HttpContext httpContext)
        {
            if (logiraniKorisnik.isAdmin)
            {
                EmailSender.Posalji(logiraniKorisnik.admin.Email, "Logiran admin", $"Login info {DateTime.Now}");
            }
        }

        public static void noviNastavnik(Admin admin, HttpContext httpContext)
        {
            if (!admin.isAktiviran)
            {
                var Request = httpContext.Request;
                var location = $"{Request.Scheme}://{Request.Host}";


                string url = location + "/nastavnik/Aktivacija/" + admin.aktivacijaGUID;
                string poruka = $"Postovani/a {admin.Ime}, <br> Link za aktivaciju <a href='{url}'>{url}</a>... {DateTime.Now}";
                EmailSender.Posalji(admin.Email, "Aktivacija korisnika", poruka, true);

            }
        }
    }
}
