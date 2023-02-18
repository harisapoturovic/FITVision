import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";
import {LoginInformacije} from "../_helpers/login-informacije";
import {AutentifikacijaHelper} from "../_helpers/autentifikacija-helper";
import {MojConfig} from "../moj-config";
import {KorpaService} from "../shop/KorpaService";

declare function porukaSuccess(a: string):any;
declare function porukaError(a: string):any;

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  txtKorisnickiIme:any;
  txtLozinka:any;
  korisnik_id:any;
  constructor(private httpKlijent:HttpClient,private  router:Router, private korpaService:KorpaService) { }

  ngOnInit(): void {
  }

  btnLogiranje() {
    let saljemo={
      korisnickoIme:this.txtKorisnickiIme,
      lozinka:this.txtLozinka
    }
    this.httpKlijent.post<LoginInformacije>(MojConfig.adresa_servera+ "/Autentifikacija/Login", saljemo).subscribe(
      (x:LoginInformacije)=>{
        if(x.isLogiran){
          porukaSuccess("login upjesan");
          AutentifikacijaHelper.setLoginInfo(x);
          if(x.isPremisijaKorisnik)
          {
            this.korisnik_id = x.autentifikacijaToken.korisnickiNalogId;
            this.napraviKorpu();
          }
          this.router.navigateByUrl("/pocetna");
        }
        else{
          AutentifikacijaHelper.setLoginInfo(null);
          porukaError("nesuspjesan login");
        }
      }
    )
  }

  napraviKorpu() {
    this.httpKlijent.post(MojConfig.adresa_servera + `/Korpa/Snimi?korisnikId=${this.korisnik_id}`, null).subscribe(x => {
      this.korpaService.setKorpaID(x);
    })
  }

}
