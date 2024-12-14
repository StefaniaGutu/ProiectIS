using EvaluationSpaceAPI.Repositories.Interfaces;
using EvaluationSpaceAPI.Services.Classrooms;
using EvaluationSpaceAPI.Services.Users;
using NUnit.Framework;
using AutoMapper;
using Moq;
using EvaluationSpaceAPI.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using EvaluationSpaceAPI.DTOs;

namespace EvaluationSpaceTests.UnitTests
{
    public class ClassroomServiceTests
    {
        private IClassroomService _classroomService;
        private Mock<IClassroomRepository> _classroomRepositoryMock;
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IUserService> _userServiceMock;
        private Mock<IMapper> _mapperMock;

        private readonly string _email = "teacher@email.com";

        [SetUp]
        public void Setup()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _userServiceMock = new Mock<IUserService>();
            _classroomRepositoryMock = new Mock<IClassroomRepository>();
            _mapperMock = new Mock<IMapper>();

            _userServiceMock.Setup(x => x.GetUserId(_email)).ReturnsAsync(Guid.NewGuid());

            _classroomRepositoryMock.Setup(x => x.GetAll()).ReturnsAsync(new List<Classroom> { new Classroom(), new Classroom() });
            _classroomRepositoryMock.Setup(x => x.GetAllClassroomsWithDetails()).ReturnsAsync(new List<Classroom>());

            _classroomService = new ClassroomService(_mapperMock.Object, _classroomRepositoryMock.Object,
                _userServiceMock.Object, _userRepositoryMock.Object);
        }

        [Test]
        public async Task GetSelectListItemsClassrooms_ValidRequest_ShouldReturnListOfClassrooms()
        {
            var result = await _classroomService.GetSelectListItemsClassrooms();
            Assert.That(result, Is.TypeOf<List<SelectListItem>>());
        }

        [Test]
        public async Task GetSelectListItemsClassroomsForTeacher_ValidRequest_ShouldReturnListOfClassrooms()
        {
            
            var result = await _classroomService.GetSelectListItemsClassroomsForTeacher(_email);
            Assert.That(result, Is.TypeOf<List<SelectListItem>>());
        }

        [Test]
        public async Task GetStudentClassroom_ValidRequest_ShouldReturnListOfClassrooms()
        {
            var guid = Guid.NewGuid();
            _classroomRepositoryMock.Setup(x => x.GetStudentClassroomId(It.IsAny<Guid>())).ReturnsAsync(guid);

            var classroom = new Classroom() { Id = guid };
            _classroomRepositoryMock.Setup(x => x.GetAllClassroomsWithDetails()).ReturnsAsync(new List<Classroom> { classroom });

            var result = await _classroomService.GetStudentClassroom("student@email.com");
            Assert.That(result, Is.TypeOf<StudentClassroomDTO>());
        }


        [Test]
        public async Task GetTeacherClassrooms_ValidRequest_ShouldReturnListOfClassrooms()
        {
            var guid = Guid.NewGuid();
            _classroomRepositoryMock.Setup(x => x.GetTeacherClassroomIds(It.IsAny<Guid>())).ReturnsAsync(new List<Guid> { guid });

            var classroom = new Classroom() { Id = guid };
            _classroomRepositoryMock.Setup(x => x.GetAllClassroomsWithDetails()).ReturnsAsync(new List<Classroom> { classroom });

            var result = await _classroomService.GetTeacherClassrooms(_email);
            Assert.That(result, Is.TypeOf<List<TeacherClassroomDTO>>());
        }
        
        [Test]
        public async Task GetTeacherClassroomsById_ValidRequest_ShouldReturnListOfClassrooms()
        {
            var guid = Guid.NewGuid();
            _classroomRepositoryMock.Setup(x => x.GetTeacherClassroomIds(It.IsAny<Guid>())).ReturnsAsync(new List<Guid> { guid });

            var classroom = new Classroom() { Id = guid };
            _classroomRepositoryMock.Setup(x => x.GetAllClassroomsWithDetails()).ReturnsAsync(new List<Classroom> { classroom });

            var result = await _classroomService.GetTeacherClassroomById(_email, guid);
            Assert.That(result, Is.TypeOf<TeacherClassroomDTO>());
        }

        [Test]
        public async Task UpdateTeacherClassroomById_ValidRequest_ShouldReturnListOfClassrooms()
        {
            var guid = Guid.NewGuid();
            _classroomRepositoryMock.Setup(x => x.GetTeacherClassroomIds(It.IsAny<Guid>())).ReturnsAsync(new List<Guid> { guid });

            var classroom = new Classroom() { Id = guid };
            _classroomRepositoryMock.Setup(x => x.GetAllClassroomWithDetails(guid)).ReturnsAsync(classroom);

            await _classroomService.UpdateTeacherClassroomById(_email, new UpdateClassroomDTO() { Id = guid, StudentIds = new List<Guid>() });

            _classroomRepositoryMock.Verify(x => x.Update(It.IsAny<Classroom>()), Times.Once, "Classroom was not updated");
            _classroomRepositoryMock.Verify(x => x.SaveAsync(), Times.Once, "Changes were not saved");
        }

        [Test]
        public async Task GetStudentsWithoutClassroom_ValidRequest_ShouldReturnListOfClassrooms()
        {
            _userRepositoryMock.Setup(x => x.GetAll()).ReturnsAsync(new List<User>());

            var result = await _classroomService.GetStudentsWithoutClassroom();

            Assert.That(result, Is.TypeOf<List<UserDTO>>());
        }
    }
}
