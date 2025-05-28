using AutoMapper;
using MediatR;
using SK.CRM.Application.DTOs;
using SK.CRM.Application.Interfaces;
using SK.CRM.Domain.Entities;

namespace SK.CRM.Application.Features.Prospects.Commands
{
    public sealed record CreateProspectGeneralInformationsCommand(ProspectGeneralInformationsDto Customer) : IRequest<ProspectGeneralInformationsDto>;
    public class CreateProspectGeneralInformationsCommandHandler : IRequestHandler<CreateProspectGeneralInformationsCommand, ProspectGeneralInformationsDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateProspectGeneralInformationsCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ProspectGeneralInformationsDto> Handle(CreateProspectGeneralInformationsCommand request, CancellationToken cancellationToken)
        {
            var customer = _mapper.Map<Customer>(request.Customer);
            customer.IsProspect = true;
            customer = await _unitOfWork.CustomerRepository.CreateAsync(customer);
            await _unitOfWork.SaveChangesAsync();
            // Map back to DTO after creation to include any updates (e.g., ID)
            return _mapper.Map<ProspectGeneralInformationsDto>(customer);
        }
    }
}
