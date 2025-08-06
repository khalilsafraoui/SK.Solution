using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SK.CRM.Application.DTOs;
using SK.CRM.Application.Interfaces;
using SK.CRM.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace SK.CRM.Application.Features.Prospects.Commands
{
    public sealed record ConvertToCustomerCommand(ProspectGeneralInformationsDto Customer, List<AddressDto> AddressDtos) : IRequest<(bool IsSuccess, string ErrorMessage)>;

    public class ConvertToCustomerCommandHandler : IRequestHandler<ConvertToCustomerCommand, (bool IsSuccess, string ErrorMessage)>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<ConvertToCustomerCommandHandler> _logger;
        public ConvertToCustomerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<ConvertToCustomerCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<(bool IsSuccess, string ErrorMessage)> Handle(ConvertToCustomerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(request.Customer.Id);
                if (customer == null)
                    return (false, "Prospect not found.");

                if (!customer.IsProspect)
                {
                    return (false, "This prospect is already a customer.");
                }

                _mapper.Map(request.Customer, customer);
                await Validate(customer, request.AddressDtos);
                customer.IsProspect = false; // Mark as customer
                var updated = await _unitOfWork.CustomerRepository.UpdateAsync(customer);
                await _unitOfWork.SaveChangesAsync();
                if (!updated)
                {
                    return (false, "Failed to convert prospect to customer.");
                }
                return (true, string.Empty);
            }
            catch (ValidationException valEx)
            {
                return (false, $"Validation failed: {valEx.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while converting the prospect: {Message}", ex.Message);
                return (false, "An unexpected error occurred while converting the prospect. Please try again later.");
            }
        }

        private async Task Validate(Customer customer, List<AddressDto> addresses)
        {
            if (string.IsNullOrEmpty(customer.FirstName))
            {
                throw new ValidationException("First Name is required.");
            }

            if (string.IsNullOrEmpty(customer.LastName))
            {
                throw new ValidationException("Last Name is required.");
            }

            if (string.IsNullOrEmpty(customer.Email))
            {
                throw new ValidationException("Email is required.");
            }

            if (string.IsNullOrEmpty(customer.PhoneNumber))
            {
                throw new ValidationException("Phone Number is required.");
            }

            if (addresses.Count == 0)
            {
                throw new ValidationException("Customer should have at least one addresses");
            }

        }
    }
}
