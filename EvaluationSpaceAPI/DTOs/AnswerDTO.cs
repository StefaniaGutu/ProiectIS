using EvaluationSpaceAPI.Entities;

namespace EvaluationSpaceAPI.DTOs
{
    public class AnswerDTO
    {
        public Guid Id { get; set; }

        public string AnswerText { get; set; } = null!;

        public bool IsCorrect { get; set; }

        public AnswerDTO(Answer answer)
        {
            Id = answer.Id;
            AnswerText = answer.AnswerText;
            IsCorrect = answer.IsCorrect;
        }

        public AnswerDTO()
        {

        }
    }
}
