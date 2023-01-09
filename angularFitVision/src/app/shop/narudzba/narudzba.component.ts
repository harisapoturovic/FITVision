import { Component, OnInit } from '@angular/core';
import {MojConfig} from "../../moj-config";
import {HttpClient} from "@angular/common/http";

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
  constructor(private httpKlijent: HttpClient) { }

  ngOnInit(): void {
    this.ucitajDrzave();
    this.ucitajGradove();
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
}
