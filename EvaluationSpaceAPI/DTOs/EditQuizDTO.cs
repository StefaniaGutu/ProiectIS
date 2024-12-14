using EvaluationSpaceAPI.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EvaluationSpaceAPI.DTOs
{
    public class EditQuizDTO
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public DateTime StartTime { get; set; }

        public int? TotalScore { get; set; }

        public bool ResultsVisible { get; set; }

        public virtual List<CreateQuestionDTO> Questions { get; set; }

        public virtual List<SelectListItem> Classrooms { get; set; }
    }
}

