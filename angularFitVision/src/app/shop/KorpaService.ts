import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class KorpaService {

  korpaID:any;

  constructor() {

  }
  setKorpaID(id:any)
  {
    this.korpaID=id;
  }

  getKorpaID(){
    return this.korpaID;
  }
}

