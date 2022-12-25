import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {LoginInformacije} from "../../../_helpers/login-informacije";
import {AutentifikacijaHelper} from "../../../_helpers/autentifikacija-helper";
import {MojConfig} from "../../../moj-config";

@Component({
  selector: 'app-podkategorije',
  templateUrl: './podkategorije.component.html',
  styleUrls: ['./podkategorije.component.css']
})
export class PodkategorijeComponent implements OnInit {
  podkategorije:any;
  odabranaPodkat:any;
  urediPodkat:any;

  constructor(private httpKlijent: HttpClient) { }

  ngOnInit(): void {
    this.ucitajPodkategorije();
  }

  loginInfo(): LoginInformacije {
    return AutentifikacijaHelper.getLoginInfo();
  }

  ucitajPodkategorije() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/Podkategorija/GetAll").subscribe(x => {
      this.podkategorije = x;
    })
  }

  novaPodkat() {
    this.odabranaPodkat={
      id:0,
      naziv:"",
      opis:"",
      kategorija_id:0
    }
  }

  dodajPodkat() {
    this.httpKlijent.post(MojConfig.adresa_servera+"/Podkategorija/Snimi", this.odabranaPodkat).subscribe(x=>{
      this.odabranaPodkat=null;
      this.ucitajPodkategorije();
    })
  }

  Uredi(p: any) {
    this.httpKlijent.post(MojConfig.adresa_servera+"/Podkategorija/Snimi", p).subscribe(x=>{
      this.urediPodkat=null;
      this.ucitajPodkategorije();
    })
  }

  obrisi(p: any) {
    this.httpKlijent.post(`${MojConfig.adresa_servera}/Podkategorija/Obrisi/${p.id}`, null).subscribe(x=>{
      this.ucitajPodkategorije();
    })
  }
}
