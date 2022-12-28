import { Component, OnInit } from '@angular/core';
import {LoginInformacije} from "../../_helpers/login-informacije";
import {AutentifikacijaHelper} from "../../_helpers/autentifikacija-helper";
import {HttpClient} from "@angular/common/http";
import {MojConfig} from "../../moj-config";

declare function porukaSuccess(a: string):any;
declare function porukaError(a: string):any;

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
  }

  ucitajProizvode() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/Proizvod/GetAll").subscribe(x => {
      this.proivodi = x;
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
  proivodi: any;
  detaljiProizvod: any;
  url: "";


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
}
