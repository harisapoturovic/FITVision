import { Component, OnInit } from '@angular/core';
import {ProizvodService} from "../ProizvodService";
import {MojConfig} from "../../../moj-config";
import {HttpClient} from "@angular/common/http";

@Component({
  selector: 'app-proizvod-detalji',
  templateUrl: './proizvod-detalji.component.html',
  styleUrls: ['./proizvod-detalji.component.css']
})
export class ProizvodDetaljiComponent implements OnInit {
  proizvodID:any;
  proizvod:any;

  constructor(private httpKlijent: HttpClient, private proizvodService:ProizvodService ) { }

  ngOnInit(): void {
    this.proizvodID=this.proizvodService.getProizvodID();
    this.ucitajProizvod();
  }

  ucitajProizvod()
  {
    this.httpKlijent.get(MojConfig.adresa_servera + `/Proizvod/GetByProizvodID?id=${this.proizvodID}`).subscribe((x:any) => {
      this.proizvod = x;
    })
  }
}
