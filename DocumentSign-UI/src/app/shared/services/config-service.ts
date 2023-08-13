import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ConfigService {
  baseAPIUrl;
  ReportsAPIUrl;
  config;
  constructor(private httpClient: HttpClient) {
   this.loadConfig();
  }

  public loadConfig() {
    var result= this.httpClient.get('/assets/ShrsConfig.json')
      .toPromise()
      .then((config: any) => {
        this.config = config;
        this.baseAPIUrl = this.config.baseAPIUrl;
        this.ReportsAPIUrl = this.config.ReportsAPIUrl;
        console.log(  this.ReportsAPIUrl);
      })
      .catch((err: any) => {
        // this.baseAPIUrl="http://localhost:4435/";
      })
      
      return result;
  }
}
