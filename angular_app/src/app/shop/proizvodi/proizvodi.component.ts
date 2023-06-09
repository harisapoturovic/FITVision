import {Component, NgModule, OnInit} from '@angular/core';
import {LoginInformacije} from "../../_helpers/login-informacije";
import {AutentifikacijaHelper} from "../../_helpers/autentifikacija-helper";
import {HttpClient} from "@angular/common/http";
import {MojConfig} from "../../moj-config";
import {KorpaService} from "../KorpaService";
import {ProizvodService} from "./ProizvodService";

declare function porukaSuccess(a: string):any;
declare function porukaError(a: string):any;
@Component({
  selector: 'app-proizvodi',
  templateUrl: './proizvodi.component.html',
  styleUrls: ['./proizvodi.component.css']
})
export class ProizvodiComponent implements OnInit{
  popust: any;
  novaCijena:any;
  od:any=0;
  do:any=100;

  constructor(private httpKlijent: HttpClient, private korpaService:KorpaService, private proizvodService:ProizvodService ) {
  }

  loginInfo(): LoginInformacije {
    return AutentifikacijaHelper.getLoginInfo();
  }

  ngOnInit(): void {
    this.ucitajBrendove();
    this.ucitajPodKategorije();
    this.ucitajProizvode();
    if (this.loginInfo().isPremisijaKorisnik) {
      this.korpaID=this.korpaService.getKorpaID();
    }

  }

  ucitajProizvode() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/Proizvod/GetAll").subscribe(x => {
      this.proizvodi = x;
    })
    this.od=0;
    this.do=100;
    this.filter_naziv=false;
    this.naziv=null;
  }

  ucitajBrendove() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/Brend/GetAll").subscribe(x => {
      this.brendovi = x;
    })
  }

  ucitajPodKategorije() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/Podkategorija/GetAll").subscribe(x => {
      this.podKategorije = x;
    })
  }

  proizvodObject: any;
  brendovi: any;
  podKategorije: any
  proizvodi: any;
  url: "";
  filter_naziv: any;
  naziv: any;
  podkatID: number;
  brendID: number;
  korpaID: any;
  kolicina: any = 0;
  korpaObject: any;
  cijenaProizvoda:any;
  cijena2:any;
  popust2:any;
  ukupno:any;

  dodajFunc() {
    this.proizvodObject = {
      id: 0,
      naziv: "",
      jedinicna_cijena: 0,
      sastav: "",
      jedinicna_mjera: "",
      zaliha: 0,
      slika: "assets/empty.png",
      pod_kategorija_id: 0,
      brend_id: 0
    }
  }

  snimiProizvod() {
    if (this.proizvodObject.naziv != "" && this.proizvodObject.zaliha >= 0 && this.proizvodObject.brend_id > 0 &&
      this.proizvodObject.pod_kategorija_id > 0 && this.proizvodObject.jedinicna_mjera != "" && this.proizvodObject.sastav != "" &&
      this.proizvodObject.jedinicna_cijena > 0) {
      this.httpKlijent.post(MojConfig.adresa_servera + "/Proizvod/Snimi", this.proizvodObject, MojConfig.http_opcije())
        .subscribe(x => {
          this.proizvodObject = null;
          this.ucitajProizvode();
        });
      porukaSuccess("uspjesno snimljen proizvod");
    } else {
      porukaError("niste unijeli sve podatke");
    }
  }

  detaljno(p: any) {
    this.proizvodService.setProizvodID(p.id);
  }

  urediFunc(p: any) {
    this.proizvodObject = p;
  }

  ChosenFile($event: Event) {
    let file = (event.target as HTMLInputElement).files[0];
    var reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = (e: any) => {
      this.url = e.target.result;
      this.proizvodObject.slika = this.url;

    }
  }

  obrisiProizvod(p: any) {
    let con= confirm("Da li zelite obrisati proizvod?");
    if(con.valueOf()==true) {
      this.httpKlijent.post(MojConfig.adresa_servera + `/Proizvod/Obrisi/${p.id}`, null).subscribe(x => {
        this.ucitajProizvode();

      })
      porukaSuccess("uspjesno brisanje");
    }
  }
pomocna:boolean=true; //samo zbog pocetnog prikaza proizvoda
pomocna2:boolean=true;
  getProizvode() {
    if (this.proizvodi == null)
      return [];
    var pr = this.proizvodi.filter(
      (x: any) =>
        (!this.filter_naziv || (x.naziv).toLowerCase().startsWith(this.naziv)) &&
        (x.jedinicna_cijena>=this.od && x.jedinicna_cijena<this.do) &&
        //(!this.filter_podkat || x.pod_kategorija_id == this.podkatID) &&
        //(!this.filter_brend || x.brend_id == this.brendID) &&
        (this.pomocna || x.pod_kategorija_id == this.podkatID) &&
        (this.pomocna2 || x.brend_id == this.brendID)
    );
    if (pr.length == 0)
      console.log("Nema proizvoda");
    else
      return pr;
  }

 // napraviKorpu() {
 //   this.httpKlijent.post(MojConfig.adresa_servera + `/Korpa/Snimi?korisnikId=${this.korisnik_id}`, null).subscribe(x => {
 //     this.korpaID = x;
 //     this.korpaService.setKorpaID(x);
 //   })
 // }


  //getByKorisnikID()
  //{
  //  this.httpKlijent.get(MojConfig.adresa_servera + `/Korpa/GetByKorisnikID?korisnik_id=${this.korisnik_id}`).subscribe(x => {
  //    this.korpaID=x;
  //  })
  //}
