import { Component, OnInit } from '@angular/core';
import {LoginInformacije} from "../../_helpers/login-informacije";
import {AutentifikacijaHelper} from "../../_helpers/autentifikacija-helper";
import {HttpClient} from "@angular/common/http";
import {MojConfig} from "../../moj-config";

//declare function porukaSuccess(a: string):any;
//declare function porukaError(a: string):any;

@Component({
  selector: 'app-proizvodi',
  templateUrl: './proizvodi.component.html',
  styleUrls: ['./proizvodi.component.css']
})
export class ProizvodiComponent implements OnInit {

  constructor(private httpKlijent: HttpClient) {
  }

  loginInfo(): LoginInformacije {
    return AutentifikacijaHelper.getLoginInfo();
  }

  ngOnInit(): void {
    this.ucitajBrendove();
    this.ucitajPodKategorije();
    this.ucitajProizvode();
    if(this.loginInfo().isPremisijaKorisnik)
      this.napraviKorpu();
  }

  ucitajProizvode() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/Proizvod/GetAll").subscribe(x => {
      this.proizvodi = x;
    })
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
  detaljiProizvod: any;
  url: "";
  filter_naziv: any;
  naziv: any;
  filter_cijena1: any;
  filter_cijena2: any;
  filter_cijena3: any;
  filter_cijena4: any;
  filter_podkat: any;
  podkatID: number;
  filter_brend: any;
  brendID: number;
  korpaID: any;
  kolicina: any = 0;
  korpaObject: any;
  cijenaProizvoda:any;

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
      //porukaSuccess("uspjesno snimljen proizvod");
    } else {
      alert("niste unijeli sve podatke");
    }
  }

  detaljno(p: any) {
    this.detaljiProizvod = p;
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
    this.httpKlijent.post(MojConfig.adresa_servera + `/Proizvod/Obrisi/${p.id}`, null).subscribe(x => {
      this.ucitajProizvode();

    })
    //porukaSuccess("uspjesno brisanje");
  }

  getProizvode() {

    if (this.proizvodi == null)
      return [];
    var pr = this.proizvodi.filter(
      (x: any) =>
        (!this.filter_naziv ||
          (x.naziv).toLowerCase().startsWith(this.naziv)) &&
        ((!this.filter_cijena1 || x.jedinicna_cijena > 0 && x.jedinicna_cijena < 20) &&
          (!this.filter_cijena2 || x.jedinicna_cijena >= 20 && x.jedinicna_cijena < 50) &&
          (!this.filter_cijena3 || x.jedinicna_cijena >= 50 && x.jedinicna_cijena < 100) &&
          (!this.filter_cijena4 || x.jedinicna_cijena >= 100)) &&
        (!this.filter_podkat || x.pod_kategorija_id == this.podkatID) &&
        (!this.filter_brend || x.brend_id == this.brendID)
    );

    if (pr.length == 0)
      console.log("Nema proizvoda");
    else
      return pr;
  }

  private napraviKorpu() {
    this.httpKlijent.post(MojConfig.adresa_servera + '/Korpa/Dodaj', null).subscribe(x => {
      this.korpaID = x;
    })
  }

  dodajUKorpu(p: any) {
    if (this.kolicina != 0) {
      // @ts-ignore
      this.httpKlijent.post(MojConfig.adresa_servera + `/KorpaProizvod/DodajProizvod?korpaId=${this.korpaID}&proizvdId=${p.id}&kolicina=${this.kolicina}`)
        .subscribe((x: any) => {
          this.prikaziSadrzaj();
          this.kolicina=0; // kako se ne bi mogli dodavati proizvodi sa kolicinom = 0
        })
    } else
      alert("Niste odabrali količinu");
  }


  prikaziSadrzaj() {
    this.httpKlijent.get(MojConfig.adresa_servera + `/KorpaProizvod/GetByKorpa?korpa_id=${this.korpaID}`).subscribe(x => {
      this.korpaObject = x;
      if(this.korpaObject.length==0)
        alert("Korpa je prazna");
    })

  }

  ukloniProizvod(proizvodID: any) {
    this.httpKlijent.post(MojConfig.adresa_servera + `/KorpaProizvod/UkloniProizvod?korpaId=${this.korpaID}&proizvdId=${proizvodID}`, null)
      .subscribe(x => {
        this.prikaziSadrzaj();
      })
  }

  izmijeniKolicinu(kolicina: any, korpaID:any, proizvodID:any) {
   // this.kolicina=kolicina;
    // @ts-ignore
    this.httpKlijent.post(MojConfig.adresa_servera + `/KorpaProizvod/DodajProizvod?korpaId=${korpaID}&proizvdId=${proizvodID}&kolicina=${kolicina}`)
      .subscribe((x: any) => {
        this.cijenaProizvoda=x; //nova cijena
        for (let k of this.korpaObject) {
          if(k.korpaID==korpaID && k.proizvodID==proizvodID)
            k.cijena=this.cijenaProizvoda;
        }
      })
  }

  onKey(event: Event) {
    // @ts-ignore
    if(event.target.value!=0)
      { // @ts-ignore
        this.kolicina = event.target.value;
      }
  }
}

