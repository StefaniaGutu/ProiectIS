namespace EvaluationSpaceAPI.Entities;

public partial class Result
{
    public Guid Id { get; set; }

    public Guid IdStudent { get; set; }

    public Guid IdQuiz { get; set; }

    public int Score { get; set; }

    public DateTime Date { get; set; }

    public virtual Quiz IdQuizNavigation { get; set; } = null!;

    public virtual User IdStudentNavigation { get; set; } = null!;
}
