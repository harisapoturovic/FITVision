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
  odabraniKorisnik:any;
  gradovi:any;
  constructor(private httpKlijent:HttpClient, private router: Router) { }

  ngOnInit(): void {
    this.ucitajKorisnike();
    //this.getGradove();
  }

  ucitajKorisnike(){
    this.httpKlijent.get(MojConfig.adresa_servera+"/Korisnik/GetAll").subscribe(x=>{
      this.korisnici=x;
    })
  }

  getPodaci() {
    return this.korisnici.filter(
      (x:any)=>
        (!this.filter_ime_prezime ||
          (x.ime + ' ' + x.prezime).toLowerCase().startsWith(this.ime_prezime)
          || (x.prezime + ' ' + x.ime).toLowerCase().startsWith(this.ime_prezime)));
  }

  //noviKorisnik() {
  //  this.odabraniKorisnik={
  //    id:0,
  //    ime:'...',
  //    prezime:'...'
  //  };
  //}
//
  //snimi() {
  //  this.httpKlijent.post(MojConfig.adresa_servera + '/Korisnik/Snimi', this.odabraniKorisnik, MojConfig.http_opcije()).subscribe((x:any)=>{
  //    this.getPodaci();
  //    this.odabraniKorisnik=null;
  //  });
  //}
//
  //getGradove(){
  //  this.httpKlijent.get(MojConfig.adresa_servera+"/Grad/GetAll").subscribe(x=>{
  //    this.gradovi=x;
  //  })
  //}

}
