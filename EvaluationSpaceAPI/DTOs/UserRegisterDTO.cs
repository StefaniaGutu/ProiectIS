using EvaluationSpaceAPI.Entities;
using System.ComponentModel.DataAnnotations;

namespace EvaluationSpaceAPI.DTOs
{
    public class UserRegisterDTO
    {

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;

        public int IdRole { get; set; }

        public List<Guid> ClassroomIds { get; set; } = new List<Guid>();
    }
}
