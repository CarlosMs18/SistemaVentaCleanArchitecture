using System.Security.Claims;
using SistemaVenta.Application.Constants;
using SistemaVenta.Application.Models.Identity;

namespace SistemaVenta.Api.Middleware
{
    public class SessionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public SessionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context, UserSession userSession)
        {
            var claims = context.User.Claims;
            if (claims != null && claims.Count() > 0)
            {
                userSession.Id = claims.FirstOrDefault(x => x.Type == CustomClaimTypes.UID)?.Value ?? "";
                userSession.UserName = claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value ?? "";
                userSession.Email = claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value ?? "";
            }
            userSession.ClientIP = context.Connection.RemoteIpAddress?.ToString() ?? "";
            await _next(context);
        }
    }
}
