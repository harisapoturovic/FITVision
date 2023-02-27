using FitVision.Data;
using FitVision.Helpers.AutentifikacijaAutorizacija;
using FitVision.Modul2.Models;
using FitVision.Modul2.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static FitVision.Modul2.Controllers.AkcijaController;

namespace FitVision.Modul2.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class KorpaProizvodController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public KorpaProizvodController(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<List<KPGetAllVM>> GetAll()
        {
            var data = _dbContext.KorpaProizvod
                .Select(kp => new KPGetAllVM
                {
                    id = kp.Id,
                    kolicina= kp.Kolicina,
                    cijena= kp.Cijena,
                    popust=kp.Popust,
                    proizvodID=kp.proizvodID,
                    korpaID=kp.korpaID
                });

            return data.Take(100).ToList();
        }

        [HttpPost]
        public ActionResult Snimi([FromBody] KPGetAllVM x)
        {

            KorpaProizvod? kp;
            if (x.id == 0)
            {
                kp = new KorpaProizvod();
                _dbContext.Add(kp);
            }
            else
            {
                kp = _dbContext.KorpaProizvod.FirstOrDefault(p => p.Id == x.id);
                if (kp == null)
                    return BadRequest("Proizvod ne postoji");
            }

            kp.Kolicina = x.kolicina;
            kp.Cijena=x.cijena;
            kp.Popust=x.popust;
            kp.korpaID=x.korpaID;
            kp.proizvodID = x.proizvodID;

            _dbContext.SaveChanges();
            return Ok(x);
        }

        [HttpPost("{id}")]
        public ActionResult Obrisi(int id)
        {
            KorpaProizvod? kp = _dbContext.KorpaProizvod.Find(id);
            if (kp == null) return BadRequest("Pogresan ID");

            _dbContext.Remove(kp);
            _dbContext.SaveChanges();
            return Ok(kp);
        }

        [HttpGet]
        public ActionResult GetByKorpa(int korpa_id)
        {
            var data = _dbContext.KorpaProizvod.Where(x => x.korpaID == korpa_id)
                .OrderBy(s => s.korpaID) 
                .Select(s => new KPGetByKorpaVM
                {
                   id = s.Id,
                   kolicina=s.Kolicina,
                   popust=s.Popust,
                   cijena=s.Cijena,
                   cijenaPopust=s.cijenaPopust,
                   proizvodID=s.proizvodID,
                   korpaID=s.korpaID,
                   nazivProizvoda=s.proizvod.Naziv,
                   jedinicnaMjera=s.proizvod.JedinicnaMjera,
                   slika=s.proizvod.Slika
                })
                .AsQueryable();
            return Ok(data.Take(100).ToList());
        }

        [HttpPost]
        public ActionResult DodajProizvod(int korpaId, int proizvdId, int kolicina)
        {
            Korpa? korpa = _dbContext.Korpa.Find(korpaId);
            if (korpa == null) 
                return BadRequest("korpa ne postoji");

            List<Proizvod> proizvodi = _dbContext.Proizvod.Include(p => p.Akcije).ToList();
            Proizvod? proizvod = proizvodi.FirstOrDefault(p => p.ID == proizvdId);
            if (proizvod == null)
                return BadRequest("proizvod ne postoji");

            int popust=0;
            float jedCijena = proizvod.JedinicnaCijena;
            float cijena=0;
            float cijenaP = 0;

            foreach (var i in proizvod.Akcije) //provjera za popust
            {
                popust += i.Iznos;
            }

            KorpaProizvod kp = new KorpaProizvod()
            {
                Popust=popust,
                Kolicina=kolicina,
                Cijena=jedCijena*kolicina,
                cijenaPopust= (jedCijena * kolicina) - ((popust * (jedCijena * kolicina)) / 100),
                korpaID =korpaId,
                proizvodID=proizvdId
            };

            List<KorpaProizvod> kp2 = _dbContext.KorpaProizvod.Where(x => x.proizvodID == proizvdId && x.korpaID == korpaId).ToList();
            if (kp2.Count == 0)
            {
                _dbContext.Add(kp);
                cijena = kp.Cijena;
                popust = kp.Popust;
                cijenaP = kp.cijenaPopust;
            }
            else
            {
                kp2[0].Kolicina = kolicina; //kp2[0] -> [0] zato što uvijek radimo sa jednim objektom, a ne nizom
                kp2[0].Cijena = kolicina * jedCijena;
                kp2[0].Popust = popust;
                kp2[0].cijenaPopust = (jedCijena * kolicina) - ((popust * (jedCijena * kolicina)) / 100);
                cijena = kp2[0].Cijena;
                cijenaP = kp2[0].cijenaPopust;
            }
            var _kp = new
            {
                _popust = popust,
                _cijena = cijena,
                _cijenaPopust = cijenaP
            };
            _dbContext.SaveChanges();
            return Ok(_kp);

        }

        [HttpPost]
        public ActionResult UkloniProizvod(int korpaId, int proizvdId)
        {
            Korpa? korpa = _dbContext.Korpa.Find(korpaId);;
            if (korpa == null) 
                return BadRequest("akcija ne postoji");

            Proizvod? proizvod = _dbContext.Proizvod.Find(proizvdId);
            if (proizvod == null)
                return BadRequest("proizvod ne postoji");

            //List<KorpaProizvod> kp = _dbContext.KorpaProizvod.Where(x => (x.proizvodID == proizvdId && x.korpaID == korpaId)).ToList();
            //_dbContext.KorpaProizvod.RemoveRange(kp);

            List<KorpaProizvod> kp2 = _dbContext.KorpaProizvod.ToList();
            foreach (var i in kp2.ToList())
            {
                if (i.proizvodID == proizvdId && i.korpaID == korpaId)
                    _dbContext.KorpaProizvod.Remove(i);
            }

            _dbContext.SaveChanges();
            return Ok();

        }
    }
}
