import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";
import {LoginInformacije} from "../_helpers/login-informacije";
import {AutentifikacijaHelper} from "../_helpers/autentifikacija-helper";
import {MojConfig} from "../moj-config";

//declare function porukaSuccess(a: string):any;
//declare function porukaError(a: string):any;

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  txtKorisnickiIme:any;
  txtLozinka:any;

  constructor(private httpKlijent:HttpClient,private  router:Router) { }

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
          //porukaSuccess("login upjesan");
          AutentifikacijaHelper.setLoginInfo(x);
          this.router.navigateByUrl("/oprema");
        }
        else{
          AutentifikacijaHelper.setLoginInfo(null);
          //porukaError("nesuspjesan login");
        }
      }
    )
  }

}
