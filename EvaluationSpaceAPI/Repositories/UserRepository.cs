using EvaluationSpaceAPI.EFContext;
using EvaluationSpaceAPI.Entities;
using EvaluationSpaceAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EvaluationSpaceAPI.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(EvaluationSpaceDbContext context) : base(context) { }
        public async Task<User?> GetUserByEmail (string email)
        {
            return await _context.Users.Where(a => a.Email == email && !a.IsDeleted).FirstOrDefaultAsync();
        }
        public async Task<User?> GetUserClassroomsByEmail(string email)
        {
            return await _context.Users.Include(x => x.Classrooms).Where(a => a.Email == email && !a.IsDeleted).FirstOrDefaultAsync();
        }
        public async Task<User?> GetUserByEmailAndHashedPassword(string email, string hash)
        {
            return await _context.Users.Where(a => a.Email == email &&
            a.Password == hash && !a.IsDeleted).FirstOrDefaultAsync();
        }

        public async Task<string?> GetStudentClassroom(Guid id)
        {
            var student = await _context.Users.Include(u => u.Classrooms).Where(u => u.Id == id).FirstOrDefaultAsync();
            if (student == null)
            {
                return null;
            }
            var classroom = student.Classrooms.FirstOrDefault();
            if (classroom == null)
            {
                return null;
            }

            return classroom.Name;
        }
    }
}
