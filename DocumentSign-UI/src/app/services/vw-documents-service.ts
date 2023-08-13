import { Injectable } from "@angular/core";
import { BaseService } from "../shared/services/base-service";

@Injectable({
    providedIn: 'root'
  })
  
export class vwDocumentsService extends BaseService<any> {
    route = 'VwDocuments';
  }