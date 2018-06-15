using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Shop.Api.Api.Model;

namespace Shop.Api.Api.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                var response = new ErrorResponse.InternalServerError
                {
                    Details = ex.ToString()
                };
                var responseBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(response));
                await context.Response.Body.WriteAsync(responseBytes);
            }
        }
    }
}
