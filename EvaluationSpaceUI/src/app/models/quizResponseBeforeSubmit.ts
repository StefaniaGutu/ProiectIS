import {Guid} from "guid-typescript";
import {Question} from "./question";
import {QuestionTake} from "./questionTake";

export class QuizResponseBeforeSubmit{
  id: string;
  title: string;
  startTime: Date;
  totalScore: number;
  resultsVisible: boolean;
  classrooms: Guid[];
  questions: QuestionTake[];

  constructor(id: string, title: string, startTime: Date, totalScore: number, resultsVisible: boolean, classrooms: Guid[], questions: QuestionTake[]) {
    this.id = id;
    this.title = title;
    this.startTime = startTime;
    this.totalScore = totalScore;
    this.resultsVisible = resultsVisible;
    this.classrooms = classrooms;
    this.questions = questions;
  }
}
