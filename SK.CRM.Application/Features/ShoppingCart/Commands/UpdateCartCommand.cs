using MediatR;
using SK.CRM.Application.Interfaces;

namespace SK.CRM.Application.Features.ShoppingCart.Commands
{
    public sealed record UpdateCartCommand(string userId, int productId, int updateBy) : IRequest<bool>;

    public class UpdateCartCommandHandler : IRequestHandler<UpdateCartCommand, bool>
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;

        public UpdateCartCommandHandler(IShoppingCartRepository shoppingCartRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
        }

        public async Task<bool> Handle(UpdateCartCommand request, CancellationToken cancellationToken)
        {
            return await _shoppingCartRepository.UpdateCartAsync(request.userId, request.productId, request.updateBy);
        }
    }
}
