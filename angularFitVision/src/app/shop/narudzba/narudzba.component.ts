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
  cijena2:any;
  popust2:any;
  ukupno:any;
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
    else {
      let cijena=0;
      let popust=0;
      let ukupno=0;
      this.korpaObject.forEach((k:any)=>cijena+=k.cijena);
      this.korpaObject.forEach((k:any)=>popust+=(k.popust*k.cijena/100));
      this.korpaObject.forEach((k:any)=>ukupno+=k.cijenaPopust);
      this.zaPrikaz(cijena, popust, ukupno);
      // console.log("Cijena" + cijena, "popust" + popust, "Ukupno" + ukupno);
    }
    })
  }
  zaPrikaz(c:any, p:any, u:any)
  {
    this.cijena2=c;
    this.popust2=p;
    this.ukupno=u;
  }
}
