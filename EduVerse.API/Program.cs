
using EduVerse.API.Middlewares;
using EduVerse.Data;
using EduVerse.Data.Contract;
using EduVerse.Data.Implementation;
using EduVerse.Service.Contract;
using EduVerse.Service.Implementation;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Logging;
using Serilog;
using Serilog.Templates;
using System.Net;

namespace EduVerse.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Configure Serilog with the settings
            Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.Debug()
            .MinimumLevel.Information()
            .Enrich.FromLogContext()
            .CreateBootstrapLogger();

            try
            {
                #region Service Configuration
                var builder = WebApplication.CreateBuilder(args);
                var configuration = builder.Configuration;

                //Seri log & API-insights
                builder.Services.AddApplicationInsightsTelemetry();

                builder.Host.UseSerilog((context, services, loggerConfiguration) => loggerConfiguration
                .ReadFrom.Configuration(context.Configuration)
                .ReadFrom.Services(services)
                .WriteTo.Console(new ExpressionTemplate(
                    // Include trace and span ids when present.
                    "[{@t:HH:mm:ss} {@l:u3}{#if @tr is not null} ({substring(@tr,0,4)}:{substring(@sp,0,4)}){#end}] {@m}\n{@x}"))
                .WriteTo.ApplicationInsights(
                  services.GetRequiredService<TelemetryConfiguration>(),
                  TelemetryConverter.Traces));

                Log.Information("Starting the EduVerseByKashyap API...");

                #region AD B2C
                builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                  .AddMicrosoftIdentityWebApi(options =>
                  {
                      configuration.Bind("AzureAdB2C", options);
                      options.Events = new JwtBearerEvents();

                      //options.Events = new JwtBearerEvents
                      //{
                      //    OnTokenValidated = context =>
                      //    {
                      //        var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();

                      //        // Access the scope claim (scp) directly
                      //        var scopeClaim = context.Principal?.Claims.FirstOrDefault(c => c.Type == "scp")?.Value;

                      //        if (scopeClaim != null)
                      //        {
                      //            logger.LogInformation("Scope found in token: {Scope}", scopeClaim);
                      //        }
                      //        else
                      //        {
                      //            logger.LogWarning("Scope claim not found in token.");
                      //        }

                      //        return Task.CompletedTask;
                      //    },
                      //    OnAuthenticationFailed = context =>
                      //    {
                      //        var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();
                      //        logger.LogError("Authentication failed: {Message}", context.Exception.Message);
                      //        return Task.CompletedTask;
                      //    },
                      //    OnChallenge = context =>
                      //    {
                      //        var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();
                      //        logger.LogError("Challenge error: {ErrorDescription}", context.ErrorDescription);
                      //        return Task.CompletedTask;
                      //    }
                      //};
                  }, options => { configuration.Bind("AzureAdB2C", options); });

                // The following flag can be used to get more descriptive errors in development environments
                IdentityModelEventSource.ShowPII = false;
                #endregion

                //Database Configuration
                //var configuration = builder.Configuration;
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
                        policy.WithOrigins("http://localhost:4200", "https://eduversebykashyap.azurewebsites.net") // Corrected frontend URL without trailing slash
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

                //for seri log Application insights
                builder.Services.AddTransient<RequestBodyLoggingMiddleware>();
                builder.Services.AddTransient<ResponseBodyLoggingMiddleware>();

                #endregion

                #region Middlewares
                var app = builder.Build();

                app.UseCors("default");

                //Global exception handaling 
                app.UseExceptionHandler(errorApp =>
                {
                    errorApp.Run(async context =>
                    {
                        var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                        var exception = exceptionHandlerPathFeature?.Error;

                        Log.Error(exception, "Unhandled exception occurred. {ExceptionDetails}", exception?.ToString());
                        Console.WriteLine(exception?.ToString());
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        await context.Response.WriteAsync("An unexpected error occurred. Please try again later.");
                    });
                });

                //Middle ware for seri log & application insights
                app.UseMiddleware<RequestResponseLoggingMiddleware>();
                app.UseMiddleware<RequestBodyLoggingMiddleware>();
                app.UseMiddleware<ResponseBodyLoggingMiddleware>();

                // Configure the HTTP request pipeline.
                //if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }

                app.UseHttpsRedirection();

                app.UseAuthorization();

                app.UseAuthentication();

                app.MapControllers();

                app.Run();
                #endregion
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
           
        }
    }
}
