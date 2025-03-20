using MediatR;
using SK.Customer.Application.Interfaces;


namespace SK.Customer.Application.Features.Customers.Commands
{
    public sealed record DeleteCustomerCommand(Guid CustomerId) : IRequest<bool>;


    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, bool>
    {
        private readonly ICustomerRepository _customerRepository;

        public DeleteCustomerCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<bool> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetByIdAsync(request.CustomerId);
            if (customer == null)
            {
                return false; // Or throw an exception if preferred
            }

            await _customerRepository.DeleteAsync(request.CustomerId);
            return true;
        }
    }

}
