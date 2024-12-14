using EvaluationSpaceAPI.EFContext;
using EvaluationSpaceAPI.Entities;
using EvaluationSpaceAPI.Repositories.Interfaces;

namespace EvaluationSpaceAPI.Repositories
{
    public class AnswerRepository : GenericRepository<Answer>, IAnswerRepository
    {
        public AnswerRepository(EvaluationSpaceDbContext context) : base(context)
        {
        }
    }
}
