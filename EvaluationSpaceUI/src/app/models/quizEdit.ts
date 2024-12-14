import {Question} from "./question";
import {Guid} from "guid-typescript";

export class QuizEdit {
  id: string;
  title: string;
  startTime: Date;
  totalScore: number;
  resultsVisible: boolean;
  questions: Question[];
  classrooms: Guid[];

  constructor(id: string, title: string, startTime: Date, totalScore: number, resultsVisible: boolean, questions: Question[], classrooms: Guid[]) {
    this.id = id;
    this.title = title;
    this.startTime = startTime;
    this.totalScore = totalScore;
    this.resultsVisible = resultsVisible;
    this.questions = questions;
    this.classrooms = classrooms;
  }
}
