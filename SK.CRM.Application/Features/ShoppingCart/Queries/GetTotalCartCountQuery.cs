using MediatR;
using SK.CRM.Application.Interfaces;

namespace SK.CRM.Application.Features.ShoppingCart.Queries
{
    public sealed record GetTotalCartCountQuery(string? userId) : IRequest<int>;

    public sealed class GetTotalCartCountQueryHandler : IRequestHandler<GetTotalCartCountQuery, int>
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;

        public GetTotalCartCountQueryHandler(IShoppingCartRepository shoppingCartRepository)
        {
            _shoppingCartRepository = shoppingCartRepository ?? throw new ArgumentNullException(nameof(shoppingCartRepository));
        }

        public async Task<int> Handle(GetTotalCartCountQuery request, CancellationToken cancellationToken)
        {
            return await _shoppingCartRepository.GetTotalCartCountAsync(request.userId);
        }
    }
}
