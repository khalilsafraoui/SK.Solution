using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SK.Visit.Application.Interfaces.Common;
using System.Security.Claims;



namespace SK.Visit.UI.Blazor.Common
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ILogger<CurrentUserService> _logger;

        public CurrentUserService(IHttpContextAccessor contextAccessor, ILogger<CurrentUserService> logger)
        {
            _contextAccessor = contextAccessor;
            _logger = logger;
        }

        public Guid UserId
        {
            get
            {
                var user = _contextAccessor.HttpContext?.User;
                var id = user?.FindFirst(ClaimTypes.NameIdentifier).Value;
                if (string.IsNullOrWhiteSpace(id))
                {
                    _logger.LogInformation("Authenticated: {auth}", user?.Identity?.IsAuthenticated ?? false);
                    _logger.LogInformation("ClaimTypes.NameIdentifier: {id}", id ?? "null");
                    throw new InvalidOperationException("The user ID claim is missing from the current user context.");
                }
                return Guid.Parse(id);
            }
        }
    }
}
