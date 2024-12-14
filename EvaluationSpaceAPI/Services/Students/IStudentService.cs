using EvaluationSpaceAPI.DTOs;

namespace EvaluationSpaceAPI.Services.StudentQuizzes
{
    public interface IStudentService
    {
        Task<QuizStudentDTO?> GetStudentQuiz(Guid quizId);
        Task<List<QuizStudentTakenDTO>> GetStudentQuizzes(string userEmail);
        Task SubmitQuiz(SubmitQuizDTO response, string studentEmail);
        Task<List<StudentResultDTO>> GetStudentResults(string studentEmail);
    }
}