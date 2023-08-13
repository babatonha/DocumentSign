import { Injectable } from '@angular/core';
import { BaseService } from './base-service';


@Injectable({
    providedIn: 'root'
  })
  
export class CustomControllerService extends BaseService<any> {
    route = 'api/';
 }
  