using EduVerse.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduVerse.Data
{   
    //All thses are synchronous methods
    //public class CourseCategoryRepository(EduVerseDbContext dbContext) : ICourseCategoryRepository
    //{
    //    private readonly EduVerseDbContext dbContext = dbContext;
        
    //    public CourseCategory? GetById(int id)
    //    {
    //        var data = dbContext.CourseCategories.Find(id);
    //        return data;
    //    }

    //    public List<CourseCategory> GetCourseCategories()
    //    {
    //        var data = dbContext.CourseCategories.ToList();
    //        return data;
    //    }
    //}

    public class CourseCategoryRepository : ICourseCategoryRepository
    {
        private readonly EduVerseDbContext dbContext;

        public CourseCategoryRepository(EduVerseDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task<CourseCategory?> GetByIdAsync(int id)
        {
            //as long as we don't need immediatly, we can return task itself
            //if we need data immediatly we have to use async await .
            var data = dbContext.CourseCategories.FindAsync(id).AsTask();
            return data;
        }

        public Task<List<CourseCategory>> GetCourseCategoriesAsync()
        {
            var data = dbContext.CourseCategories.ToListAsync();
            return data;
        }
    }
}
