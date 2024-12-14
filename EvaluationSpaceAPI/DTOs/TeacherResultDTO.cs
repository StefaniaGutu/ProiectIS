namespace EvaluationSpaceAPI.DTOs
{
    public class TeacherResultDTO
    {
        public string QuizTitle { get; set; } = string.Empty;

        public string StudentName { get; set; } = string.Empty;

        public int? Score { get; set; }
        public int? TotalScore { get; set; }
        public string? ClassName { get; set; } = string.Empty;
    }
}
