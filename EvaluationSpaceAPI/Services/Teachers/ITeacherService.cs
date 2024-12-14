using EvaluationSpaceAPI.DTOs;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EvaluationSpaceAPI.Services.TeacherQuizzes
{
    public interface ITeacherService
    {
        Task<List<TeacherQuizzesListDTO>> GetTeacherQuizzesList(string teacherEmail);

        Task CreateQuiz(CreateQuizDTO quiz, string teacherEmail);

        Task DeleteQuiz(Guid quizId);

        Task<QuizDTO> GetQuiz(Guid quizId);

        Task EditQuiz(EditQuizDTO quiz, string teacherEmail);

        Task ShowResults(Guid quizId, bool show);
        Task<List<TeacherResultDTO>> GetTeacherResults(string teacherEmail);
    }
}
