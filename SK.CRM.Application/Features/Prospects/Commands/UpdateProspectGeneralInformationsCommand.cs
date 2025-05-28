using AutoMapper;
using MediatR;
using SK.CRM.Application.DTOs;
using SK.CRM.Application.Exceptions;
using SK.CRM.Application.Interfaces;
using SK.CRM.Domain.Entities;

namespace SK.CRM.Application.Features.Customers.Commands
{
    public sealed record UpdateCustomerGeneralInformationsCommand(CustomerGeneralInformationsDto Customer) : IRequest<CustomerGeneralInformationsDto>;

    public class UpdateCustomerGeneralInformationsCommandHandler : IRequestHandler<UpdateCustomerGeneralInformationsCommand, CustomerGeneralInformationsDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateCustomerGeneralInformationsCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CustomerGeneralInformationsDto> Handle(UpdateCustomerGeneralInformationsCommand request, CancellationToken cancellationToken)
        {
            var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(request.Customer.Id)
                            ?? throw new NotFoundException(nameof(Customer), request.Customer.Id);

            _mapper.Map(request.Customer, customer);

            var updated = await _unitOfWork.CustomerRepository.UpdateAsync(customer);
            await _unitOfWork.SaveChangesAsync();
            if (!updated)
            {
                throw new ApplicationException("Failed to update customer.");
            }

            return _mapper.Map<CustomerGeneralInformationsDto>(customer);
        }
    }

}
