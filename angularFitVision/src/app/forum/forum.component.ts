import { Component, OnInit } from '@angular/core';
import {MojConfig} from "../moj-config";
import {AutentifikacijaHelper} from "../_helpers/autentifikacija-helper";
import {LoginInformacije} from "../_helpers/login-informacije";
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";

@Component({
  selector: 'app-forum',
  templateUrl: './forum.component.html',
  styleUrls: ['./forum.component.css']
})
export class ForumComponent implements OnInit {
   teme: any;
   temaObject:any;

  constructor(private httpKlijent:HttpClient, private router:Router) { }

  ngOnInit(): void {
  this.ucitajTeme();
  }

  loginInfo():LoginInformacije {
    return AutentifikacijaHelper.getLoginInfo();
  }

  ucitajTeme(){
    this.httpKlijent.get(MojConfig.adresa_servera + "/ForumTema/GetAll").subscribe(x=>{
      this.teme=x;
    })
  }

  otvoriTemu() {
    this.temaObject={
      tema:"",
      pitanje:"",
      korsinciki_nalog_id:this.loginInfo().autentifikacijaToken.korisnickiNalogId
    }
  }

  dodajTemu() {
    if(this.temaObject.tema!="" && this.temaObject.pitanje!=""){
      this.httpKlijent.post(MojConfig.adresa_servera + "/ForumTema/Dodaj", this.temaObject).subscribe(x=>{
        this.temaObject=null;
        this.ucitajTeme();
      })
    }
    else{
      alert("niste unijeli sve podatke");
    }

  }
}
