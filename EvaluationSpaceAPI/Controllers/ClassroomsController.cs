using EvaluationSpaceAPI.DTOs;
using EvaluationSpaceAPI.Enums;
using EvaluationSpaceAPI.Services.Classrooms;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EvaluationSpaceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassroomsController : ControllerBase
    {
        private readonly IClassroomService _classroomService;
        public ClassroomsController(IClassroomService classroomService)
        {
            _classroomService = classroomService;
        }

        [HttpGet]
        [Route("SelectListClassrooms")]
        public async Task<IActionResult> GetSelectListClassrooms()
        {
            try
            {
                var classrooms = await _classroomService.GetSelectListItemsClassrooms();

                return Ok(classrooms);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Authorize]
        [Route("SelectListTeacherClassrooms")]
        public async Task<IActionResult> GetSelectListTeacherClassrooms()
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

                var classrooms = await _classroomService.GetSelectListItemsClassroomsForTeacher(userEmail.Value);

                return Ok(classrooms);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Authorize]
        [Route("GetStudentClassroom")]
        public async Task<IActionResult> GetStudentClassroom()
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

                var classroom = await _classroomService.GetStudentClassroom(userEmail.Value);

                return Ok(classroom);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Authorize]
        [Route("GetTeacherClassrooms")]
        public async Task<IActionResult> GetTeacherClassrooms()
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

                var classrooms = await _classroomService.GetTeacherClassrooms(userEmail.Value);

                return Ok(classrooms);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Authorize]
        [Route("GetTeacherClassroom/{id}")]
        public async Task<IActionResult> GetTeacherClassroom([FromRoute] Guid id)
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

                var classroom = await _classroomService.GetTeacherClassroomById(userEmail.Value, id);

                return Ok(classroom);
            }
            catch (Exception ex)
            {
                if (ex.Message == StatusCodes.Status404NotFound.ToString())
                {
                    return NotFound("No class found with this id");
                }
                if (ex.Message == StatusCodes.Status403Forbidden.ToString())
                {
                    // teacher is not assigned to this classroom
                    return Forbid();
                }
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Authorize]
        [Route("EditTeacherClassroom")]
        public async Task<IActionResult> UpdateTeacherClassroom([FromBody] UpdateClassroomDTO classroom)
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

                await _classroomService.UpdateTeacherClassroomById(userEmail.Value, classroom);

                return Ok();
            }
            catch (Exception ex)
            {
                if (ex.Message == StatusCodes.Status404NotFound.ToString())
                {
                    return NotFound("No class found with this id");
                }
                if (ex.Message == StatusCodes.Status403Forbidden.ToString())
                {
                    // teacher is not assigned to this classroom
                    return Forbid();
                }
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Authorize]
        [Route("GetStudentsWithoutClassroom")]
        public async Task<IActionResult> GetStudentsWithoutClassroom()
        {
            try
            {
                if (User.HasClaim(ClaimTypes.Role, nameof(RoleTypes.Student)))
                {
                    return Forbid();
                }

                var students = await _classroomService.GetStudentsWithoutClassroom();

                return Ok(students);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
