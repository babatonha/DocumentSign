import { Injectable } from "@angular/core";
import { BaseService } from "../shared/services/base-service";

@Injectable({
    providedIn: 'root'
  })
  
export class vwUserProfileService extends BaseService<any> {
    route = 'VwUserProfiles';
  }