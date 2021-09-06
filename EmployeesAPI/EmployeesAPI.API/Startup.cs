using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using EmployeesAPI.Infrastructure;
using EmployeesAPI.Domain.Interfaces;
using EmployeesAPI.Infrastructure.Repositories;
using EmployeesAPI.Domain.Services;
using EmployeesAPI.Infrastructure.Middlewares;
using EmployeesAPI.Infrastructure.Logger;
using EmployeesAPI.API;
using EmployeesAPI.Domain.Common;
using EmployeesAPI.Domain.Configuration;

namespace EmployeesAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    //serialize enums as strings not integers
                    options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                });

            services.AddDbContext<EmployeesDbContext>(options =>
            {
                string connectionString = Configuration.GetConnectionString("EmployeesDatabase");
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString),
                    (settings) =>
                    {
                        settings.EnableRetryOnFailure();
                    }).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

           
            services.Configure<ApiBehaviorOptions>(options =>
            {
                //Return custom model state validation results
                options.InvalidModelStateResponseFactory = (context) =>
                {
                    var modelState = context.ModelState;

                    var listOfErrorMessages = new List<string>();

                    foreach (var keyModelStatePair in modelState)
                    {
                        var key = keyModelStatePair.Key;
                        var errors = keyModelStatePair.Value.Errors;

                        if (errors != null && errors.Count > 0)
                        {
                            foreach (var error in errors)
                            {
                                if (!string.IsNullOrEmpty(error.ErrorMessage))
                                    listOfErrorMessages.Add(error.ErrorMessage);
                            }
                        }
                    }

                    var response = new Response(listOfErrorMessages, new List<ErrorCodeTypes>() { ErrorCodeTypes.ValidationErrors });
                    return new BadRequestObjectResult(response);
                };
            });

            services.AddSwaggerGen();

            services.AddScoped<IEmployeesRepository, EmployeesRepository>();
            services.AddScoped<IEmployeesService, EmployeesService>();
            services.AddScoped<ILoggerService, LoggerService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("AllowAllRequests");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.ConfigureExceptionHandler();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(options => { options.SwaggerEndpoint("/swagger/v1/swagger.json", ""); });
        }
    }
}
