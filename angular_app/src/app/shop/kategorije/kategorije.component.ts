import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {MojConfig} from "../../moj-config";
import {LoginInformacije} from "../../_helpers/login-informacije";
import {AutentifikacijaHelper} from "../../_helpers/autentifikacija-helper";

@Component({
  selector: 'app-kategorije',
  templateUrl: './kategorije.component.html',
  styleUrls: ['./kategorije.component.css']
})
export class KategorijeComponent implements OnInit {
  kategorije:any;
  odabranaKategorija:any;
  brendovi: any;
  odabranaDodatno:any;

  constructor(private httpKlijent:HttpClient) { }

  ngOnInit(): void {
    this.ucitajKategorije();
  }

  loginInfo():LoginInformacije {
    return AutentifikacijaHelper.getLoginInfo();
  }

  ucitajKategorije() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/Kategorija/GetAll").subscribe(x=>{
      this.kategorije=x;
    })
  }

  dodajKategoriju() {
    this.httpKlijent.post(MojConfig.adresa_servera + "/Kategorija/Snimi", this.odabranaKategorija).subscribe(x=>{
      this.odabranaKategorija=null;
      this.ucitajKategorije();

    })
  }

  novaKategorija(){
    this.odabranaKategorija={
      id:0,
      naziv:"",
      opis:""
    };
  }

 // prikaziBrendove(k: any) {
 //   this.httpKlijent.get(MojConfig.adresa_servera + "/Brend/GetAll").subscribe(x=>{
 //     this.brendovi=x;
 //     this.odabranaBrendovi=k;
 //     })
 // }

  //detaljnoBrendovi(b: any) {
  //  this.odabraniBrend=b;
  //}

  prikaziDodatno(k: any) {
    this.odabranaDodatno=k;
  }

  obrisiKategoriju(k: any) {
    this.httpKlijent.post(`${MojConfig.adresa_servera}/Kategorija/Obrisi/${k.id}`, null).subscribe(x=>{
      this.odabranaDodatno=null;
      this.ucitajKategorije();
    })
  }

  urediKategoriju(k: any) {
    this.httpKlijent.post(MojConfig.adresa_servera + "/Kategorija/Snimi", k).subscribe(x=> {
      this.odabranaDodatno=null;
      this.ucitajKategorije();
    })
  }

}
