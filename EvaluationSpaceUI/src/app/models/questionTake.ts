import {AnswerTake} from "./answerTake";

export class QuestionTake{
  id: string;
  idQuestionType: number;
  questionText: string;
  score: number;
  answers: AnswerTake[];

  constructor(id: string, idQuestionType: number, questionText: string, score: number, answers: AnswerTake[]) {
    this.id = id;
    this.idQuestionType = idQuestionType;
    this.questionText = questionText;
    this.score = score;
    this.answers = answers;
  }
}
