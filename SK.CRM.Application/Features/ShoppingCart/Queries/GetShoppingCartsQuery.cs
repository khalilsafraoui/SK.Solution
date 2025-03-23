using AutoMapper;
using MediatR;
using SK.CRM.Application.DTOs;
using SK.CRM.Application.Interfaces;
using SK.CRM.Domain.Entities;

namespace SK.CRM.Application.Features.ShoppingCart.Queries
{
    public sealed record GetShoppingCartsQuery(string? userId) : IRequest<IEnumerable<ShoppingCartDto>>;

    public sealed class GetShoppingCartsQueryHandler : IRequestHandler<GetShoppingCartsQuery, IEnumerable<ShoppingCartDto>>
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IMapper _mapper;

        public GetShoppingCartsQueryHandler(IShoppingCartRepository shoppingCartRepository, IMapper mapper)
        {
            _shoppingCartRepository = shoppingCartRepository ?? throw new ArgumentNullException(nameof(shoppingCartRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<ShoppingCartDto>> Handle(GetShoppingCartsQuery request, CancellationToken cancellationToken)
        {
            var shippingCarts = await _shoppingCartRepository.GetAllAsync(request.userId);
            if (!shippingCarts.Any())
            {
                return new List<ShoppingCartDto>();
            }
            return _mapper.Map<List<ShoppingCartDto>>(shippingCarts);
        }
    }
}
