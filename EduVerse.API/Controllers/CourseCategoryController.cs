﻿using EduVerse.Service.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace EduVerse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseCategoryController : ControllerBase
    {
        private readonly ICourseCategoryService courseCategoryService;

        public CourseCategoryController(ICourseCategoryService courseCategoryService)
        {
            this.courseCategoryService = courseCategoryService;
        }

        [HttpGet("{categoryId}")]
        public async Task<IActionResult> GetById(int categoryId)
        {
            var category = await courseCategoryService.GetByIdAsync(categoryId);
            //When id is not present in the database, we need to return 404 status code
            if (category == null)
            {
                return NotFound();
            }
            //When we have data, we need to return 200 status code
            return Ok(category);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCourseCategories()
        {
            var categories = await courseCategoryService.GetCourseCategories();
            
            return Ok(categories);
        }
    }
}
