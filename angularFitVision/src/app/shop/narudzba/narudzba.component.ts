import { Component, OnInit } from '@angular/core';
import {MojConfig} from "../../moj-config";
import {HttpClient} from "@angular/common/http";
import {KorpaService} from "../KorpaService";
import {LoginInformacije} from "../../_helpers/login-informacije";
import {AutentifikacijaHelper} from "../../_helpers/autentifikacija-helper";
import {AbstractControl, FormControl, FormGroup, Validators} from "@angular/forms";

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
  validationForm: FormGroup;

  constructor(private httpKlijent: HttpClient, private korpaService:KorpaService) {
    this.validationForm = new FormGroup({
      ime: new FormControl(null, { validators: Validators.required, updateOn: 'submit' }),
      prezime: new FormControl(null, { validators: Validators.required, updateOn: 'submit' }),
      drzava: new FormControl(null, { validators: Validators.required, updateOn: 'submit' }),
      grad: new FormControl(null, { validators: Validators.required, updateOn: 'submit' }),
      adresa: new FormControl(null, { validators: Validators.required, updateOn: 'submit' }),
      postanskiBroj: new FormControl(null, { validators: Validators.required, updateOn: 'submit' }),
      email: new FormControl(null, { validators: Validators.required, updateOn: 'submit' }),
      telefon: new FormControl(null, { validators: Validators.required, updateOn: 'submit' }),
    });
  }

  get ime(): AbstractControl {
    return this.validationForm.get('ime')!;
  }

  get prezime(): AbstractControl {
    return this.validationForm.get('prezime')!;
  }

  get drzava(): AbstractControl {
    return this.validationForm.get('drzava')!;
  }

  get grad(): AbstractControl {
    return this.validationForm.get('grad')!;
  }

  get adresa(): AbstractControl {
    return this.validationForm.get('adresa')!;
  }

  get postanskiBroj(): AbstractControl {
    return this.validationForm.get('postanskiBroj')!;
  }

  get email(): AbstractControl {
    return this.validationForm.get('email')!;
  }

  get telefon(): AbstractControl {
    return this.validationForm.get('telefon')!;
  }

  onSubmit(): void {
    this.validationForm.markAllAsTouched();
  }


  ngOnInit(): void {
    this.ucitajDrzave();
    this.ucitajGradove();
    this.korpaID=this.korpaService.getKorpaID();
    this.prikaziSadrzaj();
  }

  loginInfo():LoginInformacije {
    return AutentifikacijaHelper.getLoginInfo();
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
