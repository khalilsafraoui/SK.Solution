using MediatR;
using SK.CRM.Application.Interfaces;


namespace SK.CRM.Application.Features.Prospects.Commands
{
    public sealed record DeleteProspectCommand(Guid CustomerId) : IRequest<bool>;


    public class DeleteProspectCommandHandler : IRequestHandler<DeleteProspectCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteProspectCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteProspectCommand request, CancellationToken cancellationToken)
        {
            var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(request.CustomerId);
            if (customer == null)
            {
                return false; // Or throw an exception if preferred
            }

            await _unitOfWork.CustomerRepository.DeleteAsync(request.CustomerId);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }

}
