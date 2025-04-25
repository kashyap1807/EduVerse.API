using EduVerse.Core.Dtos;
using EduVerse.Service.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduVerse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoRequestController : ControllerBase
    {
        private readonly IVideoRequestService service;

        public VideoRequestController(IVideoRequestService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VideoRequestDto>>> GetAllAsync()
        {
            List<VideoRequestDto> videoRequest = await service.GetAllAsync();
            if (videoRequest == null)
            {
                return NotFound();
            }
            return Ok(videoRequest);
        }
    }
}
