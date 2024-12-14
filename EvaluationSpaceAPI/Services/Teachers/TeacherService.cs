using AutoMapper;
using EvaluationSpaceAPI.DTOs;
using EvaluationSpaceAPI.Entities;
using EvaluationSpaceAPI.Repositories.Interfaces;
using EvaluationSpaceAPI.Services.Users;

namespace EvaluationSpaceAPI.Services.TeacherQuizzes
{
    public class TeacherService : ITeacherService
    {
        private readonly IQuizzesRepository _quizzesRepository;
        private readonly IClassroomRepository _classroomRepository;
        private readonly IResultRepository _resultRepository;
        private readonly IAnswerRepository _answerRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;
        public IMapper Mapper { get; set; }

        public TeacherService(IMapper mapper, IQuizzesRepository quizzesRepository, 
            IClassroomRepository classroomRepository, 
            IResultRepository resultRepository,
            IAnswerRepository answerRepository,
            IQuestionRepository questionRepository,
            IUserRepository userRepository,
            IUserService userService)
        {
            Mapper = mapper;
            _quizzesRepository = quizzesRepository;
            _resultRepository = resultRepository;
            _classroomRepository = classroomRepository;
            _answerRepository = answerRepository;
            _questionRepository = questionRepository;
            _userRepository = userRepository;
            _userService = userService;
        }

        public async Task<List<TeacherQuizzesListDTO>> GetTeacherQuizzesList(string teacherEmail)
        {
            var teacherId = await _userService.GetUserId(teacherEmail);

            var quizzes = (await _quizzesRepository.GetAll())
                .Where(q => q.IdTeacher == teacherId)
                .OrderBy(q => q.StartTime)
                .AsQueryable();

            return Mapper.ProjectTo<TeacherQuizzesListDTO>(quizzes).ToList();
        }

        public async Task CreateQuiz(CreateQuizDTO quiz, string teacherEmail)
        {
            var teacherId = await _userService.GetUserId(teacherEmail);

            var quizToAdd = Mapper.Map<Quiz>(quiz);
            quizToAdd.IdTeacher = teacherId;

            List<Classroom> classrooms = await _classroomRepository.GetAll();
            quizToAdd.IdClassrooms = classrooms.Where(c => quiz.ClassroomIds.Contains(c.Id)).ToList();

            await _quizzesRepository.Create(quizToAdd);
            await _quizzesRepository.SaveAsync();
        }

        public async Task DeleteQuiz(Guid quizId)
        {
            var quiz = await _quizzesRepository.GetQuiz(quizId);

            if(quiz == null)
            {
                throw new Exception("This quiz doesn't exist");
            }

            if(quiz.StartTime <= DateTime.Now)
            {
                throw new Exception("This quiz can't be deleted");
            }

            quiz.IdClassrooms.Clear();

            await _resultRepository.DeleteRange(quiz.Results);

            var answers = quiz.Questions.Select(q => q.Answers);
            foreach(var answer in answers)
            {
                await _answerRepository.DeleteRange(answer);
            }

            await _questionRepository.DeleteRange(quiz.Questions);

            await _quizzesRepository.Delete(quiz);
            await _quizzesRepository.SaveAsync();
        }

        public async Task<QuizDTO> GetQuiz(Guid quizId)
        {
            var quiz = await _quizzesRepository.GetQuiz(quizId);

            if (quiz == null)
            {
                throw new Exception("This quiz doesn't exist");
            }

            return Mapper.Map<QuizDTO>(quiz);
        }

        public async Task EditQuiz(EditQuizDTO quiz, string teacherEmail)
        {
            await DeleteQuiz(quiz.Id);

            var quizToAdd = Mapper.Map<Quiz>(quiz);

            List<Classroom> classrooms = await _classroomRepository.GetAll();
            quizToAdd.IdClassrooms = classrooms.Where(c => quiz.Classrooms.Select(c => Guid.Parse(c.Value))
                    .ToList().Contains(c.Id)).ToList();

            var teacherId = await _userService.GetUserId(teacherEmail);
            quizToAdd.IdTeacher = teacherId;

            await _quizzesRepository.Create(quizToAdd);
            await _quizzesRepository.SaveAsync();
        }

        public async Task ShowResults(Guid quizId, bool show)
        {
            var quiz = await _quizzesRepository.GetById(quizId);

            if (quiz == null)
            {
                throw new Exception("This quiz doesn't exist");
            }

            quiz.ResultsVisible = show;

            await _quizzesRepository.Update(quiz);
            await _quizzesRepository.SaveAsync();
        }

        public async Task<List<TeacherResultDTO>> GetTeacherResults(string teacherEmail)
        {
            var teacherId = await _userService.GetUserId(teacherEmail);

            var allQuizzes = await _quizzesRepository.GetAllQuizzes();
            var teacherQuizzes = allQuizzes.Where(x => x.IdTeacher.Equals(teacherId)).ToList();

            var teacherResultList = new List<TeacherResultDTO>();

            foreach (var quiz in teacherQuizzes)
            {
                foreach (var result in quiz.Results)
                {
                    var resultToAdd = new TeacherResultDTO();
                    resultToAdd.QuizTitle = quiz.Title;

                    var student = await _userRepository.GetById(result.IdStudent);
                    if (student == null)
                    {
                        throw new Exception("Student doesn't exist");
                    }
                    resultToAdd.StudentName = student.LastName + ' ' +  student.FirstName;

                    resultToAdd.Score = result.Score;
                    resultToAdd.TotalScore = quiz.TotalScore;

                    resultToAdd.ClassName = await _userRepository.GetStudentClassroom(student.Id);
                    teacherResultList.Add(resultToAdd);
                }
            }

            return teacherResultList;
        }
    }
}
