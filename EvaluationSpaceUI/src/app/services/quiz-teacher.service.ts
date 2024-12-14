import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from "@angular/common/http";
import { Observable } from "rxjs";
import { Quiz } from "../models/quiz";
import { QuizResponse } from "../models/quizResponse";
import { Guid } from 'guid-typescript';
import { Classroom } from '../models/classroom';
import {QuizEdit} from "../models/quizEdit";

@Injectable({
  providedIn: 'root'
})
export class QuizTeacherService {

  constructor(private http: HttpClient) { }

  createQuiz(quiz: Quiz): Observable<HttpResponse<string>> {
    return this.http.post<string>("basePath/TeacherQuizzes/CreateQuiz", {
      "title": quiz.title,
      "startTime": quiz.startTime,
      "classroomIds": quiz.classroomIds,
      "questions": quiz.questions
    }, {
      observe: 'response',
      responseType: 'text' as 'json'
    })
  }

  getQuizzes(): Observable<QuizResponse[]> {
    return this.http.get<QuizResponse[]>('basePath/TeacherQuizzes/GetQuizzesList');
  }

  getQuiz(id: Guid): Observable<any> {
    return this.http.get<any>('basePath/TeacherQuizzes/GetQuiz/' + id);
  }

  deleteQuiz(id: Guid): Observable<HttpResponse<string>> {
    return this.http.delete<any>("basePath/TeacherQuizzes/DeleteQuiz/" + id);
  }

  showResults(id: Guid): Observable<HttpResponse<string>> {
    let quizId: Guid = id;
    return this.http.put<string>("basePath/TeacherQuizzes/ShowResults/" + quizId, quizId, {
      observe: 'response',
      responseType: 'text' as 'json'
    })
  }

  hideResuts(id: Guid): Observable<HttpResponse<string>> {
    let quizId: Guid = id;
    return this.http.put<string>("basePath/TeacherQuizzes/HideResults/" + quizId, quizId, {
      observe: 'response',
      responseType: 'text' as 'json'
    })
  }

  getClassroomsTeacher(): Observable<[Classroom]> {
    return this.http.get<[Classroom]>('basePath/Classrooms/SelectListTeacherClassrooms');
  }

  updateQuiz(quiz: QuizEdit): Observable<HttpResponse<any>> {
    return this.http.post<any>("basePath/TeacherQuizzes/EditQuiz", {
      "id": quiz.id,
      "title": quiz.title,
      "startTime": quiz.startTime,
      "totalScore": quiz.totalScore,
      "resultsVisible": quiz.resultsVisible,
      "questions": quiz.questions,
      "classrooms": quiz.classrooms
    }, {
      observe: 'response',
      responseType: 'text' as 'json'
    })
  }

}
