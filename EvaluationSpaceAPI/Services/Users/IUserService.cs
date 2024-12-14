using EvaluationSpaceAPI.DTOs;
using EvaluationSpaceAPI.Entities;

namespace EvaluationSpaceAPI.Services.Users
{
    public interface IUserService
    {
        string GenerateSalt();
        string HashPassword(string password, string salt);
        Task Register(UserRegisterDTO user);
        Task<string?> Authenticate(UserLoginDTO? user);
        Task<UserProfileDTO> GetProfile(string userEmail, string userRole);
        Task UpdateProfile(string userEmail, UserProfileDTO userProfile);
        Task DeleteUser(string userEmail);
        Task<Guid> GetUserId(string userEmail);
    }
}