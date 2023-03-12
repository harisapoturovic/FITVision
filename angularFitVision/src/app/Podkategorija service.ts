import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class PodkategorijaService {

  podkatID:any;
  constructor() {

  }
  setPodkatID(id:any)
  {
    this.podkatID=id;
  }

  getPodkatID(){
    return this.podkatID;
  }

}

