using EvaluationSpaceAPI.EFContext;
using EvaluationSpaceAPI.Entities;
using EvaluationSpaceAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EvaluationSpaceAPI.Repositories
{
    public class QuestionRepository : GenericRepository<Question>, IQuestionRepository
    {
        public QuestionRepository(EvaluationSpaceDbContext context) : base(context)
        {
        }

        public async Task<Question?> GetQuestion(Guid questionId)
        {
            return await _context.Questions
                .Where(q => q.Id == questionId)
                .Include(q => q.Answers)
                .FirstOrDefaultAsync();
        }
    }
}
