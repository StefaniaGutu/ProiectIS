import { Injectable } from '@angular/core';
import {HttpClient, HttpResponse} from "@angular/common/http";
import {Observable} from "rxjs";
import {Classroom} from "../models/classroom";
import {User} from "../models/user";

@Injectable({
  providedIn: 'root'
})
export class RegisterService {

  constructor(private http: HttpClient) { }

  getClassrooms(): Observable<[Classroom]> {
    return this.http.get<[Classroom]>('basePath/Classrooms/SelectListClassrooms');
  }

  doRegister(user:User):Observable<HttpResponse<string>> {
    return this.http.post<string>("basePath/Users/Register", {
      "firstName": user.firstName,
      "lastName": user.lastName,
      "email": user.email,
      "password": user.password,
      "idRole": user.idRole,
      "classroomIds": user.classroomIds
    }, {
      observe: 'response',
      responseType: 'text' as 'json'
    })
  }
}
