using EduVerse.Core.Dtos;
using EduVerse.Service.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduVerse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IEmailNotification emailNotification;

        public ContactController(IEmailNotification emailNotification)
        {
            this.emailNotification = emailNotification;
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody] ContactMessageDto contactMessageDto)
        {
            if(contactMessageDto == null)
            {
                return BadRequest("Contact message cannot be null");
            }

            await emailNotification.SendEmailForContactUs(contactMessageDto);

            return Ok(new { message = "Message sent successfully!", model = contactMessageDto });
        }

    }
}
