using EduVerse.Core.Dtos;
using EduVerse.Service.Contract;
using EduVerse.Service.Implementation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

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

        [HttpGet("{id}")]
        //[RequiredScope(RequiredScopesConfigurationKey = "AzureAdB2C:Scopes:Read")]
        public async Task<ActionResult<VideoRequestDto>> GetById(int id)
        {
            var videoRequest = await service.GetByIdAsync(id);
            if (videoRequest == null)
            {
                return NotFound();
            }
            return Ok(videoRequest);
        }

        [HttpGet("user/{userId}")]
        //[RequiredScope(RequiredScopesConfigurationKey = "AzureAdB2C:Scopes:Read")]
        public async Task<ActionResult<IEnumerable<VideoRequestDto>>> GetByUserId(int userId)
        {
            var videoRequests = await service.GetByUserIdAsync(userId);
            return Ok(videoRequests);
        }

        [HttpPost]
        //[RequiredScope(RequiredScopesConfigurationKey = "AzureAdB2C:Scopes:Write")]
        public async Task<ActionResult<VideoRequestDto>> Create(VideoRequestDto model)
        {
            var createdVideoRequest = await service.CreateAsync(model);
            //await _videoRequestService.SendVideoRequestAckEmail(model); // we are using SQLTrigger to send email automatically
            return CreatedAtAction(nameof(GetById), new { id = createdVideoRequest.VideoRequestId }, createdVideoRequest);
        }

        [HttpPut("{id}")]
        //[RequiredScope(RequiredScopesConfigurationKey = "AzureAdB2C:Scopes:Write")]
        public async Task<IActionResult> Update(int id, VideoRequestDto model)
        {
            try
            {                
                var updatedVideoRequest = await service.UpdateAsync(id, model);
                //await _videoRequestService.SendVideoRequestAckEmail(model); // we are using SQLTrigger to send email automatically
                return Ok(updatedVideoRequest);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        //[RequiredScope(RequiredScopesConfigurationKey = "AzureAdB2C:Scopes:Write")]
        public async Task<IActionResult> Delete(int id)
        {
            await service.DeleteAsync(id);
            return NoContent();
        }
    }
}
