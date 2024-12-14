namespace EvaluationSpaceAPI.DTOs
{
    public class SubmitQuestionAnswerDTO
    {
        public Guid IdQuestion { get; set; }
        public List<Guid> Answers { get; set; }
    }
}
