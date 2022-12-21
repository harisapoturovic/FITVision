import { Component, OnInit } from '@angular/core';
import {MojConfig} from "../moj-config";
import {AutentifikacijaHelper} from "../_helpers/autentifikacija-helper";
import {LoginInformacije} from "../_helpers/login-informacije";
import {HttpClient} from "@angular/common/http";

@Component({
  selector: 'app-oprema',
  templateUrl: './oprema.component.html',
  styleUrls: ['./oprema.component.css']
})
export class OpremaComponent implements OnInit {

  constructor(private httpKlijent:HttpClient) { }

  ngOnInit(): void {
  }

  oprema:any;
  opremaObject:any;





  loginInfo():LoginInformacije {
    return AutentifikacijaHelper.getLoginInfo();
  }

  dodajOpremu() {

    this.httpKlijent.post(MojConfig.adresa_servera +"/Oprema/Snimi", this.opremaObject).subscribe(x=>{
      this.opremaObject=null;

    })

  }


  dodajFunc() {
    this.opremaObject={
      id:0,
      naziv:"",
      broj:0,
      slika:"assets/empty.png"
    }
  }






}
