using MediatR;
using SK.CRM.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SK.CRM.Application.Features.Prospects.Commands
{
    public sealed record DeleteProspectAddressCommand(Guid AddressId) : IRequest<bool>;


    public class DeleteProspectAddressCommandHandler : IRequestHandler<DeleteProspectAddressCommand, bool>
    {
        private readonly IAddressRepository _addressRepository;

        public DeleteProspectAddressCommandHandler(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        public async Task<bool> Handle(DeleteProspectAddressCommand request, CancellationToken cancellationToken)
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
