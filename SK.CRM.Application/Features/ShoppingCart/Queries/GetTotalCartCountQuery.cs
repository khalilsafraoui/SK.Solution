using MediatR;
using SK.CRM.Application.Interfaces;

namespace SK.CRM.Application.Features.ShoppingCart.Queries
{
    public sealed record GetTotalCartCountQuery(string? userId) : IRequest<int>;

    public sealed class GetTotalCartCountQueryHandler : IRequestHandler<GetTotalCartCountQuery, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetTotalCartCountQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(GetTotalCartCountQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.ShoppingCartRepository.GetTotalCartCountAsync(request.userId);
        }
    }
}
