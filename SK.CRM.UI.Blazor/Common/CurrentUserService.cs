using Microsoft.AspNetCore.Components.Authorization;
using SK.CRM.Application.Interfaces.Common;
using System.Security.Claims;


namespace SK.CRM.UI.Blazor.Common
{
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
