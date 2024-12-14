import {Answer} from "./answer";

export class Question {
  questionText: string;
  score: number;
  idQuestionType: number;
  answers: Answer[];

  constructor(questionText: string, score: number, idQuestionType: number, answers: Answer[]) {
    this.questionText = questionText;
    this.score = score;
    this.idQuestionType = idQuestionType;
    this.answers = answers;
  }
}
