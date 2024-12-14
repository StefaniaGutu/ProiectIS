using EvaluationSpaceAPI.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EvaluationSpaceAPI.DTOs
{
    public class QuizDTO
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public DateTime StartTime { get; set; }

        public int? TotalScore { get; set; }

        public bool ResultsVisible { get; set; }

        public virtual List<QuestionDTO> Questions { get; set; }

        public virtual List<SelectListItem> Classrooms { get; set; }

        public QuizDTO(Quiz quiz)
        {
            Id = quiz.Id;
            Title = quiz.Title;
            StartTime = quiz.StartTime;
            TotalScore = quiz.TotalScore;
            ResultsVisible = quiz.ResultsVisible;
            Questions = quiz.Questions.Select(x => new QuestionDTO(x)).ToList();
        }

        public QuizDTO()
        {

        }
    }
}
