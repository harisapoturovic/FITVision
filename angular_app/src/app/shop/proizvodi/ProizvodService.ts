import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ProizvodService {

  proizvodID:any;
  constructor() {

  }
  setProizvodID(id:any)
  {
    this.proizvodID=id;
  }

  getProizvodID(){
    return this.proizvodID;
  }

}

