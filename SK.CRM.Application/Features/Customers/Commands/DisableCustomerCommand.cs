using MediatR;
using SK.CRM.Application.Interfaces;


namespace SK.CRM.Application.Features.Customers.Commands
{
    public sealed record DisableCustomerCommand(Guid CustomerId) : IRequest<bool>;


    public class DisableCustomerCommandHandler : IRequestHandler<DisableCustomerCommand, bool>
    {
        private readonly ICustomerRepository _customerRepository;

        public DisableCustomerCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<bool> Handle(DisableCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetByIdAsync(request.CustomerId);
            if (customer == null)
            {
                return false; // Or throw an exception if preferred
            }

            await _customerRepository.DisableCustomerAsync(request.CustomerId);
            return true;
        }
    }

}
