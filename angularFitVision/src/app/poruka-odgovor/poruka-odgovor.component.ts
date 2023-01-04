import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {ActivatedRoute} from "@angular/router";
import {MojConfig} from "../moj-config";
import {LoginInformacije} from "../_helpers/login-informacije";
import {AutentifikacijaHelper} from "../_helpers/autentifikacija-helper";

@Component({
  selector: 'app-poruka-odgovor',
  templateUrl: './poruka-odgovor.component.html',
  styleUrls: ['./poruka-odgovor.component.css']
})
export class PorukaOdgovorComponent implements OnInit {

  constructor(private httpKlijent:HttpClient, private route:ActivatedRoute) { }

  porukaID:any;
  porukaOdgovori:any;


  loginInfo():LoginInformacije {
    return AutentifikacijaHelper.getLoginInfo();
  }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.porukaID = +params['porukaid'];
    });
    this.ucitajPorukuOdgovor();
  }

  ucitajPorukuOdgovor(){
    this.httpKlijent.get(MojConfig.adresa_servera + "/Poruka/GetPorukaOdgovori?id=" + this.porukaID).subscribe(x=>{
      this.porukaOdgovori=x;
      console.log(this.porukaOdgovori);
    });
  }


}
