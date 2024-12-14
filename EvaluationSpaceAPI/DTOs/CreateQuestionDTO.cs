namespace EvaluationSpaceAPI.DTOs
{
    public class CreateQuestionDTO
    {
        public string QuestionText { get; set; } = null!;

        public int Score { get; set; }

        public int IdQuestionType { get; set; }

        public List<CreateAnswerDTO> Answers { get; set; }
    }
}
