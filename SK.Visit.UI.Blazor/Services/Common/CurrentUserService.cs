using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Serilog.Core;
using SK.Visit.Application.Interfaces.Common;
using System.Security.Claims;



namespace SK.Visit.UI.Blazor.Common
{
    //public class CurrentUserService : ICurrentUserService
    //{
    //    private readonly IHttpContextAccessor _contextAccessor;
    //    private readonly ILogger<CurrentUserService> _logger;

    //    public CurrentUserService(IHttpContextAccessor contextAccessor, ILogger<CurrentUserService> logger)
    //    {
    //        _contextAccessor = contextAccessor;
    //        _logger = logger;
    //    }

    //    public Guid UserId
    //    {
    //        get
    //        {
    //            var user = _contextAccessor.HttpContext?.User;
    //            var id = user?.FindFirst(ClaimTypes.NameIdentifier).Value;
    //            if (string.IsNullOrWhiteSpace(id))
    //            {
    //                if (_contextAccessor.HttpContext == null)
    //                {
    //                    _logger.LogInformation("HttpContext is null");
    //                }

    //                if (user == null)
    //                {
    //                    _logger.LogInformation("User is null");
    //                }
                    
    //                foreach (var claim in user?.Claims)
    //                {
    //                    _logger.LogInformation("CLAIM TYPE: {Type} - VALUE: {Value}", claim.Type, claim.Value);
    //                }
    //                _logger.LogInformation("Authenticated: {auth}", user?.Identity?.IsAuthenticated ?? false);
    //                _logger.LogInformation("ClaimTypes.NameIdentifier: {id}", id ?? "null");
    //                throw new InvalidOperationException("The user ID claim is missing from the current user context.");
    //            }
    //            return Guid.Parse(id);
    //        }
    //    }


    //}

    public class CurrentUserService : ICurrentUserService
    {
        private readonly AuthenticationStateProvider _authStateProvider;

        public CurrentUserService(AuthenticationStateProvider authStateProvider)
        {
            _authStateProvider = authStateProvider;
        }

        public async Task<Guid> GetUserIdAsync()
        {
            var authState = await _authStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user?.Identity?.IsAuthenticated != true)
                throw new Exception("User is not authenticated");

            var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
                throw new Exception("ClaimTypes.NameIdentifier is missing");

            return Guid.Parse(userId);
        }
    }
}
