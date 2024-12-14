export class ResultTeacher {
    quizTitle: string;
    studentName: string;
    score: number;
    totalScore: number;
    className: string;

    constructor(quizTitle: string, studentName: string, score: number, totalScore: number, className: string) {
        this.quizTitle = quizTitle;
        this.studentName = studentName;
        this.score = score;
        this.totalScore = totalScore;
        this.className = className;
    }
}
