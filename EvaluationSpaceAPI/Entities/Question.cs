using System;
using System.Collections.Generic;

namespace EvaluationSpaceAPI.Entities;

public partial class Question
{
    public Guid Id { get; set; }

    public Guid IdQuiz { get; set; }

    public int IdQuestionType { get; set; }

    public string QuestionText { get; set; } = null!;

    public int Score { get; set; }

    public virtual ICollection<Answer> Answers { get; } = new List<Answer>();

    public virtual QuestionType IdQuestionTypeNavigation { get; set; } = null!;

    public virtual Quiz IdQuizNavigation { get; set; } = null!;
}
