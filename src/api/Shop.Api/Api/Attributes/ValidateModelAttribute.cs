using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Shop.Api.Api.Model;

namespace Shop.Api.Api.Attributes
{
    /// <summary>
    /// Provides basic data model validation based on data annotations.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    internal sealed class ValidateModelAttribute : ActionFilterAttribute
    {
        public static AutoMapper.IMapper Mapper { get; set; }

        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(Mapper.Map<ErrorResponse.Validation>(context.ModelState));
            }

            return base.OnActionExecutionAsync(context, next);
        }
    }
}
