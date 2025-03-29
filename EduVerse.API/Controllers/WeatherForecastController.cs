using EduVerse.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace EduVerse.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly EduVerseDbContext dbcontext;
        public WeatherForecastController(ILogger<WeatherForecastController> logger,EduVerseDbContext dbcontext)
        {
            _logger = logger;
            this.dbcontext = dbcontext;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IActionResult Get()
        {
            var courses = dbcontext.Courses.ToList();
            return Ok(courses);
        }
    }
}
