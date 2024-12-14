import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse } from "@angular/common/http";
import { Credentials } from "../models/credentials";
import { Observable } from "rxjs";
import { Router } from "@angular/router";
import { tokenExpired } from '../util/auth.util';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  constructor(private http: HttpClient, private router: Router) {
  }

  login(credentials: Credentials): Observable<HttpResponse<string>> {
    return this.http
      .post<string>('basePath/Users/Login', credentials, {
        headers: new HttpHeaders({
          'accept': 'text/plain',
        }),
        observe: 'response',
        responseType: 'text' as 'json'
      });
  }

  logout() {
    window.localStorage.clear();
    this.router.navigate(['/login']);
  }

  isLogged(token: string | null): boolean {
    if (token !== null) {
      if (!tokenExpired(token)) {
        return true;
      }
      this.logout();
      return false;
    }
    return false;
  }

}
