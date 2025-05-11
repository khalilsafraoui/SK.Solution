using Microsoft.AspNetCore.Http;
using SK.Inventory.Application.Interfaces.Common;
using System.Security.Claims;


namespace SK.Inventory.UI.Blazor.Common
{
    class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public CurrentUserService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public Guid UserId
        {
            get
            {
                var id = _contextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
                return Guid.Parse(id);
            }
        }
    }
}
