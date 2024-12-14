using EvaluationSpaceAPI.DTOs;
using EvaluationSpaceAPI.Entities;
using EvaluationSpaceAPI.Enums;
using EvaluationSpaceAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace EvaluationSpaceAPI.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IClassroomRepository _classroomRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IConfiguration _configuration;
        public UserService(IUserRepository userRepository, IClassroomRepository classroomRepository, IRoleRepository roleRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _classroomRepository = classroomRepository;
            _roleRepository = roleRepository;
            _configuration = configuration;
        }

        public async Task Register(UserRegisterDTO user)
        {
            if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
            {
                throw new Exception("Must enter an email and password");
            }

            if (string.IsNullOrEmpty(user.FirstName))
            {
                throw new Exception("First name field is required");
            }

            if (string.IsNullOrEmpty(user.LastName))
            {
                throw new Exception("Last name field is required");
            }

            if (user.IdRole != 1 && user.IdRole != 2)
            {
                throw new Exception("Role doesn't exist");
            }

            if (!user.Email.Contains("@"))
            {
                throw new Exception("Invalid email format");
            }

            if (user.Password.Length < 10)
            {
                throw new Exception("Password must be 10 characters or longer");
            }

            var userInDb = await _userRepository.GetUserByEmail(user.Email);

            if (userInDb != null)
            {
                throw new Exception("User with this email already exists");
            }

            Guid guid = Guid.NewGuid();
            string salt = GenerateSalt();
            string hashedPassword = HashPassword(user.Password, salt);
            var allRoles = await _roleRepository.GetAll();
            var role = allRoles.Where(a => a.Id == user.IdRole).FirstOrDefault() ?? new Role();
            List<Classroom> classrooms = await _classroomRepository.GetAll();

            User newUser = new User
            {
                Id = guid,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = hashedPassword,
                Salt = salt,
                IdRole = user.IdRole,
                IdRoleNavigation = role,
                Classrooms = classrooms.Where(c => user.ClassroomIds.Contains(c.Id)).ToList(),
                IsDeleted = false
            };

            await _userRepository.Create(newUser);
            await _userRepository.SaveAsync();
        }
        public string HashPassword(string password, string salt)
        {
            byte[] saltByte = Convert.FromBase64String(salt);

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: saltByte,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 32));

            return hashed;
        }
        public string GenerateSalt()
        {
            byte[] salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            return Convert.ToBase64String(salt);
        }
        public async Task<string?> Authenticate(UserLoginDTO? user)
        {
            if (user == null || user.Email == null || user.Password == null
                || user.Email == "" || user.Password == "")
            {
                throw new Exception("Must enter a email and password");
            }

            var userInDb = await _userRepository.GetUserByEmail(user.Email);
            if (userInDb == null)
            {
                throw new Exception("User doesn't exist");
            }

            string salt = userInDb.Salt;
            string hashedPassword;
            if (salt != null)
            {
                hashedPassword = HashPassword(user.Password, salt);
            }
            else return null;

            userInDb = await _userRepository.GetUserByEmailAndHashedPassword(user.Email, hashedPassword);

            if (userInDb == null)
            {
                throw new Exception("Incorrect password");
            }

            var allRoles = await _roleRepository.GetAll();
            Role role = allRoles.Where(a => a.Id == userInDb.IdRole).FirstOrDefault() ?? new Role();

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, userInDb.Email),
                    new Claim(ClaimTypes.Role, role.Name.ToString()) //Returns the name field of the entry in the db
                }),
                Expires = DateTime.UtcNow.AddMinutes(20),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey),
                SecurityAlgorithms.HmacSha256Signature),
                Issuer = _configuration["JWT:Issuer"],
                Audience = _configuration["JWT:Audience"]
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token).ToString();
        }

        public async Task<UserProfileDTO> GetProfile(string userEmail, string userRole)
        {
            var user = await _userRepository.GetUserByEmail(userEmail);

            var userProfile = new UserProfileDTO
            {
                Email = userEmail,
                FirstName = user.FirstName,
                LastName = user.LastName
            };

            if (userRole == nameof(RoleTypes.Student))
            {
                userProfile.StudentClassroom = await _userRepository.GetStudentClassroom(user.Id);
            }

            return userProfile;
        }

        public async Task UpdateProfile(string userEmail, UserProfileDTO userProfile)
        {
            if (userProfile == null || userProfile.Email == "" || userProfile.Email == null)
            {
                throw new Exception("Must enter an email");
            }

            if (string.IsNullOrEmpty(userProfile.FirstName))
            {
                throw new Exception("First name field is required");
            }

            if (string.IsNullOrEmpty(userProfile.LastName))
            {
                throw new Exception("Last name field is required");
            }

            if (!userProfile.Email.Contains("@"))
            {
                throw new Exception("Invalid email format");
            }

            var userInDb = await _userRepository.GetUserByEmail(userEmail);

            userInDb.FirstName = userProfile.FirstName;
            userInDb.LastName = userProfile.LastName;

            await _userRepository.Update(userInDb);
            await _userRepository.SaveAsync();
        }

        public async Task DeleteUser(string userEmail)
        {
            var userToDelete = await _userRepository.GetUserByEmail(userEmail);

            userToDelete.IsDeleted = true;

            await _userRepository.Update(userToDelete);
            await _userRepository.SaveAsync();
        }

        public async Task<Guid> GetUserId(string userEmail)
        {
            return (await _userRepository.GetUserByEmail(userEmail)).Id;
        }
    }
}
