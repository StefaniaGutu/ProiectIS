using EvaluationSpaceAPI.Entities;

namespace EvaluationSpaceAPI.Repositories.Interfaces
{
    public interface IQuizzesRepository : IGenericRepository<Quiz>
    {
        Task<List<Quiz>> GetAllQuizzes();
        Task<Quiz?> GetQuiz(Guid quizId);
    }
}
