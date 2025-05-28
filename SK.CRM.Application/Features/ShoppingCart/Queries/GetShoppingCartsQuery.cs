using AutoMapper;
using MediatR;
using SK.CRM.Application.DTOs;
using SK.CRM.Application.Interfaces;

namespace SK.CRM.Application.Features.ShoppingCart.Queries
{
    public sealed record GetShoppingCartsQuery(string? userId) : IRequest<IEnumerable<ShoppingCartDto>>;

    public sealed class GetShoppingCartsQueryHandler : IRequestHandler<GetShoppingCartsQuery, IEnumerable<ShoppingCartDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetShoppingCartsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ShoppingCartDto>> Handle(GetShoppingCartsQuery request, CancellationToken cancellationToken)
        {
            var shippingCarts = await _unitOfWork.ShoppingCartRepository.GetAllAsync(request.userId);
            if (!shippingCarts.Any())
            {
                return new List<ShoppingCartDto>();
            }
            return _mapper.Map<List<ShoppingCartDto>>(shippingCarts);
        }
    }
}
