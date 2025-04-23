using EduVerse.Core.Dtos;
using EduVerse.Service.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduVerse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAdminController : ControllerBase
    {
        private readonly ICourseService courseService;

        public UserAdminController(ICourseService courseService)
        {
            this.courseService = courseService;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserDto>>> GetAllUsersAsync()
        {
            var courses = await courseService.GetAllCoursesAsync();
            return Ok(courses);
        }
    }
}
