using AutoMapper;
using EvaluationSpaceAPI.DTOs;
using EvaluationSpaceAPI.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EvaluationSpaceAPI.Services.TeacherQuizzes
{
    public class TeacherMapper : Profile
    {
        public TeacherMapper()
        {
            CreateMap<Quiz, TeacherQuizzesListDTO>()
                .ForMember(a => a.hasStarted, a => a.MapFrom(s => s.StartTime <= DateTime.Now ? true : false));

            CreateMap<CreateQuizDTO, Quiz>()
                .ForMember(a => a.Id, a => a.MapFrom(s => Guid.NewGuid()))
                .ForMember(a => a.TotalScore, a => a.MapFrom(src => src.Questions.Select(q => q.Score).Sum()))
                .ForMember(a => a.ResultsVisible, a => a.MapFrom(s => false));

            CreateMap<CreateQuestionDTO, Question>()
                .ForMember(a => a.Id, a => a.MapFrom(s => Guid.NewGuid()));

            CreateMap<CreateAnswerDTO, Answer>()
                .ForMember(a => a.Id, a => a.MapFrom(s => Guid.NewGuid()));

            CreateMap<Quiz, QuizDTO>()
                .ForMember(a => a.Classrooms, a => a.MapFrom(s => s.IdClassrooms.Select(c => 
                    new SelectListItem { Value = c.Id.ToString(), Text = c.Name }).ToList()));
            
            CreateMap<Question, QuestionDTO>();

            CreateMap<Answer, AnswerDTO>();

            CreateMap<QuizDTO, Quiz>()
                .ForMember(a => a.TotalScore, a => a.MapFrom(src => src.Questions.Select(q => q.Score).Sum()));

            CreateMap<EditQuizDTO, Quiz>()
                .ForMember(a => a.TotalScore, a => a.MapFrom(src => src.Questions.Select(q => q.Score).Sum()));

            CreateMap<QuestionDTO, Question>();

            CreateMap<AnswerDTO, Answer>();
        }
    }
}
