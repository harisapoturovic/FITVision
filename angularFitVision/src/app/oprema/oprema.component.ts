import { Component, OnInit } from '@angular/core';
import {MojConfig} from "../moj-config";
import {AutentifikacijaHelper} from "../_helpers/autentifikacija-helper";
import {LoginInformacije} from "../_helpers/login-informacije";
import {HttpClient} from "@angular/common/http";

declare function porukaSuccess(a: string):any;
declare function porukaError(a: string):any;

@Component({
  selector: 'app-oprema',
  templateUrl: './oprema.component.html',
  styleUrls: ['./oprema.component.css']
})
export class OpremaComponent implements OnInit {

  constructor(private httpKlijent:HttpClient) { }

  ngOnInit(): void {
    this.ucitajOpremu()
    this.ucitajTipove();
  }


  tipoviOpreme:any;
  oprema:any;
  opremaObject:any;
  urediOprema:any;
  url="";
  detaljiOprema:any;
  filter_naziv:boolean=false;
  naziv:string="";

  ucitajTipove(){

    this.httpKlijent.get(MojConfig.adresa_servera +"/TipOpreme/GetAll").subscribe(x=>{
      this.tipoviOpreme=x;
    })
  }

  ucitajOpremu(){
    this.httpKlijent.get(MojConfig.adresa_servera + "/Oprema/GetAll").subscribe(x=>{
      this.oprema=x;
    });

  }


  loginInfo():LoginInformacije {
    return AutentifikacijaHelper.getLoginInfo();
  }

  dodajOpremu() {
    if (this.opremaObject.broj > 0 && this.opremaObject.naziv != "" && this.opremaObject.tip_opreme_id > 0 && this.opremaObject.opis!="") {
      this.httpKlijent.post(MojConfig.adresa_servera + "/Oprema/Snimi", this.opremaObject).subscribe(x => {
        this.opremaObject = null;
        this.ucitajOpremu();
      })
      porukaSuccess("Uspjesno dodana oprema");
    } else
      alert("Niste unijeli sve podatke");
  }



  dodajFunc() {
    this.opremaObject={
      id:0,
      naziv:"",
      broj:0,
      slika:"assets/empty.png",
      opis:"",
      tip_opreme_id:0
    }
  }


  urediFunc(o: any) {
    this.urediOprema=o;
  }

  ChosenFile($event: Event) {
    let file = (event.target as HTMLInputElement).files[0];
    var reader= new FileReader();
    reader.readAsDataURL(file);
    reader.onload=(e:any)=>{
      this.url=e.target.result;
      this.urediOprema.slika=this.url;
      console.log(this.url);
    }
  }

  urediOpremu() {
    if(this.urediOprema.broj>0 && this.urediOprema.naziv!="" && this.urediOprema.opis!="") {
      this.httpKlijent.post(MojConfig.adresa_servera + "/Oprema/Snimi", this.urediOprema).subscribe(x => {
        this.urediOprema= null;
        this.ucitajOpremu();
      })
      porukaSuccess("Uspjesno promjenuta oprema");
    }
    else
      alert("Niste unijeli sve podatke");

  }

  obrisiOpremu(o: any) {
    this.httpKlijent.post(MojConfig.adresa_servera + `/Oprema/Obrisi/${o.id}`, null).subscribe(x=>{
      this.ucitajOpremu();
    })
    porukaSuccess("Uspjesno obrisana oprema");
  }


  detaljno(o: any) {
    this.detaljiOprema=o;
  }

  filtrirano() {
    if(this.oprema==null)
      return [];
    return this.oprema.filter(
      (x:any)=>
        (!this.filter_naziv ||
          (x.naziv).toLowerCase().startsWith(this.naziv)
          ));
  }
}
