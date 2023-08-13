import { Injectable } from '@angular/core';
import DataSource from 'devextreme/data/data_source';
import 'devextreme/data/odata/store';
import { HttpClient } from '@angular/common/http';
import { UtilitiesService } from './utilitites-service';

@Injectable({
  providedIn: 'root'
})

export abstract class BaseService<T> {

  abstract route: string;
  orderBy = '';
  private get orderByQuery(): string {
    return this.orderBy === '' ? '' : '?$orderby=' + this.orderBy;
  }

  constructor(protected httpClient: HttpClient,
    private utilities: UtilitiesService) { }

  get(): any {
    return this.httpClient.get<any>(this.utilities.baseAPIUrl + `${this.route}`);
  }

  getSingle(id: Number, query = ''): any {
    return this.httpClient.get<any>(this.utilities.baseAPIUrl + `${this.route}(${id})${query}`);
  }

  getSingleRecordByID(query: string): any {
    return this.httpClient.get<any>(this.utilities.baseAPIUrl  + query);
  }

  utilitiesGet(query: string): any {
    return this.httpClient.get<any>(this.utilities.baseAPIUrl + `${this.route}` + query);
  }

  utilitiesPost(query: string, requestBody: any): any {
    return this.httpClient.post<any>(this.utilities.baseAPIUrl + `${this.route}` + query, requestBody);
  }

  searchNonOdata(query: string): any {
    return this.httpClient.get<any>(this.utilities.baseAPIUrl + query);
  }

  search(query: string): DataSource {
    return new DataSource({
      store: {
        type: 'odata',
        version: 4,
        url: this.utilities.baseAPIUrl +  query
      }
    });
  }

  save(record: T) {
    return this.httpClient.post<any>(this.utilities.baseAPIUrl + `${this.route}`, record);
  }

  delete(id: Number) {
    return this.httpClient.delete<any>(this.utilities.baseAPIUrl + `${this.route}(${id})`);
  }

  public get oDataSource(): DataSource {
    return new DataSource({
      store: {
        type: 'odata',
        version: 4,
        url: this.utilities.baseAPIUrl + `${this.route}${this.orderByQuery}`
      }
    });
  }
}
