using EvaluationSpaceAPI.Repositories.Interfaces;
using EvaluationSpaceAPI.Services.TeacherQuizzes;
using EvaluationSpaceAPI.Services.Users;
using EvaluationSpaceAPI.Entities;
using EvaluationSpaceAPI.DTOs;
using NUnit.Framework;
using AutoMapper;
using Moq;

namespace EvaluationSpaceTests.UnitTests
{
    public class TeacherServiceTests
    {
        private ITeacherService _teacherService;
        private Mock<IQuizzesRepository> _quizzesRepositoryMock;
        private Mock<IClassroomRepository> _classroomRepositoryMock;
        private Mock<IResultRepository> _resultRepositoryMock;
        private Mock<IAnswerRepository> _answerRepositoryMock;
        private Mock<IQuestionRepository> _questionRepositoryMock;
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IUserService> _userServiceMock;
        private Mock<IMapper> _mapperMock;

        private readonly string _email = "teacher@email.com";

        [SetUp]
        public void Setup()
        {
            _quizzesRepositoryMock = new Mock<IQuizzesRepository>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _questionRepositoryMock = new Mock<IQuestionRepository>();
            _resultRepositoryMock = new Mock<IResultRepository>();
            _userServiceMock = new Mock<IUserService>();
            _classroomRepositoryMock = new Mock<IClassroomRepository>();
            _answerRepositoryMock = new Mock<IAnswerRepository>();
            _mapperMock = new Mock<IMapper>();

            _userServiceMock.Setup(x => x.GetUserId(_email)).ReturnsAsync(Guid.NewGuid());

            _classroomRepositoryMock.Setup(x => x.GetAll()).ReturnsAsync(new List<Classroom>());

            _teacherService = new TeacherService(_mapperMock.Object, _quizzesRepositoryMock.Object,
                _classroomRepositoryMock.Object, _resultRepositoryMock.Object, _answerRepositoryMock.Object,
                _questionRepositoryMock.Object, _userRepositoryMock.Object, _userServiceMock.Object);
        }

        [Test]
        public async Task GetTeacherQuizzes_ValidRequest_ShouldReturnListOfQuizzes()
        {
            _quizzesRepositoryMock.Setup(x => x.GetAll()).ReturnsAsync(new List<Quiz>());

            var result = await _teacherService.GetTeacherQuizzesList(_email);
            Assert.That(result, Is.TypeOf<List<TeacherQuizzesListDTO>>());
        }

        [Test]
        public async Task CreateQuiz_ValidRequest_ShouldCreateQuiz()
        {
            _mapperMock.Setup(x => x.Map<Quiz>(It.IsAny<CreateQuizDTO>())).Returns(new Quiz());

            await _teacherService.CreateQuiz(new CreateQuizDTO(), _email);

            _quizzesRepositoryMock.Verify(x => x.Create(It.IsAny<Quiz>()), Times.Once, "Quiz was not created");
            _quizzesRepositoryMock.Verify(x => x.SaveAsync(), Times.Once, "Changes were not saved");
        }

        [Test]
        public async Task DeleteQuiz_ValidRequest_ShouldDeleteQuiz()
        {
            var quiz = new Quiz() { StartTime = DateTime.Now.AddMinutes(5) };
            _quizzesRepositoryMock.Setup(x => x.GetQuiz(It.IsAny<Guid>())).ReturnsAsync(quiz);

            await _teacherService.DeleteQuiz(Guid.NewGuid());

            _resultRepositoryMock.Verify(x => x.DeleteRange(It.IsAny<ICollection<Result>>()), Times.Once, "Quiz was not deleted");
            _questionRepositoryMock.Verify(x => x.DeleteRange(It.IsAny<ICollection<Question>>()), Times.Once, "Quiz was not deleted");
            _quizzesRepositoryMock.Verify(x => x.Delete(It.IsAny<Quiz>()), Times.Once, "Quiz was not deleted");
            _quizzesRepositoryMock.Verify(x => x.SaveAsync(), Times.Once, "Changes were not saved");
        }

        [Test]
        public async Task GetQuiz_ValidRequest_ShouldReturnQuiz()
        {
            _quizzesRepositoryMock.Setup(x => x.GetQuiz(It.IsAny<Guid>())).ReturnsAsync(new Quiz());
            _mapperMock.Setup(x => x.Map<QuizDTO>(It.IsAny<Quiz>())).Returns(new QuizDTO());

            var result = await _teacherService.GetQuiz(Guid.NewGuid());
            Assert.That(result, Is.TypeOf<QuizDTO>());
        }

        // ???
        [Test]
        public async Task EditQuiz_ValidRequest_ShouldDeleteThenCreateQuizAgain()
        {
            var quiz = new Quiz() { StartTime = DateTime.Now.AddMinutes(5) };
            _quizzesRepositoryMock.Setup(x => x.GetQuiz(It.IsAny<Guid>())).ReturnsAsync(quiz);
            _mapperMock.Setup(x => x.Map<Quiz>(It.IsAny<EditQuizDTO>())).Returns(new Quiz());

            await _teacherService.EditQuiz(new EditQuizDTO(), _email);

            _resultRepositoryMock.Verify(x => x.DeleteRange(It.IsAny<ICollection<Result>>()), Times.Once, "Quiz was not deleted");
            _questionRepositoryMock.Verify(x => x.DeleteRange(It.IsAny<ICollection<Question>>()), Times.Once, "Quiz was not deleted");
            _quizzesRepositoryMock.Verify(x => x.Delete(It.IsAny<Quiz>()), Times.Once, "Quiz was not deleted");

            _quizzesRepositoryMock.Verify(x => x.Create(It.IsAny<Quiz>()), Times.Once, "Quiz was not created");
            _quizzesRepositoryMock.Verify(x => x.SaveAsync(), Times.Exactly(2), "Changes were not saved");
        }

        [Test]
        public async Task ShowResults_ValidRequest_ShouldUpdateQuiz()
        {
            _quizzesRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync(new Quiz());

            await _teacherService.ShowResults(Guid.NewGuid(), true);

            _quizzesRepositoryMock.Verify(x => x.Update(It.IsAny<Quiz>()), Times.Once, "Quiz was not updated");
            _quizzesRepositoryMock.Verify(x => x.SaveAsync(), Times.Once, "Changes were not saved");
        }

        [Test]
        public async Task GetTeacherResults_ValidRequest_ShouldReturnListOfResults()
        {
            _quizzesRepositoryMock.Setup(x => x.GetAllQuizzes()).ReturnsAsync(new List<Quiz>());
            _userRepositoryMock.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync(new User());
            _userRepositoryMock.Setup(x => x.GetStudentClassroom(It.IsAny<Guid>())).ReturnsAsync("classroom");

            var result = await _teacherService.GetTeacherResults(_email);
            Assert.That(result, Is.TypeOf<List<TeacherResultDTO>>());
        }

    }
}
