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

        public async Task UpdateCourseAsync(CourseDetailDto courseDetailDto)
        {
            var course = await courseRepository.GetCourseByIdAsync(courseDetailDto.CourseId);

            if (course == null)
            {
                throw new Exception("Course not found");
            }

            course.Title = courseDetailDto.Title;
            course.Description = courseDetailDto.Description;
            course.Price = courseDetailDto.Price;
            course.CourseType = courseDetailDto.CourseType;
            course.SeatsAvailable = courseDetailDto.SeatsAvailable;
            course.Duration = courseDetailDto.Duration;
            course.CategoryId = courseDetailDto.CategoryId;
            course.InstructorId = courseDetailDto.InstructorId;
            course.StartDate = courseDetailDto.StartDate;
            course.EndDate = courseDetailDto.EndDate;

            var existingSessionIds = course.SessionDetails.Select(s => s.SessionId).ToList();
            var updatedSessionIds = courseDetailDto.SessionDetails.Select(s => s.SessionId).ToList();

            var sessionsToRemove = course.SessionDetails.Where(s => !updatedSessionIds.Contains(s.SessionId)).ToList();
            foreach(var session in sessionsToRemove)
            {
                course.SessionDetails.Remove(session);
                courseRepository.RemoveSessionDetail(session);
            }

            foreach(var sessionDtos in courseDetailDto.SessionDetails)
            {
                var existingSession = course.SessionDetails.FirstOrDefault(s => s.SessionId == sessionDtos.SessionId);
                if (existingSession != null)
                {
                    existingSession.Title = sessionDtos.Title;
                    existingSession.Description = sessionDtos.Description;
                    existingSession.VideoUrl = sessionDtos.VideoUrl;
                    existingSession.VideoOrder = sessionDtos.VideoOrder;
                }
                else
                {
                    var newSession = new SessionDetail
                    {
                        Title = sessionDtos.Title,
                        Description = sessionDtos.Description,
                        VideoUrl = sessionDtos.VideoUrl,
                        VideoOrder = sessionDtos.VideoOrder,
                        CourseId = course.CourseId,
                    };
                    course.SessionDetails.Add(newSession);
                }
            }
            await courseRepository.UpdateCourseAsync(course);
        }
        public async Task DeleteCourseAsync(int courseId)
        {
            await courseRepository.DeleteCourseAsync(courseId);
        }

        public async Task<List<InstructorDto>> GetAllInstructorsAsync()
        {
            var inst = await courseRepository.GetAllInstructorsAsync();
            if (inst == null)
            {
                throw new Exception("Instructors not found");
            }
            List<InstructorDto> instructorDtos = new List<InstructorDto>();
            foreach (var instructor in inst)
            {
                var instructorDto = new InstructorDto
                {
                    InstructorId = instructor.InstructorId,
                    FirstName = instructor.FirstName,
                    LastName = instructor.LastName,
                    Email = instructor.Email,
                    Bio = instructor.Bio,
                    UserId = instructor.UserId
                };
                instructorDtos.Add(instructorDto);
            }
            return instructorDtos;
        }
    }
}
