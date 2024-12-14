using EvaluationSpaceAPI.Entities;

namespace EvaluationSpaceAPI.Repositories.Interfaces
{
    public interface IClassroomRepository : IGenericRepository<Classroom>
    {
        Task<List<Classroom>> GetAllClassroomsWithDetails();
        Task<Classroom?> GetAllClassroomWithDetails(Guid id);
        Task<Guid> GetStudentClassroomId(Guid studentId);
        Task<List<Guid>> GetTeacherClassroomIds(Guid teacherId);
    }
}
