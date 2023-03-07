import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {ActivatedRoute} from "@angular/router";
import {MojConfig} from "../moj-config";
import {LoginInformacije} from "../_helpers/login-informacije";
import {AutentifikacijaHelper} from "../_helpers/autentifikacija-helper";

declare function porukaSuccess(a: string):any;
declare function porukaError(a: string):any;

@Component({
  selector: 'app-poruka-odgovor',
  templateUrl: './poruka-odgovor.component.html',
  styleUrls: ['./poruka-odgovor.component.css']
})
export class PorukaOdgovorComponent implements OnInit {

  constructor(private httpKlijent:HttpClient, private route:ActivatedRoute) { }

  porukaID:any;
  porukaOdgovori:any;
  odgovorObject:any;





  loginInfo():LoginInformacije {
    return AutentifikacijaHelper.getLoginInfo();
  }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.porukaID = +params['porukaid'];
    });
    this.ucitajPorukuOdgovor();
    console.log(this.loginInfo().autentifikacijaToken.korisnickiNalog);
  }

  ucitajPorukuOdgovor(){
    this.httpKlijent.get(MojConfig.adresa_servera + "/Poruka/GetPorukaOdgovori?id=" + this.porukaID).subscribe(x=>{
      this.porukaOdgovori=x;

    });
  }

  odgovori() {
    this.odgovorObject={
      sadrzaj:"",
      admin_name: this.loginInfo().autentifikacijaToken.korisnickiNalog.korisnickoIme,
      poruka_id:this.porukaID
    }

  }

  dodajOdgovor() {

    if(this.odgovorObject.sadrzaj!=""){
      this.httpKlijent.post(MojConfig.adresa_servera + "/Odgovor/Dodaj", this.odgovorObject).subscribe(x=>{
        this.ucitajPorukuOdgovor();
        this.odgovorObject=null;
      });

      porukaSuccess("Uspjesno oddan odgovor");
    }
    else {
      alert("Ne mozete poslati praznu poruku");
    }




  }

  obrisiOdgovor(o:any) {
    let con= confirm("Da li zelite obrisati odgovor?");
    if(con.valueOf()==true) {
      this.httpKlijent.post(MojConfig.adresa_servera + "/Odgovor/Obrisi?id=" + o.id, null).subscribe(x => {
        this.ucitajPorukuOdgovor();
      })
    }
  }



}
