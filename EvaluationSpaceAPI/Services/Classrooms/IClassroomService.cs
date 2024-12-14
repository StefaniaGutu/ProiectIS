using EvaluationSpaceAPI.DTOs;
using EvaluationSpaceAPI.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EvaluationSpaceAPI.Services.Classrooms
{
    public interface IClassroomService
    {
        Task<List<SelectListItem>> GetSelectListItemsClassrooms();

        Task<List<SelectListItem>> GetSelectListItemsClassroomsForTeacher(string teacherEmail);

        Task<StudentClassroomDTO> GetStudentClassroom(string studentEmail);
        Task<List<UserDTO>> GetStudentsWithoutClassroom();
        Task<TeacherClassroomDTO> GetTeacherClassroomById(string teacherEmail, Guid id);
        Task<List<TeacherClassroomDTO>> GetTeacherClassrooms(string teacherEmail);
        Task UpdateTeacherClassroomById(string teacherEmail, UpdateClassroomDTO updateClassroom);
    }
}
