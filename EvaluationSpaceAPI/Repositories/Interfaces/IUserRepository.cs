using EvaluationSpaceAPI.Entities;

namespace EvaluationSpaceAPI.Repositories.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        public Task<User?> GetUserByEmail(string email);
        public Task<User?> GetUserByEmailAndHashedPassword(string email, string hash);
        Task<string?> GetStudentClassroom(Guid id);
        Task<User?> GetUserClassroomsByEmail(string email);
    }
}