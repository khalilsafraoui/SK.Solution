using MediatR;
using SK.CRM.Application.Interfaces;

namespace SK.CRM.Application.Features.ShoppingCart.Commands
{
    public sealed record UpdateCartQuantityCommand(string userId, int productId, int newQuantiy) : IRequest<bool>;

    public class UpdateCartQuantityCommandHandler : IRequestHandler<UpdateCartQuantityCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateCartQuantityCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(UpdateCartQuantityCommand request, CancellationToken cancellationToken)
        {
             await _unitOfWork.ShoppingCartRepository.UpdateCartWithNewQuantityAsync(request.userId, request.productId, request.newQuantiy);
             return await _unitOfWork.SaveChangesAsync()>0;
        }
    }
}
