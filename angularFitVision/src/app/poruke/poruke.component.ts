import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";
import {MojConfig} from "../moj-config";
import {LoginInformacije} from "../_helpers/login-informacije";
import {AutentifikacijaHelper} from "../_helpers/autentifikacija-helper";

@Component({
  selector: 'app-poruke',
  templateUrl: './poruke.component.html',
  styleUrls: ['./poruke.component.css']
})
export class PorukeComponent implements OnInit {

  constructor(private httpKlijent:HttpClient, private router:Router) { }

  ngOnInit(): void {
    this.ucitajPoruke();
  }

  loginInfo():LoginInformacije {
    return AutentifikacijaHelper.getLoginInfo();
  }
  porukaObject:any;



  ucitajPoruke(){
    if(this.loginInfo().isPremisijaAdmin){
      this.httpKlijent.get(MojConfig.adresa_servera+"/Poruka/GetAll").subscribe(x=>{
        this.poruke=x;
      });
    }
    else if(this.loginInfo().isPremisijaKorisnik){
      this.httpKlijent.get(MojConfig.adresa_servera+`/Poruka/GetByKorsinikId?id=${this.loginInfo().autentifikacijaToken.korisnickiNalogId}`).subscribe(x=>{
        this.poruke=x;
      });
    }

  }

  poruke:any;

  otvoriOdgovore(p: any) {
    //this.router.navigate(['/poruka-odgovor',p.id]);
  }

  dodajPitanje() {
    this.httpKlijent.post(MojConfig.adresa_servera + "/Poruka/Dodaj", this.porukaObject).subscribe(x=>{
      this.ucitajPoruke();
      this.porukaObject=null;
    })
  }

  otvoriPoruku() {
    this.porukaObject={
      naslov:"",
      sadrzaj:"",
      korsinciki_nalog_id:this.loginInfo().autentifikacijaToken.korisnickiNalogId
    }
  }

  obrsisiPoruku(p: any) {
   
    this.httpKlijent.post(MojConfig.adresa_servera + `/Poruka/Obrisi?id=${p.id}`, null).subscribe(x=>{
      this.ucitajPoruke();
    })
  }
}
