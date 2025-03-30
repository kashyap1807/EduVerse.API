using EduVerse.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduVerse.Data.Contract
{
    //Synchronous methods
    //public interface ICourseCategoryRepository
    //{
    //    CourseCategory? GetById(int id);

    //    List<CourseCategory> GetCourseCategories();
    //}

    //Asynchronous methods
    //Async methods will alwasys return Task<T> and it's methos named as async at suffix
    public interface ICourseCategoryRepository
    {
        Task<CourseCategory?> GetByIdAsync(int id);

        Task<List<CourseCategory>> GetCourseCategoriesAsync();
    }
}
