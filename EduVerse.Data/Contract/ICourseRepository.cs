using EduVerse.Core.Dtos;
using EduVerse.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduVerse.Data.Contract
{
    public interface ICourseRepository
    {
        //return just list of course
        // with get course by categoryId
        Task<List<CourseDto>> GetAllCourseAsync(int? categoryId = null);
        Task<List<CourseDto>> SearchCoursesAsync(string searchTerm);
        //return Detailed Particular course
        Task<CourseDetailDto> GetCourseDetailAsync(int courseId);
        Task AddCourseAsync(Course course);
        Task<Course> GetCourseByIdAsync(int courseId);
        Task UpdateCourseAsync(Course course);
        Task DeleteCourseAsync(int courseId);
        void RemoveSessionDetail(SessionDetail sessionDetail);
        Task<List<Instructor>> GetAllInstructorsAsync();
        Task<bool> UpdateCourseThumbnail(string courseThumbnailUrl, int courseId);
    }
}
