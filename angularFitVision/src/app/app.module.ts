import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import  {RouterModule, RouterOutlet} from "@angular/router";
import {FormsModule} from "@angular/forms";
import {HttpClientModule} from "@angular/common/http";

import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import {OpremaComponent} from "./oprema/oprema.component";
import { RegistracijaAdminComponent } from './registracija-admin/registracija-admin.component';
import { RegistracijaKorisnikComponent } from './registracija-korisnik/registracija-korisnik.component';
import { PostavkeProfilaComponent } from './postavke-profila/postavke-profila.component';
import { PostavkaAdminComponent } from './postavke-profila/postavka-admin/postavka-admin.component';
import { PostavkaKorisnikComponent } from './postavke-profila/postavka-korisnik/postavka-korisnik.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    OpremaComponent,
    RegistracijaAdminComponent,
    RegistracijaKorisnikComponent,
    PostavkeProfilaComponent,
    PostavkaAdminComponent,
    PostavkaKorisnikComponent
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
      {path: 'postavke-profila', component: PostavkeProfilaComponent}
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
