import { Injectable } from '@angular/core';
import { Observable } from "rxjs";
import { Classroom } from "../models/classroom";
import { ViewUser } from "../models/view-user";
import { HttpClient, HttpResponse } from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class ProfileService {

  constructor(private http: HttpClient) { }

  getProfile(): Observable<ViewUser> {
    return this.http.get<ViewUser>('basePath/Users/Profile');
  }

  editProfile(user: ViewUser): Observable<HttpResponse<string>> {
    return this.http.put<string>("basePath/Users/EditProfile", {
      "firstName": user.firstName,
      "lastName": user.lastName,
      "email": user.email,
      "studentClassroom": user.studentClassroom
    }, {
      observe: 'response',
      responseType: 'text' as 'json'
    })
  }

  deleteProfile(): Observable<HttpResponse<string>> {
    return this.http.delete<any>("basePath/Users/DeleteAccount")
  }

}
