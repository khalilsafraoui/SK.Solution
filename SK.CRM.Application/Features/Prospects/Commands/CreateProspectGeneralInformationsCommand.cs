using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SK.CRM.Application.DTOs;
using SK.CRM.Application.Interfaces;
using SK.CRM.Domain.Entities;

namespace SK.CRM.Application.Features.Prospects.Commands
{
    public sealed record CreateProspectGeneralInformationsCommand(ProspectGeneralInformationsDto Customer) : IRequest<(bool IsSuccess, ProspectGeneralInformationsDto? ProspectGeneralInformationsDto, string ErrorMessage)>;
    public class CreateProspectGeneralInformationsCommandHandler : IRequestHandler<CreateProspectGeneralInformationsCommand, (bool IsSuccess, ProspectGeneralInformationsDto? ProspectGeneralInformationsDto, string ErrorMessage)>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateProspectGeneralInformationsCommandHandler> _logger;
        public CreateProspectGeneralInformationsCommandHandler(IUnitOfWork unitOfWork, IMapper mapper,ILogger<CreateProspectGeneralInformationsCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<(bool IsSuccess, ProspectGeneralInformationsDto? ProspectGeneralInformationsDto, string ErrorMessage)> Handle(CreateProspectGeneralInformationsCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var customer = _mapper.Map<Customer>(request.Customer);
                customer.IsProspect = true;
                customer = await _unitOfWork.CustomerRepository.CreateAsync(customer);
                await _unitOfWork.SaveChangesAsync();
                // Map back to DTO after creation to include any updates (e.g., ID)
                return (true, _mapper.Map<ProspectGeneralInformationsDto>(customer), string.Empty);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while handling CreateProspectGeneralInformationsCommand: {Message}", ex.Message);
                return (false, null, "An unexpected error occurred while creating the prospect. Please try again later.");

            }
        }
    }
}
