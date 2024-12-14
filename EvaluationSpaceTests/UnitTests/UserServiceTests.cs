using EvaluationSpaceAPI.Repositories.Interfaces;
using EvaluationSpaceAPI.Services.Users;
using Microsoft.Extensions.Configuration;
using EvaluationSpaceAPI.Entities;
using EvaluationSpaceAPI.DTOs;
using NUnit.Framework;
using Moq;

namespace EvaluationSpaceTests.UnitTests
{
    public class UserServiceTests
    {
        private IUserService _userService;
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IClassroomRepository> _classroomRepositoryMock;
        private Mock<IRoleRepository> _roleRepositoryMock;
        private Mock<IConfiguration> _configurationMock;

        private User _user;
        private UserRegisterDTO _userRegister;
        private UserProfileDTO _userProfile;

        [SetUp]
        public void Setup()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _classroomRepositoryMock = new Mock<IClassroomRepository>();
            _roleRepositoryMock = new Mock<IRoleRepository>();
            _configurationMock = new Mock<IConfiguration>();

            _userRegister = new UserRegisterDTO()
            {
                FirstName = "user",
                LastName = "user",
                Email = "user@yahoo.com",
                Password = "Parola123!",
                IdRole = 2
            };
            _user = new User()
            {
                Id = Guid.NewGuid(),
                FirstName = _userRegister.FirstName,
                LastName = _userRegister.LastName,
                Email = _userRegister.Email,
            };
            _userProfile = new UserProfileDTO()
            {
                FirstName = _user.FirstName,
                LastName = _user.LastName,
                Email = _user.Email
            };


            Role teacher = new Role() { Id = 1, Name = "Teacher" };
            Role student = new Role() { Id = 2, Name = "Student" };
            List<Role> roles = new List<Role> { teacher, student };
            _roleRepositoryMock.Setup(x => x.GetAll()).ReturnsAsync(roles);

            _userRepositoryMock.Setup(x => x.GetUserByEmail(It.IsAny<string>())).ReturnsAsync(_user);
            _userRepositoryMock.Setup(x => x.GetUserByEmailAndHashedPassword(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync((_user));
            _userRepositoryMock.Setup(x => x.GetStudentClassroom(_user.Id)).ReturnsAsync("classroom");

            _userService = new UserService(_userRepositoryMock.Object, _classroomRepositoryMock.Object,
                _roleRepositoryMock.Object, _configurationMock.Object);
        }

        [Test]
        public async Task RegisterUser_ValidRequest_ShouldCreateUser()
        {

            _userRepositoryMock.Setup(x => x.GetUserByEmail(It.IsAny<string>())).ReturnsAsync((User?)null);
            _classroomRepositoryMock.Setup(x => x.GetAll()).ReturnsAsync(new List<Classroom>());

            await _userService.Register(_userRegister);

            _userRepositoryMock.Verify(x => x.Create(It.IsAny<User>()), Times.Once, "User was not created");
            _userRepositoryMock.Verify(x => x.SaveAsync(), Times.Once, "User was not saved");
        }

        [Test]
        public async Task GetProfile_ValidRequest_ShouldReturnUserProfile()
        {
            var result = await _userService.GetProfile(_user.Email, "Student");

            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<UserProfileDTO>());
        }

        [Test]
        public async Task UpdateProfile_ValidRequest_ShouldUpdateUserProfile()
        {
            await _userService.UpdateProfile(_user.Email, _userProfile);

            _userRepositoryMock.Verify(x => x.Update(It.IsAny<User>()), Times.Once, "User was not updated");
            _userRepositoryMock.Verify(x => x.SaveAsync(), Times.Once, "Changes were not saved");
        }

        [Test]
        public async Task DeleteUser_ValidRequest_ShouldSoftDeleteUser()
        {
            await _userService.DeleteUser(_user.Email);

            _userRepositoryMock.Verify(x => x.Update(It.IsAny<User>()), Times.Once, "User was not updated");
            _userRepositoryMock.Verify(x => x.SaveAsync(), Times.Once, "Changes were not saved");
        }

        [Test]
        public async Task GetUserId_ValidRequest_ShouldReturnUserId()
        {
            var result = await _userService.GetUserId(_user.Email);

            Assert.That(result, Is.TypeOf<Guid>());
        }
    }
}