using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace TimeProject.Services.Api.Middlewares
{
    public class ServerErrorResponseMiddleware
    {
        public RequestDelegate _next;

        public ServerErrorResponseMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception e)
            {

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                var errorObj = new { success = false, data = new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("Server Error", e.Message) } };
                await context.Response.WriteAsJsonAsync(new { success = false, data = new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("Server Error", e.Message) } });
            }
        }
    }

    public static class ServerErrorResponseMiddlewareApplicationBuilderExtension
    {
        public static void UseServerErrorResponse(this IApplicationBuilder app)
        {
            app.UseMiddleware<ServerErrorResponseMiddleware>();
        }

    }
}
