using MediatR;
using SK.CRM.Application.Interfaces;

namespace SK.CRM.Application.Features.ShoppingCart.Commands
{
    public sealed record UpdateCartCommand(string userId, int productId, int updateBy) : IRequest<bool>;

    public class UpdateCartCommandHandler : IRequestHandler<UpdateCartCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateCartCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(UpdateCartCommand request, CancellationToken cancellationToken)
        {
             await _unitOfWork.ShoppingCartRepository.UpdateCartAsync(request.userId, request.productId, request.updateBy);
             return await _unitOfWork.SaveChangesAsync()>0;
        }
    }
}
