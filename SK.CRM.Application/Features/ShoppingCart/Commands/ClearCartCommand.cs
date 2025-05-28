using MediatR;
using SK.CRM.Application.Interfaces;

namespace SK.CRM.Application.Features.ShoppingCart.Commands
{
    public sealed record ClearCartCommand(string? userId) : IRequest<bool>;

    public class ClearCartCommandHandler : IRequestHandler<ClearCartCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        public ClearCartCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(ClearCartCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.ShoppingCartRepository.ClearCartAsync(request.userId);
            return await _unitOfWork.SaveChangesAsync()>0;
        }
    }
}
