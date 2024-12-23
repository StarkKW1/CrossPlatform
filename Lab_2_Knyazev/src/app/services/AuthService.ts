import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse} from '@angular/common/http';
import {Md5} from 'ts-md5';
import { Observable, of } from 'rxjs';
import { map, catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseApiUrl: string = "http://localhost:5228/api/";
  /*token: string | null = null;*/

  constructor(private _http: HttpClient) { }

  get isAuth() {
    return !!localStorage.getItem("token");
  }

  public login(info: { login: string, password: string }): Observable<number> {

    info.password = Md5.hashStr(info.password).toUpperCase() as string;

    return this._http.post<any>(`${this.baseApiUrl}Auth/`, info, {observe: 'response'})
      .pipe(
        map(res =>
        {
          if (res.status == 200)
            localStorage.setItem("token", res.body.token);
          return res.status;
        }),
        catchError(error => {
          return of((error as HttpResponse<any>).status);
        })
      );
  }
}

