import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ClassroomStudent } from '../models/classroomStudent';
import { Result } from '../models/result';

@Injectable({
  providedIn: 'root'
})
export class StudentService {

  constructor(private http: HttpClient) { }

  GetStudentClassrooms(): Observable<ClassroomStudent> {
    return this.http.get<ClassroomStudent>('basePath/Classrooms/GetStudentClassroom');
  }

  getStudentResults(): Observable<Result[]> {
    return this.http.get<Result[]>('basePath/StudentQuizzes/getStudentResults');
  }
}
