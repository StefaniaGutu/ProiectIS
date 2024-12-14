import { Guid } from "guid-typescript";

export class QuizResponseStudent {
    id: Guid;
    title: string;
    startTime: Date;
    totalScore: number;
    resultsVisible: boolean;
    result?: number;
    isTaken: boolean;

    constructor(id: Guid, title: string, startTime: Date, totalScore: number, resultsVisible: boolean, isTaken: boolean) {
        this.id = id;
        this.title = title;
        this.startTime = startTime;
        this.totalScore = totalScore;
        this.resultsVisible = resultsVisible;
        this.isTaken = isTaken;
    }
}
