
using EduVerse.Data;
using EduVerse.Service;
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
            #endregion

            #region Middlewares
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
            #endregion
        }
    }
}
