using EvaluationSpaceAPI.EFContext;
using EvaluationSpaceAPI.Entities;
using EvaluationSpaceAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EvaluationSpaceAPI.Repositories
{
    public class QuizzesRepository : GenericRepository<Quiz>, IQuizzesRepository
    {
        public QuizzesRepository(EvaluationSpaceDbContext context) : base(context)
        {
        }

        public async Task<List<Quiz>> GetAllQuizzes()
        {
            return await _context.Quizzes
                .Include(q => q.IdClassrooms)
                .Include(q => q.Results)
                .Include(q => q.Questions)
                .ThenInclude(q => q.Answers)
                .ToListAsync();
        }

        public async Task<Quiz?> GetQuiz(Guid quizId)
        {
            return await _context.Quizzes
                .Where(q => q.Id == quizId)
                .Include(q => q.IdClassrooms)
                .Include(q => q.Results)
                .Include(q => q.Questions)
                .ThenInclude(q => q.Answers)
                .FirstOrDefaultAsync();

        }
    }
}
