
export class LoginInformacije{
  autentifikacijaToken:        AutentifikacijaToken=null;
  isLogiran:                   boolean=false;
  isPremisijaAdmin:boolean=false;
  isPremisijaKorisnik:boolean=false;
  isPremisijaTrener:boolean=false;

}

export interface AutentifikacijaToken {

  id: number;
  vrijednost: string;
  korisnickiNalogId: number;
  korisnickiNalog: KorisnickiNalog;
  vrijemeEvidentiranja: Date;
  ipAdresa: string;
}
export interface KorisnickiNalog {
  ID: number;
  korisnickomIme: string;
  isAdmin: boolean;
  isKorisnik: boolean;
  isTrener:boolean;
}