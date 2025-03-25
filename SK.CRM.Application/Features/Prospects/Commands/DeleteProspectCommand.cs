using MediatR;
using SK.CRM.Application.Interfaces;


namespace SK.CRM.Application.Features.Prospects.Commands
{
    public sealed record DeleteProspectCommand(Guid CustomerId) : IRequest<bool>;


    public class DeleteProspectCommandHandler : IRequestHandler<DeleteProspectCommand, bool>
    {
        private readonly ICustomerRepository _customerRepository;

        public DeleteProspectCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<bool> Handle(DeleteProspectCommand request, CancellationToken cancellationToken)
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
