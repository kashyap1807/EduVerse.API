using EduVerse.Core.Dtos;
using EduVerse.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduVerse.Service
{
    public class CourseCategoryService : ICourseCategoryService
    {
        private readonly ICourseCategoryRepository courseCategoryRepository;
        public CourseCategoryService(ICourseCategoryRepository courseCategoryRepository)
        {
            this.courseCategoryRepository = courseCategoryRepository;
        }

        public async Task<CourseCategoryDto?> GetByIdAsync(int id)
        {
            //When we need data, we need to await for the call to get data
            //This await keyword tells compiler to pause the execution until data is retireved
            var data = await courseCategoryRepository.GetByIdAsync(id);
            return new CourseCategoryDto()
            {
                CategoryId = data.CategoryId,
                CategoryName = data.CategoryName,
                Description = data.Description
            };
        }

        public async Task<List<CourseCategoryDto>> GetCourseCategories()
        {
            var data = await courseCategoryRepository.GetCourseCategoriesAsync();
            var dtoData = data.Select(s=> new CourseCategoryDto()
            {
                CategoryId = s.CategoryId,
                CategoryName = s.CategoryName,
                Description = s.Description
            }).ToList();

            return dtoData;
        }
    }
}
