import { Question } from "./question";

export class Quiz {
  title: string;
  startTime: Date;
  classroomIds: [];
  questions: Question[];

  constructor(title: string, startTime: Date, classroomIds: [], questions: Question[]) {
    this.title = title;
    this.startTime = startTime;
    this.classroomIds = classroomIds;
    this.questions = questions;
  }
}
