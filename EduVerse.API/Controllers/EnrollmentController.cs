using EduVerse.Core.Dtos;
using EduVerse.Service.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduVerse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        private readonly IEnrollmentService _enrollmentService;
        public EnrollmentController(IEnrollmentService enrollmentService)
        {
            _enrollmentService = enrollmentService;
        }

        [HttpPost]
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
