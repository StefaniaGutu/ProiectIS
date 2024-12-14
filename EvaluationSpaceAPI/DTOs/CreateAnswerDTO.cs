namespace EvaluationSpaceAPI.DTOs
{
    public class CreateAnswerDTO
    {
        public string AnswerText { get; set; } = null!;

        public bool IsCorrect { get; set; }
    }
}
