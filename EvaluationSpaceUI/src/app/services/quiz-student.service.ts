import { HttpClient, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Guid } from "guid-typescript";
import { SubmitQuiz } from "../models/submitQuiz";

@Injectable({
  providedIn: 'root'
})
export class QuizStudentService {

  constructor(private http: HttpClient) { }

  getQuizzes(): Observable<any[]> {
    return this.http.get<any[]>('basePath/StudentQuizzes/getStudentQuizzes');
  }

  getQuiz(id: Guid): Observable<any> {
    return this.http.get<any>('basePath/StudentQuizzes/quizId/' + id);
  }

  takeQuiz(quiz: SubmitQuiz): Observable<HttpResponse<string>> {
    return this.http.post<string>("basePath/StudentQuizzes/submitStudentQuiz", {
      "idQuiz": quiz.idQuiz,
      "questionAnswers": quiz.questionAnswers
    }, {
      observe: 'response',
      responseType: 'text' as 'json'
    })
  }
}
