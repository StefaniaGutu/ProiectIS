using EvaluationSpaceAPI.DTOs;
using EvaluationSpaceAPI.Enums;
using EvaluationSpaceAPI.Services.TeacherQuizzes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EvaluationSpaceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherQuizzesController : Controller
    {
        private readonly ITeacherService _teacherQuizzesService;

        public TeacherQuizzesController(ITeacherService teacherQuizzesService)
        {
            _teacherQuizzesService = teacherQuizzesService;
        }

        [HttpGet]
        [Authorize]
        [Route("GetQuizzesList")]
        public async Task<IActionResult> GetQuizzesList()
        {
            try
            {
                var userEmail = User.FindFirst(ClaimTypes.Email);

                if (userEmail == null)
                {
                    return Unauthorized();
                }

                if (User.HasClaim(ClaimTypes.Role, nameof(RoleTypes.Student)))
                {
                    return Forbid();
                }

                var quizzes = await _teacherQuizzesService.GetTeacherQuizzesList(userEmail.Value);

                return Ok(quizzes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Authorize]
        [Route("CreateQuiz")]
        public async Task<IActionResult> CreateQuiz(CreateQuizDTO quiz)
        {
            try
            {
                var userEmail = User.FindFirst(ClaimTypes.Email);

                if (userEmail == null)
                {
                    return Unauthorized();
                }

                if (User.HasClaim(ClaimTypes.Role, nameof(RoleTypes.Student)))
                {
                    return Forbid();
                }

                await _teacherQuizzesService.CreateQuiz(quiz, userEmail.Value);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Authorize]
        [Route("DeleteQuiz/{id}")]
        public async Task<IActionResult> DeleteQuiz([FromRoute] Guid id)
        {
            try
            {
                if (User.HasClaim(ClaimTypes.Role, nameof(RoleTypes.Student)))
                {
                    return Forbid();
                }

                try
                {
                    await _teacherQuizzesService.DeleteQuiz(id);
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Authorize]
        [Route("GetQuiz/{quizId}")]
        public async Task<IActionResult> GetQuiz([FromRoute] Guid quizId)
        {
            try
            {
                if (User.HasClaim(ClaimTypes.Role, nameof(RoleTypes.Student)))
                {
                    return Forbid();
                }

                var quiz = await _teacherQuizzesService.GetQuiz(quizId);

                if (quiz == null)
                {
                    return BadRequest("Quiz doesn't exist");
                }

                return Ok(quiz);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Authorize]
        [Route("EditQuiz")]
        public async Task<IActionResult> EditQuiz(EditQuizDTO quiz)
        {
            try
            {
                var userEmail = User.FindFirst(ClaimTypes.Email);

                if (userEmail == null)
                {
                    return Unauthorized();
                }

                if (User.HasClaim(ClaimTypes.Role, nameof(RoleTypes.Student)))
                {
                    return Forbid();
                }

                await _teacherQuizzesService.EditQuiz(quiz, userEmail.Value);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Authorize]
        [Route("ShowResults/{quizId}")]
        public async Task<IActionResult> ShowResults([FromRoute] Guid quizId)
        {
            try
            {
                if (User.HasClaim(ClaimTypes.Role, nameof(RoleTypes.Student)))
                {
                    return Forbid();
                }

                await _teacherQuizzesService.ShowResults(quizId, true);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Authorize]
        [Route("HideResults/{quizId}")]
        public async Task<IActionResult> HideResults([FromRoute] Guid quizId)
        {
            try
            {
                if (User.HasClaim(ClaimTypes.Role, nameof(RoleTypes.Student)))
                {
                    return Forbid();
                }

                await _teacherQuizzesService.ShowResults(quizId, false);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Authorize]
        [Route("GetTeacherResults")]
        public async Task<IActionResult> GetTeacherResults()
        {
            try
            {
                var userEmail = User.FindFirst(ClaimTypes.Email);

                if (userEmail == null)
                {
                    return Unauthorized();
                }

                if (User.HasClaim(ClaimTypes.Role, nameof(RoleTypes.Student)))
                {
                    return Forbid();
                }

                var result = await _teacherQuizzesService.GetTeacherResults(userEmail.Value);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
