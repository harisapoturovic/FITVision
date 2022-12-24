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
}
