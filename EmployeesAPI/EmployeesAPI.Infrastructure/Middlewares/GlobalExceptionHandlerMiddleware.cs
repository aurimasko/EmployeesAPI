using EmployeesAPI.Domain.Common;
using EmployeesAPI.Domain.Configuration;
using EmployeesAPI.Infrastructure.Logger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net;

namespace EmployeesAPI.Infrastructure.Middlewares
{
    public static class GlobalExceptionHandlerMiddleware
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        try
                        {
                            using (var scope = app.ApplicationServices.CreateScope())
                            {
                                //Log exception
                                var logService = scope.ServiceProvider.GetService<ILoggerService>();

                                if (logService != null)
                                {
                                    var logReport = await logService.Log(new Error(contextFeature.Error, context.Request)); 
                                    await context.Response.WriteAsync(new Response("An internal error occured, please contact support with error id '" + logReport.IssueId + "'", ErrorCodeTypes.Exception).ToString());
                                }
                                else
                                {
                                    await context.Response.WriteAsync(new Response("An internal error occured", ErrorCodeTypes.Exception).ToString());
                                }
                            }
                        }
                        catch (Exception)
                        {
                            await context.Response.WriteAsync(new Response("An internal error occured", ErrorCodeTypes.Exception).ToString());
                        }
                    }

                });
            });
        }
    }
}
