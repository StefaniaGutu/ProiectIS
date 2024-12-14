namespace EvaluationSpaceAPI.Entities;

public partial class Quiz
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public Guid IdTeacher { get; set; }

    public DateTime StartTime { get; set; }

    public int? TotalScore { get; set; }

    public bool ResultsVisible { get; set; }

    public virtual User IdTeacherNavigation { get; set; } = null!;

    public virtual ICollection<Question> Questions { get; } = new List<Question>();

    public virtual ICollection<Result> Results { get; } = new List<Result>();

    public virtual ICollection<Classroom> IdClassrooms { get; set; } = new List<Classroom>();
}
