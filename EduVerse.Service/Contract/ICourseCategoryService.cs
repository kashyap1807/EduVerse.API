using EduVerse.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduVerse.Service.Contract
{
    public interface ICourseCategoryService
    {
        Task<CourseCategoryDto?> GetByIdAsync(int id);
        Task<List<CourseCategoryDto>> GetCourseCategories();
    }
}
