export class SubmitQuestion{
  idQuestion: string;
  answers: string[];

  constructor(idQuestion: string, answers: string[]) {
    this.idQuestion = idQuestion;
    this.answers = answers;
  }
}
