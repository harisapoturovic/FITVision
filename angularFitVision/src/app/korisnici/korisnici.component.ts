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

  constructor(private httpKlijent:HttpClient, private router: Router) { }

  ngOnInit(): void {
    this.ucitajKorisnike();
  }

  ucitajKorisnike(){
    this.httpKlijent.get(MojConfig.adresa_servera+"/Korisnik/GetAll").subscribe(x=>{
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
    // @ts-ignore
    this.httpKlijent.post(`${MojConfig.adresa_servera}/Korisnik/Obrisi/${k.id}`, MojConfig.http_opcije()).subscribe(x=>{
      this.getPodaci();
    });
  }
}
