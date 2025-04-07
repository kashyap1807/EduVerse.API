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
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository courseRepository;
        public CourseService(ICourseRepository courseRepository)
        {
            this.courseRepository = courseRepository;
        }
        public Task<List<CourseDto>> GetAllCoursesAsync(int? categoryId = null)
        {
            return courseRepository.GetAllCourseAsync(categoryId);
        }

        public Task<CourseDetailDto> GetCourseDetailAsync(int courseId)
        {
            return courseRepository.GetCourseDetailAsync(courseId);
        }

        public async Task AddCourseAsync(CourseDetailDto courseDto)
        {
            var courseModel = new Course
            {
                Title = courseDto.Title,
                Description = courseDto.Description,
                Price = courseDto.Price,
                CourseType = courseDto.CourseType,
                SeatsAvailable = courseDto.SeatsAvailable,
                Duration = courseDto.Duration,
                CategoryId = courseDto.CategoryId,
                InstructorId = courseDto.InstructorId,
                StartDate = courseDto.StartDate,
                EndDate = courseDto.EndDate,
                SessionDetails = courseDto.SessionDetails.Select(s => new SessionDetail
                {
                    Title = s.Title,
                    Description = s.Description,
                    VideoUrl = s.VideoUrl,
                    VideoOrder = s.VideoOrder
                }).ToList()
            };

            await courseRepository.AddCourseAsync(courseModel);
        }
    }
}
