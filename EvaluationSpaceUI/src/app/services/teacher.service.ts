import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from "@angular/common/http";
import { Observable } from "rxjs";
import { ClassroomStudent } from "../models/classroomStudent";
import { ClassroomTeacher } from "../models/classroomTeacher";
import { User } from "../models/user";
import { EditClassroom } from "../models/editClassroom";
import { ViewUser } from "../models/view-user";
import { ResultTeacher } from '../models/resultTeacher';

@Injectable({
  providedIn: 'root'
})
export class TeacherService {

  constructor(private http: HttpClient) { }

  GetTeacherClassrooms(): Observable<ClassroomTeacher[]> {
    return this.http.get<ClassroomTeacher[]>('basePath/Classrooms/GetTeacherClassrooms');
  }

  editClassroom(classroom: EditClassroom): Observable<HttpResponse<string>> {
    return this.http.put<string>("basePath/Classrooms/EditTeacherClassroom", {
      "id": classroom.id,
      "name": classroom.name,
      "studentIds": classroom.studentIds
    }, {
      observe: 'response',
      responseType: 'text' as 'json'
    })
  }

  GetStudentsWithoutClassrooms(): Observable<ViewUser[]> {
    return this.http.get<ViewUser[]>('basePath/Classrooms/GetStudentsWithoutClassroom');
  }

  GetResultsStatistics(): Observable<ResultTeacher[]> {
    return this.http.get<ResultTeacher[]>('basePath/TeacherQuizzes/GetTeacherResults');
  }
}
