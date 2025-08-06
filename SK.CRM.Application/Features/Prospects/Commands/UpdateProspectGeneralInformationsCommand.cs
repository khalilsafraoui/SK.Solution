using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SK.CRM.Application.DTOs;
using SK.CRM.Application.Interfaces;

namespace SK.CRM.Application.Features.Prospects.Commands
{
    public sealed record UpdateProspectGeneralInformationsCommand(ProspectGeneralInformationsDto Customer) : IRequest<(bool IsSuccess, ProspectGeneralInformationsDto? ProspectGeneralInformationsDto, string ErrorMessage)>;

    public class UpdateProspectGeneralInformationsCommandHandler : IRequestHandler<UpdateProspectGeneralInformationsCommand, (bool IsSuccess, ProspectGeneralInformationsDto? ProspectGeneralInformationsDto, string ErrorMessage)>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateProspectGeneralInformationsCommandHandler> _logger;
        public UpdateProspectGeneralInformationsCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UpdateProspectGeneralInformationsCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<(bool IsSuccess, ProspectGeneralInformationsDto? ProspectGeneralInformationsDto, string ErrorMessage)> Handle(UpdateProspectGeneralInformationsCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(request.Customer.Id);
                if (customer == null)
                    return (false, null, "Prospect not found.");

                _mapper.Map(request.Customer, customer);

                var updated = await _unitOfWork.CustomerRepository.UpdateAsync(customer);
                await _unitOfWork.SaveChangesAsync();
                if (!updated)
                {
                    return (false, null, "Failed to update Prospect.");
                }
                return (true, _mapper.Map<ProspectGeneralInformationsDto>(customer), string.Empty);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the Prospect: {Message}", ex.Message);
                return (false, null, "An unexpected error occurred while updating the Prospect. Please try again later.");
            }
        }
    }

}
