using EduVerse.Core.Dtos;
using EduVerse.Service.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduVerse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class EnrollmentController : ControllerBase
    {
        private readonly IEnrollmentService _enrollmentService;
        public EnrollmentController(IEnrollmentService enrollmentService)
        {
            _enrollmentService = enrollmentService;
        }

        [HttpPost]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EnrollmentDto))]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //[ProducesResponseType(StatusCodes.Status403Forbidden)]
        //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> AddEnrollmentAsync([FromBody] EnrollmentDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Invalid enrollment data.");
            }

            var enrollment = await _enrollmentService.GetEnrollmentByUserIdAsync(dto.UserId);
            if (enrollment != null && enrollment.FirstOrDefault(e => e.CourseId == dto.CourseId) != null)
            {
                return BadRequest("Enrollment to this course already exists!");
            }
            var enrolledCourse = await _enrollmentService.AddEnrollmentAsync(dto);
            return Ok(enrolledCourse);
        }

        [HttpGet("{id}")]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EnrollmentDto))]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //[ProducesResponseType(StatusCodes.Status403Forbidden)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetEnrollmentByIdAsync(int id)
        {
            var enrollment = await _enrollmentService.GetEnrollmentByIdAsync(id);
            if (enrollment == null)
            {
                return NotFound();
            }
            return Ok(enrollment);
        }

        [HttpGet("user/{userId}")]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<EnrollmentDto>))]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //[ProducesResponseType(StatusCodes.Status403Forbidden)]
        //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetEnrollmentByUserIdAsync(int userId)
        {
            var enrollment = await _enrollmentService.GetEnrollmentByUserIdAsync(userId);
            if (enrollment == null)
            {
                return NotFound();
            }
            return Ok(enrollment);
        }
    }
}
