namespace EvaluationSpaceAPI.Entities;

public partial class User
{
    public Guid Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Salt { get; set; } = null!;

    public int IdRole { get; set; }

    public bool IsDeleted { get; set; }

    public virtual Role IdRoleNavigation { get; set; } = null!;

    public virtual ICollection<Quiz> Quizzes { get; } = new List<Quiz>();

    public virtual ICollection<Result> Results { get; } = new List<Result>();

    public virtual ICollection<Classroom> Classrooms { get; set; } = new List<Classroom>();
}
