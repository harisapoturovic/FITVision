import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";
import {MojConfig} from "../moj-config";
import {LoginInformacije} from "../_helpers/login-informacije";
import {AutentifikacijaHelper} from "../_helpers/autentifikacija-helper";

@Component({
  selector: 'app-poruke',
  templateUrl: './poruke.component.html',
  styleUrls: ['./poruke.component.css']
})
export class PorukeComponent implements OnInit {

  constructor(private httpKlijent:HttpClient, private router:Router) { }

  ngOnInit(): void {
    //this.ucitajPoruke();
    //this.ucitajZasebno();
  }

  loginInfo():LoginInformacije {
    return AutentifikacijaHelper.getLoginInfo();
  }
  porukaObject:any;
  filter_naslov:boolean=false;
  naslov:string="";
  filter_korisnik:boolean=false;
  korisnik_naziv:string="";
  poruke:any;
  poslanoKorisnik:any;
  poslanoAdmin:any;

  ucitajNovosti() {
      this.httpKlijent.get(MojConfig.adresa_servera + `/Poruka/GetByAdmin?id=${this.loginInfo().autentifikacijaToken.korisnickiNalogId}`).subscribe(x => {
        this.poslanoAdmin = x;
        this.poslanoKorisnik = null;
      });
  }

  ucitajZasebnoKorisnik() {
      this.httpKlijent.get(MojConfig.adresa_servera + `/Poruka/GetByKorsinikId?id=${this.loginInfo().autentifikacijaToken.korisnickiNalogId}`).subscribe(x => {
        this.poslanoKorisnik = x;
        this.poslanoAdmin = null;
      });
  }

  ucitajPorukeZaAdmina()
  {
    this.httpKlijent.get(MojConfig.adresa_servera + `/Poruka/GetByKorisnik?id=${this.loginInfo().autentifikacijaToken.korisnickiNalogId}`).subscribe(x => {
      this.poslanoKorisnik = x;
      this.poslanoAdmin = null;
    });
  }

  ucitajZasebnoAdmin() {
    if (this.loginInfo().isPremisijaAdmin) {
      this.httpKlijent.get(MojConfig.adresa_servera + `/Poruka/GetByAdminId?id=${this.loginInfo().autentifikacijaToken.korisnickiNalogId}`).subscribe(x => {
        this.poslanoAdmin = x;
        this.poruke = null;
      });
    }
  }

  otvoriOdgovore(p: any) {
    this.router.navigate(['/poruka-odgovor',p.id]);
  }

  dodajPitanje() {
    if(this.porukaObject.naslov!="" && this.porukaObject.sadrzaj!=""){
      this.httpKlijent.post(MojConfig.adresa_servera + "/Poruka/Dodaj", this.porukaObject).subscribe(x=>{
        //this.ucitajPoruke();
        this.porukaObject=null;
      })
    }
    else{
      alert("niste unijeli sve podatke");
    }

  }

  otvoriPoruku() {
    this.porukaObject={
      naslov:"Poruka",
      sadrzaj:"",
      korsinciki_nalog_id:this.loginInfo().autentifikacijaToken.korisnickiNalogId
    }
  }

  obrsisiPoruku(p: any) {
    let con= confirm("Da li zelite obrisati poruku?");
    if(con.valueOf()==true) {
      this.httpKlijent.post(MojConfig.adresa_servera + `/Poruka/Obrisi?id=${p.id}`, null).subscribe(x => {
       // this.ucitajPoruke();
      })
    }
  }

  filtrirano() {
    if(this.poruke==null)
      return [];
    return this.poruke.filter(
      (x:any)=>
        (!this.filter_naslov ||
          (x.naslov).toLowerCase().startsWith(this.naslov.toLowerCase())
        ) && (!this.filter_korisnik ||
          (x.korisnik).toLowerCase().startsWith(this.korisnik_naziv.toLowerCase())
        )) ;
  }
}
