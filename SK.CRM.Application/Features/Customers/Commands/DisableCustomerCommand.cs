using MediatR;
using SK.CRM.Application.Interfaces;


namespace SK.CRM.Application.Features.Customers.Commands
{
    public sealed record DisableCustomerCommand(Guid CustomerId) : IRequest<bool>;


    public class DisableCustomerCommandHandler : IRequestHandler<DisableCustomerCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DisableCustomerCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DisableCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(request.CustomerId);
            if (customer == null)
            {
                return false; // Or throw an exception if preferred
            }

            await _unitOfWork.CustomerRepository.DisableCustomerAsync(request.CustomerId);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }

}
