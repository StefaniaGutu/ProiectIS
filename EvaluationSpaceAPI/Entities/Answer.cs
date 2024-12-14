using System;
using System.Collections.Generic;

namespace EvaluationSpaceAPI.Entities;

public partial class Answer
{
    public Guid Id { get; set; }

    public string AnswerText { get; set; } = null!;

    public bool IsCorrect { get; set; }

    public Guid IdQuestion { get; set; }

    public virtual Question IdQuestionNavigation { get; set; } = null!;
}
