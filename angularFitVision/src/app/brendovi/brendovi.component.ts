import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {LoginInformacije} from "../_helpers/login-informacije";
import {AutentifikacijaHelper} from "../_helpers/autentifikacija-helper";
import {MojConfig} from "../moj-config";

@Component({
  selector: 'app-brendovi',
  templateUrl: './brendovi.component.html',
  styleUrls: ['./brendovi.component.css']
})
export class BrendoviComponent implements OnInit {
  brendovi: any;
  odabraniBrend:any;
  urediBrend:any;

  constructor(private httpKlijent: HttpClient) {
  }

  ngOnInit(): void {
    this.ucitajBrendove();
  }

  loginInfo(): LoginInformacije {
    return AutentifikacijaHelper.getLoginInfo();
  }

  ucitajBrendove() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/Brend/GetAll").subscribe(x => {
      this.brendovi = x;
    })
  }

  noviBrend() {
    this.odabraniBrend={
      id:0,
      naziv:"",
      opis:""
    };
  }

  dodajBrend() {
    this.httpKlijent.post(MojConfig.adresa_servera+"/Brend/Snimi", this.odabraniBrend).subscribe(x=>{
      this.odabraniBrend=null;
      this.ucitajBrendove();
    })
  }

  UrediBrend(b: any) {
    this.httpKlijent.post(MojConfig.adresa_servera+"/Brend/Snimi", b).subscribe(x=>{
      this.urediBrend=null;
      this.ucitajBrendove();
    })
  }

  obrisi(b: any) {
    this.httpKlijent.post(`${MojConfig.adresa_servera}/Brend/Obrisi/${b.id}`, null).subscribe(x=>{
      this.ucitajBrendove();
    })
  }
}
