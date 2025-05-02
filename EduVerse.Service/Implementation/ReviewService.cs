using AutoMapper;
using EduVerse.Core.Dtos;
using EduVerse.Core.Models;
using EduVerse.Data.Contract;
using EduVerse.Service.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduVerse.Service.Implementation
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _repository;
        private readonly IMapper _mapper;
        public ReviewService(IReviewRepository repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<UserReviewDto> GetReviewByIdAsync(int id)
        {
            var review = await _repository.GetReviewByIdAsync(id);
            return _mapper.Map<UserReviewDto>(review);
        }

        public async Task<IEnumerable<UserReviewDto>> GetReviewByUserIdAsync(int userId)
        {
            var review = await _repository.GetReviewByUserIdAsync(userId);
            return _mapper.Map<IEnumerable<UserReviewDto>>(review); 
        }

        public async Task<IEnumerable<UserReviewDto>> GetReviewByCourseIdAsync(int courseId)
        {
            var review = await _repository.GetReviewByCourseIdAsync(courseId);
            return _mapper.Map<IEnumerable<UserReviewDto>>(review);
        }

        public async Task AddReviewAsync(UserReviewDto userReviewDto)
        {
            var review = _mapper.Map<Review>(userReviewDto);
            await _repository.AddReviewAsync(review);
        }

        public async Task UpdateReviewAsync(UserReviewDto userReviewDto)
        {
            var review = _mapper.Map<Review>(userReviewDto);
            await _repository.UpdateReviewAsync(review);
        }

        public async Task DeleteReviewAsync(int reviewId)
        {
            await _repository.DeleteReviewAsync(reviewId);
        }
    }
}
