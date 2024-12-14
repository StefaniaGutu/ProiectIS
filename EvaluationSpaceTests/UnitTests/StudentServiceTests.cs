using EvaluationSpaceAPI.DTOs;
using EvaluationSpaceAPI.Entities;
using EvaluationSpaceAPI.Repositories.Interfaces;
using EvaluationSpaceAPI.Services.StudentQuizzes;
using EvaluationSpaceAPI.Services.Users;
using Moq;
using NUnit.Framework;

namespace EvaluationSpaceTests.UnitTests
{
    public class StudentServiceTests
    {
        private IStudentService _studentService;
        private Mock<IQuizzesRepository> _quizzesRepositoryMock;
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IQuestionRepository> _questionRepositoryMock;
        private Mock<IResultRepository> _resultRepositoryMock;
        private Mock<IUserService> _userServiceMock;

        private string _email = "user@email.com";
        private User _user;

        [SetUp]
        public void Setup()
        {
            _quizzesRepositoryMock = new Mock<IQuizzesRepository>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _questionRepositoryMock = new Mock<IQuestionRepository>();
            _resultRepositoryMock = new Mock<IResultRepository>();
            _userServiceMock = new Mock<IUserService>();

            _user = new User()
            {
                Id = Guid.NewGuid(),
                FirstName = "user",
                LastName = "user",
                Email = _email,
                IdRole = 2,
                Classrooms = new List<Classroom> { new Classroom() }
            };

            _studentService = new StudentService(_quizzesRepositoryMock.Object, _userRepositoryMock.Object,
                _questionRepositoryMock.Object, _resultRepositoryMock.Object, _userServiceMock.Object);
        }

        [Test]
        public async Task GetStudentQuizzes_ValidRequest_ShouldReturnListOfQuizzes()
        {
            _userRepositoryMock.Setup(x => x.GetUserClassroomsByEmail(_email)).ReturnsAsync(_user);
            _quizzesRepositoryMock.Setup(x => x.GetAllQuizzes()).ReturnsAsync(new List<Quiz>());

            var result = await _studentService.GetStudentQuizzes(_email);

            Assert.That(result, Is.TypeOf<List<QuizStudentTakenDTO>>());
        }

        [Test]
        public async Task GetStudentQuiz_ValidRequest_ShouldReturnQuiz()
        {
            _quizzesRepositoryMock.Setup(x => x.GetQuiz(It.IsAny<Guid>())).ReturnsAsync(new Quiz());

            var result = await _studentService.GetStudentQuiz(Guid.NewGuid());

            Assert.That(result, Is.TypeOf<QuizStudentDTO>());
        }

        [Test]
        public async Task SubmitQuiz_ValidRequest_ShouldSaveQuizResult()
        {
            _quizzesRepositoryMock.Setup(x => x.GetQuiz(It.IsAny<Guid>())).ReturnsAsync(new Quiz());
            _userServiceMock.Setup(x => x.GetUserId(_email)).ReturnsAsync(_user.Id);
            _questionRepositoryMock.Setup(x => x.GetQuestion(Guid.NewGuid())).ReturnsAsync(new Question());

            var quizResponse = new SubmitQuizDTO() { QuestionAnswers = new List<SubmitQuestionAnswerDTO>() };

            await _studentService.SubmitQuiz(quizResponse, _email);

            _resultRepositoryMock.Verify(x => x.Create(It.IsAny<Result>()), Times.Once, "Result was not created");
            _resultRepositoryMock.Verify(x => x.SaveAsync(), Times.Once, "Changes were not saved");
        }

        [Test]
        public async Task GetStudentResult_ValidRequest_ShouldReturnResult()
        {
            _userServiceMock.Setup(x => x.GetUserId(_email)).ReturnsAsync(_user.Id);
            _resultRepositoryMock.Setup(x => x.GetAll()).ReturnsAsync(new List<Result>());
            _quizzesRepositoryMock.Setup(x => x.GetQuiz(It.IsAny<Guid>())).ReturnsAsync(new Quiz());
            _userRepositoryMock.Setup(x => x.GetById(Guid.NewGuid())).ReturnsAsync(new User());

            var result = await _studentService.GetStudentResults(_email);

            Assert.That(result, Is.TypeOf<List<StudentResultDTO>>());
        }
    }
}
