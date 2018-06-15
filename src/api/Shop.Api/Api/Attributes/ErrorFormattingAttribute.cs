using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Shop.Api.Api.Model;

namespace Shop.Api.Api.Attributes
{
    /// <summary>
    /// Provides formatted error response based on exceptions.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    internal sealed class ErrorFormattingAttribute : ExceptionFilterAttribute
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public ErrorFormattingAttribute(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public override void OnException(ExceptionContext context)
        {
            var message =
                _hostingEnvironment.IsDevelopment() ?
                context.Exception.ToString() :
                context.Exception.Message;

            var response = new ErrorResponse.InternalServerError
            {
                Details = message
            };
            context.Result = new InternalServerErrorObjectResult(response);
        }

        private class InternalServerErrorObjectResult : ObjectResult
        {
            public InternalServerErrorObjectResult(object value)
                : base(value)
            {
                StatusCode = StatusCodes.Status500InternalServerError;
            }
        }
    }
}