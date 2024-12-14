using AutoMapper;
using EvaluationSpaceAPI.DTOs;
using EvaluationSpaceAPI.Entities;

namespace EvaluationSpaceAPI.Services.Classrooms
{
    public class ClassroomMapper : Profile
    {
        public ClassroomMapper() 
        {
            CreateMap<User, UserDTO>();
        }
    }
}
