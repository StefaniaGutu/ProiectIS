import { Component, OnInit } from '@angular/core';
import { QuizStudentService } from "../../services/quiz-student.service";
import { ActivatedRoute, Router } from "@angular/router";
import { Guid } from "guid-typescript";
import { MatRadioChange } from "@angular/material/radio";
import { MatCheckboxChange } from "@angular/material/checkbox";
import { SubmitQuiz } from "../../models/submitQuiz";
import { SubmitQuestion } from "../../models/submitQuestion";
import { QuizResponseBeforeSubmit } from "../../models/quizResponseBeforeSubmit";
import { QuestionTake } from "../../models/questionTake";

@Component({
  selector: 'app-quiz-student',
  templateUrl: './quiz-student.component.html',
  styleUrls: ['./quiz-student.component.scss']
})
export class QuizStudentComponent implements OnInit {
  id!: Guid;
  isAllChecked: boolean = false;
  submitQuiz: SubmitQuiz = new SubmitQuiz('', []);
  quizToTake!: QuizResponseBeforeSubmit;
  checked: boolean = false;

  constructor(private quizStudentService: QuizStudentService, private activatedRoute: ActivatedRoute, private router: Router) {
  }

  ngOnInit(): void {
    this.activatedRoute.params.subscribe((param) => {
      this.id = param['id'];
      this.getQuizById(this.id);
    })
  }

  getQuizById(id: Guid) {
    this.quizStudentService.getQuiz(id).subscribe(res => {
      this.quizToTake = res;
      this.submitQuiz.idQuiz = this.quizToTake.id;
      this.quizToTake.questions.forEach((q: QuestionTake) => {
        let question: SubmitQuestion = {
          "idQuestion": q.id,
          "answers": []
        }
        this.submitQuiz.questionAnswers.push(question);
      })
    });
  }

  putAnswer($event: MatRadioChange, j: number, i: number) {
    if (this.submitQuiz.questionAnswers[i].answers.length == 0) {
      this.submitQuiz.questionAnswers[i].answers.push(this.quizToTake.questions[i].answers[j].id);
    } else {
      this.submitQuiz.questionAnswers[i].answers.pop();
      this.submitQuiz.questionAnswers[i].answers.push(this.quizToTake.questions[i].answers[j].id);
    }
  }

  putAnswerMC(event: any, j: number, i: number) {
    let answers = this.submitQuiz.questionAnswers[i].answers;
    if (event instanceof MatCheckboxChange) {
      if (event.checked) {
        this.submitQuiz.questionAnswers[i].answers.push(this.quizToTake.questions[i].answers[j].id);
      }
      else {
        answers = answers.filter(a => a !== this.quizToTake.questions[i].answers[j].id);
        this.submitQuiz.questionAnswers[i].answers = answers;
      }
    }
  }

  checkedAllAnswers() {
    let noTotalAnswers = 0;
    this.submitQuiz.questionAnswers.forEach(question => {
      if (question.answers.length !== 0) {
        noTotalAnswers += 1
      }
    })
    return noTotalAnswers === this.submitQuiz.questionAnswers.length;
  }

  submitStudentQuiz() {
    this.quizStudentService.takeQuiz(this.submitQuiz).subscribe(res => {
      this.router.navigate(['/quizzes']);
    });
  }
}
