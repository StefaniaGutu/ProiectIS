namespace EvaluationSpaceAPI.Entities;

public partial class Classroom
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Quiz> IdQuizzes { get; set; } = new List<Quiz>();

    public virtual ICollection<User> IdUsers { get; set; } = new List<User>();
}
