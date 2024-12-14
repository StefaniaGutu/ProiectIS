using EvaluationSpaceAPI.EFContext;
using EvaluationSpaceAPI.Entities;
using EvaluationSpaceAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EvaluationSpaceAPI.Repositories
{
    public class ClassroomRepository : GenericRepository<Classroom>, IClassroomRepository
    {
        public ClassroomRepository(EvaluationSpaceDbContext context) : base(context) { }

        public async Task<List<Classroom>> GetAllClassroomsWithDetails()
        {
            return await _context.Classrooms.Include(c => c.IdUsers).Include(c => c.IdQuizzes).ToListAsync();
        }

        public async Task<Classroom?> GetAllClassroomWithDetails(Guid id)
        {
            return await _context.Classrooms.Where(x => x.Id.Equals(id)).Include(c => c.IdUsers).Include(c => c.IdQuizzes).FirstOrDefaultAsync();
        }

        public async Task<Guid> GetStudentClassroomId(Guid studentId)
        {
            var user = await _context.Users.Where(u => u.Id == studentId).Include(u => u.Classrooms).FirstOrDefaultAsync();

            if (user == null)
            {
                throw new Exception("Student not found");
            }

            return user.Classrooms.Select(x => x.Id).FirstOrDefault();
        }

        public async Task<List<Guid>> GetTeacherClassroomIds(Guid teacherId)
        {
            var user = await _context.Users.Where(u => u.Id == teacherId).Include(u => u.Classrooms).FirstOrDefaultAsync();

            if (user == null)
            {
                throw new Exception("Teacher not found");
            }

            return user.Classrooms.Select(x => x.Id).ToList();
        }
    }
}
