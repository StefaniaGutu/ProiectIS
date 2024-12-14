import {SubmitQuestion} from "./submitQuestion";

export class SubmitQuiz{
  idQuiz: string;
  questionAnswers: SubmitQuestion[];

  constructor(idQuiz: string, questionAnswers: SubmitQuestion[]) {
    this.idQuiz = idQuiz;
    this.questionAnswers = questionAnswers;
  }
}
