using System;
using System.Collections.Generic;

namespace EvaluationSpaceAPI.Entities;

public partial class QuestionType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Question> Questions { get; } = new List<Question>();
}
