
using EduVerse.Data;
using EduVerse.Data.Contract;
using EduVerse.Data.Implementation;
using EduVerse.Service.Contract;
using EduVerse.Service.Implementation;
using Microsoft.EntityFrameworkCore;

namespace EduVerse.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            #region Service Configuration
            var builder = WebApplication.CreateBuilder(args);
           
            //Database Configuration
            var configuration = builder.Configuration;
            builder.Services.AddDbContextPool<EduVerseDbContext>(options =>
            {
                options.UseSqlServer(
                    configuration.GetConnectionString("EduVerseDbContext"),
                    providerOptions => providerOptions.EnableRetryOnFailure());
                //Only for development purpose not go to PRODUCTION
                //options.EnableServiceProviderCaching();
            });
            //Cors for connection server routes to client app
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("default", policy =>
                {
                    policy.WithOrigins("http://localhost:4200") // Corrected frontend URL without trailing slash
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials();  // Required for SignalR
                });
            });
            // Add services to the container.

            builder.Services.AddControllers();
            
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //configure services DI here
            //AddScoped : when a request is hit till the request is completely processed and return only one instance of requested class will be given
            //AddTransient : when a request is hit till the request is completely processed and return new instance of requested class will be given
            //Addsingletone : only one time for the whole application
            builder.Services.AddScoped<ICourseCategoryRepository, CourseCategoryRepository>();
            builder.Services.AddScoped<ICourseCategoryService, CourseCategoryService>();
            builder.Services.AddScoped<ICourseRepository, CourseRepository>();
            builder.Services.AddScoped<ICourseService, CourseService>();
            #endregion

            #region Middlewares
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("default"); 
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
            #endregion
        }
    }
}
