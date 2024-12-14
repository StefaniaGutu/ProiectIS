namespace EvaluationSpaceAPI.DTOs
{
    public class SubmitQuizDTO
    {
        public Guid IdQuiz { get; set; }
        public virtual List<SubmitQuestionAnswerDTO> QuestionAnswers { get; set; }
    }
}
