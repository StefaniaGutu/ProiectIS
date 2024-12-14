import { Component, OnInit } from '@angular/core';
import { Quiz } from "../../models/quiz";
import { QuizTeacherService } from "../../services/quiz-teacher.service";
import { DatePipe } from "@angular/common";
import { QuizResponse } from "../../models/quizResponse";
import { Guid } from 'guid-typescript';
import { DialogComponent } from 'src/app/components/dialog/dialog.component';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { getToken } from 'src/app/util/auth.util';
import { QuizStudentService } from 'src/app/services/quiz-student.service';
import { QuizResponseStudent } from 'src/app/models/quizResponseStudent';

@Component({
  selector: 'app-quizzes-teacher',
  templateUrl: './quizzes-teacher.component.html',
  styleUrls: ['./quizzes-teacher.component.scss']
})
export class QuizzesTeacherComponent implements OnInit {
  quizzesList: QuizResponse[] = [];
  quizzesListStudent: QuizResponseStudent[] = [];
  hasStarted: boolean = false;
  public messageDelete: string = 'Are you sure you want to delete this quiz?';
  public errorMessage: string | boolean = false;

  constructor(private quizTeacherService: QuizTeacherService, public datePipe: DatePipe,
    public dialog: MatDialog, private _snackBar: MatSnackBar, private router: Router,
    private quizStudentService: QuizStudentService) { }

  ngOnInit(): void {
    if (this.isStudent()) {
      this.quizStudentService.getQuizzes().subscribe(result => {
        this.quizzesListStudent = result;
        this.quizzesListStudent = this.quizzesListStudent.sort((a, b) => a.startTime < b.startTime ? -1 : 1);
      })
    }
    else {
      this.quizTeacherService.getQuizzes().subscribe(result => {
        this.quizzesList = result;
        this.quizzesList = this.quizzesList.sort((a, b) => a.startTime < b.startTime ? -1 : 1);
      })
    }
  }

  public transformDateToString(date: Date): string | null {
    return this.datePipe.transform(date, "yyyy-MM-dd HH:mm:ss")
  }

  openDialog(id: Guid) {
    const dialogRef = this.dialog.open(DialogComponent, {
      data: this.messageDelete,
      height: '150px',
      width: '350px'
    });

    dialogRef.afterClosed().subscribe((result: string) => {
      if (result === 'Confirm') {
        this.quizTeacherService.deleteQuiz(id).subscribe(result => {
          this.quizzesList = this.quizzesList.filter(quiz => quiz.id !== id);
        }, error => {
          this.errorMessage = error.error;
          if (typeof this.errorMessage === "string") {
            this.openSnackBar(this.errorMessage, 'Close');
          }
        })
      }
    });

  }

  openSnackBar(message: string, action: string) {
    this._snackBar.open(message, action, {
      panelClass: ['blue-snackbar']
    });
  }

  changeToggle(event: any, id: Guid) {
    if (event.checked) {
      this.quizTeacherService.showResults(id).subscribe();
    }
    else {
      this.quizTeacherService.hideResuts(id).subscribe();
    }
  }

  quizStarted(startTime: Date) {
    let dateNow = new Date();
    return new Date(startTime) < dateNow;
  }

  editQuiz(id: Guid, startTime: Date) {
    this.router.navigate(['/editQuizTeacher/' + id]);
  }

  isStudent(): boolean | null {
    let token: string | null = getToken();
    if (token != null) {
      let jwtData = token.split('.')[1]
      let decodedJwtJsonData = window.atob(jwtData)
      let decodedJwtData = JSON.parse(decodedJwtJsonData).role
      return decodedJwtData === 'Student';
    }
    return null;
  }

  takeQuiz(id: Guid) {
    this.router.navigate(['/takeQuiz/' + id]);
  }

}
