import { Component, OnInit } from '@angular/core';
import {LoginInformacije} from "../_helpers/login-informacije";
import {AutentifikacijaHelper} from "../_helpers/autentifikacija-helper";
import {HttpClient} from "@angular/common/http";
import {MojConfig} from "../moj-config";
import {Router} from "@angular/router";

@Component({
  selector: 'app-korisnici',
  templateUrl: './korisnici.component.html',
  styleUrls: ['./korisnici.component.css']
})
export class KorisniciComponent implements OnInit {
  korisnici:any;
  ime_prezime:string = '';
  filter_ime_prezime: boolean;
  pomocna:any=false;

  constructor(private httpKlijent:HttpClient, private router: Router) { }

  ngOnInit(): void {
    this.ucitajKorisnike();
    this.ucitajGradove();
  }

  ucitajKorisnike(){
    this.httpKlijent.get(MojConfig.adresa_servera+"/Korisnik/GetAll", MojConfig.http_opcije()).subscribe(x=>{
      this.korisnici=x;
    })
  }

  getPodaci() {
    if(this.korisnici==null)
      return [];
    return this.korisnici.filter(
      (x:any)=>
        (!this.filter_ime_prezime ||
          (x.ime + ' ' + x.prezime).toLowerCase().startsWith(this.ime_prezime)
          || (x.prezime + ' ' + x.ime).toLowerCase().startsWith(this.ime_prezime)));
  }

  obrisi(k: any) {
    this.httpKlijent.post(`${MojConfig.adresa_servera}/Korisnik/Obrisi/${k.id}`, MojConfig.http_opcije()).subscribe(x=>{
      this.getPodaci();
    });
  }

  gradovi: any;

  ime: string = "";
  prezime: string = "";
  datum_rodjenja: Date;
  datum_polaska: Date;
  telefon: string = "";
  adresa: string = "";
  email: string = "";
  spol: string = "";
  jmbg: string = "";
  grad_ID: number = 0;
  visina: string = "";
  tezina: string = "";
  korisnickoIme: string = "";
  lozinka: any = "";


  ucitajGradove() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/Grad/GetAll").subscribe(x => {
      this.gradovi = x;
    })
  }

  registrcaija() {

    if (this.ime != "" && this.prezime != "" && this.datum_rodjenja != null && this.datum_polaska != null
      && this.telefon != "" && this.email != "" && this.adresa != "" && this.korisnickoIme != "" && this.lozinka != "" &&
      this.visina != "" && this.tezina != "" && this.jmbg != "" && this.spol != "" && this.spol != "...." && this.grad_ID != 0) {
      var korisnik = {
        id: 0,
        ime: this.ime,
        prezime: this.prezime,
        datum_rodjenja: this.datum_rodjenja,
        datum_polaska: this.datum_polaska,
        telefon: this.telefon,
        adresa: this.adresa,
        email: this.email,
        spol: this.spol,
        jmbg: this.jmbg,
        grad_ID: this.grad_ID,
        visina: this.visina,
        tezina: this.tezina,
        korisnickoIme: this.korisnickoIme,
        lozinka: this.lozinka
      }
      this.httpKlijent.post(MojConfig.adresa_servera + "/Korisnik/Snimi", korisnik, MojConfig.http_opcije()).subscribe(x => {
        alert("Uspješno dodan korisnik");
        this.pomocna=false;
      })
    }


  }
}
