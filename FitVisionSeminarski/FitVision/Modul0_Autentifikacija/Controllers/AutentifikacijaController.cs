using FitVision.Data;
using FitVision.Helpers.AutentifikacijaAutorizacija;
using FitVision.Helpers;
using FitVision.Modul0_Autentifikacija.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FitVision.Modul0_Autentifikacija.ViewModels;

namespace FitVision.Modul0_Autentifikacija.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class AutentifikacijaController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public AutentifikacijaController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpGet("{code}")]
        public ActionResult Otkljucaj(string code)
        {
            var korisnickiNalog = HttpContext.GetLoginInfo().korisnickiNalog;

            if (korisnickiNalog == null)
            {
                return BadRequest("korisnik nije logiran");
            }

            var token = _dbContext.AutentifikacijaToken.FirstOrDefault(s => s.twoFCode == code && s.KorisnickiNalogId == korisnickiNalog.ID);
            if (token != null)
            {
                token.twoFJelOtkljucano = true;
                _dbContext.SaveChanges();
                return Ok();
            }

            return BadRequest("pogresan URL");
        }

        [HttpPost]
        public ActionResult<MyAuthTokenExtension.LoginInformacije> Login([FromBody] LoginVM x)
        {
            //1- provjera logina
            KorisnickiNalog? logiraniKorisnik = _dbContext.KorisnickiNalog
                .FirstOrDefault(k =>
                k.KorisnickoIme == x.korisnickoIme && k.Lozinka == x.lozinka);

            if (logiraniKorisnik == null)
            {
                //pogresan username i password
                return new MyAuthTokenExtension.LoginInformacije(null);
            }

            //2- generisati random string
            string randomString = TokenGenerator.Generate(10);
            string twoFCode = TokenGenerator.Generate(4);

            //3- dodati novi zapis u tabelu AutentifikacijaToken za logiraniKorisnikId i randomString
            var noviToken = new AutentifikacijaToken()
            {
                ipAdresa = Request.HttpContext.Connection.RemoteIpAddress?.ToString(),
                vrijednost = randomString,
                KorisnickiNalog = logiraniKorisnik,
                vrijemeEvidentiranja = DateTime.Now,
                twoFCode = twoFCode
            };

            _dbContext.Add(noviToken);
            _dbContext.SaveChanges();

            EmailLog.uspjesnoLogiranKorisnik(noviToken, Request.HttpContext);

            //4- vratiti token string
            return new MyAuthTokenExtension.LoginInformacije(noviToken);
        }

        [HttpPost]
        public ActionResult Logout()
        {
            AutentifikacijaToken? autentifikacijaToken = HttpContext.GetAuthToken();

            if (autentifikacijaToken == null)
                return Ok();

            _dbContext.Remove(autentifikacijaToken);
            _dbContext.SaveChanges();
            return Ok();
        }

        [HttpGet]
        public ActionResult<AutentifikacijaToken?> Get()
        {
            AutentifikacijaToken? autentifikacijaToken = HttpContext.GetAuthToken();

            return autentifikacijaToken;
        }
    }
}
