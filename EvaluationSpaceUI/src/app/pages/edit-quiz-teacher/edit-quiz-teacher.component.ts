import { Component, OnInit } from '@angular/core';
import { FormArray, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatCheckboxChange } from '@angular/material/checkbox';
import { MatRadioChange } from '@angular/material/radio';
import { ActivatedRoute, Router } from '@angular/router';
import { Guid } from 'guid-typescript';
import { Answer } from 'src/app/models/answer';
import { Classroom } from 'src/app/models/classroom';
import { Question } from 'src/app/models/question';
import { QuizTeacherService } from 'src/app/services/quiz-teacher.service';
import { QuizEdit } from "../../models/quizEdit";

export interface QuestionType {
  value: number;
  viewValue: string;
}

@Component({
  selector: 'app-edit-quiz-teacher',
  templateUrl: './edit-quiz-teacher.component.html',
  styleUrls: ['./edit-quiz-teacher.component.scss']
})
export class EditQuizTeacherComponent implements OnInit {
  id!: Guid;
  oldQuiz: QuizEdit = {
    id: '',
    title: '',
    startTime: new Date(),
    totalScore: 0,
    resultsVisible: false,
    questions: [],
    classrooms: []
  };
  quizForm!: FormGroup;
  allClassrooms: Classroom[] = [];
  classroomsValue = '';
  today = new Date().toISOString().slice(0, 16);
  selectedOption: any[] = [];

  questions: QuestionType[] = [
    { value: 1, viewValue: 'Single Choice' },
    { value: 2, viewValue: 'Multiple Choice' },
    { value: 3, viewValue: 'Gap Filling' },
    { value: 4, viewValue: 'True/False' }
  ];

  isDisabled: boolean = false;

  constructor(private activatedRoute: ActivatedRoute, private quizTeacherService: QuizTeacherService, private router: Router) {
  }

  ngOnInit(): void {
    this.quizTeacherService.getClassroomsTeacher().subscribe(result => {
      this.allClassrooms = result;
      this.activatedRoute.params.subscribe((param) => {
        this.id = param['id'];
        this.getQuizById(this.id);
      })
    })
    this.initForm();
  }

  getQuizById(id: Guid) {
    this.quizTeacherService.getQuiz(id).subscribe(res => {
      this.oldQuiz.id = res.id;
      this.oldQuiz.title = res.title;
      this.oldQuiz.startTime = res.startTime;
      if (new Date(this.oldQuiz.startTime) < new Date()) {
        this.isDisabled = true;
      }
      this.oldQuiz.resultsVisible = res.resultsVisible;
      this.oldQuiz.totalScore = res.totalScore;
      this.oldQuiz.classrooms = res.classrooms;
      this.oldQuiz.questions = res.questions;
      this.allClassrooms.forEach(classroom => {
        this.oldQuiz.classrooms.forEach((classroomRes: any) => {
          if (classroomRes.value === classroom.value) {
            this.classroomsValue += classroom.text + ', ';
          }
        })
      })
      this.classroomsValue = this.classroomsValue.slice(0, -2);
      this.quizForm.controls['quizTitle'].setValue(this.oldQuiz.title);
      this.quizForm.controls['startTime'].setValue(this.oldQuiz.startTime);
      this.oldQuiz.questions.forEach((question: Question) => {
        let value = '';
        this.questions.forEach((q: QuestionType) => {
          if (q.value == question.idQuestionType) {
            value = q.viewValue;
          }
        })

        this.selectedOption.push(value);
        const quizQuestionItem = new FormGroup({
          'questionText': new FormControl(question.questionText, Validators.required),
          'score': new FormControl(question.score, Validators.required),
          'questionType': new FormControl(value, Validators.required),
          'questionAnswers': new FormGroup({})
        });

        let answers: FormArray = new FormArray([]);
        (<FormGroup>quizQuestionItem.controls['questionAnswers']).addControl('answers', answers);

        if (value === 'True/False') {
          const optionGroup1 = new FormGroup({
            'optionText1': new FormControl({ value: 'True', disabled: true }, Validators.required),
            'isCorrect': new FormControl(question.answers[0].isCorrect, Validators.required)
          });
          (<FormArray>(<FormGroup>quizQuestionItem.controls['questionAnswers']).controls['answers']).push(optionGroup1);
          const optionGroup2 = new FormGroup({
            'optionText2': new FormControl({ value: 'False', disabled: true }, Validators.required),
            'isCorrect': new FormControl(question.answers[1].isCorrect, Validators.required)
          });
          (<FormArray>(<FormGroup>quizQuestionItem.controls['questionAnswers']).controls['answers']).push(optionGroup2);
        } else {
          question.answers.forEach((answer: Answer) => {
            const optionGroup = new FormGroup({
              'optionText': new FormControl(answer.answerText, Validators.required),
              'isCorrect': new FormControl(answer.isCorrect, Validators.required)
            });
            (<FormArray>(<FormGroup>quizQuestionItem.controls['questionAnswers']).controls['answers']).push(optionGroup);
          });
        }
        if (new Date(this.oldQuiz.startTime) < new Date()) {
          quizQuestionItem.disable();
        }
        (<FormArray>this.quizForm.get('quizQuestions')).push(quizQuestionItem);
      })
      if (new Date(this.oldQuiz.startTime) < new Date()) {
        this.quizForm.disable();
      }
    });
  }

  private initForm() {
    this.quizForm = new FormGroup({
      'quizTitle': new FormControl('', [Validators.required]),
      'startTime': new FormControl(new Date(), [Validators.required]),
      'classrooms': new FormControl([]),
      'quizQuestions': new FormArray([]),
    });
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
    let oldQuizzesIds: any = [];
    formData.classrooms.forEach((classroomRes: any) => {
      let classroom: Guid = Guid.parse(classroomRes);
      oldQuizzesIds.push(classroom);
    })
    let quizToSend: QuizEdit;
    if (formData.classrooms.length == 0) {
      quizToSend = new QuizEdit(this.oldQuiz.id, formData.quizTitle, formData.startTime, this.oldQuiz.totalScore, this.oldQuiz.resultsVisible, [], this.oldQuiz.classrooms);
    } else {
      quizToSend = new QuizEdit(this.oldQuiz.id, formData.quizTitle, formData.startTime, this.oldQuiz.totalScore, this.oldQuiz.resultsVisible, [], oldQuizzesIds);
    }

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
    this.quizTeacherService.updateQuiz(quizToSend).subscribe(res => {
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

  checkCorrect(j: number, i: number): boolean {
    return (<FormGroup>(<FormArray>(<FormGroup>(<FormGroup>(<FormArray>this.quizForm.get('quizQuestions')).at(i)).controls['questionAnswers']).controls['answers']).at(j)).controls['isCorrect'].value;
  }
}
