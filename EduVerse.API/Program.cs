
using EduVerse.Data.Models;
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
                    configuration.GetConnectionString("EduVerseContext"),
                    provideroptions => provideroptions.EnableRetryOnFailure());
            } );
            // Add services to the container.
            
            builder.Services.AddControllers();
            
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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
