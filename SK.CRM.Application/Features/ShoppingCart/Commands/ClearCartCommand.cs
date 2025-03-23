using MediatR;
using SK.CRM.Application.Interfaces;

namespace SK.CRM.Application.Features.ShoppingCart.Commands
{
    public sealed record ClearCartCommand(string? userId) : IRequest<bool>;

    public class ClearCartCommandHandler : IRequestHandler<ClearCartCommand, bool>
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;

        public ClearCartCommandHandler(IShoppingCartRepository shoppingCartRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
        }

        public async Task<bool> Handle(ClearCartCommand request, CancellationToken cancellationToken)
        {
            return await _shoppingCartRepository.ClearCartAsync(request.userId);
        }
    }
}
