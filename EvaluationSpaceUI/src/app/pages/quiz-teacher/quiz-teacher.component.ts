import { Component, OnInit } from '@angular/core';
import { FormArray, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatRadioChange } from '@angular/material/radio';
import { Classroom } from 'src/app/models/classroom';
import { RegisterService } from 'src/app/services/register.service';
import { MatCheckboxChange } from "@angular/material/checkbox";
import { Quiz } from "../../models/quiz";
import { Answer } from "../../models/answer";
import { Question } from "../../models/question";
import { QuizTeacherService } from "../../services/quiz-teacher.service";
import { Router } from '@angular/router';

export interface QuestionType {
  value: number;
  viewValue: string;
}

@Component({
  selector: 'app-quiz-teacher',
  templateUrl: './quiz-teacher.component.html',
  styleUrls: ['./quiz-teacher.component.scss']
})
export class QuizTeacherComponent implements OnInit {
  quizForm!: FormGroup;
  public allClassrooms: Classroom[] = [];
  today = new Date().toISOString().slice(0, 16);
  selectedOption: any[] = [];

  questions: QuestionType[] = [
    { value: 1, viewValue: 'Single Choice' },
    { value: 2, viewValue: 'Multiple Choice' },
    { value: 3, viewValue: 'Gap Filling' },
    { value: 4, viewValue: 'True/False' }
  ];

  constructor(private quizTeacherService: QuizTeacherService, private router: Router) { }

  ngOnInit() {
    this.quizTeacherService.getClassroomsTeacher().subscribe(result => {
      this.allClassrooms = result;
    })
    this.initForm();
  }

  private initForm() {
    this.quizForm = new FormGroup({
      'quizTitle': new FormControl('', [Validators.required]),
      'startTime': new FormControl(new Date(), [Validators.required]),
      'classrooms': new FormControl([], [Validators.required]),
      'quizQuestions': new FormArray([]),
    });
    this.onAddQuestion();
  }

  onAddQuestion() {
    const quizQuestionItem = new FormGroup({
      'questionText': new FormControl('', Validators.required),
      'score': new FormControl(0, Validators.required),
      'questionType': new FormControl('', Validators.required),
      'questionAnswers': new FormGroup({})
    });
    (<FormArray>this.quizForm.get('quizQuestions')).push(quizQuestionItem);
  }

  onRemoveQuestion(index: number) {
    (<FormGroup>(<FormArray>this.quizForm.get('quizQuestions')).at(index)).controls['questionAnswers'] = new FormGroup({});
    (<FormGroup>(<FormArray>this.quizForm.get('quizQuestions')).at(index)).controls['questionType'] = new FormControl({});
    (<FormArray>this.quizForm.get('quizQuestions')).removeAt(index);
    this.selectedOption.splice(index, 1)
  }

  onSelectQuestionType(questionType: string, index: number) {
    this.addOptionControls(questionType, index);
  }

  addOptionControls(questionType: string, index: number) {
    let answers: FormArray = new FormArray([]);
    (<FormGroup>(<FormGroup>(<FormArray>this.quizForm.get('quizQuestions')).at(index)).controls['questionAnswers']).addControl('answers', answers);
    this.clearFormArray((<FormArray>(<FormGroup>(<FormGroup>(<FormArray>this.quizForm.get('quizQuestions')).at(index)).controls['questionAnswers']).controls['answers']));
    if (questionType === 'True/False') {
      this.addOptionTF(index);
    } else {
      this.addOption(index);
      this.addOption(index);
    }
  }

  private clearFormArray(formArray: FormArray) {
    while (formArray.length !== 0) {
      formArray.removeAt(0)
    }
  }

  addOptionTF(index: number) {
    const optionGroup1 = new FormGroup({
      'optionText1': new FormControl({ value: 'True', disabled: true }, Validators.required),
      'isCorrect': new FormControl(false, Validators.required)
    });
    (<FormArray>(<FormGroup>(<FormGroup>(<FormArray>this.quizForm.get('quizQuestions')).at(index)).controls['questionAnswers']).controls['answers']).push(optionGroup1);
    const optionGroup2 = new FormGroup({
      'optionText2': new FormControl({ value: 'False', disabled: true }, Validators.required),
      'isCorrect': new FormControl(false, Validators.required)
    });
    (<FormArray>(<FormGroup>(<FormGroup>(<FormArray>this.quizForm.get('quizQuestions')).at(index)).controls['questionAnswers']).controls['answers']).push(optionGroup2);
  }

