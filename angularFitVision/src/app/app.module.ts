import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import  {RouterModule, RouterOutlet} from "@angular/router";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {HttpClientModule} from "@angular/common/http";

import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import {OpremaComponent} from "./oprema/oprema.component";
import { RegistracijaAdminComponent } from './registracija-admin/registracija-admin.component';
import { RegistracijaKorisnikComponent } from './registracija-korisnik/registracija-korisnik.component';
import { PostavkeProfilaComponent } from './postavke-profila/postavke-profila.component';
import { PostavkaAdminComponent } from './postavke-profila/postavka-admin/postavka-admin.component';
import { PostavkaKorisnikComponent } from './postavke-profila/postavka-korisnik/postavka-korisnik.component';
import { KorisniciComponent } from './korisnici/korisnici.component';
import { ShopComponent } from './shop/shop.component';
import { KategorijeComponent } from './shop/kategorije/kategorije.component';
import { BrendoviComponent } from './brendovi/brendovi.component';
import { PodkategorijeComponent } from './shop/kategorije/podkategorije/podkategorije.component';
import { ProizvodiComponent } from './shop/proizvodi/proizvodi.component';
import { AkcijeComponent } from './akcije/akcije.component';
import { PorukeComponent } from './poruke/poruke.component';
import { PorukaOdgovorComponent } from './poruka-odgovor/poruka-odgovor.component';
import { ForumComponent } from './forum/forum.component';
import { ForumOdgovoriComponent } from './forum-odgovori/forum-odgovori.component';
import { PocetnaComponent } from './pocetna/pocetna.component';
import { AdminComponent } from './pocetna/admin/admin.component';
import { NarudzbaComponent } from './shop/narudzba/narudzba.component';
import {MdbCarouselModule} from "mdb-angular-ui-kit/carousel";
import { FaqComponent } from './faq/faq.component';
import {MdbAccordionModule} from "mdb-angular-ui-kit/accordion";
import {MdbRangeModule} from "mdb-angular-ui-kit/range";
import {MdbValidationModule} from "mdb-angular-ui-kit/validation";
import {MdbFormsModule} from "mdb-angular-ui-kit/forms";
import { UserNotActiveComponent } from './user-not-active/user-not-active.component';
import {AutorizacijaLoginProvjera} from "./_guards/AutorizacijaLoginProvjera";
import { TwoFOtkljucajComponent } from './two-f-otkljucaj/two-f-otkljucaj.component';
import { ProizvodDetaljiComponent } from './shop/proizvodi/proizvod-detalji/proizvod-detalji.component';

// @ts-ignore
@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    OpremaComponent,
    RegistracijaAdminComponent,
    RegistracijaKorisnikComponent,
    PostavkeProfilaComponent,
    PostavkaAdminComponent,
    PostavkaKorisnikComponent,
    KorisniciComponent,
    ShopComponent,
    KategorijeComponent,
    BrendoviComponent,
    PodkategorijeComponent,
    ProizvodiComponent,
    AkcijeComponent,
    PorukeComponent,
    PorukaOdgovorComponent,
    ForumComponent,
    ForumOdgovoriComponent,
    PocetnaComponent,
    AdminComponent,
    NarudzbaComponent,
    FaqComponent,
    UserNotActiveComponent,
    TwoFOtkljucajComponent,
    ProizvodDetaljiComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      {path: 'login', component: LoginComponent},
      {path: 'oprema', component: OpremaComponent},
      {path: 'registracija-admin', component: RegistracijaAdminComponent},
      {path: 'registracija-korisnik', component: RegistracijaKorisnikComponent},
      {path: 'postavke-profila', component: PostavkeProfilaComponent},
      {path: 'korisnici', component: KorisniciComponent, canActivate: [AutorizacijaLoginProvjera]},
      {path: 'shop', component: ShopComponent},
      {path: 'kategorije', component: KategorijeComponent},
      {path: 'brendovi', component: BrendoviComponent},
      {path: 'podkategorije', component: PodkategorijeComponent},
      {path: 'proizvodi', component: ProizvodiComponent},
      {path: 'akcije', component: AkcijeComponent},
      {path: 'poruke', component: PorukeComponent},
      {path: "poruka-odgovor/:porukaid", component: PorukaOdgovorComponent},
      {path: 'forum', component: ForumComponent},
      {path: "forum-odgovor/:forumid", component: ForumOdgovoriComponent},
      {path: "pocetna", component: PocetnaComponent, canActivate: [AutorizacijaLoginProvjera]},
      {path: "narudzba", component: NarudzbaComponent},
      {path: "faq", component: FaqComponent},
      {path: "user-not-active", component: UserNotActiveComponent},
      {path: "two-f-otkljucaj", component: TwoFOtkljucajComponent},
      {path: "proizvod-detalji", component: ProizvodDetaljiComponent}
    ]),
    MdbCarouselModule,
    MdbAccordionModule,
    MdbRangeModule,
    MdbValidationModule,
    MdbFormsModule,
    ReactiveFormsModule
  ],
  providers: [
    AutorizacijaLoginProvjera
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

