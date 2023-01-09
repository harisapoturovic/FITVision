import { Component, OnInit } from '@angular/core';
import {MojConfig} from "../../moj-config";
import {HttpClient} from "@angular/common/http";
import {KorpaService} from "../KorpaService";

@Component({
  selector: 'app-narudzba',
  templateUrl: './narudzba.component.html',
  styleUrls: ['./narudzba.component.css']
})
export class NarudzbaComponent implements OnInit {
  gradovi:any;
  drzave:any;
  drzava_ID: any;
  grad_ID: any;
  korpaObject: any;
  korpaID:any;
  cijena:any;

  constructor(private httpKlijent: HttpClient, private korpaService:KorpaService) { }

  ngOnInit(): void {
    this.ucitajDrzave();
    this.ucitajGradove();
    this.korpaID=this.korpaService.getKorpaID();
    this.prikaziSadrzaj();
  }
  ucitajGradove() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/Grad/GetAll").subscribe(x => {
      this.gradovi = x;
    })
  }

  ucitajDrzave() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/Drzava/GetAll").subscribe(x => {
      this.drzave = x;
    })
  }
  prikaziSadrzaj() {
    this.httpKlijent.get(MojConfig.adresa_servera + `/KorpaProizvod/GetByKorpa?korpa_id=${this.korpaID}`).subscribe(x => {
      this.korpaObject = x;
      if(this.korpaObject.length==0)
        alert("Korpa je prazna");
    })

  }
}
