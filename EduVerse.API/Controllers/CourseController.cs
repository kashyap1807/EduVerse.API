using EduVerse.API.Common;
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
    [AllowAnonymous]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService courseService;
        private readonly IAzureBlobStorageService azureBlobStorageService;

        public CourseController(ICourseService courseService,IAzureBlobStorageService azureBlobStorageService)
        {
            this.courseService = courseService;
            this.azureBlobStorageService = azureBlobStorageService;
        }

        [HttpGet]
        public async Task<ActionResult<List<CourseDto>>> GetAllCoursesAsync()
        {
            var courses = await courseService.GetAllCoursesAsync();
            return Ok(courses);
        }

        [HttpGet("Category/{categoryId}")]
        public async Task<ActionResult<List<CourseDto>>> GetAllCoursesByCategoryIdAsync([FromRoute] int categoryId)
        {
            var courses = await courseService.GetAllCoursesAsync(categoryId);
            return Ok(courses);
        }

        [HttpGet("Details/{courseId}")]
        public async Task<ActionResult<CourseDetailDto>> GetCourseDetailAsync(int courseId)
        {
            var courseDetail = await courseService.GetCourseDetailAsync(courseId);
            if (courseDetail == null)
            {
                return NotFound();
            }
            return Ok(courseDetail);
        }

        [HttpPost]
        [Authorize]
        [AdminRole]
        [RequiredScope(RequiredScopesConfigurationKey = "AzureAdB2C:Scopes:Write")]
        public async Task<IActionResult> AddCourse([FromBody] CourseDetailDto courseDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await courseService.AddCourseAsync(courseDto);
            return Ok();
        }

        [HttpPut("{id}")]
        [Authorize]
        [AdminRole]
        [RequiredScope(RequiredScopesConfigurationKey = "AzureAdB2C:Scopes:Write")]
        public async Task<IActionResult> UpdateCourse(int id, [FromBody] CourseDetailDto courseDetailDto)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != courseDetailDto.CourseId)
            {
                return BadRequest("Course ID mismatch");
            }
            await courseService.UpdateCourseAsync(courseDetailDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        [AdminRole]
        [RequiredScope(RequiredScopesConfigurationKey = "AzureAdB2C:Scopes:Write")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            await courseService.DeleteCourseAsync(id);
            return NoContent();
        }

        [HttpGet("Instructors")]
        [RequiredScope(RequiredScopesConfigurationKey = "AzureAdB2C:Scopes:Read")]
        public async Task<ActionResult<List<InstructorDto>>> GetInstructors()
        {
            var instructors = await courseService.GetAllInstructorsAsync();
            return Ok(instructors);
        }

        [HttpPost("upload-thumbnail")]
        [Authorize]
        public async Task<IActionResult> UploadThumbnail(IFormFile file)
        {
            var courseId = Convert.ToInt32(Request.Form["courseId"]);
            string thumbnailUrl = null;

            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded");
            }
            var course = await courseService.GetCourseDetailAsync(courseId);
            if (course == null)
            {
                return NotFound("Course not found");
            }

            if (file != null)
            {
                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);

                    thumbnailUrl = await azureBlobStorageService.UploadAsync(stream.ToArray(), $"{courseId}_{course.Title.Trim().Replace(' ', '_')}.{file.FileName.Split('.').LastOrDefault()}", "course-preview");

                    await courseService.UpdateCourseThumbnail(thumbnailUrl, courseId);
                }
            }

            return Ok(new { message = "Thumbnail uploaded successfully", thumbnailUrl });
        }
    }
}
