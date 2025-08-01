﻿using EduVerse.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduVerse.Service.Contract
{
    public interface ICourseService
    {
        Task<List<CourseDto>> GetAllCoursesAsync(int? categoryId = null);
        Task<List<CourseDto>> SearchCoursesAsync(string searchTerm);
        Task<CourseDetailDto> GetCourseDetailAsync(int courseId);

        Task AddCourseAsync(CourseDetailDto courseDto);
        Task UpdateCourseAsync(CourseDetailDto courseDetailDto);
        Task DeleteCourseAsync(int courseId);

        Task<List<InstructorDto>> GetAllInstructorsAsync();
        Task<bool> UpdateCourseThumbnail(string courseThumbnailUrl, int courseId);
    }
}
