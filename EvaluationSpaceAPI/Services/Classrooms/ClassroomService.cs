using AutoMapper;
using EvaluationSpaceAPI.DTOs;
using EvaluationSpaceAPI.Entities;
using EvaluationSpaceAPI.Enums;
using EvaluationSpaceAPI.Repositories.Interfaces;
using EvaluationSpaceAPI.Services.Users;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EvaluationSpaceAPI.Services.Classrooms
{
    public class ClassroomService : IClassroomService
    {
        private readonly IClassroomRepository _classroomRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;

        public IMapper Mapper { get; set; }
        public ClassroomService(IMapper mapper, IClassroomRepository classroomRepository, IUserService userService, IUserRepository userRepository)
        {
            Mapper = mapper;
            _classroomRepository = classroomRepository;
            _userService = userService;
            _userRepository = userRepository;
        }

        public async Task<List<SelectListItem>> GetSelectListItemsClassrooms()
        {
            var classrooms = await _classroomRepository.GetAll();

            return classrooms.Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString() }).ToList();
        }

        public async Task<List<SelectListItem>> GetSelectListItemsClassroomsForTeacher(string teacherEmail)
        {
            var teacherId = await _userService.GetUserId(teacherEmail);

            var allClassrooms = await _classroomRepository.GetAllClassroomsWithDetails();
            var classrooms = allClassrooms
                .Where(c => c.IdUsers.Select(u => u.Id).Contains(teacherId));

            return classrooms.Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString() }).ToList();
        }

        public async Task<StudentClassroomDTO> GetStudentClassroom(string studentEmail)
        {
            var studentId = await _userService.GetUserId(studentEmail);

            var studentClassroomId = await _classroomRepository.GetStudentClassroomId(studentId);

            var classroom = (await _classroomRepository.GetAllClassroomsWithDetails()).Where(c => c.Id == studentClassroomId).FirstOrDefault();

            if (classroom == null)
            {
                throw new Exception("Student has no classroom assigned");
            }

            var colleaguesList = new List<UserDTO>();
            var teachersList = new List<UserDTO>();

            foreach(var user in classroom.IdUsers)
            {
                if (user.Id != studentId && user.IdRole == (int)RoleTypes.Student)
                {
                    colleaguesList.Add(Mapper.Map<UserDTO>(user));
                }

                if (user.IdRole == (int)RoleTypes.Teacher)
                {
                    teachersList.Add(Mapper.Map<UserDTO>(user));
                }
            }

            var studentClassroom = new StudentClassroomDTO
            {
                Name = classroom.Name,
                Colleagues = colleaguesList,
                Teachers = teachersList
            };

            return studentClassroom;
        }

        public async Task<List<TeacherClassroomDTO>> GetTeacherClassrooms(string teacherEmail)
        {
            var teacherId = await _userService.GetUserId(teacherEmail);

            var classroomIds = await _classroomRepository.GetTeacherClassroomIds(teacherId);

            var allClassrooms = await _classroomRepository.GetAllClassroomsWithDetails();
            var classrooms = allClassrooms.Where(c => classroomIds.Contains(c.Id)).ToList();

            if (classrooms == null)
            {
                throw new Exception("Teacher has no classrooms assigned");
            }

            var result = new List<TeacherClassroomDTO>();

            foreach (var classroom in classrooms)
            {
                var studentsList = new List<UserDTO>();

                foreach (var user in classroom.IdUsers)
                {
                    if (user.IdRole == (int)RoleTypes.Student)
                    {
                        studentsList.Add(Mapper.Map<UserDTO>(user));
                    }
                }

                var teacherClassroom = new TeacherClassroomDTO()
                {
                    Id = classroom.Id,
                    Name = classroom.Name,
                    Students = studentsList
                };
                result.Add(teacherClassroom);
            }

            return result;
        }

        public async Task<TeacherClassroomDTO> GetTeacherClassroomById(string teacherEmail, Guid id)
        {
            var teacherId = await _userService.GetUserId(teacherEmail);

            var teacherClassroomIds = await _classroomRepository.GetTeacherClassroomIds(teacherId);

            var allClassrooms = await _classroomRepository.GetAllClassroomsWithDetails();
            var classroom = allClassrooms.Where(x => x.Id.Equals(id)).FirstOrDefault();

            if (classroom == null)
            {
                throw new Exception(StatusCodes.Status404NotFound.ToString());
            }

            if (!teacherClassroomIds.Contains(id))
            {
                throw new Exception(StatusCodes.Status403Forbidden.ToString());
            }

            var studentList = new List<UserDTO>();

            foreach (var user in classroom.IdUsers)
            {
                if (user.IdRole == (int)RoleTypes.Student)
                {
                    studentList.Add(Mapper.Map<UserDTO>(user));
                }
            }

            var teacherClassroom = new TeacherClassroomDTO
            {
                Id = classroom.Id,
                Name = classroom.Name,
                Students = studentList
            };

            return teacherClassroom;
        }

        public async Task UpdateTeacherClassroomById(string teacherEmail, UpdateClassroomDTO updateClassroom)
        {
            var teacherId = await _userService.GetUserId(teacherEmail);

            var teacherClassroomIds = await _classroomRepository.GetTeacherClassroomIds(teacherId);

            var classroomFromDb = await _classroomRepository.GetAllClassroomWithDetails(updateClassroom.Id);

            if (classroomFromDb == null)
            {
                throw new Exception(StatusCodes.Status404NotFound.ToString());
            }

            if (!teacherClassroomIds.Contains(classroomFromDb.Id))
            {
                throw new Exception(StatusCodes.Status403Forbidden.ToString());
            }

            // adding the teachers first
            List<User> updatedUsersList = classroomFromDb.IdUsers.Where(x => x.IdRole == (int)RoleTypes.Teacher).ToList();
            
            // removing students
            foreach (var student in classroomFromDb.IdUsers)
            {
                if (student.IdRole == (int)RoleTypes.Student)
                {
                    if (updateClassroom.StudentIds.Contains(student.Id))
                    {
                        updatedUsersList.Add(student);
                        updateClassroom.StudentIds.Remove(student.Id);
                    }
                }
            }

            // adding students
            foreach (var studentId in updateClassroom.StudentIds)
            {
                var studentClassroom = await _userRepository.GetStudentClassroom(studentId);
                if (studentClassroom == null)
                {
                    var student = await _userRepository.GetById(studentId);

                    if (student == null)
                    {
                        throw new Exception("Invalid student id");
                    }
                    if (student.IdRole == (int)RoleTypes.Student)
                    {
                        updatedUsersList.Add(student);
                    }
                    else
                    {
                        throw new Exception("Invalid student id");
                    }
                }
                else
                {
                    throw new Exception("A student can't be in more than one classroom");
                }
            }

            classroomFromDb.Name = updateClassroom.Name;
            classroomFromDb.IdUsers = updatedUsersList;
            await _classroomRepository.Update(classroomFromDb);
            await _classroomRepository.SaveAsync();
        }

        public async Task<List<UserDTO>> GetStudentsWithoutClassroom()
        {
            var allStudents = await _userRepository.GetAll();
            
            var studentList = new List<UserDTO>();
            foreach (var student in allStudents)
            {
                if (student.IdRole == (int)RoleTypes.Student)
                {
                    var classroom = await _userRepository.GetStudentClassroom(student.Id);
                    if (classroom == null)
                    {
                        studentList.Add(Mapper.Map<UserDTO>(student));
                    }
                }
            }

            return studentList;
        }
    }
}
