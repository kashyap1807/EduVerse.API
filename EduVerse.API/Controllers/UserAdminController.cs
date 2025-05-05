using EduVerse.Core.Dtos;
using EduVerse.Service.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace EduVerse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserAdminController : ControllerBase
    {
        private readonly ICourseService courseService;

        public UserAdminController(ICourseService courseService)
        {
            this.courseService = courseService;
        }

        [HttpGet]
        [RequiredScope(RequiredScopesConfigurationKey = "AzureAdB2C:Scopes:Read")]
        public async Task<ActionResult<List<UserDto>>> GetAllUsersAsync()
        {
            var courses = await courseService.GetAllCoursesAsync();
            return Ok(courses);
        }
    }
}
