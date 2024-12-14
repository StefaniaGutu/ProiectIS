namespace EvaluationSpaceAPI.DTOs
{
    public class StudentResultDTO
    {
        public string QuizTitle { get; set; }

        public string TeacherName { get; set; }
        public bool ResultsVisible { get; set; }

        public int? Score { get; set; }
        public int TotalScore { get; set; }
    }
}
