using EduVerse.Core.Dtos;
using EduVerse.Service.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduVerse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _service;

        public ReviewController(IReviewService service)
        {
            _service = service;
        }

        [HttpGet("{reviewId}")]
        public async Task<ActionResult<UserReviewDto?>> GetReviewById(int reviewId)
        {
            var review = await _service.GetReviewByIdAsync(reviewId);
            if (review == null)
            {
                return NotFound();
            }
            return Ok(review);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<UserReviewDto?>>> GetReviewByUserId(int userId)
        {
            var review = await _service.GetReviewByUserIdAsync(userId);
            if(review == null)
            {
                return NotFound();
            }
            return Ok(review);
        }

        [HttpGet("course/{courseId}")]
        public async Task<ActionResult<IEnumerable<UserReviewDto?>>> GetReviewByCourseId(int courseId)
        {
            var review = await _service.GetReviewByCourseIdAsync(courseId);
            if(review == null)
            {
                return NotFound();
            }
            return Ok(review);
        }

        [HttpPost]
        public async Task<ActionResult> AddReview([FromBody] UserReviewDto userReviewDto)
        {
            await _service.AddReviewAsync(userReviewDto);
            return CreatedAtAction(nameof(GetReviewById), new { id = userReviewDto.ReviewId }, userReviewDto);
        }

        [HttpPut("{reviewId}")]
        public async Task<ActionResult> UpdateReview(int reviewId, [FromBody] UserReviewDto userReviewDto)
        {
            if(reviewId != userReviewDto.ReviewId)
            {
                return BadRequest();    
            }
            await _service.UpdateReviewAsync(userReviewDto);
            return Ok();
        }

        [HttpDelete("{reviewId}")]
        public async Task<ActionResult> DeleteReview(int reviewId)
        {
            await _service.DeleteReviewAsync(reviewId);
            return Ok();
        }
    }
}
