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
    this.ucitajOpremu()
  }

  oprema:any;
  opremaObject:any;

  ucitajOpremu(){
    this.httpKlijent.get(MojConfig.adresa_servera + "/Oprema/GetAll").subscribe(x=>{
      this.oprema=x;
    });

  }


  loginInfo():LoginInformacije {
    return AutentifikacijaHelper.getLoginInfo();
  }

  dodajOpremu() {
if(this.opremaObject.broj>0 && this.opremaObject.naziv!="") {
  this.httpKlijent.post(MojConfig.adresa_servera + "/Oprema/Snimi", this.opremaObject).subscribe(x => {
    this.opremaObject = null;
    this.ucitajOpremu();
  })
}
else if(this.opremaObject.broj<1)
  alert("Broj mora biti veći od 0");
else if(this.opremaObject.naziv=="")
  alert("Niste unijeli naziv");
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
