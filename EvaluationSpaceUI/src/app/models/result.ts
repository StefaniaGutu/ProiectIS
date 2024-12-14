export class Result {
    quizTitle: string;
    teacherName: string;
    resultsVisible: boolean;
    score: number;
    totalScore: number;

    constructor(quizTitle: string, teacherName: string, resultsVisible: boolean, score: number, totalScore: number) {
        this.quizTitle = quizTitle;
        this.teacherName = teacherName;
        this.resultsVisible = resultsVisible;
        this.score = score;
        this.totalScore = totalScore;
    }
}
