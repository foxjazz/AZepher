import {Inject, Injectable} from '@angular/core';
import {Observable} from 'rxjs';
import {DbCommand, SqlCommand, Table} from '../Interfaces/Database';
import {HttpClient} from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class DBService {

  private baseUri: string;
  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUri = baseUrl;
  }

  getTables(): Observable<Table[]> {
    return this.http.get<Table[]>(this.baseUri + 'api/Commands');
  }

  sqlCommand(sc: SqlCommand): any {
    return this.http.get<any>(this.baseUri + 'api/SqlCommands');
  }

  dbCommand(dbc: DbCommand): any {

  }
}