  addOption(index: number) {
    const optionGroup = new FormGroup({
      'optionText': new FormControl('', Validators.required),
      'isCorrect': new FormControl(false, Validators.required)
    });
    (<FormArray>(<FormGroup>(<FormGroup>(<FormArray>this.quizForm.get('quizQuestions')).at(index)).controls['questionAnswers']).controls['answers']).push(optionGroup);
  }

  removeOption(questionIndex: number, itemIndex: number) {
    (<FormArray>(<FormGroup>(<FormGroup>(<FormArray>this.quizForm.get('quizQuestions')).at(questionIndex)).controls['questionAnswers']).controls['answers']).removeAt(itemIndex);
  }

  putAnswer(event: any, j: number, i: number) {
    if (event instanceof MatRadioChange) {
      var formArrayTemp = (<FormArray>(<FormGroup>(<FormGroup>(<FormArray>this.quizForm.get('quizQuestions')).at(i)).controls['questionAnswers']).controls['answers']);
      formArrayTemp.controls.forEach((element: any, index: any) => {
        if (j === index) {
          element.controls['isCorrect'].setValue(event.source.checked);
        } else {
          element.controls['isCorrect'].setValue(false);
        }
      });
    }
  }

  putAnswerMC(event: any, j: number, i: number) {
    if (event instanceof MatCheckboxChange) {
      var formArrayTemp = (<FormArray>(<FormGroup>(<FormGroup>(<FormArray>this.quizForm.get('quizQuestions')).at(i)).controls['questionAnswers']).controls['answers']);
      formArrayTemp.controls.forEach((element: any, index: any) => {
        if (j === index) {
          element.controls['isCorrect'].setValue(event.checked);
        }
      });
    }
  }

  postQuiz() {
    let formData = this.quizForm.value;
    let quizQuestions = formData.quizQuestions;
    let quizToSend = new Quiz(formData.quizTitle, formData.startTime, formData.classrooms, []);

    quizQuestions.forEach((question: any) => {
      let id = 0;
      this.questions.forEach((q: QuestionType) => {
        if (q.viewValue == question.questionType) {
          id = q.value;
        }
      })
      let questionItem: Question = {
        "questionText": question.questionText,
        "score": question.score,
        "idQuestionType": id,
        "answers": [],
      }

      if (question.questionAnswers.hasOwnProperty('answers')) {
        if (id == 4) {
          let optionItem1: Answer = {
            "answerText": 'True',
            "isCorrect": question.questionAnswers.answers[0].isCorrect
          }
          questionItem.answers.push(optionItem1);
          let optionItem2: Answer = {
            "answerText": 'False',
            "isCorrect": question.questionAnswers.answers[1].isCorrect
          }
          questionItem.answers.push(optionItem2);
        } else {
          question.questionAnswers.answers.forEach((option: any) => {
            let optionItem: Answer = {
              "answerText": option.optionText,
              "isCorrect": option.isCorrect
            }
            questionItem.answers.push(optionItem);
          });
        }
      }
      quizToSend.questions.push(questionItem)
    });
    this.quizTeacherService.createQuiz(quizToSend).subscribe(res => {
      this.router.navigate(['/quizzes']);
    });
  }

  onSubmit() {
    this.postQuiz();
  }

  get surveyQuestions(): any {
    return (<FormArray>this.quizForm.get('quizQuestions')).controls;
  }

  checkedAllAnswers(): boolean {
    let formData = this.quizForm.value;
    let noQuestionsAnswered = 0;
    if (formData.quizQuestions[0].questionAnswers.answers[0] !== undefined) {
      formData.quizQuestions.forEach((question: any) => {
        for (let i = 0; i < question.questionAnswers.answers.length; i++) {
          if (question.questionAnswers.answers[i].isCorrect === true) {
            noQuestionsAnswered++;
            break;
          }
        }
      });
      return noQuestionsAnswered === formData.quizQuestions.length;
    }
    return false;
  }
}
