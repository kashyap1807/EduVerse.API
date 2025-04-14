using EduVerse.Core.Dtos;
using EduVerse.Core.Models;
using EduVerse.Data.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduVerse.Data.Implementation
{
    public class CourseRepository : ICourseRepository
    {
        private readonly EduVerseDbContext dbContext;
        public CourseRepository(EduVerseDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<CourseDto>> GetAllCourseAsync(int? categoryId = null)
        {
            //this is dynamic query if it get categoryId it filter courses otherwise it return all courses
            //when write AsQueryable it's called deffered execution - it wan't run untill we await or use async method like ToList()
            var query = dbContext.Courses
              .Include(c => c.Category)
              .AsQueryable();

            //apply filter if categoryId is available
            if (categoryId.HasValue)
            {
                query = query.Where(c => c.CategoryId == categoryId.Value);
            }

            var courses = await query.Select(s=> new CourseDto
            {
                CourseId = s.CourseId,
                Title = s.Title,
                Description = s.Description,
                Price = s.Price,
                CourseType = s.CourseType,
                SeatsAvailable = s.SeatsAvailable,
                Duration = s.Duration,
                CategoryId = s.CategoryId,
                InstructorId = s.InstructorId,
                StartDate = s.StartDate,
                EndDate = s.EndDate,
                Category = new CourseCategoryDto
                {
                    CategoryId = s.Category.CategoryId,
                    CategoryName = s.Category.CategoryName,
                    Description = s.Category.Description
                },
                UserRating = new UserRatingDto
                {
                    CourseId = s.CourseId,
                    AverageRating = s.Reviews.Any() ? Convert.ToDecimal(s.Reviews.Average(r=>r.Rating)):0,
                    TotalRating = s.Reviews.Count
                }
            }).ToListAsync();

            return courses;
        }

        public async Task<CourseDetailDto> GetCourseDetailAsync(int courseId)
        {
            var course = await dbContext.Courses
                .Include(c => c.Category)
                .Include(c => c.Reviews)
                .Include(c => c.SessionDetails)
                .Where(c => c.CourseId == courseId)
                .Select(c => new CourseDetailDto()
                {
                    CourseId = c.CourseId,
                    Title = c.Title,
                    Description = c.Description,
                    Price = c.Price,
                    CourseType = c.CourseType,
                    SeatsAvailable = c.SeatsAvailable,
                    Duration = c.Duration,
                    CategoryId = c.CategoryId,
                    InstructorId = c.InstructorId,
                    StartDate = c.StartDate,
                    EndDate = c.EndDate,
                    Category = new CourseCategoryDto()
                    {
                        CategoryId = c.Category.CategoryId,
                        CategoryName = c.Category.CategoryName,
                        Description = c.Category.Description
                    },
                    Reviews = c.Reviews.Select(r => new UserReviewDto()
                    {
                        CourseId = r.CourseId,
                        UserName = r.User.DisplayName,
                        Rating = r.Rating,
                        Comments = r.Comments,
                        ReviewDate = r.ReviewDate
                    }).OrderByDescending(o=>o.Rating).Take(10).ToList(),
                    SessionDetails = c.SessionDetails.Select(sd => new SessionDetailDto()
                    {
                        SessionId = sd.SessionId,
                        CourseId = sd.CourseId,
                        Title = sd.Title,
                        Description = sd.Description,
                        VideoUrl = sd.VideoUrl,
                        VideoOrder = sd.VideoOrder
                    }).OrderBy(o=>o.VideoOrder).ToList(),
                    UserRating = new UserRatingDto
                    {
                        CourseId = c.CourseId,
                        AverageRating = c.Reviews.Any() ? Convert.ToDecimal(c.Reviews.Average(r=>r.Rating)):0,
                        TotalRating = c.Reviews.Count
                    }
                }).FirstOrDefaultAsync();
            return course;
        }
        public async Task AddCourseAsync(Course course)
        {
            await dbContext.Courses.AddAsync(course);
            await dbContext.SaveChangesAsync();
        }

        public async Task UpdateCourseAsync(Course course)
        {
            dbContext.Courses.Update(course);
            await dbContext.SaveChangesAsync();
        }

        public async Task<Course> GetCourseByIdAsync(int courseId)
        {
            return await dbContext.Courses.Include(c => c.SessionDetails).FirstOrDefaultAsync(c => c.CourseId == courseId);
        }

        public async Task DeleteCourseAsync(int courseId)
        {
            var course = await GetCourseByIdAsync(courseId);
            if (course != null)
            {
                dbContext.Courses.Remove(course);
                await dbContext.SaveChangesAsync();
            }
        }

        public void RemoveSessionDetail(SessionDetail sessionDetail)
        {
            dbContext.SessionDetails.Remove(sessionDetail);
        }

        public async Task<List<Instructor>> GetAllInstructorsAsync()
        {
            List<Instructor> i = await dbContext.Instructors.ToListAsync();
            return i;
        }
    }
}
