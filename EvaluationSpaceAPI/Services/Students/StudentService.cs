using EvaluationSpaceAPI.DTOs;
using EvaluationSpaceAPI.Entities;
using EvaluationSpaceAPI.Repositories.Interfaces;
using EvaluationSpaceAPI.Services.Users;

namespace EvaluationSpaceAPI.Services.StudentQuizzes
{
    public class StudentService : IStudentService
    {
        private readonly IQuizzesRepository _quizzesRepository;
        private readonly IUserRepository _userRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly IResultRepository _resultRepository;
        private readonly IUserService _userService;

        public StudentService(IQuizzesRepository quizzesRepository, IUserRepository userRepository,
            IQuestionRepository questionRepository, IResultRepository resultRepository, IUserService userService)
        {
            _quizzesRepository = quizzesRepository;
            _userRepository = userRepository;
            _questionRepository = questionRepository;
            _resultRepository = resultRepository;
            _userService = userService;
        }
        public async Task<List<QuizStudentTakenDTO>> GetStudentQuizzes(string userEmail)
        {
            var student = await _userRepository.GetUserClassroomsByEmail(userEmail);

            if (student == null)
            {
                throw new Exception("Student with this email doesn't exist");
            }

            Guid classroomId;
            Classroom classroom = new();
            try
            {
                classroom = student.Classrooms.Single();
                classroomId = classroom.Id;
            }
            catch
            {
                throw new Exception("A student can only have one classroom");
            }

            var quizzes = await _quizzesRepository.GetAllQuizzes();
            List<Quiz> quizList = new List<Quiz>();
            
            foreach (var quiz in quizzes)
            {
                if (quiz.IdClassrooms.Contains(classroom)) quizList.Add(quiz);
            }

            List<QuizStudentTakenDTO> resultsList = new List<QuizStudentTakenDTO>();

            foreach (var quiz in quizList)
            {
                var quizStudentTakenDTO = new QuizStudentTakenDTO(quiz);
                foreach(var result in quiz.Results)
                {
                    if(result.IdStudent == student.Id)
                    {
                        quizStudentTakenDTO.IsTaken = true;
                        quizStudentTakenDTO.Result = result.Score;
                        break;
                    }
                }
                resultsList.Add(quizStudentTakenDTO);
            }

            return resultsList;
        }

        public async Task<QuizStudentDTO?> GetStudentQuiz(Guid quizId)
        {
            var quiz = await _quizzesRepository.GetQuiz(quizId);
            if (quiz == null)
            {
                throw new Exception("Quiz doesn't exist");
            }
            return new QuizStudentDTO(quiz);
        }

        public async Task SubmitQuiz(SubmitQuizDTO response, string studentEmail)
        {
            var quizFromDb = await _quizzesRepository.GetQuiz(response.IdQuiz);
            var studentId = await _userService.GetUserId(studentEmail);
            if (quizFromDb == null)
            {
                throw new Exception("Quiz doesn't exist");
            }
            int totalScore = 0;

            foreach (var questionAnswer in response.QuestionAnswers)
            {
                var question = await _questionRepository.GetQuestion(questionAnswer.IdQuestion);
                if (question == null)
                {
                    throw new Exception("Question not found");
                }
                var correctAnswers = question.Answers.Where(x => x.IsCorrect).Select(x => x.Id).ToList();

                var set1 = new HashSet<Guid>(questionAnswer.Answers);
                var set2 = new HashSet<Guid>(correctAnswers);
                if (set1.SetEquals(set2)){
                    totalScore+=question.Score;
                }
            }

            Result result = new Result()
            {
                Id = Guid.NewGuid(),
                IdStudent = studentId,
                IdQuiz = response.IdQuiz,
                Score = totalScore,
                Date = DateTime.Now
            };

            await _resultRepository.Create(result);
            await _resultRepository.SaveAsync();

        }

        public async Task<List<StudentResultDTO>> GetStudentResults(string studentEmail)
        {
            var studentId = await _userService.GetUserId(studentEmail);

            var results = (await _resultRepository.GetAll()).Where(r => r.IdStudent == studentId).OrderBy(r => r.Date).ToList();

            var resultsList = new List<StudentResultDTO>();

            foreach (var result in results)
            {
                var quiz = await _quizzesRepository.GetQuiz(result.IdQuiz);
                var teacher = await _userRepository.GetById(quiz.IdTeacher);

                resultsList.Add(new StudentResultDTO
                {
                    QuizTitle = quiz.Title,
                    TeacherName = teacher.FirstName + " " + teacher.LastName,
                    ResultsVisible = quiz.ResultsVisible,
                    Score = quiz.ResultsVisible? result.Score : null,
                    TotalScore = (int)quiz.TotalScore
                });
            }

            return resultsList;
        }
    }
}
