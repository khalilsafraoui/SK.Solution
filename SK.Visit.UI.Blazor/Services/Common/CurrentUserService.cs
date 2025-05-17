using Microsoft.AspNetCore.Http;
using SK.Visit.Application.Interfaces.Common;
using System.Security.Claims;



namespace SK.Visit.UI.Blazor.Common
{
    public class CurrentUserService : ICurrentUserService
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
                var id = _contextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier).Value;
                return Guid.Parse(id);
            }
        }
    }
}
