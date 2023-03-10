using BaseService.Consul.Register;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net;

namespace BaseService.Consul
{
    public static class HealthCheckMiddlewareExtension
    {
        /// <summary>
        /// 设置心跳响应
        /// </summary>
        /// <param name="app"></param>
        /// <param name="checkPath">默认是/Health</param>
        /// <returns></returns>
        public static void UseHealthCheckMiddleware(this IApplicationBuilder app, string checkPath = "/healthcheck")
        {
            app.Map(checkPath, applicationBuilder => applicationBuilder.Run(async context =>
            {
                var id = context.Request.Query["id"].ToString();

                var consulRegister = context.RequestServices.GetRequiredService<IConsulRegister>();
                if (consulRegister != null)
                {
                    if (consulRegister.ExistServiceId(id))
                    {
                        Console.WriteLine($"This is Health Check");
                        context.Response.StatusCode = (int)HttpStatusCode.OK;
                        await context.Response.WriteAsync("OK");
                    }
                    else
                    {
                        await consulRegister.ConsulDeregistAsync(id);

                        Console.WriteLine($"This is Dead Health Check");
                        context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                        await context.Response.WriteAsync("NotFound");
                    }
                }
            }));
        }
    }
}
