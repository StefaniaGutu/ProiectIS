using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using EvaluationSpaceAPI.DTOs;
using System.Security.Claims;
using EvaluationSpaceAPI.Services.Users;

namespace EvaluationSpaceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Register")]
        public async Task<IActionResult> PostUser(UserRegisterDTO user)
        {
            try
            {
                await _userService.Register(user);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Login")]
        public async Task<IActionResult> Authenticate(UserLoginDTO user)
        {
            String? token;
            try
            {
                token = await _userService.Authenticate(user);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            if (token == null)
            {
                return Unauthorized();
            }

            return Ok(token);
        }

        [HttpGet]
        [Authorize]
        [Route("Profile")]
        public async Task<IActionResult> ViewProfile()
        {
            try
            {
                var userEmail = User.FindFirst(ClaimTypes.Email);
                var userRole = User.FindFirst(ClaimTypes.Role);

                if (userEmail == null || userRole == null)
                {
                    return Unauthorized();
                }

                var userProfile = await _userService.GetProfile(userEmail.Value, userRole.Value);

                return Ok(userProfile);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Authorize]
        [Route("EditProfile")]
        public async Task<IActionResult> EditProfile(UserProfileDTO userProfile)
        {
            try
            {
                var userEmail = User.FindFirst(ClaimTypes.Email);

                if (userEmail == null)
                {
                    return Unauthorized();
                }

                await _userService.UpdateProfile(userEmail.Value, userProfile);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok();
        }

        [HttpDelete]
        [Authorize]
        [Route("DeleteAccount")]
        public async Task<IActionResult> DeleteAccount()
        {
            try
            {
                var userEmail = User.FindFirst(ClaimTypes.Email);

                if (userEmail == null)
                {
                    return Unauthorized();
                }

                await _userService.DeleteUser(userEmail.Value);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok();
        }
    }
}
