using MediatR;
using SK.CRM.Application.Interfaces;

namespace SK.CRM.Application.Features.Prospects.Commands
{
    public sealed record DeleteProspectAddressCommand(Guid AddressId) : IRequest<bool>;


    public class DeleteProspectAddressCommandHandler : IRequestHandler<DeleteProspectAddressCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteProspectAddressCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteProspectAddressCommand request, CancellationToken cancellationToken)
        {
            var address = await _unitOfWork.AddressRepository.GetByIdAsync(request.AddressId);
            if (address == null)
            {
                return false; // Or throw an exception if preferred
            }

            if(address.IsDefault)
            {
                return false; // Or throw an exception if preferred
            }

            await _unitOfWork.AddressRepository.DeleteAsync(request.AddressId);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
