using AutoMapper;
using MediatR;
using SK.CRM.Application.DTOs;
using SK.CRM.Application.Interfaces;
using SK.CRM.Domain.Entities;

namespace SK.CRM.Application.Features.Customers.Commands
{
    public sealed record CreateCustomerGeneralInformationsCommand(CustomerGeneralInformationsDto Customer) : IRequest<CustomerGeneralInformationsDto>;
    public class CreateCustomerGeneralInformationsHandler : IRequestHandler<CreateCustomerGeneralInformationsCommand, CustomerGeneralInformationsDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateCustomerGeneralInformationsHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CustomerGeneralInformationsDto> Handle(CreateCustomerGeneralInformationsCommand request, CancellationToken cancellationToken)
        {
            var customer = _mapper.Map<Customer>(request.Customer);
            customer = await _unitOfWork.CustomerRepository.CreateAsync(customer);
            await _unitOfWork.SaveChangesAsync();
            // Map back to DTO after creation to include any updates (e.g., ID)
            return _mapper.Map<CustomerGeneralInformationsDto>(customer);
        }
    }
}
