using AutoMapper;
using MediatR;
using SK.CRM.Application.DTOs;
using SK.CRM.Application.Interfaces;

namespace SK.CRM.Application.Features.Customers.Queries
{
    public sealed record GetCustomerGeneralInformationsByIdQuery(Guid Id) : IRequest<CustomerGeneralInformationsDto?>;

    public sealed class GetCustomerGeneralInformationsByIdQueryHandler : IRequestHandler<GetCustomerGeneralInformationsByIdQuery, CustomerGeneralInformationsDto?>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetCustomerGeneralInformationsByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CustomerGeneralInformationsDto?> Handle(GetCustomerGeneralInformationsByIdQuery request, CancellationToken cancellationToken)
        {
            var customer = await _unitOfWork.CustomerRepository.GetCustomerByIdAsync(request.Id);
            return customer is null ? null : _mapper.Map<CustomerGeneralInformationsDto>(customer);
        }
    }
}
