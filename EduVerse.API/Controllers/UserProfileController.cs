using EduVerse.API.Dto;
using EduVerse.Data.Contract;
using EduVerse.Service.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduVerse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly IUserProfileService service;
        private readonly IAzureBlobStorageService azureBlobStorageService;

        public UserProfileController(IUserProfileService service,IAzureBlobStorageService azureBlobStorageService)
        {
            this.service = service;
            this.azureBlobStorageService = azureBlobStorageService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserProfile([FromRoute] int id)
        {
            var userIfo = await service.GetUserInfoAsync(id);
            if (userIfo == null)
            {
                return NotFound();
            }
            return Ok(userIfo);
        }

        [HttpPost("updateProfile")]
        public async Task<IActionResult> UpdateUserProfile([FromForm] UpdateUserProfileDto dto)
        {
            string pictureUrl = null;

            if(dto.Picture != null)
            {
                using(var stream = new MemoryStream())
                {
                    await dto.Picture.CopyToAsync(stream);

                    // Upload the byte array or stream to Azure Blob Storage
                    //pictureUrl = await azureBlobStorageService.UploadAsync(stream.ToArray(),
                    //    $"{dto.UserId}_profile_picture.{dto.Picture.FileName.Split('.').LastOrDefault()}");
                                        
                }

                await service.UpdateUserProfilePicture(dto.UserId, pictureUrl);
            }

            if (dto.Bio !=null)
            {
                await service.UpdateUserBio(dto.UserId, dto.Bio);
            }

            return Ok(dto);
        }
    }
}