//

  dodajUKorpu(p: any) {
      if (this.kolicina != 0) {
        if(this.kolicina<=p.zaliha)
        {
          if(this.kolicina<0)
            porukaError("Količina mora biti veća od 0!");
          else
          {
            // @ts-ignore
            this.httpKlijent.post(MojConfig.adresa_servera +
              `/KorpaProizvod/DodajProizvod?korpaId=${this.korpaID}&proizvdId=${p.id}&kolicina=${this.kolicina}`)
              .subscribe(x => {
                this.prikaziSadrzaj();
                this.kolicina = 0; // kako se ne bi mogli dodavati proizvodi sa kolicinom = 0
                porukaSuccess("Uspješno dodan proizvod u korpu!");
              })
          }
        }
        else
          porukaError(`Na zalihi imamo ${p.zaliha} proizvoda!`);
      } else
        porukaError("Niste odabrali količinu");
  }


  prikaziSadrzaj() {
    if(this.korpaID==null)
      porukaError("Korpa je prazna");
    else {
      this.httpKlijent.get(MojConfig.adresa_servera +
        `/KorpaProizvod/GetByKorpa?korpa_id=${this.korpaID}`).subscribe(x => {
        this.korpaObject = x;
        if (this.korpaObject.length == 0)
          porukaError("Korpa je prazna");
        else {
          let cijena=0;
          let popust=0;
          let ukupno=0;
          this.korpaObject.forEach((k:any)=>cijena+=k.cijena);
          this.korpaObject.forEach((k:any)=>popust+=(k.popust*k.cijena/100));
          this.korpaObject.forEach((k:any)=>ukupno+=k.cijenaPopust);
          this.zaPrikaz(cijena, popust, ukupno);
        }
      })
    }
  }

  zaPrikaz(c:any, p:any, u:any)
  {
    this.cijena2=c;
    this.popust2=p;
    this.ukupno=u;
  }

  ukloniProizvod(proizvodID: any) {
    this.httpKlijent.post(MojConfig.adresa_servera + `/KorpaProizvod/UkloniProizvod?korpaId=${this.korpaID}&proizvdId=${proizvodID}`, null)
      .subscribe(x => {
        this.prikaziSadrzaj();
      })
  }
  p:any;
  izmijeniKolicinu(kolicina: any, korpaID:any, proizvodID:any) {
    for (let p of this.proizvodi)
    {
      if(p.id==proizvodID)
        this.p=p;
    }
      if (this.p.id == proizvodID && kolicina<=this.p.zaliha) {
        if(kolicina<0)
          porukaError("Količina mora biti veća od 0!");
        else
        {
          // @ts-ignore
          this.httpKlijent.post(MojConfig.adresa_servera +
            `/KorpaProizvod/DodajProizvod?korpaId=${korpaID}&proizvdId=${proizvodID}&kolicina=${kolicina}`)
            .subscribe((x: any) => {
              this.cijenaProizvoda = x._cijena;
              this.popust = x._popust;
              this.novaCijena = x._cijenaPopust;
              for (let k of this.korpaObject) {
                if (k.korpaID == korpaID && k.proizvodID == proizvodID) {
                  k.cijena = this.cijenaProizvoda;
                  k.popust = this.popust;
                  k.cijenaPopust = this.novaCijena;
                }
              }
              this.prikaziSadrzaj();
            })
        }
      } else
        porukaError(`Na zalihi imamo ${this.p.zaliha} proizvoda`);
  }

  onKey(event: Event) {
    // @ts-ignore
    if(event.target.value!=0)
      { // @ts-ignore
        this.kolicina = event.target.value;
      }
  }

  prikazi(id:any) {
    this.podkatID=id;
    this.httpKlijent.get(MojConfig.adresa_servera + `/Proizvod/GetByPodkatID?id=${id}`).subscribe((x:any) => {
      this.proizvodi = x;
      this.getProizvode();
    })
  }

  prikazi2(id:any) {
    this.brendID=id;
    this.httpKlijent.get(MojConfig.adresa_servera + `/Proizvod/GetByBrendID?id=${id}`).subscribe((x:any) => {
      this.proizvodi = x;
      this.getProizvode();
    })
  }
}

