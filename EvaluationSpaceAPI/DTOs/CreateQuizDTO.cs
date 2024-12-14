namespace EvaluationSpaceAPI.DTOs
{
    public class CreateQuizDTO
    {
        public string Title { get; set; }

        public DateTime StartTime { get; set; }

        public List<Guid> ClassroomIds { get; set; }

        public List<CreateQuestionDTO> Questions { get; set; }
    }
}
