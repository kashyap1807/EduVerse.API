using EduVerse.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduVerse.Service.Contract
{
    public interface IReviewService
    {
        Task<UserReviewDto> GetReviewByIdAsync(int id);
        Task<IEnumerable<UserReviewDto>> GetReviewByUserIdAsync(int userId);
        Task<IEnumerable<UserReviewDto>> GetReviewByCourseIdAsync(int courseId);
        Task AddReviewAsync(UserReviewDto userReviewDto);
        Task UpdateReviewAsync(UserReviewDto userReviewDto);
        Task DeleteReviewAsync(int reviewId);
    }
}
