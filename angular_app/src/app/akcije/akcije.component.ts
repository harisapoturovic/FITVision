import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {MojConfig} from "../moj-config";
import {LoginInformacije} from "../_helpers/login-informacije";
import {AutentifikacijaHelper} from "../_helpers/autentifikacija-helper";

declare function porukaSuccess(a: string):any;
declare function porukaError(a: string):any;

@Component({
  selector: 'app-akcije',
  templateUrl: './akcije.component.html',
  styleUrls: ['./akcije.component.css']
})
export class AkcijeComponent implements OnInit {

  constructor(private httpKlijent: HttpClient) { }

  ngOnInit(): void {
    this.ucitajBrendove();
    this.ucitajPodKategorije();
    this.ucitajProizvode();
    this.ucitajAkcije();
  }

  loginInfo(): LoginInformacije {
    return AutentifikacijaHelper.getLoginInfo();
  }

  ucitajAkcije(){
    this.httpKlijent.get(MojConfig.adresa_servera +"/Akcija/GetAll").subscribe(x=>{
      this.akcije=x;
    })

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
  brendovi: any;
  podKategorije: any
  proizvodi: any;
  akcije:any;
  akcijaObject:any;
  akcijaProizvod:any;

  dodajAkciju() {
    this.akcijaObject={
      id:0,
      naziv:"",
      iznos: 0,
      datum_pocetka:"",
      datum_zavrsetka:""
    }
  }

  snimiAkciju() {
    if(this.akcijaObject.naziv!="" && this.akcijaObject.iznos>0 && this.akcijaObject.datum_pocetka!="" &&
      this.akcijaObject.datum_zavrsetka!=""){
      this.httpKlijent.post(MojConfig.adresa_servera + "/Akcija/Snimi", this.akcijaObject).subscribe(x=>{
        this.ucitajAkcije();
        this.akcijaObject=null;
      })
      porukaSuccess("uspjesno snimljena akcija");
    }
    else
      alert("Nista unijeli sve podatke");
  }


  urediAkciju(a: any) {
    this.akcijaObject=a;
  }


  obrisiAkciju(a: any) {
    let con= confirm("Da li zelite obrisati akciju?");
    if(con.valueOf()==true) {
      this.httpKlijent.post(MojConfig.adresa_servera + `/Akcija/Obrisi/${a.id}`, null).subscribe(x => {
        this.ucitajAkcije();
      })
      porukaSuccess("Uspjesno obrisana akcija");
    }
  }

  proizvodAkcijaOtvori(a: any) {
    this.akcijaProizvod=a;
  }

  dodajProizvodUakciju(z: any) {
    this.httpKlijent.post(MojConfig.adresa_servera + `/Akcija/DodajProizvod?akcijaId=${this.akcijaProizvod.id}&proizvdId=${z.id}`,
      null).subscribe(x=>{
      this.ucitajAkcije();
    })
    porukaSuccess("Uspjesno dodan proizvod u akciju");
  }

  ukloniProizvodAkcija(a: any, z: any) {
    let con= confirm("Da li zelite obrisati proizvod sa akcije?");
    if(con.valueOf()==true) {
      this.httpKlijent.post(MojConfig.adresa_servera +
        `/Akcija/UkloniProizvod?akcijaId=${a.id}&proizvdId=${z.id}`, null).subscribe(x => {
        this.ucitajAkcije();
      })
      porukaSuccess("Uspjesno uklonjen proizvod");
    }
  }

}
