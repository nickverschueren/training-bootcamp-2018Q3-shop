using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Shop.Api.Api.Middleware
{
    public class DummyAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public DummyAuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task InvokeAsync(HttpContext context)
        {
            context.User = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, "9001"),
                new Claim(ClaimTypes.Surname, "Dummy")
            }, "dummy"));
            return _next(context);
        }
    }
}
