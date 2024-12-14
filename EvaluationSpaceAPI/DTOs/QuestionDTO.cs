using EvaluationSpaceAPI.Entities;

namespace EvaluationSpaceAPI.DTOs
{
    public class QuestionDTO
    {

        public QuestionDTO()
        {

        }
        public QuestionDTO(Question question)
        {
            Id = question.Id;
            IdQuestionType = question.IdQuestionType;
            QuestionText = question.QuestionText;
            Score = question.Score;
            Answers = question.Answers.Select(x => new AnswerDTO(x)).ToList();
        }

        public Guid Id { get; set; }

        public int IdQuestionType { get; set; }

        public string QuestionText { get; set; }

        public int Score { get; set; }

        public virtual List<AnswerDTO> Answers { get; set; }
    }
}
