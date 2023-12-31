import { Injectable } from '@angular/core';
import { ConfigService } from './config-service';

@Injectable({
  providedIn: 'root'
})

export class UtilitiesService {

  baseAPIUrl = "http://localhost:44380/"; //this.configService.baseAPIUrl;    

  constructor(private configService: ConfigService) { }

  getCurrentDateAndTime () {
    let now = new Date();
    let year = "" + now.getFullYear();
    let month = "" + (now.getMonth() + 1); if (month.length == 1) { month = "0" + month; }
    let day = "" + now.getDate(); if (day.length == 1) { day = "0" + day; }
    let hour = "" + now.getHours(); if (hour.length == 1) { hour = "0" + hour; }
    let minute = "" + now.getMinutes(); if (minute.length == 1) { minute = "0" + minute; }
    let second = "" + now.getSeconds(); if (second.length == 1) { second = "0" + second; }
    return year + "-" + month + "-" + day + " " + hour + ":" + minute + ":" + second;
  }
}


