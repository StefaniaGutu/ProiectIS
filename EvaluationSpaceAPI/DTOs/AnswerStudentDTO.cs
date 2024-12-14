using EvaluationSpaceAPI.Entities;

namespace EvaluationSpaceAPI.DTOs
{
    public class AnswerStudentDTO
    {
        public Guid Id { get; set; }

        public string AnswerText { get; set; } = null!;

        public AnswerStudentDTO(Answer answer)
        {
            Id = answer.Id;
            AnswerText = answer.AnswerText;
        }

        public AnswerStudentDTO()
        {

        }
    }
}
