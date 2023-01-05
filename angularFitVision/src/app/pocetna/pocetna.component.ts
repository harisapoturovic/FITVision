import { Component, OnInit } from '@angular/core';
import { HubConnection, HubConnectionBuilder, LogLevel } from '@microsoft/signalr';
import {HttpClient} from "@angular/common/http";
import {MojConfig} from "../moj-config";


@Component({
  selector: 'app-pocetna',
  templateUrl: './pocetna.component.html',
  styleUrls: ['./pocetna.component.css']
})
export class PocetnaComponent implements OnInit {

  private hubConnectionBuilder!: HubConnection;
  offers: any[] = [];
  constructor(private httpKlijent:HttpClient) {}
  ngOnInit(): void {
    this.hubConnectionBuilder = new HubConnectionBuilder().withUrl('https://localhost:7300/info').configureLogging(LogLevel.Information).build();
    this.hubConnectionBuilder.start().then(() => console.log('Connection started.......!')).catch(err => console.log('Error while connect with server'));
    this.hubConnectionBuilder.on('SendToAdmin', (result: any) => {
      this.offers.push(result);
    });
  }

  pogledaj() {
    this.httpKlijent.post(MojConfig.adresa_servera + "/api/AdminInfoSignalR", null).subscribe();
  }
}
