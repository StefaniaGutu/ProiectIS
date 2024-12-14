using EvaluationSpaceAPI.Entities;

namespace EvaluationSpaceAPI.DTOs
{
    public class QuestionStudentDTO
    {
        public QuestionStudentDTO()
        {

        }
        public QuestionStudentDTO(Question question)
        {
            Id = question.Id;
            IdQuestionType = question.IdQuestionType;
            QuestionText = question.QuestionText;
            Score = question.Score;
            Answers = question.Answers.Select(x => new AnswerStudentDTO(x)).ToList();
        }

        public Guid Id { get; set; }

        public int IdQuestionType { get; set; }

        public string QuestionText { get; set; }

        public int Score { get; set; }

        public virtual List<AnswerStudentDTO> Answers { get; set; }
    }
}
