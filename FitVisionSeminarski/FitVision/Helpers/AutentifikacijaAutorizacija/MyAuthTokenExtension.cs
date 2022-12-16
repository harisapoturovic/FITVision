using FitVision.Data;
using FitVision.Modul0_Autentifikacija.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace FitVision.Helpers.AutentifikacijaAutorizacija
{
    public static class MyAuthTokenExtension
    {
        public class LoginInformacije
        {
            public LoginInformacije(AutentifikacijaToken? autentifikacijaToken)
            {
                this.autentifikacijaToken = autentifikacijaToken;
            }

            [JsonIgnore]
            public KorisnickiNalog? korisnickiNalog => autentifikacijaToken?.KorisnickiNalog;
            public AutentifikacijaToken? autentifikacijaToken { get; set; }

            public bool isLogiran => korisnickiNalog != null;
            public bool isPremisijaAdmin => isLogiran && korisnickiNalog.isAdmin;
            public bool isPremisijaKorisnik => isLogiran && korisnickiNalog.isKorisnik;

        }

        public static LoginInformacije GetLoginInfo(this HttpContext httpContext)
        {
            var token = httpContext.GetAuthToken();

            return new LoginInformacije(token);
        }

        public static AutentifikacijaToken? GetAuthToken(this HttpContext httpContext)
        {
            string token = httpContext.GetMyAuthToken();
            ApplicationDbContext db = httpContext.RequestServices.GetService<ApplicationDbContext>();

            AutentifikacijaToken? korisnickiNalog = db.AutentifikacijaToken
                .Include(s => s.KorisnickiNalog)
                .SingleOrDefault(x => x.vrijednost == token);

            return korisnickiNalog;
        }


        public static string GetMyAuthToken(this HttpContext httpContext)
        {
            string token = httpContext.Request.Headers["autentifikacija-token"];
            return token;
        }
    }
}
