import { Guid } from "guid-typescript";

export class QuizResponse {
  id: Guid;
  title: string;
  startTime: Date;
  totalScore: number;
  resultsVisible: boolean;
  hasStarted: boolean;

  constructor(id: Guid, title: string, startTime: Date, totalScore: number, resultsVisible: boolean, hasStarted: boolean) {
    this.id = id;
    this.title = title;
    this.startTime = startTime;
    this.totalScore = totalScore;
    this.resultsVisible = resultsVisible;
    this.hasStarted = hasStarted;
  }
}
