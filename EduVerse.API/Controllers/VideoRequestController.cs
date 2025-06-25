using EduVerse.API.Common;
using EduVerse.Core.Dtos;
using EduVerse.Service.Contract;
using EduVerse.Service.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace EduVerse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class VideoRequestController : ControllerBase
    {
        private readonly IVideoRequestService service;
        private readonly IUserClaims userClaims;

        public VideoRequestController(IVideoRequestService service, IUserClaims userClaims)
        {
            this.service = service;
            this.userClaims = userClaims;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VideoRequestDto>>> GetAllAsync()
        {
            List<VideoRequestDto> videoRequests;
            //var userRoles = userClaims.GetUserRoles();
            //if (userRoles.Contains("Admin"))
            //{
                videoRequests = await service.GetAllAsync();
            //}
            //else
            //{
            //    var videoRequest = await service.GetByUserIdAsync(userClaims.GetUserId());
            //    videoRequests = videoRequest.ToList();
            //}
            return Ok(videoRequests);
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
