using EvaluationSpaceAPI.Entities;

namespace EvaluationSpaceAPI.Repositories.Interfaces
{
    public interface IQuestionRepository : IGenericRepository<Question>
    {
        Task<Question?> GetQuestion(Guid questionId);
    }
}
