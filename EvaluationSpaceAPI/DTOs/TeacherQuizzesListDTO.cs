namespace EvaluationSpaceAPI.DTOs
{
    public class TeacherQuizzesListDTO
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public DateTime StartTime { get; set; }

        public int? TotalScore { get; set; }

        public bool ResultsVisible { get; set; }

        public bool hasStarted { get; set; }
    }
}
