using EvaluationSpaceAPI.DTOs;
using EvaluationSpaceAPI.Enums;
using EvaluationSpaceAPI.Services.StudentQuizzes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EvaluationSpaceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentQuizzesController : Controller
    {
        private readonly IStudentService _studentQuizzesService;

        public StudentQuizzesController(IStudentService studentQuizzesService)
        {
            _studentQuizzesService = studentQuizzesService;
        }

        [HttpGet]
        [Authorize]
        [Route("getStudentQuizzes")]
        public async Task<IActionResult> GetStudentQuizList()
        {
            try
            {
                var userEmail = User.FindFirst(ClaimTypes.Email);

                if (userEmail == null)
                {
                    return Unauthorized();
                }

                if (User.HasClaim(ClaimTypes.Role, nameof(RoleTypes.Teacher)))
                {
                    return Forbid();
                }

                var quizzes = await _studentQuizzesService.GetStudentQuizzes(userEmail.Value);

                return Ok(quizzes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Authorize]
        [Route("quizId/{quizId}")]
        public async Task<IActionResult> GetStudentQuiz([FromRoute]Guid quizId)
        {
            try
            {
                if (User.HasClaim(ClaimTypes.Role, nameof(RoleTypes.Teacher)))
                {
                    return Forbid();
                }

                var quiz = await _studentQuizzesService.GetStudentQuiz(quizId);

                return Ok(quiz);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Authorize]
        [Route("submitStudentQuiz")]
        public async Task<IActionResult> SubmitStudentQuiz(SubmitQuizDTO response)
        {
            try
            {
                var userEmail = User.FindFirst(ClaimTypes.Email);

                if (userEmail == null)
                {
                    return Unauthorized();
                }

                if (User.HasClaim(ClaimTypes.Role, nameof(RoleTypes.Teacher)))
                {
                    return Forbid();
                }

                await _studentQuizzesService.SubmitQuiz(response, userEmail.Value);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Authorize]
        [Route("getStudentResults")]
        public async Task<IActionResult> GetStudentResults()
        {
            try
            {
                var userEmail = User.FindFirst(ClaimTypes.Email);

                if (userEmail == null)
                {
                    return Unauthorized();
                }

                if (User.HasClaim(ClaimTypes.Role, nameof(RoleTypes.Teacher)))
                {
                    return Forbid();
                }

                var results = await _studentQuizzesService.GetStudentResults(userEmail.Value);

                return Ok(results);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
