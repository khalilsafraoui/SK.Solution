using MediatR;
using SK.CRM.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SK.CRM.Application.Features.Customers.Commands
{
    public sealed record DeleteCustomerAddressCommand(Guid AddressId) : IRequest<bool>;


    public class DeleteCustomerAddressCommandHandler : IRequestHandler<DeleteCustomerAddressCommand, bool>
    {
        private readonly IAddressRepository _addressRepository;

        public DeleteCustomerAddressCommandHandler(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        public async Task<bool> Handle(DeleteCustomerAddressCommand request, CancellationToken cancellationToken)
        {
            var address = await _addressRepository.GetByIdAsync(request.AddressId);
            if (address == null)
            {
                return false; // Or throw an exception if preferred
            }

            if(address.IsDefault)
            {
                return false; // Or throw an exception if preferred
            }

            await _addressRepository.DeleteAsync(request.AddressId);
            return true;
        }
    }
}
